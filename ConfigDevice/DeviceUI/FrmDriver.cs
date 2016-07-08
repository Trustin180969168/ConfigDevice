using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmDriver : FrmDevice
    {
        private Circuit circuitCtrl;//----回路控制对象----- 
        private DataTable dtCircuit = new DataTable("回路列表选择");
        public FrmDriver(Device _device)
            : base(_device)
        {          
            InitializeComponent();
            this.Device.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            this.Device.OnCallbackUI_Action += BaseViewSetting.CallBackUI;//----注册回调事件
            //-----初始化回路选择----
            dtCircuit.Columns.Add(ViewConfig.DC_ID, System.Type.GetType("System.String"));
            dtCircuit.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            num.FieldName = ViewConfig.DC_ID;
            name.FieldName = ViewConfig.DC_NAME;
            gcCircuit.DataSource = dtCircuit;
        }

        public FrmDriver()
        {
            InitializeComponent();
        }

        private void FrmBaseDevice_Load(object sender, EventArgs e)
        {
            BaseViewSetting.DeviceEdit = this.Device;
            circuitCtrl = Device.ContrlObjs["回路"] as Circuit;//获取回路控制对象
            BaseViewSetting.DeviceEdit.SearchVer();//---获取版本号-----   
            InitSelectDevice();
            circuitCtrl.ReadRoadTitle();//读取回路列表
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
                    //-----读取完探头参数----- 
                    if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Circuit.CLASS_NAME)
                    {
                        dtCircuit.Rows.Clear();
                        foreach (int key in circuitCtrl.ListCircuitIDAndName.Keys)
                            dtCircuit.Rows.Add(key, circuitCtrl.ListCircuitIDAndName[key]);
                        dtCircuit.AcceptChanges();
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



            //DeviceData deviceData = new DeviceData(SelectDeviceList[CbxSelectDevice.SelectedIndex]);//设备数据
            //Device circuitDevice = FactoryDevice.CreateDevice(deviceData.ByteKindID).CreateDevice(deviceData);//--新建同类型设备对象---
            //if (this.Device.MAC == circuitDevice.MAC) return;       //----相同忽略------
            //circuitDevice.OnCallbackUI_Action += this.CallbackUI;
            //circuitDevice.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            //BaseViewSetting.DeviceEdit = circuitDevice;             //---基础配置编辑  
            //base.Device = circuitDevice;                            //---父类设备对象-----         

            //this.Text = circuitDevice.Name;                         //----界面标题------
            //circuitCtrl = this.Device.ContrlObjs["回路"] as Circuit;     //获取回路控制对象
            //BaseViewSetting.DeviceEdit.SearchVer();                 //---获取版本号-----   
            //InitSelectDevice();
            //circuitCtrl.ReadRoadTitle();                            //读取回路列表



            this.Device.OnCallbackUI_Action -= this.callbackUI;//--退订回调事件
            this.Device.OnCallbackUI_Action -= BaseViewSetting.CallBackUI;//----退订回调事件
            DeviceData deviceData = new DeviceData(SelectDeviceList[CbxSelectDevice.SelectedIndex]);//设备数据
            Device DeviceSelect = FactoryDevice.CreateDevice(deviceData.ByteKindID).CreateDevice(deviceData);//--新建同类型设备对象---
            if (Device.MAC == DeviceSelect.MAC) return;

            BaseViewSetting.DeviceEdit = DeviceSelect;              //---基础配置编辑  
            Device = DeviceSelect;                                 //---父类设备对象-----              
            circuitCtrl = this.Device.ContrlObjs["回路"] as Circuit;     //获取回路控制对象
            Device.OnCallbackUI_Action += this.callbackUI;          //--注册回调事件
            Device.OnCallbackUI_Action += BaseViewSetting.CallBackUI;//----注册回调事件

            this.Text = Device.Name;                         //----界面标题------
            BaseViewSetting.DeviceEdit.SearchVer();                 //---获取版本号-----   
            InitSelectDevice();                                     //---初始化选择设备---
            circuitCtrl.ReadRoadTitle();                            //读取回路列表
        }

        /// <summary>
        /// 保存回路名称
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            gvCircuit.PostEditor();
            DataRow drCurrent = gvCircuit.GetDataRow(gvCircuit.FocusedRowHandle);
            if (drCurrent != null)
                drCurrent.EndEdit();
            DataTable dtModify = dtCircuit.GetChanges(DataRowState.Modified);
            if (dtModify == null) return;
            foreach (DataRow dr in dtModify.Rows)
                circuitCtrl.SaveRoadSetting(Convert.ToInt16(dr[ViewConfig.DC_ID].ToString()) - 1, dr[ViewConfig.DC_NAME].ToString());//--保存回路名称---
            dtModify.AcceptChanges();//---提交修改---
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRefresh_Click(object sender, EventArgs e)
        {
            circuitCtrl.ReadRoadTitle();//读取回路列表
        }


    }
}
