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
        /// 双击选择设备
        /// </summary>
        private void gvDevices_DoubleClick(object sender, EventArgs e)
        {
            if (gvDevices.FocusedRowHandle == -1) return;
            DataRow dr = gvDevices.GetDataRow(gvDevices.FocusedRowHandle);
            byte kindId = BitConverter.GetBytes(Convert.ToInt16(dr[DeviceConfig.DC_KIND_ID]))[0];
            ChooseDevice = FactoryDevice.CreateDevice(kindId).CreateDevice(new DeviceData(dr));//---创建相应的设备对象-----
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
            DataRow[] rows =  SysConfig.DtDevice.Select( ViewConfig.SELECT_COMMAND_DEVICE_QUERY_CONDITION);
            foreach (DataRow dr in rows)
                dt.Rows.Add(dr.ItemArray);
            dt.AcceptChanges();
            gcDevices.DataSource = dt;
            gvDevices.BestFitColumns();
        }



    }
}
