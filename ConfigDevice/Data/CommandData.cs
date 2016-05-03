using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class CommandData
    {
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
