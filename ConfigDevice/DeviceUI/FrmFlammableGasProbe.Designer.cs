namespace ConfigDevice
{
    partial class FrmFlammableGasProbe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFlammableGasProbe));
            this.tctrlEdit = new DevExpress.XtraTab.XtraTabControl();
            this.pagePzjm = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.chkClearLoudly = new DevExpress.XtraEditors.CheckEdit();
            this.chkClearLight = new DevExpress.XtraEditors.CheckEdit();
            this.chkOpenValve = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btStopValve = new DevExpress.XtraEditors.SimpleButton();
            this.btCloseValve = new DevExpress.XtraEditors.SimpleButton();
            this.btOpenValve = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.spePreTime = new DevExpress.XtraEditors.SpinEdit();
            this.speProbeEC = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btAutoRefresh = new DevExpress.XtraEditors.CheckButton();
            this.edtT = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.edtEC = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.edtGasProbe = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.edtValveState = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tsDoorInput = new System.Windows.Forms.ToolStrip();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.pageJcsz = new DevExpress.XtraTab.XtraTabPage();
            this.frmSetting = new ConfigDevice.ViewBaseEdit();
            this.pageCommand = new DevExpress.XtraTab.XtraTabPage();
            this.viewCommandEdit = new ConfigDevice.ViewCommandSetting();
            this.viewLogicSetting = new ConfigDevice.ViewLogicSetting();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cbxGroup = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblGroupName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tctrlEdit)).BeginInit();
            this.tctrlEdit.SuspendLayout();
            this.pagePzjm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkClearLoudly.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkClearLight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOpenValve.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spePreTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speProbeEC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtGasProbe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtValveState.Properties)).BeginInit();
            this.tsDoorInput.SuspendLayout();
            this.pageJcsz.SuspendLayout();
            this.pageCommand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxGroup.Properties)).BeginInit();
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
            this.tctrlEdit.Size = new System.Drawing.Size(994, 651);
            this.tctrlEdit.TabIndex = 2;
            this.tctrlEdit.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageJcsz,
            this.pagePzjm,
            this.pageCommand});
            this.tctrlEdit.Text = "xtraTabControl1";
            this.tctrlEdit.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tctrlEdit_SelectedPageChanged);
            // 
            // pagePzjm
            // 
            this.pagePzjm.Controls.Add(this.groupControl3);
            this.pagePzjm.Controls.Add(this.groupControl4);
            this.pagePzjm.Controls.Add(this.groupControl2);
            this.pagePzjm.Controls.Add(this.groupControl1);
            this.pagePzjm.Controls.Add(this.tsDoorInput);
            this.pagePzjm.Name = "pagePzjm";
            this.pagePzjm.Size = new System.Drawing.Size(985, 614);
            this.pagePzjm.Text = "配置界面";
            // 
            // groupControl3
            // 
            this.groupControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl3.Controls.Add(this.chkClearLoudly);
            this.groupControl3.Controls.Add(this.chkClearLight);
            this.groupControl3.Controls.Add(this.chkOpenValve);
            this.groupControl3.Location = new System.Drawing.Point(29, 300);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(543, 71);
            this.groupControl3.TabIndex = 3;
            this.groupControl3.Text = "警报清除键动作配置";
            // 
            // chkClearLoudly
            // 
            this.chkClearLoudly.Location = new System.Drawing.Point(264, 36);
            this.chkClearLoudly.Name = "chkClearLoudly";
            this.chkClearLoudly.Properties.Caption = "清除蜂鸣器";
            this.chkClearLoudly.Size = new System.Drawing.Size(85, 19);
            this.chkClearLoudly.TabIndex = 0;
            // 
            // chkClearLight
            // 
            this.chkClearLight.Location = new System.Drawing.Point(133, 36);
            this.chkClearLight.Name = "chkClearLight";
            this.chkClearLight.Properties.Caption = "清除指示灯";
            this.chkClearLight.Size = new System.Drawing.Size(87, 19);
            this.chkClearLight.TabIndex = 0;
            // 
            // chkOpenValve
            // 
            this.chkOpenValve.Location = new System.Drawing.Point(26, 36);
            this.chkOpenValve.Name = "chkOpenValve";
            this.chkOpenValve.Properties.Caption = "开阀门";
            this.chkOpenValve.Size = new System.Drawing.Size(64, 19);
            this.chkOpenValve.TabIndex = 0;
            // 
            // groupControl4
            // 
            this.groupControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl4.Controls.Add(this.btStopValve);
            this.groupControl4.Controls.Add(this.btCloseValve);
            this.groupControl4.Controls.Add(this.btOpenValve);
            this.groupControl4.Location = new System.Drawing.Point(29, 138);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(543, 79);
            this.groupControl4.TabIndex = 3;
            this.groupControl4.Text = "阀门测试";
            // 
            // btStopValve
            // 
            this.btStopValve.Image = global::ConfigDevice.Properties.Resources.stop;
            this.btStopValve.Location = new System.Drawing.Point(283, 33);
            this.btStopValve.Name = "btStopValve";
            this.btStopValve.Size = new System.Drawing.Size(88, 32);
            this.btStopValve.TabIndex = 0;
            this.btStopValve.Text = "停止动作";
            this.btStopValve.Click += new System.EventHandler(this.btStopValve_Click);
            // 
            // btCloseValve
            // 
            this.btCloseValve.Image = global::ConfigDevice.Properties.Resources.power_off;
            this.btCloseValve.Location = new System.Drawing.Point(155, 33);
            this.btCloseValve.Name = "btCloseValve";
            this.btCloseValve.Size = new System.Drawing.Size(88, 32);
            this.btCloseValve.TabIndex = 0;
            this.btCloseValve.Text = "关阀门";
            this.btCloseValve.Click += new System.EventHandler(this.btCloseValve_Click);
            // 
            // btOpenValve
            // 
            this.btOpenValve.Image = ((System.Drawing.Image)(resources.GetObject("btOpenValve.Image")));
            this.btOpenValve.Location = new System.Drawing.Point(28, 33);
            this.btOpenValve.Name = "btOpenValve";
            this.btOpenValve.Size = new System.Drawing.Size(88, 32);
            this.btOpenValve.TabIndex = 0;
            this.btOpenValve.Text = "开阀门";
            this.btOpenValve.Click += new System.EventHandler(this.btOpenValve_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl2.Controls.Add(this.spePreTime);
            this.groupControl2.Controls.Add(this.speProbeEC);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Location = new System.Drawing.Point(29, 223);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(543, 71);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "阀门电气参数测试";
            // 
            // spePreTime
            // 
            this.spePreTime.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spePreTime.Location = new System.Drawing.Point(366, 35);
            this.spePreTime.Name = "spePreTime";
            this.spePreTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spePreTime.Properties.Mask.EditMask = "d";
            this.spePreTime.Properties.MaxValue = new decimal(new int[] {
            2700,
            0,
            0,
            0});
            this.spePreTime.Properties.MinValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spePreTime.Size = new System.Drawing.Size(100, 21);
            this.spePreTime.TabIndex = 1;
            // 
            // speProbeEC
            // 
            this.speProbeEC.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.speProbeEC.Location = new System.Drawing.Point(91, 35);
            this.speProbeEC.Name = "speProbeEC";
            this.speProbeEC.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speProbeEC.Properties.Mask.EditMask = "d";
            this.speProbeEC.Properties.MaxValue = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.speProbeEC.Properties.MinValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.speProbeEC.Size = new System.Drawing.Size(100, 21);
            this.speProbeEC.TabIndex = 1;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(470, 38);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(12, 14);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "秒";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(196, 38);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(18, 14);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "mA";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(264, 38);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(96, 14);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "最长动作时间间隔";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(28, 38);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "可燃气探头";
            // 
            // groupControl1
            // 
            this.groupControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl1.Controls.Add(this.btAutoRefresh);
            this.groupControl1.Controls.Add(this.edtT);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.edtEC);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.edtGasProbe);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.edtValveState);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(29, 43);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(543, 89);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "设备状态";
            // 
            // btAutoRefresh
            // 
            this.btAutoRefresh.Image = global::ConfigDevice.Properties.Resources.refresh2;
            this.btAutoRefresh.Location = new System.Drawing.Point(400, 36);
            this.btAutoRefresh.Name = "btAutoRefresh";
            this.btAutoRefresh.Size = new System.Drawing.Size(100, 39);
            this.btAutoRefresh.TabIndex = 3;
            this.btAutoRefresh.Text = "自动刷新";
            this.btAutoRefresh.CheckedChanged += new System.EventHandler(this.btAutoRefresh_CheckedChanged);
            // 
            // edtT
            // 
            this.edtT.Location = new System.Drawing.Point(271, 58);
            this.edtT.Name = "edtT";
            this.edtT.Properties.ReadOnly = true;
            this.edtT.Size = new System.Drawing.Size(100, 21);
            this.edtT.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(217, 61);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "温      度";
            // 
            // edtEC
            // 
            this.edtEC.Location = new System.Drawing.Point(271, 31);
            this.edtEC.Name = "edtEC";
            this.edtEC.Properties.ReadOnly = true;
            this.edtEC.Size = new System.Drawing.Size(100, 21);
            this.edtEC.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(217, 34);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "阀门电流";
            // 
            // edtGasProbe
            // 
            this.edtGasProbe.Location = new System.Drawing.Point(91, 58);
            this.edtGasProbe.Name = "edtGasProbe";
            this.edtGasProbe.Properties.ReadOnly = true;
            this.edtGasProbe.Size = new System.Drawing.Size(100, 21);
            this.edtGasProbe.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(25, 61);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "可燃气探头";
            // 
            // edtValveState
            // 
            this.edtValveState.Location = new System.Drawing.Point(91, 31);
            this.edtValveState.Name = "edtValveState";
            this.edtValveState.Properties.ReadOnly = true;
            this.edtValveState.Size = new System.Drawing.Size(100, 21);
            this.edtValveState.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(37, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "阀门状态";
            // 
            // tsDoorInput
            // 
            this.tsDoorInput.Font = new System.Drawing.Font("宋体", 12F);
            this.tsDoorInput.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsDoorInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSave,
            this.btRefresh});
            this.tsDoorInput.Location = new System.Drawing.Point(0, 0);
            this.tsDoorInput.Name = "tsDoorInput";
            this.tsDoorInput.Size = new System.Drawing.Size(985, 31);
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
            // btRefresh
            // 
            this.btRefresh.Image = global::ConfigDevice.Properties.Resources.refresh;
            this.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(100, 28);
            this.btRefresh.Text = "刷新数据";
            // 
            // pageJcsz
            // 
            this.pageJcsz.Controls.Add(this.frmSetting);
            this.pageJcsz.Name = "pageJcsz";
            this.pageJcsz.Size = new System.Drawing.Size(985, 614);
            this.pageJcsz.Text = "基础配置";
            // 
            // frmSetting
            // 
            this.frmSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmSetting.Location = new System.Drawing.Point(0, 0);
            this.frmSetting.Name = "frmSetting";
            this.frmSetting.Size = new System.Drawing.Size(985, 614);
            this.frmSetting.TabIndex = 0;
            // 
            // pageCommand
            // 
            this.pageCommand.Controls.Add(this.viewCommandEdit);
            this.pageCommand.Controls.Add(this.viewLogicSetting);
            this.pageCommand.Controls.Add(this.panelControl1);
            this.pageCommand.Name = "pageCommand";
            this.pageCommand.Size = new System.Drawing.Size(985, 614);
            this.pageCommand.Text = "指令配置";
            // 
            // viewCommandEdit
            // 
            this.viewCommandEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewCommandEdit.Location = new System.Drawing.Point(0, 262);
            this.viewCommandEdit.Name = "viewCommandEdit";
            this.viewCommandEdit.Size = new System.Drawing.Size(985, 352);
            this.viewCommandEdit.TabIndex = 0;
            // 
            // viewLogicSetting
            // 
            this.viewLogicSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.viewLogicSetting.Location = new System.Drawing.Point(0, 49);
            this.viewLogicSetting.Name = "viewLogicSetting";
            this.viewLogicSetting.Size = new System.Drawing.Size(985, 213);
            this.viewLogicSetting.TabIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.cbxGroup);
            this.panelControl1.Controls.Add(this.lblGroupName);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(985, 49);
            this.panelControl1.TabIndex = 2;
            // 
            // cbxGroup
            // 
            this.cbxGroup.Location = new System.Drawing.Point(137, 12);
            this.cbxGroup.Name = "cbxGroup";
            this.cbxGroup.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cbxGroup.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroup.Properties.Appearance.Options.UseFont = true;
            this.cbxGroup.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroup.Properties.AppearanceDisabled.Options.UseFont = true;
            this.cbxGroup.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroup.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cbxGroup.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroup.Properties.AppearanceFocused.Options.UseFont = true;
            this.cbxGroup.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroup.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.cbxGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxGroup.Properties.DropDownRows = 16;
            this.cbxGroup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxGroup.Size = new System.Drawing.Size(347, 26);
            this.cbxGroup.TabIndex = 11;
            // 
            // lblGroupName
            // 
            this.lblGroupName.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblGroupName.Appearance.Options.UseFont = true;
            this.lblGroupName.Location = new System.Drawing.Point(67, 15);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(64, 19);
            this.lblGroupName.TabIndex = 10;
            this.lblGroupName.Text = "触发动作";
            // 
            // FrmFlammableGasProbe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(994, 675);
            this.Controls.Add(this.tctrlEdit);
            this.Name = "FrmFlammableGasProbe";
            this.Load += new System.EventHandler(this.FrmFlammableGasProbe_Load);
            this.Controls.SetChildIndex(this.tctrlEdit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.tctrlEdit)).EndInit();
            this.tctrlEdit.ResumeLayout(false);
            this.pagePzjm.ResumeLayout(false);
            this.pagePzjm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkClearLoudly.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkClearLight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOpenValve.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spePreTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speProbeEC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtGasProbe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtValveState.Properties)).EndInit();
            this.tsDoorInput.ResumeLayout(false);
            this.tsDoorInput.PerformLayout();
            this.pageJcsz.ResumeLayout(false);
            this.pageCommand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxGroup.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tctrlEdit;
        private DevExpress.XtraTab.XtraTabPage pageJcsz;
        private ViewBaseEdit frmSetting;
        private DevExpress.XtraTab.XtraTabPage pagePzjm;
        private System.Windows.Forms.ToolStrip tsDoorInput;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private DevExpress.XtraTab.XtraTabPage pageCommand;
        private ViewCommandSetting viewCommandEdit;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbxGroup;
        private DevExpress.XtraEditors.LabelControl lblGroupName;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit edtT;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit edtEC;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit edtGasProbe;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit edtValveState;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SpinEdit spePreTime;
        private DevExpress.XtraEditors.SpinEdit speProbeEC;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.CheckEdit chkClearLoudly;
        private DevExpress.XtraEditors.CheckEdit chkClearLight;
        private DevExpress.XtraEditors.CheckEdit chkOpenValve;
        private DevExpress.XtraEditors.CheckButton btAutoRefresh;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btStopValve;
        private DevExpress.XtraEditors.SimpleButton btCloseValve;
        private DevExpress.XtraEditors.SimpleButton btOpenValve;
        private ViewLogicSetting viewLogicSetting;
    }
}
