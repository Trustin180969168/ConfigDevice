﻿namespace ConfigDevice
{
    partial class FrmFourInput
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
            this.frmSetting = new ConfigDevice.UCtrlBaseEdit();
            this.pagePzjm = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.pageJcsz.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 12F);
            this.xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.pageJcsz;
            this.xtraTabControl1.Size = new System.Drawing.Size(792, 573);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pagePzjm,
            this.pageJcsz});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // pageJcsz
            // 
            this.pageJcsz.Controls.Add(this.frmSetting);
            this.pageJcsz.Name = "pageJcsz";
            this.pageJcsz.Size = new System.Drawing.Size(783, 536);
            this.pageJcsz.Text = "基础配置";
            // 
            // frmSetting
            // 
            this.frmSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmSetting.Location = new System.Drawing.Point(0, 0);
            this.frmSetting.Name = "frmSetting";
            this.frmSetting.Size = new System.Drawing.Size(783, 536);
            this.frmSetting.TabIndex = 0;
            // 
            // pagePzjm
            // 
            this.pagePzjm.Name = "pagePzjm";
            this.pagePzjm.Size = new System.Drawing.Size(783, 536);
            this.pagePzjm.Text = "配置界面";
            // 
            // FrmFourInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "FrmFourInput";
            this.Text = "门输入4配置";
            this.Load += new System.EventHandler(this.FrmFourInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.pageJcsz.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage pageJcsz;
        private UCtrlBaseEdit frmSetting;
        private DevExpress.XtraTab.XtraTabPage pagePzjm;


    }
}