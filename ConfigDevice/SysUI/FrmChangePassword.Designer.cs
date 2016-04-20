namespace ConfigDevice
{
    partial class FrmChangePassword
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btClose = new System.Windows.Forms.ToolStripButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.edtOldPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.edtNewPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.edtConfirmPassword = new DevExpress.XtraEditors.TextEdit();
            this.cbxKind = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtOldPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNewPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtConfirmPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxKind.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSave,
            this.btClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(394, 31);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btSave
            // 
            this.btSave.Image = global::ConfigDevice.Properties.Resources.save;
            this.btSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(68, 28);
            this.btSave.Text = "保存 ";
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Image = global::ConfigDevice.Properties.Resources.Exit;
            this.btClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(68, 28);
            this.btClose.Text = "退出 ";
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(93, 123);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 19);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "原 密 码";
            // 
            // edtOldPassword
            // 
            this.edtOldPassword.Location = new System.Drawing.Point(157, 120);
            this.edtOldPassword.Name = "edtOldPassword";
            this.edtOldPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtOldPassword.Properties.Appearance.Options.UseFont = true;
            this.edtOldPassword.Properties.PasswordChar = '*';
            this.edtOldPassword.Size = new System.Drawing.Size(100, 26);
            this.edtOldPassword.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(93, 155);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(58, 19);
            this.labelControl4.TabIndex = 18;
            this.labelControl4.Text = "新 密 码";
            // 
            // edtNewPassword
            // 
            this.edtNewPassword.Location = new System.Drawing.Point(157, 152);
            this.edtNewPassword.Name = "edtNewPassword";
            this.edtNewPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtNewPassword.Properties.Appearance.Options.UseFont = true;
            this.edtNewPassword.Properties.PasswordChar = '*';
            this.edtNewPassword.Size = new System.Drawing.Size(100, 26);
            this.edtNewPassword.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(87, 187);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(64, 19);
            this.labelControl5.TabIndex = 18;
            this.labelControl5.Text = "确认密码";
            // 
            // edtConfirmPassword
            // 
            this.edtConfirmPassword.Location = new System.Drawing.Point(157, 184);
            this.edtConfirmPassword.Name = "edtConfirmPassword";
            this.edtConfirmPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtConfirmPassword.Properties.Appearance.Options.UseFont = true;
            this.edtConfirmPassword.Properties.PasswordChar = '*';
            this.edtConfirmPassword.Size = new System.Drawing.Size(100, 26);
            this.edtConfirmPassword.TabIndex = 2;
            // 
            // cbxKind
            // 
            this.cbxKind.EditValue = "管理员密码";
            this.cbxKind.Location = new System.Drawing.Point(112, 48);
            this.cbxKind.Name = "cbxKind";
            this.cbxKind.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxKind.Properties.Appearance.Options.UseFont = true;
            this.cbxKind.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxKind.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cbxKind.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxKind.Properties.Items.AddRange(new object[] {
            "管理员密码",
            "用户密码"});
            this.cbxKind.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxKind.Size = new System.Drawing.Size(201, 26);
            this.cbxKind.TabIndex = 20;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(42, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 19);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "密码类型";
            // 
            // FrmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 275);
            this.Controls.Add(this.cbxKind);
            this.Controls.Add(this.edtConfirmPassword);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.edtNewPassword);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.edtOldPassword);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改密码";
            this.Load += new System.EventHandler(this.FrmChangePassword_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtOldPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNewPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtConfirmPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxKind.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripButton btClose;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit edtOldPassword;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit edtNewPassword;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit edtConfirmPassword;
        private DevExpress.XtraEditors.ComboBoxEdit cbxKind;
        private DevExpress.XtraEditors.LabelControl labelControl2;

    }
}