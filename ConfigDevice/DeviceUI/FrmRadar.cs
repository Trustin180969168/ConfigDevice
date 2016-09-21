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
    public partial class FrmRadar : ConfigDevice.FrmDevice
    {
        private ThreadActionTimer refreshSateTimer;//---动态刷新---
        private bool autoRefresh = false;
        private Radar radar;
        private LookupIDAndNameTable dtIDName = new LookupIDAndNameTable();
        private string currentGroupName = "";//当前组名
        private LogicQuickSetting logicQuickSetting;//快速配置编辑
        private bool isQuickSetting = false;//是否快速配置设定
        private bool hasInitedLogicAndCommand = false;//---是否已经初始化逻辑配置和指令配置------
        private bool hasLoadedLogicAndCommand = false;//-----是否已经加载指令配置和逻辑配置-----
        public FrmRadar(Device _device)
            : base(_device)
        {
            InitializeComponent();
            lookUpEdit.Leave += new System.EventHandler(SysConfig.Edit_Leave);
            lookUpEdit.Enter += new System.EventHandler(SysConfig.Edit_Enter);
            radar = this.Device as Radar;

            refreshSateTimer = new ThreadActionTimer(2000, new Action(radar.ReadState));//---自动刷新----
            //----------蜂鸣器-------
            sptBuzzerSeconds.Properties.MaxValue = 65535;
            sptBuzzerSeconds.Properties.MinValue = 1;
            sptBuzzerSeconds.Enter += SysConfig.Edit_Enter;
            sptBuzzerSeconds.Leave += SysConfig.Edit_Leave;
            cbxBuzzer.Properties.Items.Add(Buzzer.STATE_BUZACT_CLOSE);
            cbxBuzzer.Properties.Items.Add(Buzzer.STATE_BUZACT_OPEN);
            cbxBuzzer.Properties.Items.Add(Buzzer.STATE_BUZACT_NONE);
            //----------指示灯-------
            sptLightSeconds.Properties.MaxValue = 65535;
            sptLightSeconds.Properties.MinValue = 1;
            sptLightSeconds.Enter += SysConfig.Edit_Enter;
            sptLightSeconds.Leave += SysConfig.Edit_Leave;
 
            cbxLight.Properties.Items.Add(RadarLight.STATE_LEDACT_OFF);
            cbxLight.Properties.Items.Add(RadarLight.STATE_LEDACT_ON_R);
            cbxLight.Properties.Items.Add(RadarLight.STATE_LEDACT_GL_R);
            cbxLight.Properties.Items.Add(RadarLight.STATE_LEDACT_GL_RB);
            cbxLight.Properties.Items.Add(RadarLight.STATE_LEDACT_NONE);
            //----------回路查询选择------
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_ID, DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None));
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_NAME, 380));

            lookUpEdit.Properties.Name = "lookupEdit";
            lookUpEdit.Properties.DisplayMember = ViewConfig.DC_NAME;
            lookUpEdit.Properties.ValueMember = ViewConfig.DC_ID;
            lookUpEdit.Properties.ShowFooter = false;
            lookUpEdit.Properties.ShowHeader = false;
            lookUpEdit.Properties.DataSource = dtIDName;
            lookUpEdit.Properties.DropDownRows = radar.Circuit.CircuitCount;
            //----------可燃气体回调----------- 
            radar.OnCallbackUI_Action += this.CallbackUI;
            radar.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            BaseViewSetting.DeviceEdit = radar;           //---基础配置编辑
            //----------逻辑配置控件----
            viewLogicSetting.ShowToolBar = false;//不显示工具栏  
            //viewCommandEdit.ShowCommandBar = true;//不显示指令栏

            viewCommandSetting.ShowCommandBar = true;
            viewCommandSetting.ShowLogicToolBarSetting();     
            //----------快速配置-----
            logicQuickSetting = new LogicQuickSetting(DeviceConfig.LOCAL_LOGIC_SETTING_RADAR);
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

        private void FrmRadar_Load(object sender, EventArgs e)
        {
            base.InitSelectDevice();//初始化选择列表     
            loadData();
        }

        /// <summary>
        /// 加载界面数据
        /// </summary>
        private void loadData()
        {
            radar.SearchVer();          //---获取版本号-----   
            radar.Circuit.ReadRoadTitle();//----读取回路---- 
            radar.ReadState();          //---读取状态----      
           
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
                //-----读取完回路----
                if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Circuit.CLASS_NAME
                    && callbackParameter.Parameters[1].ToString() == radar.DeviceID)
                    initLogicAndCommand();//---初始化指令配置,逻辑配置
                //-----读取完探头参数----- 
                if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Radar.CLASS_NAME)
                {                    
                    edtRadarState.Text = radar.RadarSensorObj.LevelValue;
                    edtSwitState.Text = radar.SwitTamperObj.LevelValue;
                    //-----逻辑附加动作-------
                    if (callbackParameter.Parameters[1].ToString() == Radar.ACTION_ADDITION)
                    {
                        cbxBuzzer.SelectedIndex = radar.Buzzer.BuzAct;
                        sptBuzzerSeconds.Text = radar.Buzzer.BuzTim.ToString();
                        cbxLight.SelectedIndex = radar.Light.LedAct;
                        sptLightSeconds.Text = radar.Light.LedTim.ToString();
                    }
                    //-----安防配置-------
                    if (callbackParameter.Parameters[1].ToString() == Radar.ACTION_SAFE)
                    {
                        for (int i = 0; i < radar.SaftFlags.Length; i++)
                            ceSafeSetting.Items[i].CheckState = radar.SaftFlags[i] ? CheckState.Checked : CheckState.Unchecked;
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
            foreach (int key in radar.Circuit.ListCircuitIDAndName.Keys)
            {
                viewCommandSetting.CommmandGroups.Add(radar.Circuit.ListCircuitIDAndName[key]);    //---指令组选择----
                dtIDName.Rows.Add(new object[] { key, radar.Circuit.ListCircuitIDAndName[key] });  //---初始化逻辑项 
            }
            lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
            viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----

            edtTriggerActionName.Text = radar.Circuit.ListCircuitIDAndName[1];//----默认显示第一个组名
            if (viewLogicSetting.NeedInit)//----初始化逻辑配置----
                viewLogicSetting.InitLogicList(radar, SensorConfig.SENSOR_RADAR, SensorConfig.SENSOR_SWIT_TAMPER,
                       SensorConfig.SENSOR_TIME,SensorConfig.SENSOR_DATE,SensorConfig.SENSOR_WEEK,SensorConfig.SENSOR_SECURITY_INTERACTION,
                       SensorConfig.SENSOR_SYSTEM_INTERACTION,SensorConfig.SENSOR_INNER_INTERACTION );   
            if (viewCommandSetting.NeedInit)//----初始化指令配置-------
                viewCommandSetting.InitViewCommand(radar);//初始化       
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
            viewCommandSetting.ReadCommandData(lookUpEdit.ItemIndex);   //---读取命令数据----
            radar.ReadAdditionLogic(lookUpEdit.ItemIndex);  //---获取逻辑附加---
            radar.ReadSafeSetting(lookUpEdit.ItemIndex);  //---安防配置---
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
            radar.ReadAdditionLogic(lookUpEdit.ItemIndex);//---获取逻辑附加---
            radar.ReadSafeSetting(lookUpEdit.ItemIndex);  //---安防配置---
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
        /// 保存
        /// </summary> 
        private void btSaveTrigger_Click(object sender, EventArgs e)
        {
            //-----保存附加动作----
            lookUpEdit.ItemIndex = lookUpEdit.ItemIndex == -1 ? 0 : lookUpEdit.ItemIndex;
            //-----保存附加动作----
            if (hasChangedAdditionLogic() || isQuickSetting)
            {                
                radar.Light.LedAct = (byte)cbxLight.SelectedIndex;
                radar.Light.LedTim = (ushort)this.sptLightSeconds.Value;
                radar.Buzzer.BuzAct = (byte)cbxBuzzer.SelectedIndex;
                radar.Buzzer.BuzTim = (ushort)this.sptBuzzerSeconds.Value;
                radar.SaveAdditionLogic(lookUpEdit.ItemIndex);//---保存附加动作---
            }
            if (currentGroupName != edtTriggerActionName.Text)//---有修改就执行保存----
                radar.Circuit.SaveRoadSetting(lookUpEdit.ItemIndex, edtTriggerActionName.Text);//--保存逻辑名称---
            if (hasChangedSafeLogic())//---保存安防配置------
            {
                for (int i = 0; i < radar.SaftFlags.Length; i++)
                    radar.SaftFlags[i] = ceSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
                radar.SaveSafeSetting(lookUpEdit.ItemIndex);
            }

            viewLogicSetting.SaveLogicData(lookUpEdit.ItemIndex);//--保存逻辑数据---
            viewLogicSetting.IsSystemSetting = false;           //---恢复标志位---
            viewCommandSetting.SaveCommands(lookUpEdit.ItemIndex);//---保存指令配置---   
            
        }
        /// <summary>
        /// 是否有修改
        /// </summary>
        /// <returns></returns>
        private bool hasChangedAdditionLogic()
        {

            if (radar.Light.LedAct != cbxLight.SelectedIndex) return true;
            if (radar.Light.LedTim != (ushort)this.sptLightSeconds.Value) return true;
            if (radar.Buzzer.BuzAct != cbxBuzzer.SelectedIndex) return true;
            if (radar.Buzzer.BuzTim != (ushort)this.sptBuzzerSeconds.Value) return true;

            return false;
        }

        /// <summary>
        /// 是否有修改安防配置
        /// </summary>
        /// <returns></returns>
        private bool hasChangedSafeLogic()
        {
            for (int i = 0; i < radar.SaftFlags.Length; i++)
            {
                if (ceSafeSetting.Items[i].CheckState == CheckState.Checked && !radar.SaftFlags[i])
                    return true;
                if (ceSafeSetting.Items[i].CheckState == CheckState.Unchecked && radar.SaftFlags[i])
                    return true;
            }
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
            refreshSateTimer.Stop();
            radar.RemoveRJ45Callback();//----清空回调-----
        }

        /// <summary>
        /// 刷新状态及参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 获取附加动作值
        /// </summary>
        /// <returns></returns>
        private byte[] getAdditionValue()
        {

            radar.Light.LedAct = (byte)cbxLight.SelectedIndex;
            radar.Light.LedTim = (ushort)this.sptLightSeconds.Value;
            radar.Buzzer.BuzAct = (byte)cbxBuzzer.SelectedIndex;

            return radar.GetAdditionValue();
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
            radar.SetAdditionLogicData(adittionData);
            this.CallbackUI(new CallbackParameter(Radar.CLASS_NAME));//---回调UI---
            //----------手头变更修改状态------
            isQuickSetting = true; viewLogicSetting.IsSystemSetting = true;
        }

        /// <summary>
        /// 更换设备事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Radar _flammableGasProbe = new Radar(SelectDeviceList[CbxSelectDevice.SelectedIndex]);
            if (_flammableGasProbe.MAC == _flammableGasProbe.MAC) return;
            _flammableGasProbe.OnCallbackUI_Action += this.CallbackUI;
            _flammableGasProbe.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            BaseViewSetting.DeviceEdit = _flammableGasProbe;           //---基础配置编辑  
            base.Device = _flammableGasProbe;                         //---父类设备对象-----
            hasInitedLogicAndCommand = false;                   //---是否已经初始化逻辑配置和指令配置------
            hasLoadedLogicAndCommand = false;                   //---是否已经加载指令配置和逻辑配置-----
            viewCommandSetting.NeedInit = true;                 //---重新初始化,通过回调实现------
            viewLogicSetting.NeedInit = true;                   //---重新初始化逻辑配置
            radar = _flammableGasProbe;
            BaseViewSetting.DeviceEdit = radar;            //---基础配置编辑

            lookUpEdit.Properties.DataSource = new DataTable(); //----初始化列表选择-------
            lookUpEdit.ItemIndex = -1;
            this.Text = _flammableGasProbe.Name;                      //----界面标题------
            base.InitSelectDevice();                            //---初始化选择列表     
            loadData();
            radar.ReadAdditionLogic(0);                 //----重新加载附加数据---
        }

        private void cbxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLight.Text == RadarLight.STATE_LEDACT_OFF || cbxLight.Text == RadarLight.STATE_LEDACT_NONE)
                sptLightSeconds.Enabled = false;
            else
                sptLightSeconds.Enabled = true;

            if (cbxBuzzer.Text == Buzzer.STATE_BUZACT_CLOSE ||cbxBuzzer.Text ==  Buzzer.STATE_BUZACT_NONE)
                sptBuzzerSeconds.Enabled = false;
            else
                sptBuzzerSeconds.Enabled = true;
        }

        private void btSave_Click(object sender, EventArgs e)
        {

        }

        private void ceSafeSetting_Click(object sender, EventArgs e)
        {

        }

        private void ceSafeSetting_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {

            if (e.Index == 15)
            {
                if (ceSafeSetting.Items[15].CheckState == CheckState.Checked)
                {
                    for (int i = 0; i < radar.SaftFlags.Length; i++)
                        ceSafeSetting.Items[i].CheckState = CheckState.Checked;
                }
                if (ceSafeSetting.Items[15].CheckState == CheckState.Unchecked)
                {
                    for (int i = 0; i < radar.SaftFlags.Length; i++)
                        ceSafeSetting.Items[i].CheckState = CheckState.Unchecked;
                }
            }
        }

        /// <summary>
        /// 手动刷新状态
        /// </summary>
        private void btRefrash_Click(object sender, EventArgs e)
        {
            radar.ReadState();
        }


   
    }
}
