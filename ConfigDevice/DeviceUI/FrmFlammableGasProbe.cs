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

        public FrmFlammableGasProbe(Device _device)
            : base(_device)
        { 
            InitializeComponent();
            //-----------初始化编辑控件-------
            edtT.Properties.DisplayFormat.FormatString = "#0.0 ℃";
            edtT.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            edtT.Properties.Mask.EditMask = "#0.0 ℃";
            edtT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edtT.Properties.Mask.UseMaskAsDisplayFormat = true;

            edtEC.Properties.DisplayFormat.FormatString = "#0 mA";
            edtEC.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            edtEC.Properties.Mask.EditMask = "#0 mA";
            edtEC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edtEC.Properties.Mask.UseMaskAsDisplayFormat = true;
            speProbeEC.Enter += SysConfig.Edit_Enter;
            speProbeEC.Leave += SysConfig.Edit_Leave;
            spePreTime.Enter += SysConfig.Edit_Enter;
            spePreTime.Leave += SysConfig.Edit_Leave;
        
            //-------------回路查询选择------
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_ID, "回路", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None));
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_NAME,380));
   
            lookUpEdit.Properties.Name = "lookupEdit";
            lookUpEdit.Properties.DisplayMember = ViewConfig.DC_NAME;
            lookUpEdit.Properties.ValueMember = ViewConfig.DC_ID;
            lookUpEdit.Properties.ShowFooter = false;
            lookUpEdit.Properties.ShowHeader = false;
            lookUpEdit.Properties.DataSource = dtIDName;
            //-------------可燃气体回调-----------
            flammableGasProbe = this.Device as FlammableGasProbe;
            flammableGasProbe.OnCallbackUI_Action += this.callbackUI; 
            flammableGasProbe.OnCallbackUI_Action += frmSetting.CallBackUI; 
            frmSetting.DeviceEdit = flammableGasProbe;           //---基础配置编辑
            refreshSateTimer = new ThreadActionTimer(2000, new Action(flammableGasProbe.ReadState));//---自动刷新----
            viewCommandEdit.CbxCommandGroup.Visible = false;//----取消命令组显示------
            viewCommandEdit.CommandGroupName = "";//----取消组名显示--------
        }

        /// <summary>
        /// 回调
        /// </summary>
        private void callbackUI(CallbackParameter callbackParameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(callbackUI), callbackParameter);
                return;
            }
            //-----刷新内容-------
            edtValveState.Text = flammableGasProbe.ValveState;
            edtEC.Text = flammableGasProbe.ElectricCurrent.ToString();
            edtGasProbe.Text = flammableGasProbe.ProbeState;
            edtT.Text = flammableGasProbe.Templatetrue.ToString();

            speProbeEC.Text = flammableGasProbe.Valve.MaxStopCE.ToString();
            spePreTime.Text = flammableGasProbe.Valve.MaxRunTime.ToString();

            chkOpenValve.Checked = flammableGasProbe.OpenValve;
            chkClearLight.Checked = flammableGasProbe.ClearLight;
            chkClearLoudly.Checked = flammableGasProbe.ClearBuzzer;

            if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Circuit.CLASS_NAME)
            {
                dtIDName.Rows.Clear();
                viewCommandEdit.CbxCommandGroup.Properties.Items.Clear();
                foreach (int key in flammableGasProbe.ProbeCircuit.ListCircuitIDAndName.Keys)
                {
                   // viewCommandEdit.CbxCommandGroup.Properties.Items.Add(flammableGasProbe.ProbeCircuit.ListCircuitIDAndName[key]);
                    viewCommandEdit.CommmandGroups.Add(flammableGasProbe.ProbeCircuit.ListCircuitIDAndName[key]);
                    dtIDName.Rows.Add(new object[] { key, flammableGasProbe.ProbeCircuit.ListCircuitIDAndName[key] });
                }              
                lookUpEdit.Properties.DataSource = dtIDName;
                flammableGasProbe.Valve.ReadParameter();//---读取参数----
                //    dtIDName.Rows.Add(new object[] { key, "久久久久久久久久久久久久久久久久久久久久久久久久久久久久久久" });//----用于调试列宽---
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
                CommonTools.MessageShow("退出自动刷新!", 1, "");
            }
        }

        private void FrmFlammableGasProbe_Load(object sender, EventArgs e)
        {
            base.InitSelectDevice();//初始化选择列表     
            loadData();             
        }

        private void loadData()
        {
            flammableGasProbe.SearchVer();//---获取版本号-----   
            flammableGasProbe.ReadState();//---读取状态----     
            flammableGasProbe.ProbeCircuit.ReadRoadTitle();//----读取回路----(回路跟参数读取命令冲突需要回调后读取)
        }

        /// <summary>
        /// 开阀门
        /// </summary> 
        private void btOpenValve_Click(object sender, EventArgs e)
        {
            flammableGasProbe.Valve.MotorAction(Valve.ACTION_ROAD_FRONT_1, Valve.ACTION_RUN);
        }

        /// <summary>
        /// 关阀门
        /// </summary> 
        private void btCloseValve_Click(object sender, EventArgs e)
        {
            flammableGasProbe.Valve.MotorAction(Valve.ACTION_ROAD_BACK_1, Valve.ACTION_RUN);
        }
        /// <summary>
        /// 停止阀门
        /// </summary> 
        private void btStopValve_Click(object sender, EventArgs e)
        {
            flammableGasProbe.Valve.MotorAction(Valve.ACTION_ROAD_BACK_1, Valve.ACTION_STOP);
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

        private void tctrlEdit_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tctrlEdit.SelectedTabPageIndex == 2)
            {
                if (viewLogicSetting.NeedInit)
                {                    
                    viewLogicSetting.InitLogicList(ViewConfig.SENSOR_FLAMMABLE_GAS_PROBE, ViewConfig.SENSOR_TEMPERATURE,
                          ViewConfig.SENSOR_SYSTEM_INTERACTION
                        //------以下是界面测试,非本设备的触发对象选择-----
                        //,ViewConfig.SENSOR_HUMIDITY,ViewConfig.SENSOR_RADAR,ViewConfig.SENSOR_SWIT_TAMPER,
                        //ViewConfig.SENSOR_TIME,ViewConfig.SENSOR_DATE,ViewConfig.SENSOR_WEEK,ViewConfig.SENSOR_WINDY
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
        /// 选择回路
        /// </summary>
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            lblNum.Text = lookUpEdit.EditValue.ToString()+"、";
            edtTriggerActionName.Text = lookUpEdit.Text;
            viewCommandEdit.CbxCommandGroup.SelectedIndex = lookUpEdit.ItemIndex;
        }

        /// <summary>
        /// 双击选择
        /// </summary>
        private void edtTriggerActionName_DoubleClick(object sender, EventArgs e)
        {
            lookUpEdit.ShowPopup();
        }

        /// <summary>
        /// 保存回路
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSaveTrigger_Click(object sender, EventArgs e)
        {
            flammableGasProbe.ProbeCircuit.SaveRoadSetting(Convert.ToInt16(lookUpEdit.EditValue) - 1, edtTriggerActionName.Text);
        }
    }
}
