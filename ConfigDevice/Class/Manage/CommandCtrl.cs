using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class CommandCtrl
    {
        private MySocket mySocket = MySocket.GetInstance();
        public Device device;//-----设备---
        public event CallbackUIAction OnCallbackUI_Action;   //----回调UI----
        public CallbackFromUDP callbackGetCommandData;//---回调获取指令----
        public CommandCtrl(Device value)
        {
            this.device = value;
            callbackGetCommandData = new CallbackFromUDP(getCommandData);
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_COMMAND, callbackGetCommandData);//---注册回调----
        }

        /// <summary>
        /// 回调UI
        /// </summary>
        /// <param name="values"></param>
        private void CallbackUI(CallbackParameter callbackParameter)
        {
            if (this.OnCallbackUI_Action != null)
                OnCallbackUI_Action(callbackParameter);
        }

        /// <summary>
        /// 获取指令数据
        /// </summary>
        public void ReadCommandData(int groupNum, int startNum, int endNum)
        {
            UdpData udpSend = createReadCommandsUdp(groupNum, startNum, endNum);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadCommands), new object[] { udpSend });
        }
        private void callbackReadCommands(UdpData udpReply, object[] values)
        {
            UdpData udpSend = (UdpData)values[0];
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取指令失败!", udpReply.ReplyByte);
        }
        private UdpData createReadCommandsUdp(int _grounNum, int _startNum ,int _endNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_COMMAND;//----用户命令-----
            byte len = 0x08;//---数据长度----
            byte kind = device.ByteKindID;//指令类型(未用到,不作处理)
            byte groupNum = (byte)_grounNum;
            byte startNum = (byte)_startNum;
            byte endNum = (byte)_endNum;
            byte[] crcData = new byte[10 + 8 - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = kind;
            crcData[11] = groupNum;
            crcData[12] = startNum;
            crcData[13] = endNum;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        public void getCommandData(UdpData data, object[] values)
        {
            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
            UserUdpData userData = new UserUdpData(data);
            CallbackUI(new CallbackParameter( new object[] { userData }));//----界面回调------
        }


        /// <summary>
        /// 保存指令
        /// </summary>
        /// <param name="groupIndex">命令组</param>
        /// <param name="num">命令序号</param>
        /// <param name="data">命令数据</param>
        public void SaveCommandData(CommandData commandData)
        {
            UdpData udpSend = createWriteCommandUdp(commandData);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult),
                new object[] { "保存第" + (commandData.ucCmdNum + 1).ToString() + "指令失败!" });
        }
        private UdpData createWriteCommandUdp(CommandData commandData)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_COMMAND;//----用户命令-----
            byte len = (byte)(commandData.Len + 4);//---数据长度----

            byte[] crcData = new byte[10 + commandData.Len];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len; 

            Buffer.BlockCopy(commandData.GetValue(), 0, crcData, 10, commandData.Len);//----命令数据-----
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }


        /// <summary>
        /// 删除指令
        /// </summary>
        /// <param name="groupIndex">分组序号</param>
        /// <param name="num">开始序号</param>
        /// <param name="data">结束序号</param>
        public void DelCommandData(int groupIndex,int startIndex,int endIndex)
        {
            UdpData udpSend = createDelCommandUdp( groupIndex, startIndex, endIndex);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult),
                new object[] { "删除第" + (startIndex + 1).ToString() + "指令失败!" });
        }
        private UdpData createDelCommandUdp(int groupIndex, int startIndex, int endIndex)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_DEL_COMMAND;//----用户命令-----
            byte len = 8;//---数据长度----

            byte[] crcData = new byte[10 + 8 - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len; 
            crcData[10] = 0;
            crcData[11] = (byte)groupIndex;
            crcData[12] = (byte)startIndex;
            crcData[13] = (byte)endIndex;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }
    }
}
