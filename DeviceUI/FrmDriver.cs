﻿using System;
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
        private GridViewComboBox cbxControlObj;//---下拉选择控制对象--
        public FrmDriver(Device _device)
            : base(_device)
        {          
            InitializeComponent();
            this.DeviceEdit.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            this.DeviceEdit.OnCallbackUI_Action += BaseViewSetting.CallBackUI;//----注册回调事件
            //-----初始化回路选择----  
            dtCircuit.Columns.Add(ViewConfig.DC_ID, System.Type.GetType("System.String"));
            dtCircuit.Columns.Add(ViewConfig.DC_CONTROL_OBJ, System.Type.GetType("System.String"));
            dtCircuit.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            num.FieldName = ViewConfig.DC_ID;
            name.FieldName = ViewConfig.DC_NAME;
            dcCtrlObj.FieldName = ViewConfig.DC_CONTROL_OBJ;
            cbxControlObj = new GridViewComboBox();//---灯的下拉选择--
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_NULL_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_SCENE_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_AMPLIFIER_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_LIGHT_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_LIGHT_ON_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_LIGHT_OFF_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_CURTAIN_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_CURTAIN_ON_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_CURTAIN_OFF_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_DOOR_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_DOOR_ON_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_DOOR_OFF_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_WINDOW_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_WINDOW_ON_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_WINDOW_OFF_NAME);
            cbxControlObj.Items.Add(ViewConfig.VIRKEY_TYPE_FINDING_NAME);
            cbxControlObj.DropDownRows = 16;
            dcCtrlObj.ColumnEdit = cbxControlObj;

            gcCircuit.DataSource = dtCircuit;
        }

        public FrmDriver()
        {
            InitializeComponent();
        }

        private void FrmBaseDevice_Load(object sender, EventArgs e)
        {
            BaseViewSetting.DeviceEdit = this.DeviceEdit;
            circuitCtrl = DeviceEdit.ContrlObjs[DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME] as Circuit;//获取回路控制对象
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
                    if (callbackParameter.DeviceID == DeviceEdit.DeviceID && callbackParameter.Action == ActionKind.ReadCircuit)
                    {
                        dtCircuit.Rows.Clear();
                        foreach (int key in circuitCtrl.ListCircuitIDAndName.Keys)
                        {
                            string controlObjName = "";
                            if(ViewConfig.VIRKEY_TYPE_ID_NAME.ContainsKey(circuitCtrl.ListCircuitIDAndControlObj[key]))
                            {
                                controlObjName = ViewConfig.VIRKEY_TYPE_ID_NAME[circuitCtrl.ListCircuitIDAndControlObj[key]];
                            }
                            dtCircuit.Rows.Add(key, controlObjName, circuitCtrl.ListCircuitIDAndName[key]);
                        }
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

            //this.DeviceEdit.OnCallbackUI_Action -= this.callbackUI;//--退订回调事件
            //this.DeviceEdit.OnCallbackUI_Action -= BaseViewSetting.CallBackUI;//----退订回调事件
            //DeviceData deviceData = new DeviceData(SelectDeviceList[CbxSelectDevice.SelectedIndex]);//设备数据
            //Device DeviceSelect = FactoryDevice.CreateDevice(deviceData.ByteKindID).CreateDevice(deviceData);//--新建同类型设备对象---
            //if (DeviceEdit.MAC == DeviceSelect.MAC) return;

            //BaseViewSetting.DeviceEdit = DeviceSelect;              //---基础配置编辑  
            //DeviceEdit = DeviceSelect;                                 //---父类设备对象-----              
            //circuitCtrl = this.DeviceEdit.ContrlObjs[DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME] as Circuit;     //获取回路控制对象
            //DeviceEdit.OnCallbackUI_Action += this.callbackUI;          //--注册回调事件
            //DeviceEdit.OnCallbackUI_Action += BaseViewSetting.CallBackUI;//----注册回调事件

            //this.Text = DeviceEdit.Name;                         //----界面标题------
            //BaseViewSetting.DeviceEdit.SearchVer();                 //---获取版本号-----   
            //InitSelectDevice();                                     //---初始化选择设备---
            //circuitCtrl.ReadRoadTitle();                            //读取回路列表
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
            {
                string controlObjName = dr[ViewConfig.DC_CONTROL_OBJ].ToString(); 
                circuitCtrl.SaveRoadSetting(Convert.ToInt16(dr[ViewConfig.DC_ID].ToString()) - 1,
                   controlObjName, dr[ViewConfig.DC_NAME].ToString());//--保存回路名称---
            } 
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
