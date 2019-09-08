namespace ConfigDevice
{
    partial class MenuSecurityControl
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
            this.viewCommandSetting = new ConfigDevice.ViewCommandSetting();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.rgSecurity = new DevExpress.XtraEditors.RadioGroup();
            this.lblAfgl = new DevExpress.XtraEditors.LabelControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgSecurity.Properties)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewCommandSetting
            // 
            this.viewCommandSetting.Dock = System.Windows.Forms.DockStyle.Left;
            this.viewCommandSetting.Location = new System.Drawing.Point(0, 0);
            this.viewCommandSetting.Name = "viewCommandSetting";
            this.viewCommandSetting.ShowCommandBar = true;
            this.viewCommandSetting.ShowToolBar = true;
            this.viewCommandSetting.Size = new System.Drawing.Size(1024, 258);
            this.viewCommandSetting.TabIndex = 2;
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl1.Controls.Add(this.rgSecurity);
            this.panelControl1.Controls.Add(this.lblAfgl);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(993, 165);
            this.panelControl1.TabIndex = 3;
            // 
            // rgSecurity
            // 
            this.rgSecurity.Location = new System.Drawing.Point(89, 10);
            this.rgSecurity.Name = "rgSecurity";
            this.rgSecurity.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.rgSecurity.Properties.Appearance.Options.UseFont = true;
            this.rgSecurity.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "不关联"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "撤防"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "全部布放"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "室外布放")});
            this.rgSecurity.Size = new System.Drawing.Size(338, 138);
            this.rgSecurity.TabIndex = 3;
            // 
            // lblAfgl
            // 
            this.lblAfgl.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblAfgl.Appearance.Options.UseFont = true;
            this.lblAfgl.Location = new System.Drawing.Point(10, 10);
            this.lblAfgl.Name = "lblAfgl";
            this.lblAfgl.Size = new System.Drawing.Size(64, 19);
            this.lblAfgl.TabIndex = 2;
            this.lblAfgl.Text = "安防关联";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.viewCommandSetting);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 165);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(993, 275);
            this.xtraScrollableControl1.TabIndex = 4;
            // 
            // MenuSecurityControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "MenuSecurityControl";
            this.Size = new System.Drawing.Size(993, 440);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgSecurity.Properties)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ViewCommandSetting viewCommandSetting;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.RadioGroup rgSecurity;
        private DevExpress.XtraEditors.LabelControl lblAfgl;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
    }
}
