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
    public partial class FrmMotorDC : FrmDevice
    {
        private MotorWindow roadWindow; 
        private GridViewDigitalEdit edtCurrent = new GridViewDigitalEdit();
        private GridViewDigitalEdit edtSecond = new GridViewDigitalEdit();
        private DataTable dtMotorSetting = new DataTable("电机配置");
        private DataTable dtMotorAction = new DataTable("电机操作");
        private GridViewComboBox cbxControlObj;//---下拉选择控制对象--
        public FrmMotorDC(Device _device)
            : base(_device)
        {          
            InitializeComponent();
            roadWindow = this.DeviceEdit as MotorWindow;
            refreshSateTimer = new ThreadActionTimer(2000, new Action(this.loadData));//---自动刷新---- 
            //-----配置表数据------
            dtMotorSetting.Columns.Add(ViewConfig.DC_ID, System.Type.GetType("System.String")); 
            dtMotorSetting.Columns.Add(ViewConfig.DC_POSITION, System.Type.GetType("System.String"));
            dtMotorSetting.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            dtMotorSetting.Columns.Add(ViewConfig.DC_VALUE1, System.Type.GetType("System.String"));
            dtMotorSetting.Columns.Add(ViewConfig.DC_VALUE2, System.Type.GetType("System.String"));
            dtMotorSetting.Columns.Add(ViewConfig.DC_CONTROL_OBJ, System.Type.GetType("System.String"));
            for (int i = 0; i < roadWindow.Circuit.CircuitCount; i++)
                dtMotorSetting.Rows.Add(i, i+1+"路电机", "", "", "");
            //----动作表数据-----
            dtMotorAction.Columns.Add(ViewConfig.DC_ID, System.Type.GetType("System.String"));
            dtMotorAction.Columns.Add(ViewConfig.DC_POSITION, System.Type.GetType("System.String"));
            dtMotorAction.Columns.Add(ViewConfig.DC_STATE, System.Type.GetType("System.String"));
            dtMotorAction.Columns.Add(ViewConfig.DC_CURRENT, System.Type.GetType("System.String"));
            dtMotorAction.Columns.Add(ViewConfig.DC_ACTION1, System.Type.GetType("System.String"));
            dtMotorAction.Columns.Add(ViewConfig.DC_ACTION2, System.Type.GetType("System.String"));
            dtMotorAction.Columns.Add(ViewConfig.DC_ACTION3, System.Type.GetType("System.String"));
            dtMotorAction.Columns.Add(ViewConfig.DC_ACTION4, System.Type.GetType("System.String"));
            for (int i = 0; i < roadWindow.Circuit.CircuitCount; i++)
                dtMotorAction.Rows.Add(i, i+1+"路电机", "", "", "停转", "正转", "反转", "测试");

            //----配置绑定----
            dcPosition.FieldName = ViewConfig.DC_POSITION;
            dcName.FieldName = ViewConfig.DC_NAME;    
            dcMotorStopEC.FieldName = ViewConfig.DC_VALUE1;
            dcMotorTurnTime.FieldName = ViewConfig.DC_VALUE2;
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

            //----动作绑定
            dcPosition2.FieldName = ViewConfig.DC_POSITION;
            dcState.FieldName = ViewConfig.DC_STATE;
            dcCurrentEC.FieldName = ViewConfig.DC_CURRENT;
            dcAction1.FieldName = ViewConfig.DC_ACTION1;
            dcAction2.FieldName = ViewConfig.DC_ACTION2;
            dcAction3.FieldName = ViewConfig.DC_ACTION3;
            dcAction4.FieldName = ViewConfig.DC_ACTION4;
            //----配置电流控件-----
            edtCurrent.DisplayFormat.FormatString = "###0 mA";
            edtCurrent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            edtCurrent.Mask.EditMask = "###0 mA";
            edtCurrent.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edtCurrent.Mask.UseMaskAsDisplayFormat = true;
            //----配置秒控件----
            edtSecond.DisplayFormat.FormatString = "###0 秒";
            edtSecond.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            edtSecond.Mask.EditMask = "###0 秒";
            edtSecond.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edtSecond.Mask.UseMaskAsDisplayFormat = true;
            edtSecond.MinValue = 0;
            edtSecond.MaxValue = 900;
            //----绑定控件------
            dcMotorStopEC.ColumnEdit = edtCurrent;
            dcMotorTurnTime.ColumnEdit = edtSecond;
            dcCurrentEC.ColumnEdit = edtCurrent;
            //----列表绑定------
            gcMotorAction.DataSource = dtMotorAction;
            gcMotorSetting.DataSource = dtMotorSetting;
            
        }

        public FrmMotorDC()
        {
            InitializeComponent();
        }

        private void FrmMotorDC3_Load(object sender, EventArgs e)
        {
            this.DeviceEdit.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            this.DeviceEdit.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件
            viewBaseSetting.DeviceEdit = this.DeviceEdit;

            viewBaseSetting.DeviceEdit.SearchVer();//---获取版本号-----   
            InitSelectDevice();//----初始化选择设备列表---
            loadData();//---加载数据----
        }

        /// <summary>
        /// 加载界面数据
        /// </summary>
        private void loadData()
        {
            roadWindow.Circuit.ReadRoadTitle();        //---读取回路名称---
            roadWindow.Motor.ReadMotorParameter();     //---读取参数-----
            roadWindow.Motor.ReadMotorCurrent();       //---读取电流-----
            roadWindow.Motor.ReadMotorCurrentAction(); //---读取动作---
        }

        /// <summary>
        /// 回调
        /// </summary>
        private void callbackUI(CallbackParameter callbackParameter)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(callbackUI), callbackParameter);
            }
            else
            {
                if (callbackParameter.DeviceID == roadWindow.DeviceID)
                {
                    //-----最大电流,最大开关时间-----
                    if (callbackParameter.Action == ActionKind.ReadConfig)
                    {
                        for (int i = 0; i < roadWindow.Circuit.CircuitCount; i++)
                        {
                             dtMotorSetting.Rows[i][dcMotorTurnTime.FieldName] = roadWindow.Motor.ParameterDatas[i].MaxRunTime;
                             dtMotorSetting.Rows[i][dcMotorStopEC.FieldName] = roadWindow.Motor.ParameterDatas[i].MaxStopCE;
                        } 
                        dtMotorSetting.AcceptChanges();
                    }
                    //-----当前电流状态-------
                    if (callbackParameter.Action == ActionKind.ReadPower)
                    {
                        for (int i = 0; i < roadWindow.Circuit.CircuitCount; i++)
                        {
                            dtMotorAction.Rows[i][3] = roadWindow.Motor.CurrentDatas[i];
                        } 
                        dtMotorAction.AcceptChanges();
                    }
                    //-----当前动作状态-------
                    if (callbackParameter.Action == ActionKind.ReadSate)
                    {
                        string[] actionResult = callbackParameter.Parameters as string[];
                        if (actionResult == null) return;
                        for (int i = 0; i < roadWindow.Circuit.CircuitCount; i++)
                        {
                            dtMotorAction.Rows[i][dcState.FieldName] = actionResult[i];
                        } 
                        dtMotorAction.AcceptChanges();
                    }
                    if (callbackParameter.Action == ActionKind.ReadCircuit)
                    {
                        for (int i = 0; i < roadWindow.Circuit.CircuitCount; i++)
                        {
                            dtMotorSetting.Rows[i][dcName.FieldName] = roadWindow.Circuit.ListCircuitIDAndName[i+1];
                        } 
                        //---控制对象
                        Circuit circuit = roadWindow.Circuit; 
                        string controlObjName = "";
                        for (int i = 0; i < roadWindow.Circuit.CircuitCount; i++)
                        {
                            if (ViewConfig.VIRKEY_TYPE_ID_NAME.ContainsKey(circuit.ListCircuitIDAndControlObj[i+1]))
                            {
                                controlObjName = ViewConfig.VIRKEY_TYPE_ID_NAME[circuit.ListCircuitIDAndControlObj[1]];
                                dtMotorSetting.Rows[i][dcCtrlObj.FieldName] = controlObjName;
                            }
                        }  
                        dtMotorSetting.AcceptChanges();

                    }
                }
                else if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Circuit.CLASS_NAME)//---电机回路名称--
                {
                    dtMotorSetting.AcceptChanges();
                }

            }

        }


        /// <summary>
        /// 选择事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {

            //this.DeviceEdit.OnCallbackUI_Action -= this.callbackUI;//--退订回调事件
            //this.DeviceEdit.OnCallbackUI_Action -= viewBaseSetting.CallBackUI;//----退订回调事件
            //DeviceData deviceData = new DeviceData(SelectDeviceList[CbxSelectDevice.SelectedIndex]);//设备数据
            //Device DeviceSelect = FactoryDevice.CreateDevice(deviceData.ByteKindID).CreateDevice(deviceData);//--新建同类型设备对象---
            //if (DeviceEdit.MAC == DeviceSelect.MAC) return;

            //viewBaseSetting.DeviceEdit = DeviceSelect;              //---基础配置编辑  
            //this.DeviceEdit = DeviceSelect;                             //---父类设备对象-----              
            //road3Window = this.DeviceEdit as Road3Window;               //---本界面编辑-----    
            //road3Window.OnCallbackUI_Action += this.callbackUI;     //--注册回调事件
            //road3Window.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件

            //this.Text = road3Window.Name;                   //---界面标题----
            //viewBaseSetting.DeviceEdit.SearchVer();     //---获取版本号-----   
            //InitSelectDevice();                         //---初始化选择设备---

            //loadData();                                 //---加载数据-----
        }

        /// <summary>
        /// 刷新
        /// </summary> 
        private void btAutoRefresh_Click(object sender, EventArgs e)
        {
            autoRefresh = !autoRefresh;
            if (autoRefresh)
            {
                btAutoRefresh.Checked = true;
                refreshSateTimer.Start();
                CommonTools.MessageShow("自动 2秒 刷新一次!", 1, "");
            }
            else
            {
                btAutoRefresh.Checked = false;
                refreshSateTimer.Stop();
                CommonTools.MessageShow("取消自动刷新!", 1, "");
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            //------保存电机名称-------
            gvMotorSetting.PostEditor();
            DataRow drCurrent = gvMotorSetting.GetDataRow(gvMotorSetting.FocusedRowHandle);
            if (drCurrent != null)
                drCurrent.EndEdit();
            DataTable dtModify = dtMotorSetting.GetChanges(DataRowState.Modified);
            if (dtModify == null) return;
            else
            {
                foreach (DataRow dr in dtModify.Rows)
                    this.roadWindow.Circuit.SaveRoadSetting(Convert.ToInt16(dr[ViewConfig.DC_ID].ToString()),
                        dr[ViewConfig.DC_CONTROL_OBJ].ToString(), dr[ViewConfig.DC_NAME].ToString());//--保存回路名称---
                //------保存电机参数-------    
                for (int i = 0; i < roadWindow.Circuit.CircuitCount; i++)
                {
                    roadWindow.Motor.ParameterDatas[i].MaxRunTime = Convert.ToInt16(dtMotorSetting.Rows[i][dcMotorTurnTime.FieldName]);
                    roadWindow.Motor.ParameterDatas[i].MaxStopCE = Convert.ToInt16(dtMotorSetting.Rows[i][dcMotorStopEC.FieldName]);
                } 
                roadWindow.Motor.SaveMotorParameter();

                dtMotorSetting.AcceptChanges();
            }
        }

        /// <summary>
        /// 退出清除订阅回调
        /// </summary>
        private void FrmMotorDC3_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshSateTimer.Stop();
            roadWindow.RemoveRJ45Callback();
        }

        /// <summary>
        /// 执行电机操作
        /// </summary>
        private void linkAction_Click(object sender, EventArgs e)
        {
            string actionName = gvMotorAction.GetRowCellValue(gvMotorAction.FocusedRowHandle, gvMotorAction.FocusedColumn).ToString(); 
            int roadNum = gvMotorAction.FocusedRowHandle;
            roadWindow.Motor.MotorAction(roadNum, actionName);
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            this.loadData();
        }



 
    }
}
