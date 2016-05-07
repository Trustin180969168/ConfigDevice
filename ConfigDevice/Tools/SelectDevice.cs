using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class SelectDevice : Form
    {
        public Device ChooseDevice;//设备选择

        public SelectDevice()
        {
            InitializeComponent();

            deviceID.FieldName = DeviceConfig.DC_ID;
            deviceNetworkID.FieldName = DeviceConfig.DC_NETWORK_ID;
            deviceKind.FieldName = DeviceConfig.DC_KIND_NAME;
            deviceName.FieldName = DeviceConfig.DC_NAME;
            deviceMac.FieldName = DeviceConfig.DC_MAC;
            deviceState.FieldName = DeviceConfig.DC_STATE;
            deviceRemark.FieldName = DeviceConfig.DC_REMARK;
        }

        /// <summary>
        /// 筛选出能进行指令配置操作的设备
        /// </summary>
        /// <returns></returns>
        private string chooseCondition()
        {
            string temp = DeviceConfig.DC_KIND_ID + " in (" +
                          "'" + (int)DeviceConfig.EQUIPMENT_AMP_MP3 + "'," +
                          "'" + (int)DeviceConfig.EQUIPMENT_CURTAIN_3CH + "'," +
                          "'" + (int)DeviceConfig.EQUIPMENT_SWIT_4 + "'," +
                          "'" + (int)DeviceConfig.EQUIPMENT_SWIT_6 + "'," +
                          "'" + (int)DeviceConfig.EQUIPMENT_SWIT_8 + "'," +
                          "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_2 + "'," +
                          "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_4 + "'," +
                          "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_6 + "'," +
                          "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_8 + "'," +
                          "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_12 + "'," +
                          "'" + (int)DeviceConfig.EQUIPMENT_SERVER + "'" +
                          ")";
            return temp;
        }

        /// <summary>
        /// 双击选择设备
        /// </summary>
        private void gvDevices_DoubleClick(object sender, EventArgs e)
        {
            if (gvDevices.FocusedRowHandle == -1) return;
            DataRow dr = gvDevices.GetDataRow(gvDevices.FocusedRowHandle);
            byte kindId = BitConverter.GetBytes(Convert.ToInt16(dr[DeviceConfig.DC_KIND_ID]))[0];   
            ChooseDevice = SysCtrl.CreateDevice(kindId).CreateDevice(new DeviceData(dr));//---创建相应的设备对象-----
            if (SysConfig.ListNetworks.ContainsKey(ChooseDevice.NetworkIP) &&
                SysConfig.ListNetworks[ChooseDevice.NetworkIP].State == NetworkConfig.STATE_CONNECTED)
                this.DialogResult = DialogResult.Yes;
            else { CommonTools.MessageShow("网络链接已断开,请重新链接!", 2, ""); ChooseDevice = null; }
        }

        /// <summary>
        /// 加载
        /// </summary>
        private void SelectDevice_Load(object sender, EventArgs e)
        {
            DataTable dt = SysConfig.DtDevice.Clone();
            string cdnStr = chooseCondition();
            DataRow[] rows =  SysConfig.DtDevice.Select(cdnStr);
            foreach (DataRow dr in rows)
                dt.Rows.Add(dr.ItemArray);
            dt.AcceptChanges();
            gcDevices.DataSource = dt;
            gvDevices.BestFitColumns();
        }



    }
}
