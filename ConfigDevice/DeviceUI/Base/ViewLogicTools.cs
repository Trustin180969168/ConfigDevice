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
        /// <summary>
        /// 序号
        /// </summary>
        public int Num;

        public ViewLogicTools()
        {
            InitializeComponent();

            DataLogicSetting = new DataTable("逻辑配置");
            DataLogicSetting.Columns.Add(ViewConfig.DC_OBJECT, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_OPERATION, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_START_VALUE, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_END_VALUE, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_VALID_TIME, System.Type.GetType("System.String"));
            DataLogicSetting.Columns.Add(ViewConfig.DC_INVALID_TIME, System.Type.GetType("System.String"));

            dcObject.FieldName = ViewConfig.DC_OBJECT;
            dcOperate.FieldName = ViewConfig.DC_OPERATION;
            dcRangeStart.FieldName = ViewConfig.DC_START_VALUE;
            dcRangeEnd.FieldName = ViewConfig.DC_END_VALUE;
            dcValid.FieldName = ViewConfig.DC_VALID_TIME;
            dcInvalid.FieldName = ViewConfig.DC_INVALID_TIME; 


            DataLogicSetting.Rows.Add();
            this.gcLogic.DataSource = DataLogicSetting;
        }
        
        public ViewLogicTools(int num)
            : this()
        {
            this.Num = num;
        }





    }
}
