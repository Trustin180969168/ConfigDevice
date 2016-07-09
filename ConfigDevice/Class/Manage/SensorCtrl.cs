using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice 
{
    public static class SensorCtrl
    {

        /// <summary>
        /// 在数据中获取对应的传感器信息
        /// </summary>
        /// <param name="kindID">传感器类型ID</param>
        /// <param name="value">多个传感器值的数据</param>
        /// <returns>传感器</returns>
        public static SensorStateData GetSensorFromByte(UInt16 kindID, byte[] value)
        {
            if (value.Length < 6) return null;//长度无效
            int count = value.Length / 6;//获取有效的传感器个数

            for (int i = 0; i < count; i++)
            {
                if (kindID == ConvertTools.Bytes2ToUInt16(value[i * 6], value[i * 6 + 1]))
                {
                    byte[] sensorValue = CommonTools.CopyBytes(value, i * 6, 6);
                    return FactorySensor(kindID, sensorValue);
                }
            }
            //----找不到传感器值,默认创建一个对象返回----
            return FactorySensor(kindID,new byte[6]);
        }

        /// <summary>
        /// 根据类型ID,创建传感器对象
        /// </summary>
        /// <param name="kindID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SensorStateData FactorySensor(UInt16 kindID, byte[] value)
        {
            if (kindID == SensorConfig.LG_SENSOR_VOID)
                return new InvalidSensor(value);
            if (kindID == SensorConfig.LG_SENSOR_TEMP_FC)//---消防温度
                return new FireControlTemperatureSensor(value);
            if (kindID == SensorConfig.LG_SENSOR_LEL)//---可燃气体探头
                return new FlamableGasProbeSensor(value);
            if (kindID == SensorConfig.LG_SENSOR_TEMP)//---温度
                return new TemperatureSensor(value);
            if (kindID == SensorConfig.LG_SENSOR_HUMI)//---湿度
                return new HumiditySensor(value);
            if (kindID == SensorConfig.LG_SENSOR_LUMI)//---亮度
                return new LuminanceSensor(value);
            if (kindID == SensorConfig.LG_SENSOR_AQI)//---空气质量
                return new AQISensor(value);            
            if (kindID == SensorConfig.LG_SENSOR_TVOC)//---有害气体
                return new TVOCSensor(value);
            if (kindID == SensorConfig.LG_SENSOR_CO2)//---二氧化碳
                return new CO2Sensor(value);
            if (kindID == SensorConfig.LG_SENSOR_CH2O)//---甲醛
                return new CH2OSensor(value);
            if (kindID == SensorConfig.LG_SENSOR_PM25)//---PM2.5
                return new PM25Sensor(value);
            if (kindID == SensorConfig.LG_SENSOR_WIND)//---风速
                return new WindySensor(value);
            if (kindID == SensorConfig.LG_SENSOR_RAIN)//---雨感
                return new RainSensor(value);
            if (kindID == SensorConfig.LG_SENSOR_O2)//-----氧气
                return new O2Sensor(value);
            if (kindID == SensorConfig.LG_SENSOR_TAMPER)//-----防拆开关
                return new SwitTamperSensor(value);
            if (kindID == SensorConfig.LG_SENSOR_RSP)//-----雷达
                return new RadarSensor(value); 

            return null;
        }






    }
}
