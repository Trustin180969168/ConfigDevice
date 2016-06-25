using System;
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
        public Dictionary<int, string> LevelIDName = new Dictionary<int, string>();//级别ID表
        public Dictionary<string,int> LevelNameID = new Dictionary<string,int>();//级别名称
        public string LevelValue { get { return LevelIDName[this.LevelID]; } }//----级别名称值----
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
            LEVEL_ID_NAME.Add(SensorConfig.TEMPFC_LV_NORMAL, SensorConfig.TEMPFC_NAME_LV_NORMAL);
            LEVEL_ID_NAME.Add(SensorConfig.TEMPFC_LV_HIGH, SensorConfig.TEMPFC_NAME_LV_HIGH);
            LEVEL_ID_NAME.Add(SensorConfig.TEMPFC_LV_FIRE, SensorConfig.TEMPFC_NAME_LV_FIRE);
            LEVEL_NAME_ID.Add(SensorConfig.TEMPFC_NAME_LV_NORMAL, SensorConfig.TEMPFC_LV_NORMAL);
            LEVEL_NAME_ID.Add(SensorConfig.TEMPFC_NAME_LV_HIGH, SensorConfig.TEMPFC_LV_HIGH);
            LEVEL_NAME_ID.Add(SensorConfig.TEMPFC_NAME_LV_FIRE, SensorConfig.TEMPFC_LV_FIRE);
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

            LEVEL_ID_NAME.Add(SensorConfig.LEL_LV_NORMAL, SensorConfig.LEL_LV_NAME_NORMAL);
            LEVEL_ID_NAME.Add(SensorConfig.LEL_LV_TRIGGERED, SensorConfig.LEL_LV_NAME_TRIGGERED);
            LEVEL_NAME_ID.Add(SensorConfig.LEL_LV_NAME_NORMAL, SensorConfig.LEL_LV_NORMAL);
            LEVEL_NAME_ID.Add(SensorConfig.LEL_LV_NAME_TRIGGERED, SensorConfig.LEL_LV_TRIGGERED);

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
