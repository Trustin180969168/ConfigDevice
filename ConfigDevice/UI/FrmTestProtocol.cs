﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ConfigDevice
{
    public partial class FrmTestProtocol : Form
    {    
       
        public FrmTestProtocol()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] data = ConvertTools.StrToToHexByte(richTextBox1.Text);
            byte[] crc = CRC32.GetCheckValue(data);
            richTextBox2.Text = ConvertTools.ByteToHexStr(crc);
      
        }

        private void btANSI_Click(object sender, EventArgs e)
        {
            byte[] byteName = ConvertTools.StrToToHexByte(edtANSIStr.Text);
            edtANSIResult.Text = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0');
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<String> list = new List<string>();
            list.Add("张三");
            list.Add("李四");
            list.Add("王五");
            list.Add("田六");
            list.Add("赵七");

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("for循环：" + i.ToString() + "=" + list[i]);
            }  
  
        }  
           





    }






}
