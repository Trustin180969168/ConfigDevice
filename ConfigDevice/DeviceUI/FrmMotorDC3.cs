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
        private bool autoRefresh = false;
        private ThreadActionTimer refreshSateTimer;//---动态刷新---
        private GridViewDigitalEdit edtCurrent = new GridViewDigitalEdit();
        private GridViewDigitalEdit edtSecond = new GridViewDigitalEdit();
        private DataTable dtMotorSetting = new DataTable("电机配置");
        private DataTable dtMotorAction = new DataTable("电机操作");
        public FrmMotorDC3(Device _device)
            : base(_device)
        {          
            InitializeComponent();
            road3Window = this.Device as Road3Window;
            refreshSateTimer = new ThreadActionTimer(2000, new Action(this.loadData));//---自动刷新---- 
            //-----配置表数据------
            dtMotorSetting.Columns.Add(ViewConfig.DC_ID, System.Type.GetType("System.String"));
            dtMotorSetting.Columns.Add(ViewConfig.DC_POSITION, System.Type.GetType("System.String"));
            dtMotorSetting.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            dtMotorSetting.Columns.Add(ViewConfig.DC_VALUE1, System.Type.GetType("System.String"));
            dtMotorSetting.Columns.Add(ViewConfig.DC_VALUE2, System.Type.GetType("System.String"));
            for (int i = 0; i < road3Window.Circuit.CircuitCount; i++)
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
            for (int i = 0; i < road3Window.Circuit.CircuitCount; i++)
                dtMotorAction.Rows.Add(i, i+1+"路电机", "", "", "停转", "正转", "反转", "测试");

            //----配置绑定----
            dcPosition.FieldName = ViewConfig.DC_POSITION;
            dcName.FieldName = ViewConfig.DC_NAME;    
            dcMotorStopEC.FieldName = ViewConfig.DC_VALUE1;
            dcMotorTurnTime.FieldName = ViewConfig.DC_VALUE2;
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
            //----列表绑定------
            gcMotorAction.DataSource = dtMotorAction;
            gcMotorSetting.DataSource = dtMotorSetting;
            
        }

        public FrmMotorDC3()
        {
            InitializeComponent();
        }

        private void FrmMotorDC3_Load(object sender, EventArgs e)
        {
            this.Device.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            this.Device.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件
            viewBaseSetting.DeviceEdit = this.Device;

            viewBaseSetting.DeviceEdit.SearchVer();//---获取版本号-----   
            InitSelectDevice();//----初始化选择设备列表---
            loadData();//---加载数据----
        }

        /// <summary>
        /// 加载界面数据
        /// </summary>
        private void loadData()
        {
            road3Window.Circuit.ReadRoadTitle();        //---读取回路名称---
            road3Window.Motor.ReadMotorParameter();     //---读取参数-----
            road3Window.Motor.ReadMotorCurrent();       //---读取电流-----
            road3Window.Motor.ReadMotorCurrentAction(); //---读取动作---
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
                    if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Motor.CLASS_NAME)//---电机操作-----
                    {
                        //-----最大电流,最大开关时间-----
                        if (callbackParameter.Parameters[1].ToString() == Motor.ACTION_NAME_PARAMETER)
                        {
                            dtMotorSetting.Rows[0][dcMotorTurnTime.FieldName] = road3Window.Motor.Road1MaxRunTime;  //1路参数
                            dtMotorSetting.Rows[0][dcMotorStopEC.FieldName] = road3Window.Motor.Road1MaxStopCE;     //1路参数
                            dtMotorSetting.Rows[1][dcMotorTurnTime.FieldName] = road3Window.Motor.Road2MaxRunTime;  //2路参数
                            dtMotorSetting.Rows[1][dcMotorStopEC.FieldName] = road3Window.Motor.Road2MaxStopCE;     //2路参数
                            dtMotorSetting.Rows[2][dcMotorTurnTime.FieldName] = road3Window.Motor.Road3MaxRunTime;  //3路参数
                            dtMotorSetting.Rows[2][dcMotorStopEC.FieldName] = road3Window.Motor.Road3MaxStopCE;     //3路参数
                            dtMotorSetting.AcceptChanges();
                        }
                        //-----当前电流状态-------
                        if (callbackParameter.Parameters[1].ToString() == Motor.ACTION_NAME_CURRENT_CURRENT)
                        {
                            dtMotorAction.Rows[0][3] = road3Window.Motor.Road1Current;
                            dtMotorAction.Rows[1][3] = road3Window.Motor.Road2Current;
                            dtMotorAction.Rows[2][3] = road3Window.Motor.Road3Current;
                            dtMotorAction.AcceptChanges();
                        }
                        //-----当前动作状态-------
                        if (callbackParameter.Parameters[1].ToString() == Motor.ACTION_NAME_CURRENT_ACTION)
                        {
                            string[] actionResult = callbackParameter.Parameters[2] as string[];
                            dtMotorAction.Rows[0][2] = actionResult[0];
                            dtMotorAction.Rows[1][2] = actionResult[1];
                            dtMotorAction.Rows[2][2] = actionResult[2];
                            dtMotorAction.AcceptChanges();
                        }
                    }
                    else if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Circuit.CLASS_NAME)//---电机回路名称--
                    {
                        foreach (int key in road3Window.Circuit.ListCircuitIDAndName.Keys)
                            dtMotorSetting.Rows[key-1][dcName.FieldName] = road3Window.Circuit.ListCircuitIDAndName[key];              
                        dtMotorSetting.AcceptChanges();
                    }

                }
            
        }


        /// <summary>
        /// 选择事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Device.OnCallbackUI_Action -= this.callbackUI;//--退订回调事件
            this.Device.OnCallbackUI_Action -= viewBaseSetting.CallBackUI;//----退订回调事件
            DeviceData deviceData = new DeviceData(SelectDeviceList[CbxSelectDevice.SelectedIndex]);//设备数据
            Device DeviceSelect = FactoryDevice.CreateDevice(deviceData.ByteKindID).CreateDevice(deviceData);//--新建同类型设备对象---
            if (Device.MAC == DeviceSelect.MAC) return;

            viewBaseSetting.DeviceEdit = DeviceSelect;              //---基础配置编辑  
            this.Device = DeviceSelect;                             //---父类设备对象-----              
            road3Window = this.Device as Road3Window;               //---本界面编辑-----    
            road3Window.OnCallbackUI_Action += this.callbackUI;     //--注册回调事件
            road3Window.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件

            this.Text = road3Window.Name;                   //---界面标题----
            viewBaseSetting.DeviceEdit.SearchVer();     //---获取版本号-----   
            InitSelectDevice();                         //---初始化选择设备---

            loadData();                                 //---加载数据-----
        }

        /// <summary>
        /// 刷新
        /// </summary> 
        private void btRefresh_Click(object sender, EventArgs e)
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
                    this.road3Window.Circuit.SaveRoadSetting(Convert.ToInt16(dr[ViewConfig.DC_ID].ToString()), dr[ViewConfig.DC_NAME].ToString());//--保存回路名称---
                //------保存电机参数-------         
                road3Window.Motor.Road1MaxRunTime = Convert.ToInt16(dtMotorSetting.Rows[0][dcMotorTurnTime.FieldName]);
                road3Window.Motor.Road2MaxRunTime = Convert.ToInt16(dtMotorSetting.Rows[1][dcMotorTurnTime.FieldName]);
                road3Window.Motor.Road3MaxRunTime = Convert.ToInt16(dtMotorSetting.Rows[2][dcMotorTurnTime.FieldName]);
                road3Window.Motor.Road1MaxStopCE = Convert.ToInt16(dtMotorSetting.Rows[0][dcMotorStopEC.FieldName]);
                road3Window.Motor.Road2MaxStopCE = Convert.ToInt16(dtMotorSetting.Rows[1][dcMotorStopEC.FieldName]);
                road3Window.Motor.Road3MaxStopCE = Convert.ToInt16(dtMotorSetting.Rows[2][dcMotorStopEC.FieldName]);
                road3Window.Motor.SaveMotorParameter();

                dtMotorSetting.AcceptChanges();
            }
        }

        /// <summary>
        /// 退出清除订阅回调
        /// </summary>
        private void FrmMotorDC3_FormClosing(object sender, FormClosingEventArgs e)
        {
            road3Window.RemoveRJ45Callback();
        }

        /// <summary>
        /// 执行电机操作
        /// </summary>
        private void linkAction_Click(object sender, EventArgs e)
        {
            string actionName = gvMotorAction.GetRowCellValue(gvMotorAction.FocusedRowHandle, gvMotorAction.FocusedColumn).ToString();

            int roadNum = gvMotorAction.FocusedRowHandle;
            if (roadNum == 0)
            {
                if (actionName == "停转")
                    road3Window.Motor.MotorAction(Motor.ACTION_ROAD_FRONT_1, Motor.ACTION_STOP);
                else if (actionName == "正转")
                    road3Window.Motor.MotorAction(Motor.ACTION_ROAD_FRONT_1, Motor.ACTION_RUN);
                else if (actionName == "反转")
                    road3Window.Motor.MotorAction(Motor.ACTION_ROAD_BACK_1, Motor.ACTION_RUN);
                else if (actionName == "测试")
                    road3Window.Motor.TestMotorAction(roadNum);
            }
            else if (roadNum == 1)
            {
                if (actionName == "停转")
                    road3Window.Motor.MotorAction(Motor.ACTION_ROAD_FRONT_2, Motor.ACTION_STOP);
                else if (actionName == "正转")
                    road3Window.Motor.MotorAction(Motor.ACTION_ROAD_FRONT_2, Motor.ACTION_RUN);
                else if (actionName == "反转")
                    road3Window.Motor.MotorAction(Motor.ACTION_ROAD_BACK_2, Motor.ACTION_RUN);
                else if (actionName == "测试")
                    road3Window.Motor.TestMotorAction(roadNum);
            }
            else if (roadNum == 2)
            {
                if (actionName == "停转")
                    road3Window.Motor.MotorAction(Motor.ACTION_ROAD_FRONT_3, Motor.ACTION_STOP);
                else if (actionName == "正转")
                    road3Window.Motor.MotorAction(Motor.ACTION_ROAD_FRONT_3, Motor.ACTION_RUN);
                else if (actionName == "反转")
                    road3Window.Motor.MotorAction(Motor.ACTION_ROAD_BACK_3, Motor.ACTION_RUN);
                else if (actionName == "测试")
                    road3Window.Motor.TestMotorAction(roadNum);
            } 
        }
        

 
    }
}
