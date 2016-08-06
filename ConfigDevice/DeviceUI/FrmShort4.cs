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
    public partial class FrmShort4 : ConfigDevice.FrmDevice
    {
        private ThreadActionTimer refreshSateTimer;//---动态刷新---
        private bool autoRefresh = false;
        private Short4 short4;
        private LookupIDAndNameTable dtIDName = new LookupIDAndNameTable();
        private string currentGroupName = "";//当前组名
        private LogicQuickSetting logicQuickSetting;//快速配置编辑
        private bool isQuickSetting = false;//是否快速配置设定
        private bool hasInitedLogicAndCommand = false;//---是否已经初始化逻辑配置和指令配置------
        private bool hasLoadedLogicAndCommand = false;//-----是否已经加载指令配置和逻辑配置-----
        private DataTable dtShortOutput = new DataTable("Output");//------短路输出数据
        private GridViewComboBox cbxOutputLevel = new GridViewComboBox();//----高低电平选择
        public FrmShort4(Device _device)
            : base(_device)
        {
            InitializeComponent();
            //----初始化短路输出控件---
            speSecond.DisplayFormat.FormatString = "###0 秒";
            speSecond.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            speSecond.Mask.EditMask = "###0 秒";
            speSecond.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            speSecond.Mask.UseMaskAsDisplayFormat = true;
            speSecond.MaxValue = 3600;
            speSecond.MinValue = 0;
            cbxOutputLevel.Items.Add(SensorConfig.SCIN_LV_NAME_LOW);
            cbxOutputLevel.Items.Add(SensorConfig.SCIN_LV_NAME_HIGH);
            cbxOutputLevel.Items.Add("不动作");            
            //-----初始化短路输出数据-----
            dtShortOutput.Columns.Add(ViewConfig.DC_NAME, typeof(String));
            dtShortOutput.Columns.Add(ViewConfig.DC_PARAMETER3, typeof(String));
            dtShortOutput.Columns.Add(ViewConfig.DC_PARAMETER1, typeof(Int16));
            dtShortOutput.Columns.Add(ViewConfig.DC_PARAMETER2, typeof(Int16));
            dtShortOutput.Rows.Add(new object[] { "短路输出1" });
            dtShortOutput.Rows.Add(new object[] { "短路输出2" });
            dtShortOutput.Rows.Add(new object[] { "短路输出3" });
            dtShortOutput.Rows.Add(new object[] { "短路输出4" });
            //-----初始化短路输出----
            dcPosition.FieldName = ViewConfig.DC_NAME;
            dcDelayTime.FieldName = ViewConfig.DC_PARAMETER1;
            dcActionTime.FieldName = ViewConfig.DC_PARAMETER2;
            dcLevel.FieldName = ViewConfig.DC_PARAMETER3;
            dcLevel.ColumnEdit = cbxOutputLevel;
            gcShortOutput.DataSource = dtShortOutput;

            short4 = this.Device as Short4;
            refreshSateTimer = new ThreadActionTimer(2000, new Action(short4.ReadState));//---自动刷新----
 
            //----------回路查询选择------
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_ID, DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None));
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_NAME, 380));

            lookUpEdit.Properties.Name = "lookupEdit";
            lookUpEdit.Properties.DisplayMember = ViewConfig.DC_NAME;
            lookUpEdit.Properties.ValueMember = ViewConfig.DC_ID;
            lookUpEdit.Properties.ShowFooter = false;
            lookUpEdit.Properties.ShowHeader = false;
            lookUpEdit.Properties.DataSource = dtIDName;
            lookUpEdit.Properties.DropDownRows = short4.Circuit.CircuitCount;
            //----------可燃气体回调----------- 
            short4.OnCallbackUI_Action += this.CallbackUI;
            short4.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            BaseViewSetting.DeviceEdit = short4;           //---基础配置编辑
            //----------逻辑配置控件----
            viewLogicSetting.ShowToolBar = false;//不显示工具栏  
            //viewCommandEdit.ShowCommandBar = true;//不显示指令栏

            viewCommandSetting.ShowCommandBar = true;
            viewCommandSetting.ShowLogicToolBarSetting();     
            //----------快速配置-----
            logicQuickSetting = new LogicQuickSetting(DeviceConfig.LOCAL_LOGIC_SETTING_SHORT_IN_4);
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
            short4.SearchVer();          //---获取版本号-----   
            short4.Circuit.ReadRoadTitle();//----读取回路---- 
            short4.ReadState();          //---读取状态----      
            short4.ReadConfig();//------输出配置
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
                    && callbackParameter.Parameters[1].ToString() == short4.DeviceID)
                    initLogicAndCommand();//---初始化指令配置,逻辑配置
                //-----读取完探头参数----- 
                if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Short4.CLASS_NAME)
                {
                    //-----逻辑状态-------
                    if (callbackParameter.Parameters[1].ToString() == Short4.ACTION_STATE)
                    {
                        edtShort4State1.Text = short4.Short4Sensor1.LevelValue; 
                        edtShort4State2.Text = short4.Short4Sensor2.LevelValue;
                        edtShort4State3.Text = short4.Short4Sensor3.LevelValue;
                        edtShort4State4.Text = short4.Short4Sensor4.LevelValue;
                    }
                    //-----短路输入配置-------
                    if (callbackParameter.Parameters[1].ToString() == Short4.ACTION_CONFIG)
                    {
                        cbxShortOut1.SelectedIndex = (int)short4.ShortConfigRoad1;
                        cbxShortOut2.SelectedIndex = (int)short4.ShortConfigRoad2;
                        cbxShortOut3.SelectedIndex = (int)short4.ShortConfigRoad3;
                        cbxShortOut4.SelectedIndex = (int)short4.ShortConfigRoad4;
                    }
                    //-----逻辑附加动作-------
                    if (callbackParameter.Parameters[1].ToString() == Short4.ACTION_ADDITION)
                    {
                        dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER1] = short4.Short4CtrlObj1.usScOutDly; dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER2] = short4.Short4CtrlObj1.usScOutTim;
                        dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER1] = short4.Short4CtrlObj2.usScOutDly; dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER2] = short4.Short4CtrlObj2.usScOutTim;
                        dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER1] = short4.Short4CtrlObj3.usScOutDly; dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER2] = short4.Short4CtrlObj3.usScOutTim;
                        dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER1] = short4.Short4CtrlObj4.usScOutDly; dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER2] = short4.Short4CtrlObj4.usScOutTim;

                        dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)short4.Short4CtrlObj1.ucScOutAct];
                        dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)short4.Short4CtrlObj2.ucScOutAct];
                        dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)short4.Short4CtrlObj3.ucScOutAct];
                        dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)short4.Short4CtrlObj4.ucScOutAct];
                        dtShortOutput.AcceptChanges();
                    }
                    //-----安防配置-------
                    if (callbackParameter.Parameters[1].ToString() == Short4.ACTION_SAFE)
                    {
                        for (int i = 0; i < short4.SaftFlags.Length; i++)
                            ceSafeSetting.Items[i].CheckState = short4.SaftFlags[i] ? CheckState.Checked : CheckState.Unchecked;
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
            foreach (int key in short4.Circuit.ListCircuitIDAndName.Keys)
            {
                viewCommandSetting.CommmandGroups.Add(short4.Circuit.ListCircuitIDAndName[key]);    //---指令组选择----
                dtIDName.Rows.Add(new object[] { key, short4.Circuit.ListCircuitIDAndName[key] });  //---初始化逻辑项 
            }
            lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
            viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----

            edtTriggerActionName.Text = short4.Circuit.ListCircuitIDAndName[1];//----默认显示第一个组名
            if (viewLogicSetting.NeedInit)//----初始化逻辑配置----
                viewLogicSetting.InitLogicList(short4, SensorConfig.SENSOR_SCIN_1, SensorConfig.SENSOR_SCIN_2, 
                    SensorConfig.SENSOR_SCIN_3, SensorConfig.SENSOR_SCIN_4);   
            if (viewCommandSetting.NeedInit)//----初始化指令配置-------
                viewCommandSetting.InitViewCommand(short4);//初始化       
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
            short4.ReadAdditionLogic(lookUpEdit.ItemIndex);  //---获取逻辑附加---
            short4.ReadSafeSetting(lookUpEdit.ItemIndex);  //---安防配置---
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
            short4.ReadAdditionLogic(lookUpEdit.ItemIndex);//---获取逻辑附加---
            short4.ReadSafeSetting(lookUpEdit.ItemIndex);  //---安防配置---
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
            if (hasChangedAdditionLogic())
            {

                short4.Short4CtrlObj1.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER1]);
                short4.Short4CtrlObj2.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER1]);
                short4.Short4CtrlObj3.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER1]);
                short4.Short4CtrlObj4.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER1]);

                short4.Short4CtrlObj1.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER2]);
                short4.Short4CtrlObj2.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER2]);
                short4.Short4CtrlObj3.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER2]);
                short4.Short4CtrlObj4.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER2]);

                short4.Short4CtrlObj1.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER3]));
                short4.Short4CtrlObj2.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER3]));
                short4.Short4CtrlObj3.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER3]));
                short4.Short4CtrlObj4.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER3]));

                short4.SaveAdditionLogic(lookUpEdit.ItemIndex);//---保存附加动作---
            }
            if (currentGroupName != edtTriggerActionName.Text)//---有修改就执行保存----
                short4.Circuit.SaveRoadSetting(lookUpEdit.ItemIndex, edtTriggerActionName.Text);//--保存逻辑名称---
            if (hasChangedSafeLogic())//---保存安防配置------
            {
                for (int i = 0; i < short4.SaftFlags.Length; i++)
                    short4.SaftFlags[i] = ceSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
                short4.SaveSafeSetting(lookUpEdit.ItemIndex);
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
            gvShortOutput.PostEditor();
            DataRow dr = gvShortOutput.GetDataRow(0);
            dr.EndEdit();
            DataTable dtModify = dtShortOutput.GetChanges(DataRowState.Modified);
            if (dtModify != null && dtModify.Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 是否有修改安防配置
        /// </summary>
        /// <returns></returns>
        private bool hasChangedSafeLogic()
        {
            for (int i = 0; i < short4.SaftFlags.Length; i++)
            {
                if (ceSafeSetting.Items[i].CheckState == CheckState.Checked && !short4.SaftFlags[i])
                    return true;
                if (ceSafeSetting.Items[i].CheckState == CheckState.Unchecked && short4.SaftFlags[i])
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
            short4.RemoveRJ45Callback();//----清空回调-----
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
            short4.Short4CtrlObj1.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER1]);
            short4.Short4CtrlObj2.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER1]);
            short4.Short4CtrlObj3.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER1]);
            short4.Short4CtrlObj4.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER1]);

            short4.Short4CtrlObj1.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER2]);
            short4.Short4CtrlObj2.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER2]);
            short4.Short4CtrlObj3.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER2]);
            short4.Short4CtrlObj4.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER2]);

            short4.Short4CtrlObj1.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER3]));
            short4.Short4CtrlObj2.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER3]));
            short4.Short4CtrlObj3.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER3]));
            short4.Short4CtrlObj4.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER3]));

            return short4.GetAdditionValue();
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
            short4.SetAdditionLogicData(adittionData);
            this.CallbackUI(new CallbackParameter(Radar.CLASS_NAME));//---回调UI---
            //----------手头变更修改状态------
            isQuickSetting = true; viewLogicSetting.IsSystemSetting = true;
        }

        /// <summary>
        /// 更换设备事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Short4 _short4 = new Short4(SelectDeviceList[CbxSelectDevice.SelectedIndex]);
            if (_short4.MAC == _short4.MAC) return;
            _short4.OnCallbackUI_Action += this.CallbackUI;
            _short4.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            BaseViewSetting.DeviceEdit = _short4;           //---基础配置编辑  
            base.Device = _short4;                         //---父类设备对象-----
            hasInitedLogicAndCommand = false;                   //---是否已经初始化逻辑配置和指令配置------
            hasLoadedLogicAndCommand = false;                   //---是否已经加载指令配置和逻辑配置-----
            viewCommandSetting.NeedInit = true;                 //---重新初始化,通过回调实现------
            viewLogicSetting.NeedInit = true;                   //---重新初始化逻辑配置
            short4 = _short4;
            BaseViewSetting.DeviceEdit = short4;            //---基础配置编辑

            lookUpEdit.Properties.DataSource = new DataTable(); //----初始化列表选择-------
            lookUpEdit.ItemIndex = -1;
            this.Text = _short4.Name;                      //----界面标题------
            base.InitSelectDevice();                            //---初始化选择列表     
            loadData();                                 //----加载配置界面数据
            short4.ReadAdditionLogic(0);                //----重新加载附加数据---
        }

        private void cbxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
 
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            //-----保存配置------
            short4.ShortConfigRoad1 = (byte)cbxShortOut1.SelectedIndex;
            short4.ShortConfigRoad2 = (byte)cbxShortOut2.SelectedIndex;
            short4.ShortConfigRoad3 = (byte)cbxShortOut3.SelectedIndex;
            short4.ShortConfigRoad4 = (byte)cbxShortOut4.SelectedIndex;
            short4.SaveConfig();
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
                    for (int i = 0; i < short4.SaftFlags.Length; i++)
                        ceSafeSetting.Items[i].CheckState = CheckState.Checked;
                }
                if (ceSafeSetting.Items[15].CheckState == CheckState.Unchecked)
                {
                    for (int i = 0; i < short4.SaftFlags.Length; i++)
                        ceSafeSetting.Items[i].CheckState = CheckState.Unchecked;
                }
            }
        }

        /// <summary>
        /// 手动刷新状态
        /// </summary>
        private void btRefrash_Click(object sender, EventArgs e)
        {
            short4.ReadState();
            short4.ReadConfig();
        }


   
    }
}
