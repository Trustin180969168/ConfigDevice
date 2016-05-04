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

        /// <summary>
        /// 双击选择设备
        /// </summary>
        private void gvDevices_DoubleClick(object sender, EventArgs e)
        {
            if (gvDevices.FocusedRowHandle == -1) return;

            if (SysConfig.ListNetworks.ContainsKey(ChooseDevice.NetworkIP) &&
                SysConfig.ListNetworks[ChooseDevice.NetworkIP].State == NetworkConfig.STATE_CONNECTED)
            {
                DataRow dr = gvDevices.GetDataRow(gvDevices.FocusedRowHandle);
                byte kindId = BitConverter.GetBytes(Convert.ToInt16(dr[DeviceConfig.DC_KIND_ID]))[0];
                ChooseDevice = SysCtrl.CreateDevice(kindId).CreateDevice(dr);//---创建相应的设备对象-----
                this.DialogResult = DialogResult.Yes;
            }
            else { CommonTools.MessageShow("网络链接已断开,请重新链接!", 2, ""); return; }
        }

        /// <summary>
        /// 加载
        /// </summary>
        private void SelectDevice_Load(object sender, EventArgs e)
        {
            gcDevices.DataSource = SysConfig.DtDevice;
        }







    }
}
