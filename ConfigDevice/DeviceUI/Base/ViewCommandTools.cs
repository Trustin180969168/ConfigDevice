﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace ConfigDevice
{
    public partial class ViewCommandTools : UserControl
    {
        public ControlObj CurrentControlObj;//---控制对象-----
        public ViewCommandControl ViewCommandControlObj;// ----视图控制----
        public DataTable DataCommandSetting;//---指令配置表-----
        public Device CurrentDevice;//当前设备
        public SyncCommandSetting SyncCommandEdit;//---同步编辑----
        public GridControl GridCommandView { get { return this.gcCommands; } }
        private bool allowSync = true;//是否允许同步
        public event DeleteCommandData DelCommandData;//删除命令


        /// <summary>
        /// 序号
        /// </summary>
        public int Num { set { cedtNum.Text = value.ToString(); } get { return Convert.ToInt16(cedtNum.Text); } }
        public bool Checked { set { cedtNum.Checked = value; } get { return cedtNum.Checked; } }
        public bool IsNull //---是否为空-----
        {
            get
            {
                DataRow dr = gvCommands.GetDataRow(0); 
                if (dr[DeviceConfig.DC_ID].ToString() == "") return true;
                else
                    return false;
            }
        }
        public bool HasChanged  //------是否执行了更改------
        {
            get
            {
                gvCommands.PostEditor();
                DataRow dr = gvCommands.GetDataRow(0);
                dr.EndEdit();
                DataTable dt = DataCommandSetting.GetChanges(DataRowState.Modified);
                if (dt !=null && dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public ViewCommandTools()
        {
            InitializeComponent();

            DataCommandSetting = new DataTable();

            DataCommandSetting.Columns.Add(DeviceConfig.DC_NUM, System.Type.GetType("System.Int16"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_ID, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_NETWORK_ID, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_KIND_ID, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_NAME, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_PC_ADDRESS, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_NETWORK_IP, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_CONTROL_OBJ, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_COMMAND, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_PARAMETER1, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_PARAMETER2, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_PARAMETER3, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_PARAMETER4, System.Type.GetType("System.String"));
            DataCommandSetting.Columns.Add(DeviceConfig.DC_PARAMETER5, System.Type.GetType("System.String"));

            deviceID.FieldName = DeviceConfig.DC_ID;
            deviceNetworkID.FieldName = DeviceConfig.DC_NETWORK_ID;
            deviceKind.FieldName = DeviceConfig.DC_KIND_NAME;
            deviceName.FieldName = DeviceConfig.DC_NAME;
            deviceCtrlObj.FieldName = DeviceConfig.DC_CONTROL_OBJ;
            command.FieldName = DeviceConfig.DC_COMMAND;
            parameter1.FieldName = DeviceConfig.DC_PARAMETER1;
            parameter2.FieldName = DeviceConfig.DC_PARAMETER2;
            parameter3.FieldName = DeviceConfig.DC_PARAMETER3;
            parameter4.FieldName = DeviceConfig.DC_PARAMETER4;
            parameter5.FieldName = DeviceConfig.DC_PARAMETER5;

            DataCommandSetting.Rows.Add();
            gcCommands.DataSource = DataCommandSetting;

        }
        public ViewCommandTools(int num)
            : this()
        {
            this.Num = num;
        }
        /// <summary>
        /// 选择设备
        /// </summary>
        private void gvDevices_DoubleClick(object sender, EventArgs e)
        {
            SelectDevice select = new SelectDevice();
            if (select.ShowDialog() == DialogResult.Yes)
            {
                CleanCommandSetting();
                CurrentDevice = select.ChooseDevice;
                DataRow dr = DataCommandSetting.Rows[0];
                dr[DeviceConfig.DC_NUM] = cedtNum.Text;
                dr[DeviceConfig.DC_ID] = CurrentDevice.DeviceID;
                dr[DeviceConfig.DC_NETWORK_ID] = CurrentDevice.NetworkID;
                dr[DeviceConfig.DC_KIND_NAME] = CurrentDevice.KindName;
                dr[DeviceConfig.DC_NAME] = CurrentDevice.Name;
                dr.EndEdit();
                gvCommands.BestFitColumns();

                cbxControlObj.Items.Clear();
                foreach (string key in CurrentDevice.ContrlObjs.Keys)
                    cbxControlObj.Items.Add(key);

                //-------默认第一个控制对象,涉及多个值的变动,采取手动同步------
                allowSync = false;
                DataCommandSetting.Rows[0][DeviceConfig.DC_CONTROL_OBJ] = cbxControlObj.Items[0].ToString();
                CurrentControlObj = CurrentDevice.ContrlObjs[cbxControlObj.Items[0].ToString()];
                ViewCommandControlObj = SysCtrl.GetViewCommandControl(CurrentControlObj, gvCommands);
                refreshView();
                allowSync = true;
                SyncCommandSetting();
                

            }
        }

        /// <summary>
        /// 选择控制对象
        /// </summary>
        private void cbxControlObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            //-------默认第一个控制对象,涉及多个值的变动,采取手动同步------
            allowSync = false;
            string name = (string)cbxControlObj.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            CurrentControlObj = CurrentDevice.ContrlObjs[name];
            ViewCommandControlObj = SysCtrl.GetViewCommandControl(CurrentControlObj, gvCommands);
            DataCommandSetting.Rows[0][DeviceConfig.DC_CONTROL_OBJ] = name;
            SyncCommandSetting();
            refreshView();
            allowSync = true;
            SyncCommandSetting();

        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        private void refreshView()
        {
            //gvCommands.PostEditor();取消自适应宽度
            //gvCommands.BestFitColumns();
            //gvCommands.RefreshData();
            //foreach (GridColumn dc in gvCommands.Columns)
            //    if (dc.VisibleIndex > 3) dc.Width += (int)(dc.Width * 0.5);
            if (this.ViewCommandControlObj is ViewServerControl)
                parameter1.Width = 400;
            else
                parameter1.Width = 80;
        }

        /// <summary>
        /// 清空指令配置
        /// </summary>
        private void linkEdit_Click(object sender, EventArgs e)
        {
            if (CommonTools.MessageShow("是否删除第" + this.Num.ToString() + "指令?", 4, "") == DialogResult.No)
                return;
            DelCommandData(this.Num-1);
            CleanCommandSetting();//清空界面
            //SyncCommandSetting();----删除取消同步----
        }

        /// <summary>
        /// 清空指令配置
        /// </summary>
        public void CleanCommandSetting()
        {
            DataRow dr = gvCommands.GetDataRow(0);
            dr.Delete();
            DataCommandSetting.Rows.Add();
            
            parameter1.Visible = false;
            parameter2.Visible = false;
            parameter3.Visible = false;
            parameter4.Visible = false;
            parameter5.Visible = false;
            cbxControlObj.Items.Clear();
            command.Visible = false;

            DataCommandSetting.AcceptChanges();
            gvCommands.RefreshData();
        }

        /// <summary>
        /// 获取指令数据
        /// </summary>
        /// <returns>CommandData</returns>
        public CommandData GetCommandData()
        {
            try
            {
                return ViewCommandControlObj.GetCommand();
            }
            catch { return null; }
        }

        /// <summary>
        /// 改变值后,自动同步
        /// </summary>
        private void gvCommands_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            refreshView();
            SyncCommandSetting();
        }

        /// <summary>
        /// 执行同步
        /// </summary>
        private void SyncCommandSetting()
        {
            gvCommands.PostEditor();
            gvCommands.GetDataRow(0).EndEdit();
            if (SyncCommandEdit != null && this.Checked && allowSync)
                SyncCommandEdit(this);
        }

        /// <summary>
        /// 同步本地编辑
        /// </summary>
        /// <param name="value"></param>
        public void SyncCommandSettingEdit(ViewCommandTools value)
        {
            string valueCtrlObjName = value.DataCommandSetting.Rows[0][DeviceConfig.DC_CONTROL_OBJ].ToString();//空为删除操作
            string myCtrlObjName = this.DataCommandSetting.Rows[0][DeviceConfig.DC_CONTROL_OBJ].ToString();//空为删除操作
            if (this.CurrentDevice == null || this.CurrentDevice.KindID != value.CurrentDevice.KindID)
            {
                this.CurrentDevice = SysCtrl.CreateDevice(value.CurrentDevice.ByteKindID).CreateDevice(value.CurrentDevice.GetDeviceData());
                this.CleanCommandSetting();//----清空----
                cbxControlObj.Items.Clear();
                foreach (string key in CurrentDevice.ContrlObjs.Keys)
                    cbxControlObj.Items.Add(key);
                if (valueCtrlObjName == "") return;     //-----------不同设备,初始化后退出------------------
            }
            else if (valueCtrlObjName == "")
            { this.CleanCommandSetting(); return; }//------相同设备且为删除操作,执行删除并退出-------
            //---------控制对象为空,或者不同,则创建------
            if (this.CurrentControlObj == null || this.CurrentControlObj.Name != value.CurrentControlObj.Name)
            {
                CurrentControlObj = CurrentDevice.ContrlObjs[value.CurrentControlObj.Name];
                ViewCommandControlObj = SysCtrl.GetViewCommandControl(CurrentControlObj, gvCommands);
            }//--------本地删除后,需要重新初始化----
            else if (this.CurrentControlObj.Name == value.CurrentControlObj.Name && myCtrlObjName == "")
            {
                CurrentControlObj = CurrentDevice.ContrlObjs[value.CurrentControlObj.Name];
                ViewCommandControlObj = SysCtrl.GetViewCommandControl(CurrentControlObj, gvCommands);
            }

            this.DataCommandSetting = value.DataCommandSetting.Copy();
            gcCommands.DataSource = this.DataCommandSetting;
            refreshView();
        }

        /// <summary>
        /// 设置命令行
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="data">命令</param>
        /// <returns></returns>
        public void SetCommandData(CommandData data)
        {
            allowSync = false;

            DataRow dr = gvCommands.GetDataRow(0);
            dr[DeviceConfig.DC_ID] = (int)data.TargetId;//-----设备ID---
            dr[DeviceConfig.DC_NETWORK_ID] = (int)data.TargetNet;//---网络ID---
            dr[DeviceConfig.DC_KIND_ID] = (int)data.TargetType;//---设备ID----
            dr[DeviceConfig.DC_KIND_NAME] = DeviceConfig.EQUIPMENT_ID_NAME[data.TargetType];//---设备类型----
            dr[DeviceConfig.DC_NAME] = this.getDeviceName(((int)data.TargetId).ToString(), ((int)data.TargetNet).ToString());//---名称-----
            dr.EndEdit();
            this.CurrentDevice = SysCtrl.CreateDevice(data.TargetType).CreateDevice(new DeviceData(dr));//----获取当前设备对象---

            //-----获取控制对象---------------
            cbxControlObj.Items.Clear();
            foreach (string key in CurrentDevice.ContrlObjs.Keys)
                cbxControlObj.Items.Add(key);
            string objName = this.getControlObj(data);
            if (objName == "无效") return;
            if (!CurrentDevice.ContrlObjs.ContainsKey(objName)) return;
            dr[DeviceConfig.DC_CONTROL_OBJ] = objName;//----判断设备是否含有相应的控制对象
            dr.EndEdit();
            CurrentControlObj = CurrentDevice.ContrlObjs[objName];//----获取当前控制对象
            ViewCommandControlObj = SysCtrl.GetViewCommandControl(CurrentControlObj, gvCommands);//----创建视图控制对象
            ViewCommandControlObj.SetCommandData(data);//-----设置命令内容----

            refreshView();
            DataCommandSetting.AcceptChanges();
            allowSync = true;
        }

        /// <summary>
        /// 获取被控制的设备名称
        /// </summary>
        /// <param name="deviceID">设备ID</param>
        /// <param name="networkID">网络ID</param>
        /// <returns>设备名称</returns>
        private string getDeviceName(string deviceID, string networkID)
        {
            string temp = DeviceConfig.DC_ID + " = '" + deviceID + "' and " + DeviceConfig.DC_NETWORK_ID + "= '" + networkID + "'";
            DataRow[] rows = SysConfig.DtDevice.Select(temp);
            if (rows.Length == 0) return "未知设备";
            return rows[0][DeviceConfig.DC_NAME].ToString();
        }

        /// <summary>
        /// 获取控制对象
        /// </summary>
        /// <param name="cmd">指令</param>
        /// <returns></returns>
        private string getControlObj(CommandData cmdData)
        {
            byte kind = cmdData.TargetType;
            switch (kind)
            {
                case DeviceConfig.EQUIPMENT_CURTAIN_3CH:
                case DeviceConfig.EQUIPMENT_CURTAIN_2CH: return "电机";
                case DeviceConfig.EQUIPMENT_SWIT_4:
                case DeviceConfig.EQUIPMENT_SWIT_6:
                case DeviceConfig.EQUIPMENT_SWIT_8:
                case DeviceConfig.EQUIPMENT_SWIT_12:
                    {
                        if (CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP_NOT) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN_CONDITION) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE_CONDITION) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP) )
                            return "回路";
                        if (CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_SCENE) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_SCENE_OPEN) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_SCENE_CLOSE) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_SCENE_NOT) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP))
                            return "场景";
                        if (CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LIST) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LIST_OPEN) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LIST_CLOSE) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LIST_NOT) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP) )
                            return "时序";
                        if (CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_ALL) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_ALL_OPEN) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_ALL_CLOSE) )
                            return "全部";
                    } break;
                case DeviceConfig.EQUIPMENT_AMP_MP3:
                    {
                        if (cmdData.Data[2] == (byte)AudioKind.GENERAL_BGM)
                            return "背景";
                        if (cmdData.Data[2] == (byte)AudioKind.TG_MESSAGE)
                            return "消息";
                    } break;
                case DeviceConfig.EQUIPMENT_SERVER: return "服务器";
                default: return "无效";
            }
            return "无效";

        }

 

    }
}