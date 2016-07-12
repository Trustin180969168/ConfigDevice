namespace ConfigDevice
{
    partial class KeySettingTools
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.gcKeyData = new DevExpress.XtraGrid.GridControl();
            this.gvKeyData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dcNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcCtrlObj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcCtrlKind = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcDectMax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcDectMin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcStep = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcRelevance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcMutex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcComunication = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcKeyData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKeyData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1024, 25);
            this.toolStrip1.TabIndex = 16;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::ConfigDevice.Properties.Resources.section;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(63, 22);
            this.toolStripButton1.Text = "第一页";
            // 
            // gcKeyData
            // 
            this.gcKeyData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcKeyData.EmbeddedNavigator.Name = "";
            this.gcKeyData.FormsUseDefaultLookAndFeel = false;
            this.gcKeyData.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gcKeyData.Location = new System.Drawing.Point(0, 25);
            this.gcKeyData.MainView = this.gvKeyData;
            this.gcKeyData.Name = "gcKeyData";
            this.gcKeyData.Size = new System.Drawing.Size(1024, 314);
            this.gcKeyData.TabIndex = 17;
            this.gcKeyData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvKeyData,
            this.gridView2});
            // 
            // gvKeyData
            // 
            this.gvKeyData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dcNum,
            this.dcName,
            this.dcCtrlObj,
            this.dcCtrlKind,
            this.dcDectMax,
            this.dcDectMin,
            this.dcStep,
            this.dcRelevance,
            this.dcMutex,
            this.dcComunication,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.gvKeyData.GridControl = this.gcKeyData;
            this.gvKeyData.GroupFormat = "";
            this.gvKeyData.Name = "gvKeyData";
            this.gvKeyData.OptionsView.EnableAppearanceEvenRow = true;
            this.gvKeyData.OptionsView.EnableAppearanceOddRow = true;
            this.gvKeyData.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvKeyData.OptionsView.ShowGroupPanel = false;
            this.gvKeyData.OptionsView.ShowIndicator = false;
            this.gvKeyData.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvKeyData_FocusedRowChanged);
            // 
            // dcNum
            // 
            this.dcNum.AppearanceCell.Options.UseTextOptions = true;
            this.dcNum.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcNum.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcNum.AppearanceHeader.Options.UseTextOptions = true;
            this.dcNum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcNum.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcNum.Caption = "编号";
            this.dcNum.FieldName = "xh";
            this.dcNum.Name = "dcNum";
            this.dcNum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcNum.OptionsColumn.ReadOnly = true;
            this.dcNum.OptionsFilter.AllowFilter = false;
            this.dcNum.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.dcNum.Visible = true;
            this.dcNum.VisibleIndex = 0;
            this.dcNum.Width = 80;
            // 
            // dcName
            // 
            this.dcName.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcName.AppearanceCell.Options.UseForeColor = true;
            this.dcName.AppearanceCell.Options.UseTextOptions = true;
            this.dcName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcName.AppearanceHeader.Options.UseTextOptions = true;
            this.dcName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcName.Caption = "名称";
            this.dcName.Name = "dcName";
            this.dcName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcName.OptionsFilter.AllowFilter = false;
            this.dcName.Visible = true;
            this.dcName.VisibleIndex = 1;
            this.dcName.Width = 120;
            // 
            // dcCtrlObj
            // 
            this.dcCtrlObj.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcCtrlObj.AppearanceCell.Options.UseForeColor = true;
            this.dcCtrlObj.AppearanceCell.Options.UseTextOptions = true;
            this.dcCtrlObj.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcCtrlObj.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcCtrlObj.AppearanceHeader.Options.UseTextOptions = true;
            this.dcCtrlObj.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcCtrlObj.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcCtrlObj.Caption = "控制对象";
            this.dcCtrlObj.Name = "dcCtrlObj";
            this.dcCtrlObj.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcCtrlObj.OptionsFilter.AllowFilter = false;
            this.dcCtrlObj.Visible = true;
            this.dcCtrlObj.VisibleIndex = 2;
            this.dcCtrlObj.Width = 120;
            // 
            // dcCtrlKind
            // 
            this.dcCtrlKind.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcCtrlKind.AppearanceCell.Options.UseForeColor = true;
            this.dcCtrlKind.AppearanceCell.Options.UseTextOptions = true;
            this.dcCtrlKind.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcCtrlKind.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcCtrlKind.AppearanceHeader.Options.UseTextOptions = true;
            this.dcCtrlKind.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcCtrlKind.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcCtrlKind.Caption = "控制类型";
            this.dcCtrlKind.Name = "dcCtrlKind";
            this.dcCtrlKind.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcCtrlKind.OptionsFilter.AllowFilter = false;
            this.dcCtrlKind.Visible = true;
            this.dcCtrlKind.VisibleIndex = 3;
            this.dcCtrlKind.Width = 120;
            // 
            // dcDectMax
            // 
            this.dcDectMax.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcDectMax.AppearanceCell.Options.UseForeColor = true;
            this.dcDectMax.AppearanceCell.Options.UseTextOptions = true;
            this.dcDectMax.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDectMax.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDectMax.AppearanceHeader.Options.UseTextOptions = true;
            this.dcDectMax.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDectMax.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDectMax.Caption = "方向最大值";
            this.dcDectMax.Name = "dcDectMax";
            this.dcDectMax.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcDectMax.OptionsFilter.AllowFilter = false;
            this.dcDectMax.Visible = true;
            this.dcDectMax.VisibleIndex = 4;
            this.dcDectMax.Width = 120;
            // 
            // dcDectMin
            // 
            this.dcDectMin.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcDectMin.AppearanceCell.Options.UseForeColor = true;
            this.dcDectMin.AppearanceCell.Options.UseTextOptions = true;
            this.dcDectMin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDectMin.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDectMin.AppearanceHeader.Options.UseTextOptions = true;
            this.dcDectMin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDectMin.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDectMin.Caption = "方向最小值";
            this.dcDectMin.Name = "dcDectMin";
            this.dcDectMin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcDectMin.OptionsFilter.AllowFilter = false;
            this.dcDectMin.Visible = true;
            this.dcDectMin.VisibleIndex = 5;
            this.dcDectMin.Width = 120;
            // 
            // dcStep
            // 
            this.dcStep.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcStep.AppearanceCell.Options.UseForeColor = true;
            this.dcStep.AppearanceCell.Options.UseTextOptions = true;
            this.dcStep.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcStep.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcStep.AppearanceHeader.Options.UseTextOptions = true;
            this.dcStep.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcStep.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcStep.Caption = "方向步进";
            this.dcStep.Name = "dcStep";
            this.dcStep.Visible = true;
            this.dcStep.VisibleIndex = 6;
            this.dcStep.Width = 120;
            // 
            // dcRelevance
            // 
            this.dcRelevance.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcRelevance.AppearanceCell.Options.UseForeColor = true;
            this.dcRelevance.AppearanceCell.Options.UseTextOptions = true;
            this.dcRelevance.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcRelevance.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcRelevance.AppearanceHeader.Options.UseTextOptions = true;
            this.dcRelevance.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcRelevance.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcRelevance.Caption = "关联号";
            this.dcRelevance.Name = "dcRelevance";
            this.dcRelevance.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcRelevance.OptionsFilter.AllowFilter = false;
            this.dcRelevance.Visible = true;
            this.dcRelevance.VisibleIndex = 7;
            this.dcRelevance.Width = 120;
            // 
            // dcMutex
            // 
            this.dcMutex.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcMutex.AppearanceCell.Options.UseForeColor = true;
            this.dcMutex.AppearanceCell.Options.UseTextOptions = true;
            this.dcMutex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcMutex.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcMutex.AppearanceHeader.Options.UseTextOptions = true;
            this.dcMutex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcMutex.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcMutex.Caption = "互斥号";
            this.dcMutex.Name = "dcMutex";
            this.dcMutex.Visible = true;
            this.dcMutex.VisibleIndex = 8;
            this.dcMutex.Width = 120;
            // 
            // dcComunication
            // 
            this.dcComunication.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcComunication.AppearanceCell.Options.UseForeColor = true;
            this.dcComunication.AppearanceCell.Options.UseTextOptions = true;
            this.dcComunication.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcComunication.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcComunication.AppearanceHeader.Options.UseTextOptions = true;
            this.dcComunication.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcComunication.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcComunication.Caption = "通讯模式";
            this.dcComunication.Name = "dcComunication";
            this.dcComunication.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcComunication.OptionsFilter.AllowFilter = false;
            this.dcComunication.Width = 120;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn3.AppearanceCell.Options.UseForeColor = true;
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
            this.gridColumn4.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn4.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "gridColumn4";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn5.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.Caption = "gridColumn5";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn6.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn6.Caption = "gridColumn6";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn7.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn7.Caption = "gridColumn7";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn8.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn8.Caption = "gridColumn8";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn9.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn9.Caption = "gridColumn9";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn10.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn10.Caption = "gridColumn10";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcKeyData;
            this.gridView2.Name = "gridView2";
            // 
            // KeySettingTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcKeyData);
            this.Controls.Add(this.toolStrip1);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "KeySettingTools";
            this.Size = new System.Drawing.Size(1024, 339);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcKeyData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKeyData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevExpress.XtraGrid.GridControl gcKeyData;
        private DevExpress.XtraGrid.Views.Grid.GridView gvKeyData;
        private DevExpress.XtraGrid.Columns.GridColumn dcNum;
        private DevExpress.XtraGrid.Columns.GridColumn dcName;
        private DevExpress.XtraGrid.Columns.GridColumn dcCtrlObj;
        private DevExpress.XtraGrid.Columns.GridColumn dcCtrlKind;
        private DevExpress.XtraGrid.Columns.GridColumn dcComunication;
        private DevExpress.XtraGrid.Columns.GridColumn dcDectMax;
        private DevExpress.XtraGrid.Columns.GridColumn dcDectMin;
        private DevExpress.XtraGrid.Columns.GridColumn dcRelevance;
        private DevExpress.XtraGrid.Columns.GridColumn dcMutex;
        private DevExpress.XtraGrid.Columns.GridColumn dcStep;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;


    }
}
