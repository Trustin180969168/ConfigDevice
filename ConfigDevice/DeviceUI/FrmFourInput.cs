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
        public FrmFourInput(DeviceData _device)
            : base(_device)
        {
          
            InitializeComponent();
            frmSetting.DeviceEdit = _device;
        }   

        private void FrmFourInput_Load(object sender, EventArgs e)
        {     
            this.Device.CallbackUI += this.callbackUI;
            this.Device.CallbackUI += frmSetting.CallBackUI;
            this.Device.SearchVer();//---获取版本号-----
            xtraTabControl1.SelectedTabPageIndex = 0;
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
                    return;
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
