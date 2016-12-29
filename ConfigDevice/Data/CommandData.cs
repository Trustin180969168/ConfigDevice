using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public  class CommandData
    {
        //---以下是指令内容------
        public byte TargetId;//目标ID
        public byte TargetNet;//目标网段
        public byte TargetType;//目标类型
        public byte[] Cmd = new byte[2];//命令
        public int DataLen;//数据长度,最长30字节
        public byte[] Data = new byte[30];//数据最长30字节 
        public string Name = "";//命令名称

        public string ValueStr { get { return ConvertTools.ByteToHexStr(GetCommandValue()); } }

        //------补充信息成员------
        public string PCAddress = "";//-----PC地址-----
        public string NetworkIP = "";//----IP地址----- 


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
        public CommandData()
        {

        }

    

        /// <summary>
        /// 获取命令值
        /// </summary>
        /// <returns></returns>
        //public abstract byte[] GetValue();

        /// <summary>
        ///指令内容值
        /// </summary>
        /// <returns></returns>
        public byte[] GetCommandValue()
        {
            byte[] all = new byte[3 + 3 + DataLen];
            all[0] = TargetId;
            all[1] = TargetNet;
            all[2] = TargetType;
            Buffer.BlockCopy(Cmd, 0, all, 3, 2);
            all[5] = (byte)DataLen;
            Buffer.BlockCopy(Data, 0, all, 6, DataLen);

            return all;
        }

    }



}
