using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Swit : ControlObj
    {
        public const string NAME_CMD_SW_SWIT_ALL = "开关全部";
        public const string NAME_CMD_SW_SWIT_ALL_OPEN = "开全部";
        public const string NAME_CMD_SW_SWIT_ALL_CLOSE = "关全部";
        
        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系
        public Swit(Device _deviceCtrl)
            : base(_deviceCtrl)
        {
            Name = "全部"; 
            if (NameAndCommand.Count == 0)
            {
                NameAndCommand.Add(NAME_CMD_SW_SWIT_ALL, DeviceConfig.CMD_SW_SWIT_ALL);
                NameAndCommand.Add(NAME_CMD_SW_SWIT_ALL_OPEN, DeviceConfig.CMD_SW_SWIT_ALL_OPEN);
                NameAndCommand.Add(NAME_CMD_SW_SWIT_ALL_CLOSE, DeviceConfig.CMD_SW_SWIT_ALL_CLOSE);
            }
        }

        /// <summary>
        /// 获取执行命令数据
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="usRunTime">运行时间</param>
        /// <returns>CommandData</returns>
        public CommandData GetCommandData(byte[] command,  int usRunTime)
        {
            CommandData cmdData = new CommandData("开关全部");
            cmdData.TargetId = deviceControled.ByteDeviceID;
            cmdData.TargetNet = deviceControled.ByteNetworkId;
            cmdData.TargetType = deviceControled.ByteKindID;

            cmdData.Cmd = command;
            cmdData.DataLen = 8;

            cmdData.Data[0] = 0;//开或关，1表示开，0表示关,当指令是CMD_SW_SWIT_ALL_OPEN/CMD_SW_SWIT_ALL_CLOSE，此字节无效
            cmdData.Data[1] = 0;//保留
            Buffer.BlockCopy(BitConverter.GetBytes(usRunTime), 0, cmdData.Data, 2, 4);//运行时间

            return cmdData;
        }




    }




}
