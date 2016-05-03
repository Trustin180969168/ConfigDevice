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

    }



}
