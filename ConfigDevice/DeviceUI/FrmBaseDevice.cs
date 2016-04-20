using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmFourInput : FrmDevice
    {
        public FrmFourInput(DeviceData _device):base(_device)
        {
          
            InitializeComponent();
            frmSetting.DeviceEdit = _device;
        }

        public FrmFourInput():base()
        {
            InitializeComponent();
        }

        private void FrmBaseDevice_Load(object sender, EventArgs e)
        {     
            this.Device.CallbackUI = new CallbackUIAction(this.callbackUI);
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
                }
            }
            catch { }
        }


    }
}
