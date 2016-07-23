using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class PanelEnvironment : UserControl
    {
        private GridViewGridLookupEdit gridLookupDevice;//设备下拉选择
        private GridViewComboBox cbxSensor;//传感器选择
        public DataTable DataSensorSetting;//---传感器配置表-----
        public PanelEnvironment()
        {
            InitializeComponent();

            DataSensorSetting = new DataTable();

            DataSensorSetting.Columns.Add(ViewConfig.DC_NUM, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_ID, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_NETWORK_ID, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_KIND, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));

            dcNum.FieldName = ViewConfig.DC_NUM;
            dcID.FieldName = ViewConfig.DC_DEVICE_ID;
            dcNetwork.FieldName = ViewConfig.DC_DEVICE_NETWORK_ID;
            dcDeviceValue.FieldName = ViewConfig.DC_DEVICE_VALUE;
            dcKindName.FieldName = ViewConfig.DC_KIND_NAME;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            for (int i = 0; i < 8; i++)
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
            cbxSensor.EditValueChanged += this.CbxSensorKind_EditValueChanged;
            dcKindName.ColumnEdit = cbxSensor;

            gridLookupDevice = ViewEditCtrl.GetDevicesLookupEdit(ViewConfig.SELECT_ENVIRONMENT_DEVICE_QUERY_CONDITION);//-----下拉选择------        
            gridLookupDevice.EditValueChanged += this.lookUpEdit_EditValueChanged;
            dcDeviceValue.ColumnEdit = gridLookupDevice;
        }

        /// <summary>
        /// 选择切换
        /// </summary> 
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            this.gvSensors.PostEditor(); 
            DataRow drCommand = gvSensors.GetDataRow(gvSensors.FocusedRowHandle);
            drCommand.EndEdit();
            string deviceValue = drCommand[ViewConfig.DC_DEVICE_VALUE].ToString();
            //-----获取选择的设备-------------
            int i = gridLookupDevice.GetIndexByKeyValue(deviceValue);
            DataRow drSelect = (gridLookupDevice.DataSource as DataTable).Rows[i];
            //-----添加选择设备信息到指令列表-------
            drCommand[ViewConfig.DC_ID] = drSelect[DeviceConfig.DC_ID];
            drCommand[ViewConfig.DC_DEVICE_NETWORK_ID] = drSelect[DeviceConfig.DC_NETWORK_ID]; 
            drCommand.EndEdit();
            gvSensors.BestFitColumns();
        }

        /// <summary>
        /// 选择传感器类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CbxSensorKind_EditValueChanged(object sender, EventArgs e)
        {
            this.gvSensors.PostEditor();
            DataRow drCommand = gvSensors.GetDataRow(gvSensors.FocusedRowHandle);
            drCommand.EndEdit();
            drCommand[ViewConfig.DC_KIND] = ViewConfig.TRIGGER_NAME_ID[drCommand[ViewConfig.DC_KIND_NAME].ToString()];            
        }


        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void SetOptionData(PanelOptionData optionData)
        {
            for (int i = 0; i < 8; i++)
            {
                PanelSensorInfo panelSensor = optionData.PanelSensors[i];
                string deviceValue = panelSensor.DeviceNetworkID.ToString() + "_" + panelSensor.DeviceID.ToString();

                DataTable dtSelect = this.gridLookupDevice.DataSource as DataTable;
                DataRow[] rows = dtSelect.Select(ViewConfig.DC_DEVICE_VALUE + "='" + deviceValue + "'");
                if (rows.Length <= 0)//----选择设备列表没有,则手动加上----
                {
                    DataRow drInsert = dtSelect.Rows.Add();
                    drInsert[DeviceConfig.DC_NAME] = ViewConfig.NAME_INVALID_DEVICE;
                    drInsert[ViewConfig.DC_DEVICE_VALUE] = deviceValue;
                    drInsert[DeviceConfig.DC_NETWORK_ID] = (int)panelSensor.DeviceNetworkID;//---网络ID---
                    drInsert[DeviceConfig.DC_ID] = (int)panelSensor.DeviceID;//-----设备ID---
                    drInsert.EndEdit();
                    dtSelect.AcceptChanges();
                }
                //------赋值到列表中----------
                DataRow dr = gvSensors.GetDataRow(i);
                dr[ViewConfig.DC_DEVICE_ID] = panelSensor.DeviceID;
                dr[ViewConfig.DC_DEVICE_NETWORK_ID] = panelSensor.DeviceNetworkID;
                dr[ViewConfig.DC_NAME] = ViewConfig.TRIGGER_ID_NAME[panelSensor.SensorKind];
                dr.EndEdit();
            }

        }

        /// <summary>
        /// 获取设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void GetOptionData(ref PanelOptionData optionData)
        {
            for (int i = 0; i < 8; i++)
            {
                PanelSensorInfo panelSensor = optionData.PanelSensors[i]; 
                //------赋值到列表中----------
                DataRow dr = gvSensors.GetDataRow(i);
                panelSensor.DeviceID=Convert.ToByte( dr[ViewConfig.DC_DEVICE_ID]);
                panelSensor.DeviceNetworkID= Convert.ToByte(dr[ViewConfig.DC_DEVICE_NETWORK_ID]);
                panelSensor.SensorKind = ViewConfig.TRIGGER_NAME_ID[dr[ViewConfig.DC_NAME].ToString()];
       
            }
        }


    }
}
