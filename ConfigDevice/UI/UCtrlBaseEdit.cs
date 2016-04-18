using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class UCtrlBaseEdit : UserControl
    {
        public DeviceData DeviceEdit;
        public UCtrlBaseEdit()
        {
            InitializeComponent();
        }

        public void CallBackUI(object[] values)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new CallBackUIAction(this.CallBackUI),new object[]{values}); 
                else
                {
                    edtHardwareVer.Text = DeviceEdit.HardwareVer;
                    edtSoftwareVer.Text = DeviceEdit.SoftwareVer;

                    edtName.Text = DeviceEdit.Name;
                    edtDeviceID.Text = DeviceEdit.DeviceID;
                    edtNetworkID.Text = DeviceEdit.NetworkID;
                    cbxDeviceKind.Text = DeviceEdit.KindName;

                    getPosition();
                    cbxPosition.Text = DeviceEdit.AddressName;
                }
            }
            catch { }
        }

        /// <summary>
        /// 获取位置
        /// </summary>
        private void getPosition()
        {
            if (cbxPosition.Properties.Items.Count == 0)
            {
                NetworkData network = SysConfig.ListNetworks[DeviceEdit.NetworkID];
                foreach (Position p in network.ListPosition)
                    if (p.Name.Trim() != "")
                        cbxPosition.Properties.Items.Add(p.Name);
      ;
            }
        }

        private void btSave_MouseHover(object sender, EventArgs e)
        {
            btSave.ShowDropDown();
        }

        /// <summary>
        /// 保存设备ID
        /// </summary>
        private void btSaveID_Click(object sender, EventArgs e)
        {
            DeviceEdit.SaveDeviceID(edtDeviceID.Text);
           
        }

        /// <summary>
        /// 保存设备名称
        /// </summary>
        private void btSaveNamePosition_Click(object sender, EventArgs e)
        {
            int pos = cbxPosition.SelectedIndex;
            byte[] bytePos = ConvertTools.GetByteFrom16BitInt(pos);
            string newPos = cbxPosition.Text;
            DeviceEdit.SaveDeviceName(edtName.Text, bytePos, newPos);
         
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void btRefresh_Click(object sender, EventArgs e)
        {
            DeviceEdit.RefreshData();   
        }


        private void edtName_Leave(object sender, EventArgs e)
        {
            string newName = edtName.Text;
            byte[] byteName = Encoding.GetEncoding("GB2312").GetBytes(newName);
            if (byteName.Length > 30)
                CommonTools.MessageShow("设备名称不能大于30字节!", 3, "");
        }

        /// <summary>
        /// 关灯
        /// </summary>
        private void btCloseLight_Click(object sender, EventArgs e)
        {
            DeviceEdit.CloseLight();
        }

        /// <summary>
        /// 开灯
        /// </summary>
        private void btOpenLight_Click(object sender, EventArgs e)
        {
            DeviceEdit.OpenLight();
        }

        /// <summary>
        /// 开启设备发现
        /// </summary>
        private void btFindOn_Click(object sender, EventArgs e)
        {
            DeviceEdit.OpenDiscover();
        }

        /// <summary>
        /// 关闭设备发现
        /// </summary>
        private void btFindOff_Click(object sender, EventArgs e)
        {
            DeviceEdit.CloseDiscover();
        }




    }
}
