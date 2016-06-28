using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.Collections;

namespace ConfigDevice
{
    public partial class FrmEnvironment : ConfigDevice.FrmDevice
    {
        private ThreadActionTimer refreshSateTimer;//---动态刷新---
        private bool autoRefresh = false;
        private Environment environment;
        private LookupIDAndNameTable dtIDName = new LookupIDAndNameTable();
        private string currentGroupName = "";//当前组名
        private LogicQuickSetting logicQuickSetting;//快速配置编辑
        private bool isQuickSetting = false;//是否快速配置设定
        private DataTable dtSensorState = new DataTable("SensorState");//传感器状态
        public FrmEnvironment(Device _device)
            : base(_device)
        {
            InitializeComponent();
            environment = this.Device as Environment;
            //-----------初始化编辑控件------- 
            refreshSateTimer = new ThreadActionTimer(2000, new Action(environment.ReadState));//---自动刷新---- 
            //----------初始化传感器数据--------
            dcSensor.FieldName = ViewConfig.DC_SENSOR_NAME;
            dcValue.FieldName = ViewConfig.DC_SENSOR_VALUE;
            dcLevel.FieldName = ViewConfig.DC_SENSOR_LEVEL;
            dtSensorState.Columns.Add(ViewConfig.DC_SENSOR_NAME, System.Type.GetType("System.String"));
            dtSensorState.Columns.Add(ViewConfig.DC_SENSOR_VALUE, System.Type.GetType("System.String"));
            dtSensorState.Columns.Add(ViewConfig.DC_SENSOR_LEVEL, System.Type.GetType("System.String"));
            dtSensorState.Rows.Add(new object[] { "温度", "", "" });
            dtSensorState.Rows.Add(new object[] { "湿度", "", "" });
            dtSensorState.Rows.Add(new object[] { "亮度", "", "" });
            dtSensorState.Rows.Add(new object[] { "环境噪声", "", "" });
            dtSensorState.Rows.Add(new object[] { "空气质量", "", "" });
            dtSensorState.Rows.Add(new object[] { "TVOC有害气体","", "" });
            dtSensorState.Rows.Add(new object[] { "二氧化碳(等效)", "", "" });
            dtSensorState.Rows.Add(new object[] { "甲醛", "", "" });
            dtSensorState.Rows.Add(new object[] { "PM2.5", "", "" });
           // dtSensorState.Rows.Add(new object[] { "氧气浓度", "", "" });
            gcEnvironment.DataSource = dtSensorState;
            //----------指示灯-------
            sptLightSeconds.Properties.MaxValue = 65535;
            sptLightSeconds.Properties.MinValue = 1;
            sptLightSeconds.Enter += SysConfig.Edit_Enter;
            sptLightSeconds.Leave += SysConfig.Edit_Leave;
            cbxLight.Properties.Items.Add(Light.STATE_LEDACT_OFF);
            cbxLight.Properties.Items.Add(Light.STATE_LEDACT_GREEN_ON);
            cbxLight.Properties.Items.Add(Light.STATE_LEDACT_ORANGE_ON);
            cbxLight.Properties.Items.Add(Light.STATE_LEDACT_RED_ON); 
            cbxLight.Properties.Items.Add(Light.STATE_LEDACT_RED_GLINT); 

            //----------回路查询选择------
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_ID, "回路", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None));
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_NAME, 380));

            lookUpEdit.Properties.Name = "lookupEdit";
            lookUpEdit.Properties.DisplayMember = ViewConfig.DC_NAME;
            lookUpEdit.Properties.ValueMember = ViewConfig.DC_ID;
            lookUpEdit.Properties.ShowFooter = false;
            lookUpEdit.Properties.ShowHeader = false;
            lookUpEdit.Properties.DataSource = dtIDName;
            //----------可燃气体回调----------- 
            environment.OnCallbackUI_Action += this.CallbackUI;
            environment.OnCallbackUI_Action += frmSetting.CallBackUI;
            frmSetting.DeviceEdit = environment;           //---基础配置编辑
            //----------逻辑配置控件----
            viewLogicSetting.ShowToolBar = false;//不显示工具栏  
            //viewCommandEdit.ShowCommandBar = true;//不显示指令栏

            viewCommandSetting.ShowLogicToolBarSetting();
            viewCommandSetting.ShowToolBar = false;
            //----------快速配置-----
            logicQuickSetting = new LogicQuickSetting("EQUIPMENT_AIR_QUALITY");
            initLogicQuitSetting();
        }

        /// <summary>
        /// 初始化快速配置
        /// </summary>
        private void initLogicQuitSetting()
        {
            cbxQuickSetting.Properties.Items.Clear();
            ArrayList names = logicQuickSetting.GetLogicQuickNameList();
            foreach (string name in names)
                cbxQuickSetting.Properties.Items.Add(name);
            edtLogicLocalSetting.Text = "";
        }

        private void FrmFlammableGasProbe_Load(object sender, EventArgs e)
        {
            base.InitSelectDevice();//初始化选择列表     
            loadData();
        }

        /// <summary>
        /// 加载界面数据
        /// </summary>
        private void loadData()
        {
            environment.SearchVer();          //---获取版本号-----   
            environment.Circuit.ReadRoadTitle();//----读取回路---- 
            environment.ReadState();          //---读取状态----            
            environment.PointLight.ReadParameter();//---读取参数----
           
        }

        /// <summary>
        /// 回调
        /// </summary>
        public override void CallbackUI(CallbackParameter callbackParameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(CallbackUI), callbackParameter);
                return;
            }
            lock (lockObject)
            {
                //---读取完回路----
                if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Circuit.CLASS_NAME)
                {
                    viewCommandSetting.CbxCommandGroup.Items.Clear();
                    dtIDName.Rows.Clear();
                    foreach (int key in environment.Circuit.ListCircuitIDAndName.Keys)
                    {
                        viewCommandSetting.CommmandGroups.Add(environment.Circuit.ListCircuitIDAndName[key]);//----指令组选择----
                        dtIDName.Rows.Add(new object[] { key, environment.Circuit.ListCircuitIDAndName[key] });//初始化逻辑项 
                    }
                    lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
                    viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
                }
                //-----读取完探头参数----- 
                if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Environment.CLASS_NAME)
                {
                    dtSensorState.Rows[0][1] = environment.temperatureSensor.ValueAndUnit; dtSensorState.Rows[0][2] = environment.temperatureSensor.LevelValue;//温度
                    dtSensorState.Rows[1][1] = environment.humiditySensor.ValueAndUnit; dtSensorState.Rows[1][2] = environment.humiditySensor.LevelValue;//湿度
                    dtSensorState.Rows[2][1] = environment.luminanceSensor.ValueAndUnit; dtSensorState.Rows[2][2] = environment.luminanceSensor.LevelValue;//亮度
                    dtSensorState.Rows[3][1] = ""; dtSensorState.Rows[3][2] = "";                                                                   //环境噪声
                    dtSensorState.Rows[4][1] = ""; dtSensorState.Rows[4][2] = environment.AQISensor.LevelValue;//空气质量,只显示等级
                    dtSensorState.Rows[5][1] = environment.TVOCSensor.ValueAndUnit; dtSensorState.Rows[5][2] = environment.TVOCSensor.LevelValue;//TVOC有害气体
                    dtSensorState.Rows[6][1] = environment.CO2Sensor.ValueAndUnit; dtSensorState.Rows[6][2] = environment.CO2Sensor.LevelValue;//二氧化碳(等效)
                    dtSensorState.Rows[7][1] = environment.CH2OSensor.ValueAndUnit; dtSensorState.Rows[7][2] = environment.CH2OSensor.LevelValue;//甲醛
                    dtSensorState.Rows[8][1] = environment.PM25Sensor.ValueAndUnit; dtSensorState.Rows[8][2] = environment.PM25Sensor.LevelValue;//PM2.5
               //     dtSensorState.Rows[9][1] = environment.O2Sensor.ValueAndUnit; dtSensorState.Rows[9][2] = environment.O2Sensor.LevelValue;//氧气浓度           
                    dtSensorState.AcceptChanges();

                    gcEnvironment.DataSource = dtSensorState;
                    gvEnvironment.RefreshData();
                    //-----逻辑附加动作------- 
                    this.cbxLight.SelectedIndex = environment.PointLight.LedAct;
                    this.sptLightSeconds.Text = environment.PointLight.LedTim.ToString(); 
                }
                //-----读取指示灯参数----------
                if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Light.CLASS_NAME)
                {
                    cedtOpenHealthLight.Checked = environment.PointLight.OpenHealthLight;
                }
            }
        }
        /// <summary>
        /// 自动刷新
        /// </summary> 
        private void btAutoRefresh_CheckedChanged(object sender, EventArgs e)
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
        /// 保存参数
        /// </summary> 
        private void btSave_Click(object sender, EventArgs e)
        {
            if (environment.PointLight.OpenHealthLight != cedtOpenHealthLight.Checked)
            {
                environment.PointLight.WriteParameter(new LightParameter(cedtOpenHealthLight.Checked));
            }
        }

        /// <summary>
        /// 切换分页后进行初始化
        /// </summary> 
        private void tctrlEdit_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tctrlEdit.SelectedTabPageIndex == 2)
            {
                if (viewLogicSetting.NeedInit)
                {
                    viewLogicSetting.InitLogicList( environment,
                        SensorConfig.SENSOR_TEMPERATURE, SensorConfig.SENSOR_HUMIDITY,SensorConfig.SENSOR_LUMINANCE, SensorConfig.SENSOR_SYSTEM_INTERACTION, SensorConfig.SENSOR_AQI,
                        SensorConfig.SENSOR_TVOC, SensorConfig.SENSOR_CO2, SensorConfig.SENSOR_CH20, SensorConfig.SENSOR_PM25,SensorConfig.SENSOR_O2,
                        SensorConfig.SENSOR_TIME,SensorConfig.SENSOR_DATE,SensorConfig.SENSOR_WEEK,SensorConfig.SENSOR_SYSTEM_INTERACTION,SensorConfig.SENSOR_INNER_INTERACTION, 
                        SensorConfig.SENSOR_INVALID);
                    lookUpEdit.ItemIndex = 0;
                }
                if (viewCommandSetting.NeedInit)
                {
                    viewCommandSetting.InitViewCommand(environment);//初始化
                    viewCommandSetting.CbxCommandGroup.SelectedIndex = lookUpEdit.ItemIndex;
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary> 
        private void btSaveTrigger_Click(object sender, EventArgs e)
        {
            //-----保存附加动作----
            if (hasChangedAdditionLogic() || isQuickSetting)
            {    
                environment.PointLight.LedAct = (byte)cbxLight.SelectedIndex;
                environment.PointLight.LedTim = (ushort)this.sptLightSeconds.Value; 
                environment.SaveAdditionLogic(lookUpEdit.ItemIndex);//---保存附加动作---
            }
            if (currentGroupName != edtTriggerActionName.Text)//---有修改就执行保存----
                environment.Circuit.SaveRoadSetting(lookUpEdit.ItemIndex, edtTriggerActionName.Text);//--保存逻辑名称---

            viewLogicSetting.SaveLogicData(lookUpEdit.ItemIndex);//--保存逻辑数据---
            viewLogicSetting.IsSystemSetting = false;//---恢复标志位---
            viewCommandSetting.SaveCommands();//---保存指令配置---   

        }
        /// <summary>
        /// 是否有修改
        /// </summary>
        /// <returns></returns>
        private bool hasChangedAdditionLogic()
        { 
            if (environment.PointLight.LedAct != cbxLight.SelectedIndex) return true;
            if (environment.PointLight.LedTim != (ushort)this.sptLightSeconds.Value) return true; 

            return false;
        }


        /// <summary>
        /// 刷新
        /// </summary>
        private void btRefreshTrigger_Click(object sender, EventArgs e)
        {
            viewLogicSetting.ReadLogicList(lookUpEdit.ItemIndex);//----读取逻辑数据----
            viewCommandSetting.ReadCommandData();//---读取命令数据----
            environment.ReadAdditionLogic(lookUpEdit.ItemIndex);//---获取逻辑附加---
        }

        /// <summary>
        /// 双击打开选择
        /// </summary> 
        private void edtTriggerActionName_DoubleClick(object sender, EventArgs e)
        {
            lookUpEdit.ShowPopup();
        }

        /// <summary>
        /// 选择切换
        /// </summary> 
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            lblNum.Text = lookUpEdit.EditValue.ToString() + "、";
            edtTriggerActionName.Text = lookUpEdit.Text;
            currentGroupName = edtTriggerActionName.Text;
            if (!viewLogicSetting.NeedInit)
                viewLogicSetting.ReadLogicList(lookUpEdit.ItemIndex);//----获取逻辑列表----
            if (!viewCommandSetting.NeedInit)
                //viewCommandEdit.ReadCommandData(lookUpEdit.ItemIndex);//---获取指令配置列表----
                viewCommandSetting.CbxCommandGroup.SelectedIndex = lookUpEdit.ItemIndex;
            environment.ReadAdditionLogic(lookUpEdit.ItemIndex);//---获取逻辑附加---

        }

        private void FrmFlammableGasProbe_FormClosing(object sender, FormClosingEventArgs e)
        {
            environment.RemoveRJ45Callback();//----清空回调-----
        }


        /// <summary>
        /// 获取附加动作值
        /// </summary>
        /// <returns></returns>
        private byte[] getAdditionValue()
        {
            //-----保存附加动作----    
            environment.PointLight.LedAct = (byte)cbxLight.SelectedIndex;
            environment.PointLight.LedTim = (ushort)this.sptLightSeconds.Value; 
            return environment.GetAdditionValue();
        }


        /// <summary>
        /// 保存本地逻辑配置
        /// </summary>
        private void SaveLocalLogic()
        {
            string name = edtLogicLocalSetting.Text;
            if (name == "") return;
            if (!cbxQuickSetting.Properties.Items.Contains(name))//----列表没有,追加到本地列表----
            {
                cbxQuickSetting.Properties.Items.Add(name);
                logicQuickSetting.SaveLogicLocalSetting(cbxQuickSetting.Properties.Items.Count - 1, name,
                    this.viewLogicSetting.GetLogicData(), this.getAdditionValue());
                cbxQuickSetting.Text = edtLogicLocalSetting.Text;
            }
            else
            {
                logicQuickSetting.SaveLogicLocalSetting(cbxQuickSetting.SelectedIndex, name,
                    this.viewLogicSetting.GetLogicData(), this.getAdditionValue());
            }
        }


        /// <summary>
        /// 双击打开快速选择
        /// </summary> 
        private void edtLogicLocalSetting_DoubleClick(object sender, EventArgs e)
        {
            cbxQuickSetting.ShowPopup();
        }


        /// <summary>
        /// 保存本地逻辑
        /// </summary> 
        private void edtLogicLocalSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.Modifiers == ((int)Keys.Control + (int)Keys.Alt) && e.KeyCode == Keys.S)
            {
                SaveLocalLogic();
            }
            else if ((int)e.Modifiers == ((int)Keys.Control + (int)Keys.Alt) && e.KeyCode == Keys.D)
            {
                logicQuickSetting.DelLogicLocalSetting(cbxQuickSetting.SelectedIndex);
                initLogicQuitSetting();//---初始化快速配置列表---
            }
        }


        /// <summary>
        /// 加载快速配置
        /// </summary>
        private void cbxQuickSetting_Closed(object sender, ClosedEventArgs e)
        {
            edtLogicLocalSetting.Text = cbxQuickSetting.Text;
            //-------逻辑数据-----
            viewLogicSetting.ClearTrggerData();
            LogicData logicData = new LogicData(logicQuickSetting.GetLogicData(cbxQuickSetting.SelectedIndex));
            viewLogicSetting.ReturnLogicData(new CallbackParameter(logicData));
            //-------附加动作------
            byte[] adittionData = logicQuickSetting.GetLogicAdditionData(cbxQuickSetting.SelectedIndex);
            environment.SetAdditionLogicData(adittionData);
            this.CallbackUI(new CallbackParameter(FlammableGasProbe.CLASS_NAME));//---回调UI---
            //----------手头变更修改状态------
            isQuickSetting = true; viewLogicSetting.IsSystemSetting = true;
        }

        /// <summary>
        /// 更换设备事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Environment _environment = new Environment(SelectDeviceList[CbxSelectDevice.SelectedIndex]);
            if (environment.MAC == _environment.MAC) return;

            _environment.OnCallbackUI_Action += this.CallbackUI;
            _environment.OnCallbackUI_Action += frmSetting.CallBackUI;
            frmSetting.DeviceEdit = _environment;           //---基础配置编辑  
            viewCommandSetting.NeedInit = true;//----重新初始化,通过回调实现------
            viewLogicSetting.NeedInit = true;//---重新初始化逻辑配置
            environment = _environment;
            this.Text = _environment.Name;
            loadData();
        }

        private void cbxLight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLight.Text == Light.STATE_LEDACT_OFF || cbxLight.Text == Light.STATE_LEDACT_NONE)
                sptLightSeconds.Enabled = false;
            else
                sptLightSeconds.Enabled = true;
        }


    }
}
