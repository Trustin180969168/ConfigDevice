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
        public const string DC_KIND_NAME = "KindName";//内容类型
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
        public const string DC_DEVICE_KIND = "DeviceKind";//设备类型
        public const string DC_DEVICE_KIND_ID = "DeviceKindID";//设备类型ID
        public const string DC_LOGIC_VALUE = "LogicValue";//逻辑配置值
        public const string DC_LOGIC_ADDITION_VALUE = "LogicAddtionValue";//逻辑附加值
        public const string DC_SENSOR_NAME = "Name";//传感器名称
        public const string DC_SENSOR_VALUE = "Value";//传感器值
        public const string DC_SENSOR_LEVEL = "Level";//传感器级别
        public const string DC_STATE = "State";//状态
        public const string DC_CURRENT = "Current";//电流
        public const string DC_MAC = "Mac";//mac地址
        public const string DC_DELETE = "Delete";//删除
        public const string DC_ADD = "Add";//增加
        public const string DC_CLEAR = "Clear";//清除

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

        public const string DC_CONTROL_OBJ = "ControlObj";//控制对象
        public const string DC_CONTROL_KIND = "ControlKind";//控制类型
        public const string DC_COMMUNICATE_KIND = "CommunicateKind";//通信模式
        public const string DC_DIRECTION_MAX = "DirectionMax";//方向最大值
        public const string DC_DIRECTION_MIN = "DirectionMin";//方向最小值
        public const string DC_DIRECTION_STEP = "DirectionStep";//方向步进
        public const string DC_RELEVANCE_NUM = "RelevanceNum";//关联号
        public const string DC_MUTEX_NUM = "MutexNum";//互斥号 
        public const string NAME_INVALID_DEVICE = "未知设备";
        public const string NAME_INVALID_KIND = "未知类型";
        //--------按键类型定义----------
        public const byte KEY_TYPE_LIGHT = 0;//--灯光
        public const byte KEY_TYPE_SOUND = 16;//--音响
        public const byte KEY_TYPE_CURTAIN = 32;//--窗帘
        public const byte KEY_TYPE_HELP = 48;//--求助
        public const string KEY_TYPE_NAME_LIGHT = "灯光";//--灯光
        public const string KEY_TYPE_NAME_SOUND = "音响";//--音响
        public const string KEY_TYPE_NAME_CURTAIN = "窗帘";//--窗帘
        public const string KEY_TYPE_NAME_HELP = "求助";//--求助

        public const int VIRKEY_TYPE_NULL = 0x00;  //非特殊类型
        public const int VIRKEY_TYPE_SCENE = 0x10;  //场景；     //例如：回/离家、空间消毒、求助
        public const int VIRKEY_TYPE_AMPLIFIER = 0x20;  //音响
        public const int VIRKEY_TYPE_LIGHT = 0x30;  //灯         //独立设备
        public const int VIRKEY_TYPE_LIGHT_ON = 0x31;  //灯-开┐    //当出现配置两个按键，所有灯开，所有灯关时
        public const int VIRKEY_TYPE_LIGHT_OFF = 0x32;  //灯-关┘
        public const int VIRKEY_TYPE_CURTAIN = 0x40;  //窗帘
        public const int VIRKEY_TYPE_CURTAIN_ON = 0x41;  //窗帘-开┐
        public const int VIRKEY_TYPE_CURTAIN_OFF = 0x42;  //窗帘-关┘
        public const int VIRKEY_TYPE_DOOR = 0x50;  //门
        public const int VIRKEY_TYPE_DOOR_ON = 0x51;  //门-开┐
        public const int VIRKEY_TYPE_DOOR_OFF = 0x52;  //门-关┘
        public const int VIRKEY_TYPE_WINDOW = 0x60;  //窗
        public const int VIRKEY_TYPE_WINDOW_ON = 0x61;  //窗-开┐
        public const int VIRKEY_TYPE_WINDOW_OFF = 0x62;  //窗-关┘
        public const int VIRKEY_TYPE_FINDING = 0x70;  //找物 

        public const string VIRKEY_TYPE_NULL_NAME = "非特殊类型";
        public const string VIRKEY_TYPE_SCENE_NAME = "场景";    //例如：回/离家、空间消毒、求助
        public const string VIRKEY_TYPE_AMPLIFIER_NAME = "音响";
        public const string VIRKEY_TYPE_LIGHT_NAME = "灯";         //独立设备
        public const string VIRKEY_TYPE_LIGHT_ON_NAME = "灯-开";    //当出现配置两个按键，所有灯开，所有灯关时
        public const string VIRKEY_TYPE_LIGHT_OFF_NAME = "灯-关";  //灯-关┘
        public const string VIRKEY_TYPE_CURTAIN_NAME = "窗帘";  //
        public const string VIRKEY_TYPE_CURTAIN_ON_NAME = "窗帘-开";
        public const string VIRKEY_TYPE_CURTAIN_OFF_NAME = "窗帘-关";
        public const string VIRKEY_TYPE_DOOR_NAME = "门";  //
        public const string VIRKEY_TYPE_DOOR_ON_NAME = "门-开";
        public const string VIRKEY_TYPE_DOOR_OFF_NAME = "门-关";
        public const string VIRKEY_TYPE_WINDOW_NAME = "窗";  //
        public const string VIRKEY_TYPE_WINDOW_ON_NAME = "窗-开";
        public const string VIRKEY_TYPE_WINDOW_OFF_NAME = "窗-关";
        public const string VIRKEY_TYPE_FINDING_NAME = "找物";  //

        //-------按键的控制类型选择-----
        public const string KEY_CONTROL_KIND_NAME_OPEN_CLOSE = "开关";
        public const string KEY_CONTROL_KIND_NAME_OPEN = "开";
        public const string KEY_CONTROL_KIND_NAME_CLOSE = "关";
        public const string KEY_CONTROL_KIND_NAME_LOOP_LIGHT = "循环调光";
        public const string KEY_CONTROL_KIND_NAME_OPEN_CLOSE_LOOP_LIGHT = "开关+循环调光";
        public const string KEY_CONTROL_KIND_NAME_OPEN_LOOP_LIGHT = "开+循环调光";
        public const string KEY_CONTROL_KIND_NAME_CLOSE_LOOP_LIGHT = "关+循环调光";

        //-------液晶面板标题-----
        public const string LCD_CAPTION_SCENE = DeviceConfig.CONTROL_OBJECT_SCENE_NAME;
        public const string LCD_CAPTION_LIGHT = "灯光";
        public const string LCD_CAPTION_CURTAIN = "窗帘";
        public const string LCD_CAPTION_LEAVE_BACK = "离、回家";

        //--------按键布防选择-----
        public const string KEY_LEAVE_SAFE_OUTSIDE = "室外布防";
        public const string KEY_LEAVE_SAFE_ALL = "全部布防";
        public const string KET_LEAVE_DOOR_WINDOW_SHOW = "门窗显示";
        public const string KET_LEAVE_DOOR_WINDOW_HINT_SOUND = "门窗提示音";
        public const string KEY_BACK_SAFE_NONE = "回家撤防";
        public const string KEY_SAFE_NONE = "不关联";
        public const string KEY_BACK_SAFE_ALARM_SOUND = "预警报提示音";
        public const string KET_BACK_SAFE_RED_SWIT = "红外线开关";
        public const string KET_BACK_SAFE_LCD_SAVE = "时间屏保";
        public const string KET_BACK_DOOR_WINDOW_SHOW = "门窗显示 关窗提示音";
        public const string KET_BACK_DOOR_WINDOW_HINT_SOUND = "门窗显示 锁窗提示音";



        public static Dictionary<string, UInt16> TRIGGER_NAME_ID = new Dictionary<string, UInt16>(); //-----触发对象对应的ID---- 
        public static Dictionary<UInt16, string> TRIGGER_ID_NAME = new Dictionary<UInt16, string>(); //-----触发对象ID对应的值---- 
        public static Dictionary<string, UInt16> TRIGGER_KIND_NAME_ID = new Dictionary<string, UInt16>(); //-----触发级别ID对应的值---- 
        public static Dictionary<UInt16, string> TRIGGER_KIND_ID_NAME = new Dictionary<UInt16, string>(); //-----触发级别值对应的ID---- 
        public static Dictionary<string, UInt16> MATH_NAME_ID = new Dictionary<string, UInt16>(); //-----运算ID值---- 
        public static Dictionary<UInt16, string> MATH_ID_NAME = new Dictionary<UInt16, string>(); //-----运算ID对应的名称---- 
        public static Dictionary<string, UInt16> TRIGGER_POSITION_NAME_ID = new Dictionary<string, UInt16>(); //-----触发位置ID---- 
        public static Dictionary<UInt16, string> TRIGGER_POSITION_ID_NAME = new Dictionary<UInt16, string>(); //-----触发位置名称---- 
        public static Dictionary<string, byte> KEY_TYPE_NAME_ID = new Dictionary<string, byte>(); //-----按键类型名称ID---- 
        public static Dictionary<byte, string> KEY_TYPE_ID_NAME = new Dictionary<byte, string>(); //-----按键类型ID名称---- 

        public static Dictionary<string, int> VIRKEY_TYPE_NAME_ID = new Dictionary<string, int>(); //-----按键类型名称ID---- 
        public static Dictionary<int, string> VIRKEY_TYPE_ID_NAME = new Dictionary<int, string>(); //-----按键类型ID名称---- 




        public static string SELECT_COMMAND_DEVICE_QUERY_CONDITION = DeviceConfig.DC_KIND_ID + " in (" +
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
                   "'" + (int)DeviceConfig.EQUIPMENT_EL_CUPBOARD + "'" +
            //"'" + (int)DeviceConfig.EQUIPMENT_SERVER + "'" +
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

        /// <summary>
        /// 传感器设备查询条件
        /// </summary>
        public static string SELECT_ENVIRONMENT_DEVICE_QUERY_CONDITION = DeviceConfig.DC_KIND_ID + " in (" +

           "'" + (int)DeviceConfig.EQUIPMENT_AIR_QUALITY + "'" +
           ")";

        /// <summary>
        /// 专用面板设备选择
        /// </summary>
        public static string SELECT_DEVICE_PANEL_SPECIAL_QUERY_CONDITION = DeviceConfig.DC_KIND_ID + " in (" +

           "'" + (int)DeviceConfig.EQUIPMENT_EL_CUPBOARD + "'" +
           ")";
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
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_WINDYDIR, SensorConfig.LG_SENSOR_WIND_DIR);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_LUMINANCE, SensorConfig.LG_SENSOR_LUMI);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_FLAMMABLE_GAS_PROBE, SensorConfig.LG_SENSOR_LEL);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_RADAR, SensorConfig.LG_SENSOR_RSP);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SWIT_TAMPER, SensorConfig.LG_SENSOR_TAMPER);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SYSTEM_INTERACTION, SensorConfig.LG_EXT_SENSOR_SYS_LKID);//系统联动
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_INNER_INTERACTION, SensorConfig.LG_EXT_SENSOR_SLF_LKID);//内部联动
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SECURITY_INTERACTION, SensorConfig.LG_EXT_SENSOR_SECURITY);//---安防联动
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_TIME, SensorConfig.LG_EXT_SENSOR_TIME_SEG);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_DATE, SensorConfig.LG_EXT_SENSOR_DATE_SEG);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_WEEK, SensorConfig.LG_EXT_SENSOR_WEEK_CYC);
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_FIRE_TEMPERATURE, SensorConfig.LG_SENSOR_TEMP_FC);

            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SCIN_1, SensorConfig.LG_SENSOR_SCIN_1); //短路输入1    (含有外设,只有等级)
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SCIN_2, SensorConfig.LG_SENSOR_SCIN_2); //短路输入2    (含有外设,只有等级)
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SCIN_3, SensorConfig.LG_SENSOR_SCIN_3); //短路输入3    (含有外设,只有等级)
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SCIN_4, SensorConfig.LG_SENSOR_SCIN_4); //短路输入4    (含有外设,只有等级)
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SCIN_5, SensorConfig.LG_SENSOR_SCIN_5); //短路输入5    (含有外设,只有等级)
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SCIN_6, SensorConfig.LG_SENSOR_SCIN_6); //短路输入6    (含有外设,只有等级)
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SCIN_7, SensorConfig.LG_SENSOR_SCIN_7); //短路输入7    (含有外设,只有等级)
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SCIN_8, SensorConfig.LG_SENSOR_SCIN_8); //短路输入8    (含有外设,只有等级)


            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SENONAUTOOFF, SensorConfig.LG_SENSOR_SENONAUTOOFF); //感应开自动关     （注：对应短路输入1）
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SENONOFF, SensorConfig.LG_SENSOR_SENONOFF); // 感应开关         （注：对应短路输入2）
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_KEY1, SensorConfig.LG_SENSOR_KEY1); //按键1           （注：对应短路输入3）
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_KEY2, SensorConfig.LG_SENSOR_KEY2); //按键2           （注：对应短路输入4）
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SAFETYLC, SensorConfig.LG_SENSOR_SAFETYLC); //安全光栅         （注：对应短路输入5）
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_ALWAYSON, SensorConfig.LG_SENSOR_ALWAYSON); //常开模式         （注：对应短路输入6）
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_CLOSECHECK, SensorConfig.LG_SENSOR_CLOSECHECK); //关门到位         （注：对应短路输入7）
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_OPENCHECK, SensorConfig.LG_SENSOR_OPENCHECK); //开门到位         （注：对应短路输入8）


            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_UW_1, SensorConfig.LG_SENSOR_UW_1);//        超声波-1
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_UW_2, SensorConfig.LG_SENSOR_UW_2);//        超声波-2
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_IR, SensorConfig.LG_SENSOR_IR);//          红外感应  
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SN_1_2, SensorConfig.LG_SENSOR_SN_1_2);//      顺序触发1->2
            TRIGGER_NAME_ID.Add(SensorConfig.SENSOR_SN_1_2_3, SensorConfig.LG_SENSOR_SN_1_2_3);//    顺序触发1->2->3 

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
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_WIND_DIR, SensorConfig.SENSOR_WINDYDIR);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_LUMI, SensorConfig.SENSOR_LUMINANCE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_TVOC, SensorConfig.SENSOR_TVOC);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_LEL, SensorConfig.SENSOR_FLAMMABLE_GAS_PROBE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_RSP, SensorConfig.SENSOR_RADAR);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_TAMPER, SensorConfig.SENSOR_SWIT_TAMPER);//----防拆开关---
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_SYS_LKID, SensorConfig.SENSOR_SYSTEM_INTERACTION);//系统联动
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_SLF_LKID, SensorConfig.SENSOR_INNER_INTERACTION);//内部联动
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_SECURITY, SensorConfig.SENSOR_SECURITY_INTERACTION);//---安防联动
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_TIME_SEG, SensorConfig.SENSOR_TIME);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_DATE_SEG, SensorConfig.SENSOR_DATE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_EXT_SENSOR_WEEK_CYC, SensorConfig.SENSOR_WEEK);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_TEMP_FC, SensorConfig.SENSOR_FIRE_TEMPERATURE);
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SCIN_1, SensorConfig.SENSOR_SCIN_1); //短路输入1    (含有外设,只有等级)
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SCIN_2, SensorConfig.SENSOR_SCIN_2); //短路输入2    (含有外设,只有等级)
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SCIN_3, SensorConfig.SENSOR_SCIN_3); //短路输入3    (含有外设,只有等级)
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SCIN_4, SensorConfig.SENSOR_SCIN_4); //短路输入4    (含有外设,只有等级)
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SCIN_5, SensorConfig.SENSOR_SCIN_5); //短路输入5    (含有外设,只有等级)
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SCIN_6, SensorConfig.SENSOR_SCIN_6); //短路输入6    (含有外设,只有等级)
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SCIN_7, SensorConfig.SENSOR_SCIN_7); //短路输入7    (含有外设,只有等级)
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SCIN_8, SensorConfig.SENSOR_SCIN_8); //短路输入8    (含有外设,只有等级)

            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SENONAUTOOFF, SensorConfig.SENSOR_SENONAUTOOFF); //感应开自动关     （注：对应短路输入1）
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SENONOFF, SensorConfig.SENSOR_SENONOFF); // 感应开关         （注：对应短路输入2）
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_KEY1, SensorConfig.SENSOR_KEY1); //按键1           （注：对应短路输入3）
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_KEY2, SensorConfig.SENSOR_KEY2); //按键2           （注：对应短路输入4）
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SAFETYLC, SensorConfig.SENSOR_SAFETYLC); //安全光栅         （注：对应短路输入5）
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_ALWAYSON, SensorConfig.SENSOR_ALWAYSON); //常开模式         （注：对应短路输入6）
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_CLOSECHECK, SensorConfig.SENSOR_CLOSECHECK); //关门到位         （注：对应短路输入7）
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_OPENCHECK, SensorConfig.SENSOR_OPENCHECK); //开门到位         （注：对应短路输入8）

            //-----人体感应-----
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_UW_1, SensorConfig.SENSOR_UW_1);//        超声波-1
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_UW_2, SensorConfig.SENSOR_UW_2);//        超声波-2
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_IR, SensorConfig.SENSOR_IR);//          红外感应  
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SN_1_2, SensorConfig.SENSOR_SN_1_2);//      顺序触发1->2
            TRIGGER_ID_NAME.Add(SensorConfig.LG_SENSOR_SN_1_2_3, SensorConfig.SENSOR_SN_1_2_3);//    顺序触发1->2->3 

            //------触发位置的对应关系--------
            TRIGGER_POSITION_NAME_ID.Add(SensorConfig.SENSOR_INVALID, SensorConfig.LG_SENSOR_VOID);//-------无效-----
            TRIGGER_POSITION_NAME_ID.Add(SensorConfig.SENSOR_POSITION_LOCAL, SensorConfig.LG_SENSOR_VOID);//-------本地-----
            TRIGGER_POSITION_NAME_ID.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL, SensorConfig.LG_SENSOR_DEV_FLAG);//----外设----
            TRIGGER_POSITION_NAME_ID.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT, SensorConfig.LG_SENSOR_DIF_FLAG_VALUE);//----外设差值----
            TRIGGER_POSITION_ID_NAME.Add(0, SensorConfig.SENSOR_POSITION_LOCAL);//-------本地-----
            TRIGGER_POSITION_ID_NAME.Add(SensorConfig.LG_SENSOR_DEV_FLAG, SensorConfig.SENSOR_POSITION_PERIPHERAL);//----外设----
            TRIGGER_POSITION_ID_NAME.Add(SensorConfig.LG_SENSOR_DIF_FLAG_VALUE, SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT);//----外设差值----
            //-------触发类型-------
            TRIGGER_KIND_NAME_ID.Add(SensorConfig.SENSOR_INVALID, SensorConfig.LG_SENSOR_VOID);//-------无效-----
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
            MATH_ID_NAME.Add(SensorConfig.LG_MATH_EQUAL_AND_TRUE2, SensorConfig.LG_MATH_NAME_EQUAL_AND_TRUE);
            MATH_ID_NAME.Add(SensorConfig.LG_MATH_TOTAL, SensorConfig.LG_MATH_NAME_TOTAL);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO, SensorConfig.LG_MATH_EQUAL_TO);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_LESS_THAN, SensorConfig.LG_MATH_LESS_THAN);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_GREATER_THAN, SensorConfig.LG_MATH_GREATER_THAN);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_WITHIN, SensorConfig.LG_MATH_WITHIN);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_WITHOUT, SensorConfig.LG_MATH_WITHOUT);
            MATH_NAME_ID.Add(SensorConfig.LG_MATH_NAME_TOTAL, SensorConfig.LG_MATH_TOTAL);

            //-------控制键类型关系------
            KEY_TYPE_NAME_ID.Add(KEY_TYPE_NAME_LIGHT, KEY_TYPE_LIGHT);
            KEY_TYPE_NAME_ID.Add(KEY_TYPE_NAME_SOUND, KEY_TYPE_SOUND);
            KEY_TYPE_NAME_ID.Add(KEY_TYPE_NAME_CURTAIN, KEY_TYPE_CURTAIN);
            KEY_TYPE_NAME_ID.Add(KEY_TYPE_NAME_HELP, KEY_TYPE_HELP);
            KEY_TYPE_ID_NAME.Add(KEY_TYPE_LIGHT, KEY_TYPE_NAME_LIGHT);
            KEY_TYPE_ID_NAME.Add(KEY_TYPE_SOUND, KEY_TYPE_NAME_SOUND);
            KEY_TYPE_ID_NAME.Add(KEY_TYPE_CURTAIN, KEY_TYPE_NAME_CURTAIN);
            KEY_TYPE_ID_NAME.Add(KEY_TYPE_HELP, KEY_TYPE_NAME_HELP);
            //-------控制键类型(V2)关系------
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_NULL_NAME, VIRKEY_TYPE_NULL);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_SCENE_NAME, VIRKEY_TYPE_SCENE);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_AMPLIFIER_NAME, VIRKEY_TYPE_AMPLIFIER);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_LIGHT_NAME, VIRKEY_TYPE_LIGHT);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_LIGHT_ON_NAME, VIRKEY_TYPE_LIGHT_ON);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_LIGHT_OFF_NAME, VIRKEY_TYPE_LIGHT_OFF);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_CURTAIN_NAME, VIRKEY_TYPE_CURTAIN);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_CURTAIN_ON_NAME, VIRKEY_TYPE_CURTAIN_ON);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_CURTAIN_OFF_NAME, VIRKEY_TYPE_CURTAIN_OFF);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_DOOR_NAME, VIRKEY_TYPE_DOOR);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_DOOR_ON_NAME, VIRKEY_TYPE_DOOR_ON);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_DOOR_OFF_NAME, VIRKEY_TYPE_DOOR_OFF);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_WINDOW_NAME, VIRKEY_TYPE_WINDOW);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_WINDOW_ON_NAME, VIRKEY_TYPE_WINDOW_ON);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_WINDOW_OFF_NAME, VIRKEY_TYPE_WINDOW_OFF);
            VIRKEY_TYPE_NAME_ID.Add(VIRKEY_TYPE_FINDING_NAME, VIRKEY_TYPE_FINDING);

            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_NULL, VIRKEY_TYPE_NULL_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_SCENE, VIRKEY_TYPE_SCENE_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_AMPLIFIER, VIRKEY_TYPE_AMPLIFIER_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_LIGHT, VIRKEY_TYPE_LIGHT_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_LIGHT_ON, VIRKEY_TYPE_LIGHT_ON_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_LIGHT_OFF, VIRKEY_TYPE_LIGHT_OFF_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_CURTAIN, VIRKEY_TYPE_CURTAIN_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_CURTAIN_ON, VIRKEY_TYPE_CURTAIN_ON_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_CURTAIN_OFF, VIRKEY_TYPE_CURTAIN_OFF_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_DOOR, VIRKEY_TYPE_DOOR_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_DOOR_ON, VIRKEY_TYPE_DOOR_ON_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_DOOR_OFF, VIRKEY_TYPE_DOOR_OFF_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_WINDOW, VIRKEY_TYPE_WINDOW_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_WINDOW_ON, VIRKEY_TYPE_WINDOW_ON_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_WINDOW_OFF, VIRKEY_TYPE_WINDOW_OFF_NAME);
            VIRKEY_TYPE_ID_NAME.Add(VIRKEY_TYPE_FINDING, VIRKEY_TYPE_FINDING_NAME);
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
    public class GridViewComboBox : DevExpress.XtraEditors.Repository.RepositoryItemComboBox
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
            IsFloatValue = false;
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
            this.Name = "lookupEdit";
            this.ShowFooter = false;
        }
    }

    /// <summary>
    /// 下拉表查找选择
    /// </summary>
    public class GridViewGridLookupEdit : DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    {


        public GridViewGridLookupEdit()
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
            this.PopupFormMinSize = new System.Drawing.Size(50, 200);
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



    /// <summary>
    /// 多项选择
    /// </summary>
    public class GridViewMultipleCheckEdit : DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit
    {
        private int count = 0;//下拉选项数量
        /// <summary>
        /// 选择行的前序
        /// </summary>
        private string prefixName = "";
        public string PrefixName
        {
            get { return prefixName; }
            set
            {
                prefixName = value;
                this.Items.Clear();
                for (int i = 1; i <= count; i++)
                    this.Items.Add(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(prefixName + i));
            }
        }
        private string weekValue = "";
        private bool allowEdit = true;
        public GridViewMultipleCheckEdit(string _prefixName, int _count)
            : base()
        {
            this.Name = "MultipleCheckEdit";
            this.count = _count;
            PrefixName = _prefixName;
            if (count >= 30)
                this.PopupFormMinSize = new System.Drawing.Size(150, 25 * 30);
            else
                this.PopupFormMinSize = new System.Drawing.Size(150, 22 * count);
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
                if ((sender as CheckedComboBoxEdit).Text.Contains(prefixName))
                {
                    weekValue = (sender as CheckedComboBoxEdit).Text;
                    (sender as CheckedComboBoxEdit).Text = weekValue;
                    (sender as CheckedComboBoxEdit).Text = (sender as CheckedComboBoxEdit).Text.Replace(prefixName, "");
                }
            }
            else
            {
                weekValue = "";
                (sender as CheckedComboBoxEdit).Text = "";
                (sender as CheckedComboBoxEdit).Text = (sender as CheckedComboBoxEdit).Text.Replace(prefixName, "");
            }
        }

        private void checkedComboBoxEdit_Closed(object sender, ClosedEventArgs e)
        {
            allowEdit = false;

            if ((sender as CheckedComboBoxEdit).Text.Contains(prefixName))
            {
                weekValue = (sender as CheckedComboBoxEdit).Text;
                (sender as CheckedComboBoxEdit).Text = weekValue;
                (sender as CheckedComboBoxEdit).Text = (sender as CheckedComboBoxEdit).Text.Replace(prefixName, "");
            }
            allowEdit = true;
        }



        private void checkedComboBoxEdit_Properties_QueryPopUp(object sender, CancelEventArgs e)
        {
            allowEdit = false;
            if ((sender as CheckedComboBoxEdit).Text == "") return;

            string temp1 = (sender as CheckedComboBoxEdit).EditValue.ToString().Replace(", ", ", " + prefixName);
            string temp2 = prefixName + temp1;
            (sender as CheckedComboBoxEdit).Text = temp2;
            allowEdit = true;
        }

    }




}
