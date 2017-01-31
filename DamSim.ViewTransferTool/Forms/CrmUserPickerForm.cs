using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using DamSim.ViewTransferTool.AppCode;
using Microsoft.Xrm.Sdk.Query;

namespace DamSim.ViewTransferTool.Forms
{
    /// <summary>
    /// Class that display the control to select CRM user.
    /// </summary>
    public partial class CrmUserPickerForm : Form
    {
        #region Variables

        /// <summary>
        /// List of selected users
        /// </summary>
        public Entity SelectedUser;

        /// <summary>
        /// CRM access object
        /// </summary>
        private readonly IOrganizationService service;
        private readonly string userFullname;
        private readonly string userName;
        private readonly string viewName;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class CrmUserPickerForm
        /// </summary>
        /// <param name="service"></param>
        /// <param name="viewName"></param>
        /// <param name="userFullname"></param>
        /// <param name="userName"></param>
        public CrmUserPickerForm(IOrganizationService service, string viewName, string userFullname, string userName)
        {
            InitializeComponent();

            this.service = service;
            this.userFullname = userFullname;
            this.userName = userName;
            this.viewName = viewName;

            lblHeaderDesc.Text =
                $"The user view '{viewName}' needs to be mapped to a user in the target organization (source user : {userFullname}";
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Action when user clicks on the cancel button
        /// </summary>
        private void ButtonCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Action when the user click on the button "Search"
        /// </summary>
        private void ButtonSearchClick(object sender, EventArgs e)
        {
            lvUsers.Items.Clear();

            var qe = new QueryExpression("systemuser")
            {
                ColumnSet =
                    new ColumnSet("systemuserid", "fullname", "lastname", "firstname", "domainname", "businessunitid"),
                Orders =
                {
                    new OrderExpression("lastname", OrderType.Ascending)
                },
                Criteria = new FilterExpression
                {
                    Filters =
                    {
                        new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("isdisabled", ConditionOperator.Equal, false)
                            }
                        },
                        new FilterExpression(LogicalOperator.Or)
                    }
                }
            };

            if (txtSearchFilter.Text.Length > 0)
            {
                bool isGuid = false;

                Guid g;
                if (Guid.TryParse(txtSearchFilter.Text, out g))
                {
                     isGuid = true;
                }

                if (isGuid)
                {
                    var ce = new ConditionExpression("systemuserid", ConditionOperator.Equal, g.ToString("B"));
                    qe.Criteria.Filters[0].Filters[1].AddCondition(ce);
                }
                else
                {
                    var ce = new ConditionExpression("fullname", ConditionOperator.Like, txtSearchFilter.Text.Replace("*", "%"));
                    var ce2 = new ConditionExpression("firstname", ConditionOperator.Like, txtSearchFilter.Text.Replace("*", "%"));
                    var ce3 = new ConditionExpression("lastname", ConditionOperator.Like, txtSearchFilter.Text.Replace("*", "%"));
                    var ce4 = new ConditionExpression("domainname", ConditionOperator.Like, txtSearchFilter.Text.Replace("*", "%"));

                    qe.Criteria.Filters[1].AddCondition(ce);
                    qe.Criteria.Filters[1].AddCondition(ce2);
                    qe.Criteria.Filters[1].AddCondition(ce3);
                    qe.Criteria.Filters[1].AddCondition(ce4);
                }
            }

            foreach (var user in service.RetrieveMultiple(qe).Entities)
            {
                var item = new ListViewItem(user.GetAttributeValue<string>("lastname")) { Tag = user.Id };
                item.SubItems.Add(user.GetAttributeValue<string>("firstname"));
                item.SubItems.Add(user.GetAttributeValue<string>("domainname"));
                item.SubItems.Add(user.GetAttributeValue<EntityReference>("businessunitid").Name);
                item.ImageIndex = 0;
                item.Tag = user;
                lvUsers.Items.Add(item);
            }
        }

        /// <summary>
        /// Action when user clicks on the validation button
        /// </summary>
        private void ButtonValidateClick(object sender, EventArgs e)
        {
            if (lvUsers.SelectedItems.Count == 0) return;

            SelectedUser = (Entity)lvUsers.SelectedItems[0].Tag;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Action when user press a keyboard key
        /// </summary>
        private void TxtSearchFilterKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ButtonSearchClick(null, null);
            }
        }

        #endregion Methods

        private void ListViewUsersMouseDoubleClick(object sender, MouseEventArgs e)
        {
            ButtonValidateClick(null, null);
        }

        private void LvUsersColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvUsers.Sorting = lvUsers.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvUsers.ListViewItemSorter = new ListViewItemComparer(e.Column, lvUsers.Sorting);
        }
    }
}