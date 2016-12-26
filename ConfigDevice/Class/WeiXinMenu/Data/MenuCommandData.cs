using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class MenuCommandData:CommandData
    {
        public UInt32 UInt32MenuId;//第几个菜单  (从0开始计数)
        public byte ByteKindId;//控制类型   （指出是哪个控制类型的配置数据）(MS_COBJ_AMP等)即:菜单类型
        public byte ByteCmdNum;//第几个指令  

        /// <summary>
        /// 指令头(类型,分组,指令序)+指令设备(id+网段id+目标类型)
        /// 指令内容+指令内容长度
        /// </summary>
        public int Len { get { return DataLen + 4 + 1 + 1 + 3 + 2 + 1; } }  

  

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userData">用户数据</param>
        public MenuCommandData(UserUdpData userData)
        {
            UInt32MenuId = ConvertTools.Bytes4ToUInt32( userData.Data[0],userData.Data[1],userData.Data[2],userData.Data[3]);
            ByteKindId =  userData.Data[4];
            ByteCmdNum = userData.Data[5];
            TargetId = userData.Data[6];
            TargetNet = userData.Data[7];
            TargetType = userData.Data[8];
            Cmd = CommonTools.CopyBytes(userData.Data, 9, 2);
            DataLen = (int)userData.Data[11];

            Buffer.BlockCopy(userData.Data, 12, Data, 0, (int)DataLen);

            PCAddress = userData.Target[0].ToString();
            NetworkIP = userData.IP;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuCommandData()
        {

        }

        public MenuCommandData(int menuID, int menuKind, int numIndex, CommandData commandData)
        {
            UInt32MenuId = (UInt32)menuID;
            ByteKindId = (byte)menuKind;
            ByteCmdNum = (byte)numIndex;

            TargetId = commandData.TargetId;
            TargetNet = commandData.TargetNet;
            TargetType = commandData.TargetType;
            Cmd = commandData.Cmd;
            DataLen = commandData.DataLen;
            Data = commandData.Data;
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

            All[6] = TargetId;
            All[7] = TargetNet;
            All[8] = TargetType;
            Buffer.BlockCopy(Cmd, 0, All, 9, 2);
            All[11] = (byte)DataLen;
            Buffer.BlockCopy(Data, 0, All, 11, DataLen);

            return All;
        }

    }



}
