using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmDevice : Form
    {
        public DeviceData Device;
        public ToolStripComboBox CbxSelectDevice { get { return (cbxSelectDevice as ToolStripComboBox); } }
        public Dictionary<int, DeviceData> SelectDeviceList = new Dictionary<int, DeviceData>();//---列表---

        public FrmDevice(DeviceData _device)
        {
            this.Device = _device;
            InitializeComponent();
      
        }

        public FrmDevice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化列表
        /// </summary>
        public void InitSelectDevice()
        {
            if (SelectDeviceList.Count > 0) return;
            string temp = DeviceConfig.DC_KIND_ID + "='" + Device.KindID + "'";
            DataRow[] rows = SysConfig.DtDevice.Select(temp);
            int i = 0;
            foreach (DataRow dr in rows)
            {
                cbxSelectDevice.Items.Add(dr[DeviceConfig.DC_NAME].ToString());
                SelectDeviceList.Add(i++, new DeviceData(dr));
            }
            cbxSelectDevice.Text = Device.Name;
        }

        /// <summary>
        /// 供子类覆盖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            



        }

        private void cbxSelectDevice_Click(object sender, EventArgs e)
        {
            //if(SelectDeviceList.Count ==0)
            //InitSelectDevice();
        }

 



    }
}
