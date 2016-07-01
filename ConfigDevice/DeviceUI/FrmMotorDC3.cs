using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    /// <summary>
    /// 三路直流电机
    /// </summary>
    public partial class FrmMotorDC3 : FrmDevice
    {
        private Road3Window road3Window;
        public FrmMotorDC3(Device _device)
            : base(_device)
        {          
            InitializeComponent();
            this.Device.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            this.Device.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件
            viewBaseSetting.DeviceEdit = this.Device;

            road3Window = this.Device as Road3Window;
        }

        public FrmMotorDC3()
        {
            InitializeComponent();
        }

        private void FrmMotorDC3_Load(object sender, EventArgs e)
        {
            viewBaseSetting.DeviceEdit.SearchVer();//---获取版本号-----   
            InitSelectDevice();//----初始化选择设备列表---
            loadData();//---加载数据----
        }

        /// <summary>
        /// 加载界面数据
        /// </summary>
        private void loadData()
        {
            road3Window.SearchVer();          //---获取版本号-----    
            road3Window.Motor.ReadValveParameter();//---读取参数-----

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
                    if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Motor.CLASS_NAME)
                    {

                    }

                }
            }
            catch { }
        }


        /// <summary>
        /// 选择事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Device DeviceSelect = new BaseDevice(SelectDeviceList[CbxSelectDevice.SelectedIndex]);
            if (Device.MAC == DeviceSelect.MAC) return;

            DeviceSelect.OnCallbackUI_Action += this.callbackUI;
            DeviceSelect.OnCallbackUI_Action += viewBaseSetting.CallBackUI;
            viewBaseSetting.DeviceEdit = DeviceSelect;
            Device = DeviceSelect;
            this.Text = Device.Name;
            Device.SearchVer();

        }


    }
}
