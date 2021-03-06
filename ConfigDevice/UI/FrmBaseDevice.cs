﻿using System;
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
        public FrmBaseDevice(DeviceData _device):base(_device)
        {
          
            InitializeComponent();
            frmSetting.DeviceEdit = _device;
        }

        public FrmBaseDevice():base()
        {
            InitializeComponent();
        }

        private void FrmBaseDevice_Load(object sender, EventArgs e)
        {     
            this.Device.CallbackUI = new CallBackUIAction(this.callbackUI);
            this.Device.SearchVer();//---获取版本号-----
        }

        /// <summary>
        /// 回调
        /// </summary>
        private void callbackUI()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new CallBackUIAction(callbackUI));
                }
                else
                {
                    frmSetting.CallBackUI();
                }
            }
            catch { }
        }


    }
}
