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
    public partial class FrmFlammableGasProbe : ConfigDevice.FrmDevice
    { 
        private FlammableGasProbe flammableGasProbe;
        private LookupIDAndNameTable dtIDName = new LookupIDAndNameTable();
        private string currentGroupName = "";//当前组名
        private LogicQuickSetting logicQuickSetting;//快速配置编辑
        private bool isQuickSetting = false;//是否快速配置设定
        private bool hasInitedLogicAndCommand = false;//---是否已经初始化逻辑配置和指令配置------
        private bool hasLoadedLogicAndCommand = false;//-----是否已经加载指令配置和逻辑配置-----
        public FrmFlammableGasProbe(Device _device)
            : base(_device)
        {
            InitializeComponent();
            flammableGasProbe = this.DeviceEdit as FlammableGasProbe;
            //-----------初始化编辑控件-------
            //edtFireCtrlTemperatue.Properties.DisplayFormat.FormatString = "#0.0 ℃";
            //edtFireCtrlTemperatue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //edtFireCtrlTemperatue.Properties.Mask.EditMask = "#0.0 ℃";
            //edtFireCtrlTemperatue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //edtFireCtrlTemperatue.Properties.Mask.UseMaskAsDisplayFormat = true;

            edtVavleEC.Properties.DisplayFormat.FormatString = "#0 mA";
            edtVavleEC.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            edtVavleEC.Properties.Mask.EditMask = "#0 mA";
            edtVavleEC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edtVavleEC.Properties.Mask.UseMaskAsDisplayFormat = true;
            speProbeEC.Enter += SysConfig.Edit_Enter;
            speProbeEC.Leave += SysConfig.Edit_Leave;
            spePreTime.Enter += SysConfig.Edit_Enter;
            spePreTime.Leave += SysConfig.Edit_Leave;
            refreshSateTimer = new ThreadActionTimer(2000, new Action(flammableGasProbe.ReadState));//---自动刷新----
            //----------蜂鸣器-------
            sptBuzzerSeconds.Properties.MaxValue = 65535;
            sptBuzzerSeconds.Properties.MinValue = 0;
            sptBuzzerSeconds.Enter += SysConfig.Edit_Enter;
            sptBuzzerSeconds.Leave += SysConfig.Edit_Leave;
            cbxBuzzer.Properties.Items.Add(Buzzer.STATE_BUZACT_CLOSE);
            cbxBuzzer.Properties.Items.Add(Buzzer.STATE_BUZACT_OPEN);
            cbxBuzzer.Properties.Items.Add(Buzzer.STATE_BUZACT_NONE);
            //----------指示灯-------
            sptLightSeconds.Properties.MaxValue = 65535;
            sptLightSeconds.Properties.MinValue = 0;
            sptLightSeconds.Enter += SysConfig.Edit_Enter;
            sptLightSeconds.Leave += SysConfig.Edit_Leave;      
            cbxLight.Properties.Items.Add(FlammableGasProbeLight.STATE_LEDACT_OFF);
            cbxLight.Properties.Items.Add(FlammableGasProbeLight.STATE_LEDACT_ON_R);
            cbxLight.Properties.Items.Add(FlammableGasProbeLight.STATE_LEDACT_GL_R);
            cbxLight.Properties.Items.Add(FlammableGasProbeLight.STATE_LEDACT_NONE);
            //----------阀门-------
            sptValveSeconds.Properties.MaxValue = 65535;
            sptValveSeconds.Properties.MinValue = 0;
            sptValveSeconds.Enter += SysConfig.Edit_Enter;
            sptValveSeconds.Leave += SysConfig.Edit_Leave;
            cbxValveAction.Properties.Items.Add(Motor.STATE_VALVE_CLOSE);
            cbxValveAction.Properties.Items.Add(Motor.STATE_VALVE_OPEN);
            cbxValveAction.Properties.Items.Add(Motor.STATE_VALVE_NONE); 
            //----------回路查询选择------
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_ID, DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None));
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_NAME, 380));

            lookUpEdit.Properties.Name = "lookupEdit";
            lookUpEdit.Properties.DisplayMember = ViewConfig.DC_NAME;
            lookUpEdit.Properties.ValueMember = ViewConfig.DC_ID;
            lookUpEdit.Properties.ShowFooter = false;
            lookUpEdit.Properties.ShowHeader = false;
            lookUpEdit.Properties.DataSource = dtIDName;
            lookUpEdit.Properties.DropDownRows = flammableGasProbe.ProbeCircuit.CircuitCount;
            lookUpEdit.Enter += SysConfig.Edit_Enter;
            lookUpEdit.Leave += SysConfig.Edit_Leave;
            //----------可燃气体回调----------- 
            flammableGasProbe.OnCallbackUI_Action += this.CallbackUI;
            flammableGasProbe.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            BaseViewSetting.DeviceEdit = flammableGasProbe;           //---基础配置编辑
            //----------逻辑配置控件----
            viewLogicSetting.ShowToolBar = false;//不显示工具栏  
            //viewCommandEdit.ShowCommandBar = true;//不显示指令栏

            viewCommandSetting.ShowCommandBar = true;
            viewCommandSetting.ShowLogicToolBarSetting();     
            //----------快速配置-----
            logicQuickSetting = new LogicQuickSetting(DeviceConfig.LOCAL_LOGIC_SETTING_GAS);
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
            flammableGasProbe.SearchVer();          //---获取版本号-----   
            flammableGasProbe.ProbeCircuit.ReadRoadTitle();//----读取回路---- 
            flammableGasProbe.ReadState();          //---读取状态----     
            flammableGasProbe.Valve.ReadValveParameter();//---读取参数---            
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
                if (flammableGasProbe.DeviceID == callbackParameter.DeviceID)
                {
                    //---读取完回路----
                    if (callbackParameter.Action == ActionKind.ReadCircuit)
                    {
                        if (!hasInitedLogicAndCommand)
                            initLogicAndCommand();//---初始化指令配置,逻辑配置
                        else
                        {
                            dtIDName.Rows.Clear();
                            foreach (int key in flammableGasProbe.ProbeCircuit.ListCircuitIDAndName.Keys)
                            {
                                viewCommandSetting.CommmandGroups.Add(flammableGasProbe.ProbeCircuit.ListCircuitIDAndName[key]);    //---指令组选择----
                                dtIDName.Rows.Add(new object[] { key, flammableGasProbe.ProbeCircuit.ListCircuitIDAndName[key] });  //---初始化逻辑项 
                            }
                            lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
                            viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
                        }
                    }
                    //-----读取完探头参数----- 

                    //-----刷新探头内容-------
                    if (callbackParameter.Action == ActionKind.ReadSate)
                    {
                        edtValveState.Text = flammableGasProbe.Valve.ValveState;
                        edtVavleEC.Text = flammableGasProbe.Valve.ValveElectricCurrent.ToString();
                        edtGasProbeLevel.Text = flammableGasProbe.Probe.LevelValue;
                        edtFireCtrlTemperatue.Text = flammableGasProbe.Temperature.Value.ToString() + " ℃  " + flammableGasProbe.Temperature.LevelValue;
                    }
                    //-----逻辑附加动作-------
                    if (callbackParameter.Action == ActionKind.ReadAdditionAciton)
                    {
                        cbxBuzzer.SelectedIndex = flammableGasProbe.FGP_Buzzer.BuzAct;
                        sptBuzzerSeconds.Text = flammableGasProbe.FGP_Buzzer.BuzTim.ToString();
                        this.cbxLight.SelectedIndex = flammableGasProbe.FGP_Light.LedAct;
                        this.sptLightSeconds.Text = flammableGasProbe.FGP_Light.LedTim.ToString();
                        this.cbxValveAction.SelectedIndex = flammableGasProbe.Valve.ValAct;
                        this.sptValveSeconds.Text = flammableGasProbe.Valve.ValTim.ToString();
                    }

                    //-----读取完阀门参数----------
                    if (callbackParameter.Action == ActionKind.ReadConfig)
                    {
                        speProbeEC.Text = flammableGasProbe.Valve.MaxStopCE.ToString();
                        spePreTime.Text = flammableGasProbe.Valve.MaxRunTime.ToString();

                        chkOpenValve.Checked = flammableGasProbe.Valve.OpenValve;
                        chkClearLight.Checked = flammableGasProbe.Valve.ClearLight;
                        chkClearLoudly.Checked = flammableGasProbe.Valve.ClearBuzzer;
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
            foreach (int key in flammableGasProbe.ProbeCircuit.ListCircuitIDAndName.Keys)
            {
                viewCommandSetting.CommmandGroups.Add(flammableGasProbe.ProbeCircuit.ListCircuitIDAndName[key]);    //---指令组选择----
                dtIDName.Rows.Add(new object[] { key, flammableGasProbe.ProbeCircuit.ListCircuitIDAndName[key] });  //---初始化逻辑项 
            }
            lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
            viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
      
            edtTriggerActionName.Text = flammableGasProbe.ProbeCircuit.ListCircuitIDAndName[1];//----默认显示第一个组名
            if (viewLogicSetting.NeedInit)//----初始化逻辑配置----
                viewLogicSetting.InitLogicList(flammableGasProbe, SensorConfig.SENSOR_FLAMMABLE_GAS_PROBE,
                    SensorConfig.SENSOR_FIRE_TEMPERATURE,SensorConfig.SENSOR_SYSTEM_INTERACTION,SensorConfig.SENSOR_INNER_INTERACTION,
                    SensorConfig.SENSOR_INVALID);   
            if (viewCommandSetting.NeedInit)//----初始化指令配置-------
                viewCommandSetting.InitViewCommand(flammableGasProbe);//初始化       
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
            flammableGasProbe.ReadAdditionLogic(lookUpEdit.ItemIndex);  //---获取逻辑附加---
        }
        /// <summary>
        /// 选择切换
        /// </summary> 
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            lblNum.Text = lookUpEdit.EditValue.ToString() + "、";
            edtTriggerActionName.Text = lookUpEdit.Text;
            currentGroupName = edtTriggerActionName.Text;
            viewLogicSetting.ReadLogicList(lookUpEdit.ItemIndex);//----获取逻辑列表----
            viewCommandSetting.CbxCommandGroup.SelectedIndex = lookUpEdit.ItemIndex;//----获取指令配置----
            flammableGasProbe.ReadAdditionLogic(lookUpEdit.ItemIndex);//---获取逻辑附加---
        }

        /// <summary>
        /// 自动刷新
        /// </summary> 
        private void btAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            autoRefresh = !autoRefresh;
            if (autoRefresh)
            { 
                refreshSateTimer.Start();
                btAutoRefresh.Checked = true;
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
        /// 开阀门
        /// </summary> 
        private void btOpenValve_Click(object sender, EventArgs e)
        {
            flammableGasProbe.Valve.MotorAction(Motor.ACTION_ROAD_FRONT, Motor.ACTION_RUN);
        }

        /// <summary>
        /// 关阀门
        /// </summary> 
        private void btCloseValve_Click(object sender, EventArgs e)
        {
            flammableGasProbe.Valve.MotorAction(Motor.ACTION_ROAD_BACK, Motor.ACTION_RUN);
        }
        /// <summary>
        /// 停止阀门
        /// </summary> 
        private void btStopValve_Click(object sender, EventArgs e)
        {
            flammableGasProbe.Valve.MotorAction(Motor.ACTION_ROAD_BACK, Motor.ACTION_STOP);
        }

        /// <summary>
        /// 保存参数
        /// </summary> 
        private void btSave_Click(object sender, EventArgs e)
        {
            UInt32 flag = 0;
            if (chkOpenValve.Checked) flag = flag | 1;
            if (chkClearLight.Checked) flag = flag | 2;
            if (chkClearLoudly.Checked) flag = flag | 4;
            flammableGasProbe.Valve.SaveValveParameter((short)spePreTime.Value, (short)speProbeEC.Value, flag);
        }

        /// <summary>
        /// 保存
        /// </summary> 
        private void btSaveTrigger_Click(object sender, EventArgs e)
        {
            //-----保存附加动作----
            lookUpEdit.ItemIndex = lookUpEdit.ItemIndex == -1 ? 0 : lookUpEdit.ItemIndex;
            //-----保存附加动作----
            if (hasChangedAdditionLogic() || isQuickSetting)
            {
                flammableGasProbe.Valve.ValAct = (byte)cbxValveAction.SelectedIndex;
                flammableGasProbe.Valve.ValTim = (ushort)sptValveSeconds.Value;
                flammableGasProbe.FGP_Light.LedAct = (byte)cbxLight.SelectedIndex;
                flammableGasProbe.FGP_Light.LedTim = (ushort)this.sptLightSeconds.Value;
                flammableGasProbe.FGP_Buzzer.BuzAct = (byte)cbxBuzzer.SelectedIndex;
                flammableGasProbe.FGP_Buzzer.BuzTim = (ushort)sptBuzzerSeconds.Value;
                flammableGasProbe.SaveAdditionLogic(lookUpEdit.ItemIndex);//---保存附加动作---
            }
            if (currentGroupName != edtTriggerActionName.Text)//---有修改就执行保存----
                flammableGasProbe.ProbeCircuit.SaveRoadSetting(lookUpEdit.ItemIndex, edtTriggerActionName.Text);//--保存逻辑名称---

            viewLogicSetting.SaveLogicData(lookUpEdit.ItemIndex);//--保存逻辑数据---
            viewLogicSetting.IsSystemSetting = false;           //---恢复标志位---
            viewCommandSetting.SaveDeviceCommands(lookUpEdit.ItemIndex);//---保存指令配置---   
            
        }
        /// <summary>
        /// 是否有修改
        /// </summary>
        /// <returns></returns>
        private bool hasChangedAdditionLogic()
        {
            if (flammableGasProbe.Valve.ValAct != cbxValveAction.SelectedIndex) return true;
            if (flammableGasProbe.Valve.ValTim != (ushort)sptValveSeconds.Value) return true;
            if (flammableGasProbe.FGP_Light.LedAct != cbxLight.SelectedIndex) return true;
            if (flammableGasProbe.FGP_Light.LedTim != (ushort)this.sptLightSeconds.Value) return true;
            if (flammableGasProbe.FGP_Buzzer.BuzAct != cbxBuzzer.SelectedIndex) return true;
            if (flammableGasProbe.FGP_Buzzer.BuzTim != (ushort)this.sptBuzzerSeconds.Value) return true;

            return false;
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
            flammableGasProbe.RemoveRJ45Callback();//----清空回调-----
            refreshSateTimer.Stop();//---停止执行----
        }

        /// <summary>
        /// 刷新状态及参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRefresh_Click(object sender, EventArgs e)
        {
            flammableGasProbe.ReadState();//---读取状态----     
            flammableGasProbe.Valve.ReadValveParameter();//---读取参数---
        }



        /// <summary>
        /// 获取附加动作值
        /// </summary>
        /// <returns></returns>
        private byte[] getAdditionValue()
        {
            //-----保存附加动作----  
            flammableGasProbe.Valve.ValAct = (byte)cbxValveAction.SelectedIndex;
            flammableGasProbe.Valve.ValTim = (ushort)sptValveSeconds.Value;
            flammableGasProbe.FGP_Light.LedAct = (byte)cbxLight.SelectedIndex;
            flammableGasProbe.FGP_Light.LedTim = (ushort)this.sptLightSeconds.Value;
            flammableGasProbe.FGP_Buzzer.BuzAct = (byte)cbxBuzzer.SelectedIndex;
            flammableGasProbe.FGP_Buzzer.BuzTim = (ushort)sptValveSeconds.Value;

            return flammableGasProbe.GetAdditionValue();
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
            viewLogicSetting.ReturnLogicData(new CallbackParameter(ActionKind.ReadLogicConfig,this.flammableGasProbe.DeviceID, logicData));
            //-------附加动作------
            byte[] adittionData = logicQuickSetting.GetLogicAdditionData(cbxQuickSetting.SelectedIndex);
            flammableGasProbe.SetAdditionLogicData(adittionData);
            this.CallbackUI(new CallbackParameter(ActionKind.ReadAdditionAciton, FlammableGasProbe.CLASS_NAME));//---回调UI---
            //----------手头变更修改状态------
            isQuickSetting = true; viewLogicSetting.IsSystemSetting = true;

        }

        /// <summary>
        /// 更换设备事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FlammableGasProbe _flammableGasProbe = new FlammableGasProbe(SelectDeviceList[CbxSelectDevice.SelectedIndex]);
            //if (_flammableGasProbe.MAC == _flammableGasProbe.MAC) return;
            //_flammableGasProbe.OnCallbackUI_Action += this.CallbackUI;
            //_flammableGasProbe.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            //BaseViewSetting.DeviceEdit = _flammableGasProbe;           //---基础配置编辑  
            //base.DeviceEdit = _flammableGasProbe;                         //---父类设备对象-----
            //hasInitedLogicAndCommand = false;                   //---是否已经初始化逻辑配置和指令配置------
            //hasLoadedLogicAndCommand = false;                   //---是否已经加载指令配置和逻辑配置-----
            //viewCommandSetting.NeedInit = true;                 //---重新初始化,通过回调实现------
            //viewLogicSetting.NeedInit = true;                   //---重新初始化逻辑配置
            //flammableGasProbe = _flammableGasProbe;
            //BaseViewSetting.DeviceEdit = flammableGasProbe;            //---基础配置编辑

            //lookUpEdit.Properties.DataSource = new DataTable(); //----初始化列表选择-------
            //lookUpEdit.ItemIndex = -1;
            //this.Text = _flammableGasProbe.Name;                      //----界面标题------
            //base.InitSelectDevice();                            //---初始化选择列表     
            //loadData();
        }

        private void cbxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxValveAction.Text == Motor.STATE_VALVE_NONE)
                sptValveSeconds.Enabled = false;
            else
                sptValveSeconds.Enabled = true;

            if (cbxLight.Text == FlammableGasProbeLight.STATE_LEDACT_OFF || cbxLight.Text == FlammableGasProbeLight.STATE_LEDACT_NONE)
                sptLightSeconds.Enabled = false;
            else
                sptLightSeconds.Enabled = true;

            if (cbxBuzzer.Text == Buzzer.STATE_BUZACT_CLOSE ||cbxBuzzer.Text ==  Buzzer.STATE_BUZACT_NONE)
                sptBuzzerSeconds.Enabled = false;
            else
                sptBuzzerSeconds.Enabled = true;  
        }

   
   
    }
}
