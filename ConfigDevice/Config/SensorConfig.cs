using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class SensorConfig
    {
        // 设备[内部条件][外部条件]类型 (低10bit传感器类型,高6bit特别标志位)---------------------
        public const int LG_SENSOR_VOID = 0;                    //无效(★不能改动★)
        public const int LG_SENSOR_TEMP = 1;                    //内部条件:温度   (含有外设,含有等级)
        public const int LG_SENSOR_HUMI = 2;                    //内部条件:湿度   (含有外设,含有等级)
        public const int LG_SENSOR_RAIN = 3;                    //内部条件:雨感   (含有外设         )
        public const int LG_SENSOR_WIND = 4;                    //内部条件:风速   (含有外设,含有等级)
        public const int LG_SENSOR_LUMI = 5;                    //内部条件:亮度   (含有外设,含有等级)
        public const int LG_SENSOR_LEL = 6;                     //内部条件:可燃气探头  (含有外设         )
        public const int LG_SENSOR_RSP = 7;                     //内部条件:雷达    (含有外设         )
        public const int LG_SENSOR_TAMPER = 8;                  //内部条件:防拆开关 (含有外设         )
        public const int LG_EXT_SENSOR_SYS_LKID = 9;            //外部条件:系统联动
        public const int LG_EXT_SENSOR_SECURITY = 10;           //外部条件:安防联动
        public const int LG_EXT_SENSOR_TIME_SEG = 11;           //外部条件:时间段
        public const int LG_EXT_SENSOR_DATE_SEG = 12;           //外部条件:日期段
        public const int LG_EXT_SENSOR_WEEK_CYC = 13;           //外部条件:周循环
        public const int LG_DEV_SENSOR_TEMP = 14;               //消防温控    (含有外设,含有等级)
        public const int LG_SENSOR_TOTAL = 15;                  //总数(★★数量以后会不断增加,必须在最尾处增加★★)
        public const UInt16 LG_SENSOR_KIND_FLAG = 0;            //默认触发级别
        public const UInt16 LG_SENSOR_DEV_FLAG = 0x8000;        //[外设]传感器[标志位]->如:本设备,外设
        public const UInt16 LG_SENSOR_LVL_FLAG = 0x4000;        //传感器[级别][标志位]->如:温度,27℃(数值),舒适(级别)
        public const UInt16 LG_SENSOR_DIF_FLAG = 0x2000;     //传感器[差值][标志位]->只针对[本设备]与[外设]同类传感器的[差值],所以须要有[LG_SENSOR_DEV_FLAG]标志
        public const UInt16 LG_SENSOR_MASK = 0xBFFF;            //[同一个]传感器[掩码]->如:本设备的温度传感器,外设的温度传感器
        public const UInt16 LG_SENSOR_TYP_MASK = 0x3FFF;        //[同类型]传感器[掩码]->如:温度,湿度
        public const UInt16 LG_SENSOR_END_MARK = 0xFFFF;        //传感器结束符 
        public const UInt16 LG_SENSOR_DEFAULT = LG_SENSOR_VOID;
        //------触发位置--------
        public const string POSITION_LOCAL = "本地";
        public const string POSITION_PERIPHERAL = "外设";
        public const string POSITION_PERIPHERAL_DIFFERENT = "外设差异";
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
        public const string SENSOR_VALUE_KIND_LEVEL = "等级";
        public const string SENSOR_VALUE_KIND_SAME_UNIT = "同一个";
        public const string SENSOR_VALUE_KIND_SAME_TYPE = "同类型";
        public const string SENSOR_END_MASK = "结束";
        public const string SENSOR_DEFAULT = SENSOR_INVALID;

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

        // [温度]级别 <- LG_SENSOR_TEMP
        public const int TEMP_LV_COLD_H = 0;          //【寒冷】
        public const int TEMP_LV_COLD_L = 1;          //【小冷】
        public const int TEMP_LV_COMF = 2;          //【舒适】
        public const int TEMP_LV_HOT_L = 3;          //【小热】
        public const int TEMP_LV_HOT_H = 4;          //【酷热】
        public const int TEMP_LV_TOTAL = 5;

        // [湿度]级别 <- LG_SENSOR_HUMI
        public const int HUMI_LV_ARID_H = 0;          //【干燥】
        public const int HUMI_LV_ARID_L = 1;          //【微燥】
        public const int HUMI_LV_OPTIMUM = 2;          //【适宜】
        public const int HUMI_LV_MOIST_L = 3;          //【微湿】
        public const int HUMI_LV_MOIST_H = 4;          //【潮湿】
        public const int HUMI_LV_TOTAL = 5;

        // [雨感]级别 <- LG_SENSOR_RAIN
        public const int RAIN_LV_NOT = 0;          //【无雨】
        public const int RAIN_LV_OWN = 1;          //【有雨】
        public const int RAIN_LV_TOTAL = 2;

        // [风速]级别 <- LG_SENSOR_WIND
        public const int WIND_LV_NOT = 0;         //【无风】
        public const int WIND_LV_TINY = 1;          //【微风】
        public const int WIND_LV_SMALL = 2;          //【小风】
        public const int WIND_LV_HIGH = 3;          //【大风】
        public const int WIND_LV_TOTAL = 4;

        // [亮度]级别 <- LG_SENSOR_LUMI
        public const int LUMI_LV_NIGHT = 0;          //【黑夜】
        public const int LUMI_LV_DIM = 1;          //【昏暗】
        public const int LUMI_LV_BRIGHT = 2;          //【明亮】
        public const int LUMI_LV_DAY = 3;          //【白天】
        public const int LUMI_LV_SUNLIGHT = 4;          //【日照】
        public const int LUMI_LV_TOTAL = 5;

        // [可燃气探头]级别 <- LG_SENSOR_LEL
        public const int LEL_LV_NORMAL = 0;          //【正常】
        public const int LEL_LV_TRIGGERED = 1;          //【泄漏】
        public const int LEL_LV_TOTAL = 2;

        // [雷达]级别 <- LG_SENSOR_RSP
        public const int RSP_LV_NORMAL = 0;          //【正常】
        public const int RSP_LV_TRIGGERED = 1;          //【触发】
        public const int RSP_LV_TOTAL = 2;

        // [防拆开关]级别 <- LG_SENSOR_TAMPER
        public const int TAMPER_LV_NORMAL = 0;          //【正常】
        public const int TAMPER_LV_TRIGGERED = 1;          //【被拆】
        public const int TAMPER_LV_TOTAL = 2;

        // [消防温控]级别 <- LG_SENSOR_TEMP_FC
        public const int TEMPFC_LV_NORMAL = 0;          //【正常】
        public const int TEMPFC_LV_HIGH = 1;          //【高温】
        public const int TEMPFC_LV_FIRE = 2;          //【火灾】
        public const int TEMPFC_LV_TOTAL = 3;

        // [空气质量]级别 <- LG_SENSOR_AQI
        public const int AQI_LV_GOOD = 0;          //【优】
        public const int AQI_LV_MODERATE = 1;          //【良】
        public const int AQI_LV_POOR = 2;          //【差】
        public const int AQI_LV_TOTAL = 3;

        // [TVOC有害气体]级别 <- LG_SENSOR_TVOC
        public const int TVOC_LV_LOW = 0;          //【低】
        public const int TVOC_LV_MEDIUM = 1;          //【中】
        public const int TVOC_LV_HIGH = 2;          //【高】
        public const int TVOC_LV_TOTAL = 3;

        // [二氧化碳]级别 <- LG_SENSOR_CO2
        public const int CO2_LV_LOW = 0;          //【低】
        public const int CO2_LV_MEDIUM = 1;          //【中】
        public const int CO2_LV_HIGH = 2;          //【高】
        public const int CO2_LV_TOTAL = 3;

        // [甲醛]级别 <- LG_SENSOR_CH2O
        public const int CH2O_LV_LOW = 0;          //【低】
        public const int CH2O_LV_MEDIUM = 1;          //【中】
        public const int CH2O_LV_HIGH = 2;          //【高】
        public const int CH2O_LV_TOTAL = 3;

    }

}
