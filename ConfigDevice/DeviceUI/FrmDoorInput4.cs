﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;

namespace ConfigDevice
{
    public partial class FrmDoorInput4 : FrmDevice
    {
        private DoorInput4 doorInput4;
        public FrmDoorInput4(Device _device)
            : base(_device)
        {
            InitializeComponent();           

            doorInput4 = this.Device as DoorInput4;
            doorInput4.OnCallbackUI_Action += this.callbackUI;
            doorInput4.OnCallbackUI_Action += frmSetting.CallBackUI;
            doorInput4.OnCallbackRoad_Action += this.callbackRoadName;       

            frmSetting.DeviceEdit = doorInput4;
  
        }

        private void FrmFourInput_Load(object sender, EventArgs e)
        {
            base.InitSelectDevice();//初始化选择列表
            loadData();
            doorInput4.ReadRoadTitle();//---读取回路名称----     
            viewCommandEdit.CommandGroupName = "当前区域";
        }

        private void loadData()
        {
            doorInput4.SearchVer();//---获取版本号-----   

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
                    this.Invoke(new CallbackUIAction(callbackUI), new object[] { values });
                    return;
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                        clbcAqjb.Items[i].CheckState = doorInput4.SecurityLevelValue[i] == true ? CheckState.Checked : CheckState.Unchecked;
                    for (int i = 0; i < 4; i++)
                        clbcWldkpb.Items[i].CheckState = doorInput4.PhysicalShieldingPortsValue[i] == true ? CheckState.Checked : CheckState.Unchecked;
                    
                    cedtAfpb1.Checked = doorInput4.RoadShield1;
                    cedtAfpb2.Checked = doorInput4.RoadShield2;
                    cedtAfpb3.Checked = doorInput4.RoadShield3;
                    cedtAfpb4.Checked = doorInput4.RoadShield4;

                    edtNum1.Text = doorInput4.RoadMusicNum1.ToString();
                    edtNum2.Text = doorInput4.RoadMusicNum2.ToString();
                    edtNum3.Text = doorInput4.RoadMusicNum3.ToString();
                    edtNum4.Text = doorInput4.RoadMusicNum4.ToString();
                    if (clbcAqjb.CheckedItems.Count == 15)
                        cdtSelectAll.Checked = true;

                }
            }
            catch { }
        }

        /// <summary>
        /// 获取回路名称
        /// </summary>
        /// <param name="values"></param>
        private void callbackRoadName(object[] values)

        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(callbackRoadName), new object[] { values });
                return;
            }
            //----------刷新命令组-----------------
            edtMcmc1.Text = doorInput4.RoadTitle1;
            edtMcmc2.Text = doorInput4.RoadTitle2;
            edtMcmc3.Text = doorInput4.RoadTitle3;
            edtMcmc4.Text = doorInput4.RoadTitle4;

            viewCommandEdit.CommmandGroups.Clear();
            viewCommandEdit.CommmandGroups.Add("第1路:门窗开-" + edtMcmc1.Text);
            viewCommandEdit.CommmandGroups.Add("第1路:门窗关-" + edtMcmc1.Text);
            viewCommandEdit.CommmandGroups.Add("第1路:警报-" + edtMcmc1.Text);
            viewCommandEdit.CommmandGroups.Add("第1路:撤防-" + edtMcmc1.Text);
            viewCommandEdit.CommmandGroups.Add("第2路:门窗开-" + edtMcmc2.Text);
            viewCommandEdit.CommmandGroups.Add("第2路:门窗关-" + edtMcmc2.Text);
            viewCommandEdit.CommmandGroups.Add("第2路:警报-" + edtMcmc2.Text);
            viewCommandEdit.CommmandGroups.Add("第2路:撤防-" + edtMcmc2.Text);
            viewCommandEdit.CommmandGroups.Add("第3路:门窗开-" + edtMcmc3.Text);
            viewCommandEdit.CommmandGroups.Add("第3路:门窗关-" + edtMcmc3.Text);
            viewCommandEdit.CommmandGroups.Add("第3路:警报-" + edtMcmc3.Text);
            viewCommandEdit.CommmandGroups.Add("第3路:撤防-" + edtMcmc3.Text);
            viewCommandEdit.CommmandGroups.Add("第4路:门窗开-" + edtMcmc4.Text);
            viewCommandEdit.CommmandGroups.Add("第4路:门窗关-" + edtMcmc4.Text);
            viewCommandEdit.CommmandGroups.Add("第4路:警报-" + edtMcmc4.Text);
            viewCommandEdit.CommmandGroups.Add("第4路:撤防-" + edtMcmc4.Text);
            if (doorInput4.FinishReadRoads)
            {
                if (viewCommandEdit.NeedInit && tctrlEdit.SelectedTabPageIndex == 2)
                    viewCommandEdit.InitViewCommand(doorInput4);
                else if (!viewCommandEdit.NeedInit)
                    viewCommandEdit.UpdateGroupName();
            }
        }

        /// <summary>
        /// 更换设备事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {           
            DoorInput4 _doorInput4 = new DoorInput4(SelectDeviceList[CbxSelectDevice.SelectedIndex]);
            if (doorInput4.MAC == _doorInput4.MAC) return;
            
            _doorInput4.OnCallbackUI_Action += this.callbackUI;
            _doorInput4.OnCallbackUI_Action += frmSetting.CallBackUI;
            _doorInput4.OnCallbackRoad_Action += this.callbackRoadName;
            frmSetting.DeviceEdit = _doorInput4;
            viewCommandEdit.NeedInit = true;//----重新初始化,通过回调实现------
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
            loadData();
            doorInput4.ReadRoadTitle();//---读取回路名称----     
 
        }

        /// <summary>
        /// 同步修改结果
        /// </summary>
        private void updateDoorInput4Data()
        {
            for (int i = 0; i < 15; i++)
                doorInput4.SecurityLevelValue[i] = clbcAqjb.Items[i].CheckState == CheckState.Checked ? true : false;
            for (int i = 0; i < 4; i++)
                doorInput4.PhysicalShieldingPortsValue[i] = clbcWldkpb.Items[i].CheckState == CheckState.Checked ? true : false;
            doorInput4.RoadShield1 = cedtAfpb1.Checked;
            doorInput4.RoadShield2 = cedtAfpb2.Checked;
            doorInput4.RoadShield3 = cedtAfpb3.Checked;
            doorInput4.RoadShield4 = cedtAfpb4.Checked;
            doorInput4.RoadMusicNum1 = Convert.ToInt16(edtNum1.Text);
            doorInput4.RoadMusicNum2 = Convert.ToInt16(edtNum2.Text);
            doorInput4.RoadMusicNum3 = Convert.ToInt16(edtNum3.Text);
            doorInput4.RoadMusicNum4 = Convert.ToInt16(edtNum4.Text);
            doorInput4.RoadTitle1 = edtMcmc1.Text;
            doorInput4.RoadTitle2 = edtMcmc2.Text;
            doorInput4.RoadTitle3 = edtMcmc3.Text;
            doorInput4.RoadTitle4 = edtMcmc4.Text;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            updateDoorInput4Data();//同步数据结果
            doorInput4.SaveSetting();//保存门输入4
            doorInput4.SaveRoadSetting();//保存回路配置
            callbackRoadName(null);//不管是否保存成功,同步到选择表;
        }

        private void edtMcmc_Leave(object sender, EventArgs e)
        {
            TextEdit edtName = sender as TextEdit;
            string newName = edtName.Text;
            byte[] byteName = Encoding.GetEncoding("GB2312").GetBytes(newName);
            if (byteName.Length > 30)
            { CommonTools.MessageShow("设备名称不能大于30字节!", 3, ""); edtName.Focus(); }
        }

        /// <summary>
        /// 加载指令列表
        /// </summary>
        private void tctrlEdit_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tctrlEdit.SelectedTabPageIndex == 2)
            {
                if (viewCommandEdit.NeedInit)
                    viewCommandEdit.InitViewCommand(doorInput4);//初始化
            }
            else if (tctrlEdit.SelectedTabPageIndex == 1)
            {
                doorInput4.ReadSettingInfo();//----读取配置信息-----
                  
            }
        }




    }
}