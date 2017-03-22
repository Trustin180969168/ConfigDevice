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
        public FrmBaseDevice(Device _device)
            : base(_device)
        {          
            InitializeComponent();
            this.DeviceEdit.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            this.DeviceEdit.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件
            viewBaseSetting.DeviceEdit = this.DeviceEdit;
        }

        public FrmBaseDevice()
        {
            InitializeComponent();
        }

        private void FrmBaseDevice_Load(object sender, EventArgs e)
        {
            viewBaseSetting.DeviceEdit.SearchVer();//---获取版本号-----   
            InitSelectDevice();
        }

        /// <summary>
        /// 回调
        /// </summary>
        private void callbackUI(CallbackParameter callbackParameter)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new CallbackUIAction(callbackUI), callbackParameter);                  
                }
                else
                {


                }
            }
            catch { }
        }

        /// <summary>
        /// 选择事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Device DeviceSelect = new BaseDevice(SelectDeviceList[CbxSelectDevice.SelectedIndex]);
            //if (DeviceEdit.MAC == DeviceSelect.MAC) return;
            ////this.Close();
            ////FrmDevice frm = SysCtrl.GetFactory(DeviceSelect.ByteKindID).CreateDevice(DeviceSelect);
            ////frm.Text = DeviceSelect.Name;
            ////frm.Show();

            //DeviceSelect.OnCallbackUI_Action += this.callbackUI;
            //DeviceSelect.OnCallbackUI_Action += viewBaseSetting.CallBackUI;
            //viewBaseSetting.DeviceEdit = DeviceSelect;
            //DeviceEdit = DeviceSelect;
            //this.Text = DeviceEdit.Name;
            //DeviceEdit.SearchVer();


        }


    }
}
