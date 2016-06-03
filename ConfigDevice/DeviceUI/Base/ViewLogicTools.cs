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
        public Device DeviceEdit;//设备对象
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

            dcObject.ColumnEdit = cbxLogicObj;//---触发对象---

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

        public void InitTriggers(params string[] Triggers)
        {
            foreach (string trigger in Triggers)
                cbxLogicObj.Items.Add(trigger); 
        }

        /// <summary>
        /// 选择
        /// </summary>
        private void cbxLogicObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (string)cbxLogicObj.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            ViewLogicControl vlc =  ViewEditCtrl.GetViewLogicControl(name,DeviceEdit,gvLogic);
      
            
        }





    }
}
