using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.Collections;
using DevExpress.XtraEditors;

namespace ConfigDevice
{
    public partial class FrmBodyIndction : ConfigDevice.FrmDevice
    {
        private ThreadActionTimer refreshSateTimer;//---动态刷新---
        private bool autoRefresh = false;
        private BodyInduction bodyInduction;//---人体感应----
        private LookupIDAndNameTable dtIDName = new LookupIDAndNameTable();
        private string currentGroupName = "";//当前组名
        private LogicQuickSetting logicQuickSetting;//快速配置编辑
        private bool isQuickSetting = false;//是否快速配置设定
        private bool hasInitedLogicAndCommand = false;//---是否已经初始化逻辑配置和指令配置------
        private bool hasLoadedLogicAndCommand = false;//-----是否已经加载指令配置和逻辑配置-----
        private DataTable dtShortOutput = new DataTable("Output");//------短路输出数据
        private GridViewComboBox cbxOutputLevel = new GridViewComboBox();//----高低电平选择

        private GridViewTextEdit edtLevel = new GridViewTextEdit();
        public FrmBodyIndction(Device _device)
            : base(_device)
        {
            InitializeComponent();
            //----------指示灯------- 
            sptLightSeconds.Properties.MaxValue = 3600;
            sptLightSeconds.Properties.MinValue = 0;
            cbxOutputLevel.Items.Add(SensorConfig.SCIN_LV_NAME_LOW);
            cbxOutputLevel.Items.Add(SensorConfig.SCIN_LV_NAME_HIGH);
            cbxOutputLevel.Items.Add(SensorConfig.SCIN_LV_NAME_NONE);
            cbxOutputLevel.SelectedIndexChanged += this.cbxAction_Changed;
            edtLevel.ReadOnly = true;
            edtLevel.NullText = "无效";
            edtLevel.Appearance.BackColor = Color.Gainsboro;//灰色
            edtLevel.Appearance.ForeColor = Color.Black; 
            cbxLight.Properties.Items.Add(BodyInductionLight.STATE_LEDACT_OFF);
            cbxLight.Properties.Items.Add(BodyInductionLight.STATE_LEDACT_ON_R);
            cbxLight.Properties.Items.Add(BodyInductionLight.STATE_LEDACT_GL_R); 
            cbxLight.Properties.Items.Add(BodyInductionLight.STATE_LEDACT_NONE);

            bodyInduction = this.DeviceEdit as BodyInduction;
            refreshSateTimer = new ThreadActionTimer(2000, new Action(bodyInduction.ReadState));//---自动刷新----
            viewSecurity.InitViewSecurity(bodyInduction.SecurityObj);//---安防对象---
            //----------回路查询选择------
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_ID, DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None));
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_NAME, 380));

            lookUpEdit.Properties.Name = "lookupEdit";
            lookUpEdit.Properties.DisplayMember = ViewConfig.DC_NAME;
            lookUpEdit.Properties.ValueMember = ViewConfig.DC_ID;
            lookUpEdit.Properties.ShowFooter = false;
            lookUpEdit.Properties.ShowHeader = false;
            lookUpEdit.Properties.DataSource = dtIDName;
            lookUpEdit.Properties.DropDownRows = bodyInduction.Circuit.CircuitCount;
            lookUpEdit.Enter += SysConfig.Edit_Enter;
            lookUpEdit.Leave += SysConfig.Edit_Leave;
            //----------人体感应回调----------- 
            bodyInduction.OnCallbackUI_Action += this.CallbackUI;
            bodyInduction.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            BaseViewSetting.DeviceEdit = bodyInduction;           //---基础配置编辑
            //----------逻辑配置控件----
            viewLogicSetting.ShowToolBar = false;//不显示工具栏  
            //viewCommandEdit.ShowCommandBar = true;//不显示指令栏
            //----------指令配置控件------
            viewCommandSetting.ShowCommandBar = true;
            viewCommandSetting.ShowLogicToolBarSetting();     
            //----------快速配置-----
            logicQuickSetting = new LogicQuickSetting(DeviceConfig.LOCAL_LOGIC_SETTING_EQUIPMENT_PRI_3);
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
            bodyInduction.SearchVer();          //---获取版本号-----   
            bodyInduction.Circuit.ReadRoadTitle();//----读取回路---- 
            bodyInduction.ReadState();  //---读取状态----      
            bodyInduction.ReadConfig(); //---输出配置
            bodyInduction.Light.ReadOpenState();//----读取是否开启---
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
                if ( callbackParameter.DeviceID == bodyInduction.DeviceID)
                {
                    if (callbackParameter.Action == ActionKind.ReadCircuit)
                    {
                        if (!hasInitedLogicAndCommand)
                        {
                            initLogicAndCommand();//---初始化指令配置,逻辑配置
                            viewSecurity.ReadSecurity(lookUpEdit.ItemIndex);//---读取安防配置---
                        }
                        else
                        {
                            dtIDName.Rows.Clear();
                            foreach (int key in bodyInduction.Circuit.ListCircuitIDAndName.Keys)
                            {
                                viewCommandSetting.CommmandGroups.Add(bodyInduction.Circuit.ListCircuitIDAndName[key]);    //---指令组选择----
                                dtIDName.Rows.Add(new object[] { key, bodyInduction.Circuit.ListCircuitIDAndName[key] });  //---初始化逻辑项 
                            }
                            lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
                            viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
                        }
                    }
                   
                }
                //-----读取状态---
                if (callbackParameter.Action == ActionKind.ReadSate )
                {
                    edtUW1.Text = bodyInduction.UWSensor1.SensorData.LevelValue;
                    edtIR.Text = bodyInduction.IRSensor.SensorData.LevelValue;
                    edtUW2.Text = bodyInduction.UWSensor2.SensorData.LevelValue;
                    edtLum.Text = bodyInduction.IRSensor.LumSensorData.ValueAndUnit + " " + bodyInduction.IRSensor.LumSensorData.LevelValue;
                }
                //-----逻辑附加动作-------
                if ( callbackParameter.Action == ActionKind.ReadAdditionAciton )
                {

                    cbxLight.SelectedIndex = bodyInduction.Light.LedAct;//指示灯动作
                    sptLightSeconds.Value = bodyInduction.Light.LedTim;//指示灯秒数
                }
                //-----逻辑配置-------
                if ( callbackParameter.Action == ActionKind.ReadConfig )
                {
                    ztbcUWSensor1.Value = (int)bodyInduction.UWSensor1.Sensitivity;
                    ztbcUWSensor2.Value = (int)bodyInduction.UWSensor2.Sensitivity;
                    ztbcIRSensor.Value = (int)bodyInduction.IRSensor.Sensitivity;
                    cetSentivityLight.Checked = bodyInduction.Light.Open;//----是否开启指示灯---
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
            foreach (int key in bodyInduction.Circuit.ListCircuitIDAndName.Keys)
            {
                viewCommandSetting.CommmandGroups.Add(bodyInduction.Circuit.ListCircuitIDAndName[key]);    //---指令组选择----
                dtIDName.Rows.Add(new object[] { key, bodyInduction.Circuit.ListCircuitIDAndName[key] });  //---初始化逻辑项 
            }
            lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
            viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----



            edtTriggerActionName.Text = bodyInduction.Circuit.ListCircuitIDAndName[1];//----默认显示第一个组名
            if (viewLogicSetting.NeedInit)//----初始化逻辑配置----
                viewLogicSetting.InitLogicList(bodyInduction, SensorConfig.SENSOR_UW_1, SensorConfig.SENSOR_UW_2, 
                    SensorConfig.SENSOR_IR, SensorConfig.SENSOR_LUMINANCE,SensorConfig.SENSOR_SN_1_2,SensorConfig.SENSOR_SN_1_2_3,
                    SensorConfig.SENSOR_TIME,SensorConfig.SENSOR_DATE,
                    SensorConfig.SENSOR_WEEK,SensorConfig.SENSOR_SYSTEM_INTERACTION,SensorConfig.SENSOR_INNER_INTERACTION,
                    SensorConfig.SENSOR_SECURITY_INTERACTION,
                    SensorConfig.SENSOR_INVALID);   
            if (viewCommandSetting.NeedInit)//----初始化指令配置-------
                viewCommandSetting.InitViewCommand(bodyInduction);//初始化       
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
            bodyInduction.ReadAdditionLogic(lookUpEdit.ItemIndex);  //---获取逻辑附加---
            bodyInduction.SecurityObj.ReadSafeSetting(lookUpEdit.ItemIndex);  //---安防配置---
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
            bodyInduction.ReadAdditionLogic(lookUpEdit.ItemIndex);//---获取逻辑附加---
            bodyInduction.SecurityObj.ReadSafeSetting(lookUpEdit.ItemIndex);  //---安防配置---
   
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
                bodyInduction.Light.LedAct = (byte)cbxLight.SelectedIndex;
                bodyInduction.Light.LedTim = (UInt16)sptLightSeconds.Value;
                bodyInduction.SaveAdditionLogic(lookUpEdit.ItemIndex);//---保存附加动作---            
            }
            if (currentGroupName != edtTriggerActionName.Text)//---有修改就执行保存----
                bodyInduction.Circuit.SaveRoadSetting(lookUpEdit.ItemIndex, edtTriggerActionName.Text);//--保存逻辑名称---

            viewSecurity.SaveSecurity(lookUpEdit.ItemIndex);//------保存安防-----
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
            return true;
        } 

        /// <summary>
        /// 双击打开选择
        /// </summary> 
        private void edtTriggerActionName_DoubleClick(object sender, EventArgs e)
        {
            lookUpEdit.ShowPopup();
        }

        /// <summary>
        /// 退出前清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmFlammableGasProbe_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshSateTimer.Stop();
            bodyInduction.RemoveRJ45Callback();//----清空回调-----
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
 

            return bodyInduction.GetAdditionValue();
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
            viewLogicSetting.ReturnLogicData(new CallbackParameter(ActionKind.ReadLogicConfig,logicData));
            //-------附加动作------
            byte[] adittionData = logicQuickSetting.GetLogicAdditionData(cbxQuickSetting.SelectedIndex);
            bodyInduction.SetAdditionLogicData(adittionData);
            this.CallbackUI(new CallbackParameter(ActionKind.ReadAdditionAciton,Radar.CLASS_NAME));//---回调UI---
            //----------手头变更修改状态------
            isQuickSetting = true; viewLogicSetting.IsSystemSetting = true;
        }

        /// <summary>
        /// 更换设备事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BodyInduction _bodyInduction = new BodyInduction(SelectDeviceList[CbxSelectDevice.SelectedIndex]);
            //if (_bodyInduction.MAC == _bodyInduction.MAC) return;
            //_bodyInduction.OnCallbackUI_Action += this.CallbackUI;
            //_bodyInduction.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            //BaseViewSetting.DeviceEdit = _bodyInduction;           //---基础配置编辑  
            //base.DeviceEdit = _bodyInduction;                         //---父类设备对象-----
            //hasInitedLogicAndCommand = false;                   //---是否已经初始化逻辑配置和指令配置------
            //hasLoadedLogicAndCommand = false;                   //---是否已经加载指令配置和逻辑配置-----
            //viewCommandSetting.NeedInit = true;                 //---重新初始化,通过回调实现------
            //viewLogicSetting.NeedInit = true;                   //---重新初始化逻辑配置
            //bodyInduction = _bodyInduction;
            //BaseViewSetting.DeviceEdit = bodyInduction;            //---基础配置编辑

            //lookUpEdit.Properties.DataSource = new DataTable(); //----初始化列表选择-------
            //lookUpEdit.ItemIndex = -1;
            //this.Text = _bodyInduction.Name;                      //----界面标题------
            //base.InitSelectDevice();                            //---初始化选择列表     
            //loadData();                                 //----加载配置界面数据
            //bodyInduction.ReadAdditionLogic(0);                //----重新加载附加数据---
        }

        private void cbxAction_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void btSave_Click(object sender, EventArgs e)
        {
            //-----保存配置------
            bodyInduction.UWSensor1.Sensitivity = (byte)ztbcUWSensor1.Value;
            bodyInduction.UWSensor2.Sensitivity = (byte)ztbcUWSensor2.Value;
            bodyInduction.IRSensor.Sensitivity = (byte)ztbcIRSensor.Value;
            bodyInduction.SaveConfig();
            bodyInduction.Light.SaveConfig(cetSentivityLight.Checked);//-是否开启指示灯---
        }

        private void ceSafeSetting_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 动作切换
        /// </summary>
        private void cbxAction_Changed(object sender, EventArgs e)
        {
      

        }

        private void gvShortOutput_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            cbxAction_Changed(sender, e);

        }

        private void gvShortOutput_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

        }

        private void gvShortOutput_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    string action = gvShortOutput.GetRowCellDisplayText(e.RowHandle, dcLevel);
            //    if (action == SensorConfig.SCIN_LV_NAME_NONE)
            //    {
            //        if (e.Column.Name == dcActionTime.Name || e.Column.Name == dcDelayTime.Name)
            //        { e.Column.AppearanceCell.BackColor = Color.Gray; }

            //    }
            //    else
            //    {
            //        e.Column.AppearanceCell.BackColor = Color.LightYellow;
            //    }
        
            //}
        }


        /// <summary>
        /// 刷新
        /// </summary>
        private void btRefrash_Click(object sender, EventArgs e)
        {
            bodyInduction.ReadState();
            bodyInduction.ReadConfig();
        }

        /// <summary>
        /// 显示当前灵敏度
        /// </summary>
        private void ztbcSensor_EditValueChanged(object sender, EventArgs e)
        {
            lblIR.Text = ztbcIRSensor.Value.ToString();
            lblUW1.Text = ztbcUWSensor1.Value.ToString();
            lblUW2.Text = ztbcUWSensor2.Value.ToString();
        }

        private bool changingTestState = false;
        private void testChangeStateView()
        {
            if (changingTestState) return;
            changingTestState = true;
            int groupNum = lookUpEdit.ItemIndex;
            //------互斥----------
            cbnTestIR.Appearance.BackColor = Color.Transparent; cbnTestUW1.Appearance.BackColor = Color.Transparent;
            cbnTestUW2.Appearance.BackColor = Color.Transparent;
            if (cbnTestIR.Checked)
            {
                cbnTestUW1.Checked = false; cbnTestUW2.Checked = false; cbnTestIR.Appearance.BackColor = Color.Orange;
                bodyInduction.IRSensor.OpenTest(groupNum); //bodyInduction.UWSensor1.CloseTest(groupNum); bodyInduction.UWSensor2.CloseTest(groupNum);
            }
            else
                bodyInduction.IRSensor.CloseTest(groupNum);
            if (cbnTestUW1.Checked)
            {
                cbnTestIR.Checked = false; cbnTestUW2.Checked = false; cbnTestUW1.Appearance.BackColor = Color.Orange;
                bodyInduction.UWSensor1.OpenTest(groupNum); //bodyInduction.IRSensor.CloseTest(groupNum); bodyInduction.UWSensor2.CloseTest(groupNum);
            }
            else
                bodyInduction.UWSensor1.CloseTest(groupNum);
            if (cbnTestUW2.Checked)
            {
                cbnTestUW1.Checked = false; cbnTestIR.Checked = false; cbnTestUW2.Appearance.BackColor = Color.Orange;
                bodyInduction.UWSensor2.OpenTest(groupNum);// bodyInduction.UWSensor1.CloseTest(groupNum); bodyInduction.IRSensor.CloseTest(groupNum);
            }
            else
                bodyInduction.UWSensor2.CloseTest(groupNum);
            changingTestState = false;
        }

  

        private void cbnTestUW1_CheckedChanged(object sender, EventArgs e)
        {
            int groupNum = lookUpEdit.ItemIndex;
            if (cbnTestUW1.Checked)
            {
                cbnTestIR.Checked = false; cbnTestUW2.Checked = false; cbnTestUW1.Appearance.BackColor = Color.Orange;
                bodyInduction.UWSensor1.OpenTest(groupNum); //bodyInduction.IRSensor.CloseTest(groupNum); bodyInduction.UWSensor2.CloseTest(groupNum);
                cbnTestIR.Appearance.BackColor = Color.Transparent; 
                cbnTestUW2.Appearance.BackColor = Color.Transparent;
            }
            else
            {
                cbnTestUW1.Checked = false; cbnTestIR.Checked = false; cbnTestUW2.Checked = false;
                cbnTestIR.Appearance.BackColor = Color.Transparent;
                cbnTestUW1.Appearance.BackColor = Color.Transparent;
                cbnTestUW2.Appearance.BackColor = Color.Transparent;
                bodyInduction.UWSensor1.CloseTest(groupNum);
            }
        }

        private void cbnTestIR_CheckedChanged(object sender, EventArgs e)
        {
            int groupNum = lookUpEdit.ItemIndex;
            if (cbnTestIR.Checked)
            {
                cbnTestUW1.Checked = false; cbnTestUW2.Checked = false; cbnTestIR.Appearance.BackColor = Color.Orange;
                bodyInduction.IRSensor.OpenTest(groupNum); //bodyInduction.UWSensor1.CloseTest(groupNum); bodyInduction.UWSensor2.CloseTest(groupNum);
      
                cbnTestUW1.Appearance.BackColor = Color.Transparent;
                cbnTestUW2.Appearance.BackColor = Color.Transparent;
            }
            else
            {
                cbnTestUW1.Checked = false; cbnTestIR.Checked = false; cbnTestUW2.Checked = false;
                cbnTestIR.Appearance.BackColor = Color.Transparent; 
                cbnTestUW1.Appearance.BackColor = Color.Transparent;
                cbnTestUW2.Appearance.BackColor = Color.Transparent;
                bodyInduction.IRSensor.CloseTest(groupNum);
            }
        }

        private void cbnTestUW2_CheckedChanged(object sender, EventArgs e)
        {
            int groupNum = lookUpEdit.ItemIndex;
            if (cbnTestUW2.Checked)
            {
                cbnTestUW1.Checked = false; cbnTestIR.Checked = false; cbnTestUW2.Appearance.BackColor = Color.Orange;
                bodyInduction.UWSensor2.OpenTest(groupNum);

                cbnTestIR.Appearance.BackColor = Color.Transparent;
                cbnTestUW1.Appearance.BackColor = Color.Transparent; 
            }else
            {
                cbnTestUW1.Checked = false; cbnTestIR.Checked = false; cbnTestUW2.Checked = false;

                cbnTestIR.Appearance.BackColor = Color.Transparent;
                cbnTestUW1.Appearance.BackColor = Color.Transparent;
                cbnTestUW2.Appearance.BackColor = Color.Transparent;
                bodyInduction.UWSensor2.CloseTest(groupNum);
            }
        }

 

    }
}
