using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class CommandData
    {
        public byte ucCmdType;//指令类型
        public int ucCmdKey;//第几个按键/分组
        public int ucCmdNum;//第几个指令 
        public string Name = "";//命令名称
        public byte TargetId;//目标ID
        public byte TargetNet;//目标网段
        public byte TargetType;//目标类型

        public byte[] Cmd = new byte[2];//命令
        public byte Len;//长度
        public byte[] Data = new byte[30];//数据最长30字节

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_name">名字</param>
        public CommandData(string _name)
        {
            Name = _name;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userData">用户数据</param>
        public CommandData(UserUdpData userData)
        {
            ucCmdType = userData.Data[0];
            ucCmdKey = (int)userData.Data[1];
            ucCmdNum = (int)userData.Data[2];
            TargetId = userData.Data[3];
            TargetNet = userData.Data[4];
            TargetType = userData.Data[5];
            Cmd = CommonTools.CopyBytes(userData.Data, 6, 2);
            Len = userData.Data[8];
            Buffer.BlockCopy(userData.Data, 9, Data, 0, (int)Len);
        }

        /// <summary>
        /// 获取指令字节
        /// </summary>
        /// <returns></returns>
        public byte[] GetValue()
        {
            byte[] All = new byte[36];
            All[0] = TargetId;
            All[1] = TargetNet;
            All[2] = TargetType;
            Buffer.BlockCopy(Cmd, 0, All, 3, 2);
            All[5] = Len;
            Buffer.BlockCopy(Data, 0, All, 6, 30);

            return All;
        }

    }



}
