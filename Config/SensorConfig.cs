﻿using System;
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
        public const int LG_SENSOR_TEMP_FC = 14;               //消防温控    (含有外设,含有等级)
        public const int LG_SENSOR_AQI = 15;        //空气质量     (含有外设,只有等级)
        public const int LG_SENSOR_TVOC = 16;         //TVOC有害气体 (含有外设,含有级别)
        public const int LG_SENSOR_CO2 = 17;         //二氧化碳     (含有外设,含有级别)
        public const int LG_SENSOR_CH2O = 18;         //甲醛         (含有外设,含有级别)
        public const int LG_EXT_SENSOR_SLF_LKID = 19;         //特殊:内部联动(                 )
        public const int LG_SENSOR_PM25 = 20;         //PM2.5        (含有外设,含有级别) 
        public const int LG_SENSOR_O2 = 21;         //氧气浓度     (含有外设,含有等级)
        public const int LG_SENSOR_UW_1 = 22;         //超声波-1     (含有外设,只有等级)
        public const int LG_SENSOR_UW_2 = 23;         //超声波-2     (含有外设,只有等级)
        public const int LG_SENSOR_IR = 24;         //红外         (含有外设,只有等级)
        public const int LG_SENSOR_SN_1_2 = 25;         //传感器顺序触发:1->2(   只有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_SN_1_2_3 = 26;        //传感器顺序触发:1->2->3(只有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_SCIN_1 = 27;         //短路输入1    (含有外设,只有等级)
        public const int LG_SENSOR_SCIN_2 = 28;         //短路输入2    (含有外设,只有等级)
        public const int LG_SENSOR_SCIN_3 = 29;         //短路输入3    (含有外设,只有等级)
        public const int LG_SENSOR_SCIN_4 = 30;         //短路输入4    (含有外设,只有等级)
        public const int LG_SENSOR_SCIN_5 = 31;         //短路输入5    (含有外设,只有等级)
        public const int LG_SENSOR_SCIN_6 = 32;         //短路输入6    (含有外设,只有等级)
        public const int LG_SENSOR_SCIN_7 = 33;         //短路输入7    (含有外设,只有等级)
        public const int LG_SENSOR_SCIN_8 = 34;         //短路输入8    (含有外设,只有等级)
        public const int LG_SENSOR_EL_1 = 35;         //漏电传感器1  (         只有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_EL_2 = 36;         //漏电传感器2  (         只有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_EL_3 = 37;         //漏电传感器3  (         只有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_EL_4 = 38;         //漏电传感器4  (         只有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_V_1 = 39;         //电压传感器1  (         含有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_V_2 = 40;         //电压传感器2  (         含有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_V_3 = 41;         //电压传感器3  (         含有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_V_4 = 42;         //电压传感器4  (         含有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_I_1 = 43;         //电流传感器1  (         含有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_I_2 = 44;         //电流传感器2  (         含有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_I_3 = 45;         //电流传感器3  (         含有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_I_4 = 46;         //电流传感器4  (         含有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_W_1 = 47;         //功率传感器1  (         含有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_W_2 = 48;         //功率传感器2  (         含有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_W_3 = 49;         //功率传感器3  (         含有等级)(目前只能设备内部使用)
        public const int LG_SENSOR_W_4 = 50;         //功率传感器4  (         含有等级)(目前只能设备内部使用)
        public const int LG_EXT_SENSOR_DATE_CYC = 51;         //特殊:日期循环(                 )
        public const int LG_SENSOR_WIND_DIR = 52;         //风向         (含有外设,含有等级)
        //public const int LG_SENSOR_TOTAL = 53;         //总数(★★数量以后会不断增加,必须在最尾处增加★★)
        public const int LG_SENSOR_SENONAUTOOFF = 53;         //感应开自动关 (含有外设,只有等级)(实质为短路输入)
        public const int LG_SENSOR_SENONOFF = 54;         //感应开关     (含有外设,只有等级)(实质为短路输入)
        public const int LG_SENSOR_KEY1 = 55;         //按键1        (含有外设,只有等级)(实质为短路输入)
        public const int LG_SENSOR_KEY2 = 56;         //按键2        (含有外设,只有等级)(实质为短路输入)
        public const int LG_SENSOR_SAFETYLC = 57;         //安全光栅     (含有外设,只有等级)(实质为短路输入)
        public const int LG_SENSOR_ALWAYSON = 58;         //常开模式     (含有外设,只有等级)(实质为短路输入)
        public const int LG_SENSOR_CLOSECHECK = 59;         //关门检测     (含有外设,只有等级)(实质为短路输入)
        public const int LG_SENSOR_OPENCHECK = 60;         //开门检测     (含有外设,只有等级)(实质为短路输入)

        public const int LG_SENSOR_DEV_FLAG = 0x8000;     //[外设]传感器[标志位]->如:本设备(bit=0),外设(bit=1) (补充说明:有[外设]且有[数值]时,才有[外设差值])
        public const int LG_SENSOR_LVL_FLAG = 0x4000;     //传感器[等级][标志位]->如:温度,27℃(数值(bit=0)),舒适(等级(bit=1))
        public const int LG_SENSOR_DIF_FLAG = 0x2000;     //传感器[差值][标志位]->只针对[本设备]与[外设]同类传感器的[差值],所以须要有[LG_SENSOR_DEV_FLAG]标志

        public const int LG_SENSOR_MASK = 0xBFFF;     //[同一个]传感器[掩码]->如:本设备的温度传感器,外设的温度传感器.之后要区分:传感原始值还是等级值
        public const int LG_SENSOR_TYP_MASK = 0x03FF;     //[同类型]传感器[掩码]->如:温度,湿度
        public const int LG_SENSOR_END_MARK = 0xFFFF;     //传感器结束符
        public const int LG_SENSOR_DEFAULT = LG_SENSOR_VOID;

        //---特殊值---
        public const UInt16 LG_SENSOR_DIF_FLAG_VALUE = 0xA000;  //外设差值  0x8000 | 0x2000
        public const UInt16 LG_SENSOR_DEF_FLAG = 0;        //级别默认值0
        public const Int16 LG_LINKAGE_NUM = 100;
        //------触发位置--------
        public const string SENSOR_POSITION_LOCAL = "本地";
        public const string SENSOR_POSITION_PERIPHERAL = "外设";
        public const string SENSOR_POSITION_PERIPHERAL_DIFFERENT = "外设(差值)";
        //------触发对象------
        public const string SENSOR_INVALID = "无效";
        public const string SENSOR_TEMPERATURE = "温度";
        public const string SENSOR_HUMIDITY = "湿度";
        public const string SENSOR_AQI = "空气质量";
        public const string SENSOR_TVOC = "TVOC有害气体";
        public const string SENSOR_CO2 = "二氧化碳";
        public const string SENSOR_CH20 = "甲醛";
        public const string SENSOR_PM25 = "PM2.5";
        public const string SENSOR_O2 = "氧气浓度";
        public const string SENSOR_RAIN = "雨感";
        public const string SENSOR_WINDY = "风速";
        public const string SENSOR_WINDYDIR = "风向";
        public const string SENSOR_LUMINANCE = "亮度";
        public const string SENSOR_FLAMMABLE_GAS_PROBE = "可燃气体探头";
        public const string SENSOR_RADAR = "雷达探头";
        public const string SENSOR_SWIT_TAMPER = "防拆开关";
        public const string SENSOR_SYSTEM_INTERACTION = "系统联动";
        public const string SENSOR_INNER_INTERACTION = "内部联动";
        public const string SENSOR_SECURITY_INTERACTION = "安防联动";
        public const string SENSOR_SCIN_1 = "短路输入1";
        public const string SENSOR_SCIN_2 = "短路输入2";
        public const string SENSOR_SCIN_3 = "短路输入3";
        public const string SENSOR_SCIN_4 = "短路输入4";
        public const string SENSOR_SCIN_5 = "短路输入5";
        public const string SENSOR_SCIN_6 = "短路输入6";
        public const string SENSOR_SCIN_7 = "短路输入7";
        public const string SENSOR_SCIN_8 = "短路输入8"; 
 

        public const string SENSOR_SENONAUTOOFF = "感应开自动关";
        public const string SENSOR_SENONOFF = "感应开关";
        public const string SENSOR_KEY1 = "按键1";
        public const string SENSOR_KEY2 = "按键2";
        public const string SENSOR_SAFETYLC = "安全光栅";
        public const string SENSOR_ALWAYSON = "常开模式";
        public const string SENSOR_CLOSECHECK = "关门到位";
        public const string SENSOR_OPENCHECK = "开门到位";
        public const string SENSOR_TIME = "时间段";
        public const string SENSOR_DATE = "日期段";
        public const string SENSOR_WEEK = "周循环";
        public const string SENSOR_FIRE_TEMPERATURE = "消防温控";
        //------人体感应------
        public const string SENSOR_UW_1 = "超声波-1";
        public const string SENSOR_UW_2 = "超声波-2";
        public const string SENSOR_IR = "红外感应";
        public const string SENSOR_SN_1_2 = "顺序触发1->2";
        public const string SENSOR_SN_1_2_3 = "顺序触发1->2->3";


        public const string SENSOR_VALUE_KIND_VALUE = "数值";
        public const string SENSOR_VALUE_KIND_LEVEL = "等级";
        public const string SENSOR_VALUE_KIND_SAME_UNIT = "同一个";
        public const string SENSOR_VALUE_KIND_SAME_TYPE = "同类型";
        public const string SENSOR_END_MASK = "结束";
        public const string SENSOR_DEFAULT = SENSOR_INVALID;

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
        public const int LG_MATH_EQUAL_AND_TRUE2 = 7;          //等于("与"运算后如果为"真")   (判断[slSiz1]和[slSiz2]) <- if (val2 &  slSiz2)  { if (val1 &  slSiz1)  {逻辑1} else {逻辑0}}
        public const int LG_MATH_TOTAL = 8;//等于("与"运算后如果为"真")   (判断[slSiz1]和[slSiz2]) <- if (val2 &  slSiz2)  { if (val1 &  slSiz1)  {逻辑1} else {逻辑0}}
        public const int LG_MATH_DEFAULT = LG_MATH_EQUAL_TO;

        public const string LG_MATH_NAME_EQUAL_TO = "等于";          //等于(只判断[slSiz1])                                  <- if (val1 == slSiz1)
        public const string LG_MATH_NAME_LESS_THAN = "小于";          //小于(只判断[slSiz1])                                  <- if (val1 <  slSiz1)
        public const string LG_MATH_NAME_GREATER_THAN = "大于";          //大于(只判断[slSiz1])                                  <- if (val1 >  slSiz1)
        public const string LG_MATH_NAME_WITHIN = "以内";          //以内(范围内)(包括两边的数值) (判断[slSiz1]和[slSiz2]) <- if (val1 >= slSiz1) && if (val1 <= slSiz2)
        public const string LG_MATH_NAME_WITHOUT = "以外";          //以外(范围外)                 (判断[slSiz1]和[slSiz2]) <- if (val1 <  slSiz1) || if (val1 >  slSiz2)
        public const string LG_MATH_NAME_EQUAL_TO2 = "等于";          //等于(判断[slSiz1]和[slSiz2])                          <- if (val2 == slSiz2)  { if (val1 == slSiz1) }
        public const string LG_MATH_NAME_EQUAL_AND_TRUE = "等于";          //等于("与"运算后如果为"真") (只判断[slSiz1])           <- if (val1 &  slSiz1)
        public const string LG_MATH_NAME_TOTAL = "相加";
        public const string LG_MATH_NAME_DEFAULT = LG_MATH_NAME_EQUAL_TO;


        //系统联动[ucCmp ]值:           LG_MATH_EQUAL_TO2
        //系统联动[slSiz2]值:           0~LG_LINKAGE_NUM-1
        //系统联动[slSiz1]值
        public const int LG_SYSLKID_ACT_OFF = 0;          //关闭
        public const int LG_SYSLKID_ACT_ON = 1;          //打开
        public const int LG_SYSLKID_ACT_TOTAL = 2;          //总数
        public const int LG_SYSLKID_ACT_DEFAULT = LG_SYSLKID_ACT_ON;
        public const string LG_SYSLKID_NAME_ACT_OFF = "关闭";          //关闭
        public const string LG_SYSLKID_NAME_ACT_ON = "打开";          //打开



        // [温度]级别 <- LG_SENSOR_TEMP
        public const int TEMP_LV_COLD_H = 0;          //【寒冷】
        public const int TEMP_LV_COLD_L = 1;          //【较冷】
        public const int TEMP_LV_COMF = 2;          //【舒适】
        public const int TEMP_LV_HOT_L = 3;          //【较热】
        public const int TEMP_LV_HOT_H = 4;          //【炎热】
        public const int TEMP_LV_TOTAL = 5;
        public const string TEMP_LV_NAME_COLD_H = "寒冷";          //【寒冷】
        public const string TEMP_LV_NAME_COLD_L = "较冷";          //【较冷】
        public const string TEMP_LV_NAME_COMF = "舒适";          //【舒适】
        public const string TEMP_LV_NAME_HOT_L = "较热";          //【较热】
        public const string TEMP_LV_NAME_HOT_H = "炎热";          //【炎热】 

        // [湿度]级别 <- LG_SENSOR_HUMI
        public const int HUMI_LV_ARID_H = 0;          //【干燥】
        public const int HUMI_LV_ARID_L = 1;          //【稍燥】
        public const int HUMI_LV_OPTIMUM = 2;          //【适宜】
        public const int HUMI_LV_MOIST_L = 3;          //【较湿】
        public const int HUMI_LV_MOIST_H = 4;          //【潮湿】
        public const int HUMI_LV_TOTAL = 5;
        public const string HUMI_LV_NAME_ARID_H = "干燥";          //【干燥】
        public const string HUMI_LV_NAME_ARID_L = "稍燥";          //【稍燥】
        public const string HUMI_LV_NAME_OPTIMUM = "适宜";          //【适宜】
        public const string HUMI_LV_NAME_MOIST_L = "较湿";          //【较湿】
        public const string HUMI_LV_NAME_MOIST_H = "潮湿";          //【潮湿】

        // [雨感]级别 <- LG_SENSOR_RAIN
        public const int RAIN_LV_NOT = 0;          //【无雨】
        public const int RAIN_LV_OWN = 1;          //【有雨】
        public const int RAIN_LV_TOTAL = 2;
        public const string RAIN_LV_NAME_NOT = "无雨";          //【无雨】
        public const string RAIN_LV_NAME_OWN = "有雨";          //【有雨】

        // [风速]级别 <- LG_SENSOR_WIND
        public const int WIND_LV_NOT = 0;          //【无风】
        public const int WIND_LV_TINY = 1;          //【微风】
        public const int WIND_LV_SMALL = 2;          //【小风】
        public const int WIND_LV_HIGH = 3;          //【大风】
        public const int WIND_LV_TOTAL = 4;
        public const string WIND_LV_NAME_NOT = "无风";          //【无风】
        public const string WIND_LV_NAME_TINY = "微风";          //【微风】
        public const string WIND_LV_NAME_SMALL = "小风";          //【小风】
        public const string WIND_LV_NAME_HIGH = "大风";          //【大风】

        // [亮度]级别 <- LG_SENSOR_LUMI
        public const int LUMI_LV_NIGHT = 0;          //【黑夜】
        public const int LUMI_LV_DIM = 1;          //【昏暗】
        public const int LUMI_LV_BRIGHT = 2;          //【明亮】
        public const int LUMI_LV_DAY = 3;          //【白天】
        public const int LUMI_LV_SUNLIGHT = 4;          //【日照】
        public const int LUMI_LV_TOTAL = 5;
        public const string LUMI_LV_NAME_NIGHT = "黑夜";          //【黑夜】
        public const string LUMI_LV_NAME_DIM = "昏暗";          //【昏暗】
        public const string LUMI_LV_NAME_BRIGHT = "明亮";          //【明亮】
        public const string LUMI_LV_NAME_DAY = "白天";          //【白天】
        public const string LUMI_LV_NAME_SUNLIGHT = "日照";          //【日照】

        // [可燃气探头]级别 <- LG_SENSOR_LEL
        public const int LEL_LV_NORMAL = 0;          //【正常】
        public const int LEL_LV_TRIGGERED = 1;          //【泄漏】
        public const int LEL_LV_TOTAL = 2;
        public const string LEL_LV_NAME_NORMAL = "正常";          //【正常】
        public const string LEL_LV_NAME_TRIGGERED = "泄漏";          //【泄漏】
        // [雷达]级别 <- LG_SENSOR_RSP
        public const int RSP_LV_NORMAL = 0;          //【正常】
        public const int RSP_LV_TRIGGERED = 1;          //【触发】
        public const int RSP_LV_TOTAL = 2;
        public const string RSP_LV_NAME_NORMAL = "正常";          //【正常】
        public const string RSP_LV_NAME_TRIGGERED = "触发";          //【触发】

        // [防拆开关]级别 <- LG_SENSOR_TAMPER
        public const int TAMPER_LV_NORMAL = 0;          //【正常】
        public const int TAMPER_LV_TRIGGERED = 1;          //【被拆】
        public const int TAMPER_LV_TOTAL = 2;
        public const string TAMPER_LV_NAME_NORMAL = "正常";          //【正常】
        public const string TAMPER_LV_NAME_TRIGGERED = "被拆";          //【被拆】

        // [消防温控]级别 <- LG_SENSOR_TEMP_FC
        public const int TEMPFC_LV_NORMAL = 0;          //【正常】
        public const int TEMPFC_LV_HIGH = 1;          //【高温】
        public const int TEMPFC_LV_FIRE = 2;          //【火灾】
        public const int TEMPFC_LV_TOTAL = 3;
        public const string TEMPFC_LV_NAME_NORMAL = "正常";          //【正常】
        public const string TEMPFC_LV_NAME_HIGH = "高温";          //【高温】
        public const string TEMPFC_LV_NAME_FIRE = "火灾";          //【火灾】


        // [空气质量]等级 <- LG_SENSOR_AQI
        public const int AQI_LV_POOR = 0;          //【差】
        public const int AQI_LV_MODERATE = 1;          //【良】
        public const int AQI_LV_GOOD = 2;          //【优】
        public const int AQI_LV_TOTAL = 3;
        public const string AQI_LV_NAME_POOR = "差";          //【差】
        public const string AQI_LV_NAME_MODERATE = "良";          //【良】
        public const string AQI_LV_NAME_GOOD = "优";          //【优】

        // [TVOC有害气体]等级 <- LG_SENSOR_TVOC
        public const int TVOC_LV_POOR = 0;          //【差】
        public const int TVOC_LV_MODERATE = 1;          //【良】
        public const int TVOC_LV_GOOD = 2;          //【优】
        public const int TVOC_LV_TOTAL = 3;
        public const string TVOC_LV_NAME_POOR = "差";          //【差】
        public const string TVOC_LV_NAME_MODERATE = "良";          //【良】
        public const string TVOC_LV_NAME_GOOD = "优";          //【优】

        // [二氧化碳]等级 <- LG_SENSOR_CO2
        public const int CO2_LV_POOR = 0;          //【差】
        public const int CO2_LV_MODERATE = 1;          //【良】
        public const int CO2_LV_GOOD = 2;          //【优】
        public const int CO2_LV_TOTAL = 3;
        public const string CO2_LV_NAME_POOR = "差";          //【差】
        public const string CO2_LV_NAME_MODERATE = "良";          //【良】
        public const string CO2_LV_NAME_GOOD = "优";          //【优】

        // [甲醛]等级 <- LG_SENSOR_CH2O
        public const int CH2O_LV_POOR = 0;          //【差】
        public const int CH2O_LV_MODERATE = 1;          //【良】
        public const int CH2O_LV_GOOD = 2;          //【优】
        public const int CH2O_LV_TOTAL = 3;
        public const string CH2O_LV_NAME_POOR = "差";          //【差】
        public const string CH2O_LV_NAME_MODERATE = "良";          //【良】
        public const string CH2O_LV_NAME_GOOD = "优";          //【优】

        // [PM2.5]等级 <- LG_SENSOR_PM25
        public const int PM25_LV_POOR = 0;          //【差】
        public const int PM25_LV_MODERATE = 1;         //【良】
        public const int PM25_LV_GOOD = 2;          //【优】
        public const int PM25_LV_TOTAL = 3;
        public const string PM25_LV_NAME_POOR = "差";          //【差】
        public const string PM25_LV_NAME_MODERATE = "良";          //【良】
        public const string PM25_LV_NAME_GOOD = "优";          //【优】

        // [氧气浓度]等级 <- LG_SENSOR_O2
        public const int O2_LV_LOW = 0;          //【过低】
        public const int O2_LV_MEDIUM = 1;          //【适宜】
        public const int O2_LV_HIGH = 2;          //【富氧】
        public const int O2_LV_TOTAL = 3;
        public const string O2_LV_NAME_LOW = "过低";          //【过低】
        public const string O2_LV_NAME_MEDIUM = "适宜";          //【适宜】
        public const string O2_LV_NAME_HIGH = "富氧";          //【富氧】

        //短路输入
        public const int SCIN_LV_LOW = 0;          //【低电平】
        public const int SCIN_LV_HIGH = 1;          //【高电平】
        public const int SCIN_LV_TOTAL = 2;
        public const string SCIN_LV_NAME_LOW = "低电平";
        public const string SCIN_LV_NAME_HIGH = "高电平";
        public const string SCIN_LV_NAME_NONE = "不动作";

        // [超声波]等级 <- LG_SENSOR_UW_x
        public const int UW_LV_NORMAL = 0;          //【正常】
        public const int UW_LV_TRIGGERED = 1;          //【触发】
        public const int UW_LV_TOTAL = 2;
        public const string UW_LV_NAME_LOW = "正常";
        public const string UW_LV_NAME_HIGH = "触发";

        // [红外]等级 <- LG_SENSOR_IR
        public const int IR_LV_NORMAL = 0;          //【正常】
        public const int IR_LV_TRIGGERED = 1;          //【触发】
        public const int IR_LV_TOTAL = 2;
        public const string IR_LV_NAME_LOW = "正常";
        public const string IR_LV_NAME_HIGH = "触发";

        // [传感器顺序触发]等级 <- LG_SENSOR_SN_1_2, LG_SENSOR_SN_1_2_3
        public const int SN_LV_NORMAL = 0;          //【正常】
        public const int SN_LV_TRIGGERED = 1;          //【触发】
        public const int SN_LV_TOTAL = 2;
        public const string SN_LV_NAME_LOW = "正常";
        public const string SN_LV_NAME_HIGH = "触发";

        //风向传感器
        public const int WINDDIR_LV_N = 0;          //【北风      】
        public const int WINDDIR_LV_NNE = 1;         //【东北偏北风】
        public const int WINDDIR_LV_NE = 2;          //【东北风    】
        public const int WINDDIR_LV_ENE = 3;          //【东北偏东风】
        public const int WINDDIR_LV_E = 4;          //【东风      】
        public const int WINDDIR_LV_ESE = 5;          //【东南偏东风】
        public const int WINDDIR_LV_SE = 6;          //【东南风    】
        public const int WINDDIR_LV_SSE = 7;          //【东南偏南风】
        public const int WINDDIR_LV_S = 8;          //【南风      】
        public const int WINDDIR_LV_SSW = 9;          //【西南偏南风】
        public const int WINDDIR_LV_SW = 10;         //【西南风    】
        public const int WINDDIR_LV_WSW = 11;         //【西南偏西风】
        public const int WINDDIR_LV_W = 12;         //【西风      】
        public const int WINDDIR_LV_WNW = 13;         //【西北偏西风】
        public const int WINDDIR_LV_NW = 14;         //【西北风    】
        public const int WINDDIR_LV_NNW = 15;         //【西北偏北风】
        public const int WINDDIR_LV_TOTAL = 16;

        public const string WINDDIR_LV_N_NAME = "北风";          //【北风      】
        public const string WINDDIR_LV_NNE_NAME = "东北偏北风";         //【东北偏北风】
        public const string WINDDIR_LV_NE_NAME = "东北风";          //【东北风    】
        public const string WINDDIR_LV_ENE_NAME = "东北偏东风";          //【东北偏东风】
        public const string WINDDIR_LV_E_NAME = "东风";          //【东风      】
        public const string WINDDIR_LV_ESE_NAME = "东南偏东风";          //【东南偏东风】
        public const string WINDDIR_LV_SE_NAME = "东南风";          //【东南风    】
        public const string WINDDIR_LV_SSE_NAME = "东南偏南风";          //【东南偏南风】
        public const string WINDDIR_LV_S_NAME = "南风";          //【南风      】
        public const string WINDDIR_LV_SSW_NAME = "西南偏南风";          //【】
        public const string WINDDIR_LV_SW_NAME = "西南风";         //【西南风    】
        public const string WINDDIR_LV_WSW_NAME = "西南偏西风";         //【西南偏西风】
        public const string WINDDIR_LV_W_NAME = "西风";         //【西风      】
        public const string WINDDIR_LV_WNW_NAME = "西北偏西风";         //【西北偏西风】
        public const string WINDDIR_LV_NW_NAME = "西北风";         //【西北风    】
        public const string WINDDIR_LV_NNW_NAME = "西北偏北风";         //【西北偏北风】
        public const string WINDDIR_LV_TOTAL_NAME = "";


        //-----安防联动级别----
        public const int LG_SAF_SYST_DI = 0x1;          //系统被撤防         (系统全部的安防标志全部被清除)   ──┐
        public const int LG_SAF_SYST_EN_DLY = 0x2;          //系统进入布防延时中 (系统安防标志只要任一个被置位)       │
        public const int LG_SAF_SYST_EN = 0x4;          //系统进入布防       (系统安防标志只要任一个被置位)       ├←┐
        public const int LG_SAF_SYST_WAR = 0x8;          //系统触发预警                                            │  │
        public const int LG_SAF_SYST_ALM = 0x10;          //系统触发报警                                        ──┘  │补充说明:系统安防都是由[本机/它机]触发的
        public const int LG_SAF_SELF_DI = 0x20;          //本机被撤防         (本机关联的安防标志全部被清除)   ──┐  ↑
        public const int LG_SAF_SELF_EN_DLY = 0x40;          //本机进入布防延时中 (本机关联的安防标志只要任一个被置位) │  │
        public const int LG_SAF_SELF_EN = 0x80;         //本机进入布防       (本机关联的安防标志只要任一个被置位) ├─┘
        public const int LG_SAF_SELF_WAR = 0x100;          //本机触发预警                                            │
        public const int LG_SAF_SELF_ALM = 0x200;          //本机触发报警                                        ──┘
        public const int LG_SAF_TOTAL = 10;         //总数
        public const int LG_SAF_DEFAULT = LG_SAF_SELF_ALM;
        public const string LG_NAME_SAF_SYST_DI = "系统被撤防";          //         (系统全部的安防标志全部被清除)   ──┐
        public const string LG_NAME_SAF_SYST_EN_DLY = "系统进入布防延时中";          // (系统安防标志只要任一个被置位)       │
        public const string LG_NAME_SAF_SYST_EN = "系统进入布防";          //       (系统安防标志只要任一个被置位)       ├←┐
        public const string LG_NAME_SAF_SYST_WAR = "系统触发预警";          //                                            │  │
        public const string LG_NAME_SAF_SYST_ALM = "系统触发报警";          //                                        ──┘  │补充说明:系统安防都是由[本机/它机]触发的
        public const string LG_NAME_SAF_SELF_DI = "本机被撤防";         //         (本机关联的安防标志全部被清除)   ──┐  ↑
        public const string LG_NAME_SAF_SELF_EN_DLY = "本机进入布防延时中";       // (本机关联的安防标志只要任一个被置位) │  │
        public const string LG_NAME_SAF_SELF_EN = "本机进入布防";        //       (本机关联的安防标志只要任一个被置位) ├─┘
        public const string LG_NAME_SAF_SELF_WAR = "本机触发预警";          //                                            │
        public const string LG_NAME_SAF_SELF_ALM = "本机触发报警";          //         

    }

}