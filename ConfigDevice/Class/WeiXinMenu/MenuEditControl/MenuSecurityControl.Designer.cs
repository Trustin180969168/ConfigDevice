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
            this.lblAfgl = new DevExpress.XtraEditors.LabelControl();
            this.rgSecurity = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.rgSecurity.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAfgl
            // 
            this.lblAfgl.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblAfgl.Appearance.Options.UseFont = true;
            this.lblAfgl.Location = new System.Drawing.Point(45, 12);
            this.lblAfgl.Name = "lblAfgl";
            this.lblAfgl.Size = new System.Drawing.Size(64, 19);
            this.lblAfgl.TabIndex = 0;
            this.lblAfgl.Text = "安防关联";
            // 
            // rgSecurity
            // 
            this.rgSecurity.Location = new System.Drawing.Point(115, 12);
            this.rgSecurity.Name = "rgSecurity";
            this.rgSecurity.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.rgSecurity.Properties.Appearance.Options.UseFont = true;
            this.rgSecurity.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "不关联"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "撤防"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "全部布放"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "室外布放")});
            this.rgSecurity.Size = new System.Drawing.Size(338, 138);
            this.rgSecurity.TabIndex = 1;
            // 
            // MenuSecurityEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rgSecurity);
            this.Controls.Add(this.lblAfgl);
            this.Name = "MenuSecurityEdit";
            this.Size = new System.Drawing.Size(632, 162);
            ((System.ComponentModel.ISupportInitialize)(this.rgSecurity.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblAfgl;
        private DevExpress.XtraEditors.RadioGroup rgSecurity;
    }
}
