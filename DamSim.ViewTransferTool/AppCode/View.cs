using DamSim.ViewTransferTool.Forms;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DamSim.ViewTransferTool.AppCode
{
    public class View
    {
        private static List<EntityMetadata> sourceEntitiesMetadata;
        private static Guid targetCurrentUserId;
        private static List<EntityMetadata> targetEntitiesMetadata;
        private readonly Entity originalRecord;
        private readonly Entity record;
        private readonly IOrganizationService sourceService;
        private readonly IOrganizationService targetService;
        private int stateCode;

        public View(Entity record, IOrganizationService sourceService, IOrganizationService targetService)
        {
            if (sourceEntitiesMetadata == null)
            {
                sourceEntitiesMetadata = new List<EntityMetadata>();
            }

            if (targetEntitiesMetadata == null)
            {
                targetEntitiesMetadata = new List<EntityMetadata>();
            }

            if (targetCurrentUserId == Guid.Empty)
            {
                targetCurrentUserId = ((WhoAmIResponse)targetService.Execute(new WhoAmIRequest())).UserId;
            }

            originalRecord = record;
            this.sourceService = sourceService;
            this.targetService = targetService;

            // Clone view record
            this.record = new Entity(record.LogicalName) { Id = record.Id };
            foreach (var attribute in record.Attributes)
            {
                this.record[attribute.Key] = attribute.Value;
            }
        }

        public void Transfer(TransferType type, UserControl ctrl)
        {
            TransformObjectTypeCode();
            TransformOwner(type, ctrl);
            GetState();
            DoTransfer();
            ApplyState();
            MoveTranslations();
        }

        private void ApplyState()
        {
            if (stateCode == 1)
            {
                targetService.Update(new Entity(record.LogicalName)
                {
                    Id = record.Id,
                    Attributes =
                    {
                        {"statecode", new OptionSetValue(1) },
                        {"statuscode", new OptionSetValue(-1) }
                    }
                });
            }
        }

        private void CleanEntity(bool create)
        {
            var targetEntityMetadata = targetEntitiesMetadata.FirstOrDefault(emd => emd.LogicalName == record.LogicalName);
            if (targetEntityMetadata == null || targetEntityMetadata.Attributes == null)
            {
                targetEntityMetadata = MetadataHelper.GetEntityMetadata(record.LogicalName, EntityFilters.Entity | EntityFilters.Attributes, targetService);

                var existingEmd = targetEntitiesMetadata.FirstOrDefault(emd => emd.LogicalName == record.LogicalName);
                if (existingEmd != null)
                {
                    targetEntitiesMetadata.Remove(existingEmd);
                }

                targetEntitiesMetadata.Add(targetEntityMetadata);
            }

            foreach (var attribute in targetEntityMetadata.Attributes)
            {
                if (create && !attribute.IsValidForCreate.Value && record.Contains(attribute.LogicalName))
                {
                    record.Attributes.Remove(attribute.LogicalName);
                }

                if (!create && !attribute.IsValidForUpdate.Value && record.Contains(attribute.LogicalName))
                {
                    record.Attributes.Remove(attribute.LogicalName);
                }
            }

            record.Attributes.Remove("statecode");
            record.Attributes.Remove("statuscode");
        }

        private void DoTransfer()
        {
            // Check if the view already exists on target organization
            var targetView = targetService.RetrieveMultiple(new QueryExpression(record.LogicalName)
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression(record.LogicalName + "id", ConditionOperator.Equal, record.Id)
                    }
                }
            }).Entities.FirstOrDefault();

            if (targetView != null)
            {
                // We need to update the existing view
                CleanEntity(false);
                targetService.Update(record);
            }
            else
            {
                CleanEntity(true);

                var owner = record.GetAttributeValue<EntityReference>("ownerid");
                if (owner != null && (owner.LogicalName == "team" || owner.Id != targetCurrentUserId))
                {
                    // Need to create the view for the current user
                    record.Attributes.Remove("ownerid");
                    record.Id = targetService.Create(record);
                    record[record.LogicalName + "id"] = record.Id;

                    // Share it with ourselves
                    GrantAccessRequest grantRequest = new GrantAccessRequest()
                    {
                        Target = record.ToEntityReference(),
                        PrincipalAccess = new PrincipalAccess
                        {
                            Principal = new EntityReference("systemuser", targetCurrentUserId),
                            AccessMask = AccessRights.WriteAccess | AccessRights.ReadAccess
                        }
                    };

                    // and then assign it to the team or user
                    record["ownerid"] = owner;
                    targetService.Update(record);
                }
                else
                {
                    targetService.Create(record);
                }
            }
        }

        private void GetState()
        {
            stateCode = record.GetAttributeValue<OptionSetValue>("statecode").Value;
        }

        private void MoveTranslations()
        {
            if (record.LogicalName == "userquery") return;

            var response = (RetrieveLocLabelsResponse)sourceService.Execute(new RetrieveLocLabelsRequest
            {
                AttributeName = "name",
                EntityMoniker = record.ToEntityReference()
            });

            if (response.Label.LocalizedLabels.Count == 1) return;

            targetService.Execute(new SetLocLabelsRequest
            {
                AttributeName = "name",
                EntityMoniker = record.ToEntityReference(),
                Labels = response.Label.LocalizedLabels.ToArray()
            });
        }

        private void TransformObjectTypeCode()
        {
            var targetEntityMetadata = targetEntitiesMetadata.FirstOrDefault(emd => emd.LogicalName == record.GetAttributeValue<string>("returnedtypecode"));
            if (targetEntityMetadata == null)
            {
                targetEntityMetadata = MetadataHelper.GetEntityMetadata(record.GetAttributeValue<string>("returnedtypecode"), EntityFilters.Entity, targetService);
                targetEntitiesMetadata.Add(targetEntityMetadata);
            }

            XDocument layoutDoc = XDocument.Parse(record.GetAttributeValue<string>("layoutxml"));
            layoutDoc.Descendants("grid").Single().Attribute("object").Value = targetEntityMetadata.ObjectTypeCode.Value.ToString(CultureInfo.InvariantCulture);

            record["layoutxml"] = layoutDoc.ToString();
        }

        private void TransformOwner(TransferType type, UserControl ctrl)
        {
            if (record.LogicalName == "userquery")
            {
                EntityReference targetReference;
                string name;
                var sourceOwnerRef = record.GetAttributeValue<EntityReference>("ownerid");

                if (sourceOwnerRef.LogicalName == "systemuser")
                {
                    var sourceUser = sourceService.Retrieve("systemuser", sourceOwnerRef.Id, new ColumnSet("domainname", "fullname"));
                    name = sourceUser.GetAttributeValue<string>("domainname");
                    var fullname = sourceUser.GetAttributeValue<string>("fullname");

                    if (type != TransferType.Same)
                    {
                        var cupDialog = new CrmUserPickerForm(targetService, record.GetAttributeValue<string>("name"), fullname, name);
                        if (cupDialog.ShowDialog(ctrl) != DialogResult.OK)
                        {
                            throw new Exception("It is mandatory to select a target user");
                        }

                        targetReference = cupDialog.SelectedUser.ToEntityReference();
                    }
                    else
                    {
                        // Let's find the user based on systemuserid or domainname
                        var targetUser = targetService.RetrieveMultiple(new QueryExpression("systemuser")
                        {
                            Criteria = new FilterExpression(LogicalOperator.Or)
                            {
                                Conditions =
                                {
                                    new ConditionExpression("domainname", ConditionOperator.Equal,
                                        name ?? "dummyValueNotExpectedAsDomainNameToAvoidSystemAccount"),
                                    new ConditionExpression("systemuserid", ConditionOperator.Equal, sourceOwnerRef.Id),
                                }
                            }
                        }).Entities.FirstOrDefault();

                        targetReference = targetUser?.ToEntityReference();
                    }
                }
                else
                {
                    var sourceTeam = sourceService.Retrieve("team", sourceOwnerRef.Id, new ColumnSet("name"));
                    name = sourceTeam.GetAttributeValue<string>("name");

                    // Let's find the team based on id or name
                    var targetTeam = targetService.RetrieveMultiple(new QueryExpression("team")
                    {
                        Criteria = new FilterExpression(LogicalOperator.Or)
                        {
                            Conditions =
                                    {
                                        new ConditionExpression("name", ConditionOperator.Equal, name),
                                        new ConditionExpression("teamid", ConditionOperator.Equal, sourceTeam.Id),
                                    }
                        }
                    }).Entities.FirstOrDefault();

                    targetReference = targetTeam?.ToEntityReference();
                }

                if (targetReference != null)
                {
                    record["ownerid"] = targetReference;
                }
                else
                {
                    throw new Exception(string.Format(
                        "Unable to find a user or team in the target organization with name '{0}' or id '{1}'",
                        name,
                        record.GetAttributeValue<EntityReference>("ownerid").Id));
                }
            }
        }
    }
}