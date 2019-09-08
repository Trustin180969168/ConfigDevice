namespace ConfigDevice
{
    partial class FrmNetworkPW
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblNetwork = new System.Windows.Forms.Label();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.btCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btOK = new DevExpress.XtraEditors.SimpleButton();
            this.tbxPw = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbxPw.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblNetwork);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(394, 111);
            this.panelControl1.TabIndex = 4;
            // 
            // lblNetwork
            // 
            this.lblNetwork.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblNetwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNetwork.Font = new System.Drawing.Font("宋体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNetwork.ForeColor = System.Drawing.Color.Blue;
            this.lblNetwork.Location = new System.Drawing.Point(76, 0);
            this.lblNetwork.Name = "lblNetwork";
            this.lblNetwork.Size = new System.Drawing.Size(318, 111);
            this.lblNetwork.TabIndex = 5;
            this.lblNetwork.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEdit1.EditValue = global::ConfigDevice.Properties.Resources.logo_connect;
            this.pictureEdit1.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.PictureAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(76, 111);
            this.pictureEdit1.TabIndex = 4;
            // 
            // btCancel
            // 
            this.btCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btCancel.Appearance.Options.UseFont = true;
            this.btCancel.Image = global::ConfigDevice.Properties.Resources.del12;
            this.btCancel.Location = new System.Drawing.Point(211, 178);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(70, 30);
            this.btCancel.TabIndex = 6;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btOK.Appearance.Options.UseFont = true;
            this.btOK.Image = global::ConfigDevice.Properties.Resources.confirm;
            this.btOK.Location = new System.Drawing.Point(111, 178);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(70, 30);
            this.btOK.TabIndex = 5;
            this.btOK.Text = "确认 ";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // tbxPw
            // 
            this.tbxPw.EditValue = "1234";
            this.tbxPw.Location = new System.Drawing.Point(111, 117);
            this.tbxPw.Name = "tbxPw";
            this.tbxPw.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxPw.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tbxPw.Properties.Appearance.Options.UseBackColor = true;
            this.tbxPw.Properties.Appearance.Options.UseFont = true;
            this.tbxPw.Properties.PasswordChar = '*';
            this.tbxPw.Size = new System.Drawing.Size(170, 26);
            this.tbxPw.TabIndex = 0;
            this.tbxPw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxPw_KeyUp);
            // 
            // FrmNetworkPW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(394, 278);
            this.Controls.Add(this.tbxPw);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmNetworkPW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请输入密码";
            this.Load += new System.EventHandler(this.FrmNetworkPW_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbxPw.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lblNetwork;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton btOK;
        private DevExpress.XtraEditors.SimpleButton btCancel;
        private DevExpress.XtraEditors.TextEdit tbxPw;
    }
}