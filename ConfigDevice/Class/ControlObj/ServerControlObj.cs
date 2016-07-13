using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class ServerControlObj : ControlObj
    {
        public const string NAME_CMD_SEND_WEIXIN = "发微信";

        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系
        public ServerControlObj(Device _deviceCtrl):base(_deviceCtrl)
        {
            Name = "服务器"; 

            if (NameAndCommand.Count == 0)
            {
                NameAndCommand.Add(NAME_CMD_SEND_WEIXIN, DeviceConfig.CMD_SERVER_WEIXIN);
            }
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="content">内容</param>
        /// <returns>CommandData</returns>
        private CommandData ControlAction(byte[] cmd, string content)
        {
            CommandData cmdData = new CommandData("发微信");
            cmdData.TargetId = deviceControled.ByteDeviceID;
            cmdData.TargetNet = deviceControled.ByteNetworkId;
            cmdData.TargetType = deviceControled.ByteKindID;

            cmdData.Cmd = cmd;
            cmdData.Data = Encoding.Unicode.GetBytes(content);
            if (cmdData.DataLen > 30)
            { 
                CommonTools.MessageShow("数据长度不能大于30字节!", 2, "");
                return null;
            }
            cmdData.DataLen = cmdData.Data.Length;
            return cmdData;
        }


        /// <summary>
        /// 获取命令数据
        /// </summary>
        /// <param name="command">byte[]</param>
        /// <param name="content">内容</param>
        /// <returns>CommandData</returns>
        public CommandData GetCommandData(byte[] command, string content)
        {
            return ControlAction(command, content);
        }


    }

}
