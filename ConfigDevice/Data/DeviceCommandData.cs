using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class DeviceCommandData :CommandData
    {
        public byte ByteCmdType;//指令类型
        public byte ByteCmdKey;//第几个按键/分组
        public byte ByteCmdNum;//第几个指令 
       

        /// <summary>
        /// 指令长度,指令头(类型,分组,指令序)+指令设备(id+网段id+目标类型)
        /// +指令内容+指令内容长度
        /// </summary>
        public int Len { get { return DataLen + 3 + 3 + 2 + 1; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userData">用户数据</param>
        public DeviceCommandData(UserUdpData userData)
        {
            ByteCmdType = userData.Data[0];
            ByteCmdKey = userData.Data[1];
            ByteCmdNum = userData.Data[2];

            TargetId = userData.Data[3];
            TargetNet = userData.Data[4];
            TargetType = userData.Data[5];
            Cmd = CommonTools.CopyBytes(userData.Data, 6, 2);
            DataLen = (int)userData.Data[8];

            Buffer.BlockCopy(userData.Data, 9, Data, 0, (int)DataLen);

            PCAddress = userData.Target[0].ToString();
            NetworkIP = userData.IP;
        }

        public DeviceCommandData(int groupIndex, int numIndex, CommandData commandData)
        {
            ByteCmdType = 0;
            ByteCmdKey = (byte)groupIndex;
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
            byte[] value = GetCommandValue();
            byte[] all = new byte[value.Length+3];

            all[0] = ByteCmdType;
            all[1] = (byte)ByteCmdKey;
            all[2] = (byte)ByteCmdNum;
            Buffer.BlockCopy(value, 0, all, 3, value.Length);  

            return all;
        }




    }
}
