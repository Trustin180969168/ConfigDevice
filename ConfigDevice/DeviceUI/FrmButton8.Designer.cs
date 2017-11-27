namespace ConfigDevice
{
    partial class FrmButton8
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
            this.list8Keys = new ConfigDevice.KeyBaseSetting();
            this.plKeyOption = new DevExpress.XtraEditors.PanelControl();
            this.keySecuritySetting = new ConfigDevice.ToolsUI.LCDUI.KeySecuritySetting();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.rgInitState = new DevExpress.XtraEditors.RadioGroup();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelSpecialCtrlObj = new ConfigDevice.PanelSpecialCtrlObj();
            this.rgpPanelKind = new DevExpress.XtraEditors.RadioGroup();
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
            ((System.ComponentModel.ISupportInitialize)(this.rgpPanelKind.Properties)).BeginInit();
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
            this.tctrlEdit.Size = new System.Drawing.Size(1030, 718);
            this.tctrlEdit.TabIndex = 1;
            this.tctrlEdit.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageJcsz,
            this.pageJmpz,
            this.pageZlpz});
            this.tctrlEdit.Text = "去xtraTabControl1";
            // 
            // pageJmpz
            // 
            this.pageJmpz.Controls.Add(this.list8Keys);
            this.pageJmpz.Controls.Add(this.plKeyOption);
            this.pageJmpz.Controls.Add(this.tsDoorInput);
            this.pageJmpz.Name = "pageJmpz";
            this.pageJmpz.Size = new System.Drawing.Size(1021, 681);
            this.pageJmpz.Text = "配置界面";
            // 
            // list8Keys
            // 
            this.list8Keys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list8Keys.ImeMode = System.Windows.Forms.ImeMode.On;
            this.list8Keys.Location = new System.Drawing.Point(0, 434);
            this.list8Keys.Name = "list8Keys";
            this.list8Keys.ShowCount = 8;
            this.list8Keys.Size = new System.Drawing.Size(1021, 247);
            this.list8Keys.TabIndex = 16;
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
            this.plKeyOption.Size = new System.Drawing.Size(1021, 403);
            this.plKeyOption.TabIndex = 15;
            // 
            // keySecuritySetting
            // 
            this.keySecuritySetting.Location = new System.Drawing.Point(2, 181);
            this.keySecuritySetting.Name = "keySecuritySetting";
            this.keySecuritySetting.Size = new System.Drawing.Size(853, 220);
            this.keySecuritySetting.TabIndex = 2;
            // 
            // groupControl2
            // 
            this.groupControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl2.Controls.Add(this.rgInitState);
            this.groupControl2.Location = new System.Drawing.Point(712, 11);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(308, 164);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "面板上电初始状态";
            // 
            // rgInitState
            // 
            this.rgInitState.Location = new System.Drawing.Point(11, 27);
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
            this.groupControl1.Controls.Add(this.panelSpecialCtrlObj);
            this.groupControl1.Controls.Add(this.rgpPanelKind);
            this.groupControl1.Location = new System.Drawing.Point(24, 11);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(657, 164);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "按键功能类配置";
            // 
            // panelSpecialCtrlObj
            // 
            this.panelSpecialCtrlObj.Location = new System.Drawing.Point(213, 24);
            this.panelSpecialCtrlObj.Name = "panelSpecialCtrlObj";
            this.panelSpecialCtrlObj.Size = new System.Drawing.Size(406, 132);
            this.panelSpecialCtrlObj.TabIndex = 2;
            // 
            // rgpPanelKind
            // 
            this.rgpPanelKind.EditValue = 0;
            this.rgpPanelKind.Location = new System.Drawing.Point(5, 24);
            this.rgpPanelKind.Name = "rgpPanelKind";
            this.rgpPanelKind.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rgpPanelKind.Properties.Columns = 2;
            this.rgpPanelKind.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "键盘样式-0"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "键盘样式-1"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "键盘样式-2"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "键盘样式-3"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(4, "键盘样式-4"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(5, "键盘样式-5"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(6, "键盘样式-6"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(7, "键盘样式-7")});
            this.rgpPanelKind.Size = new System.Drawing.Size(202, 132);
            this.rgpPanelKind.TabIndex = 1;
            this.rgpPanelKind.SelectedIndexChanged += new System.EventHandler(this.rgpPanelKind_SelectedIndexChanged);
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
            this.tsDoorInput.Size = new System.Drawing.Size(1021, 31);
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
            this.pageJcsz.Size = new System.Drawing.Size(1125, 681);
            this.pageJcsz.Text = "基础配置";
            // 
            // viewBaseSetting
            // 
            this.viewBaseSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewBaseSetting.Location = new System.Drawing.Point(0, 0);
            this.viewBaseSetting.Name = "viewBaseSetting";
            this.viewBaseSetting.Size = new System.Drawing.Size(1125, 681);
            this.viewBaseSetting.TabIndex = 0;
            // 
            // pageZlpz
            // 
            this.pageZlpz.Controls.Add(this.viewCommandEdit);
            this.pageZlpz.Name = "pageZlpz";
            this.pageZlpz.Size = new System.Drawing.Size(1125, 681);
            this.pageZlpz.Text = "指令配置";
            // 
            // viewCommandEdit
            // 
            this.viewCommandEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewCommandEdit.Location = new System.Drawing.Point(0, 0);
            this.viewCommandEdit.Name = "viewCommandEdit";
            this.viewCommandEdit.ShowCommandBar = false;
            this.viewCommandEdit.ShowToolBar = false;
            this.viewCommandEdit.Size = new System.Drawing.Size(1125, 681);
            this.viewCommandEdit.TabIndex = 1;
            // 
            // FrmButton8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 742);
            this.Controls.Add(this.tctrlEdit);
            this.Name = "FrmButton8";
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
            ((System.ComponentModel.ISupportInitialize)(this.rgpPanelKind.Properties)).EndInit();
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
        private DevExpress.XtraEditors.RadioGroup rgInitState;
        private ConfigDevice.ToolsUI.LCDUI.KeySecuritySetting keySecuritySetting;
        private KeyBaseSetting list8Keys;
        private DevExpress.XtraEditors.RadioGroup rgpPanelKind;
        private PanelSpecialCtrlObj panelSpecialCtrlObj;
 


    }
}