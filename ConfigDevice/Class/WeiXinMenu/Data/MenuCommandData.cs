using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class MenuCommandData
    {
        public UInt32 UInt32MenuId;//第几个菜单  (从0开始计数)
        public byte ByteKindId;//控制类型   （指出是哪个控制类型的配置数据）(MS_COBJ_AMP等)即:菜单类型
        public byte ByteCmdNum;//第几个指令 
       
        public byte ByteTargetId;//目标ID
        public byte ByteTargetNet;//目标网段
        public byte ByteTargetType;//目标类型

        public byte[] ByteArrCmd = new byte[2];//命令
        public int DataLen;//数据长度
        public byte[] ByteArrData = new byte[30];//数据最长30字节
        public int Len { get { return DataLen + 4 + 1 + 1 + 3 + 2 + 1; } }

 
        public string ValueStr { get { return ConvertTools.ByteToHexStr(GetValue()); } }

        //------补充信息成员------
        public string PCAddress = "";//-----PC地址-----
        public string NetworkIP = "";//----IP地址----- 
        public string Name = "";//命令名称

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_name">名字</param>
        public MenuCommandData(string _name)
        {
            Name = _name;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userData">用户数据</param>
        public MenuCommandData(UserUdpData userData)
        {
            UInt32MenuId = ConvertTools.Bytes4ToUInt32( userData.Data[0],userData.Data[1],userData.Data[2],userData.Data[3]);
            ByteKindId =  userData.Data[4];
            ByteCmdNum = userData.Data[5];
            ByteTargetId = userData.Data[6];            
            ByteTargetNet = userData.Data[7];
            ByteTargetType = userData.Data[8];
            ByteArrCmd = CommonTools.CopyBytes(userData.Data, 9, 2);
            DataLen = (int)userData.Data[11];

            Buffer.BlockCopy(userData.Data, 12, ByteArrData, 0, (int)DataLen);

            PCAddress = userData.Target[0].ToString();
            NetworkIP = userData.IP;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuCommandData()
        {

        }

        /// <summary>
        /// 获取指令字节
        /// </summary>
        /// <returns></returns>
        public byte[] GetValue()
        {
            byte[] All = new byte[Len+DataLen];
            
            byte[] byteArrMenuId = ConvertTools.GetByteFromUInt32(UInt32MenuId);
            Buffer.BlockCopy(byteArrMenuId, 0, All, 0, 4);
            All[4] = ByteKindId;
            All[5] = (byte)ByteCmdNum;

            All[6] = ByteTargetId;
            All[7] = ByteTargetNet;
            All[8] = ByteTargetType;
            Buffer.BlockCopy(ByteArrCmd, 0, All, 9, 2);
            All[11] = (byte)DataLen;
            Buffer.BlockCopy(ByteArrData, 0, All, 11, DataLen);

            return All;
        }

    }



}
