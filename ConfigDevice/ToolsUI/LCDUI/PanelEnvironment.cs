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
        private GridViewDigitalEdit edtNum = new GridViewDigitalEdit();//地址编辑
        public DataTable DataSensorSetting;//---传感器配置表-----
        public bool DisableAddNew = false;//是否允许添加新行
        public int DeviceCount = 8;//---默认8个
        public PanelEnvironment()
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
            gridLookupDevice = ViewEditCtrl.GetDevicesLookupEdit();//-----下拉选择------ 
            gridLookupDevice.DataSource = dtSelectDevices;

            gridLookupDevice.EditValueChanged += this.lookUpEdit_EditValueChanged;
            dcDeviceValue.ColumnEdit = gridLookupDevice;

            dcID.ColumnEdit = edtNum;
            dcNetwork.ColumnEdit = edtNum;

            //gridLookupDevice.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(CustomGridLookUpEdit_ProcessNewValue);
            //gridLookupDevice.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            //gridLookupDevice.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }

        /// <summary>
        /// 实现在列表没有记录的时候，可以录入一个不存在的记录，类似ComoboEidt功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CustomGridLookUpEdit_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            if (!DisableAddNew )
            { 
                string display = e.DisplayValue.ToString();

                DataTable dtTemp = gridLookupDevice.DataSource as DataTable;
                if (dtTemp != null)
                {
                    DataRow[] selectedRows = dtTemp.Select(string.Format("{0}='{1}'", DeviceConfig.DC_NAME, display));
                    if (selectedRows == null || selectedRows.Length == 0)
                    {
                        DataRow row = dtTemp.NewRow();
                        row[DeviceConfig.DC_NAME] = display;
                        dtTemp.Rows.Add(row);
                        row.EndEdit();
                    }
                }

                e.Handled = true;
            }
        }

        /// <summary>
        /// 选择切换
        /// </summary> 
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            this.gvSensors.PostEditor(); 
            DataRow drSensor = gvSensors.GetDataRow(gvSensors.FocusedRowHandle);
            drSensor.EndEdit();
            string deviceValue = drSensor[ViewConfig.DC_DEVICE_VALUE].ToString();
            //-----获取选择的设备-------------
            int i = gridLookupDevice.GetIndexByKeyValue(deviceValue);
            if (i == -1) return;
            DataRow drSelect = (gridLookupDevice.DataSource as DataTable).Rows[i];
            //-----添加选择设备信息到指令列表-------
            drSensor[ViewConfig.DC_DEVICE_ID] = drSelect[DeviceConfig.DC_ID];
            drSensor[ViewConfig.DC_DEVICE_NETWORK_ID] = drSelect[DeviceConfig.DC_NETWORK_ID]; 
            drSensor.EndEdit();
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
            DataRow drSensor = gvSensors.GetDataRow(gvSensors.FocusedRowHandle);
            drSensor.EndEdit();
            drSensor[ViewConfig.DC_KIND] = ViewConfig.TRIGGER_NAME_ID[drSensor[ViewConfig.DC_KIND_NAME].ToString()];            
        }


        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void SetOptionData(ref LCDPanelOptionData optionData)
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
                    drInsert[DeviceConfig.DC_NAME] = ViewConfig.NAME_INVALID_DEVICE;//---未知设备-----
                    drInsert[DeviceConfig.DC_KIND_NAME] = ViewConfig.NAME_INVALID_KIND;//---未知类型
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
                dr[ViewConfig.DC_KIND_NAME] = ViewConfig.TRIGGER_ID_NAME[panelSensor.SensorKind];
                dr[ViewConfig.DC_DEVICE_VALUE] = deviceValue;
                dr.EndEdit();
            }

        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void SetOptionData(ref SpecialPanelOptionData optionData)
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
                    drInsert[DeviceConfig.DC_NAME] = ViewConfig.NAME_INVALID_DEVICE;//---未知设备-----
                    drInsert[DeviceConfig.DC_KIND_NAME] = ViewConfig.NAME_INVALID_KIND;//---未知类型
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
                dr[ViewConfig.DC_KIND_NAME] = ViewConfig.TRIGGER_ID_NAME[panelSensor.SensorKind];
                dr[ViewConfig.DC_DEVICE_VALUE] = deviceValue;
                dr.EndEdit();
            }

        }

        /// <summary>
        /// 获取设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void GetOptionData(ref LCDPanelOptionData optionData)
        {
            this.gvSensors.PostEditor();
            DataRow drSensor = gvSensors.GetDataRow(gvSensors.FocusedRowHandle);
            drSensor.EndEdit();

            for (int i = 0; i < 8; i++)
            {
                PanelSensorInfo panelSensor = optionData.PanelSensors[i]; 
                //------赋值到列表中----------
                DataRow dr = gvSensors.GetDataRow(i);
                panelSensor.DeviceID=Convert.ToByte( dr[ViewConfig.DC_DEVICE_ID]);
                panelSensor.DeviceNetworkID= Convert.ToByte(dr[ViewConfig.DC_DEVICE_NETWORK_ID]);
                panelSensor.SensorKind = ViewConfig.TRIGGER_NAME_ID[dr[ViewConfig.DC_KIND_NAME].ToString()];
       
            }
        }


    }
}
