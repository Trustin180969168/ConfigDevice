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
            return FactorySensor(kindID, new byte[6]);
        }

        /// <summary>
        /// 根据类型ID,创建传感器对象
        /// </summary>
        /// <param name="kindID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SensorStateData FactorySensor(UInt16 kindID, byte[] value)
        {

            switch (kindID)
            {
                case SensorConfig.LG_SENSOR_VOID: return new InvalidSensor(value);//无效传感器
                case SensorConfig.LG_SENSOR_TEMP_FC: return new FireControlTemperatureSensor(value);//---消防温度
                case SensorConfig.LG_SENSOR_LEL: return new FlamableGasProbeSensor(value);//---可燃气体探头
                case SensorConfig.LG_SENSOR_TEMP: return new TemperatureSensor(value);//---温度
                case SensorConfig.LG_SENSOR_HUMI: return new HumiditySensor(value);//---湿度
                case SensorConfig.LG_SENSOR_LUMI: return new LuminanceSensorData(value);//---亮度
                case SensorConfig.LG_SENSOR_AQI: return new AQISensor(value); //---空气质量    
                case SensorConfig.LG_SENSOR_TVOC: return new TVOCSensor(value);//---有害气体
                case SensorConfig.LG_SENSOR_CO2: return new CO2Sensor(value);//---二氧化碳
                case SensorConfig.LG_SENSOR_CH2O: return new CH2OSensor(value);//---甲醛
                case SensorConfig.LG_SENSOR_PM25: return new PM25Sensor(value);//---PM2.5
                case SensorConfig.LG_SENSOR_WIND: return new WindySensor(value);//---风速
                case SensorConfig.LG_SENSOR_WIND_DIR: return new WindDirSensor(value);//---风向
                case SensorConfig.LG_SENSOR_RAIN: return new RainSensor(value);//---雨感
                case SensorConfig.LG_SENSOR_O2: return new O2Sensor(value);//-----氧气
                case SensorConfig.LG_SENSOR_TAMPER: return new SwitTamperSensor(value);//-----防拆开关
                case SensorConfig.LG_SENSOR_RSP: return new RadarSensor(value);//-----雷达
                case SensorConfig.LG_SENSOR_SCIN_1://-----短路输入1
                case SensorConfig.LG_SENSOR_SCIN_2://-----短路输入2
                case SensorConfig.LG_SENSOR_SCIN_3://-----短路输入3
                case SensorConfig.LG_SENSOR_SCIN_4://-----短路输入4
                case SensorConfig.LG_SENSOR_SCIN_5://-----短路输入5
                case SensorConfig.LG_SENSOR_SCIN_6://-----短路输入6
                case SensorConfig.LG_SENSOR_SCIN_7://-----短路输入7
                case SensorConfig.LG_SENSOR_SCIN_8: //-----短路输入8
                case SensorConfig.LG_SENSOR_SENONAUTOOFF:        //感应开自动关 (含有外设,只有等级)(实质为短路输入)
                case SensorConfig.LG_SENSOR_SENONOFF:        //感应开关     (含有外设,只有等级)(实质为短路输入)
                case SensorConfig.LG_SENSOR_KEY1:         //按键1        (含有外设,只有等级)(实质为短路输入)
                case SensorConfig.LG_SENSOR_KEY2:         //按键2        (含有外设,只有等级)(实质为短路输入)
                case SensorConfig.LG_SENSOR_SAFETYLC:         //安全光栅     (含有外设,只有等级)(实质为短路输入)
                case SensorConfig.LG_SENSOR_ALWAYSON:        //常开模式     (含有外设,只有等级)(实质为短路输入)
                case SensorConfig.LG_SENSOR_CLOSECHECK :         //关门检测     (含有外设,只有等级)(实质为短路输入)
                case SensorConfig.LG_SENSOR_OPENCHECK: return new Short4Sensor(value); //开门检测     (含有外设,只有等级)(实质为短路输入)
                case SensorConfig.LG_SENSOR_UW_1:  //-----超声波1
                case SensorConfig.LG_SENSOR_UW_2: return new UWSensorData(value);//-----超声波2
                case SensorConfig.LG_SENSOR_IR: return new IRSensorData(value); //-----红外
                default: return new InvalidSensor(value);//无效传感器;
            }




        }






    }
}
