using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice 
{
    public partial class MenuSensorControl : MenuEditControl
    {

        private GridViewLookupEdit lookupDevice;//设备下拉选择
        private GridViewComboBox cbxSensor;//传感器选择
        private GridViewDigitalEdit edtNum = new GridViewDigitalEdit();//地址编辑
        public DataTable DataSensorSetting;//---传感器配置表-----
        public int DeviceCount = 4;//---默认8个 

        public MenuSensorControl()
        {
            InitializeComponent();


            DataSensorSetting = new DataTable();

            DataSensorSetting.Columns.Add(ViewConfig.DC_NUM, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_ID, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_NETWORK_ID, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_KIND, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));

            dcNum.FieldName = ViewConfig.DC_NUM;
            dcID.FieldName = ViewConfig.DC_DEVICE_ID;
            dcNetwork.FieldName = ViewConfig.DC_DEVICE_NETWORK_ID;
            dcDeviceValue.FieldName = ViewConfig.DC_DEVICE_VALUE;
            dcKindName.FieldName = ViewConfig.DC_KIND_NAME;

            edtNum.MinValue = 0;
            edtNum.MaxValue = 200;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            for (int i = 0; i < DeviceCount; i++)
                DataSensorSetting.Rows.Add((i + 1).ToString());
            gcSensors.DataSource = DataSensorSetting;

            cbxSensor = new GridViewComboBox();
            cbxSensor.Items.Add(SensorConfig.SENSOR_TEMPERATURE);
            cbxSensor.Items.Add(SensorConfig.SENSOR_HUMIDITY);
            cbxSensor.Items.Add(SensorConfig.SENSOR_LUMINANCE);
            cbxSensor.Items.Add(SensorConfig.SENSOR_AQI);
            cbxSensor.Items.Add(SensorConfig.SENSOR_TVOC);
            cbxSensor.Items.Add(SensorConfig.SENSOR_CO2);
            cbxSensor.Items.Add(SensorConfig.SENSOR_CH20);
            cbxSensor.Items.Add(SensorConfig.SENSOR_PM25);
            cbxSensor.Items.Add(SensorConfig.SENSOR_O2);
            cbxSensor.Items.Add(SensorConfig.SENSOR_INVALID);
            cbxSensor.DropDownRows = 16;
            cbxSensor.EditValueChanged += this.CbxSensorKind_EditValueChanged;
            dcKindName.ColumnEdit = cbxSensor;

            DataTable dtSelectDevices = ViewEditCtrl.GetDevicesLookupData(ViewConfig.SELECT_ENVIRONMENT_DEVICE_QUERY_CONDITION);
            lookupDevice = new GridViewLookupEdit();
            lookupDevice.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DeviceConfig.DC_ID, DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DeviceConfig.DC_NAME, "设备名称", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None)});
            lookupDevice.Name = "lookupEdit";
            lookupDevice.DisplayMember = DeviceConfig.DC_ID;
            lookupDevice.ValueMember = DeviceConfig.DC_ID;



            //lookupDevice.EditValueChanged += this.lookUpEdit_EditValueChanged;
            //dcDeviceValue.ColumnEdit = lookupDevice;

            dcID.ColumnEdit = edtNum;
            dcNetwork.ColumnEdit = edtNum;

            //gridLookupDevice.AllowNullInput =  DevExpress.Utils.DefaultBoolean.True;
            //gridLookupDevice.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }


        /// <summary>
        /// 选择切换
        /// </summary> 
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            //this.gvSensors.PostEditor();
            //DataRow drSensor = gvSensors.GetDataRow(gvSensors.FocusedRowHandle);
            //drSensor.EndEdit(); 
            ////-----获取选择的设备-------------
            //int i = lookupDevice.getdatasourcerowindex
            //DataRow drSelect = (lookupDevice.DataSource as DataTable).Rows[i];
            ////-----添加选择设备信息到指令列表-------
            //drSensor[ViewConfig.DC_DEVICE_ID] = drSelect[DeviceConfig.DC_ID];
            //drSensor[ViewConfig.DC_DEVICE_NETWORK_ID] = drSelect[DeviceConfig.DC_NETWORK_ID];
            //drSensor.EndEdit();
            //gvSensors.BestFitColumns();
        }

        /// <summary>
        /// 选择传感器类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CbxSensorKind_EditValueChanged(object sender, EventArgs e)
        {
            this.gvSensors.PostEditor();
            DataRow drSensor = gvSensors.GetDataRow(gvSensors.FocusedRowHandle);
            drSensor.EndEdit();
            drSensor[ViewConfig.DC_KIND] = ViewConfig.TRIGGER_NAME_ID[drSensor[ViewConfig.DC_KIND_NAME].ToString()];
        }


        public void InitEnvironmentSetting(MenuSensorSettingData data)
        {
            for (int i = 0; i < data.MenuDeviceDataList.Length; i++)
            {
                MenuSensorData sensorData = data.MenuDeviceDataList[0];
                //DataSensorSetting.Columns.Add(ViewConfig.DC_NUM, System.Type.GetType("System.Int16"));
                //DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_ID, System.Type.GetType("System.Int16"));
                //DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_NETWORK_ID, System.Type.GetType("System.Int16"));
                //DataSensorSetting.Columns.Add(ViewConfig.DC_KIND, System.Type.GetType("System.Int16"));
                //DataSensorSetting.Columns.Add(ViewConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
                //DataSensorSetting.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
                //DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));

                DataSensorSetting.Rows.Add(new object[] { 1,sensorData.DeviceID,sensorData.DeviceNetworkID,
                sensorData.DeviceKindID,"",sensorData.DeviceName});
            }
            DataSensorSetting.AcceptChanges();
        }


    }
}
