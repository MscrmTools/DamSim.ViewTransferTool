namespace DamSim.ViewTransferTool
{
    partial class ViewTransferTool
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewTransferTool));
            this.gbEntities = new System.Windows.Forms.GroupBox();
            this.lvEntities = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbSourceViews = new System.Windows.Forms.GroupBox();
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.chkShowActiveViews = new System.Windows.Forms.CheckBox();
            this.lvSourceViews = new System.Windows.Forms.ListView();
            this.allViewName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.allViewType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.allViewIsActive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.viewImageList = new System.Windows.Forms.ImageList(this.components);
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadEntities = new System.Windows.Forms.ToolStripButton();
            this.tsbTransferViews = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPublishEntity = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPublishAll = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTarget = new System.Windows.Forms.Label();
            this.gbSourceViewLayout = new System.Windows.Forms.GroupBox();
            this.lvSourceViewLayoutPreview = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbSourceValue = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbTargetValue = new System.Windows.Forms.Label();
            this.llSelectTarget = new System.Windows.Forms.LinkLabel();
            this.gbEntities.SuspendLayout();
            this.gbSourceViews.SuspendLayout();
            this.gbFilters.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.gbSourceViewLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEntities
            // 
            this.gbEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbEntities.Controls.Add(this.lvEntities);
            this.gbEntities.Enabled = false;
            this.gbEntities.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.gbEntities.Location = new System.Drawing.Point(4, 118);
            this.gbEntities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEntities.Name = "gbEntities";
            this.gbEntities.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEntities.Size = new System.Drawing.Size(418, 801);
            this.gbEntities.TabIndex = 92;
            this.gbEntities.TabStop = false;
            this.gbEntities.Text = "Entities";
            // 
            // lvEntities
            // 
            this.lvEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvEntities.FullRowSelect = true;
            this.lvEntities.HideSelection = false;
            this.lvEntities.Location = new System.Drawing.Point(9, 31);
            this.lvEntities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.Size = new System.Drawing.Size(398, 759);
            this.lvEntities.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvEntities.TabIndex = 79;
            this.lvEntities.UseCompatibleStateImageBehavior = false;
            this.lvEntities.View = System.Windows.Forms.View.Details;
            this.lvEntities.SelectedIndexChanged += new System.EventHandler(this.lvEntities_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Display name";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Logical name";
            this.columnHeader2.Width = 100;
            // 
            // gbSourceViews
            // 
            this.gbSourceViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSourceViews.Controls.Add(this.gbFilters);
            this.gbSourceViews.Controls.Add(this.lvSourceViews);
            this.gbSourceViews.Enabled = false;
            this.gbSourceViews.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.gbSourceViews.Location = new System.Drawing.Point(438, 118);
            this.gbSourceViews.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbSourceViews.Name = "gbSourceViews";
            this.gbSourceViews.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbSourceViews.Size = new System.Drawing.Size(758, 668);
            this.gbSourceViews.TabIndex = 91;
            this.gbSourceViews.TabStop = false;
            this.gbSourceViews.Text = "Source Views";
            // 
            // gbFilters
            // 
            this.gbFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFilters.Controls.Add(this.chkShowActiveViews);
            this.gbFilters.Location = new System.Drawing.Point(9, 31);
            this.gbFilters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbFilters.Size = new System.Drawing.Size(740, 69);
            this.gbFilters.TabIndex = 64;
            this.gbFilters.TabStop = false;
            this.gbFilters.Text = "View Filters";
            // 
            // chkShowActiveViews
            // 
            this.chkShowActiveViews.AutoSize = true;
            this.chkShowActiveViews.Location = new System.Drawing.Point(8, 32);
            this.chkShowActiveViews.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkShowActiveViews.Name = "chkShowActiveViews";
            this.chkShowActiveViews.Size = new System.Drawing.Size(176, 27);
            this.chkShowActiveViews.TabIndex = 0;
            this.chkShowActiveViews.Text = "Show Active Views";
            this.chkShowActiveViews.UseVisualStyleBackColor = true;
            this.chkShowActiveViews.CheckedChanged += new System.EventHandler(this.chkShowActiveViews_CheckedChanged);
            // 
            // lvSourceViews
            // 
            this.lvSourceViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSourceViews.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.allViewName,
            this.allViewType,
            this.allViewIsActive});
            this.lvSourceViews.FullRowSelect = true;
            this.lvSourceViews.HideSelection = false;
            this.lvSourceViews.Location = new System.Drawing.Point(6, 102);
            this.lvSourceViews.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvSourceViews.Name = "lvSourceViews";
            this.lvSourceViews.Size = new System.Drawing.Size(740, 555);
            this.lvSourceViews.SmallImageList = this.viewImageList;
            this.lvSourceViews.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvSourceViews.TabIndex = 63;
            this.lvSourceViews.UseCompatibleStateImageBehavior = false;
            this.lvSourceViews.View = System.Windows.Forms.View.Details;
            this.lvSourceViews.SelectedIndexChanged += new System.EventHandler(this.lvSourceViews_SelectedIndexChanged);
            // 
            // allViewName
            // 
            this.allViewName.Text = "View Name";
            this.allViewName.Width = 350;
            // 
            // allViewType
            // 
            this.allViewType.Text = "View Type";
            this.allViewType.Width = 130;
            // 
            // allViewIsActive
            // 
            this.allViewIsActive.Text = "View State";
            this.allViewIsActive.Width = 130;
            // 
            // viewImageList
            // 
            this.viewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("viewImageList.ImageStream")));
            this.viewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.viewImageList.Images.SetKeyName(0, "ico_16_1039.gif");
            this.viewImageList.Images.SetKeyName(1, "ico_16_1039_advFind.gif");
            this.viewImageList.Images.SetKeyName(2, "ico_16_1039_associated.gif");
            this.viewImageList.Images.SetKeyName(3, "ico_16_1039_default.gif");
            this.viewImageList.Images.SetKeyName(4, "ico_16_1039_lookup.gif");
            this.viewImageList.Images.SetKeyName(5, "ico_16_1039_quickFind.gif");
            this.viewImageList.Images.SetKeyName(6, "userquery.png");
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseThisTab,
            this.toolStripSeparator2,
            this.tsbLoadEntities,
            this.tsbTransferViews,
            this.toolStripSeparator1,
            this.tsbPublishEntity,
            this.toolStripSeparator3,
            this.tsbPublishAll});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsMain.Size = new System.Drawing.Size(1200, 38);
            this.tsMain.TabIndex = 90;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbCloseThisTab
            // 
            this.tsbCloseThisTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCloseThisTab.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tsbCloseThisTab.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseThisTab.Image")));
            this.tsbCloseThisTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseThisTab.Name = "tsbCloseThisTab";
            this.tsbCloseThisTab.Size = new System.Drawing.Size(28, 35);
            this.tsbCloseThisTab.Text = "Close this tab";
            this.tsbCloseThisTab.Click += new System.EventHandler(this.tsbCloseThisTab_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // tsbLoadEntities
            // 
            this.tsbLoadEntities.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tsbLoadEntities.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadEntities.Image")));
            this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadEntities.Name = "tsbLoadEntities";
            this.tsbLoadEntities.Size = new System.Drawing.Size(135, 35);
            this.tsbLoadEntities.Text = "Load Entities";
            this.tsbLoadEntities.Click += new System.EventHandler(this.tsbLoadEntities_Click);
            // 
            // tsbTransferViews
            // 
            this.tsbTransferViews.Image = ((System.Drawing.Image)(resources.GetObject("tsbTransferViews.Image")));
            this.tsbTransferViews.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTransferViews.Name = "tsbTransferViews";
            this.tsbTransferViews.Size = new System.Drawing.Size(149, 35);
            this.tsbTransferViews.Text = "Transfer views";
            this.tsbTransferViews.Click += new System.EventHandler(this.tsbTransferViews_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // tsbPublishEntity
            // 
            this.tsbPublishEntity.Enabled = false;
            this.tsbPublishEntity.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tsbPublishEntity.Image = ((System.Drawing.Image)(resources.GetObject("tsbPublishEntity.Image")));
            this.tsbPublishEntity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPublishEntity.Name = "tsbPublishEntity";
            this.tsbPublishEntity.Size = new System.Drawing.Size(141, 35);
            this.tsbPublishEntity.Text = "Publish entity";
            this.tsbPublishEntity.Click += new System.EventHandler(this.tsbPublishEntity_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // tsbPublishAll
            // 
            this.tsbPublishAll.Enabled = false;
            this.tsbPublishAll.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tsbPublishAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbPublishAll.Image")));
            this.tsbPublishAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPublishAll.Name = "tsbPublishAll";
            this.tsbPublishAll.Size = new System.Drawing.Size(115, 35);
            this.tsbPublishAll.Text = "Publish all";
            this.tsbPublishAll.Click += new System.EventHandler(this.tsbPublishAll_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 35);
            this.label1.TabIndex = 96;
            this.label1.Text = "Source environnement";
            // 
            // lbTarget
            // 
            this.lbTarget.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbTarget.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lbTarget.Location = new System.Drawing.Point(0, 0);
            this.lbTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Size = new System.Drawing.Size(200, 35);
            this.lbTarget.TabIndex = 97;
            this.lbTarget.Text = "Target environnement";
            // 
            // gbSourceViewLayout
            // 
            this.gbSourceViewLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSourceViewLayout.Controls.Add(this.lvSourceViewLayoutPreview);
            this.gbSourceViewLayout.Enabled = false;
            this.gbSourceViewLayout.Location = new System.Drawing.Point(438, 796);
            this.gbSourceViewLayout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbSourceViewLayout.Name = "gbSourceViewLayout";
            this.gbSourceViewLayout.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbSourceViewLayout.Size = new System.Drawing.Size(758, 113);
            this.gbSourceViewLayout.TabIndex = 98;
            this.gbSourceViewLayout.TabStop = false;
            this.gbSourceViewLayout.Text = "Source view layout";
            // 
            // lvSourceViewLayoutPreview
            // 
            this.lvSourceViewLayoutPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSourceViewLayoutPreview.ForeColor = System.Drawing.Color.Black;
            this.lvSourceViewLayoutPreview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSourceViewLayoutPreview.Location = new System.Drawing.Point(9, 29);
            this.lvSourceViewLayoutPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvSourceViewLayoutPreview.Name = "lvSourceViewLayoutPreview";
            this.lvSourceViewLayoutPreview.Size = new System.Drawing.Size(738, 72);
            this.lvSourceViewLayoutPreview.TabIndex = 66;
            this.lvSourceViewLayoutPreview.UseCompatibleStateImageBehavior = false;
            this.lvSourceViewLayoutPreview.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbSourceValue);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 35);
            this.panel1.TabIndex = 99;
            // 
            // lbSourceValue
            // 
            this.lbSourceValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSourceValue.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lbSourceValue.ForeColor = System.Drawing.Color.Red;
            this.lbSourceValue.Location = new System.Drawing.Point(200, 0);
            this.lbSourceValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSourceValue.Name = "lbSourceValue";
            this.lbSourceValue.Size = new System.Drawing.Size(1000, 35);
            this.lbSourceValue.TabIndex = 97;
            this.lbSourceValue.Text = "Unselected";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.llSelectTarget);
            this.panel2.Controls.Add(this.lbTargetValue);
            this.panel2.Controls.Add(this.lbTarget);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1200, 35);
            this.panel2.TabIndex = 100;
            // 
            // lbTargetValue
            // 
            this.lbTargetValue.AutoSize = true;
            this.lbTargetValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbTargetValue.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lbTargetValue.ForeColor = System.Drawing.Color.Red;
            this.lbTargetValue.Location = new System.Drawing.Point(200, 0);
            this.lbTargetValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTargetValue.Name = "lbTargetValue";
            this.lbTargetValue.Size = new System.Drawing.Size(94, 23);
            this.lbTargetValue.TabIndex = 98;
            this.lbTargetValue.Text = "Unselected";
            // 
            // llSelectTarget
            // 
            this.llSelectTarget.AutoSize = true;
            this.llSelectTarget.Dock = System.Windows.Forms.DockStyle.Left;
            this.llSelectTarget.Location = new System.Drawing.Point(294, 0);
            this.llSelectTarget.Name = "llSelectTarget";
            this.llSelectTarget.Size = new System.Drawing.Size(51, 20);
            this.llSelectTarget.TabIndex = 99;
            this.llSelectTarget.TabStop = true;
            this.llSelectTarget.Text = "select";
            this.llSelectTarget.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSelectTarget_LinkClicked);
            // 
            // ViewTransferTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbSourceViewLayout);
            this.Controls.Add(this.gbEntities);
            this.Controls.Add(this.gbSourceViews);
            this.Controls.Add(this.tsMain);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ViewTransferTool";
            this.Size = new System.Drawing.Size(1200, 923);
            this.gbEntities.ResumeLayout(false);
            this.gbSourceViews.ResumeLayout(false);
            this.gbFilters.ResumeLayout(false);
            this.gbFilters.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.gbSourceViewLayout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEntities;
        private System.Windows.Forms.ListView lvEntities;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox gbSourceViews;
        private System.Windows.Forms.ListView lvSourceViews;
        private System.Windows.Forms.ColumnHeader allViewName;
        private System.Windows.Forms.ColumnHeader allViewType;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbLoadEntities;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbPublishEntity;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbPublishAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.GroupBox gbSourceViewLayout;
        private System.Windows.Forms.ListView lvSourceViewLayoutPreview;
        private System.Windows.Forms.ToolStripButton tsbTransferViews;
        private System.Windows.Forms.ColumnHeader allViewIsActive;
        private System.Windows.Forms.GroupBox gbFilters;
        private System.Windows.Forms.CheckBox chkShowActiveViews;
        private System.Windows.Forms.ImageList viewImageList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbSourceValue;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbTargetValue;
        private System.Windows.Forms.LinkLabel llSelectTarget;
    }
}
