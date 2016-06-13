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
            DataLogicSetting.Columns.Add(ViewConfig.DC_KIND, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_OPERATION, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_START_VALUE, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_END_VALUE, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_VALID_TIME, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_INVALID_TIME, System.Type.GetType("System.String"));

            dcObject.FieldName = ViewConfig.DC_OBJECT;
            dcKind.FieldName = ViewConfig.DC_KIND;
            dcOperate.FieldName = ViewConfig.DC_OPERATION;
            dcRangeStart.FieldName = ViewConfig.DC_START_VALUE;
            dcRangeEnd.FieldName = ViewConfig.DC_END_VALUE;
            dcValid.FieldName = ViewConfig.DC_VALID_TIME;
            dcInvalid.FieldName = ViewConfig.DC_INVALID_TIME;

            //---触发对象---
            cbxLogicObj = new GridViewComboBox();
            dcObject.ColumnEdit = cbxLogicObj;
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
            cbxLogicObj.Items.Add(ViewConfig.SENSOR_INVALID);//添加无效选择 
            //gvLogic.SetRowCellValue(0,dcObject,cbxLogicObj.Items[cbxLogicObj.Items.Count-1].ToString());

            gvLogic.SetRowCellValue(0,dcObject,ViewConfig.SENSOR_INVALID);//---默认选择无效---
            ViewLogicObj = ViewEditCtrl.GetViewLogicControl(ViewConfig.LG_SENSOR_DEFAULT, DeviceEdit, gvLogic);
            DataRow dr = gvLogic.GetDataRow(0);
            dr[ViewConfig.DC_OBJECT] = ViewConfig.SENSOR_INVALID;
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
        public void SetLogicData(TriggerData td)
        {          
            DataRow dr = this.gvLogic.GetDataRow(0);
            dr[ViewConfig.DC_OBJECT] = ViewConfig.TRIGGER_ID_NAME[td.TriggerObjectID];
            ViewLogicObj = ViewEditCtrl.GetViewLogicControl(td.TriggerObjectID, DeviceEdit, gvLogic);
            ViewLogicObj.InitViewSetting();  
            ViewLogicObj.SetLogicData(td);
            
            dr.EndEdit();          
 
        }

        /// <summary>
        /// 设置命令行
        /// </summary>
        /// <returns>TriggerData</returns>
        public TriggerData GetLogicData()
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


    }
}
