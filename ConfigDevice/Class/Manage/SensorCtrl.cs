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
        public static Sensor GetSensorFromByte(UInt16 kindID, byte[] value)
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
            return null;
        }

        /// <summary>
        /// 根据类型ID,创建传感器对象
        /// </summary>
        /// <param name="kindID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Sensor FactorySensor(UInt16 kindID, byte[] value)
        {
            if (kindID == SensorConfig.LG_SENSOR_VOID)
                return new InvalidSensor(value);
            if (kindID == SensorConfig.LG_DEV_SENSOR_TEMP)
                return new FireControlTemperatureSensor(value);
            if (kindID == SensorConfig.LG_SENSOR_LEL)
                return new FlamableGasProbeSensor(value);

            return null;
            


        }






    }
}
