using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    /// <summary>
    /// 抽象命令类
    /// </summary>
    public abstract class CommandData
    {
        public byte TargetId;//目标ID
        public byte TargetNet;//目标网段
        public byte TargetType;//目标类型

        public byte[] Cmd = new byte[2];//命令
        public byte Len;//长度
        public byte[] Data = new byte[30];//数据最长30字节

        /// <summary>
        /// 创建指令
        /// </summary>
        /// <returns>byte[]</returns>
        public abstract byte[] CreateCommand();
        


    }
}
