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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.pageJcsz = new DevExpress.XtraTab.XtraTabPage();
            this.viewBaseSetting = new ConfigDevice.ViewBaseEdit();
            this.pageJmpz = new DevExpress.XtraTab.XtraTabPage();
            this.tsDoorInput = new System.Windows.Forms.ToolStrip();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.edtButton2Name = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.edtButton1Name = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.pageZlpz = new DevExpress.XtraTab.XtraTabPage();
            this.viewCommandEdit = new ConfigDevice.ViewCommandSetting();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.pageJcsz.SuspendLayout();
            this.pageJmpz.SuspendLayout();
            this.tsDoorInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtButton2Name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtButton1Name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.pageZlpz.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 12F);
            this.xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 24);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.pageJcsz;
            this.xtraTabControl1.Size = new System.Drawing.Size(1018, 718);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageJcsz,
            this.pageJmpz,
            this.pageZlpz});
            this.xtraTabControl1.Text = "xtraTabControl1";
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
            // pageJmpz
            // 
            this.pageJmpz.Controls.Add(this.groupControl1);
            this.pageJmpz.Controls.Add(this.tsDoorInput);
            this.pageJmpz.Name = "pageJmpz";
            this.pageJmpz.Size = new System.Drawing.Size(1009, 681);
            this.pageJmpz.Text = "配置界面";
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
            // 
            // btRefresh
            // 
            this.btRefresh.Image = global::ConfigDevice.Properties.Resources.refresh;
            this.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(100, 28);
            this.btRefresh.Text = "刷新数据";
            // 
            // edtButton2Name
            // 
            this.edtButton2Name.Location = new System.Drawing.Point(86, 64);
            this.edtButton2Name.Name = "edtButton2Name";
            this.edtButton2Name.Size = new System.Drawing.Size(300, 21);
            this.edtButton2Name.TabIndex = 11;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(49, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(31, 14);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "按键2";
            // 
            // edtButton1Name
            // 
            this.edtButton1Name.Location = new System.Drawing.Point(86, 37);
            this.edtButton1Name.Name = "edtButton1Name";
            this.edtButton1Name.Size = new System.Drawing.Size(300, 21);
            this.edtButton1Name.TabIndex = 12;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(49, 40);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(31, 14);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "按键1";
            // 
            // groupControl1
            // 
            this.groupControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.edtButton2Name);
            this.groupControl1.Controls.Add(this.edtButton1Name);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Location = new System.Drawing.Point(36, 53);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(613, 103);
            this.groupControl1.TabIndex = 13;
            this.groupControl1.Text = "按键配置";
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
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "FrmButton2";
            this.Text = "FrmBaseDevice";
            this.Load += new System.EventHandler(this.FrmBaseDevice_Load);
            this.Controls.SetChildIndex(this.xtraTabControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.pageJcsz.ResumeLayout(false);
            this.pageJmpz.ResumeLayout(false);
            this.pageJmpz.PerformLayout();
            this.tsDoorInput.ResumeLayout(false);
            this.tsDoorInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtButton2Name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtButton1Name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.pageZlpz.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage pageJcsz;
        private ViewBaseEdit viewBaseSetting;
        private DevExpress.XtraTab.XtraTabPage pageJmpz;
        private System.Windows.Forms.ToolStrip tsDoorInput;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit edtButton2Name;
        private DevExpress.XtraEditors.TextEdit edtButton1Name;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraTab.XtraTabPage pageZlpz;
        private ViewCommandSetting viewCommandEdit;


    }
}