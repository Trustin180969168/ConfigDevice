using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Controls;
using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Data;

namespace ConfigDevice
{
    public static class ViewConfig
    {
        //---执行操作名称----
        public const string ACTION_CHANGE_LOGIC_GROUP = "ChangeLogicGroup";

        //---逻辑列表-----
        public const string DC_OBJECT = "Object";//触发对象
        public const string DC_POSITION = "Position";//触发位置
        public const string DC_KIND = "Kind";//内容类型
        public const string DC_DIFFERENT_DEVICE = "Device";//差异设备
        public const string DC_OPERATION = "Operation";//运算操作
        public const string DC_START_VALUE = "StartValue";//开始值
        public const string DC_END_VALUE = "EndValue";//结束值
        public const string DC_VALID_TIME = "ValidTime";//有效就
        public const string DC_INVALID_TIME = "InvalidTime";//无效时间 
        public const string DC_NAME = "Name";
        public const string DC_ID = "ID";

        // 逻辑关系 ----------------------------------------------------------------------------------------------
        public const int LG_4OR = 0;          //第1种逻辑关系:4个条件[或]
        public const int LG_4AND = 1;          //第2种逻辑关系:4个条件[与]
        public const int LG_3OR_1AND = 2;          //第3种逻辑关系:3个条件[或],再1个条件[与]
        public const int LG_3AND_1OR = 3;          //第4种逻辑关系:3个条件[与],再1个条件[或]
        public const int LG_2OR_2AND_OR = 4;          //第5种逻辑关系:2个条件[或],2个条件[与],再两者[或]
        public const int LG_2OR_2AND_AND = 5;          //第5种逻辑关系:2个条件[或],2个条件[与],再两者[与]
        public const int LG_TOTAL = 6;
        public const int LG_DEFAULT = LG_4OR;

        // 数学关系 ----------------------------------------------------------------------------------------------
        public const int LG_MATH_EQUAL_TO = 0;          //等于(只判断[slSiz1])                                  <- if (val1 == slSiz1)
        public const int LG_MATH_LESS_THAN = 1;          //小于(只判断[slSiz1])                                  <- if (val1 <  slSiz1)
        public const int LG_MATH_GREATER_THAN = 2;          //大于(只判断[slSiz1])                                  <- if (val1 >  slSiz1)
        public const int LG_MATH_WITHIN = 3;          //以内(范围内)(包括两边的数值) (判断[slSiz1]和[slSiz2]) <- if (val1 >= slSiz1) && if (val1 <= slSiz2)
        public const int LG_MATH_WITHOUT = 4;          //以外(范围外)                 (判断[slSiz1]和[slSiz2]) <- if (val1 <  slSiz1) || if (val1 >  slSiz2)
        public const int LG_MATH_EQUAL_TO2 = 5;          //等于(判断[slSiz1]和[slSiz2])                          <- if (val2 == slSiz2)  { if (val1 == slSiz1) }
        public const int LG_MATH_EQUAL_AND_TRUE = 6;          //等于("与"运算后如果为"真") (只判断[slSiz1])           <- if (val1 &  slSiz1)
        public const int LG_MATH_TOTAL = 7;
        public const int LG_MATH_DEFAULT = LG_MATH_EQUAL_TO;

        public const string LG_MATH_NAME_EQUAL_TO = "等于";          //等于(只判断[slSiz1])                                  <- if (val1 == slSiz1)
        public const string LG_MATH_NAME_LESS_THAN = "小于";          //小于(只判断[slSiz1])                                  <- if (val1 <  slSiz1)
        public const string LG_MATH_NAME_GREATER_THAN = "大于";          //大于(只判断[slSiz1])                                  <- if (val1 >  slSiz1)
        public const string LG_MATH_NAME_WITHIN = "以内";          //以内(范围内)(包括两边的数值) (判断[slSiz1]和[slSiz2]) <- if (val1 >= slSiz1) && if (val1 <= slSiz2)
        public const string LG_MATH_NAME_WITHOUT = "以外";          //以外(范围外)                 (判断[slSiz1]和[slSiz2]) <- if (val1 <  slSiz1) || if (val1 >  slSiz2)
        public const string LG_MATH_NAME_EQUAL_TO2 = "两者值相等";          //等于(判断[slSiz1]和[slSiz2])                          <- if (val2 == slSiz2)  { if (val1 == slSiz1) }
        public const string LG_MATH_NAME_EQUAL_AND_TRUE = "两者与为真";          //等于("与"运算后如果为"真") (只判断[slSiz1])           <- if (val1 &  slSiz1)
        public const string LG_MATH_NAME_TOTAL = "相加";
        public const string LG_MATH_NAME_DEFAULT = LG_MATH_NAME_EQUAL_TO;
 

        public static Dictionary<string, UInt16> TRIGGER_NAME_ID = new Dictionary<string, UInt16>(); //-----触发对象对应的值---- 
        public static Dictionary<UInt16, string> TRIGGER_ID_NAME = new Dictionary<UInt16, string>(); //-----触发对象对应的值---- 
        public static Dictionary<string, UInt16> TRIGGER_KIND_NAME_ID = new Dictionary<string, UInt16>(); //-----触发级别ID对应的值---- 
        public static Dictionary<UInt16, string> TRIGGER_KIND_ID_NAME = new Dictionary<UInt16, string>(); //-----触发级别值对应的ID---- 
        public static Dictionary<string, UInt16> MATH_NAME_ID = new Dictionary<string, UInt16>(); //-----运算ID值---- 
        public static Dictionary<UInt16, string> MATH_ID_NAME = new Dictionary<UInt16, string>(); //-----运算ID对应的名称---- 
        static ViewConfig()
        {
            //-------传感器触发-------
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_INVALID, SensorConfig.LG_SENSOR_VOID);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_TEMPERATURE, SensorConfig.LG_SENSOR_TEMP);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_HUMIDITY, SensorConfig.LG_SENSOR_HUMI);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_RAIN_SENSOR, SensorConfig.LG_SENSOR_RAIN);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_WINDY, SensorConfig.LG_SENSOR_WIND);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_LUMINANCE, SensorConfig.LG_SENSOR_LUMI);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_FLAMMABLE_GAS_PROBE, SensorConfig.LG_SENSOR_LEL);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_RADAR, SensorConfig.LG_SENSOR_RSP);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SWIT_TAMPER, SensorConfig.LG_SENSOR_TAMPER);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SYSTEM_INTERACTION, SensorConfig.LG_EXT_SENSOR_SYS_LKID);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SECURITY_INTERACTION, SensorConfig.LG_EXT_SENSOR_SECURITY);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_TIME, SensorConfig.LG_EXT_SENSOR_TIME_SEG);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_DATE, SensorConfig.LG_EXT_SENSOR_DATE_SEG);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_WEEK, SensorConfig.LG_EXT_SENSOR_WEEK_CYC);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_FIRE_TEMPERATURE, SensorConfig.LG_DEV_SENSOR_TEMP);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_VOID, SensorConfig.SENSOR_INVALID);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_TEMP, SensorConfig.SENSOR_TEMPERATURE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_HUMI, SensorConfig.SENSOR_HUMIDITY);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_RAIN, SensorConfig.SENSOR_RAIN_SENSOR);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_WIND, SensorConfig.SENSOR_WINDY);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_LUMI, SensorConfig.SENSOR_LUMINANCE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_LEL, SensorConfig.SENSOR_FLAMMABLE_GAS_PROBE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_RSP, SensorConfig.SENSOR_RADAR);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_TAMPER, SensorConfig.SENSOR_SWIT_TAMPER);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_SYS_LKID, SensorConfig.SENSOR_SYSTEM_INTERACTION);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_SECURITY, SensorConfig.SENSOR_SECURITY_INTERACTION);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_TIME_SEG, SensorConfig.SENSOR_TIME);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_DATE_SEG, SensorConfig.SENSOR_DATE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_WEEK_CYC, SensorConfig.SENSOR_WEEK);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_DEV_SENSOR_TEMP, SensorConfig.SENSOR_FIRE_TEMPERATURE);

            //-------触发类型-------
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_VALUE_KIND_VALUE, SensorConfig.LG_SENSOR_KIND_FLAG);//---默认触发类别---
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_VALUE_KIND_PERIPHERAL, SensorConfig.LG_SENSOR_DEV_FLAG);//----外设-----
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL, SensorConfig.LG_SENSOR_LVL_FLAG);//----级别-----
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_VALUE_KIND_SAME_UNIT, SensorConfig.LG_SENSOR_MASK);
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_VALUE_KIND_SAME_TYPE, SensorConfig.LG_SENSOR_TYP_MASK);
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_END_MASK, SensorConfig.LG_SENSOR_END_MARK);

            TRIGGER_KIND_ID_NAME.Add(SensorConfig.LG_SENSOR_KIND_FLAG, SensorConfig.SENSOR_VALUE_KIND_VALUE);//---默认触发类别---
            TRIGGER_KIND_ID_NAME.Add(SensorConfig.LG_SENSOR_DEV_FLAG, SensorConfig.SENSOR_VALUE_KIND_PERIPHERAL);//----外设-----
            TRIGGER_KIND_ID_NAME.Add(SensorConfig.LG_SENSOR_LVL_FLAG, SensorConfig.SENSOR_VALUE_KIND_LEVEL);//----级别-----
            TRIGGER_KIND_ID_NAME.Add(SensorConfig.LG_SENSOR_MASK, SensorConfig.SENSOR_VALUE_KIND_SAME_UNIT);
            TRIGGER_KIND_ID_NAME.Add(SensorConfig.LG_SENSOR_TYP_MASK, SensorConfig.SENSOR_VALUE_KIND_SAME_TYPE);
            TRIGGER_KIND_ID_NAME.Add(SensorConfig.LG_SENSOR_END_MARK, SensorConfig.SENSOR_END_MASK);
            
            //-------运算符-------
            MATH_ID_NAME.Add(LG_MATH_EQUAL_TO, LG_MATH_NAME_EQUAL_TO);
            MATH_ID_NAME.Add(LG_MATH_LESS_THAN, LG_MATH_NAME_LESS_THAN);
            MATH_ID_NAME.Add(LG_MATH_GREATER_THAN, LG_MATH_NAME_GREATER_THAN);
            MATH_ID_NAME.Add(LG_MATH_WITHIN, LG_MATH_NAME_WITHIN);
            MATH_ID_NAME.Add(LG_MATH_WITHOUT, LG_MATH_NAME_WITHOUT);
            MATH_ID_NAME.Add(LG_MATH_EQUAL_TO2, LG_MATH_NAME_EQUAL_TO2);
            MATH_ID_NAME.Add(LG_MATH_EQUAL_AND_TRUE, LG_MATH_NAME_EQUAL_AND_TRUE);
            MATH_ID_NAME.Add(LG_MATH_TOTAL, LG_MATH_NAME_TOTAL); 
            MATH_NAME_ID.Add(LG_MATH_NAME_EQUAL_TO, LG_MATH_EQUAL_TO);
            MATH_NAME_ID.Add(LG_MATH_NAME_LESS_THAN, LG_MATH_LESS_THAN);
            MATH_NAME_ID.Add(LG_MATH_NAME_GREATER_THAN, LG_MATH_GREATER_THAN);
            MATH_NAME_ID.Add(LG_MATH_NAME_WITHIN, LG_MATH_WITHIN);
            MATH_NAME_ID.Add(LG_MATH_NAME_WITHOUT, LG_MATH_WITHOUT);
            MATH_NAME_ID.Add(LG_MATH_NAME_EQUAL_TO2, LG_MATH_EQUAL_TO2);
            MATH_NAME_ID.Add(LG_MATH_NAME_EQUAL_AND_TRUE, LG_MATH_EQUAL_AND_TRUE);
            MATH_NAME_ID.Add(LG_MATH_NAME_TOTAL, LG_MATH_TOTAL); 


        }
   
    }

    /// <summary>
    /// 触发对象列表数据
    /// </summary>
    public class LookupIDAndNameTable : DataTable
    {
        public LookupIDAndNameTable()
            : base()
        {
            this.TableName = "ID名称列表";
            Columns.Add(ViewConfig.DC_ID, System.Type.GetType("System.Int16"));
            Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
        }
    }

    /// <summary>
    /// 下拉控件
    /// </summary>
    public class GridViewComboBox:DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    {
        public GridViewComboBox()
            : base()
        {
            TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
        }
    }

    /// <summary>
    /// 时间控件
    /// </summary>
    public class GridViewTimeEdit : DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit
    {
        public GridViewTimeEdit()
            : base()
        {
            DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            Mask.EditMask = "HH:mm:ss";
            Mask.UseMaskAsDisplayFormat = true;
            Leave += new System.EventHandler(SysConfig.Edit_Leave);
            Enter += new System.EventHandler(SysConfig.Edit_Enter);
        }
    }

    /// <summary>
    /// 月日类型控件
    /// </summary>
    public class GridViewDateOfMonthDayEdit : DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit
    {
        public GridViewDateOfMonthDayEdit()
            : base()
        {
            DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            Mask.EditMask = "MM-dd";
            Mask.UseMaskAsDisplayFormat = true;
            Leave += new System.EventHandler(SysConfig.Edit_Leave);
            Enter += new System.EventHandler(SysConfig.Edit_Enter);
        }
    }

    /// <summary>
    /// 数字类型控件
    /// </summary>
    public class GridViewDigitalEdit : DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    {
        public GridViewDigitalEdit()
            : base()
        {
            AutoHeight = false;
            Mask.EditMask = "d";
            Leave += new System.EventHandler(SysConfig.Edit_Leave);
            Enter += new System.EventHandler(SysConfig.Edit_Enter);
        }
    }


    /// <summary>
    /// 百分比控件
    /// </summary>
    public class GridViewPercentEdit : DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    {
        public GridViewPercentEdit()
            : base()
        {
            AutoHeight = false;
            Mask.EditMask = "P0";
            Mask.UseMaskAsDisplayFormat = true;
            MaxValue = new decimal(new int[] { 100, 0, 0, 0 });
            MinValue = new decimal(new int[] { 0, 0, 0, 0 });
            Leave += new System.EventHandler(SysConfig.Edit_Leave);
            Enter += new System.EventHandler(SysConfig.Edit_Enter);
        }
    }

    /// <summary>
    /// 文本编辑控件
    /// </summary>
    public class GridViewTextEdit : DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    {
        public GridViewTextEdit()
            : base()
        {
            AutoHeight = false;
        }
    }


    /// <summary>
    /// 周编辑
    /// </summary>
    public class GridViewWeekEdit : DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit
    {

        private string weekValue = ""; 
        private bool allowEdit = true;
        public GridViewWeekEdit()
            : base()
        {
            this.Name = "WeekEdit";
             
            this.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期一"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期二"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期三"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期四"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期五"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期六"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期日")});
            this.ShowAllItemCaption = "全选";

            this.EditValueChanged += checkedComboBoxEdit_EditValueChanged;
            this.Closed += checkedComboBoxEdit_Closed;
            this.QueryPopUp += checkedComboBoxEdit_Properties_QueryPopUp;
            this.ShowPopupCloseButton = false;
           
        }
       
        private void checkedComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            
            if (!allowEdit) return;
            if ((sender as CheckedComboBoxEdit).Text != "")
            {
                if ((sender as CheckedComboBoxEdit).Text.Contains("星期"))
                {
                    weekValue = (sender as CheckedComboBoxEdit).Text;
                    (sender as CheckedComboBoxEdit).Text = weekValue;
                    (sender as CheckedComboBoxEdit).Text = (sender as CheckedComboBoxEdit).Text.Replace("星期", "");
                }
            }
            else
            {
                weekValue = "";
                (sender as CheckedComboBoxEdit).Text = "";
                (sender as CheckedComboBoxEdit).Text = (sender as CheckedComboBoxEdit).Text.Replace("星期", "");
            }
        }

        private void checkedComboBoxEdit_Closed(object sender, ClosedEventArgs e)
        {
            allowEdit = false;

            if ((sender as CheckedComboBoxEdit).Text.Contains("星期"))
            {
                weekValue = (sender as CheckedComboBoxEdit).Text;
                (sender as CheckedComboBoxEdit).Text = weekValue;
                (sender as CheckedComboBoxEdit).Text = (sender as CheckedComboBoxEdit).Text.Replace("星期", "");
            }
            allowEdit = true;
        }



        private void checkedComboBoxEdit_Properties_QueryPopUp(object sender, CancelEventArgs e)
        {
            allowEdit = false;
            if ((sender as CheckedComboBoxEdit).Text == "") return;

            string temp1 = (sender as CheckedComboBoxEdit).EditValue.ToString().Replace(", ", ", 星期");
            string temp2 = "星期" + temp1;
            (sender as CheckedComboBoxEdit).Text = temp2;
            allowEdit = true;
        }

   

    }

      






}
