using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmBaseDevice : FrmDevice
    {
        public FrmBaseDevice(DeviceData _device)
            : base(_device)
        {          
            InitializeComponent();
            this.Device.CallbackUI = new CallbackUIAction(this.callbackUI);
            frmSetting.DeviceEdit = this.Device;
        }

        public FrmBaseDevice()
        {
            InitializeComponent();
        }

        private void FrmBaseDevice_Load(object sender, EventArgs e)
        { 
            this.Device.SearchVer();//---获取版本号-----       
        }

        /// <summary>
        /// 回调
        /// </summary>
        private void callbackUI(object[] values)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new CallbackUIAction(callbackUI),new object[]{values});                  
                }
                else
                {
                    frmSetting.CallBackUI(null);
                    InitSelectDevice();
                }
            }
            catch { }
        }

        /// <summary>
        /// 选择事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceData DeviceSelect = SelectDeviceList[CbxSelectDevice.SelectedIndex];
            if (Device.MAC == DeviceSelect.MAC) return;

            this.Device = DeviceSelect;
            this.Device.CallbackUI = new CallbackUIAction(this.callbackUI);
            frmSetting.DeviceEdit = this.Device;
        }


    }
}
