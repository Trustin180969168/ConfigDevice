using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace ConfigDevice
{
    public partial class IpInputTextbox : UserControl
    {
        /// <summary>
        /// Ip��ַ
        /// </summary>
        public string IP
        {
            get
            {
                return this.ToString();
            }
            set
            {
                string ipStr = value;
                if (string.IsNullOrEmpty(ipStr))
                {
                    Ip1.Text = "";
                    Ip2.Text = "";
                    Ip3.Text = "";
                    Ip4.Text = "";
                    ip = "";
                }
                else
                {
                    try
                    {
                        IPAddress ipValue;
                        ipValue = IPAddress.Parse(ipStr);
                        string[] ips = ipStr.Split('.');
                        Ip1.Text = ips[0];
                        Ip2.Text = ips[1];
                        Ip3.Text = ips[2];
                        Ip4.Text = ips[3];
                        ip = ipStr;
                    }
                    catch
                    {
                        Ip1.Text = "";
                        Ip2.Text = "";
                        Ip3.Text = "";
                        Ip4.Text = "";
                        ip = "";
                    }
                }
            }
        }

        public string DefaultGateWay
        {
            get {
                string ip = this.IP;
                string temp = ip.Substring(0, ip.LastIndexOf(".")) + ".1";
                return temp;     
            }
        }
      
        private string ip;



        public byte[] GetBypeIP()
        {
            byte[] temp = new byte[4];
            string tempStr = ip.Substring(0, ip.Length);
            string[] sub = tempStr.Split(new string[1] { "." }, StringSplitOptions.None);
            if (sub.Length != 4) return null;
            temp[0] = ConvertTools.GetByteFrom8BitNumStr(sub[0]);
            temp[1] = ConvertTools.GetByteFrom8BitNumStr(sub[1]);
            temp[2] = ConvertTools.GetByteFrom8BitNumStr(sub[2]);
            temp[3] = ConvertTools.GetByteFrom8BitNumStr(sub[3]);

            return temp;
        }

        public IpInputTextbox()
        {
            InitializeComponent();
            this.Ip1.Index = 1;
            this.Ip2.Index = 2;
            this.Ip3.Index = 3;
            this.Ip4.Index = 4;

            this.Ip1.OnPressBackspace += new NumberTextBoxExt.PressBackspaceHandle(Ip_OnPressBackspace);
            this.Ip2.OnPressBackspace += new NumberTextBoxExt.PressBackspaceHandle(Ip_OnPressBackspace);
            this.Ip3.OnPressBackspace += new NumberTextBoxExt.PressBackspaceHandle(Ip_OnPressBackspace);
            this.Ip4.OnPressBackspace += new NumberTextBoxExt.PressBackspaceHandle(Ip_OnPressBackspace);
        }

        private void Ip_OnPressBackspace(int index)
        {
            switch (index)
            {
                case 4:
                    Ip3.Focus();
                    Ip3.SelectionStart = Ip3.Text.Length;
                    break;
                case 3:
                    Ip2.Focus();
                    Ip2.SelectionStart = Ip2.Text.Length;
                    break;
                case 2:
                    Ip1.Focus();
                    Ip1.SelectionStart = Ip1.Text.Length;
                    break;
                default:
                    break;
            }
        }

        private void Ip1_TextChanged(object sender, EventArgs e)
        {
            if (Ip1.Text.Length == 3 && Ip1.Text.Length > 0 && Ip1.SelectionLength == 0)
            {
                Ip2.Focus();
                Ip2.Select(0, Ip2.Text.Length);
            }
        }

        private void Ip2_TextChanged(object sender, EventArgs e)
        {
            if (Ip2.Text.Length == 3 && Ip2.Text.Length > 0 && Ip2.SelectionLength == 0)
            {
                Ip3.Focus();
                Ip3.Select(0, Ip3.Text.Length);
            }
        }

        private void Ip3_TextChanged(object sender, EventArgs e)
        {
            if (Ip3.Text.Length == 3 && Ip3.Text.Length > 0 && Ip3.SelectionLength == 0)
            {
                Ip4.Focus();
                Ip4.Select(0, Ip4.Text.Length);
            }
        }

        /// <summary>
        /// ToString��д
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string Ipstr = Ip1.Text + "." + Ip2.Text + "." + Ip3.Text + "." + Ip4.Text;
            try
            {
                IPAddress.Parse(Ipstr);
            }
            catch
            {
                return "IP��ַ��ʽ����ȷ";
            }
            this.ip = Ipstr;
            return this.ip;
        }

        private void Ip1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Tab && Ip1.Text.Length > 0 && Ip1.SelectionLength == 0)
            {
                Ip2.Focus();
                Ip2.Select(0, Ip2.Text.Length);
            }
        }

        private void Ip2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Tab && Ip2.Text.Length > 0 && Ip2.SelectionLength == 0)
            {
                Ip3.Focus();
                Ip3.Select(0, Ip3.Text.Length);
            }
        }

        private void Ip3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Tab && Ip3.Text.Length > 0 && Ip3.SelectionLength == 0)
            {
                Ip4.Focus();
                Ip4.Select(0, Ip4.Text.Length);
            }
        }

    }
}
