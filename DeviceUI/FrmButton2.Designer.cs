namespace ConfigDevice
{
    partial class FrmButton2
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
            this.list2Keys = new ConfigDevice.KeyBaseSetting();
            this.plKeyOption = new DevExpress.XtraEditors.PanelControl();
            this.keySecuritySetting = new ConfigDevice.ToolsUI.LCDUI.KeySecuritySetting();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.rgInitState = new DevExpress.XtraEditors.RadioGroup();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblLightSize = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tbcLight = new DevExpress.XtraEditors.TrackBarControl();
            this.ceLittleLight = new DevExpress.XtraEditors.CheckEdit();
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
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgInitState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
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
            this.tctrlEdit.Text = "去xtraTabControl1";
            // 
            // pageJmpz
            // 
            this.pageJmpz.Controls.Add(this.list2Keys);
            this.pageJmpz.Controls.Add(this.plKeyOption);
            this.pageJmpz.Controls.Add(this.tsDoorInput);
            this.pageJmpz.Name = "pageJmpz";
            this.pageJmpz.Size = new System.Drawing.Size(1009, 681);
            this.pageJmpz.Text = "配置界面";
            // 
            // list2Keys
            // 
            this.list2Keys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list2Keys.ImeMode = System.Windows.Forms.ImeMode.On;
            this.list2Keys.Location = new System.Drawing.Point(0, 434);
            this.list2Keys.Name = "list2Keys";
            this.list2Keys.ShowCount = 8;
            this.list2Keys.Size = new System.Drawing.Size(1009, 247);
            this.list2Keys.TabIndex = 16;
            // 
            // plKeyOption
            // 
            this.plKeyOption.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.plKeyOption.Controls.Add(this.keySecuritySetting);
            this.plKeyOption.Controls.Add(this.groupControl2);
            this.plKeyOption.Controls.Add(this.groupControl1);
            this.plKeyOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.plKeyOption.Location = new System.Drawing.Point(0, 31);
            this.plKeyOption.Name = "plKeyOption";
            this.plKeyOption.Size = new System.Drawing.Size(1009, 403);
            this.plKeyOption.TabIndex = 15;
            // 
            // keySecuritySetting
            // 
            this.keySecuritySetting.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.keySecuritySetting.Location = new System.Drawing.Point(2, 181);
            this.keySecuritySetting.Name = "keySecuritySetting";
            this.keySecuritySetting.Size = new System.Drawing.Size(1005, 220);
            this.keySecuritySetting.TabIndex = 2;
            // 
            // groupControl2
            // 
            this.groupControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl2.Controls.Add(this.rgInitState);
            this.groupControl2.Location = new System.Drawing.Point(262, 11);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(626, 164);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "面板上电初始状态";
            // 
            // rgInitState
            // 
            this.rgInitState.Location = new System.Drawing.Point(28, 25);
            this.rgInitState.Name = "rgInitState";
            this.rgInitState.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rgInitState.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "指示灯开，并执行控制"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "指示灯关，并执行控制"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(4, "恢复关机前指示灯状态，并执行相应的控制"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(4, "只恢复关机前指示灯状态，但不执行控制")});
            this.rgInitState.Size = new System.Drawing.Size(297, 132);
            this.rgInitState.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl1.Controls.Add(this.lblLightSize);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.tbcLight);
            this.groupControl1.Controls.Add(this.ceLittleLight);
            this.groupControl1.Location = new System.Drawing.Point(24, 11);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(232, 164);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "指示灯";
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
            this.lblLightSize.Location = new System.Drawing.Point(104, 87);
            this.lblLightSize.Name = "lblLightSize";
            this.lblLightSize.Size = new System.Drawing.Size(42, 24);
            this.lblLightSize.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(38, 92);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "指示灯亮度";
            // 
            // tbcLight
            // 
            this.tbcLight.EditValue = null;
            this.tbcLight.Location = new System.Drawing.Point(35, 111);
            this.tbcLight.Name = "tbcLight";
            this.tbcLight.Properties.LargeChange = 10;
            this.tbcLight.Size = new System.Drawing.Size(180, 45);
            this.tbcLight.TabIndex = 1;
            this.tbcLight.Modified += new System.EventHandler(this.tbcLight_Modified);
            this.tbcLight.EditValueChanged += new System.EventHandler(this.tbcLight_Modified);
            // 
            // ceLittleLight
            // 
            this.ceLittleLight.Location = new System.Drawing.Point(36, 45);
            this.ceLittleLight.Name = "ceLittleLight";
            this.ceLittleLight.Properties.Caption = "指示灯熄灭时微亮";
            this.ceLittleLight.Size = new System.Drawing.Size(128, 19);
            this.ceLittleLight.TabIndex = 0;
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
            this.pageJcsz.Size = new System.Drawing.Size(1009, 705);
            this.pageJcsz.Text = "基础配置";
            // 
            // viewBaseSetting
            // 
            this.viewBaseSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewBaseSetting.Location = new System.Drawing.Point(0, 0);
            this.viewBaseSetting.Name = "viewBaseSetting";
            this.viewBaseSetting.Size = new System.Drawing.Size(1009, 705);
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
            // FrmButton2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 742);
            this.Controls.Add(this.tctrlEdit);
            this.Name = "FrmButton2";
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
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgInitState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
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
        private DevExpress.XtraEditors.PanelControl plKeyOption;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit ceLittleLight;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TrackBarControl tbcLight;
        private DevExpress.XtraEditors.LabelControl lblLightSize;
        private DevExpress.XtraEditors.RadioGroup rgInitState;
        private ConfigDevice.ToolsUI.LCDUI.KeySecuritySetting keySecuritySetting;
        private KeyBaseSetting list2Keys;
 


    }
}