using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace ConfigDevice
{
    public partial class FrmFourInput : FrmDevice
    {
        public FrmFourInput(DeviceData _device)
            : base(_device)
        {          
            InitializeComponent();
            this.Device = this.Device as DoorInput4;
            this.Device.CallbackUI += this.callbackUI;
            this.Device.CallbackUI += frmSetting.CallBackUI;
            frmSetting.DeviceEdit = this.Device;
        }   

        private void FrmFourInput_Load(object sender, EventArgs e)
        {
            frmSetting.DeviceEdit.SearchVer();//---获取版本号-----   
            InitSelectDevice();
            tctrlEdit.SelectedTabPageIndex = 0;
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

                }
            }
            catch { }
        }

        /// <summary>
        /// 选择事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceData DeviceSelect = new BaseDevice(SelectDeviceList[CbxSelectDevice.SelectedIndex]);
            if (Device.MAC == DeviceSelect.MAC) return;

            DeviceSelect.CallbackUI += this.callbackUI;
            DeviceSelect.CallbackUI += frmSetting.CallBackUI;
            frmSetting.DeviceEdit = DeviceSelect;
            Device = DeviceSelect;
            Device.SearchVer();
        }

        /// <summary>
        /// 全选,全不选
        /// </summary>
        private void cdtSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (CheckedListBoxItem item in clbcAqjb.Items)
                item.CheckState = cdtSelectAll.CheckState;
        }


    }
}
