using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmFlammableGasProbe : ConfigDevice.FrmDevice
    {
        private ThreadActionTimer refreshSateTimer;//---动态刷新---
        private bool autoRefresh = false;
        private FlammableGasProbe flammableGasProbe;
        public FrmFlammableGasProbe(Device _device)
            : base(_device)
        { 
            InitializeComponent();
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

            viewCommandEdit.CbxCommandGroup.Visible = false;

            flammableGasProbe = this.Device as FlammableGasProbe;
            flammableGasProbe.OnCallbackUI_Action += this.callbackUI;
            flammableGasProbe.OnCallbackUI_Action += frmSetting.CallBackUI; 

            frmSetting.DeviceEdit = flammableGasProbe;


            refreshSateTimer = new ThreadActionTimer(2000, new Action(flammableGasProbe.ReadState));//---自动刷新----
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
            viewCommandEdit.CommandGroupName = "当前区域";
            loadData(); 
        }

        private void loadData()
        {
            flammableGasProbe.SearchVer();//---获取版本号-----   
            flammableGasProbe.ReadState();//---读取状态----     
            flammableGasProbe.Valve.ReadParameter();//---读取参数----
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
                if (!viewLogicSetting.Inited)
                    viewLogicSetting.InitLogicList(ViewConfig.TRIGGER_INVALID, ViewConfig.TRIGGER_FLAMMABLE_GAS_PROBE,
                        ViewConfig.TRIGGER_TEMPERATURE, ViewConfig.TRIGGER_SYSTEM_INTERACTION);
        }
    }
}
