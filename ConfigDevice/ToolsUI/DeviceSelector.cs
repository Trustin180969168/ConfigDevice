using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice
{
    public partial class DeviceSelector : UserControl
    {
        private GridViewGridLookupEdit gridLookupDevice;//设备下拉选择 
        private GridViewDigitalEdit edtNum = new GridViewDigitalEdit();//地址编辑
        public DataTable DevicesData = null;//---对象配置表-----
        public bool DisableAddNew = false;//是否允许添加新行
        public GridView GvEdit = null;
        public Action InitViewDelegate = null;
        public int DeviceCount = 1;//---默认2个
        public DeviceSelector()
        {
            InitializeComponent();
            GvEdit = this.gvDevices;
            DevicesData = new DataTable();

            DevicesData.Columns.Add(ViewConfig.DC_NUM, System.Type.GetType("System.Int16"));
            DevicesData.Columns.Add(ViewConfig.DC_DEVICE_ID, System.Type.GetType("System.Int16"));
            DevicesData.Columns.Add(ViewConfig.DC_CONTROL_OBJ, System.Type.GetType("System.String"));
            DevicesData.Columns.Add(ViewConfig.DC_KIND, System.Type.GetType("System.Int16"));
            DevicesData.Columns.Add(ViewConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
            DevicesData.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            DevicesData.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));
              
            dcID.FieldName = ViewConfig.DC_DEVICE_ID;
            dcControlObject.FieldName = ViewConfig.DC_CONTROL_OBJ;
            dcDeviceValue.FieldName = ViewConfig.DC_DEVICE_VALUE;

            
            edtNum.MinValue = 0;
            edtNum.MaxValue = 200;
        }

 
        /// <summary>
        /// 初始化,根据分类id设计界面
        /// </summary>
        public virtual void Init()
        {
            if (InitViewDelegate != null)
                InitViewDelegate.Invoke();
            gcDevices.DataSource = DevicesData;  
            gridLookupDevice = ViewEditCtrl.GetDevicesLookupEdit();//-----下拉选择------ 
            gridLookupDevice.DataSource = DevicesData;

            gridLookupDevice.EditValueChanged += this.lookUpEdit_EditValueChanged;
            dcDeviceValue.ColumnEdit = gridLookupDevice;

            dcID.ColumnEdit = edtNum;
        }

        /// <summary>
        /// 选择切换
        /// </summary> 
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            this.gvDevices.PostEditor(); 
            DataRow drControlObj = gvDevices.GetDataRow(gvDevices.FocusedRowHandle);
            drControlObj.EndEdit();
            string deviceValue = drControlObj[ViewConfig.DC_DEVICE_VALUE].ToString();
            //-----获取选择的设备-------------
            int i = gridLookupDevice.GetIndexByKeyValue(deviceValue);
            if (i == -1) return;
            DataRow drSelect = (gridLookupDevice.DataSource as DataTable).Rows[i];
            //-----添加选择设备信息到指令列表-------
            drControlObj[ViewConfig.DC_DEVICE_ID] = drSelect[DeviceConfig.DC_ID]; 
            drControlObj.EndEdit();
            gvDevices.BestFitColumns();
        }
       

 


    }
}
