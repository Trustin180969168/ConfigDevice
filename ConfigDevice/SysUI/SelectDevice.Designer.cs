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
            this.xh = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gvDevices.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.xh,
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
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.deviceKind, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.xh, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvDevices.DoubleClick += new System.EventHandler(this.gvDevices_DoubleClick);
            // 
            // xh
            // 
            this.xh.AppearanceCell.Options.UseTextOptions = true;
            this.xh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xh.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xh.AppearanceHeader.Options.UseTextOptions = true;
            this.xh.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xh.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xh.Caption = "序号";
            this.xh.Name = "xh";
            this.xh.OptionsColumn.AllowEdit = false;
            this.xh.OptionsColumn.ReadOnly = true;
            this.xh.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.xh.Visible = true;
            this.xh.VisibleIndex = 0;
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
            this.deviceID.VisibleIndex = 1;
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
            this.deviceNetworkID.VisibleIndex = 2;
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
            this.deviceKind.Visible = true;
            this.deviceKind.VisibleIndex = 3;
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
            this.deviceName.VisibleIndex = 3;
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
            this.deviceMac.VisibleIndex = 4;
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
            this.deviceState.VisibleIndex = 5;
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
            this.deviceRemark.VisibleIndex = 6;
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
        private DevExpress.XtraGrid.Columns.GridColumn xh;
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