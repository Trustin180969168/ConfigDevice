namespace ConfigDevice
{
    partial class FrmWeatherStation
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
            this.pagePzjm = new DevExpress.XtraTab.XtraTabPage();
            this.tsDoorInput = new System.Windows.Forms.ToolStrip();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.pageJcsz = new DevExpress.XtraTab.XtraTabPage();
            this.frmSetting = new ConfigDevice.UCtrlBaseEdit();
            this.pageActionList = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.tctrlEdit)).BeginInit();
            this.tctrlEdit.SuspendLayout();
            this.pagePzjm.SuspendLayout();
            this.tsDoorInput.SuspendLayout();
            this.pageJcsz.SuspendLayout();
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
            this.tctrlEdit.Size = new System.Drawing.Size(901, 506);
            this.tctrlEdit.TabIndex = 2;
            this.tctrlEdit.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pagePzjm,
            this.pageJcsz,
            this.pageActionList});
            this.tctrlEdit.Text = "xtraTabControl1";
            // 
            // pagePzjm
            // 
            this.pagePzjm.Controls.Add(this.tsDoorInput);
            this.pagePzjm.Name = "pagePzjm";
            this.pagePzjm.Size = new System.Drawing.Size(892, 469);
            this.pagePzjm.Text = "配置界面";
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
            this.tsDoorInput.Size = new System.Drawing.Size(892, 31);
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
            this.pageJcsz.Size = new System.Drawing.Size(892, 469);
            this.pageJcsz.Text = "基础配置";
            // 
            // frmSetting
            // 
            this.frmSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmSetting.Location = new System.Drawing.Point(0, 0);
            this.frmSetting.Name = "frmSetting";
            this.frmSetting.Size = new System.Drawing.Size(892, 469);
            this.frmSetting.TabIndex = 0;
            // 
            // pageActionList
            // 
            this.pageActionList.Name = "pageActionList";
            this.pageActionList.Size = new System.Drawing.Size(892, 469);
            this.pageActionList.Text = "指令配置";
            // 
            // FrmWeatherStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 530);
            this.Controls.Add(this.tctrlEdit);
            this.Name = "FrmWeatherStation";
            this.Text = "气象站配置";
            this.Load += new System.EventHandler(this.FrmWeatherStation_Load);
            this.Controls.SetChildIndex(this.tctrlEdit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.tctrlEdit)).EndInit();
            this.tctrlEdit.ResumeLayout(false);
            this.pagePzjm.ResumeLayout(false);
            this.pagePzjm.PerformLayout();
            this.tsDoorInput.ResumeLayout(false);
            this.tsDoorInput.PerformLayout();
            this.pageJcsz.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tctrlEdit;
        private DevExpress.XtraTab.XtraTabPage pagePzjm;
        private System.Windows.Forms.ToolStrip tsDoorInput;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private DevExpress.XtraTab.XtraTabPage pageJcsz;
        private UCtrlBaseEdit frmSetting;
        private DevExpress.XtraTab.XtraTabPage pageActionList;
    }
}