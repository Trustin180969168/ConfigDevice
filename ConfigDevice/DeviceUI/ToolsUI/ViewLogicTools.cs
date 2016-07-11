using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class ViewLogicTools : UserControl
    {
        public ChangePosition GoUp;//---向上移动---
        public ChangePosition GoDown;//---向下移动----
        public DataTable DataLogicSetting;//---逻辑配置表-----
        public Device DeviceEdit;//---设备对象---
        private GridViewComboBox cbxLogicObj;//---触发对象---
        public BaseViewLogicControl ViewLogicObj;//----逻辑视图编辑对象-----
        /// <summary>
        /// 序号
        /// </summary>
        private int num;
        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        public ViewLogicTools()
        {
            InitializeComponent();

            DataLogicSetting = new DataTable("逻辑配置");
            DataLogicSetting.Columns.Add(ViewConfig.DC_OBJECT, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_POSITION, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_KIND, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_OPERATION, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_START_VALUE, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_END_VALUE, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_VALID_TIME, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_INVALID_TIME, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_DEVICE_ID, System.Type.GetType("System.String"));//差异设备ID
            DataLogicSetting.Columns.Add(ViewConfig.DC_DEVICE_KIND_ID, System.Type.GetType("System.String"));//差异类型ID
            DataLogicSetting.Columns.Add(ViewConfig.DC_DEVICE_NETWORK_ID, System.Type.GetType("System.String"));//差异网络ID 

            dcObject.FieldName = ViewConfig.DC_OBJECT;
            dcKind.FieldName = ViewConfig.DC_KIND;
            dcOperate.FieldName = ViewConfig.DC_OPERATION;
            dcRangeStart.FieldName = ViewConfig.DC_START_VALUE;
            dcRangeEnd.FieldName = ViewConfig.DC_END_VALUE;
            dcValid.FieldName = ViewConfig.DC_VALID_TIME;
            dcInvalid.FieldName = ViewConfig.DC_INVALID_TIME;
            dcDiffDevice.FieldName = ViewConfig.DC_DEVICE_VALUE;
            dcPosition.FieldName = ViewConfig.DC_POSITION;

            //---触发对象---
            cbxLogicObj = new GridViewComboBox();
            dcObject.ColumnEdit = cbxLogicObj;
            cbxLogicObj.DropDownRows = 16;
            cbxLogicObj.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cbxLogicObj.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cbxLogicObj.SelectedIndexChanged += new EventHandler(cbxLogicObj_SelectedIndexChanged);

            DataLogicSetting.Rows.Add();
            this.gcLogic.DataSource = DataLogicSetting;
        }

        public ViewLogicTools(int num, params string[] Triggers)
            : this()
        {
            this.num = num;
            foreach (string trigger in Triggers)
                cbxLogicObj.Items.Add(trigger);
        }
        /// <summary>
        /// 初始化触发列表
        /// </summary>
        /// <param name="Triggers"></param>
        public void InitTriggerList(params string[] Triggers)
        {
            foreach (string trigger in Triggers)//-----初始化触发列表---
                cbxLogicObj.Items.Add(trigger);
            cbxLogicObj.Items.Add(SensorConfig.SENSOR_INVALID);//添加无效选择 
            //gvLogic.SetRowCellValue(0,dcObject,cbxLogicObj.Items[cbxLogicObj.Items.Count-1].ToString());

            gvLogic.SetRowCellValue(0,dcObject,SensorConfig.SENSOR_INVALID);//---默认选择无效---
            ViewLogicObj = ViewEditCtrl.GetViewLogicControl(SensorConfig.LG_SENSOR_DEFAULT, DeviceEdit, gvLogic);
            DataRow dr = gvLogic.GetDataRow(0);
            dr[ViewConfig.DC_OBJECT] = SensorConfig.SENSOR_INVALID;//----触发初始化,触发对象----
            dr.EndEdit();
        }

        /// <summary>
        /// 清空触发对象
        /// </summary>
        public void ClearTriggerView()
        {
            gvLogic.SetRowCellValue(0, dcObject, SensorConfig.SENSOR_INVALID);//---默认选择无效---
            ViewLogicObj = ViewEditCtrl.GetViewLogicControl(SensorConfig.LG_SENSOR_DEFAULT, DeviceEdit, gvLogic);
            DataRow dr = gvLogic.GetDataRow(0);
            dr[ViewConfig.DC_OBJECT] = SensorConfig.SENSOR_INVALID;//----触发初始化,触发对象----
            dr.EndEdit();
        }

        /// <summary>
        /// 选择
        /// </summary>
        private void cbxLogicObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (string)cbxLogicObj.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            ViewLogicObj = ViewEditCtrl.GetViewLogicControl(ViewConfig.TRIGGER_NAME_ID[name], DeviceEdit, gvLogic);
            DataRow dr = gvLogic.GetDataRow(0);
            dr[ViewConfig.DC_OBJECT] = name;
            dr.EndEdit();
            ViewLogicObj.InitViewSetting();   
        }

  
        /// <summary>
        /// 设置命令行
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="data">命令</param>
        /// <returns></returns>
        public void SetTriggerData(TriggerData td)
        {          
            DataRow dr = this.gvLogic.GetDataRow(0);
            ViewLogicObj = ViewEditCtrl.GetViewLogicControl(td.TriggerObjectID, DeviceEdit, gvLogic);//---获取逻辑控制对象-----
            ViewLogicObj.InitViewSetting();     //------初始化视图配置------
            ViewLogicObj.SetLogicData(td);      //------设置逻辑数据-------            
            dr.EndEdit();
            dr.AcceptChanges();//---提交变更----
        }

        public bool HasChanged  //------是否执行了更改------
        {
            get
            {
                this.gvLogic.PostEditor();
                DataRow dr = gvLogic.GetDataRow(0);
                dr.EndEdit();
                DataTable dt = this.DataLogicSetting.GetChanges(DataRowState.Modified);
                if (dt != null && dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 设置命令行
        /// </summary>
        /// <returns>TriggerData</returns>
        public TriggerData GetTriggerData()
        {
            try
            {
                //----保存设备的网络ID和设备ID-----
                gvLogic.PostEditor();
                DataRow dr = gvLogic.GetDataRow(0);
                dr.EndEdit();
                return ViewLogicObj.GetLogicData();
            }
            catch { return null; }
        }

        /// <summary>
        /// 向上移动
        /// </summary> 
        private void btGoUp_Click(object sender, EventArgs e)
        {
            this.GoUp(this.num);
        }

        /// <summary>
        /// 向下移动
        /// </summary> 
        private void btGoDown_Click(object sender, EventArgs e)
        {
            this.GoDown(this.num);
        }


    }
}
