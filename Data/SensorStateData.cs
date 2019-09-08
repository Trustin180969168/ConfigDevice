﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    /// <summary>
    /// 抽象传感器状态
    /// </summary>
    public abstract class SensorStateData
    {
        public UInt16 KindID;
        public byte LevelID;
        public byte Retain;
        public Int16 Value;
        public string KindName = "";
        public string Unit = "";//---单位----
       
        public Dictionary<int, string> LevelIDName = new Dictionary<int, string>();//级别ID表
        public Dictionary<string,int> LevelNameID = new Dictionary<string,int>();//级别名称
        public string LevelValue//----级别名称值----
        {
            get
            {
                if (LevelIDName.ContainsKey(this.LevelID))
                    return LevelIDName[this.LevelID];
                else
                    return "";
            }
        }
        public string ValueAndUnit { get { return Value.ToString() + " " + Unit; } }//---值及单位----
        public SensorStateData(byte[] value)
        {
            KindID = ConvertTools.Bytes2ToUInt16(value[0], value[1]);
            LevelID = value[2];
            Retain = value[3];
            Value = ConvertTools.Bytes2ToInt16(value[4], value[5]);
        }
        public SensorStateData()
        {

        } 

        /// <summary>
        /// 获取对象体的值
        /// </summary>
        /// <returns></returns>
        public virtual byte[] GetValue()
        {
            byte[] byteValue = new byte[12];
            Buffer.BlockCopy(ConvertTools.GetByteFromUInt16(KindID),0,byteValue,0,2);
            byteValue[3]=LevelID;
            byteValue[4] = Retain;
            Buffer.BlockCopy(ConvertTools.GetByteFromInt16(Value),0,byteValue,0,2);
            return byteValue;
        }
        public abstract void Init();//----具体对象初始化-----
    }

    /// <summary>
    /// 消防温控传感器
    /// </summary>
    public class FireControlTemperatureSensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        
        static FireControlTemperatureSensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.TEMPFC_LV_NORMAL, SensorConfig.TEMPFC_LV_NAME_NORMAL);
            LEVEL_ID_NAME.Add(SensorConfig.TEMPFC_LV_HIGH, SensorConfig.TEMPFC_LV_NAME_HIGH);
            LEVEL_ID_NAME.Add(SensorConfig.TEMPFC_LV_FIRE, SensorConfig.TEMPFC_LV_NAME_FIRE);
            LEVEL_NAME_ID.Add(SensorConfig.TEMPFC_LV_NAME_NORMAL, SensorConfig.TEMPFC_LV_NORMAL);
            LEVEL_NAME_ID.Add(SensorConfig.TEMPFC_LV_NAME_HIGH, SensorConfig.TEMPFC_LV_HIGH);
            LEVEL_NAME_ID.Add(SensorConfig.TEMPFC_LV_NAME_FIRE, SensorConfig.TEMPFC_LV_FIRE);
        }
        public FireControlTemperatureSensor(byte[] value):base(value)        
        {
            Init();
        }

        public FireControlTemperatureSensor()
        {
            Init();
        }
        public override void Init()
        {
            Unit = "℃";
            KindName = "消防温控";
            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        } 
    }

    /// <summary>
    /// 可燃气探头
    /// </summary>
    public class FlamableGasProbeSensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        static FlamableGasProbeSensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.LEL_LV_NORMAL, SensorConfig.LEL_LV_NAME_NORMAL);
            LEVEL_ID_NAME.Add(SensorConfig.LEL_LV_TRIGGERED, SensorConfig.LEL_LV_NAME_TRIGGERED);
            LEVEL_NAME_ID.Add(SensorConfig.LEL_LV_NAME_NORMAL, SensorConfig.LEL_LV_NORMAL);
            LEVEL_NAME_ID.Add(SensorConfig.LEL_LV_NAME_TRIGGERED, SensorConfig.LEL_LV_TRIGGERED);
        }
        public FlamableGasProbeSensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public FlamableGasProbeSensor()
        {
            Init();
        }
        public override void Init()
        {
            KindName = "可燃气探头";
            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        } 

    }


    /// <summary>
    /// 温度传感器
    /// </summary>
    public class TemperatureSensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        static TemperatureSensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.TEMP_LV_COLD_H, SensorConfig.TEMP_LV_NAME_COLD_H);
            LEVEL_ID_NAME.Add(SensorConfig.TEMP_LV_COLD_L, SensorConfig.TEMP_LV_NAME_COLD_L);
            LEVEL_ID_NAME.Add(SensorConfig.TEMP_LV_COMF, SensorConfig.TEMP_LV_NAME_COMF);
            LEVEL_ID_NAME.Add(SensorConfig.TEMP_LV_HOT_L, SensorConfig.TEMP_LV_NAME_HOT_L);
            LEVEL_ID_NAME.Add(SensorConfig.TEMP_LV_HOT_H, SensorConfig.TEMP_LV_NAME_HOT_H);
 
            LEVEL_NAME_ID.Add(SensorConfig.TEMP_LV_NAME_COLD_H, SensorConfig.TEMP_LV_COLD_H);
            LEVEL_NAME_ID.Add(SensorConfig.TEMP_LV_NAME_COLD_L, SensorConfig.TEMP_LV_COLD_L);
            LEVEL_NAME_ID.Add(SensorConfig.TEMP_LV_NAME_COMF, SensorConfig.TEMP_LV_COMF);
            LEVEL_NAME_ID.Add(SensorConfig.TEMP_LV_NAME_HOT_L, SensorConfig.TEMP_LV_HOT_L);
            LEVEL_NAME_ID.Add(SensorConfig.TEMP_LV_NAME_HOT_H, SensorConfig.TEMP_LV_HOT_H); 
        }
        public TemperatureSensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public TemperatureSensor()
        {
            Init();
        }
        public override void Init()
        {
            KindName = "温度传感器";
            Unit = "℃"; 

            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }

    }



    /// <summary>
    /// 湿度传感器
    /// </summary>
    public class HumiditySensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        static HumiditySensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.HUMI_LV_ARID_H, SensorConfig.HUMI_LV_NAME_ARID_H);
            LEVEL_ID_NAME.Add(SensorConfig.HUMI_LV_ARID_L, SensorConfig.HUMI_LV_NAME_ARID_L);
            LEVEL_ID_NAME.Add(SensorConfig.HUMI_LV_OPTIMUM, SensorConfig.HUMI_LV_NAME_OPTIMUM);
            LEVEL_ID_NAME.Add(SensorConfig.HUMI_LV_MOIST_L, SensorConfig.HUMI_LV_NAME_MOIST_L);
            LEVEL_ID_NAME.Add(SensorConfig.HUMI_LV_MOIST_H, SensorConfig.HUMI_LV_NAME_MOIST_H);

            LEVEL_NAME_ID.Add(SensorConfig.HUMI_LV_NAME_ARID_H, SensorConfig.HUMI_LV_ARID_H);
            LEVEL_NAME_ID.Add(SensorConfig.HUMI_LV_NAME_ARID_L, SensorConfig.HUMI_LV_ARID_L);
            LEVEL_NAME_ID.Add(SensorConfig.HUMI_LV_NAME_OPTIMUM, SensorConfig.HUMI_LV_OPTIMUM);
            LEVEL_NAME_ID.Add(SensorConfig.HUMI_LV_NAME_MOIST_L, SensorConfig.HUMI_LV_MOIST_L);
            LEVEL_NAME_ID.Add(SensorConfig.HUMI_LV_NAME_MOIST_H, SensorConfig.HUMI_LV_MOIST_H);
        }
        public HumiditySensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public HumiditySensor()
        {
            Init();
        }
 

        public override void Init()
        {
            KindName = "湿度传感器";
            Unit = "%";


            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }

    }


    /// <summary>
    /// 雨水传感器
    /// </summary>
    public class RainSensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        static RainSensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.RAIN_LV_NOT, SensorConfig.RAIN_LV_NAME_NOT);
            LEVEL_ID_NAME.Add(SensorConfig.RAIN_LV_OWN, SensorConfig.RAIN_LV_NAME_OWN);
            LEVEL_NAME_ID.Add(SensorConfig.RAIN_LV_NAME_NOT, SensorConfig.RAIN_LV_NOT);
            LEVEL_NAME_ID.Add(SensorConfig.RAIN_LV_NAME_OWN, SensorConfig.RAIN_LV_OWN); 
        }

        public RainSensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public RainSensor()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "雨水传感器";

            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
        
    }

    /// <summary>
    /// 风速传感器
    /// </summary>
    public class WindySensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();

        static WindySensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.WIND_LV_NOT, SensorConfig.WIND_LV_NAME_NOT);
            LEVEL_ID_NAME.Add(SensorConfig.WIND_LV_TINY, SensorConfig.WIND_LV_NAME_TINY);
            LEVEL_ID_NAME.Add(SensorConfig.WIND_LV_SMALL, SensorConfig.WIND_LV_NAME_SMALL);
            LEVEL_ID_NAME.Add(SensorConfig.WIND_LV_HIGH, SensorConfig.WIND_LV_NAME_HIGH);

            LEVEL_NAME_ID.Add(SensorConfig.WIND_LV_NAME_NOT, SensorConfig.WIND_LV_NOT);
            LEVEL_NAME_ID.Add(SensorConfig.WIND_LV_NAME_TINY, SensorConfig.WIND_LV_TINY);
            LEVEL_NAME_ID.Add(SensorConfig.WIND_LV_NAME_SMALL, SensorConfig.WIND_LV_SMALL);
            LEVEL_NAME_ID.Add(SensorConfig.WIND_LV_NAME_HIGH, SensorConfig.WIND_LV_HIGH);
        }

        public WindySensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public WindySensor()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "风速传感器";
            Unit = "米/秒";


            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }

    }

    /// <summary>
    /// 风向传感器
    /// </summary>
    public class WindDirSensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();

        static WindDirSensor()
        {

            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_N, SensorConfig.WINDDIR_LV_N_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_NNE, SensorConfig.WINDDIR_LV_NNE_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_NE, SensorConfig.WINDDIR_LV_NE_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_ENE, SensorConfig.WINDDIR_LV_ENE_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_E, SensorConfig.WINDDIR_LV_E_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_ESE, SensorConfig.WINDDIR_LV_ESE_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_SE, SensorConfig.WINDDIR_LV_SE_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_SSE, SensorConfig.WINDDIR_LV_SSE_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_S, SensorConfig.WINDDIR_LV_S_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_SSW, SensorConfig.WINDDIR_LV_SSW_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_SW, SensorConfig.WINDDIR_LV_SW_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_WSW, SensorConfig.WINDDIR_LV_WSW_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_W, SensorConfig.WINDDIR_LV_W_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_WNW, SensorConfig.WINDDIR_LV_WNW_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_NW, SensorConfig.WINDDIR_LV_NW_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_NNW, SensorConfig.WINDDIR_LV_NNW_NAME);
            LEVEL_ID_NAME.Add(SensorConfig.WINDDIR_LV_TOTAL, SensorConfig.WINDDIR_LV_TOTAL_NAME);

            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_N_NAME, SensorConfig.WINDDIR_LV_N);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_NNE_NAME, SensorConfig.WINDDIR_LV_NNE);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_NE_NAME, SensorConfig.WINDDIR_LV_NE);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_ENE_NAME, SensorConfig.WINDDIR_LV_ENE);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_E_NAME, SensorConfig.WINDDIR_LV_E);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_ESE_NAME, SensorConfig.WINDDIR_LV_ESE);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_SE_NAME, SensorConfig.WINDDIR_LV_SE);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_SSE_NAME, SensorConfig.WINDDIR_LV_SSE);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_S_NAME, SensorConfig.WINDDIR_LV_S);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_SSW_NAME, SensorConfig.WINDDIR_LV_SSW);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_SW_NAME, SensorConfig.WINDDIR_LV_SW);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_WSW_NAME, SensorConfig.WINDDIR_LV_WSW);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_W_NAME, SensorConfig.WINDDIR_LV_W);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_WNW_NAME, SensorConfig.WINDDIR_LV_WNW);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_NW_NAME, SensorConfig.WINDDIR_LV_NW);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_NNW_NAME, SensorConfig.WINDDIR_LV_NNW);
            LEVEL_NAME_ID.Add(SensorConfig.WINDDIR_LV_TOTAL_NAME, SensorConfig.WINDDIR_LV_TOTAL); 
  
        }

        public WindDirSensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public WindDirSensor()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "风向传感器";
            Unit = " °";


            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }

    }

    /// <summary>
    /// 亮度传感器
    /// </summary>
    public class LuminanceSensorData : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        static LuminanceSensorData()
        {
            LEVEL_ID_NAME.Add(SensorConfig.LUMI_LV_NIGHT, SensorConfig.LUMI_LV_NAME_NIGHT);
            LEVEL_ID_NAME.Add(SensorConfig.LUMI_LV_DIM, SensorConfig.LUMI_LV_NAME_DIM);
            LEVEL_ID_NAME.Add(SensorConfig.LUMI_LV_BRIGHT, SensorConfig.LUMI_LV_NAME_BRIGHT);
            LEVEL_ID_NAME.Add(SensorConfig.LUMI_LV_DAY, SensorConfig.LUMI_LV_NAME_DAY);
            LEVEL_ID_NAME.Add(SensorConfig.LUMI_LV_SUNLIGHT, SensorConfig.LUMI_LV_NAME_SUNLIGHT);

            LEVEL_NAME_ID.Add(SensorConfig.LUMI_LV_NAME_NIGHT, SensorConfig.LUMI_LV_NIGHT);
            LEVEL_NAME_ID.Add(SensorConfig.LUMI_LV_NAME_DIM, SensorConfig.LUMI_LV_DIM);
            LEVEL_NAME_ID.Add(SensorConfig.LUMI_LV_NAME_BRIGHT, SensorConfig.LUMI_LV_BRIGHT);
            LEVEL_NAME_ID.Add(SensorConfig.LUMI_LV_NAME_DAY, SensorConfig.LUMI_LV_DAY);
            LEVEL_NAME_ID.Add(SensorConfig.LUMI_LV_NAME_SUNLIGHT, SensorConfig.LUMI_LV_SUNLIGHT);
        }
        public LuminanceSensorData(byte[] value)
            : base(value)
        {
            Init();
        }

        public LuminanceSensorData()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "亮度传感器";
            Unit = "Lux";


            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
        
    }


    /// <summary>
    /// 空气质量传感器
    /// </summary>
    public class AQISensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        static AQISensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.AQI_LV_POOR, SensorConfig.AQI_LV_NAME_POOR);
            LEVEL_ID_NAME.Add(SensorConfig.AQI_LV_MODERATE, SensorConfig.AQI_LV_NAME_MODERATE);
            LEVEL_ID_NAME.Add(SensorConfig.AQI_LV_GOOD, SensorConfig.AQI_LV_NAME_GOOD); 

            LEVEL_NAME_ID.Add(SensorConfig.AQI_LV_NAME_POOR, SensorConfig.AQI_LV_POOR);
            LEVEL_NAME_ID.Add(SensorConfig.AQI_LV_NAME_MODERATE, SensorConfig.AQI_LV_MODERATE);
            LEVEL_NAME_ID.Add(SensorConfig.AQI_LV_NAME_GOOD, SensorConfig.AQI_LV_GOOD); 
        }
        public AQISensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public AQISensor()
        {
            Init();
        }
 
        public override void Init()
        {
            KindName = "空气质量传感器";



            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }


    /// <summary>
    /// 有害气体传感器
    /// </summary>
    public class TVOCSensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        static TVOCSensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.TVOC_LV_POOR, SensorConfig.TVOC_LV_NAME_POOR);
            LEVEL_ID_NAME.Add(SensorConfig.TVOC_LV_MODERATE, SensorConfig.TVOC_LV_NAME_MODERATE);
            LEVEL_ID_NAME.Add(SensorConfig.TVOC_LV_GOOD, SensorConfig.TVOC_LV_NAME_GOOD);

            LEVEL_NAME_ID.Add(SensorConfig.TVOC_LV_NAME_POOR, SensorConfig.TVOC_LV_POOR);
            LEVEL_NAME_ID.Add(SensorConfig.TVOC_LV_NAME_MODERATE, SensorConfig.TVOC_LV_MODERATE);
            LEVEL_NAME_ID.Add(SensorConfig.TVOC_LV_NAME_GOOD, SensorConfig.TVOC_LV_GOOD);
        }
        public TVOCSensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public TVOCSensor()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "有害气体传感器(总挥发性有机化合物)";
            Unit = "ppb";
            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }


    /// <summary>
    /// 二氧化碳
    /// </summary>
    public class CO2Sensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        static CO2Sensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.CO2_LV_POOR, SensorConfig.CO2_LV_NAME_POOR);
            LEVEL_ID_NAME.Add(SensorConfig.CO2_LV_MODERATE, SensorConfig.CO2_LV_NAME_MODERATE);
            LEVEL_ID_NAME.Add(SensorConfig.CO2_LV_GOOD, SensorConfig.CO2_LV_NAME_GOOD);

            LEVEL_NAME_ID.Add(SensorConfig.CO2_LV_NAME_POOR, SensorConfig.CO2_LV_POOR);
            LEVEL_NAME_ID.Add(SensorConfig.CO2_LV_NAME_MODERATE, SensorConfig.CO2_LV_MODERATE);
            LEVEL_NAME_ID.Add(SensorConfig.CO2_LV_NAME_GOOD, SensorConfig.CO2_LV_GOOD);
        }
        public CO2Sensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public CO2Sensor()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "二氧化碳传感器";
            Unit = "ppm";

            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }

    /// <summary>
    /// 甲醛
    /// </summary>
    public class CH2OSensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        static CH2OSensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.CH2O_LV_POOR, SensorConfig.CH2O_LV_NAME_POOR);
            LEVEL_ID_NAME.Add(SensorConfig.CH2O_LV_MODERATE, SensorConfig.CH2O_LV_NAME_MODERATE);
            LEVEL_ID_NAME.Add(SensorConfig.CH2O_LV_GOOD, SensorConfig.CH2O_LV_NAME_GOOD);

            LEVEL_NAME_ID.Add(SensorConfig.CH2O_LV_NAME_POOR, SensorConfig.CH2O_LV_POOR);
            LEVEL_NAME_ID.Add(SensorConfig.CH2O_LV_NAME_MODERATE, SensorConfig.CH2O_LV_MODERATE);
            LEVEL_NAME_ID.Add(SensorConfig.CH2O_LV_NAME_GOOD, SensorConfig.CH2O_LV_GOOD);
        }

        public CH2OSensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public CH2OSensor()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "甲醛传感器";
            Unit = "ppb";


            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }



    /// <summary>
    /// PM2.5
    /// </summary>
    public class PM25Sensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        static PM25Sensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.PM25_LV_POOR, SensorConfig.PM25_LV_NAME_POOR);
            LEVEL_ID_NAME.Add(SensorConfig.PM25_LV_MODERATE, SensorConfig.PM25_LV_NAME_MODERATE);
            LEVEL_ID_NAME.Add(SensorConfig.PM25_LV_GOOD, SensorConfig.PM25_LV_NAME_GOOD);

            LEVEL_NAME_ID.Add(SensorConfig.PM25_LV_NAME_POOR, SensorConfig.PM25_LV_POOR);
            LEVEL_NAME_ID.Add(SensorConfig.PM25_LV_NAME_MODERATE, SensorConfig.PM25_LV_MODERATE);
            LEVEL_NAME_ID.Add(SensorConfig.PM25_LV_NAME_GOOD, SensorConfig.PM25_LV_GOOD);
        }
        public PM25Sensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public PM25Sensor()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "PM2.5传感器";
            Unit = "ug/m3";


            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }


    /// <summary>
    /// 氧气
    /// </summary>
    public class O2Sensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();
        static O2Sensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.O2_LV_LOW, SensorConfig.O2_LV_NAME_LOW);
            LEVEL_ID_NAME.Add(SensorConfig.O2_LV_MEDIUM, SensorConfig.O2_LV_NAME_MEDIUM);
            LEVEL_ID_NAME.Add(SensorConfig.O2_LV_HIGH, SensorConfig.O2_LV_NAME_HIGH);

            LEVEL_NAME_ID.Add(SensorConfig.O2_LV_NAME_LOW, SensorConfig.O2_LV_LOW);
            LEVEL_NAME_ID.Add(SensorConfig.O2_LV_NAME_MEDIUM, SensorConfig.O2_LV_MEDIUM);
            LEVEL_NAME_ID.Add(SensorConfig.O2_LV_NAME_HIGH, SensorConfig.O2_LV_HIGH);
        }
        public O2Sensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public O2Sensor()
        {
            Init();
        }
 
        public override void Init()
        {
            KindName = "氧气传感器";
            Unit = "%";
            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }


    /// <summary>
    /// 雷达
    /// </summary>
    public class RadarSensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();

        static RadarSensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.RSP_LV_NORMAL, SensorConfig.RSP_LV_NAME_NORMAL);
            LEVEL_ID_NAME.Add(SensorConfig.RSP_LV_TRIGGERED, SensorConfig.RSP_LV_NAME_TRIGGERED); 

            LEVEL_NAME_ID.Add(SensorConfig.RSP_LV_NAME_NORMAL, SensorConfig.RSP_LV_NORMAL);
            LEVEL_NAME_ID.Add(SensorConfig.RSP_LV_NAME_TRIGGERED, SensorConfig.RSP_LV_TRIGGERED); 
        }
        public RadarSensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public RadarSensor()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "雷达传感器";

            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }

    /// <summary>
    /// 防拆开关
    /// </summary>
    public class SwitTamperSensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();


        static SwitTamperSensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.TAMPER_LV_NORMAL, SensorConfig.TAMPER_LV_NAME_NORMAL);
            LEVEL_ID_NAME.Add(SensorConfig.TAMPER_LV_TRIGGERED, SensorConfig.TAMPER_LV_NAME_TRIGGERED);

            LEVEL_NAME_ID.Add(SensorConfig.TAMPER_LV_NAME_NORMAL, SensorConfig.TAMPER_LV_NORMAL);
            LEVEL_NAME_ID.Add(SensorConfig.TAMPER_LV_NAME_TRIGGERED, SensorConfig.TAMPER_LV_TRIGGERED);
        }
        public SwitTamperSensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public SwitTamperSensor()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "防拆开关";

            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }

    /// <summary>
    /// 安防级别
    /// </summary>
    public class SecuritySensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();

 

        static SecuritySensor()
        {
          

            LEVEL_ID_NAME.Add(SensorConfig.LG_SAF_SYST_DI, SensorConfig.LG_NAME_SAF_SYST_DI);
            LEVEL_ID_NAME.Add(SensorConfig.LG_SAF_SYST_EN_DLY, SensorConfig.LG_NAME_SAF_SYST_EN_DLY);
            LEVEL_ID_NAME.Add(SensorConfig.LG_SAF_SYST_EN, SensorConfig.LG_NAME_SAF_SYST_EN);
            LEVEL_ID_NAME.Add(SensorConfig.LG_SAF_SYST_WAR, SensorConfig.LG_NAME_SAF_SYST_WAR);
            LEVEL_ID_NAME.Add(SensorConfig.LG_SAF_SYST_ALM, SensorConfig.LG_NAME_SAF_SYST_ALM);
            LEVEL_ID_NAME.Add(SensorConfig.LG_SAF_SELF_DI, SensorConfig.LG_NAME_SAF_SELF_DI);
            LEVEL_ID_NAME.Add(SensorConfig.LG_SAF_SELF_EN_DLY, SensorConfig.LG_NAME_SAF_SELF_EN_DLY);
            LEVEL_ID_NAME.Add(SensorConfig.LG_SAF_SELF_EN, SensorConfig.LG_NAME_SAF_SELF_EN);
            LEVEL_ID_NAME.Add(SensorConfig.LG_SAF_SELF_WAR, SensorConfig.LG_NAME_SAF_SELF_WAR);
            LEVEL_ID_NAME.Add(SensorConfig.LG_SAF_SELF_ALM, SensorConfig.LG_NAME_SAF_SELF_ALM);


            LEVEL_NAME_ID.Add(SensorConfig.LG_NAME_SAF_SYST_DI, SensorConfig.LG_SAF_SYST_DI);
            LEVEL_NAME_ID.Add(SensorConfig.LG_NAME_SAF_SYST_EN_DLY, SensorConfig.LG_SAF_SYST_EN_DLY);
            LEVEL_NAME_ID.Add(SensorConfig.LG_NAME_SAF_SYST_EN, SensorConfig.LG_SAF_SYST_EN );
            LEVEL_NAME_ID.Add(SensorConfig.LG_NAME_SAF_SYST_WAR, SensorConfig.LG_SAF_SYST_WAR );
            LEVEL_NAME_ID.Add(SensorConfig.LG_NAME_SAF_SYST_ALM, SensorConfig.LG_SAF_SYST_ALM );
            LEVEL_NAME_ID.Add(SensorConfig.LG_NAME_SAF_SELF_DI, SensorConfig.LG_SAF_SELF_DI );
            LEVEL_NAME_ID.Add(SensorConfig.LG_NAME_SAF_SELF_EN_DLY, SensorConfig.LG_SAF_SELF_EN_DLY );
            LEVEL_NAME_ID.Add(SensorConfig.LG_NAME_SAF_SELF_EN, SensorConfig.LG_SAF_SELF_EN );
            LEVEL_NAME_ID.Add(SensorConfig.LG_NAME_SAF_SELF_WAR, SensorConfig.LG_SAF_SELF_WAR );
            LEVEL_NAME_ID.Add(SensorConfig.LG_NAME_SAF_SELF_ALM, SensorConfig.LG_SAF_SELF_ALM );

 
        }
        public SecuritySensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public SecuritySensor()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "安防级别";

            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }

    /// <summary>
    /// 短路输入4
    /// </summary>
    public class Short4Sensor : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();


        static Short4Sensor()
        {
            LEVEL_ID_NAME.Add(SensorConfig.SCIN_LV_LOW, SensorConfig.SCIN_LV_NAME_LOW);
            LEVEL_ID_NAME.Add(SensorConfig.SCIN_LV_HIGH, SensorConfig.SCIN_LV_NAME_HIGH);

            LEVEL_NAME_ID.Add(SensorConfig.SCIN_LV_NAME_LOW, SensorConfig.SCIN_LV_LOW);
            LEVEL_NAME_ID.Add(SensorConfig.SCIN_LV_NAME_HIGH, SensorConfig.SCIN_LV_HIGH);
        }
        public Short4Sensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public Short4Sensor()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "短路输入4";

            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }

    /// <summary>
    /// 超声波
    /// </summary>
    public class UWSensorData : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();


        static UWSensorData()
        {
            LEVEL_ID_NAME.Add(SensorConfig.UW_LV_NORMAL, SensorConfig.UW_LV_NAME_LOW);
            LEVEL_ID_NAME.Add(SensorConfig.UW_LV_TRIGGERED, SensorConfig.UW_LV_NAME_HIGH);

            LEVEL_NAME_ID.Add(SensorConfig.UW_LV_NAME_LOW, SensorConfig.UW_LV_NORMAL);
            LEVEL_NAME_ID.Add(SensorConfig.UW_LV_NAME_HIGH, SensorConfig.UW_LV_TRIGGERED);
        }
        public UWSensorData(byte[] value)
            : base(value)
        {
            Init();
        }

        public UWSensorData()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "超声波";

            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }

    /// <summary>
    /// 红外线
    /// </summary>
    public class IRSensorData : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();

        static IRSensorData()
        {
            LEVEL_ID_NAME.Add(SensorConfig.IR_LV_NORMAL, SensorConfig.IR_LV_NAME_LOW);
            LEVEL_ID_NAME.Add(SensorConfig.IR_LV_TRIGGERED, SensorConfig.IR_LV_NAME_HIGH);

            LEVEL_NAME_ID.Add(SensorConfig.IR_LV_NAME_LOW, SensorConfig.IR_LV_NORMAL);
            LEVEL_NAME_ID.Add(SensorConfig.IR_LV_NAME_HIGH, SensorConfig.IR_LV_TRIGGERED);
        }
        public IRSensorData(byte[] value)
            : base(value)
        {
            Init();
        }

        public IRSensorData()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "红外线";

            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }


    /// <summary>
    /// 顺序触发
    /// </summary>
    public class OrderSensorData : SensorStateData
    {
        public static Dictionary<int, string> LEVEL_ID_NAME = new Dictionary<int, string>();
        public static Dictionary<string, int> LEVEL_NAME_ID = new Dictionary<string, int>();

        static OrderSensorData()
        {
            LEVEL_ID_NAME.Add(SensorConfig.SN_LV_NORMAL, SensorConfig.SN_LV_NAME_LOW);
            LEVEL_ID_NAME.Add(SensorConfig.SN_LV_TRIGGERED, SensorConfig.SN_LV_NAME_HIGH);

            LEVEL_NAME_ID.Add(SensorConfig.SN_LV_NAME_LOW, SensorConfig.SN_LV_NORMAL);
            LEVEL_NAME_ID.Add(SensorConfig.SN_LV_NAME_HIGH, SensorConfig.SN_LV_TRIGGERED);
        }
        public OrderSensorData(byte[] value)
            : base(value)
        {
            Init();
        }

        public OrderSensorData()
        {
            Init();
        }

        public override void Init()
        {
            KindName = "顺序触发";

            foreach (int key in LEVEL_ID_NAME.Keys)
                LevelIDName.Add(key, LEVEL_ID_NAME[key]);
            foreach (string key in LEVEL_NAME_ID.Keys)
                LevelNameID.Add(key, LEVEL_NAME_ID[key]);
        }
    }


    /// <summary>
    /// 无效的传感器
    /// </summary>
    public class InvalidSensor : SensorStateData
    { 
        public new string LevelValue { get { return ""; } }//----级别名称值----
        public InvalidSensor(byte[] value)
            : base(value)
        {
            Init();
        }

        public InvalidSensor()
        {
            Init();
        }
        public override void Init()
        {
            KindName = "无效";  
 
        }
        public override byte[] GetValue()
        {
            return new byte[12];
        }

    }










}