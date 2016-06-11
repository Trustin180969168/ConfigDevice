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
        public const string DC_KIND = "Kind";//内容类型
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

        // 设备[内部条件][外部条件]类型 --------------------------------------------------------------------------
        public const int LG_SENSOR_VOID = 0;          //无效(★不能改动★)
        public const int LG_SENSOR_TEMP = 1;          //内部条件:温度   (含有外设,含有等级)
        public const int LG_SENSOR_HUMI = 2;          //内部条件:湿度   (含有外设,含有等级)
        public const int LG_SENSOR_RAIN = 3;          //内部条件:雨感   (含有外设         )
        public const int LG_SENSOR_WIND = 4;          //内部条件:风速   (含有外设,含有等级)
        public const int LG_SENSOR_LUMI = 5;          //内部条件:亮度   (含有外设,含有等级)
        public const int LG_SENSOR_LEL = 6;          //内部条件:可燃气探头  (含有外设         )
        public const int LG_SENSOR_RSP = 7;          //内部条件:雷达    (含有外设         )
        public const int LG_SENSOR_TAMPER = 8;          //内部条件:防拆开关 (含有外设         )
        public const int LG_EXT_SENSOR_SYS_LKID = 9;          //外部条件:系统联动
        public const int LG_EXT_SENSOR_SECURITY = 10;         //外部条件:安防联动
        public const int LG_EXT_SENSOR_TIME_SEG = 11;         //外部条件:时间段
        public const int LG_EXT_SENSOR_DATE_SEG = 12;         //外部条件:日期段
        public const int LG_EXT_SENSOR_WEEK_CYC = 13;         //外部条件:周循环
        public const int LG_DEV_SENSOR_TEMP = 14;           //消防温控    (含有外设,含有等级)
        public const int LG_SENSOR_TOTAL = 15;              //总数(★★数量以后会不断增加,必须在最尾处增加★★)
        public const UInt16 LG_SENSOR_KIND_FLAG = 0;       //默认触发级别
        public const UInt16 LG_SENSOR_DEV_FLAG = 0x8000;       //[外设]传感器[标志位]->如:本设备,外设
        public const UInt16 LG_SENSOR_LVL_FLAG = 0x4000;       //传感器[级别][标志位]->如:温度,27℃(数值),舒适(级别)
        public const UInt16 LG_SENSOR_MASK = 0xBFFF;           //[同一个]传感器[掩码]->如:本设备的温度传感器,外设的温度传感器
        public const UInt16 LG_SENSOR_TYP_MASK = 0x3FFF;       //[同类型]传感器[掩码]->如:温度,湿度
        public const UInt16 LG_SENSOR_END_MARK = 0xFFFF;       //传感器结束符 
        public const UInt16 LG_SENSOR_DEFAULT = LG_SENSOR_VOID;


        //系统联动[ucCmp ]值:           LG_MATH_EQUAL_TO2
        //系统联动[slSiz2]值:           0~LG_LINKAGE_NUM-1
        //系统联动[slSiz1]值
        public const int LG_SYSLKID_ACT_OFF = 0;          //关闭
        public const int LG_SYSLKID_ACT_ON = 1;          //打开
        public const int LG_SYSLKID_ACT_TOTAL = 2;          //总数
        public const int LG_SYSLKID_ACT_DEFAULT = LG_SYSLKID_ACT_ON;


        //安防联动[ucCmp ]值:           LG_MATH_EQUAL_AND_TRUE  → 下面的[slSiz1]值为单选bit ─┐
        //安防联动[slSiz1]值 ←────────────────────────────────┘
        public const int LG_SAF_SYST_DI = 0;          //系统被撤防         (系统全部的安防标志全部被清除)   ──┐
        public const int LG_SAF_SYST_EN_DLY = 1;          //系统进入布防延时中 (系统安防标志只要任一个被置位)       │
        public const int LG_SAF_SYST_EN = 2;          //系统进入布防       (系统安防标志只要任一个被置位)       ├←┐
        public const int LG_SAF_SYST_WAR = 3;          //系统触发预警                                            │  │
        public const int LG_SAF_SYST_ALM = 4;          //系统触发报警                                        ──┘  │补充说明:系统安防都是由[本机/它机]触发的
        public const int LG_SAF_SELF_DI = 5;          //本机被撤防         (本机关联的安防标志全部被清除)   ──┐  ↑
        public const int LG_SAF_SELF_EN_DLY = 6;          //本机进入布防延时中 (本机关联的安防标志只要任一个被置位) │  │
        public const int LG_SAF_SELF_EN = 7;          //本机进入布防       (本机关联的安防标志只要任一个被置位) ├─┘
        public const int LG_SAF_SELF_WAR = 8;          //本机触发预警                                            │
        public const int LG_SAF_SELF_ALM = 9;          //本机触发报警                                        ──┘
        public const int LG_SAF_TOTAL = 10;         //总数
        public const int LG_SAF_DEFAULT = LG_SAF_SELF_ALM;

        //------触发对象------
        public const string SENSOR_INVALID = "无效";
        public const string SENSOR_TEMPERATURE = "温度";
        public const string SENSOR_HUMIDITY = "湿度";
        public const string SENSOR_RAIN_SENSOR = "雨感";
        public const string SENSOR_WINDY = "风速";
        public const string SENSOR_LUMINANCE = "亮度";
        public const string SENSOR_FLAMMABLE_GAS_PROBE = "可燃气体探头";
        public const string SENSOR_RADAR = "雷达";
        public const string SENSOR_SWIT_TAMPER = "防拆开关";
        public const string SENSOR_SYSTEM_INTERACTION = "系统联动号";
        public const string SENSOR_SECURITY_INTERACTION = "安防联动";

        public const string SENSOR_TIME = "时间段";
        public const string SENSOR_DATE = "日期段";
        public const string SENSOR_WEEK = "周循环";
        public const string SENSOR_FIRE_TEMPERATURE = "消防温控";

        public const string SENSOR_VALUE_KIND_VALUE = "触发值";
        public const string SENSOR_VALUE_KIND_PERIPHERAL = "外设";
        public const string SENSOR_VALUE_KIND_LEVEL = "级别";
        public const string SENSOR_VALUE_KIND_SAME_UNIT = "同一个";
        public const string SENSOR_VALUE_KIND_SAME_TYPE = "同类型";
        public const string SENSOR_END_MASK = "结束";
        public const string SENSOR_DEFAULT = SENSOR_INVALID;



        

        public static Dictionary<string, UInt16> TRIGGER_NAME_ID = new Dictionary<string, UInt16>(); //-----触发对象对应的值---- 
        public static Dictionary<UInt16, string> TRIGGER_ID_NAME = new Dictionary<UInt16, string>(); //-----触发对象对应的值---- 
        public static Dictionary<string, UInt16> TRIGGER_KIND_NAME_ID = new Dictionary<string, UInt16>(); //-----触发级别ID对应的值---- 
        public static Dictionary<UInt16, string> TRIGGER_KIND_ID_NAME = new Dictionary<UInt16, string>(); //-----触发级别值对应的ID---- 
        public static Dictionary<string, UInt16> MATH_NAME_ID = new Dictionary<string, UInt16>(); //-----运算ID值---- 
        public static Dictionary<UInt16, string> MATH_ID_NAME = new Dictionary<UInt16, string>(); //-----运算ID对应的名称---- 
        static ViewConfig()
        {
            //-------传感器触发-------
            TRIGGER_NAME_ID.Add(SENSOR_INVALID, LG_SENSOR_VOID);
            TRIGGER_NAME_ID.Add(SENSOR_TEMPERATURE, LG_SENSOR_TEMP);
            TRIGGER_NAME_ID.Add(SENSOR_HUMIDITY, LG_SENSOR_HUMI);
            TRIGGER_NAME_ID.Add(SENSOR_RAIN_SENSOR, LG_SENSOR_RAIN);
            TRIGGER_NAME_ID.Add(SENSOR_WINDY, LG_SENSOR_WIND);
            TRIGGER_NAME_ID.Add(SENSOR_LUMINANCE, LG_SENSOR_LUMI);
            TRIGGER_NAME_ID.Add(SENSOR_FLAMMABLE_GAS_PROBE, LG_SENSOR_LEL);
            TRIGGER_NAME_ID.Add(SENSOR_RADAR, LG_SENSOR_RSP);
            TRIGGER_NAME_ID.Add(SENSOR_SWIT_TAMPER, LG_SENSOR_TAMPER);
            TRIGGER_NAME_ID.Add(SENSOR_SYSTEM_INTERACTION, LG_EXT_SENSOR_SYS_LKID);
            TRIGGER_NAME_ID.Add(SENSOR_SECURITY_INTERACTION, LG_EXT_SENSOR_SECURITY);
            TRIGGER_NAME_ID.Add(SENSOR_TIME, LG_EXT_SENSOR_TIME_SEG);
            TRIGGER_NAME_ID.Add(SENSOR_DATE, LG_EXT_SENSOR_DATE_SEG);
            TRIGGER_NAME_ID.Add(SENSOR_WEEK, LG_EXT_SENSOR_WEEK_CYC);
            TRIGGER_NAME_ID.Add(SENSOR_FIRE_TEMPERATURE, LG_DEV_SENSOR_TEMP);
            TRIGGER_ID_NAME.Add(LG_SENSOR_VOID, SENSOR_INVALID);
            TRIGGER_ID_NAME.Add(LG_SENSOR_TEMP, SENSOR_TEMPERATURE);
            TRIGGER_ID_NAME.Add(LG_SENSOR_HUMI, SENSOR_HUMIDITY);
            TRIGGER_ID_NAME.Add(LG_SENSOR_RAIN, SENSOR_RAIN_SENSOR);
            TRIGGER_ID_NAME.Add(LG_SENSOR_WIND, SENSOR_WINDY);
            TRIGGER_ID_NAME.Add(LG_SENSOR_LUMI, SENSOR_LUMINANCE);
            TRIGGER_ID_NAME.Add(LG_SENSOR_LEL, SENSOR_FLAMMABLE_GAS_PROBE);
            TRIGGER_ID_NAME.Add(LG_SENSOR_RSP, SENSOR_RADAR);
            TRIGGER_ID_NAME.Add(LG_SENSOR_TAMPER, SENSOR_SWIT_TAMPER);
            TRIGGER_ID_NAME.Add(LG_EXT_SENSOR_SYS_LKID, SENSOR_SYSTEM_INTERACTION);
            TRIGGER_ID_NAME.Add(LG_EXT_SENSOR_SECURITY, SENSOR_SECURITY_INTERACTION);
            TRIGGER_ID_NAME.Add(LG_EXT_SENSOR_TIME_SEG, SENSOR_TIME);
            TRIGGER_ID_NAME.Add(LG_EXT_SENSOR_DATE_SEG, SENSOR_DATE);
            TRIGGER_ID_NAME.Add(LG_EXT_SENSOR_WEEK_CYC, SENSOR_WEEK);
            TRIGGER_ID_NAME.Add(LG_DEV_SENSOR_TEMP, SENSOR_FIRE_TEMPERATURE);

            //-------触发类型-------
            TRIGGER_KIND_NAME_ID.Add(SENSOR_VALUE_KIND_PERIPHERAL, LG_SENSOR_DEV_FLAG);
            TRIGGER_KIND_NAME_ID.Add(SENSOR_VALUE_KIND_LEVEL, LG_SENSOR_LVL_FLAG);
            TRIGGER_KIND_NAME_ID.Add(SENSOR_VALUE_KIND_SAME_UNIT, LG_SENSOR_MASK);
            TRIGGER_KIND_NAME_ID.Add(SENSOR_VALUE_KIND_SAME_TYPE, LG_SENSOR_TYP_MASK);
            TRIGGER_KIND_NAME_ID.Add(SENSOR_END_MASK, LG_SENSOR_END_MARK);
            TRIGGER_KIND_NAME_ID.Add(SENSOR_VALUE_KIND_VALUE, LG_SENSOR_KIND_FLAG);
            TRIGGER_KIND_ID_NAME.Add(LG_SENSOR_DEV_FLAG, SENSOR_VALUE_KIND_PERIPHERAL);
            TRIGGER_KIND_ID_NAME.Add(LG_SENSOR_LVL_FLAG, SENSOR_VALUE_KIND_LEVEL);
            TRIGGER_KIND_ID_NAME.Add(LG_SENSOR_MASK, SENSOR_VALUE_KIND_SAME_UNIT);
            TRIGGER_KIND_ID_NAME.Add(LG_SENSOR_TYP_MASK, SENSOR_VALUE_KIND_SAME_TYPE);
            TRIGGER_KIND_ID_NAME.Add(LG_SENSOR_END_MARK, SENSOR_END_MASK);
            TRIGGER_KIND_ID_NAME.Add(LG_SENSOR_KIND_FLAG, SENSOR_VALUE_KIND_VALUE);
            
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
