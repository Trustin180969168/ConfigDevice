namespace ConfigDevice
{
    partial class FrmNetworkIPEdit
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
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.edtNetworkID = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btSaveInfo = new System.Windows.Forms.ToolStripButton();
            this.edtDNS2 = new ConfigDevice.IpInputTextbox();
            this.edtDNS1 = new ConfigDevice.IpInputTextbox();
            this.edtNetworkIP = new ConfigDevice.IpInputTextbox();
            this.edtMask = new ConfigDevice.IpInputTextbox();
            this.edtGateway = new ConfigDevice.IpInputTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.edtNetworkID.Properties)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label8.Location = new System.Drawing.Point(7, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 19);
            this.label8.TabIndex = 34;
            this.label8.Text = "首选DNS";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label9.Location = new System.Drawing.Point(333, 134);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 19);
            this.label9.TabIndex = 35;
            this.label9.Text = "备用DNS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label3.Location = new System.Drawing.Point(39, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 19);
            this.label3.TabIndex = 30;
            this.label3.Text = "网关";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label2.Location = new System.Drawing.Point(25, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 19);
            this.label2.TabIndex = 31;
            this.label2.Text = "IP地址";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label4.Location = new System.Drawing.Point(332, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 19);
            this.label4.TabIndex = 28;
            this.label4.Text = "子网掩码";
            // 
            // edtNetworkID
            // 
            this.edtNetworkID.Location = new System.Drawing.Point(406, 69);
            this.edtNetworkID.Name = "edtNetworkID";
            this.edtNetworkID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtNetworkID.Properties.Appearance.Options.UseFont = true;
            this.edtNetworkID.Size = new System.Drawing.Size(240, 26);
            this.edtNetworkID.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label1.Location = new System.Drawing.Point(363, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 19);
            this.label1.TabIndex = 29;
            this.label1.Text = "网段";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSaveInfo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(677, 31);
            this.toolStrip1.TabIndex = 36;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btSaveInfo
            // 
            this.btSaveInfo.Image = global::ConfigDevice.Properties.Resources.save;
            this.btSaveInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveInfo.Name = "btSaveInfo";
            this.btSaveInfo.Size = new System.Drawing.Size(92, 28);
            this.btSaveInfo.Text = "保存信息";
            this.btSaveInfo.Click += new System.EventHandler(this.btSaveInfo_Click);
            // 
            // edtDNS2
            // 
            this.edtDNS2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtDNS2.IP = "IP地址格式不正确";
            this.edtDNS2.Location = new System.Drawing.Point(406, 132);
            this.edtDNS2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtDNS2.Name = "edtDNS2";
            this.edtDNS2.Size = new System.Drawing.Size(240, 26);
            this.edtDNS2.TabIndex = 32;
            // 
            // edtDNS1
            // 
            this.edtDNS1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtDNS1.IP = "IP地址格式不正确";
            this.edtDNS1.Location = new System.Drawing.Point(82, 132);
            this.edtDNS1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtDNS1.Name = "edtDNS1";
            this.edtDNS1.Size = new System.Drawing.Size(240, 26);
            this.edtDNS1.TabIndex = 33;
            // 
            // edtNetworkIP
            // 
            this.edtNetworkIP.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtNetworkIP.IP = "IP地址格式不正确";
            this.edtNetworkIP.Location = new System.Drawing.Point(82, 69);
            this.edtNetworkIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtNetworkIP.Name = "edtNetworkIP";
            this.edtNetworkIP.Size = new System.Drawing.Size(240, 26);
            this.edtNetworkIP.TabIndex = 25;
            // 
            // edtMask
            // 
            this.edtMask.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtMask.IP = "IP地址格式不正确";
            this.edtMask.Location = new System.Drawing.Point(406, 101);
            this.edtMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtMask.Name = "edtMask";
            this.edtMask.Size = new System.Drawing.Size(240, 26);
            this.edtMask.TabIndex = 27;
            // 
            // edtGateway
            // 
            this.edtGateway.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtGateway.IP = "IP地址格式不正确";
            this.edtGateway.Location = new System.Drawing.Point(82, 100);
            this.edtGateway.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtGateway.Name = "edtGateway";
            this.edtGateway.Size = new System.Drawing.Size(240, 26);
            this.edtGateway.TabIndex = 26;
            // 
            // FrmNetworkIPEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 283);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.edtDNS2);
            this.Controls.Add(this.edtDNS1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.edtNetworkID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtNetworkIP);
            this.Controls.Add(this.edtMask);
            this.Controls.Add(this.edtGateway);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmNetworkIPEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改网络IP";
            this.Load += new System.EventHandler(this.FrmNetworkIPEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.edtNetworkID.Properties)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private IpInputTextbox edtDNS2;
        private IpInputTextbox edtDNS1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit edtNetworkID;
        private System.Windows.Forms.Label label1;
        private IpInputTextbox edtNetworkIP;
        private IpInputTextbox edtMask;
        private IpInputTextbox edtGateway;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btSaveInfo;
    }
}