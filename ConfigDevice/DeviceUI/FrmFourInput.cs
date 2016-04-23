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
    public partial class FrmFourInput : FrmDevice
    {
        private DoorInput4 doorInput4;
        public FrmFourInput(DeviceData _device)
            : base(_device)
        {          
            InitializeComponent();

            doorInput4 = this.Device as DoorInput4;
            doorInput4.OnCallbackUI_Action += this.callbackUI;
            doorInput4.OnCallbackUI_Action += frmSetting.CallBackUI;
            frmSetting.DeviceEdit = doorInput4;
        }   

        private void FrmFourInput_Load(object sender, EventArgs e)
        {    
            base.InitSelectDevice();//初始化选择列表
            tctrlEdit.SelectedTabPageIndex = 0;
            loadData();
        }

        private void loadData()
        {
            doorInput4.SearchVer();//---获取版本号-----   
            doorInput4.ReadSettingInfo();//----读取配置信息-----
            doorInput4.ReadRoadTitle();//---读取回路名称----
        }

        /// <summary>
        /// 回调
        /// </summary>
        private void callbackUI(object[] values)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new CallbackUIAction(callbackUI),new object[]{values});
                    return;
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                        clbcAqjb.Items[i].CheckState = doorInput4.SecurityLevelValue[i] == true ? CheckState.Checked : CheckState.Unchecked;
                    for (int i = 0; i < 4; i++)
                        clbcWldkpb.Items[i].CheckState = doorInput4.PhysicalShieldingPortsValue[i] == true ?  CheckState.Checked : CheckState.Unchecked;
                    
                    edtMcmc1.Text = doorInput4.RoadTitle1;
                    edtMcmc2.Text = doorInput4.RoadTitle2;
                    edtMcmc3.Text = doorInput4.RoadTitle3;
                    edtMcmc4.Text = doorInput4.RoadTitle4;

                    cedtAfpb1.Checked = doorInput4.RoadShield1;
                    cedtAfpb2.Checked = doorInput4.RoadShield2;
                    cedtAfpb3.Checked = doorInput4.RoadShield3;
                    cedtAfpb4.Checked = doorInput4.RoadShield4;

                    edtNum1.Text = doorInput4.RoadMusicNum1.ToString();
                    edtNum2.Text = doorInput4.RoadMusicNum2.ToString();
                    edtNum3.Text = doorInput4.RoadMusicNum3.ToString();
                    edtNum4.Text = doorInput4.RoadMusicNum4.ToString();

                    if (clbcAqjb.SelectedItems.Count == 15)
                        cdtSelectAll.Checked = true; 
                }
            }
            catch { }
        }

        /// <summary>
        /// 选择事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoorInput4 _doorInput4 = new DoorInput4(SelectDeviceList[CbxSelectDevice.SelectedIndex]);
            if (doorInput4.MAC == _doorInput4.MAC) return;

            _doorInput4.OnCallbackUI_Action += this.callbackUI;
            _doorInput4.OnCallbackUI_Action += frmSetting.CallBackUI;
            frmSetting.DeviceEdit = _doorInput4;
            doorInput4 = _doorInput4;
            this.Text = _doorInput4.Name;
            loadData();
        }

        /// <summary>
        /// 全选,全不选
        /// </summary>
        private void cdtSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (CheckedListBoxItem item in clbcAqjb.Items)
                item.CheckState = cdtSelectAll.CheckState;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void btRefresh_Click(object sender, EventArgs e)
        {
            doorInput4.RefreshData();
        }


    }
}
