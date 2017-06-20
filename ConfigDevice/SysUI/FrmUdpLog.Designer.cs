namespace ConfigDevice
{
    partial class FrmUdpLog
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
            this.mdtUpdLog = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.mdtUpdLog.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mdtUpdLog
            // 
            this.mdtUpdLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdtUpdLog.Location = new System.Drawing.Point(0, 0);
            this.mdtUpdLog.Name = "mdtUpdLog";
            this.mdtUpdLog.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.mdtUpdLog.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.mdtUpdLog.Properties.Appearance.Options.UseBackColor = true;
            this.mdtUpdLog.Properties.Appearance.Options.UseForeColor = true;
            this.mdtUpdLog.Size = new System.Drawing.Size(784, 561);
            this.mdtUpdLog.TabIndex = 0;
            // 
            // FrmUdpLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.mdtUpdLog);
            this.Name = "FrmUdpLog";
            this.Text = "通讯日志";
            this.Load += new System.EventHandler(this.FrmUdpLog_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmUdpLog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.mdtUpdLog.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit mdtUpdLog;
    }
}