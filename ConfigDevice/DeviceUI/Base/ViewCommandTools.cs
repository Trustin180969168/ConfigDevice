using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;

namespace ConfigDevice
{
    public partial class ViewCommandTools : UserControl
    {
        public ControlObj currentControlObj;//---控制对象-----
        public ViewCommandControl viewControl;// ----视图控制----
        private DataTable dtCommandSetting;//---指令配置表-----
        public int Num = 1;
        public DeviceData CurrentDevice;//当前设备

        public ViewCommandTools()
        {
            InitializeComponent();

            dtCommandSetting = new DataTable();

            dtCommandSetting.Columns.Add(DeviceConfig.DC_NUM, System.Type.GetType("System.Int16"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_ID, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_NETWORK_ID, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_KIND_ID, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_NAME, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_PC_ADDRESS, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_NETWORK_IP, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_CONTROL_OBJ, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_COMMAND, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_PARAMETER1, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_PARAMETER2, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_PARAMETER3, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_PARAMETER4, System.Type.GetType("System.String"));
            dtCommandSetting.Columns.Add(DeviceConfig.DC_PARAMETER5, System.Type.GetType("System.String"));

            xh.FieldName = DeviceConfig.DC_NUM;
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

            dtCommandSetting.Rows.Add();
            dtCommandSetting.AcceptChanges();
            gcCommands.DataSource = dtCommandSetting;
        }

        /// <summary>
        /// 选择设备
        /// </summary>
        private void gvDevices_DoubleClick(object sender, EventArgs e)
        {
            SelectDevice select = new SelectDevice();
            if (select.ShowDialog() == DialogResult.Yes)
            {
                this.linkEdit_Click(sender, e);//清空
                CurrentDevice = select.ChooseDevice;
                DataRow dr = dtCommandSetting.Rows[0];
                dr[DeviceConfig.DC_NUM] = Num;
                dr[DeviceConfig.DC_ID] = CurrentDevice.DeviceID;
                dr[DeviceConfig.DC_NETWORK_ID] = CurrentDevice.NetworkID;
                dr[DeviceConfig.DC_KIND_NAME] = CurrentDevice.KindName;
                dr[DeviceConfig.DC_NAME] = CurrentDevice.Name;

                dr.EndEdit();
                dtCommandSetting.AcceptChanges();
                gvCommands.BestFitColumns();

                cbxControlObj.Items.Clear();
                foreach(string key in CurrentDevice.ContrlObjs.Keys)
                    cbxControlObj.Items.Add(key);
            }
        }

        /// <summary>
        /// 选择控制对象
        /// </summary>
        private void cbxControlObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (string)cbxControlObj.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            currentControlObj = CurrentDevice.ContrlObjs[name];
            viewControl = SysCtrl.GetViewCommandControl(currentControlObj, gvCommands);

            dtCommandSetting.Rows[0][DeviceConfig.DC_CONTROL_OBJ] = name;
            dtCommandSetting.AcceptChanges();

            gvCommands.BestFitColumns();
            foreach (GridColumn dc in gvCommands.Columns)
                if (dc.VisibleIndex > 5) dc.Width += 15;
        }

        /// <summary>
        /// 清空指令配置
        /// </summary>
        private void linkEdit_Click(object sender, EventArgs e)
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

            dtCommandSetting.AcceptChanges();
            gvCommands.RefreshData();
        }

        /// <summary>
        /// 获取指令数据
        /// </summary>
        /// <returns>CommandData</returns>
        public CommandData GetCommandData()
        {
          return  viewControl.GetCommand();
        }

        private void gvCommands_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvCommands.BestFitColumns();
            foreach (GridColumn dc in gvCommands.Columns)
                if (dc.VisibleIndex > 5) dc.Width += 15;
        }




            
    }
}
