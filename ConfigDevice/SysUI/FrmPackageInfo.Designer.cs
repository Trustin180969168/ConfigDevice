namespace ConfigDevice
{
    partial class FrmPackageInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gcPackage = new DevExpress.XtraGrid.GridControl();
            this.gvPackage = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.packageId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.packageKindID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.packageProperty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.packagePort = new DevExpress.XtraGrid.Columns.GridColumn();
            this.packageUserProtocol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.packageUserData = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rteData = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.packageAdd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btStop = new System.Windows.Forms.ToolStripButton();
            this.btAutoRelesh = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rteData)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcPackage
            // 
            this.gcPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPackage.EmbeddedNavigator.Name = "";
            this.gcPackage.FormsUseDefaultLookAndFeel = false;
            this.gcPackage.Location = new System.Drawing.Point(0, 31);
            this.gcPackage.MainView = this.gvPackage;
            this.gcPackage.Name = "gcPackage";
            this.gcPackage.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rteData});
            this.gcPackage.Size = new System.Drawing.Size(792, 542);
            this.gcPackage.TabIndex = 7;
            this.gcPackage.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPackage});
            // 
            // gvPackage
            // 
            this.gvPackage.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.packageId,
            this.packageKindID,
            this.packageProperty,
            this.packagePort,
            this.packageUserProtocol,
            this.packageUserData,
            this.packageAdd,
            this.gridColumn3,
            this.gridColumn4});
            this.gvPackage.GridControl = this.gcPackage;
            this.gvPackage.Name = "gvPackage";
            this.gvPackage.OptionsView.RowAutoHeight = true;
            this.gvPackage.OptionsView.ShowGroupPanel = false;
            // 
            // packageId
            // 
            this.packageId.AppearanceCell.Options.UseTextOptions = true;
            this.packageId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packageId.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageId.AppearanceHeader.Options.UseTextOptions = true;
            this.packageId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packageId.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageId.Caption = "包标识码";
            this.packageId.Name = "packageId";
            this.packageId.OptionsColumn.ReadOnly = true;
            this.packageId.Visible = true;
            this.packageId.VisibleIndex = 0;
            // 
            // packageKindID
            // 
            this.packageKindID.AppearanceCell.Options.UseTextOptions = true;
            this.packageKindID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packageKindID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageKindID.AppearanceHeader.Options.UseTextOptions = true;
            this.packageKindID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packageKindID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageKindID.Caption = "包数据类";
            this.packageKindID.Name = "packageKindID";
            this.packageKindID.OptionsColumn.ReadOnly = true;
            this.packageKindID.Visible = true;
            this.packageKindID.VisibleIndex = 1;
            // 
            // packageProperty
            // 
            this.packageProperty.AppearanceCell.Options.UseTextOptions = true;
            this.packageProperty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packageProperty.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageProperty.AppearanceHeader.Options.UseTextOptions = true;
            this.packageProperty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packageProperty.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageProperty.Caption = "包属性";
            this.packageProperty.Name = "packageProperty";
            this.packageProperty.OptionsColumn.ReadOnly = true;
            this.packageProperty.Visible = true;
            this.packageProperty.VisibleIndex = 2;
            // 
            // packagePort
            // 
            this.packagePort.AppearanceCell.Options.UseTextOptions = true;
            this.packagePort.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packagePort.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packagePort.AppearanceHeader.Options.UseTextOptions = true;
            this.packagePort.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packagePort.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packagePort.Caption = "发送端口";
            this.packagePort.Name = "packagePort";
            this.packagePort.OptionsColumn.ReadOnly = true;
            this.packagePort.Visible = true;
            this.packagePort.VisibleIndex = 3;
            // 
            // packageUserProtocol
            // 
            this.packageUserProtocol.AppearanceCell.Options.UseTextOptions = true;
            this.packageUserProtocol.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packageUserProtocol.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageUserProtocol.AppearanceHeader.Options.UseTextOptions = true;
            this.packageUserProtocol.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packageUserProtocol.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageUserProtocol.Caption = "用户协议";
            this.packageUserProtocol.Name = "packageUserProtocol";
            this.packageUserProtocol.OptionsColumn.ReadOnly = true;
            this.packageUserProtocol.Visible = true;
            this.packageUserProtocol.VisibleIndex = 4;
            // 
            // packageUserData
            // 
            this.packageUserData.AppearanceCell.Options.UseTextOptions = true;
            this.packageUserData.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageUserData.AppearanceHeader.Options.UseTextOptions = true;
            this.packageUserData.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packageUserData.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageUserData.Caption = "用户数据";
            this.packageUserData.ColumnEdit = this.rteData;
            this.packageUserData.Name = "packageUserData";
            this.packageUserData.OptionsColumn.ReadOnly = true;
            this.packageUserData.Visible = true;
            this.packageUserData.VisibleIndex = 5;
            // 
            // rteData
            // 
            this.rteData.Appearance.Options.UseTextOptions = true;
            this.rteData.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.rteData.AppearanceFocused.Options.UseTextOptions = true;
            this.rteData.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.rteData.AppearanceReadOnly.Options.UseTextOptions = true;
            this.rteData.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.rteData.Name = "rteData";
            this.rteData.ReadOnly = true;
            // 
            // packageAdd
            // 
            this.packageAdd.AppearanceCell.Options.UseTextOptions = true;
            this.packageAdd.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packageAdd.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageAdd.AppearanceHeader.Options.UseTextOptions = true;
            this.packageAdd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.packageAdd.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.packageAdd.Caption = "校验";
            this.packageAdd.Name = "packageAdd";
            this.packageAdd.OptionsColumn.ReadOnly = true;
            this.packageAdd.Visible = true;
            this.packageAdd.VisibleIndex = 6;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.Caption = "gridColumn3";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "gridColumn4";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btStop,
            this.btAutoRelesh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 31);
            this.toolStrip1.TabIndex = 16;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btStop
            // 
            this.btStop.Image = global::ConfigDevice.Properties.Resources.Point1;
            this.btStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(92, 28);
            this.btStop.Text = "暂停更新";
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // btAutoRelesh
            // 
            this.btAutoRelesh.Image = global::ConfigDevice.Properties.Resources.Point1;
            this.btAutoRelesh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btAutoRelesh.Name = "btAutoRelesh";
            this.btAutoRelesh.Size = new System.Drawing.Size(92, 28);
            this.btAutoRelesh.Text = "自动更新";
            this.btAutoRelesh.Click += new System.EventHandler(this.btAutoRelesh_Click);
            // 
            // FrmPackageInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.gcPackage);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmPackageInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "包信息";
            this.Load += new System.EventHandler(this.FrmPackageInfo_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPackageInfo_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gcPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rteData)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcPackage;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPackage;
        private DevExpress.XtraGrid.Columns.GridColumn packageId;
        private DevExpress.XtraGrid.Columns.GridColumn packageKindID;
        private DevExpress.XtraGrid.Columns.GridColumn packageProperty;
        private DevExpress.XtraGrid.Columns.GridColumn packagePort;
        private DevExpress.XtraGrid.Columns.GridColumn packageUserData;
        private DevExpress.XtraGrid.Columns.GridColumn packageUserProtocol;
        private DevExpress.XtraGrid.Columns.GridColumn packageAdd;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit rteData;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btStop;
        private System.Windows.Forms.ToolStripButton btAutoRelesh;
    }
}