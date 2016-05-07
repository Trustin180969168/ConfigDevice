using System;
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

       
        /// <summary>
        /// 序号
        /// </summary>
        public int Num { set { cedtNum.Text = value.ToString(); } get { return Convert.ToInt16( cedtNum.Text); } }
        public bool Checked { set { cedtNum.Checked = value; } get { return cedtNum.Checked; } }        

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
            DataCommandSetting.AcceptChanges();
            gcCommands.DataSource = DataCommandSetting;

        }
        public ViewCommandTools(int num):this()
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
                DelCommandSetting();
                CurrentDevice = select.ChooseDevice;
                DataRow dr = DataCommandSetting.Rows[0];
                dr[DeviceConfig.DC_NUM] = cedtNum.Text;
                dr[DeviceConfig.DC_ID] = CurrentDevice.DeviceID;
                dr[DeviceConfig.DC_NETWORK_ID] = CurrentDevice.NetworkID;
                dr[DeviceConfig.DC_KIND_NAME] = CurrentDevice.KindName;
                dr[DeviceConfig.DC_NAME] = CurrentDevice.Name;
                dr.EndEdit();
                DataCommandSetting.AcceptChanges();
                gvCommands.BestFitColumns();

                cbxControlObj.Items.Clear();
                foreach(string key in CurrentDevice.ContrlObjs.Keys)
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
            DataCommandSetting.AcceptChanges();
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
            gvCommands.PostEditor();
            gvCommands.BestFitColumns();
            DataCommandSetting.AcceptChanges();
            foreach (GridColumn dc in gvCommands.Columns)
                if (dc.VisibleIndex > 5) dc.Width += 15;
        }

        /// <summary>
        /// 清空指令配置
        /// </summary>
        private void linkEdit_Click(object sender, EventArgs e)
        {
            DelCommandSetting();
            SyncCommandSetting();
        }
        /// <summary>
        /// 清空指令配置
        /// </summary>
        private void DelCommandSetting()
        {
            DataRow dr = gvCommands.GetDataRow(0);

            dr[DeviceConfig.DC_CONTROL_OBJ] = "";
            dr[DeviceConfig.DC_COMMAND] = "";
            dr[DeviceConfig.DC_PARAMETER1] = "";
            dr[DeviceConfig.DC_PARAMETER2] = "";
            dr[DeviceConfig.DC_PARAMETER3] = "";
            dr[DeviceConfig.DC_PARAMETER4] = "";
            dr[DeviceConfig.DC_PARAMETER5] = "";

            parameter1.Visible = false;
            parameter2.Visible = false;
            parameter3.Visible = false;
            parameter4.Visible = false;
            parameter5.Visible = false;

            DataCommandSetting.AcceptChanges();
            gvCommands.RefreshData();
        }

        /// <summary>
        /// 获取指令数据
        /// </summary>
        /// <returns>CommandData</returns>
        public CommandData GetCommandData()
        {
          return  ViewCommandControlObj.GetCommand();
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
            DataCommandSetting.AcceptChanges();
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
                this.CurrentDevice = SysCtrl.CreateDevice(value.CurrentDevice.ByteKindID).CreateDevice(value.CurrentDevice.GetDeviceInfo());                               
                this.DelCommandSetting();//----清空----
                cbxControlObj.Items.Clear();
                foreach (string key in CurrentDevice.ContrlObjs.Keys)
                    cbxControlObj.Items.Add(key);
                if (valueCtrlObjName == "") return;     //-----------不同设备,初始化后退出------------------
            }
            else if (valueCtrlObjName == "")
            { this.DelCommandSetting(); return; }//------相同设备且为删除操作,执行删除并退出-------
            //---------控制对象为空,或者不同,则创建
            if (this.CurrentControlObj == null || this.CurrentControlObj.Name != value.CurrentControlObj.Name )
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


            
    }
}
