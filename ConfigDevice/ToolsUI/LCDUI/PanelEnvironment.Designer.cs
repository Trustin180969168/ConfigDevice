namespace ConfigDevice
{
    partial class PanelEnvironment
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
            this.gcSensors = new DevExpress.XtraGrid.GridControl();
            this.gvSensors = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dcNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcDeviceValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcKindName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcNetwork = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbxControlObj = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.linkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.timeTest = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.spinTest = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.cedtSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.numEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.meeEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSensors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSensors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxControlObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meeEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // gcSensors
            // 
            this.gcSensors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSensors.EmbeddedNavigator.Name = "";
            this.gcSensors.FormsUseDefaultLookAndFeel = false;
            this.gcSensors.Location = new System.Drawing.Point(0, 0);
            this.gcSensors.MainView = this.gvSensors;
            this.gcSensors.Name = "gcSensors";
            this.gcSensors.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbxControlObj,
            this.linkEdit,
            this.timeTest,
            this.spinTest,
            this.cedtSelect,
            this.numEdit,
            this.meeEdit});
            this.gcSensors.Size = new System.Drawing.Size(577, 351);
            this.gcSensors.TabIndex = 9;
            this.gcSensors.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSensors});
            // 
            // gvSensors
            // 
            this.gvSensors.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dcNum,
            this.dcDeviceValue,
            this.dcKindName,
            this.dcNetwork,
            this.dcID,
            this.parameter2,
            this.parameter3,
            this.parameter4,
            this.parameter5});
            this.gvSensors.GridControl = this.gcSensors;
            this.gvSensors.Name = "gvSensors";
            this.gvSensors.OptionsCustomization.AllowColumnMoving = false;
            this.gvSensors.OptionsCustomization.AllowColumnResizing = false;
            this.gvSensors.OptionsCustomization.AllowFilter = false;
            this.gvSensors.OptionsCustomization.AllowGroup = false;
            this.gvSensors.OptionsCustomization.AllowSort = false;
            this.gvSensors.OptionsSelection.InvertSelection = true;
            this.gvSensors.OptionsView.ColumnAutoWidth = false;
            this.gvSensors.OptionsView.ShowGroupPanel = false;
            this.gvSensors.OptionsView.ShowIndicator = false;
            this.gvSensors.RowHeight = 18;
            // 
            // dcNum
            // 
            this.dcNum.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcNum.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcNum.AppearanceCell.Options.UseBackColor = true;
            this.dcNum.AppearanceCell.Options.UseForeColor = true;
            this.dcNum.AppearanceCell.Options.UseTextOptions = true;
            this.dcNum.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcNum.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcNum.AppearanceHeader.Options.UseTextOptions = true;
            this.dcNum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcNum.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcNum.Caption = "序号";
            this.dcNum.Name = "dcNum";
            this.dcNum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcNum.Visible = true;
            this.dcNum.VisibleIndex = 0;
            this.dcNum.Width = 50;
            // 
            // dcDeviceValue
            // 
            this.dcDeviceValue.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcDeviceValue.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcDeviceValue.AppearanceCell.Options.UseBackColor = true;
            this.dcDeviceValue.AppearanceCell.Options.UseForeColor = true;
            this.dcDeviceValue.AppearanceCell.Options.UseTextOptions = true;
            this.dcDeviceValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDeviceValue.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDeviceValue.AppearanceHeader.Options.UseTextOptions = true;
            this.dcDeviceValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDeviceValue.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDeviceValue.Caption = "设备名称";
            this.dcDeviceValue.Name = "dcDeviceValue";
            this.dcDeviceValue.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcDeviceValue.Visible = true;
            this.dcDeviceValue.VisibleIndex = 1;
            this.dcDeviceValue.Width = 140;
            // 
            // dcKindName
            // 
            this.dcKindName.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcKindName.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcKindName.AppearanceCell.Options.UseBackColor = true;
            this.dcKindName.AppearanceCell.Options.UseForeColor = true;
            this.dcKindName.AppearanceCell.Options.UseTextOptions = true;
            this.dcKindName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcKindName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcKindName.AppearanceHeader.Options.UseTextOptions = true;
            this.dcKindName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcKindName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcKindName.Caption = "传感器类型";
            this.dcKindName.Name = "dcKindName";
            this.dcKindName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcKindName.Visible = true;
            this.dcKindName.VisibleIndex = 2;
            this.dcKindName.Width = 80;
            // 
            // dcNetwork
            // 
            this.dcNetwork.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcNetwork.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcNetwork.AppearanceCell.Options.UseBackColor = true;
            this.dcNetwork.AppearanceCell.Options.UseForeColor = true;
            this.dcNetwork.AppearanceCell.Options.UseTextOptions = true;
            this.dcNetwork.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcNetwork.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcNetwork.AppearanceHeader.Options.UseTextOptions = true;
            this.dcNetwork.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcNetwork.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcNetwork.Caption = "网段";
            this.dcNetwork.Name = "dcNetwork";
            this.dcNetwork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcNetwork.Visible = true;
            this.dcNetwork.VisibleIndex = 3;
            this.dcNetwork.Width = 50;
            // 
            // dcID
            // 
            this.dcID.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcID.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcID.AppearanceCell.Options.UseBackColor = true;
            this.dcID.AppearanceCell.Options.UseForeColor = true;
            this.dcID.AppearanceCell.Options.UseTextOptions = true;
            this.dcID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcID.AppearanceHeader.Options.UseTextOptions = true;
            this.dcID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcID.Caption = "ID";
            this.dcID.Name = "dcID";
            this.dcID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcID.Visible = true;
            this.dcID.VisibleIndex = 4;
            this.dcID.Width = 50;
            // 
            // parameter2
            // 
            this.parameter2.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter2.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter2.AppearanceCell.Options.UseBackColor = true;
            this.parameter2.AppearanceCell.Options.UseForeColor = true;
            this.parameter2.AppearanceCell.Options.UseTextOptions = true;
            this.parameter2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter2.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter2.Caption = "参数2";
            this.parameter2.Name = "parameter2";
            this.parameter2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter2.Width = 80;
            // 
            // parameter3
            // 
            this.parameter3.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter3.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter3.AppearanceCell.Options.UseBackColor = true;
            this.parameter3.AppearanceCell.Options.UseForeColor = true;
            this.parameter3.AppearanceCell.Options.UseTextOptions = true;
            this.parameter3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter3.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter3.Caption = "参数3";
            this.parameter3.Name = "parameter3";
            this.parameter3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter3.Width = 80;
            // 
            // parameter4
            // 
            this.parameter4.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter4.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter4.AppearanceCell.Options.UseBackColor = true;
            this.parameter4.AppearanceCell.Options.UseForeColor = true;
            this.parameter4.AppearanceCell.Options.UseTextOptions = true;
            this.parameter4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter4.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter4.Caption = "参数4";
            this.parameter4.Name = "parameter4";
            this.parameter4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter4.Width = 80;
            // 
            // parameter5
            // 
            this.parameter5.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter5.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter5.AppearanceCell.Options.UseBackColor = true;
            this.parameter5.AppearanceCell.Options.UseForeColor = true;
            this.parameter5.AppearanceCell.Options.UseTextOptions = true;
            this.parameter5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter5.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter5.Caption = "参数5";
            this.parameter5.Name = "parameter5";
            this.parameter5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter5.Width = 80;
            // 
            // cbxControlObj
            // 
            this.cbxControlObj.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cbxControlObj.AutoHeight = false;
            this.cbxControlObj.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxControlObj.Name = "cbxControlObj";
            this.cbxControlObj.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // linkEdit
            // 
            this.linkEdit.AutoHeight = false;
            this.linkEdit.Image = global::ConfigDevice.Properties.Resources.del1;
            this.linkEdit.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.linkEdit.Name = "linkEdit";
            this.linkEdit.NullText = "1";
            this.linkEdit.ReadOnly = true;
            // 
            // timeTest
            // 
            this.timeTest.AutoHeight = false;
            this.timeTest.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeTest.DisplayFormat.FormatString = "d";
            this.timeTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeTest.HideSelection = false;
            this.timeTest.Mask.EditMask = "HH:mm:ss";
            this.timeTest.Mask.UseMaskAsDisplayFormat = true;
            this.timeTest.Name = "timeTest";
            // 
            // spinTest
            // 
            this.spinTest.AutoHeight = false;
            this.spinTest.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinTest.Mask.EditMask = "P0";
            this.spinTest.Mask.UseMaskAsDisplayFormat = true;
            this.spinTest.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinTest.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinTest.Name = "spinTest";
            // 
            // cedtSelect
            // 
            this.cedtSelect.AutoHeight = false;
            this.cedtSelect.Name = "cedtSelect";
            this.cedtSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // numEdit
            // 
            this.numEdit.AutoHeight = false;
            this.numEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numEdit.Mask.EditMask = "\\d+";
            this.numEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numEdit.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numEdit.Name = "numEdit";
            // 
            // meeEdit
            // 
            this.meeEdit.AutoHeight = false;
            this.meeEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.meeEdit.Name = "meeEdit";
            // 
            // PanelEnvironment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcSensors);
            this.Name = "PanelEnvironment";
            this.Size = new System.Drawing.Size(577, 351);
            ((System.ComponentModel.ISupportInitialize)(this.gcSensors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSensors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxControlObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meeEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcSensors;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSensors;
        private DevExpress.XtraGrid.Columns.GridColumn dcNum;
        private DevExpress.XtraGrid.Columns.GridColumn dcKindName;
        private DevExpress.XtraGrid.Columns.GridColumn dcNetwork;
        private DevExpress.XtraGrid.Columns.GridColumn dcID;
        private DevExpress.XtraGrid.Columns.GridColumn dcDeviceValue;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxControlObj;
        private DevExpress.XtraGrid.Columns.GridColumn parameter2;
        private DevExpress.XtraGrid.Columns.GridColumn parameter3;
        private DevExpress.XtraGrid.Columns.GridColumn parameter4;
        private DevExpress.XtraGrid.Columns.GridColumn parameter5;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit timeTest;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTest;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit cedtSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit numEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit meeEdit;

    }
}
