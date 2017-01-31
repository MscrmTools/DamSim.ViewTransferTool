using DamSim.ViewTransferTool.AppCode;
using DamSim.ViewTransferTool.Forms;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace DamSim.ViewTransferTool
{
    public enum TransferType
    {
        Same,
        OnlineToOnPremise,
        OnPremiseToOnline
    }

    public partial class ViewTransferTool : UserControl, IXrmToolBoxPluginControl, IGitHubPlugin, IHelpPlugin, IStatusBarMessenger
    {
        #region Variables

        private EntityMetadata _savedQueryMetadata;

        /// <summary>
        /// List of entities
        /// </summary>
        private List<EntityMetadata> entitiesCache;

        /// <summary>
        /// Information panel
        /// </summary>
        private Panel informationPanel;

        /// <summary>
        /// Dynamics CRM 2011 organization service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// Dynamics CRM 2011 target organization service
        /// </summary>
        private IOrganizationService targetService;

        private ConnectionDetail sourceDetail;

        private ConnectionDetail targetDetail;

        #endregion Variables

        public ViewTransferTool()
        {
            InitializeComponent();
        }

        #region XrmToolbox

        public event EventHandler OnCloseTool;
        public event EventHandler OnRequestConnection;
        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public Image PluginLogo
        {
            get { return null; }
        }

        public IOrganizationService Service
        {
            get { throw new NotImplementedException(); }
        }

        public string HelpUrl
        {
            get
            {
                return "https://github.com/MscrmTools/DamSim.ViewTransferTool/wiki";
            }
        }

        public string RepositoryName
        {
            get
            {
                return "DamSim.ViewTransferTool";
            }
        }

        public string UserName
        {
            get
            {
                return "MscrmTools";
            }
        }

        public void ClosingPlugin(PluginCloseInfo info)
        {
            if (info.FormReason != CloseReason.None ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAll ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAllExceptActive)
            {
                return;
            }

            info.Cancel = MessageBox.Show(@"Are you sure you want to close this tab?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
        }

        public void UpdateConnection(IOrganizationService newService, ConnectionDetail connectionDetail, string actionName = "", object parameter = null)
        {
            if (actionName == "TargetOrganization")
            {
                targetService = newService;
                targetDetail = connectionDetail;
                SetConnectionLabel(connectionDetail, "Target");
            }
            else
            {
                service = newService;
                sourceDetail = connectionDetail;
                SetConnectionLabel(connectionDetail, "Source");
            }
        }

        public string GetCompany()
        {
            return GetType().GetCompany();
        }

        public string GetMyType()
        {
            return GetType().FullName;
        }

        public string GetVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }

        #endregion XrmToolbox

        #region Form events

        private void llSelectTarget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "TargetOrganization", Control = this };
                OnRequestConnection(this, args);
            }
        }

        private void tsbCloseThisTab_Click(object sender, EventArgs e)
        {
            if (OnCloseTool != null)
            {
                const string message = "Are you sure to exit?";
                if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                    OnCloseTool(this, null);
            }
        }

        private void tsbLoadEntities_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                    {
                        ActionName = "Load",
                        Control = this
                    };
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                LoadEntities();
            }
        }
        
        private void tsbTransferViews_Click(object sender, EventArgs e)
        {
            Transfer();
        }

        private void tsbPublishEntity_Click(object sender, EventArgs e)
        {
            Publish(false);
        }

        private void tsbPublishAll_Click(object sender, EventArgs e)
        {
            Publish(true);
        }

        private void chkShowActiveViews_CheckedChanged(object sender, EventArgs e)
        {
            PopulateSourceViews();
        }

        private void lvSourceViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSourceViews.SelectedItems.Count == 0) { return; }
            if (lvSourceViews.SelectedItems.Count > 1) { return; }


            lvSourceViews.SelectedIndexChanged -= lvSourceViews_SelectedIndexChanged;

            ManageWorkingState(true);

            lvSourceViewLayoutPreview.Items.Clear();
            lvSourceViewLayoutPreview.Columns.Clear();

            var bwDisplayView = new BackgroundWorker();
            bwDisplayView.DoWork += (bw, evt) =>
            {
                // Gets current view data
                Entity currentSelectedView = (Entity)evt.Argument;
                string layoutXml = currentSelectedView.GetAttributeValue<string>("layoutxml");
                string fetchXml = currentSelectedView.GetAttributeValue<string>("fetchxml");
                EntityMetadata emdWithItems = MetadataHelper.RetrieveEntity(currentSelectedView.GetAttributeValue<string>("returnedtypecode"), service);

                XmlDocument layoutDoc = new XmlDocument();
                layoutDoc.LoadXml(layoutXml);


                ListViewItem item = new ListViewItem();
                List<ColumnHeader> headers = new List<ColumnHeader>();

                foreach (XmlNode columnNode in layoutDoc.SelectNodes("grid/row/cell"))
                {
                    int columnWidth = columnNode.Attributes["width"] == null ? 0 : int.Parse(columnNode.Attributes["width"].Value);


                    ColumnHeader header = new ColumnHeader();
                    header.Width = columnWidth;
                    header.Text = MetadataHelper.RetrieveAttributeDisplayName(emdWithItems,
                                                                              columnNode.Attributes["name"].Value,
                                                                              fetchXml, service);

                    headers.Add(header);

                    if (string.IsNullOrEmpty(item.Text))
                        item.Text = columnWidth == 0 ? "(undefined)" : (columnWidth + "px");
                    else
                        item.SubItems.Add(columnWidth == 0 ? "(undefined)" : (columnWidth + "px"));
                }

                evt.Result = new Tuple<ListViewItem, List<ColumnHeader>>(item, headers);
            };
            bwDisplayView.RunWorkerCompleted += (bw, evt) => {
                lvSourceViews.SelectedIndexChanged += lvSourceViews_SelectedIndexChanged;
                ManageWorkingState(false);

                if (evt.Error != null)
                {
                    MessageBox.Show(ParentForm, "Error while displaying view: " + evt.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var result = (Tuple<ListViewItem, List<ColumnHeader>>)evt.Result;

                    lvSourceViewLayoutPreview.Columns.AddRange(result.Item2.ToArray());
                    lvSourceViewLayoutPreview.Items.Add(result.Item1);
                }
            };
            bwDisplayView.RunWorkerAsync(lvSourceViews.SelectedItems[0].Tag);
        }

        private void lvEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetFilterControls();
            PopulateSourceViews();
        }

        #endregion Form events

        #region Methods

        private void ResetFilterControls()
        {
            chkShowActiveViews.CheckedChanged -= chkShowActiveViews_CheckedChanged;
            chkShowActiveViews.Checked = false;
            chkShowActiveViews.CheckedChanged += chkShowActiveViews_CheckedChanged;
        }

        private void SetConnectionLabel(ConnectionDetail detail, string serviceType)
        {
            switch (serviceType)
            {
                case "Source":
                    lbSourceValue.Text = detail.ConnectionName;
                    lbSourceValue.ForeColor = Color.Green;
                    break;

                case "Target":
                    lbTargetValue.Text = detail.ConnectionName;
                    lbTargetValue.ForeColor = Color.Green;
                    break;
            }
        }

        private void ManageWorkingState(bool working)
        {
            gbSourceViews.Enabled = !working;
            gbSourceViewLayout.Enabled = !working;
            gbEntities.Enabled = !working;
            tsbPublishEntity.Enabled = !working;
            tsbPublishAll.Enabled = !working;

            Cursor = working ? Cursors.WaitCursor : Cursors.Default;
        }

        private void LoadEntities()
        {
            ManageWorkingState(false);

            lvEntities.Items.Clear();
            lvSourceViews.Items.Clear();
            lvSourceViewLayoutPreview.Columns.Clear();

            informationPanel = InformationPanel.GetInformationPanel(this, "Loading entities...", 340, 120);

            var bwFillEntities = new BackgroundWorker();
            bwFillEntities.DoWork += (sender, e) => {
                _savedQueryMetadata = MetadataHelper.RetrieveEntity("savedquery", service);
                entitiesCache = MetadataHelper.RetrieveEntities(service);

                var list = new List<ListViewItem>();

                foreach (EntityMetadata emd in entitiesCache)
                {
                    var item = new ListViewItem { Text = emd.DisplayName.UserLocalizedLabel.Label, Tag = emd };
                    item.SubItems.Add(emd.LogicalName);
                    list.Add(item);
                }

                e.Result = list;
            };
            bwFillEntities.RunWorkerCompleted += (sender,e)=> {
                if (e.Error != null)
                {
                   MessageBox.Show(ParentForm, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    lvEntities.Items.AddRange(((List<ListViewItem>)e.Result).ToArray());
                }

                Controls.Remove(informationPanel);
                ManageWorkingState(false);
            };
            bwFillEntities.RunWorkerAsync();
        }
      
        private void PopulateSourceViews()
        {
            if (lvEntities.SelectedItems.Count == 0) { return; }

            var emd= (EntityMetadata)lvEntities.SelectedItems[0].Tag;

            // Reinit other controls
            lvSourceViews.Items.Clear();
            lvSourceViewLayoutPreview.Columns.Clear();
            ManageWorkingState(true);

            // Launch treatment
            var bwFillViews = new BackgroundWorker();
            bwFillViews.DoWork += (sender, e) => {
                string entityLogicalName = e.Argument.ToString();

                // Retrieve views
                List<Entity> viewsList = ViewHelper.RetrieveViews(entityLogicalName, entitiesCache, service);
                viewsList.AddRange(ViewHelper.RetrieveUserViews(entityLogicalName, entitiesCache, service));

                // Prepare list of items
                var sourceViewsList = new List<ListViewItem>();

                foreach (Entity view in viewsList)
                {
                    var item = new ListViewItem(view["name"].ToString());
                    item.Tag = view;
                    SetViewImageAndType(item);

                    if (view.Contains("statecode"))
                    {
                        int statecodeValue = ((OptionSetValue)view["statecode"]).Value;
                        switch (statecodeValue)
                        {
                            case ViewHelper.VIEW_STATECODE_ACTIVE:
                                item.SubItems.Add("Active");
                                break;

                            case ViewHelper.VIEW_STATECODE_INACTIVE:
                                item.SubItems.Add("Inactive");
                                break;
                        }
                    }

                    if (view.Contains("iscustomizable") &&
                       ((BooleanManagedProperty)view["iscustomizable"]).Value == false)
                    {
                        item.ForeColor = Color.Gray;
                        item.ToolTipText = "This view has not been defined as customizable";
                    }

                    sourceViewsList.Add(item);
                }

                e.Result = sourceViewsList;
            };
            bwFillViews.RunWorkerCompleted += (sender, e) => {
                if (e.Error != null)
                {
                    MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                else
                {
                    var items = (List<ListViewItem>)e.Result;
                    if (items.Count == 0)
                    {
                        MessageBox.Show(this, "This entity does not contain any view", "Warning", MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    else
                    {
                        lvSourceViews.Items.AddRange(items.Where(i => ShouldDisplayItem(i)).ToArray());
                    }
                }

                ManageWorkingState(false);
            };
            bwFillViews.RunWorkerAsync(emd.LogicalName);
        }
     
        private void Transfer()
        {
            if (service == null || targetService == null)
            {
                MessageBox.Show("You must select both a source and a target organization", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lvSourceViews.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select at least one view to be transfered", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ManageWorkingState(true);

            informationPanel = InformationPanel.GetInformationPanel(this, "Transfering views...", 340, 150);
            SendMessageToStatusBar(this, new StatusBarMessageEventArgs("Transfering views..."));

            var bwTransferViews = new BackgroundWorker { WorkerReportsProgress = true };
            bwTransferViews.DoWork += (sender, e) =>
            {
                var views = (List<Entity>)e.Argument;
                var errors = new List<Tuple<string, string>>();

                foreach (var viewEntity in views)
                {
                    var name = viewEntity.GetAttributeValue<string>("name");
                    var worker = (BackgroundWorker)sender;
                    worker.ReportProgress(0, "Transfering view '" + name + "'...");

                    try
                    {
                        TransferType type = TransferType.Same;
                        if (sourceDetail.UseOnline && !targetDetail.UseOnline)
                        {
                            type = TransferType.OnlineToOnPremise;
                        }
                        else if (!sourceDetail.UseOnline && targetDetail.UseOnline)
                        {
                            type = TransferType.OnPremiseToOnline;
                        }

                        var view = new AppCode.View(viewEntity, service, targetService);
                        view.Transfer(type, this);
                    }
                    catch (FaultException<OrganizationServiceFault> error)
                    {
                        if(error.HResult == -2146233087)
                        {
                            errors.Add(new Tuple<string, string>(name, "The view you tried to transfert already exists but you don't have read access to it. Get access to this view on the target organization to update it"));
                        }
                        else
                        {
                            errors.Add(new Tuple<string, string>(name, error.Message));
                        }
                    }
                }

                e.Result = errors;
            };
            bwTransferViews.RunWorkerCompleted += (sender, e) => {
                Controls.Remove(informationPanel);
                informationPanel.Dispose();
                SendMessageToStatusBar(this, new StatusBarMessageEventArgs(""));
                ManageWorkingState(false);

                var errors = (List<Tuple<string, string>>)e.Result;

                if (errors.Count > 0)
                {
                    var errorDialog = new ErrorList((List<Tuple<string, string>>)e.Result);
                    errorDialog.ShowDialog(ParentForm);
                }
            };
            bwTransferViews.ProgressChanged += (sender, e) => {
                InformationPanel.ChangeInformationPanelMessage(informationPanel, e.UserState.ToString());
                SendMessageToStatusBar(this, new StatusBarMessageEventArgs(e.UserState.ToString()));
            };
            bwTransferViews.RunWorkerAsync(lvSourceViews.SelectedItems.Cast<ListViewItem>().Select(v=>(Entity)v.Tag).ToList());
        }
     
        private void Publish(bool all)
        {
            if(lvEntities.SelectedItems.Count == 0) { return; }

            ManageWorkingState(true);

            var message = all ? "Publishing all customizations" : "Publishing entity...";
            informationPanel = InformationPanel.GetInformationPanel(this, message, 340, 120);

            var bwPublish = new BackgroundWorker();
            bwPublish.DoWork += (sender, e) => {

                if (string.IsNullOrEmpty(e.Argument.ToString()))
                {
                    var pubRequest = new PublishAllXmlRequest();
                    targetService.Execute(pubRequest);
                }
                else
                {
                    var pubRequest = new PublishXmlRequest();
                    pubRequest.ParameterXml = string.Format(@"<importexportxml>
                                                           <entities>
                                                              <entity>{0}</entity>
                                                           </entities>
                                                           <nodes/><securityroles/><settings/><workflows/>
                                                        </importexportxml>",
                                                            e.Argument.ToString());

                    targetService.Execute(pubRequest);
                }
            };
            bwPublish.RunWorkerCompleted += (sender, e) => {
                ManageWorkingState(false);
                Controls.Remove(informationPanel);
                informationPanel.Dispose();

                if (e.Error != null)
                {
                    MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            bwPublish.RunWorkerAsync(all ? "" : ((EntityMetadata)lvEntities.SelectedItems[0].Tag).LogicalName);
        }

        private void SetViewImageAndType(ListViewItem item)
        {
            var view = (Entity)item.Tag;

            switch ((int)view["querytype"])
            {
                case ViewHelper.VIEW_BASIC:
                    {
                        if (view.LogicalName == "savedquery")
                        {
                            if ((bool)view["isdefault"])
                            {
                                item.SubItems.Add("Default public view");
                                item.ImageIndex = 3;
                            }
                            else
                            {
                                item.SubItems.Add("Public view");
                                item.ImageIndex = 0;
                            }
                        }
                        else
                        {
                            item.SubItems.Add("User view");
                            item.ImageIndex = 6;
                        }
                    }
                    break;

                case ViewHelper.VIEW_ADVANCEDFIND:
                    {
                        item.SubItems.Add("Advanced find view");
                        item.ImageIndex = 1;
                    }
                    break;

                case ViewHelper.VIEW_ASSOCIATED:
                    {
                        item.SubItems.Add("Associated view");
                        item.ImageIndex = 2;
                    }
                    break;

                case ViewHelper.VIEW_QUICKFIND:
                    {
                        item.SubItems.Add("QuickFind view");
                        item.ImageIndex = 5;
                    }
                    break;

                case ViewHelper.VIEW_SEARCH:
                    {
                        item.SubItems.Add("Lookup view");
                        item.ImageIndex = 4;
                    }
                    break;
                default:
                    {
                        item.SubItems.Add(view["querytype"].ToString());
                    }
                    break;
            }
        }

        private bool ShouldDisplayItem(ListViewItem item)
        {
            var view = (Entity)item.Tag;

            if (chkShowActiveViews.Checked)
            {
                var viewStateCode = view.GetAttributeValue<OptionSetValue>("statecode").Value;
                if (viewStateCode == ViewHelper.VIEW_STATECODE_INACTIVE)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion Methods
    }
}