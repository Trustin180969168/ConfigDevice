namespace ConfigDevice.DeviceUI
{
    partial class FrmLockDevices
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tctrlEdit = new DevExpress.XtraTab.XtraTabControl();
            this.pagePzjm = new DevExpress.XtraTab.XtraTabPage();
            this.gcLockConfigs = new DevExpress.XtraGrid.GridControl();
            this.gvLockConfigs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dcRowID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcKindName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcOpenMusic = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcStartTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcEndTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbxControlObj = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.linkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.timeTest = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.spinTest = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.cedtSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.numEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.meeEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.linkAdd = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.linkClear = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.spdtVolume = new DevExpress.XtraEditors.SpinEdit();
            this.tsDoorInput = new System.Windows.Forms.ToolStrip();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btRefrash = new System.Windows.Forms.ToolStripButton();
            this.pageJcsz = new DevExpress.XtraTab.XtraTabPage();
            this.viewBaseSetting = new ConfigDevice.ViewBaseEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.spdAddress = new DevExpress.XtraEditors.SpinEdit();
            this.lookUpEditAmp = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tctrlEdit)).BeginInit();
            this.tctrlEdit.SuspendLayout();
            this.pagePzjm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLockConfigs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLockConfigs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxControlObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meeEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdtVolume.Properties)).BeginInit();
            this.tsDoorInput.SuspendLayout();
            this.pageJcsz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAmp.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tctrlEdit
            // 
            this.tctrlEdit.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tctrlEdit.AppearancePage.Header.Options.UseFont = true;
            this.tctrlEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tctrlEdit.Location = new System.Drawing.Point(0, 24);
            this.tctrlEdit.Name = "tctrlEdit";
            this.tctrlEdit.SelectedTabPage = this.pagePzjm;
            this.tctrlEdit.Size = new System.Drawing.Size(893, 547);
            this.tctrlEdit.TabIndex = 3;
            this.tctrlEdit.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageJcsz,
            this.pagePzjm});
            this.tctrlEdit.Text = "xtraTabControl1";
            // 
            // pagePzjm
            // 
            this.pagePzjm.Controls.Add(this.gcLockConfigs);
            this.pagePzjm.Controls.Add(this.panelControl1);
            this.pagePzjm.Controls.Add(this.tsDoorInput);
            this.pagePzjm.Name = "pagePzjm";
            this.pagePzjm.Size = new System.Drawing.Size(884, 510);
            this.pagePzjm.Text = "配置界面";
            // 
            // gcLockConfigs
            // 
            this.gcLockConfigs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLockConfigs.EmbeddedNavigator.Name = "";
            this.gcLockConfigs.FormsUseDefaultLookAndFeel = false;
            this.gcLockConfigs.Location = new System.Drawing.Point(0, 82);
            this.gcLockConfigs.MainView = this.gvLockConfigs;
            this.gcLockConfigs.Name = "gcLockConfigs";
            this.gcLockConfigs.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbxControlObj,
            this.linkEdit,
            this.timeTest,
            this.spinTest,
            this.cedtSelect,
            this.numEdit,
            this.meeEdit,
            this.linkAdd,
            this.linkClear});
            this.gcLockConfigs.Size = new System.Drawing.Size(884, 428);
            this.gcLockConfigs.TabIndex = 9;
            this.gcLockConfigs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLockConfigs});
            // 
            // gvLockConfigs
            // 
            this.gvLockConfigs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dcRowID,
            this.dcKindName,
            this.dcOpenMusic,
            this.dcStartTime,
            this.dcEndTime,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gvLockConfigs.GridControl = this.gcLockConfigs;
            this.gvLockConfigs.Name = "gvLockConfigs";
            this.gvLockConfigs.OptionsCustomization.AllowColumnMoving = false;
            this.gvLockConfigs.OptionsCustomization.AllowColumnResizing = false;
            this.gvLockConfigs.OptionsCustomization.AllowFilter = false;
            this.gvLockConfigs.OptionsCustomization.AllowGroup = false;
            this.gvLockConfigs.OptionsCustomization.AllowSort = false;
            this.gvLockConfigs.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvLockConfigs.OptionsView.ColumnAutoWidth = false;
            this.gvLockConfigs.OptionsView.ShowGroupPanel = false;
            this.gvLockConfigs.RowHeight = 18;
            // 
            // dcRowID
            // 
            this.dcRowID.AppearanceCell.Options.UseTextOptions = true;
            this.dcRowID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcRowID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcRowID.AppearanceHeader.Options.UseTextOptions = true;
            this.dcRowID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcRowID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcRowID.Caption = "序号";
            this.dcRowID.Name = "dcRowID";
            this.dcRowID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.dcRowID.OptionsColumn.ReadOnly = true;
            this.dcRowID.Visible = true;
            this.dcRowID.VisibleIndex = 0;
            this.dcRowID.Width = 50;
            // 
            // dcKindName
            // 
            this.dcKindName.AppearanceCell.Options.UseTextOptions = true;
            this.dcKindName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcKindName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcKindName.AppearanceHeader.Options.UseTextOptions = true;
            this.dcKindName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcKindName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcKindName.Caption = "用户分类";
            this.dcKindName.Name = "dcKindName";
            this.dcKindName.OptionsColumn.ReadOnly = true;
            this.dcKindName.Visible = true;
            this.dcKindName.VisibleIndex = 1;
            // 
            // dcOpenMusic
            // 
            this.dcOpenMusic.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcOpenMusic.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcOpenMusic.AppearanceCell.Options.UseBackColor = true;
            this.dcOpenMusic.AppearanceCell.Options.UseForeColor = true;
            this.dcOpenMusic.AppearanceCell.Options.UseTextOptions = true;
            this.dcOpenMusic.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcOpenMusic.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcOpenMusic.AppearanceHeader.Options.UseTextOptions = true;
            this.dcOpenMusic.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcOpenMusic.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcOpenMusic.Caption = "开锁提示音(曲目)(SD卡\\WINDOW\\)";
            this.dcOpenMusic.Name = "dcOpenMusic";
            this.dcOpenMusic.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.dcOpenMusic.Visible = true;
            this.dcOpenMusic.VisibleIndex = 2;
            this.dcOpenMusic.Width = 300;
            // 
            // dcStartTime
            // 
            this.dcStartTime.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcStartTime.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcStartTime.AppearanceCell.Options.UseBackColor = true;
            this.dcStartTime.AppearanceCell.Options.UseForeColor = true;
            this.dcStartTime.AppearanceCell.Options.UseTextOptions = true;
            this.dcStartTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcStartTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcStartTime.AppearanceHeader.Options.UseTextOptions = true;
            this.dcStartTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcStartTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcStartTime.Caption = "提示音开始时段";
            this.dcStartTime.Name = "dcStartTime";
            this.dcStartTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.dcStartTime.Visible = true;
            this.dcStartTime.VisibleIndex = 3;
            this.dcStartTime.Width = 120;
            // 
            // dcEndTime
            // 
            this.dcEndTime.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcEndTime.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcEndTime.AppearanceCell.Options.UseBackColor = true;
            this.dcEndTime.AppearanceCell.Options.UseForeColor = true;
            this.dcEndTime.AppearanceCell.Options.UseTextOptions = true;
            this.dcEndTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcEndTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcEndTime.AppearanceHeader.Options.UseTextOptions = true;
            this.dcEndTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcEndTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcEndTime.Caption = "提示音结束时段";
            this.dcEndTime.Name = "dcEndTime";
            this.dcEndTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.dcEndTime.Visible = true;
            this.dcEndTime.VisibleIndex = 4;
            this.dcEndTime.Width = 120;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
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
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.Caption = "gridColumn5";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.ReadOnly = true;
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
            // linkAdd
            // 
            this.linkAdd.AutoHeight = false;
            this.linkAdd.Image = global::ConfigDevice.Properties.Resources.Add;
            this.linkAdd.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.linkAdd.Name = "linkAdd";
            this.linkAdd.ReadOnly = true;
            // 
            // linkClear
            // 
            this.linkClear.AutoHeight = false;
            this.linkClear.Image = global::ConfigDevice.Properties.Resources.Clear;
            this.linkClear.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.linkClear.Name = "linkClear";
            this.linkClear.ReadOnly = true;
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl1.Controls.Add(this.lookUpEditAmp);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.spdAddress);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.spdtVolume);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 31);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(884, 51);
            this.panelControl1.TabIndex = 10;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(26, 17);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "提示音功放";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(265, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "功放地址";
            // 
            // spdtVolume
            // 
            this.spdtVolume.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spdtVolume.Location = new System.Drawing.Point(488, 14);
            this.spdtVolume.Name = "spdtVolume";
            this.spdtVolume.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spdtVolume.Properties.Mask.EditMask = "d";
            this.spdtVolume.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spdtVolume.Size = new System.Drawing.Size(75, 21);
            this.spdtVolume.TabIndex = 0;
            // 
            // tsDoorInput
            // 
            this.tsDoorInput.Font = new System.Drawing.Font("宋体", 12F);
            this.tsDoorInput.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsDoorInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSave,
            this.btRefrash});
            this.tsDoorInput.Location = new System.Drawing.Point(0, 0);
            this.tsDoorInput.Name = "tsDoorInput";
            this.tsDoorInput.Size = new System.Drawing.Size(884, 31);
            this.tsDoorInput.TabIndex = 1;
            this.tsDoorInput.Text = "toolStrip2";
            // 
            // btSave
            // 
            this.btSave.Image = global::ConfigDevice.Properties.Resources.save;
            this.btSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(76, 28);
            this.btSave.Text = "保存 ";
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btRefrash
            // 
            this.btRefrash.Image = global::ConfigDevice.Properties.Resources.refresh;
            this.btRefrash.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefrash.Name = "btRefrash";
            this.btRefrash.Size = new System.Drawing.Size(76, 28);
            this.btRefrash.Text = "刷新 ";
            this.btRefrash.Click += new System.EventHandler(this.btRefrash_Click);
            // 
            // pageJcsz
            // 
            this.pageJcsz.Controls.Add(this.viewBaseSetting);
            this.pageJcsz.Name = "pageJcsz";
            this.pageJcsz.Size = new System.Drawing.Size(884, 510);
            this.pageJcsz.Text = "基础配置";
            // 
            // viewBaseSetting
            // 
            this.viewBaseSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewBaseSetting.Location = new System.Drawing.Point(0, 0);
            this.viewBaseSetting.Name = "viewBaseSetting";
            this.viewBaseSetting.Size = new System.Drawing.Size(884, 510);
            this.viewBaseSetting.TabIndex = 0;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(458, 17);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "音量";
            // 
            // spdAddress
            // 
            this.spdAddress.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spdAddress.Location = new System.Drawing.Point(319, 14);
            this.spdAddress.Name = "spdAddress";
            this.spdAddress.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spdAddress.Properties.Mask.EditMask = "d";
            this.spdAddress.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spdAddress.Size = new System.Drawing.Size(75, 21);
            this.spdAddress.TabIndex = 4;
            // 
            // lookUpEditAmp
            // 
            this.lookUpEditAmp.EditValue = "";
            this.lookUpEditAmp.Location = new System.Drawing.Point(92, 14);
            this.lookUpEditAmp.Name = "lookUpEditAmp";
            this.lookUpEditAmp.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lookUpEditAmp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditAmp.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name1", "功放", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name2", "地址", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.lookUpEditAmp.Properties.NullText = "选择功放设备";
            this.lookUpEditAmp.Size = new System.Drawing.Size(112, 21);
            this.lookUpEditAmp.TabIndex = 15;
            this.lookUpEditAmp.EditValueChanged += new System.EventHandler(this.lookUpEditAmp_EditValueChanged);
            // 
            // FrmLockDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(893, 571);
            this.Controls.Add(this.tctrlEdit);
            this.Name = "FrmLockDevices";
            this.Load += new System.EventHandler(this.FrmWirlessDevices_Load);
            this.Controls.SetChildIndex(this.tctrlEdit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.tctrlEdit)).EndInit();
            this.tctrlEdit.ResumeLayout(false);
            this.pagePzjm.ResumeLayout(false);
            this.pagePzjm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLockConfigs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLockConfigs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxControlObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meeEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdtVolume.Properties)).EndInit();
            this.tsDoorInput.ResumeLayout(false);
            this.tsDoorInput.PerformLayout();
            this.pageJcsz.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAmp.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tctrlEdit;
        private DevExpress.XtraTab.XtraTabPage pagePzjm;
        private System.Windows.Forms.ToolStrip tsDoorInput;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripButton btRefrash;
        private DevExpress.XtraTab.XtraTabPage pageJcsz;
        private ViewBaseEdit viewBaseSetting;
        private DevExpress.XtraGrid.GridControl gcLockConfigs;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLockConfigs;
        private DevExpress.XtraGrid.Columns.GridColumn dcRowID;
        private DevExpress.XtraGrid.Columns.GridColumn dcOpenMusic;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxControlObj;
        private DevExpress.XtraGrid.Columns.GridColumn dcEndTime;
        private DevExpress.XtraGrid.Columns.GridColumn dcStartTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit timeTest;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTest;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit cedtSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit numEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit meeEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkAdd;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkClear;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SpinEdit spdtVolume;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn dcKindName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit spdAddress;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditAmp;
    }
}
