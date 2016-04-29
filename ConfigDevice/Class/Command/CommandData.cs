using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public abstract class CommandData//:ICommandData
    {
        public string Name = "";//命令名称

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_name">名字</param>
        public CommandData(string _name)
        {
            Name = _name;
        }

        /// <summary>
        /// 创建指令
        /// </summary>
        /// <returns>byte[]</returns>
        public abstract byte[] CreateCommand();




    }
}
