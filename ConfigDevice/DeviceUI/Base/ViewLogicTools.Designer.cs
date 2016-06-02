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
            this.dcOperate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcRangeStart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcRangeEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcInvalid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLogic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
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
            this.gcLogic.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcLogic.EmbeddedNavigator.Name = "";
            this.gcLogic.FormsUseDefaultLookAndFeel = false;
            this.gcLogic.Location = new System.Drawing.Point(0, 0);
            this.gcLogic.MainView = this.gvLogic;
            this.gcLogic.Name = "gcLogic";
            this.gcLogic.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gcLogic.Size = new System.Drawing.Size(800, 46);
            this.gcLogic.TabIndex = 14;
            this.gcLogic.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLogic});
            // 
            // gvLogic
            // 
            this.gvLogic.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvLogic.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dcObject,
            this.dcOperate,
            this.dcRangeStart,
            this.dcRangeEnd,
            this.dcValid,
            this.dcInvalid});
            this.gvLogic.GridControl = this.gcLogic;
            this.gvLogic.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gvLogic.Name = "gvLogic";
            this.gvLogic.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvLogic.OptionsView.ShowGroupPanel = false;
            this.gvLogic.OptionsView.ShowIndicator = false;
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
            this.dcObject.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.dcObject.Visible = true;
            this.dcObject.VisibleIndex = 0;
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
            this.dcOperate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.dcOperate.Visible = true;
            this.dcOperate.VisibleIndex = 1;
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
            this.dcRangeStart.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.dcRangeStart.Visible = true;
            this.dcRangeStart.VisibleIndex = 2;
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
            this.dcRangeEnd.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.dcRangeEnd.Visible = true;
            this.dcRangeEnd.VisibleIndex = 3;
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
            this.dcValid.Caption = "持续时间(秒)";
            this.dcValid.Name = "dcValid";
            this.dcValid.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.dcValid.Visible = true;
            this.dcValid.VisibleIndex = 4;
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
            this.dcInvalid.Caption = "无效持续(秒)";
            this.dcInvalid.Name = "dcInvalid";
            this.dcInvalid.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.dcInvalid.Visible = true;
            this.dcInvalid.VisibleIndex = 5;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // ViewLogicTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcLogic);
            this.Name = "ViewLogicTools";
            this.Size = new System.Drawing.Size(800, 165);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLogic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gcLogic;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLogic;
        private DevExpress.XtraGrid.Columns.GridColumn dcObject;
        private DevExpress.XtraGrid.Columns.GridColumn dcOperate;
        private DevExpress.XtraGrid.Columns.GridColumn dcRangeStart;
        private DevExpress.XtraGrid.Columns.GridColumn dcRangeEnd;
        private DevExpress.XtraGrid.Columns.GridColumn dcValid;
        private DevExpress.XtraGrid.Columns.GridColumn dcInvalid;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;



    }
}
