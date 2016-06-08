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
        public Int16 TriggerKindID;//触发类型类型ID(可燃气体探头,温度,系统联动号)
        public Int16 CompareID;//比较(大于、小于、等于)
        public Int32 Size1;//大小1
        public Int32 Size2;//大小2
        public Int16 ValidSeconds;//有效时间(秒)
        public Int16 InvalidSeconds;//无效时间(秒)
        public byte[] Retain = new byte[16];//保留16个字节
    }

    /// <summary>
    /// 逻辑数据
    /// </summary>
    public class LogicData
    {
        public string Name = "";//逻辑名称
        public const int TRIGGER_COUNT = 4;//触发条件固定为4个
        public Int16 GroupNum;//第几个按键/分组
        public Int16 Logic4KindID;//逻辑关系类型ID(4路,6种逻辑类型)
        public TriggerData[] TriggerList = new TriggerData[TRIGGER_COUNT];//--4组逻辑数据


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_name">名字</param>
        public LogicData(string _name)
        {
            Name = _name;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userData">用户数据</param>
        public LogicData(UserUdpData userData)
        {
            byte[] data = userData.Data;
            this.GroupNum = (Int16)data[0];
            Logic4KindID = (Int16)data[1]; 
            for(int i = 0;i<TRIGGER_COUNT;i++)
            {
                TriggerList[i] = new TriggerData();
                TriggerList[i].CompareID = data[2 + i * 31];
                TriggerList[i].Size1 = ConvertTools.Bytes4ToInt(data[3 + i * 31], data[4 + i * 31], data[5 + i * 31], data[6 + i * 31]);
                TriggerList[i].Size1 = ConvertTools.Bytes4ToInt(data[7 + i * 31], data[8 + i * 31], data[9 + i * 31], data[10 + i * 31]);
                TriggerList[i].ValidSeconds = ConvertTools.Bytes2ToInt(data[11 + i * 31], data[12 + i * 31]);
                TriggerList[i].InvalidSeconds = ConvertTools.Bytes2ToInt(data[13 + i * 31], data[14 + i * 31]);
                TriggerList[i].Retain = CommonTools.CopyBytes(data, 15 + i * 31, 16);
        }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LogicData()
        {

        }
 
    }



}
