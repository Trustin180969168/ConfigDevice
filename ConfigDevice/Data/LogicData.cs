using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    /// <summary>
    /// 触发条件值
    /// </summary>
    public class TriggerData
    {
        public UInt16 TriggerObjectID = 0 ;         //触发类型ID(可燃气体探头,温度,系统联动号),默认为0
        public UInt16 TriggerKindID = 0;            //触发级别,触发值,级别
        public UInt16 TriggerPositionID = 0;        //触发位置ID,本地,外设,差值,默认为本地
        public byte CompareID = 0;                  //比较(大于、小于、等于)
        public Int32 Size1 = 0;                     //大小1
        public Int32 Size2 = 0;                     //大小2
        public UInt16 ValidSeconds = 0;             //有效时间(秒)
        public UInt16 InvalidSeconds = 0;           //无效时间(秒)
        public byte[] Retain = new byte[16];        //保留16个字节
        public byte DeviceKindID = 0;               //设备类型ID
        public byte DeviceNetworkID = 0;            //设备网段ID
        public byte DeviceID = 0;                   //设备ID


        /// <summary>
        /// 获取触发值
        /// </summary>
        /// <returns></returns>
        public byte[] Value()
        {
            byte[] value = new byte[31];
            UInt16 objInt = (UInt16)(TriggerKindID | TriggerObjectID | TriggerPositionID);
            byte[] objByte = ConvertTools.GetByteFromUInt16(objInt);               
            byte[] sizeByte1 = ConvertTools.GetByteFromInt32(Size1);
            byte[] sizeByte2 = ConvertTools.GetByteFromInt32(Size2);
            byte[] byteValidSeconds = ConvertTools.GetByteFromUInt16(ValidSeconds);
            byte[] byteInvalidSeconds = ConvertTools.GetByteFromUInt16(InvalidSeconds);

            Buffer.BlockCopy(objByte, 0, value, 0, 2);
            value[2] = ConvertTools.GetByteFromIntNum(CompareID);
            Buffer.BlockCopy(sizeByte1, 0, value, 3, 4);
            Buffer.BlockCopy(sizeByte2, 0, value, 7, 4);
            Buffer.BlockCopy(byteValidSeconds, 0, value, 11, 2);
            Buffer.BlockCopy(byteInvalidSeconds, 0, value, 13, 2);
            value[15] = DeviceKindID;
            value[16] = DeviceNetworkID;
            value[17] = DeviceID;

            return value;
        }
    }

    /// <summary>
    /// 逻辑数据
    /// </summary>
    public class LogicData
    {
        public string Name = "";//逻辑名称
        public const int TRIGGER_COUNT = 4;//触发条件固定为4个
        public byte GroupNum;//第几个按键/分组
        public byte Logic4KindID;//逻辑关系类型ID(4路,6种逻辑类型)
        public TriggerData[] TriggerList = new TriggerData[TRIGGER_COUNT];//--4组逻辑数据         

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userData">用户数据</param>
        public LogicData(UserUdpData userData)
        {
            byte[] data = userData.Data;
            GetLogicData(data);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userData">数据</param>
        public LogicData(byte[] data)
        { 
            GetLogicData(data);
        }

        private void GetLogicData(byte[] data)
        {
            this.GroupNum = data[0];
            Logic4KindID = data[1];
            for (int i = 0; i < TRIGGER_COUNT; i++)
            {
                TriggerList[i] = new TriggerData();
                Int16 objKindInfo = ConvertTools.Bytes2ToInt16(data[i * 31 + 2], data[i * 31 + 3]);

                TriggerList[i].TriggerObjectID = (UInt16)(objKindInfo & 0x3FF);     //---低10位为传感器类型-----
                TriggerList[i].TriggerPositionID = (UInt16)(objKindInfo & SensorConfig.LG_SENSOR_DIF_FLAG_VALUE);     //---获取外设标识值----
                TriggerList[i].TriggerKindID = (UInt16)(objKindInfo & SensorConfig.LG_SENSOR_LVL_FLAG);     //---获取级别标识值-----

                TriggerList[i].CompareID = data[4 + i * 31];//---比较符-----
                TriggerList[i].Size1 = ConvertTools.Bytes4ToInt(data[5 + i * 31], data[6 + i * 31], data[7 + i * 31], data[8 + i * 31]);        //---开始值---
                TriggerList[i].Size2 = ConvertTools.Bytes4ToInt(data[9 + i * 31], data[10 + i * 31], data[11 + i * 31], data[12 + i * 31]);     //---结束值---
                TriggerList[i].ValidSeconds = (UInt16)ConvertTools.Bytes2ToInt16(data[13 + i * 31], data[14 + i * 31]);     //----有效触发时间
                TriggerList[i].InvalidSeconds = (UInt16)ConvertTools.Bytes2ToInt16(data[15 + i * 31], data[16 + i * 31]);   //----失效触发时间
                TriggerList[i].DeviceKindID = data[17 + i * 31];    //----设备类型ID---
                TriggerList[i].DeviceNetworkID = data[18 + i * 31]; //---设备网络ID---
                TriggerList[i].DeviceID = data[19 + i * 31];        //---设备ID---
                TriggerList[i].Retain = CommonTools.CopyBytes(data, 20 + i * 31, 13);
            }
        }



        /// <summary>
        /// 获取逻辑数据值
        /// </summary>
        /// <returns>byte[]</returns>
        public byte[] GetValue()
        {
            byte[] value = new byte[126];
            value[0] = GroupNum;
            value[1] = Logic4KindID;
            Buffer.BlockCopy(TriggerList[0].Value(), 0, value, 2, 31);
            Buffer.BlockCopy(TriggerList[1].Value(), 0, value, 33, 31);
            Buffer.BlockCopy(TriggerList[2].Value(), 0, value, 34, 31);
            Buffer.BlockCopy(TriggerList[3].Value(), 0, value, 95, 31);

            return value;
        }
 
    }

}
