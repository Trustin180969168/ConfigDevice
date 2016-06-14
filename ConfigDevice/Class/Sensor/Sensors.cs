using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    /// <summary>
    /// 抽象传感器
    /// </summary>
    public abstract class Sensor
    {
        public UInt16 KindID;
        public byte LevelID;
        public byte Retain;
        public Int16 Value;
        public string KindName = "";
        public Dictionary<int, string> LevelIDValue = new Dictionary<int, string>();//级别ID表
        public Dictionary<string,int> LevelValueID = new Dictionary<string,int>();//级别名称


        public string LevelValue { get { return LevelIDValue[this.LevelID]; } }//----级别名称值----
        public Sensor(byte[] value)
        {
            KindID = ConvertTools.Bytes2ToUInt16(value[0], value[1]);
            LevelID = value[2];
            Retain = value[3];
            Value = ConvertTools.Bytes2ToInt16(value[4], value[5]);
        }
        public Sensor()
        {
        }
        public abstract void Init();//----具体对象初始化-----
    }


    /// <summary>
    /// 消防温控传感器
    /// </summary>
    public class FireControlTemperatureSensor : Sensor
    {

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

            LevelIDValue.Add(SensorConfig.TEMPFC_LV_NORMAL, "正常");
            LevelIDValue.Add(SensorConfig.TEMPFC_LV_HIGH, "高温");
            LevelIDValue.Add(SensorConfig.TEMPFC_LV_FIRE, "火灾");
            LevelIDValue.Add(SensorConfig.TEMPFC_LV_TOTAL, "");

            LevelValueID.Add("正常", SensorConfig.TEMPFC_LV_NORMAL);
            LevelValueID.Add("高温", SensorConfig.TEMPFC_LV_HIGH);
            LevelValueID.Add("火灾", SensorConfig.TEMPFC_LV_FIRE);
            LevelValueID.Add("", SensorConfig.TEMPFC_LV_TOTAL);
 
        }
 
    }

    /// <summary>
    /// 可燃气探头
    /// </summary>
    public class FlamableGasProbeSensor : Sensor
    {
 
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

            LevelIDValue.Add(SensorConfig.LEL_LV_NORMAL, "正常");
            LevelIDValue.Add(SensorConfig.LEL_LV_TRIGGERED, "泄漏");
            LevelIDValue.Add(SensorConfig.LEL_LV_TOTAL, "");

            LevelValueID.Add("正常", SensorConfig.LEL_LV_NORMAL);
            LevelValueID.Add("泄漏", SensorConfig.LEL_LV_TRIGGERED);
            LevelValueID.Add("", SensorConfig.LEL_LV_TOTAL); 
  
        }

    }



    /// <summary>
    /// 无效的传感器
    /// </summary>
    public class InvalidSensor : Sensor
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

    }










}
