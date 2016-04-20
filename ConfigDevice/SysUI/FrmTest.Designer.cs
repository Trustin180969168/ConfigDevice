namespace ConfigDevice
{
    partial class FrmTest
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
            this.edtANSIStr = new System.Windows.Forms.TextBox();
            this.edtANSIResult = new System.Windows.Forms.TextBox();
            this.btANSI = new System.Windows.Forms.Button();
            this.bt2312 = new System.Windows.Forms.Button();
            this.edtSource2312 = new System.Windows.Forms.TextBox();
            this.btEx2312 = new System.Windows.Forms.Button();
            this.edtGB2312 = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
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
            this.richTextBox1.Size = new System.Drawing.Size(278, 96);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // edtANSIStr
            // 
            this.edtANSIStr.Location = new System.Drawing.Point(120, 136);
            this.edtANSIStr.Name = "edtANSIStr";
            this.edtANSIStr.Size = new System.Drawing.Size(319, 21);
            this.edtANSIStr.TabIndex = 2;
            this.edtANSIStr.Text = "B5 DA D2 BB D0 D0 00 00 00 00 00 00";
            // 
            // edtANSIResult
            // 
            this.edtANSIResult.Location = new System.Drawing.Point(120, 163);
            this.edtANSIResult.Name = "edtANSIResult";
            this.edtANSIResult.Size = new System.Drawing.Size(319, 21);
            this.edtANSIResult.TabIndex = 3;
            this.edtANSIResult.Text = "D0 C2 57 49 46 49 B9 A6 B7 C5 31 79 87 C6 20";
            // 
            // btANSI
            // 
            this.btANSI.Location = new System.Drawing.Point(12, 134);
            this.btANSI.Name = "btANSI";
            this.btANSI.Size = new System.Drawing.Size(75, 23);
            this.btANSI.TabIndex = 4;
            this.btANSI.Text = "ANSI";
            this.btANSI.UseVisualStyleBackColor = true;
            this.btANSI.Click += new System.EventHandler(this.btANSI_Click);
            // 
            // bt2312
            // 
            this.bt2312.Location = new System.Drawing.Point(12, 188);
            this.bt2312.Name = "bt2312";
            this.bt2312.Size = new System.Drawing.Size(75, 23);
            this.bt2312.TabIndex = 5;
            this.bt2312.Text = "解码GB2312";
            this.bt2312.UseVisualStyleBackColor = true;
            this.bt2312.Click += new System.EventHandler(this.bt2312_Click);
            // 
            // edtSource2312
            // 
            this.edtSource2312.Location = new System.Drawing.Point(120, 217);
            this.edtSource2312.Name = "edtSource2312";
            this.edtSource2312.Size = new System.Drawing.Size(166, 21);
            this.edtSource2312.TabIndex = 3;
            // 
            // btEx2312
            // 
            this.btEx2312.Location = new System.Drawing.Point(12, 217);
            this.btEx2312.Name = "btEx2312";
            this.btEx2312.Size = new System.Drawing.Size(75, 23);
            this.btEx2312.TabIndex = 5;
            this.btEx2312.Text = "加码GB2312";
            this.btEx2312.UseVisualStyleBackColor = true;
            this.btEx2312.Click += new System.EventHandler(this.btEx2312_Click);
            // 
            // edtGB2312
            // 
            this.edtGB2312.Location = new System.Drawing.Point(120, 190);
            this.edtGB2312.Name = "edtGB2312";
            this.edtGB2312.Size = new System.Drawing.Size(319, 21);
            this.edtGB2312.TabIndex = 3;
            this.edtGB2312.Text = "D0 C2 57 49 46 49 B9 A6 B7 C5 31 79 87 C6 20";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(404, 34);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(221, 96);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 425);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btEx2312);
            this.Controls.Add(this.bt2312);
            this.Controls.Add(this.btANSI);
            this.Controls.Add(this.edtSource2312);
            this.Controls.Add(this.edtGB2312);
            this.Controls.Add(this.edtANSIResult);
            this.Controls.Add(this.edtANSIStr);
            this.Controls.Add(this.button1);
            this.Name = "FrmTest";
            this.Text = "FrmTestProtocol";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox edtANSIStr;
        private System.Windows.Forms.TextBox edtANSIResult;
        private System.Windows.Forms.Button btANSI;
        private System.Windows.Forms.Button bt2312;
        private System.Windows.Forms.TextBox edtSource2312;
        private System.Windows.Forms.Button btEx2312;
        private System.Windows.Forms.TextBox edtGB2312;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}