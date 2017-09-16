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
            this.tbxPw = new System.Windows.Forms.TextBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lblNetwork = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbxPw
            // 
            this.tbxPw.Location = new System.Drawing.Point(113, 131);
            this.tbxPw.Name = "tbxPw";
            this.tbxPw.PasswordChar = '*';
            this.tbxPw.Size = new System.Drawing.Size(147, 21);
            this.tbxPw.TabIndex = 0;
            this.tbxPw.Text = "1234";
            this.tbxPw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxPw_KeyUp);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(83, 179);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "确认";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(222, 179);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // lblNetwork
            // 
            this.lblNetwork.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNetwork.Font = new System.Drawing.Font("宋体", 18F);
            this.lblNetwork.ForeColor = System.Drawing.Color.Blue;
            this.lblNetwork.Location = new System.Drawing.Point(0, 0);
            this.lblNetwork.Name = "lblNetwork";
            this.lblNetwork.Size = new System.Drawing.Size(394, 95);
            this.lblNetwork.TabIndex = 2;
            this.lblNetwork.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmNetworkPW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 275);
            this.Controls.Add(this.lblNetwork);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.tbxPw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmNetworkPW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请输入密码";
            this.Load += new System.EventHandler(this.FrmNetworkPW_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxPw;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label lblNetwork;
    }
}