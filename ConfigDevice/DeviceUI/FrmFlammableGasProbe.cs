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
    public partial class FrmFlammableGasProbe : ConfigDevice.FrmDevice
    {
        private ThreadActionTimer refreshSateTimer;//---动态刷新---
        private bool autoRefresh = false;
        private FlammableGasProbe flammableGasProbe;
        private LookupIDAndNameTable dtIDName = new LookupIDAndNameTable();
        private string currentGroupName = "";//当前组名

        public FrmFlammableGasProbe(Device _device)
            : base(_device)
        {
            InitializeComponent();
            flammableGasProbe = this.Device as FlammableGasProbe;
            //-----------初始化编辑控件-------
            edtFireCtrlTemperatue.Properties.DisplayFormat.FormatString = "#0.0 ℃";
            edtFireCtrlTemperatue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            edtFireCtrlTemperatue.Properties.Mask.EditMask = "#0.0 ℃";
            edtFireCtrlTemperatue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edtFireCtrlTemperatue.Properties.Mask.UseMaskAsDisplayFormat = true;

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
            sptBuzzerSeconds.Properties.MaxValue = 3600;
            sptBuzzerSeconds.Properties.MinValue = 0;
            sptBuzzerSeconds.Enter += SysConfig.Edit_Enter;
            sptBuzzerSeconds.Leave += SysConfig.Edit_Leave;
            cbxBuzzer.Properties.Items.Add(Buzzer.STATE_BUZACT_CLOSE);
            cbxBuzzer.Properties.Items.Add(Buzzer.STATE_BUZACT_OPEN);
            cbxBuzzer.Properties.Items.Add(Buzzer.STATE_BUZACT_NONE);
            //----------指示灯-------
            sptLightSeconds.Properties.MaxValue = 3600;
            sptLightSeconds.Properties.MinValue = 0;
            sptLightSeconds.Enter += SysConfig.Edit_Enter;
            sptLightSeconds.Leave += SysConfig.Edit_Leave;
            cbxLight.Properties.Items.Add(Light.STATE_LEDACT_OFF);
            cbxLight.Properties.Items.Add(Light.STATE_LEDACT_ON);
            cbxLight.Properties.Items.Add(Light.STATE_LEDACT_GLINT);
            cbxLight.Properties.Items.Add(Light.STATE_LEDACT_NOT);
            //----------阀门-------
            sptValveSeconds.Properties.MaxValue = 3600;
            sptValveSeconds.Properties.MinValue = 0;
            sptValveSeconds.Enter += SysConfig.Edit_Enter;
            sptValveSeconds.Leave += SysConfig.Edit_Leave;
            cbxValveAction.Properties.Items.Add(Motor.STATE_VALVE_CLOSE);
            cbxValveAction.Properties.Items.Add(Motor.STATE_VALVE_OPEN);
            cbxValveAction.Properties.Items.Add(Motor.STATE_VALVE_NONE); 
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
            flammableGasProbe.OnCallbackUI_Action += this.CallbackUI;
            flammableGasProbe.OnCallbackUI_Action += frmSetting.CallBackUI;
            frmSetting.DeviceEdit = flammableGasProbe;           //---基础配置编辑
            //----------逻辑配置控件----
            viewLogicSetting.ShowToolBar = false;//不显示工具栏  
            viewCommandEdit.ShowCommandBar = false;//不显示指令栏
            viewCommandEdit.ShowToolBar = false;
           
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
            flammableGasProbe.SearchVer();//---获取版本号-----   
            flammableGasProbe.ProbeCircuit.ReadRoadTitle();//----读取回路---- 
            flammableGasProbe.ReadState();//---读取状态----     
            flammableGasProbe.Valve.ReadParameter();//---读取参数---            
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
                    viewCommandEdit.CbxCommandGroup.Items.Clear();
                    dtIDName.Rows.Clear();
                    foreach (int key in flammableGasProbe.ProbeCircuit.ListCircuitIDAndName.Keys)
                    {
                        viewCommandEdit.CommmandGroups.Add(flammableGasProbe.ProbeCircuit.ListCircuitIDAndName[key]);//----指令组选择----
                        dtIDName.Rows.Add(new object[] { key, flammableGasProbe.ProbeCircuit.ListCircuitIDAndName[key] });//初始化逻辑项 
                    }
                    lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
                    viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
                }
                //-----读取完探头参数----- 
                if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == FlammableGasProbe.CLASS_NAME)
                {
                    //-----刷新探头内容-------
                    edtValveState.Text = flammableGasProbe.Valve.ValveState;
                    edtVavleEC.Text = flammableGasProbe.Valve.ValveElectricCurrent.ToString();
                    edtGasProbeLevel.Text = flammableGasProbe.Probe.LevelValue;
                    edtFireCtrlTemperatue.Text = flammableGasProbe.Temperature.Value.ToString();
                    //-----逻辑附加动作-------
                    cbxBuzzer.SelectedIndex = flammableGasProbe.FGP_Buzzer.BuzAct;
                    sptBuzzerSeconds.Text = flammableGasProbe.FGP_Buzzer.BuzTim.ToString();
                    this.cbxLight.SelectedIndex = flammableGasProbe.FGP_Light.LedAct;
                    this.sptLightSeconds.Text = flammableGasProbe.FGP_Light.LedTim.ToString();
                    this.cbxValveAction.SelectedIndex = flammableGasProbe.Valve.ValAct;
                    this.sptValveSeconds.Text = flammableGasProbe.Valve.ValTim.ToString();
                }
                //-----读取完阀门参数----------
                if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Motor.CLASS_NAME)
                {
                    speProbeEC.Text = flammableGasProbe.Valve.MaxStopCE.ToString();
                    spePreTime.Text = flammableGasProbe.Valve.MaxRunTime.ToString();

                    chkOpenValve.Checked = flammableGasProbe.Valve.OpenValve;
                    chkClearLight.Checked = flammableGasProbe.Valve.ClearLight;
                    chkClearLoudly.Checked = flammableGasProbe.Valve.ClearBuzzer;
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
                btAutoRefresh.Image = global::ConfigDevice.Properties.Resources.refresh1;
                refreshSateTimer.Start();
                btAutoRefresh.Checked = true;
                CommonTools.MessageShow("自动 2秒 刷新一次!", 1, "");
            }
            else
            {
                btAutoRefresh.Image = global::ConfigDevice.Properties.Resources.refresh2;
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
            flammableGasProbe.Valve.MotorAction(Motor.ACTION_ROAD_FRONT_1, Motor.ACTION_RUN);
        }

        /// <summary>
        /// 关阀门
        /// </summary> 
        private void btCloseValve_Click(object sender, EventArgs e)
        {
            flammableGasProbe.Valve.MotorAction(Motor.ACTION_ROAD_BACK_1, Motor.ACTION_RUN);
        }
        /// <summary>
        /// 停止阀门
        /// </summary> 
        private void btStopValve_Click(object sender, EventArgs e)
        {
            flammableGasProbe.Valve.MotorAction(Motor.ACTION_ROAD_BACK_1, Motor.ACTION_STOP);
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
            flammableGasProbe.Valve.WriteParameter((short)spePreTime.Value, (short)speProbeEC.Value, flag);
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
                    viewLogicSetting.InitLogicList(flammableGasProbe, SensorConfig.SENSOR_FLAMMABLE_GAS_PROBE,
                        SensorConfig.SENSOR_FIRE_TEMPERATURE, SensorConfig.SENSOR_SYSTEM_INTERACTION
                        //------以下是界面测试,非本设备的触发对象选择-----
                        //, SensorConfig.SENSOR_HUMIDITY, SensorConfig.SENSOR_RADAR, SensorConfig.SENSOR_SWIT_TAMPER,
                        //SensorConfig.SENSOR_TIME, SensorConfig.SENSOR_DATE, SensorConfig.SENSOR_WEEK, SensorConfig.SENSOR_WINDY
                          );
                    lookUpEdit.ItemIndex = 0;
                }
                if (viewCommandEdit.NeedInit)
                {
                    viewCommandEdit.InitViewCommand(flammableGasProbe);//初始化
                    viewCommandEdit.CbxCommandGroup.SelectedIndex = lookUpEdit.ItemIndex;
                }       
            }
        } 

        /// <summary>
        /// 保存
        /// </summary> 
        private void btSaveTrigger_Click(object sender, EventArgs e)
        {
            //-----保存附加动作----
            if (hasChangedAdditionLogic())
            {
                flammableGasProbe.Valve.ValAct = (byte)cbxValveAction.SelectedIndex;
                flammableGasProbe.Valve.ValTim = (ushort)sptValveSeconds.Value;
                flammableGasProbe.FGP_Light.LedAct = (byte)cbxLight.SelectedIndex;
                flammableGasProbe.FGP_Light.LedTim = (ushort)this.sptLightSeconds.Value;
                flammableGasProbe.FGP_Buzzer.BuzAct = (byte)cbxBuzzer.SelectedIndex;
                flammableGasProbe.FGP_Buzzer.BuzTim = (ushort)sptValveSeconds.Value;
                flammableGasProbe.SaveAdditionLogic(lookUpEdit.ItemIndex);//---保存附加动作---
            }
            if(currentGroupName != edtTriggerActionName.Text)//---有修改就执行保存----
                flammableGasProbe.ProbeCircuit.SaveRoadSetting(lookUpEdit.ItemIndex, edtTriggerActionName.Text);//--保存逻辑名称---
            viewLogicSetting.SaveLogicData(lookUpEdit.ItemIndex);//--保存逻辑数据---
            viewCommandEdit.SaveCommands();//---保存指令配置---
      
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
        /// 刷新
        /// </summary>
        private void btRefreshTrigger_Click(object sender, EventArgs e)
        {
            viewLogicSetting.ReadLogicList(lookUpEdit.ItemIndex);//----读取逻辑数据----
            viewCommandEdit.ReadCommandData();//---读取命令数据----
            flammableGasProbe.ReadAdditionLogic(lookUpEdit.ItemIndex);//---获取逻辑附加---
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
            if (!viewCommandEdit.NeedInit)
                viewCommandEdit.ReadCommandData(lookUpEdit.ItemIndex);//---获取指令配置列表----
            flammableGasProbe.ReadAdditionLogic(lookUpEdit.ItemIndex);//---获取逻辑附加---

        }

        private void FrmFlammableGasProbe_FormClosing(object sender, FormClosingEventArgs e)
        {
            flammableGasProbe.RemoveRJ45Callback();//----清空回调-----
        }

        /// <summary>
        /// 刷新状态及参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRefresh_Click(object sender, EventArgs e)
        {
            flammableGasProbe.ReadState();//---读取状态----     
            flammableGasProbe.Valve.ReadParameter();//---读取参数---
        }
        
    }
}
