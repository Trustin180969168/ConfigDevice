using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ConfigDevice
{

    public partial class FrmNetworkPW : Form
    {
        /// 注意密码格式如下:
        /// 21 43 FF FF	-管理员密码:1234
        /// FF FF FF FF	-用户密码:空

        public string PassWord = "";
        public string NetworkName = "";
        public byte[] BytePassWord;
        public FrmNetworkPW()
        {
            InitializeComponent();
        }

        private void FrmNetworkPW_Load(object sender, EventArgs e)
        {
            tbxPw.Text = "1234";
            lblNetwork.Text ="  "+ NetworkName+"  ";
            BytePassWord = new byte[4];
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (IsFourInt(tbxPw.Text))
            {  
                PassWord = tbxPw.Text;
                //---密码:1234 => 0x21,0x43,0xFF,0xFF----
                char[] pwChar= PassWord.ToCharArray();
                BytePassWord = ConvertTools.StrToToHexByte(pwChar[1].ToString() + pwChar[0].ToString() +
                    pwChar[3].ToString() + pwChar[2].ToString());    
            
            }
            else
            { CommonTools.MessageShow("格式错误!", 2, ""); return; }
            this.DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


        private bool IsFourInt(string value)
        {
            return Regex.IsMatch(value, @"^\d{4}$");
        }

        private void tbxPw_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btOK_Click(sender, e);
        }
    }
}
