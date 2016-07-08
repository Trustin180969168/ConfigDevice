using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
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
        public const string DC_DEVICE_VALUE = "DeviceValue";//差异设备
        public const string DC_OPERATION = "Operation";//运算操作
        public const string DC_START_VALUE = "StartValue";//开始值
        public const string DC_END_VALUE = "EndValue";//结束值
        public const string DC_VALID_TIME = "ValidTime";//有效就
        public const string DC_INVALID_TIME = "InvalidTime";//无效时间 
        public const string DC_NAME = "Name";
        public const string DC_NUM = "Num";//---序号--
        public const string DC_ID = "ID";
        public const string DC_DEVICE_ID = "DeviceID";//设备ID
        public const string DC_DEVICE_NETWORK_ID = "DeviceNetworkID";//设备的网络ID
        public const string DC_DEVICE_KIND_ID = "DeviceKindID";//设备类型ID
        public const string DC_LOGIC_VALUE = "LogicValue";//逻辑配置值
        public const string DC_LOGIC_ADDITION_VALUE = "LogicAddtionValue";//逻辑附加值
        public const string DC_SENSOR_NAME = "Name";//传感器名称
        public const string DC_SENSOR_VALUE = "Value";//传感器值
        public const string DC_SENSOR_LEVEL = "Level";//传感器级别
        public const string DC_STATE = "State";//状态
        public const string DC_CURRENT = "Current";//电流
        public const string DC_VALUE1 = "Value1";//值1 
        public const string DC_VALUE2 = "Value2";//值2 
        public const string DC_VALUE3 = "Value3";//值3 
        public const string DC_VALUE4 = "Value4";//值4 
        public const string DC_VALUE5 = "Value5";//值5 
        public const string DC_VALUE6 = "Value6";//值6 
        public const string DC_VALUE7 = "Value7";//值7 
        public const string DC_VALUE8 = "Value8";//值8 
        public const string DC_VALUE9 = "Value9";//值9 
        public const string DC_ACTION1 = "Action1";//动作1 
        public const string DC_ACTION2 = "Action2";//动作2 
        public const string DC_ACTION3 = "Action3";//动作3 
        public const string DC_ACTION4 = "Action4";//动作4 
        public const string DC_ACTION5 = "Action5";//动作5 
        public const string DC_ACTION6 = "Action6";//动作6 
        public const string DC_ACTION7 = "Action7";//动作7 
        public const string DC_ACTION8 = "Action8";//动作8 
        public const string DC_ACTION9 = "Action9";//动作9 
        public const string DC_PARAMETER1 = "PARAMETER1";//参数1 
        public const string DC_PARAMETER2 = "PARAMETER2";//参数2 
        public const string DC_PARAMETER3 = "PARAMETER3";//参数3 
        public const string DC_PARAMETER4 = "PARAMETER4";//参数4 
        public const string DC_PARAMETER5 = "PARAMETER5";//参数5 
        public const string DC_PARAMETER6 = "PARAMETER6";//参数6 
        public const string DC_PARAMETER7 = "PARAMETER7";//参数7 
        public const string DC_PARAMETER8 = "PARAMETER8";//参数8 
        public const string DC_PARAMETER9 = "PARAMETER9";//参数9 

        //public const string DC_DEVICE_VALUE = "DeviceValue";//设备唯一值

        public const string NAME_INVALID_DEVICE = "未知设备";

        public static Dictionary<string, UInt16> TRIGGER_NAME_ID = new Dictionary<string, UInt16>(); //-----触发对象对应的值---- 
        public static Dictionary<UInt16, string> TRIGGER_ID_NAME = new Dictionary<UInt16, string>(); //-----触发对象对应的值---- 
        public static Dictionary<string, UInt16> TRIGGER_KIND_NAME_ID = new Dictionary<string, UInt16>(); //-----触发级别ID对应的值---- 
        public static Dictionary<UInt16, string> TRIGGER_KIND_ID_NAME = new Dictionary<UInt16, string>(); //-----触发级别值对应的ID---- 
        public static Dictionary<string, UInt16> MATH_NAME_ID = new Dictionary<string, UInt16>(); //-----运算ID值---- 
        public static Dictionary<UInt16, string> MATH_ID_NAME = new Dictionary<UInt16, string>(); //-----运算ID对应的名称---- 
        public static Dictionary<string, UInt16> TRIGGER_POSITION_NAME_ID = new Dictionary<string, UInt16>(); //-----触发位置ID---- 
        public static Dictionary<UInt16, string> TRIGGER_POSITION_ID_NAME = new Dictionary<UInt16, string>(); //-----触发位置名称---- 

        public static  string SELECT_COMMAND_DEVICE_QUERY_CONDITION = DeviceConfig.DC_KIND_ID + " in (" +
                   "'" + (int)DeviceConfig.EQUIPMENT_AMP_MP3 + "'," +
                   "'" + (int)DeviceConfig.EQUIPMENT_CURTAIN_3CH + "'," +
                   "'" + (int)DeviceConfig.EQUIPMENT_SWIT_4 + "'," +
                   "'" + (int)DeviceConfig.EQUIPMENT_SWIT_6 + "'," +
                   "'" + (int)DeviceConfig.EQUIPMENT_SWIT_8 + "'," +
                   "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_2 + "'," +
                   "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_4 + "'," +
                   "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_6 + "'," +
                   "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_8 + "'," +
                   "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_12 + "'," +
                   "'" + (int)DeviceConfig.EQUIPMENT_SERVER + "'" +
                   ")";

        public static string SELECT_LOGIC_DEVICE_QUERY_CONDITION = " 1 = 1 ";
            //DeviceConfig.DC_KIND_ID + " in (" +
            //       "'" + (int)DeviceConfig.EQUIPMENT_AMP_MP3 + "'," +
            //       "'" + (int)DeviceConfig.EQUIPMENT_CURTAIN_3CH + "'," +
            //       "'" + (int)DeviceConfig.EQUIPMENT_SWIT_4 + "'," +
            //       "'" + (int)DeviceConfig.EQUIPMENT_SWIT_6 + "'," +
            //       "'" + (int)DeviceConfig.EQUIPMENT_SWIT_8 + "'," +
            //       "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_2 + "'," +
            //       "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_4 + "'," +
            //       "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_6 + "'," +
            //       "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_8 + "'," +
            //       "'" + (int)DeviceConfig.EQUIPMENT_TRAILING_12 + "'," +
            //       "'" + (int)DeviceConfig.EQUIPMENT_SERVER + "'" +
            //       ")";

        static ViewConfig()
        {
            //-------传感器触发-------
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_INVALID, SensorConfig.LG_SENSOR_VOID);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_TEMPERATURE, SensorConfig.LG_SENSOR_TEMP);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_HUMIDITY, SensorConfig.LG_SENSOR_HUMI);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_AQI, SensorConfig.LG_SENSOR_AQI);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_TVOC, SensorConfig.LG_SENSOR_TVOC);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_CO2, SensorConfig.LG_SENSOR_CO2);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_PM25, SensorConfig.LG_SENSOR_PM25);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_CH20, SensorConfig.LG_SENSOR_CH2O);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_O2, SensorConfig.LG_SENSOR_O2);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_RAIN, SensorConfig.LG_SENSOR_RAIN);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_WINDY, SensorConfig.LG_SENSOR_WIND);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_LUMINANCE, SensorConfig.LG_SENSOR_LUMI);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_FLAMMABLE_GAS_PROBE, SensorConfig.LG_SENSOR_LEL);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_RADAR, SensorConfig.LG_SENSOR_RSP);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SWIT_TAMPER, SensorConfig.LG_SENSOR_TAMPER);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SYSTEM_INTERACTION, SensorConfig.LG_EXT_SENSOR_SYS_LKID);//系统联动
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_INNER_INTERACTION, SensorConfig.LG_EXT_SENSOR_SLF_LKID);//内部联动
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SECURITY_INTERACTION, SensorConfig.LG_EXT_SENSOR_SECURITY);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_TIME, SensorConfig.LG_EXT_SENSOR_TIME_SEG);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_DATE, SensorConfig.LG_EXT_SENSOR_DATE_SEG);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_WEEK, SensorConfig.LG_EXT_SENSOR_WEEK_CYC);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_FIRE_TEMPERATURE, SensorConfig.LG_SENSOR_TEMP_FC);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_VOID, SensorConfig.SENSOR_INVALID);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_TEMP, SensorConfig.SENSOR_TEMPERATURE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_HUMI, SensorConfig.SENSOR_HUMIDITY);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_AQI, SensorConfig.SENSOR_AQI);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_CO2, SensorConfig.SENSOR_CO2);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_PM25, SensorConfig.SENSOR_PM25);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_CH2O, SensorConfig.SENSOR_CH20);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_O2, SensorConfig.SENSOR_O2);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_RAIN, SensorConfig.SENSOR_RAIN);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_WIND, SensorConfig.SENSOR_WINDY);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_LUMI, SensorConfig.SENSOR_LUMINANCE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_TVOC, SensorConfig.SENSOR_TVOC);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_LEL, SensorConfig.SENSOR_FLAMMABLE_GAS_PROBE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_RSP, SensorConfig.SENSOR_RADAR);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_TAMPER, SensorConfig.SENSOR_SWIT_TAMPER);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_SYS_LKID, SensorConfig.SENSOR_SYSTEM_INTERACTION);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_SLF_LKID, SensorConfig.SENSOR_INNER_INTERACTION);//内部联动
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_SECURITY, SensorConfig.SENSOR_SECURITY_INTERACTION);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_TIME_SEG, SensorConfig.SENSOR_TIME);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_DATE_SEG, SensorConfig.SENSOR_DATE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_WEEK_CYC, SensorConfig.SENSOR_WEEK);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_TEMP_FC, SensorConfig.SENSOR_FIRE_TEMPERATURE);
            //------触发位置的对应关系--------
            TRIGGER_POSITION_NAME_ID.Add(SensorConfig.SENSOR_POSITION_LOCAL, 0);//-------本地-----
            TRIGGER_POSITION_NAME_ID.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL, SensorConfig.LG_SENSOR_DEV_FLAG);//----外设----
            TRIGGER_POSITION_NAME_ID.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT, SensorConfig.LG_SENSOR_DIF_FLAG_VALUE);//----外设差值----
            TRIGGER_POSITION_ID_NAME.Add(0, SensorConfig.SENSOR_POSITION_LOCAL);//-------本地-----
            TRIGGER_POSITION_ID_NAME.Add(SensorConfig.LG_SENSOR_DEV_FLAG, SensorConfig.SENSOR_POSITION_PERIPHERAL);//----外设----
            TRIGGER_POSITION_ID_NAME.Add(SensorConfig.LG_SENSOR_DIF_FLAG_VALUE, SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT);//----外设差值----
            //-------触发类型-------
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_VALUE_KIND_VALUE, SensorConfig.LG_SENSOR_DEF_FLAG);//---默认触发类别---
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL, SensorConfig.LG_SENSOR_LVL_FLAG);//----级别-----
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_VALUE_KIND_SAME_UNIT, SensorConfig.LG_SENSOR_MASK);
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_VALUE_KIND_SAME_TYPE, SensorConfig.LG_SENSOR_TYP_MASK);
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_END_MASK, SensorConfig.LG_SENSOR_END_MARK);

            TRIGGER_KIND_ID_NAME.Add(SensorConfig.LG_SENSOR_DEF_FLAG, SensorConfig.SENSOR_VALUE_KIND_VALUE);//---默认触发类别---
            TRIGGER_KIND_ID_NAME.Add(SensorConfig.LG_SENSOR_LVL_FLAG, SensorConfig.SENSOR_VALUE_KIND_LEVEL);//----级别-----
            TRIGGER_KIND_ID_NAME.Add(SensorConfig.LG_SENSOR_MASK, SensorConfig.SENSOR_VALUE_KIND_SAME_UNIT);
            TRIGGER_KIND_ID_NAME.Add(SensorConfig.LG_SENSOR_TYP_MASK, SensorConfig.SENSOR_VALUE_KIND_SAME_TYPE);
            TRIGGER_KIND_ID_NAME.Add(SensorConfig.LG_SENSOR_END_MARK, SensorConfig.SENSOR_END_MASK);

            //-------运算符-------
            MATH_ID_NAME.Add(SensorConfig.LG_MATH_EQUAL_TO, SensorConfig.LG_MATH_NAME_EQUAL_TO);
            MATH_ID_NAME.Add(SensorConfig.LG_MATH_LESS_THAN, SensorConfig.LG_MATH_NAME_LESS_THAN);
            MATH_ID_NAME.Add(SensorConfig.LG_MATH_GREATER_THAN, SensorConfig.LG_MATH_NAME_GREATER_THAN);
            MATH_ID_NAME.Add(SensorConfig.LG_MATH_WITHIN, SensorConfig.LG_MATH_NAME_WITHIN);
            MATH_ID_NAME.Add(SensorConfig.LG_MATH_WITHOUT, SensorConfig.LG_MATH_NAME_WITHOUT);
            MATH_ID_NAME.Add(SensorConfig.LG_MATH_EQUAL_TO2, SensorConfig.LG_MATH_NAME_EQUAL_TO2);
            MATH_ID_NAME.Add(SensorConfig.LG_MATH_EQUAL_AND_TRUE, SensorConfig.LG_MATH_NAME_EQUAL_AND_TRUE);
            MATH_ID_NAME.Add(SensorConfig.LG_MATH_TOTAL, SensorConfig.LG_MATH_NAME_TOTAL);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO, SensorConfig.LG_MATH_EQUAL_TO);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_LESS_THAN, SensorConfig.LG_MATH_LESS_THAN);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_GREATER_THAN, SensorConfig.LG_MATH_GREATER_THAN);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_WITHIN, SensorConfig.LG_MATH_WITHIN);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_WITHOUT, SensorConfig.LG_MATH_WITHOUT);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_TOTAL, SensorConfig.LG_MATH_TOTAL);

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
    /// 下拉查找选择
    /// </summary>
    public class GridViewLookupEdit : DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    {
        public GridViewLookupEdit()
            : base()
        {
    
        }
    }

    /// <summary>
    /// 下拉表查找选择
    /// </summary>
    public class GridViewGridLookupEdit : DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    {


        public GridViewGridLookupEdit( )
            : base()
        {
 
            this.View.OptionsView.ShowIndicator = false;
            this.View.OptionsView.ShowGroupPanel = false;
            this.View.OptionsView.ShowAutoFilterRow = true;
            this.View.BestFitColumns();
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
            this.PopupFormMinSize =  new System.Drawing.Size(50, 200);
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
