namespace ConfigDevice
{
    partial class ViewLogicTools
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcLogic = new DevExpress.XtraGrid.GridControl();
            this.gvLogic = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dcObject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcPosition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcDiffDevice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcKind = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcOperate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcRangeStart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcRangeEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcInvalid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLogic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogic)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // gridView2
            // 
            this.gridView2.Name = "gridView2";
            // 
            // gcLogic
            // 
            this.gcLogic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLogic.EmbeddedNavigator.Name = "";
            this.gcLogic.FormsUseDefaultLookAndFeel = false;
            this.gcLogic.Location = new System.Drawing.Point(0, 0);
            this.gcLogic.MainView = this.gvLogic;
            this.gcLogic.Name = "gcLogic";
            this.gcLogic.Size = new System.Drawing.Size(836, 45);
            this.gcLogic.TabIndex = 14;
            this.gcLogic.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLogic});
            // 
            // gvLogic
            // 
            this.gvLogic.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvLogic.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dcObject,
            this.dcPosition,
            this.dcDiffDevice,
            this.dcKind,
            this.dcOperate,
            this.dcRangeStart,
            this.dcRangeEnd,
            this.dcValid,
            this.dcInvalid,
            this.gridColumn3,
            this.gridColumn4});
            this.gvLogic.GridControl = this.gcLogic;
            this.gvLogic.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gvLogic.Name = "gvLogic";
            this.gvLogic.OptionsCustomization.AllowColumnMoving = false;
            this.gvLogic.OptionsCustomization.AllowColumnResizing = false;
            this.gvLogic.OptionsCustomization.AllowFilter = false;
            this.gvLogic.OptionsCustomization.AllowGroup = false;
            this.gvLogic.OptionsCustomization.AllowRowSizing = true;
            this.gvLogic.OptionsCustomization.AllowSort = false;
            this.gvLogic.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvLogic.OptionsView.ColumnAutoWidth = false;
            this.gvLogic.OptionsView.ShowGroupPanel = false;
            this.gvLogic.OptionsView.ShowIndicator = false;
            this.gvLogic.RowHeight = 22;
            this.gvLogic.RowSeparatorHeight = 4;
            this.gvLogic.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.gvLogic.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            // 
            // dcObject
            // 
            this.dcObject.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcObject.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcObject.AppearanceCell.Options.UseBackColor = true;
            this.dcObject.AppearanceCell.Options.UseForeColor = true;
            this.dcObject.AppearanceCell.Options.UseTextOptions = true;
            this.dcObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcObject.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcObject.AppearanceHeader.Options.UseTextOptions = true;
            this.dcObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcObject.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcObject.Caption = "触发对象";
            this.dcObject.Name = "dcObject";
            this.dcObject.Visible = true;
            this.dcObject.VisibleIndex = 0;
            this.dcObject.Width = 140;
            // 
            // dcPosition
            // 
            this.dcPosition.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcPosition.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcPosition.AppearanceCell.Options.UseBackColor = true;
            this.dcPosition.AppearanceCell.Options.UseForeColor = true;
            this.dcPosition.AppearanceCell.Options.UseTextOptions = true;
            this.dcPosition.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcPosition.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcPosition.AppearanceHeader.Options.UseTextOptions = true;
            this.dcPosition.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcPosition.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcPosition.Caption = "触发位置";
            this.dcPosition.Name = "dcPosition";
            this.dcPosition.Visible = true;
            this.dcPosition.VisibleIndex = 1;
            this.dcPosition.Width = 80;
            // 
            // dcDiffDevice
            // 
            this.dcDiffDevice.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcDiffDevice.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcDiffDevice.AppearanceCell.Options.UseBackColor = true;
            this.dcDiffDevice.AppearanceCell.Options.UseForeColor = true;
            this.dcDiffDevice.AppearanceCell.Options.UseTextOptions = true;
            this.dcDiffDevice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDiffDevice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDiffDevice.AppearanceHeader.Options.UseTextOptions = true;
            this.dcDiffDevice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDiffDevice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDiffDevice.Caption = "触发设备";
            this.dcDiffDevice.Name = "dcDiffDevice";
            this.dcDiffDevice.Visible = true;
            this.dcDiffDevice.VisibleIndex = 2;
            this.dcDiffDevice.Width = 80;
            // 
            // dcKind
            // 
            this.dcKind.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcKind.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcKind.AppearanceCell.Options.UseBackColor = true;
            this.dcKind.AppearanceCell.Options.UseForeColor = true;
            this.dcKind.AppearanceCell.Options.UseTextOptions = true;
            this.dcKind.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcKind.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcKind.AppearanceHeader.Options.UseTextOptions = true;
            this.dcKind.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcKind.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcKind.Caption = "触发类型";
            this.dcKind.Name = "dcKind";
            this.dcKind.Visible = true;
            this.dcKind.VisibleIndex = 3;
            this.dcKind.Width = 80;
            // 
            // dcOperate
            // 
            this.dcOperate.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcOperate.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcOperate.AppearanceCell.Options.UseBackColor = true;
            this.dcOperate.AppearanceCell.Options.UseForeColor = true;
            this.dcOperate.AppearanceCell.Options.UseTextOptions = true;
            this.dcOperate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcOperate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcOperate.AppearanceHeader.Options.UseTextOptions = true;
            this.dcOperate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcOperate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcOperate.Caption = "运算";
            this.dcOperate.Name = "dcOperate";
            this.dcOperate.Visible = true;
            this.dcOperate.VisibleIndex = 4;
            this.dcOperate.Width = 80;
            // 
            // dcRangeStart
            // 
            this.dcRangeStart.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcRangeStart.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcRangeStart.AppearanceCell.Options.UseBackColor = true;
            this.dcRangeStart.AppearanceCell.Options.UseForeColor = true;
            this.dcRangeStart.AppearanceCell.Options.UseTextOptions = true;
            this.dcRangeStart.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcRangeStart.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcRangeStart.AppearanceHeader.Options.UseTextOptions = true;
            this.dcRangeStart.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcRangeStart.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcRangeStart.Caption = "范围(开始)";
            this.dcRangeStart.Name = "dcRangeStart";
            this.dcRangeStart.Visible = true;
            this.dcRangeStart.VisibleIndex = 5;
            this.dcRangeStart.Width = 80;
            // 
            // dcRangeEnd
            // 
            this.dcRangeEnd.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcRangeEnd.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcRangeEnd.AppearanceCell.Options.UseBackColor = true;
            this.dcRangeEnd.AppearanceCell.Options.UseForeColor = true;
            this.dcRangeEnd.AppearanceCell.Options.UseTextOptions = true;
            this.dcRangeEnd.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcRangeEnd.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcRangeEnd.AppearanceHeader.Options.UseTextOptions = true;
            this.dcRangeEnd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcRangeEnd.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcRangeEnd.Caption = "范围(结束)";
            this.dcRangeEnd.Name = "dcRangeEnd";
            this.dcRangeEnd.Visible = true;
            this.dcRangeEnd.VisibleIndex = 6;
            this.dcRangeEnd.Width = 80;
            // 
            // dcValid
            // 
            this.dcValid.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcValid.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcValid.AppearanceCell.Options.UseBackColor = true;
            this.dcValid.AppearanceCell.Options.UseForeColor = true;
            this.dcValid.AppearanceCell.Options.UseTextOptions = true;
            this.dcValid.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcValid.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcValid.AppearanceHeader.Options.UseTextOptions = true;
            this.dcValid.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcValid.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcValid.Caption = "触发时间(秒)";
            this.dcValid.Name = "dcValid";
            this.dcValid.Visible = true;
            this.dcValid.VisibleIndex = 7;
            this.dcValid.Width = 80;
            // 
            // dcInvalid
            // 
            this.dcInvalid.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcInvalid.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcInvalid.AppearanceCell.Options.UseBackColor = true;
            this.dcInvalid.AppearanceCell.Options.UseForeColor = true;
            this.dcInvalid.AppearanceCell.Options.UseTextOptions = true;
            this.dcInvalid.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcInvalid.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcInvalid.AppearanceHeader.Options.UseTextOptions = true;
            this.dcInvalid.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcInvalid.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcInvalid.Caption = "恢复时间(秒)";
            this.dcInvalid.Name = "dcInvalid";
            this.dcInvalid.Visible = true;
            this.dcInvalid.VisibleIndex = 8;
            this.dcInvalid.Width = 80;
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
            // 
            // ViewLogicTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcLogic);
            this.Name = "ViewLogicTools";
            this.Size = new System.Drawing.Size(836, 45);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLogic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gcLogic;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLogic;
        private DevExpress.XtraGrid.Columns.GridColumn dcOperate;
        private DevExpress.XtraGrid.Columns.GridColumn dcRangeStart;
        private DevExpress.XtraGrid.Columns.GridColumn dcRangeEnd;
        private DevExpress.XtraGrid.Columns.GridColumn dcValid;
        private DevExpress.XtraGrid.Columns.GridColumn dcInvalid;
        private DevExpress.XtraGrid.Columns.GridColumn dcKind;
        private DevExpress.XtraGrid.Columns.GridColumn dcObject;
        private DevExpress.XtraGrid.Columns.GridColumn dcPosition;
        private DevExpress.XtraGrid.Columns.GridColumn dcDiffDevice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;



    }
}
