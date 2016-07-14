﻿namespace ConfigDevice
{
    partial class FrmFunctionsBoard
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
            this.tctrlEdit = new DevExpress.XtraTab.XtraTabControl();
            this.pageJmpz = new DevExpress.XtraTab.XtraTabPage();
            this.plKeyOption = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ceBackSafeSetting = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ceLeaveSafeSetting = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.ceAlarmSound = new DevExpress.XtraEditors.CheckEdit();
            this.ceDoorWindowSound = new DevExpress.XtraEditors.CheckEdit();
            this.speAlarmDelay = new DevExpress.XtraEditors.SpinEdit();
            this.speHintVolume = new DevExpress.XtraEditors.SpinEdit();
            this.speSpeakerAddress = new DevExpress.XtraEditors.SpinEdit();
            this.speSecurityDelay = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblLightSize = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            this.tbcLight = new DevExpress.XtraEditors.TrackBarControl();
            this.ceLittleLight = new DevExpress.XtraEditors.CheckEdit();
            this.keySettingTools = new ConfigDevice.KeySettingTools();
            this.tsDoorInput = new System.Windows.Forms.ToolStrip();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.pageJcsz = new DevExpress.XtraTab.XtraTabPage();
            this.viewBaseSetting = new ConfigDevice.ViewBaseEdit();
            this.pageZlpz = new DevExpress.XtraTab.XtraTabPage();
            this.viewCommandEdit = new ConfigDevice.ViewCommandSetting();
            ((System.ComponentModel.ISupportInitialize)(this.tctrlEdit)).BeginInit();
            this.tctrlEdit.SuspendLayout();
            this.pageJmpz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plKeyOption)).BeginInit();
            this.plKeyOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceBackSafeSetting)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceLeaveSafeSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceAlarmSound.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceDoorWindowSound.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speAlarmDelay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speHintVolume.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speSpeakerAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speSecurityDelay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbcLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbcLight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLittleLight.Properties)).BeginInit();
            this.tsDoorInput.SuspendLayout();
            this.pageJcsz.SuspendLayout();
            this.pageZlpz.SuspendLayout();
            this.SuspendLayout();
            // 
            // tctrlEdit
            // 
            this.tctrlEdit.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tctrlEdit.AppearancePage.Header.Options.UseFont = true;
            this.tctrlEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tctrlEdit.Location = new System.Drawing.Point(0, 24);
            this.tctrlEdit.Name = "tctrlEdit";
            this.tctrlEdit.SelectedTabPage = this.pageJmpz;
            this.tctrlEdit.Size = new System.Drawing.Size(1018, 718);
            this.tctrlEdit.TabIndex = 1;
            this.tctrlEdit.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageJcsz,
            this.pageJmpz,
            this.pageZlpz});
            this.tctrlEdit.Text = "xtraTabControl1";
            // 
            // pageJmpz
            // 
            this.pageJmpz.Controls.Add(this.plKeyOption);
            this.pageJmpz.Controls.Add(this.keySettingTools);
            this.pageJmpz.Controls.Add(this.tsDoorInput);
            this.pageJmpz.Name = "pageJmpz";
            this.pageJmpz.Size = new System.Drawing.Size(1009, 681);
            this.pageJmpz.Text = "配置界面";
            // 
            // plKeyOption
            // 
            this.plKeyOption.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.plKeyOption.Controls.Add(this.groupControl2);
            this.plKeyOption.Controls.Add(this.groupControl3);
            this.plKeyOption.Controls.Add(this.groupControl1);
            this.plKeyOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plKeyOption.Location = new System.Drawing.Point(0, 31);
            this.plKeyOption.Name = "plKeyOption";
            this.plKeyOption.Size = new System.Drawing.Size(1009, 358);
            this.plKeyOption.TabIndex = 15;
            // 
            // groupControl2
            // 
            this.groupControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl2.Location = new System.Drawing.Point(502, 11);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(344, 143);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "密码页面设置";
            // 
            // groupControl3
            // 
            this.groupControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl3.Controls.Add(this.groupBox1);
            this.groupControl3.Controls.Add(this.groupBox2);
            this.groupControl3.Controls.Add(this.ceAlarmSound);
            this.groupControl3.Controls.Add(this.ceDoorWindowSound);
            this.groupControl3.Controls.Add(this.speAlarmDelay);
            this.groupControl3.Controls.Add(this.speHintVolume);
            this.groupControl3.Controls.Add(this.speSpeakerAddress);
            this.groupControl3.Controls.Add(this.speSecurityDelay);
            this.groupControl3.Controls.Add(this.labelControl7);
            this.groupControl3.Controls.Add(this.labelControl5);
            this.groupControl3.Controls.Add(this.labelControl6);
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Location = new System.Drawing.Point(24, 181);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(864, 177);
            this.groupControl3.TabIndex = 0;
            this.groupControl3.Text = "安防";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ceBackSafeSetting);
            this.groupBox1.Location = new System.Drawing.Point(442, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 82);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "回家撤防";
            // 
            // ceBackSafeSetting
            // 
            this.ceBackSafeSetting.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ceBackSafeSetting.CheckOnClick = true;
            this.ceBackSafeSetting.ColumnWidth = 100;
            this.ceBackSafeSetting.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.ceBackSafeSetting.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("全部撤防")});
            this.ceBackSafeSetting.LeftCoord = 0;
            this.ceBackSafeSetting.Location = new System.Drawing.Point(6, 16);
            this.ceBackSafeSetting.MultiColumn = true;
            this.ceBackSafeSetting.Name = "ceBackSafeSetting";
            this.ceBackSafeSetting.Size = new System.Drawing.Size(374, 60);
            this.ceBackSafeSetting.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ceLeaveSafeSetting);
            this.groupBox2.Location = new System.Drawing.Point(35, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 82);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "离家安防";
            // 
            // ceLeaveSafeSetting
            // 
            this.ceLeaveSafeSetting.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ceLeaveSafeSetting.CheckOnClick = true;
            this.ceLeaveSafeSetting.ColumnWidth = 60;
            this.ceLeaveSafeSetting.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.ceLeaveSafeSetting.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("1"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("2"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("3"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("4"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("5"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("6"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("7"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("8"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("9"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("10"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("11"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("12"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("13"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("14"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("15"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("全部")});
            this.ceLeaveSafeSetting.LeftCoord = 0;
            this.ceLeaveSafeSetting.Location = new System.Drawing.Point(6, 16);
            this.ceLeaveSafeSetting.MultiColumn = true;
            this.ceLeaveSafeSetting.Name = "ceLeaveSafeSetting";
            this.ceLeaveSafeSetting.Size = new System.Drawing.Size(374, 60);
            this.ceLeaveSafeSetting.TabIndex = 0;
            this.ceLeaveSafeSetting.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.ceLeaveSafeSetting_ItemCheck);
            // 
            // ceAlarmSound
            // 
            this.ceAlarmSound.Location = new System.Drawing.Point(33, 119);
            this.ceAlarmSound.Name = "ceAlarmSound";
            this.ceAlarmSound.Properties.Caption = "预警提示音";
            this.ceAlarmSound.Size = new System.Drawing.Size(85, 19);
            this.ceAlarmSound.TabIndex = 3;
            // 
            // ceDoorWindowSound
            // 
            this.ceDoorWindowSound.Location = new System.Drawing.Point(33, 147);
            this.ceDoorWindowSound.Name = "ceDoorWindowSound";
            this.ceDoorWindowSound.Properties.Caption = "门窗提示音";
            this.ceDoorWindowSound.Size = new System.Drawing.Size(85, 19);
            this.ceDoorWindowSound.TabIndex = 3;
            // 
            // speAlarmDelay
            // 
            this.speAlarmDelay.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speAlarmDelay.Location = new System.Drawing.Point(191, 145);
            this.speAlarmDelay.Name = "speAlarmDelay";
            this.speAlarmDelay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speAlarmDelay.Properties.Mask.EditMask = "d";
            this.speAlarmDelay.Properties.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.speAlarmDelay.Size = new System.Drawing.Size(61, 21);
            this.speAlarmDelay.TabIndex = 2;
            // 
            // speHintVolume
            // 
            this.speHintVolume.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speHintVolume.Location = new System.Drawing.Point(361, 118);
            this.speHintVolume.Name = "speHintVolume";
            this.speHintVolume.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speHintVolume.Properties.Mask.EditMask = "d";
            this.speHintVolume.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.speHintVolume.Properties.MaxValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.speHintVolume.Size = new System.Drawing.Size(60, 21);
            this.speHintVolume.TabIndex = 2;
            // 
            // speSpeakerAddress
            // 
            this.speSpeakerAddress.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speSpeakerAddress.Location = new System.Drawing.Point(361, 145);
            this.speSpeakerAddress.Name = "speSpeakerAddress";
            this.speSpeakerAddress.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speSpeakerAddress.Properties.MaxValue = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.speSpeakerAddress.Size = new System.Drawing.Size(60, 21);
            this.speSpeakerAddress.TabIndex = 2;
            // 
            // speSecurityDelay
            // 
            this.speSecurityDelay.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speSecurityDelay.Location = new System.Drawing.Point(191, 118);
            this.speSecurityDelay.Name = "speSecurityDelay";
            this.speSecurityDelay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speSecurityDelay.Properties.Mask.EditMask = "d";
            this.speSecurityDelay.Properties.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.speSecurityDelay.Size = new System.Drawing.Size(61, 21);
            this.speSecurityDelay.TabIndex = 2;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(271, 148);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(84, 14);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "提示音功放地址";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(137, 148);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "预警延时";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(283, 122);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 14);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "门窗提示音量";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(137, 121);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "布防延时";
            // 
            // groupControl1
            // 
            this.groupControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.lblLightSize);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.trackBarControl1);
            this.groupControl1.Controls.Add(this.tbcLight);
            this.groupControl1.Controls.Add(this.ceLittleLight);
            this.groupControl1.Location = new System.Drawing.Point(24, 11);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(472, 143);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "指示灯";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(262, 56);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(31, 24);
            this.labelControl3.TabIndex = 3;
            // 
            // lblLightSize
            // 
            this.lblLightSize.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblLightSize.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblLightSize.Appearance.Options.UseFont = true;
            this.lblLightSize.Appearance.Options.UseForeColor = true;
            this.lblLightSize.Appearance.Options.UseTextOptions = true;
            this.lblLightSize.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblLightSize.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblLightSize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLightSize.Location = new System.Drawing.Point(262, 96);
            this.lblLightSize.Name = "lblLightSize";
            this.lblLightSize.Size = new System.Drawing.Size(31, 24);
            this.lblLightSize.TabIndex = 3;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(14, 98);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(60, 14);
            this.labelControl8.TabIndex = 2;
            this.labelControl8.Text = "指示灯亮度";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 64);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "指示灯亮度";
            // 
            // trackBarControl1
            // 
            this.trackBarControl1.EditValue = null;
            this.trackBarControl1.Location = new System.Drawing.Point(80, 58);
            this.trackBarControl1.Name = "trackBarControl1";
            this.trackBarControl1.Properties.LargeChange = 10;
            this.trackBarControl1.Size = new System.Drawing.Size(180, 42);
            this.trackBarControl1.TabIndex = 1;
            this.trackBarControl1.Modified += new System.EventHandler(this.tbcLight_Modified);
            this.trackBarControl1.EditValueChanged += new System.EventHandler(this.tbcLight_Modified);
            // 
            // tbcLight
            // 
            this.tbcLight.EditValue = null;
            this.tbcLight.Location = new System.Drawing.Point(80, 96);
            this.tbcLight.Name = "tbcLight";
            this.tbcLight.Properties.LargeChange = 10;
            this.tbcLight.Size = new System.Drawing.Size(180, 42);
            this.tbcLight.TabIndex = 1;
            this.tbcLight.Modified += new System.EventHandler(this.tbcLight_Modified);
            this.tbcLight.EditValueChanged += new System.EventHandler(this.tbcLight_Modified);
            // 
            // ceLittleLight
            // 
            this.ceLittleLight.Location = new System.Drawing.Point(12, 33);
            this.ceLittleLight.Name = "ceLittleLight";
            this.ceLittleLight.Properties.Caption = "指示灯熄灭时微亮";
            this.ceLittleLight.Size = new System.Drawing.Size(128, 19);
            this.ceLittleLight.TabIndex = 0;
            // 
            // keySettingTools
            // 
            this.keySettingTools.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.keySettingTools.ImeMode = System.Windows.Forms.ImeMode.On;
            this.keySettingTools.Location = new System.Drawing.Point(0, 389);
            this.keySettingTools.Name = "keySettingTools";
            this.keySettingTools.Size = new System.Drawing.Size(1009, 292);
            this.keySettingTools.TabIndex = 14;
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
            this.tsDoorInput.Size = new System.Drawing.Size(1009, 31);
            this.tsDoorInput.TabIndex = 8;
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
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // pageJcsz
            // 
            this.pageJcsz.Controls.Add(this.viewBaseSetting);
            this.pageJcsz.Name = "pageJcsz";
            this.pageJcsz.Size = new System.Drawing.Size(1009, 681);
            this.pageJcsz.Text = "基础配置";
            // 
            // viewBaseSetting
            // 
            this.viewBaseSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewBaseSetting.Location = new System.Drawing.Point(0, 0);
            this.viewBaseSetting.Name = "viewBaseSetting";
            this.viewBaseSetting.Size = new System.Drawing.Size(1009, 681);
            this.viewBaseSetting.TabIndex = 0;
            // 
            // pageZlpz
            // 
            this.pageZlpz.Controls.Add(this.viewCommandEdit);
            this.pageZlpz.Name = "pageZlpz";
            this.pageZlpz.Size = new System.Drawing.Size(1009, 681);
            this.pageZlpz.Text = "指令配置";
            // 
            // viewCommandEdit
            // 
            this.viewCommandEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewCommandEdit.Location = new System.Drawing.Point(0, 0);
            this.viewCommandEdit.Name = "viewCommandEdit";
            this.viewCommandEdit.ShowCommandBar = false;
            this.viewCommandEdit.ShowToolBar = false;
            this.viewCommandEdit.Size = new System.Drawing.Size(1009, 681);
            this.viewCommandEdit.TabIndex = 1;
            // 
            // FrmFunctionsBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 742);
            this.Controls.Add(this.tctrlEdit);
            this.Name = "FrmFunctionsBoard";
            this.Text = "FrmBaseDevice";
            this.Load += new System.EventHandler(this.FrmBaseDevice_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmButton2_FormClosing);
            this.Controls.SetChildIndex(this.tctrlEdit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.tctrlEdit)).EndInit();
            this.tctrlEdit.ResumeLayout(false);
            this.pageJmpz.ResumeLayout(false);
            this.pageJmpz.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plKeyOption)).EndInit();
            this.plKeyOption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceBackSafeSetting)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceLeaveSafeSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceAlarmSound.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceDoorWindowSound.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speAlarmDelay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speHintVolume.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speSpeakerAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speSecurityDelay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbcLight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbcLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLittleLight.Properties)).EndInit();
            this.tsDoorInput.ResumeLayout(false);
            this.tsDoorInput.PerformLayout();
            this.pageJcsz.ResumeLayout(false);
            this.pageZlpz.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tctrlEdit;
        private DevExpress.XtraTab.XtraTabPage pageJcsz;
        private ViewBaseEdit viewBaseSetting;
        private DevExpress.XtraTab.XtraTabPage pageJmpz;
        private System.Windows.Forms.ToolStrip tsDoorInput;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private DevExpress.XtraTab.XtraTabPage pageZlpz;
        private ViewCommandSetting viewCommandEdit;
        private KeySettingTools keySettingTools;
        private DevExpress.XtraEditors.PanelControl plKeyOption;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit ceLittleLight;
        private DevExpress.XtraEditors.TrackBarControl tbcLight;
        private DevExpress.XtraEditors.LabelControl lblLightSize;
        private DevExpress.XtraEditors.CheckEdit ceAlarmSound;
        private DevExpress.XtraEditors.CheckEdit ceDoorWindowSound;
        private DevExpress.XtraEditors.SpinEdit speAlarmDelay;
        private DevExpress.XtraEditors.SpinEdit speHintVolume;
        private DevExpress.XtraEditors.SpinEdit speSpeakerAddress;
        private DevExpress.XtraEditors.SpinEdit speSecurityDelay;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.GroupBox groupBox2;
        protected DevExpress.XtraEditors.CheckedListBoxControl ceLeaveSafeSetting;
        private System.Windows.Forms.GroupBox groupBox1;
        protected DevExpress.XtraEditors.CheckedListBoxControl ceBackSafeSetting;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TrackBarControl trackBarControl1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
 


    }
}