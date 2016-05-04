using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class SelectDevice : Form
    {
        public DeviceData ChooseDevice;//设备选择

        public SelectDevice()
        {
            InitializeComponent();
        }

        private void gvDevices_DoubleClick(object sender, EventArgs e)
        {
            if (gvDevices.FocusedRowHandle == -1) return;
            DataRow dr = gvDevices.GetDataRow(gvDevices.FocusedRowHandle);
            ChooseDevice = new BaseDevice(dr);
            if (SysConfig.ListNetworks.ContainsKey(ChooseDevice.NetworkIP) &&
                SysConfig.ListNetworks[ChooseDevice.NetworkIP].State == NetworkConfig.STATE_CONNECTED)
            {
                this.DialogResult = DialogResult.Yes;
            }
            else { CommonTools.MessageShow("网络链接已断开,请重新链接!", 2, ""); return; }
        }

        private void SelectDevice_Load(object sender, EventArgs e)
        {
            gcDevices.DataSource = SysConfig.DtDevice;
        }


    }
}
