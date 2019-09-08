using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmNetworkIPEdit : Form
    {

        public Network NetworkEdit = null;
        public String PassWord = "";

        public FrmNetworkIPEdit()
        {
            InitializeComponent(); 
        }

        private void FrmNetworkIPEdit_Load(object sender, EventArgs e)
        {　
            edtNetworkID.Text = NetworkEdit.NetworkID;
      
            edtNetworkIP.IP = NetworkEdit.NetworkIP;
            edtMask.IP = SysConfig.SubnetMask.ToString();//----目前固定 255.255.255.0----
            edtGateway.IP = edtNetworkIP.DefaultGateWay;//----目前固定xxx.xxx.xxx.1-----
        }

        private void btSaveInfo_Click(object sender, EventArgs e)
        {
            edtNetworkIP.IP = edtNetworkIP.IP;
            NetworkEdit.ManagerPassword = PassWord;
            NetworkEdit.SaveNetworkParameter(edtNetworkIP.GetBypeIP(), edtGateway.GetBypeIP(), edtMask.GetBypeIP(),
                ConvertTools.GetByteFrom8BitNumStr(edtNetworkID.Text), edtDNS1.GetBypeIP(), edtDNS2.GetBypeIP());
        }


     　



    }
}
