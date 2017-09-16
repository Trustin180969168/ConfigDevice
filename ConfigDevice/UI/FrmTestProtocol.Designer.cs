namespace ConfigDevice
{
    partial class FrmTestProtocol
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
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.edtANSIStr = new System.Windows.Forms.TextBox();
            this.edtANSIResult = new System.Windows.Forms.TextBox();
            this.btANSI = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "测试CRC校验程序";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(120, 34);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(505, 96);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(120, 150);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(505, 96);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 191);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 55);
            this.button2.TabIndex = 0;
            this.button2.Text = "测试线程";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // edtANSIStr
            // 
            this.edtANSIStr.Location = new System.Drawing.Point(120, 271);
            this.edtANSIStr.Name = "edtANSIStr";
            this.edtANSIStr.Size = new System.Drawing.Size(319, 21);
            this.edtANSIStr.TabIndex = 2;
            this.edtANSIStr.Text = "B5 DA D2 BB D0 D0 00 00 00 00 00 00";
            // 
            // edtANSIResult
            // 
            this.edtANSIResult.Location = new System.Drawing.Point(120, 298);
            this.edtANSIResult.Name = "edtANSIResult";
            this.edtANSIResult.Size = new System.Drawing.Size(319, 21);
            this.edtANSIResult.TabIndex = 3;
            // 
            // btANSI
            // 
            this.btANSI.Location = new System.Drawing.Point(12, 298);
            this.btANSI.Name = "btANSI";
            this.btANSI.Size = new System.Drawing.Size(75, 23);
            this.btANSI.TabIndex = 4;
            this.btANSI.Text = "ANSI";
            this.btANSI.UseVisualStyleBackColor = true;
            this.btANSI.Click += new System.EventHandler(this.btANSI_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(50, 351);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FrmTestProtocol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 425);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btANSI);
            this.Controls.Add(this.edtANSIResult);
            this.Controls.Add(this.edtANSIStr);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FrmTestProtocol";
            this.Text = "FrmTestProtocol";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox edtANSIStr;
        private System.Windows.Forms.TextBox edtANSIResult;
        private System.Windows.Forms.Button btANSI;
        private System.Windows.Forms.Button button3;
    }
}