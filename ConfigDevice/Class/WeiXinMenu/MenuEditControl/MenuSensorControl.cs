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
        MenuSensorEdit menuSensorEdit; //---传感器设备编辑---
        MenuSensorSettingData menuSensorSettingData;//---设备绑定数据---

        private GridViewLookupEdit lookupDeviceKind;//设备下拉选择
        private GridViewDigitalEdit edtNum = new GridViewDigitalEdit();//地址编辑
        public DataTable DataSensorSetting;//---传感器配置表-----
        public const int DeviceCount = 2;
        public DataTable dtSelectKind = new DataTable("SelectKind");

        public MenuSensorControl()
        {
            InitializeComponent();

            DataSensorSetting = new DataTable("Sensor");

            DataSensorSetting.Columns.Add(ViewConfig.DC_NUM, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_ID, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_NETWORK_ID, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_KIND, System.Type.GetType("System.Int16"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            DataSensorSetting.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));

            dtSelectKind.Columns.Add(ViewConfig.DC_KIND, System.Type.GetType("System.Int16"));
            dtSelectKind.Columns.Add(ViewConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
            dtSelectKind.Rows.Add(new object[] { DeviceConfig.EQUIPMENT_AIR_QUALITY, DeviceConfig.EQUIPMENT_ID_NAME[DeviceConfig.EQUIPMENT_AIR_QUALITY] });
            dtSelectKind.Rows.Add(new object[] { DeviceConfig.EQUIPMENT_AIR_O2, DeviceConfig.EQUIPMENT_ID_NAME[DeviceConfig.EQUIPMENT_AIR_O2] });
            dtSelectKind.Rows.Add(new object[] { DeviceConfig.EQUIPMENT_WEATHER, DeviceConfig.EQUIPMENT_ID_NAME[DeviceConfig.EQUIPMENT_WEATHER] });
            dtSelectKind.Rows.Add(new object[] { DeviceConfig.EQUIPMENT_TEMP, DeviceConfig.EQUIPMENT_ID_NAME[DeviceConfig.EQUIPMENT_TEMP] });
            dtSelectKind.Rows.Add(new object[] { DeviceConfig.EQUIPMENT_PRI_3, DeviceConfig.EQUIPMENT_ID_NAME[DeviceConfig.EQUIPMENT_PRI_3] });
            dtSelectKind.Rows.Add(new object[] { DeviceConfig.EQUIPMENT_RSP, DeviceConfig.EQUIPMENT_ID_NAME[DeviceConfig.EQUIPMENT_RSP] });
            dtSelectKind.Rows.Add(new object[] { DeviceConfig.EQUIPMENT_FUEL_GAS, DeviceConfig.EQUIPMENT_ID_NAME[DeviceConfig.EQUIPMENT_FUEL_GAS] });
            dtSelectKind.AcceptChanges();

            dcNum.FieldName = ViewConfig.DC_NUM;
            dcID.FieldName = ViewConfig.DC_DEVICE_ID;
            dcDeviceName.FieldName = ViewConfig.DC_NAME;
            dcNetwork.FieldName = ViewConfig.DC_DEVICE_NETWORK_ID; 
            dcKindID.FieldName = ViewConfig.DC_KIND;

            edtNum.MinValue = 0;
            edtNum.MaxValue = 200;
        }

        /// <summary>
        /// 覆盖基类的初始化方法
        /// </summary>
        /// <param name="_device"></param>
        /// <param name="_data"></param>
        /// <param name="_edit"></param>
        public override void InitEdit(WeiXin _device, MenuData _data)
        {
            base.InitEdit(_device, _data);
            this.Init();
            menuSensorEdit = new MenuSensorEdit(_device, _data);
            menuEdit = menuSensorEdit;

            menuSensorEdit.OnCallbackUI_Action += this.callbackUI;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            for (int i = 0; i < DeviceCount; i++)
                DataSensorSetting.Rows.Add((i + 1).ToString());
            DataSensorSetting.AcceptChanges();
            gcSensors.DataSource = DataSensorSetting;  
            
            lookupDeviceKind = new GridViewLookupEdit();
            lookupDeviceKind.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ViewConfig.DC_KIND, "类型ID", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(ViewConfig.DC_KIND_NAME, "设备类型", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None)});
            lookupDeviceKind.Name = "lookupEdit";
            lookupDeviceKind.DisplayMember = ViewConfig.DC_KIND_NAME;
            lookupDeviceKind.ValueMember = ViewConfig.DC_KIND;
            lookupDeviceKind.DataSource =  dtSelectKind;
            lookupDeviceKind.NullText = "";
            lookupDeviceKind.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            dcKindID.ColumnEdit = lookupDeviceKind;

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
            //this.gvSensors.PostEditor();
            //DataRow drSensor = gvSensors.GetDataRow(gvSensors.FocusedRowHandle);
            //drSensor.EndEdit();
            //drSensor[ViewConfig.DC_KIND] = ViewConfig.TRIGGER_ID_NAME.Sensor_n.EQUIPMENT_NAME_ID[drSensor[ViewConfig.DC_KIND_NAME].ToString()];
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

                DataSensorSetting.Rows.Add(new object[] { 1+i,sensorData.DeviceID,sensorData.DeviceNetworkID,
                sensorData.DeviceKindID, DeviceConfig.EQUIPMENT_ID_NAME[(byte)sensorData.DeviceKindID],sensorData.DeviceName});
            }
            DataSensorSetting.AcceptChanges();
        }

        /// <summary>
        /// 获取传感器设备数据
        /// </summary>
        public void GetSensorData()
        {
            menuSensorEdit.ReadMenuSensor();
        }

        private void callbackUI(CallbackParameter callbackParameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(this.callbackUI), callbackParameter);
                return;
            }
            if (callbackParameter.Action == ActionKind.ReadMenuSensor)
            {
                menuSensorSettingData = callbackParameter.Parameters[0] as MenuSensorSettingData;
                int i = 0;
                foreach (MenuSensorData sensor in menuSensorSettingData.MenuDeviceDataList)
                {                    
                    if (sensor == null) continue;                    
                    DataRow dr = DataSensorSetting.Rows[i++];
                    dr[ViewConfig.DC_NAME] = sensor.DeviceName;
                    dr[ViewConfig.DC_KIND] = sensor.DeviceKindID;
                    dr[ViewConfig.DC_DEVICE_ID] = sensor.DeviceID;
                    dr[ViewConfig.DC_DEVICE_NETWORK_ID] = sensor.DeviceNetworkID;
                    dr.EndEdit(); 
                }
            }
        }
       
        /// <summary>
        /// 保存菜单配置
        /// </summary>
        public override void SaveSetting()
        {
            MenuSensorSettingData menuSensorSettingData = new MenuSensorSettingData(this.menuData);
            menuSensorSettingData.KindId = (byte)this.menuData.ByteKindID;
            menuSensorSettingData.MenuId = this.menuData.MenuID;
            gvSensors.PostEditor();
            DataRow drEdit = gvSensors.GetDataRow(gvSensors.FocusedRowHandle);
            drEdit.EndEdit();
            foreach (MenuSensorData sensor in menuSensorSettingData.MenuDeviceDataList)
            {
                int i = 0;
                DataRow dr = DataSensorSetting.Rows[i];
                sensor.DeviceName = dr[ViewConfig.DC_NAME].ToString();
                sensor.ByteDeviceKindID = Convert.ToByte(dr[ViewConfig.DC_KIND]);
                sensor.ByteDeviceID = Convert.ToByte(dr[ViewConfig.DC_DEVICE_ID]);
                sensor.ByteDeviceNetworkID = Convert.ToByte(dr[ViewConfig.DC_DEVICE_NETWORK_ID]);
                i++;
            }
            menuSensorEdit.SaveMenuSensor(menuSensorSettingData);
        }

    }
}
