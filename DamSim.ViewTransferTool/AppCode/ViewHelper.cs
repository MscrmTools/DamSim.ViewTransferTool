using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DamSim.ViewTransferTool.AppCode
{
    /// <summary>
    /// Helps to interact with Crm views
    /// </summary>
    internal class ViewHelper
    {
        #region Constants

        public const int VIEW_ADVANCEDFIND = 1;
        public const int VIEW_ASSOCIATED = 2;
        public const int VIEW_BASIC = 0;
        public const int VIEW_QUICKFIND = 4;
        public const int VIEW_SEARCH = 64;
        public const int VIEW_STATECODE_ACTIVE = 0;
        public const int VIEW_STATECODE_INACTIVE = 1;

        #endregion Constants

        /// <summary>
        /// Retrieve the list of views for a specific entity
        /// </summary>
        /// <param name="entityDisplayName">Logical name of the entity</param>
        /// <param name="entitiesCache">Entities cache</param>
        /// <param name="service">Organization Service</param>
        /// <returns>List of views</returns>
        public static List<Entity> RetrieveViews(string entityLogicalName, List<EntityMetadata> entitiesCache, IOrganizationService service)
        {
            try
            {
                EntityMetadata currentEmd = entitiesCache.First(emd => emd.LogicalName == entityLogicalName);

                var views = service.RetrieveMultiple(new QueryExpression("savedquery")
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression("returnedtypecode", ConditionOperator.Equal, currentEmd.ObjectTypeCode.Value),
                            new ConditionExpression("querytype", ConditionOperator.NotIn, new[] { 32,2048,8192,32768,131072})
                        }
                    }
                });

               return views.Entities.ToList();
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                throw new Exception("Error while retrieving views: " + errorMessage);
            }
        }

        internal static IEnumerable<Entity> RetrieveUserViews(string entityLogicalName, List<EntityMetadata> entitiesCache, IOrganizationService service)
        {
            try
            {
                EntityMetadata currentEmd = entitiesCache.Find(e => e.LogicalName == entityLogicalName);

                QueryByAttribute qba = new QueryByAttribute
                {
                    EntityName = "userquery",
                    ColumnSet = new ColumnSet(true)
                };

                qba.Attributes.AddRange("returnedtypecode", "querytype");
                qba.Values.AddRange(currentEmd.ObjectTypeCode.Value, 0);

                EntityCollection views = service.RetrieveMultiple(qba);

                List<Entity> viewsList = new List<Entity>();

                foreach (Entity entity in views.Entities)
                {
                    viewsList.Add(entity);
                }

                return viewsList;
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                throw new Exception("Error while retrieving user views: " + errorMessage);
            }
        }
    }
}