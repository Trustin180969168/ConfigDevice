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
    public partial class FrmMaglevDoor : ConfigDevice.FrmDevice
    {
        private Maglev maglev;
        private LookupIDAndNameTable dtIDName = new LookupIDAndNameTable();
        private string currentGroupName = "";//当前组名
        private LogicQuickSetting logicQuickSetting;//快速配置编辑
        //private bool isQuickSetting = false;//是否快速配置设定
        private bool hasInitedLogicAndCommand = false;//---是否已经初始化逻辑配置和指令配置------
        private bool hasLoadedLogicAndCommand = false;//-----是否已经加载指令配置和逻辑配置-----
        private DataTable dtShortOutput = new DataTable("Output");//------短路输出数据
        private GridViewComboBox cbxOutputLevel = new GridViewComboBox();//----高低电平选择
        private GridViewTextEdit edtLevel = new GridViewTextEdit();
        public FrmMaglevDoor(Device _device)
            : base(_device)
        {
            InitializeComponent();

            lookUpEdit.Leave += new System.EventHandler(SysConfig.Edit_Leave);
            lookUpEdit.Enter += new System.EventHandler(SysConfig.Edit_Enter);
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
            cbxOutputLevel.Items.Add(SensorConfig.SCIN_LV_NAME_NONE);
            cbxOutputLevel.SelectedIndexChanged += this.cbxAction_Changed;
            edtLevel.ReadOnly = true;
            edtLevel.NullText = "无效";
            edtLevel.Appearance.BackColor = Color.Gainsboro;//灰色
            edtLevel.Appearance.ForeColor = Color.Black;
            //-----初始化短路输出数据-----
            dtShortOutput.Columns.Add(ViewConfig.DC_NAME, typeof(String));
            dtShortOutput.Columns.Add(ViewConfig.DC_PARAMETER3, typeof(String));
            dtShortOutput.Columns.Add(ViewConfig.DC_PARAMETER1, typeof(UInt16));
            dtShortOutput.Columns.Add(ViewConfig.DC_PARAMETER2, typeof(UInt16));
            dtShortOutput.Rows.Add(new object[] { "开门" });
            dtShortOutput.Rows.Add(new object[] { "关门" });
            dtShortOutput.Rows.Add(new object[] { "停止" });
            dtShortOutput.Rows.Add(new object[] { "锁门" });
            dtShortOutput.Rows.Add(new object[] { "指示灯1" });
            dtShortOutput.Rows.Add(new object[] { "指示灯2" });
            dtShortOutput.Rows.Add(new object[] { "输出1" });
            dtShortOutput.Rows.Add(new object[] { "输出2" });
            //-----初始化短路输出----
            dcPosition.FieldName = ViewConfig.DC_NAME;
            dcDelayTime.FieldName = ViewConfig.DC_PARAMETER1;
            dcActionTime.FieldName = ViewConfig.DC_PARAMETER2;
            dcLevel.FieldName = ViewConfig.DC_PARAMETER3;
            dcLevel.ColumnEdit = cbxOutputLevel;
            gcShortOutput.DataSource = dtShortOutput;

            maglev = this.DeviceEdit as Maglev; 
            refreshSateTimer = new ThreadActionTimer(2000, new Action(maglev.ReadState));//---自动刷新----
 
            //----------回路查询选择------
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_ID, DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None));
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_NAME, 380));

            lookUpEdit.Properties.Name = "lookupEdit";
            lookUpEdit.Properties.DisplayMember = ViewConfig.DC_NAME;
            lookUpEdit.Properties.ValueMember = ViewConfig.DC_ID;
            lookUpEdit.Properties.ShowFooter = false;
            lookUpEdit.Properties.ShowHeader = false;
            lookUpEdit.Properties.DataSource = dtIDName;
            lookUpEdit.Properties.DropDownRows = maglev.Circuit.CircuitCount;
            lookUpEdit.Enter += SysConfig.Edit_Enter;
            lookUpEdit.Leave += SysConfig.Edit_Leave;
            //----------可燃气体回调----------- 
            maglev.OnCallbackUI_Action += this.CallbackUI;
            maglev.OnCallbackUI_Action += BaseViewSetting.CallBackUI;
            BaseViewSetting.DeviceEdit = maglev;           //---基础配置编辑
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
            maglev.SearchVer();          //---获取版本号-----   
            maglev.Circuit.ReadRoadTitle();//--读取回路---- 
            maglev.ReadState();          //---读取状态----      
            maglev.ReadConfig();        //----输出配置
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
                if (callbackParameter.DeviceID == maglev.DeviceID)
                {
                    //-----读取完回路----
                    if (callbackParameter.Action == ActionKind.ReadCircuit)
                    {
                        if (!hasInitedLogicAndCommand)
                            initLogicAndCommand();//---初始化指令配置,逻辑配置
                        else
                        {
                            dtIDName.Rows.Clear();
                            foreach (int key in maglev.Circuit.ListCircuitIDAndName.Keys)
                            {
                                viewCommandSetting.CommmandGroups.Add(maglev.Circuit.ListCircuitIDAndName[key]);    //---指令组选择----
                                dtIDName.Rows.Add(new object[] { key, maglev.Circuit.ListCircuitIDAndName[key] });  //---初始化逻辑项 
                            }
                            lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
                            viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
                        }
                    } 
                    //-----逻辑状态-------
                    if (callbackParameter.Action == ActionKind.ReadSate)
                    {
                        edtShort4State1.Text = maglev.Short4Sensor1.LevelValue;
                        edtShort4State2.Text = maglev.Short4Sensor2.LevelValue;
                        edtShort4State3.Text = maglev.Short4Sensor3.LevelValue;
                        edtShort4State4.Text = maglev.Short4Sensor4.LevelValue;
                        edtShort4State5.Text = maglev.Short4Sensor5.LevelValue;
                        edtShort4State6.Text = maglev.Short4Sensor6.LevelValue;
                        edtShort4State7.Text = maglev.Short4Sensor7.LevelValue;
                        edtShort4State8.Text = maglev.Short4Sensor8.LevelValue;
                    }
                    //-----短路输入配置-------
                    if (callbackParameter.Action == ActionKind.ReadConfig)
                    {
                        cbxShortOut1.SelectedIndex = (int)maglev.ShortConfigRoad1;
                        cbxShortOut2.SelectedIndex = (int)maglev.ShortConfigRoad2;
                        cbxShortOut3.SelectedIndex = (int)maglev.ShortConfigRoad3;
                        cbxShortOut4.SelectedIndex = (int)maglev.ShortConfigRoad4;
                        cbxShortOut5.SelectedIndex = (int)maglev.ShortConfigRoad5;
                        cbxShortOut6.SelectedIndex = (int)maglev.ShortConfigRoad6;
                        cbxShortOut7.SelectedIndex = (int)maglev.ShortConfigRoad7;
                        cbxShortOut8.SelectedIndex = (int)maglev.ShortConfigRoad8;
                    }
                    //-----逻辑附加动作-------
                    if (callbackParameter.Action == ActionKind.ReadAdditionAciton)
                    {
                        dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER1] = maglev.Short4CtrlObj1.usScOutDly; 
                        dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER2] = maglev.Short4CtrlObj1.usScOutTim;
                        dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)maglev.Short4CtrlObj1.ucScOutAct];
                        dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER1] = maglev.Short4CtrlObj2.usScOutDly; 
                        dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER2] = maglev.Short4CtrlObj2.usScOutTim;
                        dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)maglev.Short4CtrlObj2.ucScOutAct];
                        dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER1] = maglev.Short4CtrlObj3.usScOutDly; 
                        dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER2] = maglev.Short4CtrlObj3.usScOutTim;
                        dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)maglev.Short4CtrlObj3.ucScOutAct];
                        dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER1] = maglev.Short4CtrlObj4.usScOutDly; 
                        dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER2] = maglev.Short4CtrlObj4.usScOutTim;
                        dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)maglev.Short4CtrlObj4.ucScOutAct];
                        
                        dtShortOutput.Rows[4][ViewConfig.DC_PARAMETER1] = maglev.Short4CtrlObj5.usScOutDly;
                        dtShortOutput.Rows[4][ViewConfig.DC_PARAMETER2] = maglev.Short4CtrlObj5.usScOutTim;
                        dtShortOutput.Rows[4][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)maglev.Short4CtrlObj5.ucScOutAct];
                        dtShortOutput.Rows[5][ViewConfig.DC_PARAMETER1] = maglev.Short4CtrlObj6.usScOutDly;
                        dtShortOutput.Rows[5][ViewConfig.DC_PARAMETER2] = maglev.Short4CtrlObj6.usScOutTim;
                        dtShortOutput.Rows[5][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)maglev.Short4CtrlObj6.ucScOutAct];
                        dtShortOutput.Rows[6][ViewConfig.DC_PARAMETER1] = maglev.Short4CtrlObj7.usScOutDly;
                        dtShortOutput.Rows[6][ViewConfig.DC_PARAMETER2] = maglev.Short4CtrlObj7.usScOutTim;
                        dtShortOutput.Rows[6][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)maglev.Short4CtrlObj7.ucScOutAct];
                        dtShortOutput.Rows[7][ViewConfig.DC_PARAMETER1] = maglev.Short4CtrlObj8.usScOutDly;
                        dtShortOutput.Rows[7][ViewConfig.DC_PARAMETER2] = maglev.Short4CtrlObj8.usScOutTim;
                        dtShortOutput.Rows[7][ViewConfig.DC_PARAMETER3] = cbxOutputLevel.Items[(int)maglev.Short4CtrlObj8.ucScOutAct];


                        dtShortOutput.AcceptChanges();
                    }
                    //-----安防配置-------
                    if (callbackParameter.Action == ActionKind.ReadSafe)
                    {
                        for (int i = 0; i < maglev.SaftFlags.Length; i++)
                            ceSafeSetting.Items[i].CheckState = maglev.SaftFlags[i] ? CheckState.Checked : CheckState.Unchecked;
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
            foreach (int key in maglev.Circuit.ListCircuitIDAndName.Keys)
            {
                viewCommandSetting.CommmandGroups.Add(maglev.Circuit.ListCircuitIDAndName[key]);    //---指令组选择----
                dtIDName.Rows.Add(new object[] { key, maglev.Circuit.ListCircuitIDAndName[key] });  //---初始化逻辑项 
            }
            lookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----
            viewLogicSetting.LookUpEdit.Properties.DataSource = dtIDName;//----逻辑组选择----

            edtTriggerActionName.Text = maglev.Circuit.ListCircuitIDAndName[1];//----默认显示第一个组名
            if (viewLogicSetting.NeedInit)//----初始化逻辑配置----
 
                viewLogicSetting.InitLogicList(maglev, 
                    SensorConfig.SENSOR_TIME,SensorConfig.SENSOR_DATE,
                    SensorConfig.SENSOR_SENONAUTOOFF,SensorConfig.SENSOR_SENONOFF,
                    SensorConfig.SENSOR_KEY1,SensorConfig.SENSOR_KEY2,
                    SensorConfig.SENSOR_SAFETYLC,SensorConfig.SENSOR_ALWAYSON,
                    SensorConfig.SENSOR_CLOSECHECK,SensorConfig.SENSOR_OPENCHECK,
                    SensorConfig.SENSOR_WEEK,SensorConfig.SENSOR_SYSTEM_INTERACTION,SensorConfig.SENSOR_INNER_INTERACTION,
                    SensorConfig.SENSOR_SECURITY_INTERACTION,
                    SensorConfig.SENSOR_INVALID);   
            if (viewCommandSetting.NeedInit)//----初始化指令配置-------
                viewCommandSetting.InitViewCommand(maglev);//初始化       
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
            maglev.ReadAdditionLogic(lookUpEdit.ItemIndex);  //---获取逻辑附加---
            maglev.ReadSafeSetting(lookUpEdit.ItemIndex);  //---安防配置---
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
            maglev.ReadAdditionLogic(lookUpEdit.ItemIndex);//---获取逻辑附加---
            maglev.ReadSafeSetting(lookUpEdit.ItemIndex);  //---安防配置---
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

                maglev.Short4CtrlObj1.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER1]);
                maglev.Short4CtrlObj2.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER1]);
                maglev.Short4CtrlObj3.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER1]);
                maglev.Short4CtrlObj4.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER1]);

                maglev.Short4CtrlObj1.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER2]);
                maglev.Short4CtrlObj2.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER2]);
                maglev.Short4CtrlObj3.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER2]);
                maglev.Short4CtrlObj4.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER2]);

                maglev.Short4CtrlObj1.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER3]));
                maglev.Short4CtrlObj2.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER3]));
                maglev.Short4CtrlObj3.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER3]));
                maglev.Short4CtrlObj4.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER3]));


                maglev.Short4CtrlObj5.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[4][ViewConfig.DC_PARAMETER1]);
                maglev.Short4CtrlObj6.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[5][ViewConfig.DC_PARAMETER1]);
                maglev.Short4CtrlObj7.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[6][ViewConfig.DC_PARAMETER1]);
                maglev.Short4CtrlObj8.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[7][ViewConfig.DC_PARAMETER1]);

                maglev.Short4CtrlObj5.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[4][ViewConfig.DC_PARAMETER2]);
                maglev.Short4CtrlObj6.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[5][ViewConfig.DC_PARAMETER2]);
                maglev.Short4CtrlObj7.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[6][ViewConfig.DC_PARAMETER2]);
                maglev.Short4CtrlObj8.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[7][ViewConfig.DC_PARAMETER2]);

                maglev.Short4CtrlObj5.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[4][ViewConfig.DC_PARAMETER3]));
                maglev.Short4CtrlObj6.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[5][ViewConfig.DC_PARAMETER3]));
                maglev.Short4CtrlObj7.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[6][ViewConfig.DC_PARAMETER3]));
                maglev.Short4CtrlObj8.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[7][ViewConfig.DC_PARAMETER3]));



                maglev.SaveAdditionLogic(lookUpEdit.ItemIndex);//---保存附加动作---
            }
            if (currentGroupName != edtTriggerActionName.Text)//---有修改就执行保存----
                maglev.Circuit.SaveRoadSetting(lookUpEdit.ItemIndex, edtTriggerActionName.Text);//--保存逻辑名称---
            if (hasChangedSafeLogic())//---保存安防配置------
            {
                for (int i = 0; i < maglev.SaftFlags.Length; i++)
                    maglev.SaftFlags[i] = ceSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
                maglev.SaveSafeSetting(lookUpEdit.ItemIndex);
            }

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
            gvShortOutput.PostEditor();
            DataRow dr = gvShortOutput.GetDataRow(gvShortOutput.FocusedRowHandle);
            dr.EndEdit();
            DataTable dtModify = dtShortOutput.GetChanges(DataRowState.Modified);
            dtShortOutput.AcceptChanges();
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
            for (int i = 0; i < maglev.SaftFlags.Length; i++)
            {
                if (ceSafeSetting.Items[i].CheckState == CheckState.Checked && !maglev.SaftFlags[i])
                    return true;
                if (ceSafeSetting.Items[i].CheckState == CheckState.Unchecked && maglev.SaftFlags[i])
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


        private void FrmShort4_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshSateTimer.Stop(); 
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
            maglev.Short4CtrlObj1.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER1]);
            maglev.Short4CtrlObj2.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER1]);
            maglev.Short4CtrlObj3.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER1]);
            maglev.Short4CtrlObj4.usScOutDly = Convert.ToUInt16(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER1]);

            maglev.Short4CtrlObj1.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER2]);
            maglev.Short4CtrlObj2.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER2]);
            maglev.Short4CtrlObj3.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER2]);
            maglev.Short4CtrlObj4.usScOutTim = Convert.ToUInt16(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER2]);

            maglev.Short4CtrlObj1.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[0][ViewConfig.DC_PARAMETER3]));
            maglev.Short4CtrlObj2.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[1][ViewConfig.DC_PARAMETER3]));
            maglev.Short4CtrlObj3.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[2][ViewConfig.DC_PARAMETER3]));
            maglev.Short4CtrlObj4.ucScOutAct = Convert.ToByte(cbxOutputLevel.Items.IndexOf(dtShortOutput.Rows[3][ViewConfig.DC_PARAMETER3]));

            return maglev.GetAdditionValue();
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
            viewLogicSetting.ReturnLogicData(new CallbackParameter(ActionKind.ReadLogicConfig,this.maglev.DeviceID,logicData));
            //-------附加动作------
            byte[] adittionData = logicQuickSetting.GetLogicAdditionData(cbxQuickSetting.SelectedIndex);
            maglev.SetAdditionLogicData(adittionData);
            this.CallbackUI(new CallbackParameter(ActionKind.ReadAdditionAciton,Radar.CLASS_NAME));//---回调UI---
            //----------手头变更修改状态------
             viewLogicSetting.IsSystemSetting = true;
        }

       

        private void cbxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
 
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            //-----保存配置------
            maglev.ShortConfigRoad1 = (byte)cbxShortOut1.SelectedIndex;
            maglev.ShortConfigRoad2 = (byte)cbxShortOut2.SelectedIndex;
            maglev.ShortConfigRoad3 = (byte)cbxShortOut3.SelectedIndex;
            maglev.ShortConfigRoad4 = (byte)cbxShortOut4.SelectedIndex;
            maglev.ShortConfigRoad5 = (byte)cbxShortOut5.SelectedIndex;
            maglev.ShortConfigRoad6 = (byte)cbxShortOut6.SelectedIndex;
            maglev.ShortConfigRoad7 = (byte)cbxShortOut7.SelectedIndex;
            maglev.ShortConfigRoad8 = (byte)cbxShortOut8.SelectedIndex;
            maglev.SaveConfig();
        }

        private void ceSafeSetting_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {

            if (e.Index == 15)
            {
                if (ceSafeSetting.Items[15].CheckState == CheckState.Checked)
                {
                    for (int i = 0; i < maglev.SaftFlags.Length; i++)
                        ceSafeSetting.Items[i].CheckState = CheckState.Checked;
                }
                if (ceSafeSetting.Items[15].CheckState == CheckState.Unchecked)
                {
                    for (int i = 0; i < maglev.SaftFlags.Length; i++)
                        ceSafeSetting.Items[i].CheckState = CheckState.Unchecked;
                }
            }
        }

        /// <summary>
        /// 手动刷新状态
        /// </summary>
        private void btRefrash_Click(object sender, EventArgs e)
        {
            maglev.ReadState();
            maglev.ReadConfig();
        }

        /// <summary>
        /// 动作切换
        /// </summary>
        private void cbxAction_Changed(object sender, EventArgs e)
        {
            gvShortOutput.PostEditor();
            DataRow dr = gvShortOutput.GetDataRow(gvShortOutput.FocusedRowHandle);
            dr.EndEdit();
            string action = dr[dcLevel.FieldName].ToString();
            if (action == SensorConfig.SCIN_LV_NAME_NONE)
            {
                dr[dcActionTime.FieldName] = 0;
                dr[dcDelayTime.FieldName] = 0;
                dr.EndEdit();
                speSecond.ReadOnly = true;      
            }
            else
            {
                speSecond.ReadOnly = false;
            }

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
   
    }
}
