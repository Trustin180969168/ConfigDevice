namespace ConfigDevice
{
    partial class SelectDevice
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
            this.gcDevices = new DevExpress.XtraGrid.GridControl();
            this.gvDevices = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.deviceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceNetworkID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceKind = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceMac = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDevices)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDevices
            // 
            this.gcDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDevices.EmbeddedNavigator.Name = "";
            this.gcDevices.FormsUseDefaultLookAndFeel = false;
            this.gcDevices.Location = new System.Drawing.Point(0, 0);
            this.gcDevices.MainView = this.gvDevices;
            this.gcDevices.Name = "gcDevices";
            this.gcDevices.Size = new System.Drawing.Size(792, 573);
            this.gcDevices.TabIndex = 8;
            this.gcDevices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDevices});
            // 
            // gvDevices
            // 
            this.gvDevices.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gvDevices.Appearance.GroupRow.ForeColor = System.Drawing.Color.Blue;
            this.gvDevices.Appearance.GroupRow.Options.UseFont = true;
            this.gvDevices.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvDevices.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.deviceID,
            this.deviceNetworkID,
            this.deviceKind,
            this.deviceName,
            this.deviceMac,
            this.deviceState,
            this.deviceRemark,
            this.gridColumn13});
            this.gvDevices.CustomizationFormBounds = new System.Drawing.Rectangle(478, 386, 208, 175);
            this.gvDevices.GridControl = this.gcDevices;
            this.gvDevices.GroupCount = 1;
            this.gvDevices.Name = "gvDevices";
            this.gvDevices.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvDevices.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvDevices.OptionsView.EnableAppearanceEvenRow = true;
            this.gvDevices.OptionsView.EnableAppearanceOddRow = true;
            this.gvDevices.OptionsView.ShowGroupPanel = false;
            this.gvDevices.OptionsView.ShowIndicator = false;
            this.gvDevices.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.deviceKind, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvDevices.DoubleClick += new System.EventHandler(this.gvDevices_DoubleClick);
            // 
            // deviceID
            // 
            this.deviceID.AppearanceCell.Options.UseTextOptions = true;
            this.deviceID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceID.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceID.Caption = "设备ID";
            this.deviceID.Name = "deviceID";
            this.deviceID.OptionsColumn.AllowEdit = false;
            this.deviceID.OptionsColumn.ReadOnly = true;
            this.deviceID.Visible = true;
            this.deviceID.VisibleIndex = 0;
            // 
            // deviceNetworkID
            // 
            this.deviceNetworkID.AppearanceCell.Options.UseTextOptions = true;
            this.deviceNetworkID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceNetworkID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceNetworkID.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceNetworkID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceNetworkID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceNetworkID.Caption = "网段ID";
            this.deviceNetworkID.Name = "deviceNetworkID";
            this.deviceNetworkID.OptionsColumn.AllowEdit = false;
            this.deviceNetworkID.OptionsColumn.ReadOnly = true;
            this.deviceNetworkID.Visible = true;
            this.deviceNetworkID.VisibleIndex = 1;
            // 
            // deviceKind
            // 
            this.deviceKind.AppearanceCell.Options.UseTextOptions = true;
            this.deviceKind.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceKind.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceKind.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceKind.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceKind.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceKind.Caption = "设备类型";
            this.deviceKind.Name = "deviceKind";
            this.deviceKind.OptionsColumn.AllowEdit = false;
            this.deviceKind.OptionsColumn.ReadOnly = true;
            // 
            // deviceName
            // 
            this.deviceName.AppearanceCell.Options.UseTextOptions = true;
            this.deviceName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceName.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceName.Caption = "设备名称";
            this.deviceName.Name = "deviceName";
            this.deviceName.OptionsColumn.AllowEdit = false;
            this.deviceName.OptionsColumn.ReadOnly = true;
            this.deviceName.Visible = true;
            this.deviceName.VisibleIndex = 2;
            // 
            // deviceMac
            // 
            this.deviceMac.AppearanceCell.Options.UseTextOptions = true;
            this.deviceMac.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceMac.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceMac.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceMac.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceMac.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceMac.Caption = "设备机身码";
            this.deviceMac.Name = "deviceMac";
            this.deviceMac.OptionsColumn.AllowEdit = false;
            this.deviceMac.OptionsColumn.ReadOnly = true;
            this.deviceMac.Visible = true;
            this.deviceMac.VisibleIndex = 3;
            // 
            // deviceState
            // 
            this.deviceState.AppearanceCell.Options.UseTextOptions = true;
            this.deviceState.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceState.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceState.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceState.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceState.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceState.Caption = "状态";
            this.deviceState.Name = "deviceState";
            this.deviceState.OptionsColumn.AllowEdit = false;
            this.deviceState.OptionsColumn.ReadOnly = true;
            this.deviceState.Visible = true;
            this.deviceState.VisibleIndex = 4;
            // 
            // deviceRemark
            // 
            this.deviceRemark.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.deviceRemark.AppearanceCell.Options.UseForeColor = true;
            this.deviceRemark.AppearanceCell.Options.UseTextOptions = true;
            this.deviceRemark.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceRemark.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceRemark.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceRemark.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceRemark.Caption = "提示";
            this.deviceRemark.Name = "deviceRemark";
            this.deviceRemark.OptionsColumn.AllowEdit = false;
            this.deviceRemark.OptionsColumn.ReadOnly = true;
            this.deviceRemark.Visible = true;
            this.deviceRemark.VisibleIndex = 5;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn13.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn13.Caption = "gridColumn4";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.ReadOnly = true;
            // 
            // SelectDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.gcDevices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDevice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备选择";
            this.Load += new System.EventHandler(this.SelectDevice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDevices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDevices;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDevices;
        private DevExpress.XtraGrid.Columns.GridColumn deviceID;
        private DevExpress.XtraGrid.Columns.GridColumn deviceNetworkID;
        private DevExpress.XtraGrid.Columns.GridColumn deviceKind;
        private DevExpress.XtraGrid.Columns.GridColumn deviceName;
        private DevExpress.XtraGrid.Columns.GridColumn deviceMac;
        private DevExpress.XtraGrid.Columns.GridColumn deviceState;
        private DevExpress.XtraGrid.Columns.GridColumn deviceRemark;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
    }
}