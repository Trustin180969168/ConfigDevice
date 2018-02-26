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
            edtGateway.Refresh();
            //byte[] networkByteIP = edtNetworkIP.GetByteIP();
            //byte[] gatewayByteIP = edtGateway.GetByteIP();
            //byte[] maskByteIP = edtMask.GetByteIP();
            //byte[] dns1ByteIP = edtDNS1.GetByteIP();
            //byte[] dns2ByteIP = edtDNS2.GetByteIP(); 
            NetworkEdit.SaveNetworkParameter(edtNetworkIP.GetByteIP(), edtGateway.GetByteIP(), edtMask.GetByteIP(),
                ConvertTools.GetByteFrom8BitNumStr(edtNetworkID.Text), edtDNS1.GetByteIP(), edtDNS2.GetByteIP());
        
        
        }


     　



    }
}
