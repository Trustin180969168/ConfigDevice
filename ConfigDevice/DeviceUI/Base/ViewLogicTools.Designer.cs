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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewLogicTools));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gcLogic = new DevExpress.XtraGrid.GridControl();
            this.gvLogic = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dcTriggerCondition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcInputCondition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcRangeStart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcRangeEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcContinue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.imageComboBoxEdit1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLogic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AutoSize = true;
            this.groupControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(800, 152);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "触发列表";
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gcLogic);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 26);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(796, 124);
            this.panelControl1.TabIndex = 5;
            // 
            // gcLogic
            // 
            this.gcLogic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLogic.EmbeddedNavigator.Name = "";
            this.gcLogic.FormsUseDefaultLookAndFeel = false;
            this.gcLogic.Location = new System.Drawing.Point(0, 0);
            this.gcLogic.MainView = this.gvLogic;
            this.gcLogic.Name = "gcLogic";
            this.gcLogic.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gcLogic.Size = new System.Drawing.Size(620, 124);
            this.gcLogic.TabIndex = 13;
            this.gcLogic.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLogic});
            // 
            // gvLogic
            // 
            this.gvLogic.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvLogic.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dcTriggerCondition,
            this.dcInputCondition,
            this.dcRangeStart,
            this.dcRangeEnd,
            this.dcContinue,
            this.dcEnd});
            this.gvLogic.GridControl = this.gcLogic;
            this.gvLogic.Name = "gvLogic";
            this.gvLogic.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvLogic.OptionsView.EnableAppearanceEvenRow = true;
            this.gvLogic.OptionsView.EnableAppearanceOddRow = true;
            this.gvLogic.OptionsView.ShowGroupPanel = false;
            this.gvLogic.OptionsView.ShowIndicator = false;
            this.gvLogic.RowSeparatorHeight = 4;
            // 
            // dcTriggerCondition
            // 
            this.dcTriggerCondition.AppearanceCell.Options.UseTextOptions = true;
            this.dcTriggerCondition.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcTriggerCondition.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcTriggerCondition.AppearanceHeader.Options.UseTextOptions = true;
            this.dcTriggerCondition.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcTriggerCondition.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcTriggerCondition.Caption = "触发条件";
            this.dcTriggerCondition.Name = "dcTriggerCondition";
            this.dcTriggerCondition.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.dcTriggerCondition.Visible = true;
            this.dcTriggerCondition.VisibleIndex = 0;
            // 
            // dcInputCondition
            // 
            this.dcInputCondition.AppearanceCell.Options.UseTextOptions = true;
            this.dcInputCondition.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcInputCondition.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcInputCondition.AppearanceHeader.Options.UseTextOptions = true;
            this.dcInputCondition.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcInputCondition.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcInputCondition.Caption = "输入条件";
            this.dcInputCondition.Name = "dcInputCondition";
            this.dcInputCondition.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.dcInputCondition.Visible = true;
            this.dcInputCondition.VisibleIndex = 1;
            // 
            // dcRangeStart
            // 
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
            // dcContinue
            // 
            this.dcContinue.AppearanceCell.Options.UseTextOptions = true;
            this.dcContinue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcContinue.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcContinue.AppearanceHeader.Options.UseTextOptions = true;
            this.dcContinue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcContinue.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcContinue.Caption = "持续时间(秒)";
            this.dcContinue.Name = "dcContinue";
            this.dcContinue.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.dcContinue.Visible = true;
            this.dcContinue.VisibleIndex = 4;
            // 
            // dcEnd
            // 
            this.dcEnd.AppearanceCell.Options.UseTextOptions = true;
            this.dcEnd.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcEnd.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcEnd.AppearanceHeader.Options.UseTextOptions = true;
            this.dcEnd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcEnd.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcEnd.Caption = "无效持续(秒)";
            this.dcEnd.Name = "dcEnd";
            this.dcEnd.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.dcEnd.Visible = true;
            this.dcEnd.VisibleIndex = 5;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.Options.UseTextOptions = true;
            this.groupControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.groupControl2.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.groupControl2.AutoSize = true;
            this.groupControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl2.Controls.Add(this.imageComboBoxEdit1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl2.Location = new System.Drawing.Point(620, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(176, 124);
            this.groupControl2.TabIndex = 15;
            this.groupControl2.Text = "逻辑关系";
            // 
            // imageComboBoxEdit1
            // 
            this.imageComboBoxEdit1.Dock = System.Windows.Forms.DockStyle.Right;
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(2, 21);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboBoxEdit1.Properties.DropDownRows = 4;
            this.imageComboBoxEdit1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 3, 3),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 4, 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 5, 5)});
            this.imageComboBoxEdit1.Properties.LargeImages = this.imageCollection1;
            this.imageComboBoxEdit1.Size = new System.Drawing.Size(172, 102);
            this.imageComboBoxEdit1.TabIndex = 15;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(168, 100);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // ViewLogicTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "ViewLogicTools";
            this.Size = new System.Drawing.Size(800, 152);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLogic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gcLogic;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLogic;
        private DevExpress.XtraGrid.Columns.GridColumn dcTriggerCondition;
        private DevExpress.XtraGrid.Columns.GridColumn dcInputCondition;
        private DevExpress.XtraGrid.Columns.GridColumn dcRangeStart;
        private DevExpress.XtraGrid.Columns.GridColumn dcRangeEnd;
        private DevExpress.XtraGrid.Columns.GridColumn dcContinue;
        private DevExpress.XtraGrid.Columns.GridColumn dcEnd;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboBoxEdit1;
        private DevExpress.Utils.ImageCollection imageCollection1;


    }
}
