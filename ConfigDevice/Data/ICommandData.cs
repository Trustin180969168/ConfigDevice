using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    /// <summary>
    /// 抽象命令类
    /// </summary>
    public interface ICommandData
    {

        /// <summary>
        /// 创建指令
        /// </summary>
        /// <returns>byte[]</returns>
        byte[] CreateCommand();
        


    }
}
