﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class Command
    {
        private MySocket mySocket = MySocket.GetInstance();
        public Device device;//-----设备---
        public event CallbackUIAction OnCallbackUI_Action;   //----回调UI----
        public CallbackFromUDP callbackGetCommandData;//---回调获取指令----
        public Command(Device value)
        {
            this.device = value;
            callbackGetCommandData = new CallbackFromUDP(getCommandData);
            SysConfig.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_COMMAND, callbackGetCommandData);//---注册回调----
        }

        /// <summary>
        /// 回调UI
        /// </summary>
        /// <param name="values"></param>
        private void CallbackUI(object[] values)
        {
            if (this.OnCallbackUI_Action != null)
                OnCallbackUI_Action(values);
        }

        /// <summary>
        /// 获取指令数据
        /// </summary>
        public void ReadCommandData(int groupNum, int startNum, int endNum)
        {
            UdpData udpSend = createReadCommandsUdp(groupNum,startNum,endNum);
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
            crcData[10] = 0;//起始回路为第一回路
            crcData[11] = 0xFF;//结束回路

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
            CommandData cmdData = new CommandData(userData);

            CallbackUI(new object[]{cmdData});
     
        }




    }
}
