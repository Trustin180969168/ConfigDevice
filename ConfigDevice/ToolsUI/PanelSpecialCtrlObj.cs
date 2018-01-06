using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class PanelSpecialCtrlObj : UserControl
    {
        private GridViewGridLookupEdit gridLookupDevice;//设备下拉选择 
        private GridViewDigitalEdit edtNum = new GridViewDigitalEdit();//地址编辑
        public DataTable DataControlObjects;//---对象配置表-----
        public bool DisableAddNew = false;//是否允许添加新行
        public int DeviceCount = 2;//---默认2个
        public PanelSpecialCtrlObj()
        {
            InitializeComponent();

            DataControlObjects = new DataTable();

            DataControlObjects.Columns.Add(ViewConfig.DC_NUM, System.Type.GetType("System.Int16"));
            DataControlObjects.Columns.Add(ViewConfig.DC_DEVICE_ID, System.Type.GetType("System.Int16"));
            DataControlObjects.Columns.Add(ViewConfig.DC_CONTROL_OBJ, System.Type.GetType("System.String"));
            DataControlObjects.Columns.Add(ViewConfig.DC_KIND, System.Type.GetType("System.Int16"));
            DataControlObjects.Columns.Add(ViewConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
            DataControlObjects.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            DataControlObjects.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));
              
            dcID.FieldName = ViewConfig.DC_DEVICE_ID;
            dcControlObject.FieldName = ViewConfig.DC_CONTROL_OBJ;
            dcDeviceValue.FieldName = ViewConfig.DC_DEVICE_VALUE; 

            edtNum.MinValue = 0;
            edtNum.MaxValue = 255;
        }

        /// <summary>
        /// 初始化,根据分类id设计界面
        /// </summary>
        public void Init(int specialNum)
        {
            switch (specialNum)
            {
                case 0:
                case 1:
                case 4:
                case 2: DataControlObjects.Rows.Clear(); break;
                case 3:
                case 6:
                case 7: 
                    DataControlObjects.Rows.Clear(); 
                    DataControlObjects.Rows.Add(0, 0, "吊柜"); break;
                case 5: 
                    DataControlObjects.Rows.Clear(); 
                    DataControlObjects.Rows.Add(0, 0, "吊柜");
                    DataControlObjects.Rows.Add(1, 0, "地柜");  break;
                default: break;
            }
     
            gcSpecialObjs.DataSource = DataControlObjects; 
            DataTable dtSelectDevices = ViewEditCtrl.GetDevicesSpecialPanelLookupData(ViewConfig.SELECT_DEVICE_PANEL_SPECIAL_QUERY_CONDITION);
            gridLookupDevice = ViewEditCtrl.GetDevicesLookupEdit();//-----下拉选择------ 
            gridLookupDevice.DataSource = dtSelectDevices;

            gridLookupDevice.EditValueChanged += this.lookUpEdit_EditValueChanged;
            dcDeviceValue.ColumnEdit = gridLookupDevice;

            dcID.ColumnEdit = edtNum;
        }

        /// <summary>
        /// 选择切换
        /// </summary> 
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            this.gvSpecialObjs.PostEditor(); 
            DataRow drControlObj = gvSpecialObjs.GetDataRow(gvSpecialObjs.FocusedRowHandle);
            drControlObj.EndEdit();
            string deviceValue = drControlObj[ViewConfig.DC_DEVICE_VALUE].ToString();
            //-----获取选择的设备-------------
            int i = gridLookupDevice.GetIndexByKeyValue(deviceValue);
            if (i == -1) return;
            DataRow drSelect = (gridLookupDevice.DataSource as DataTable).Rows[i];
            //-----添加选择设备信息到指令列表-------
            drControlObj[ViewConfig.DC_DEVICE_ID] = drSelect[DeviceConfig.DC_ID]; 
            drControlObj.EndEdit();
            gvSpecialObjs.BestFitColumns();
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void GetOptionData(int kindNum, SpecialPanelOptionData optionData)
        {
        
            for (int i = 0; i < 2; i++)
            {
                ControlObjectInfo specialObj = optionData.ControlObjects[i];
                string deviceValue = specialObj.DeviceNetworkID.ToString() + "_" + specialObj.DeviceID.ToString();
                DataTable dtSelect = this.gridLookupDevice.DataSource as DataTable;
                DataRow[] rows = dtSelect.Select(ViewConfig.DC_DEVICE_VALUE + "='" + deviceValue + "'");
                if (rows.Length <= 0)//----选择设备列表没有,则手动加上----
                {
                    DataRow drInsert = dtSelect.Rows.Add();
                    drInsert[DeviceConfig.DC_NAME] = ViewConfig.NAME_INVALID_DEVICE;//---未知设备----- 
                    drInsert[ViewConfig.DC_DEVICE_VALUE] = deviceValue; 
                    drInsert[DeviceConfig.DC_ID] = (int)specialObj.DeviceID;//-----设备ID---
                    drInsert.EndEdit();
                    dtSelect.AcceptChanges();
                }
                //------赋值到列表中----------
                DataRow dr = gvSpecialObjs.GetDataRow(i);
                if (dr != null)
                {
                    dr[ViewConfig.DC_DEVICE_ID] = specialObj.DeviceID;
                    dr[ViewConfig.DC_DEVICE_VALUE] = deviceValue;
                    dr.EndEdit();
                }
            }

        }         

        /// <summary>
        /// 获取设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void SetOptionData(int kindNum, SpecialPanelOptionData optionData)
        {
            this.gvSpecialObjs.PostEditor();
            //---按键类型是空的时候，清空---
            if (DataControlObjects.Rows.Count == 0)
            {
                optionData.ControlObjects[0] = new ControlObjectInfo();
                optionData.ControlObjects[1] = new ControlObjectInfo();
                return;
            }
            DataRow drSensor = gvSpecialObjs.GetDataRow(gvSpecialObjs.FocusedRowHandle);
            drSensor.EndEdit(); 
            for (int i = 0; i<DataControlObjects.Rows.Count; i++)
            {
                ControlObjectInfo panelSensor = optionData.ControlObjects[i]; 
                //------赋值到列表中----------
                DataRow dr = gvSpecialObjs.GetDataRow(i);
                panelSensor.DeviceID=Convert.ToByte( dr[ViewConfig.DC_DEVICE_ID]);
            } 
        }


    }
}
