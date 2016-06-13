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
        public UInt16 TriggerObjectID = 0 ;//触发类型类型ID(可燃气体探头,温度,系统联动号)
        public UInt16 TriggerKindID = 0;//触发级别
        public byte CompareID = 0;//比较(大于、小于、等于)
        public Int32 Size1 = 0;//大小1
        public Int32 Size2 = 0;//大小2
        public UInt16 ValidSeconds = 0;//有效时间(秒)
        public UInt16 InvalidSeconds = 0;//无效时间(秒)
        public byte[] Retain = new byte[16];//保留16个字节

        public byte[] Value()
        {
            byte[] value = new byte[31];
            UInt16 objInt = (UInt16)(TriggerKindID | TriggerObjectID);
            byte[] objByte = ConvertTools.GetByteFrom16BitUInt(objInt);               
            byte[] sizeByte1 = ConvertTools.GetByteFrom32BitInt(Size1);
            byte[] sizeByte2 = ConvertTools.GetByteFrom32BitInt(Size2);
            byte[] byteValidSeconds = ConvertTools.GetByteFrom16BitUInt(ValidSeconds);
            byte[] byteInvalidSeconds = ConvertTools.GetByteFrom16BitUInt(InvalidSeconds);

            Buffer.BlockCopy(objByte, 0, value, 0, 2);
            value[2] = ConvertTools.GetByteFromIntNum(CompareID);
            Buffer.BlockCopy(sizeByte1, 0, value, 3, 4);
            Buffer.BlockCopy(sizeByte2, 0, value, 7, 4);
            Buffer.BlockCopy(byteValidSeconds, 0, value, 11, 2);
            Buffer.BlockCopy(byteInvalidSeconds, 0, value, 13, 2);
            
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
            this.GroupNum = data[0];
            Logic4KindID = data[1];
            for (int i = 0; i < TRIGGER_COUNT; i++)
            {
                TriggerList[i] = new TriggerData();
                Int16 objKindInfo = ConvertTools.Bytes2ToInt(data[i * 31 + 2], data[i * 31 + 3]);

                TriggerList[i].TriggerObjectID = (UInt16)(objKindInfo & 0x3FF);//----低12位为传感器类型-----
                TriggerList[i].TriggerKindID = (UInt16)(objKindInfo & 0xFC00);//---前六位标识位---

                TriggerList[i].CompareID = data[4 + i * 31];//---比较符-----
                TriggerList[i].Size1 = ConvertTools.Bytes4ToInt(data[5 + i * 31], data[6 + i * 31], data[7 + i * 31], data[8 + i * 31]);
                TriggerList[i].Size1 = ConvertTools.Bytes4ToInt(data[9 + i * 31], data[10 + i * 31], data[11 + i * 31], data[12 + i * 31]);
                TriggerList[i].ValidSeconds = (UInt16)ConvertTools.Bytes2ToInt(data[13 + i * 31], data[14 + i * 31]);
                TriggerList[i].InvalidSeconds = (UInt16)ConvertTools.Bytes2ToInt(data[15 + i * 31], data[16 + i * 31]);
                TriggerList[i].Retain = CommonTools.CopyBytes(data, 17 + i * 31, 16);
            }
        }

  

 
    }



}
