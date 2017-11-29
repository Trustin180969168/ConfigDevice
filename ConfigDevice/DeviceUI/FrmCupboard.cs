using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmCupboard : FrmDevice
    {
        private Cupboard cupboard;
        public FrmCupboard(Device _device)
            : base(_device)
        {          
            InitializeComponent();
            this.DeviceEdit.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            this.DeviceEdit.OnCallbackUI_Action += BaseViewSetting.CallBackUI;//----注册回调事件

            cupboard = this.DeviceEdit as Cupboard;

        }

        public FrmCupboard()
        {
            InitializeComponent();
        }

        private void FrmCupboard_Load(object sender, EventArgs e)
        {
            BaseViewSetting.DeviceEdit = this.DeviceEdit;
            BaseViewSetting.DeviceEdit.SearchVer();//---获取版本号-----   
            InitSelectDevice();
            deviceSelector.InitViewDelegate = initSelectDeviceView;
            deviceSelector.Init();
            cupboard.ReadConfig();
        }

        private void initSelectDeviceView()
        {   
            deviceSelector.InitViewDelegate += initSelectDeviceView;
            deviceSelector.GvEdit.Columns.ColumnByFieldName(ViewConfig.DC_CONTROL_OBJ).Visible = false;
            deviceSelector.DevicesData.Rows.Add(0, 0);
        
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
                    //-----读取完参数----- 
                    if (callbackParameter.DeviceID == DeviceEdit.DeviceID && callbackParameter.Action == ActionKind.ReadConfig)
                    {
                        DataRow dr = this.deviceSelector.DevicesData.Rows[0];
                        dr[ViewConfig.DC_DEVICE_ID] = cupboard.ExclusionDeviceId.ToString();
                        dr.EndEdit();
                    }
                }
            }
            catch { }
        }
     

        /// <summary>
        /// 保存回路名称
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            this.deviceSelector.GvEdit.PostEditor(); 
            DataRow dr = this.deviceSelector.DevicesData.Rows[0];
            dr.EndEdit();
            cupboard.ExclusionDeviceId = Convert.ToInt16(dr[ViewConfig.DC_DEVICE_ID]);
            cupboard.SaveConfig();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRefresh_Click(object sender, EventArgs e)
        {
            cupboard.ReadConfig();
        }


    }


}
