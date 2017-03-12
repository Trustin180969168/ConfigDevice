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
    public partial class FrmWeather : ConfigDevice.FrmDevice
    {
        private ThreadActionTimer refreshSateTimer;//---动态刷新---
        private bool autoRefresh = false;
        private Environment environment;
        private LookupIDAndNameTable dtIDName = new LookupIDAndNameTable();
        private string currentGroupName = "";//当前组名
        private LogicQuickSetting logicQuickSetting;//快速配置编辑
        private bool isQuickSetting = false;//是否快速配置设定
        private bool hasInitedLogicAndCommand = false;//---是否已经初始化逻辑配置和指令配置------
        private bool hasLoadedLogicAndCommand = false;//-----是否已经加载指令配置和逻辑配置-----
        private DataTable dtSensorState = new DataTable("SensorState");//传感器状态
        public FrmWeather(Device _device)
            : base(_device)
        {
            InitializeComponent();
            environment = this.DeviceEdit as Environment;
            //-----------初始化编辑控件------- 
            refreshSateTimer = new ThreadActionTimer(2000, new Action(environment.ReadState));//---自动刷新---- 
            //----------初始化传感器数据--------
            dcSensor.FieldName = ViewConfig.DC_SENSOR_NAME;
            dcValue.FieldName = ViewConfig.DC_SENSOR_VALUE;
            dcLevel.FieldName = ViewConfig.DC_SENSOR_LEVEL;
            dtSensorState.Columns.Add(ViewConfig.DC_SENSOR_NAME, System.Type.GetType("System.String"));
            dtSensorState.Columns.Add(ViewConfig.DC_SENSOR_VALUE, System.Type.GetType("System.String"));
            dtSensorState.Columns.Add(ViewConfig.DC_SENSOR_LEVEL, System.Type.GetType("System.String"));
            dtSensorState.Rows.Add(new object[] { "风速", "", "" });
            dtSensorState.Rows.Add(new object[] { "雨感", "", "" });
            dtSensorState.Rows.Add(new object[] { "温度", "", "" }); 
            dtSensorState.Rows.Add(new object[] { "湿度", "", "" }); 
            dtSensorState.Rows.Add(new object[] { "亮度", "", "" });
            gcEnvironment.DataSource = dtSensorState;

            //----------回路查询选择------
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_ID, DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None));
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_NAME, 380));
            lookUpEdit.Properties.Name = "lookupEdit";
            lookUpEdit.Properties.DisplayMember = ViewConfig.DC_NAME;
            lookUpEdit.Properties.ValueMember = ViewConfig.DC_ID;
            lookUpEdit.Properties.ShowFooter = false;
            lookUpEdit.Properties.ShowHeader = false;
            lookUpEdit.Properties.DataSource = dtIDName;
            lookUpEdit.Properties.DropDownRows = environment.Circuit.CircuitCount;
            lookUpEdit.Enter += SysConfig.Edit_Enter;
            lookUpEdit.Leave += SysConfig.Edit_Leave;
            //--------逻辑配置,命令配置界面配置           
            viewLogicSetting.ShowToolBar = false;// -----逻辑配置控件不显示工具栏  
            viewCommandSetting.ShowCommandBar = true;// -----命令配置控件配置工具栏  
            viewCommandSetting.ShowLogicToolBarSetting();     
            //--------初始化快速配置------------
            logicQuickSetting = new LogicQuickSetting("EQUIPMENT_AIR_QUALITY");
            initLogicQuitSetting();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void FrmFlammableGasProbe_Load(object sender, EventArgs e)
        { 
            environment.OnCallbackUI_Action += this.CallbackUI;
            environment.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            BaseViewSetting.DeviceEdit = environment;           //---基础配置编辑
            base.InitSelectDevice();//初始化选择列表     
            loadData();
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

        /// <summary>
        /// 加载界面数据
        /// </summary>
        private void loadData()
        {
            environment.SearchVer();          //---获取版本号-----   
            environment.Circuit.ReadRoadTitle();//----读取回路---- 
            environment.ReadState();          //---读取状态----            
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
                if (callbackParameter.DeviceID == environment.DeviceID)
                {
                    //---读取完回路----
                    if (callbackParameter.Action == ActionKind.ReadCircuit)
                    {
                        if (!hasInitedLogicAndCommand)
                            initLogicAndCommand();
                        else
                        {
                            dtIDName.Rows.Clear();
                            foreach (int key in environment.Circuit.ListCircuitIDAndName.Keys)
                            {
                                viewCommandSetting.CommmandGroups.Add(this.environment.Circuit.ListCircuitIDAndName[key]);    //---指令组选择----
                                dtIDName.Rows.Add(new object[] { key, environment.Circuit.ListCircuitIDAndName[key] });  //---初始化逻辑项 
                            }
                            lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
                            viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----

                        }
                    }
                    //-----读取完探头参数----- 
                    if (callbackParameter.Action == ActionKind.ReadSate)
                    {
                        dtSensorState.Rows[0][1] = environment.WindySensor.Value / 10.0 + " " + environment.WindySensor.Unit; dtSensorState.Rows[0][2] = environment.WindySensor.LevelValue;//风速    
                        dtSensorState.Rows[1][1] = ""; dtSensorState.Rows[1][2] = environment.RainSensor.LevelValue;//雨感
                        dtSensorState.Rows[2][1] = environment.temperatureSensor.ValueAndUnit; dtSensorState.Rows[2][2] = environment.temperatureSensor.LevelValue;//温度
                        dtSensorState.Rows[3][1] = environment.humiditySensor.ValueAndUnit; dtSensorState.Rows[3][2] = environment.humiditySensor.LevelValue;//湿度
                        dtSensorState.Rows[4][1] = environment.luminanceSensor.ValueAndUnit; dtSensorState.Rows[4][2] = environment.luminanceSensor.LevelValue;//亮度

                        dtSensorState.AcceptChanges();
                        gcEnvironment.DataSource = dtSensorState;
                        gvEnvironment.RefreshData();
                    }
                }
            }
        }

        /// <summary>
        /// 初始化逻辑和指令配置
        /// </summary>
        private void initLogicAndCommand()
        {
            viewCommandSetting.CbxCommandGroup.Items.Clear();
            dtIDName.Rows.Clear();
            foreach (int key in environment.Circuit.ListCircuitIDAndName.Keys)
            {
                viewCommandSetting.CommmandGroups.Add(environment.Circuit.ListCircuitIDAndName[key]);    //---指令组选择----
                dtIDName.Rows.Add(new object[] { key, environment.Circuit.ListCircuitIDAndName[key] });  //---初始化逻辑项 
            }
            lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
            viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----

            edtTriggerActionName.Text = environment.Circuit.ListCircuitIDAndName[1];//----默认显示第一个组名
            if (viewLogicSetting.NeedInit)//----初始化逻辑配置----
                viewLogicSetting.InitLogicList(environment,
                    SensorConfig.SENSOR_WINDY, SensorConfig.SENSOR_RAIN, SensorConfig.SENSOR_TEMPERATURE, SensorConfig.SENSOR_HUMIDITY, SensorConfig.SENSOR_LUMINANCE,
                     SensorConfig.SENSOR_TIME, SensorConfig.SENSOR_DATE, SensorConfig.SENSOR_WEEK, SensorConfig.SENSOR_SYSTEM_INTERACTION,
                     SensorConfig.SENSOR_INNER_INTERACTION
                    );
            if (viewCommandSetting.NeedInit)//----初始化指令配置-------
                viewCommandSetting.InitViewCommand(environment);//初始化       
            hasInitedLogicAndCommand = true;//----初始化完毕-----
            if (tctrlEdit.SelectedTabPageIndex == 2)
            {
                if (lookUpEdit.ItemIndex == 0)
                    btRefreshTrigger_Click(null, null);
                else
                    lookUpEdit.ItemIndex = 0;
                hasLoadedLogicAndCommand = true;//----加载完毕------
            }

        }

        /// <summary>
        /// 切换分页后进行初始化
        /// </summary> 
        private void tctrlEdit_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tctrlEdit.SelectedTabPageIndex == 2 && hasInitedLogicAndCommand && !hasLoadedLogicAndCommand)
            {
                btRefreshTrigger_Click(null, null);
                hasLoadedLogicAndCommand = true;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void btRefreshTrigger_Click(object sender, EventArgs e)
        {
            if (lookUpEdit.ItemIndex == -1) { lookUpEdit.ItemIndex = 0; return; }//----
            viewLogicSetting.ReadLogicList(lookUpEdit.ItemIndex);       //---读取逻辑数据----
            viewCommandSetting.ReadDeviceCommandData(lookUpEdit.ItemIndex);   //---读取命令数据----
           
        }

        /// <summary>
        /// 选择切换
        /// </summary> 
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            lblNum.Text = lookUpEdit.EditValue.ToString() + "、";   //---当前组序号---
            edtTriggerActionName.Text = lookUpEdit.Text;            //---触发组名-----
            currentGroupName = edtTriggerActionName.Text;           //---当前触发组名
            viewLogicSetting.ReadLogicList(lookUpEdit.ItemIndex);   //---获取逻辑列表----
            viewCommandSetting.CbxCommandGroup.SelectedIndex = lookUpEdit.ItemIndex;//----获取指令配置----

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

        }



        /// <summary>
        /// 保存
        /// </summary> 
        private void btSaveTrigger_Click(object sender, EventArgs e)
        {
            //-----保存附加动作----
            lookUpEdit.ItemIndex = lookUpEdit.ItemIndex == -1 ? 0 : lookUpEdit.ItemIndex;
            if ( isQuickSetting)
            {    

            }
            if (currentGroupName != edtTriggerActionName.Text)//---有修改就执行保存----
                environment.Circuit.SaveRoadSetting(lookUpEdit.ItemIndex, edtTriggerActionName.Text);//--保存逻辑名称---

            viewLogicSetting.SaveLogicData(lookUpEdit.ItemIndex);//--保存逻辑数据---
            viewLogicSetting.IsSystemSetting = false;//---恢复标志位---
            viewCommandSetting.SaveDeviceCommands(lookUpEdit.ItemIndex);//---保存指令配置---   
        }





        /// <summary>
        /// 双击打开选择
        /// </summary> 
        private void edtTriggerActionName_DoubleClick(object sender, EventArgs e)
        {
            lookUpEdit.ShowPopup();
        }



        private void FrmFlammableGasProbe_FormClosing(object sender, FormClosingEventArgs e)
        {
            environment.RemoveRJ45Callback();//----清空回调-----
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
                    this.viewLogicSetting.GetLogicData(), null);
                cbxQuickSetting.Text = edtLogicLocalSetting.Text;
            }
            else
            {
                logicQuickSetting.SaveLogicLocalSetting(cbxQuickSetting.SelectedIndex, name,
                    this.viewLogicSetting.GetLogicData(), null);
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
            viewLogicSetting.ReturnLogicData(new CallbackParameter(ActionKind.ReadLogicConfig, logicData));
            //-------手头变更修改状态------
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
            _environment.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            BaseViewSetting.DeviceEdit = _environment;           //---基础配置编辑  
            base.DeviceEdit = _environment;                         //---父类设备对象-----
            hasInitedLogicAndCommand = false;                   //---是否已经初始化逻辑配置和指令配置------
            hasLoadedLogicAndCommand = false;                   //---是否已经加载指令配置和逻辑配置-----
            viewCommandSetting.NeedInit = true;                 //---重新初始化,通过回调实现------
            viewLogicSetting.NeedInit = true;                   //---重新初始化逻辑配置
            environment = _environment;
            BaseViewSetting.DeviceEdit = environment;            //---基础配置编辑

            lookUpEdit.Properties.DataSource = new DataTable(); //----初始化列表选择-------
            lookUpEdit.ItemIndex = -1;     
            this.Text = _environment.Name;                      //----界面标题------
            base.InitSelectDevice();                            //---初始化选择列表     
            loadData();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            environment.ReadState();
        }

        private void FrmWeather_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshSateTimer.Stop();
            environment.RemoveRJ45Callback();
        }



    }
}
