using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class WirlessTransform:Device
    {
        private CallbackFromUDP getReadDevList;//---获取无效设备信息---- 
        public List<WirlessDeviceData> WireLessDeviceList = new List<WirlessDeviceData>();//无线设备列表
        public WirlessTransform(UserUdpData userUdpData)
            : base(userUdpData)
        {
            getReadDevList = new CallbackFromUDP(getReadDevListData);
        }  


        /// <summary>
        /// 申请读取设备
        /// 默认从0开始读取,endIndex为最后设备
        /// </summary>
        public void ReadDevList(int startIndex, int endIndex)
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_RFLINE_WRITE_DEV_LIST, this.DeviceID, getReadDevList);
            UdpData udpSend = createReadDevListUdp(startIndex, endIndex);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadDevList), null);
        }
        private void callbackReadDevList(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取无线设备失败!", udpReply.ReplyByte);
            else
                WireLessDeviceList.Clear();
        }
        private UdpData createReadDevListUdp(int startIndex, int endIndex)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_RFLINE_READ_DEV_LIST;//----用户命令-----
            byte len = 6;//---数据长度----
            byte[] crcData = new byte[10 + 2];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = (byte)startIndex;
            crcData[11] = (byte)endIndex;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取设备列表数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getReadDevListData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != this.DeviceID) return;//不是本设备ID不接收.
            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----   
            //------找出数据段------
            WirlessDeviceData wirlessDeviceData = new WirlessDeviceData(data.ProtocolData);
            CallbackUI(new CallbackParameter(ActionKind.ReadWirlessDevice, this.DeviceID, wirlessDeviceData ));//----读完状态信息,回调界面----
        }


        /// <summary>
        /// 申请修改设备
        /// 默认从0开始读取,endIndex为最后设备
        /// </summary>
        public void SaveWirlessData(WirlessDeviceData wirlessData)
        {
            UdpData udpSend = createWriteWirlessDataUdp(wirlessData);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackWriteWirlessDataUdp), null);
        }
        private void callbackWriteWirlessDataUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取无线设备失败!", udpReply.ReplyByte);
            else
                WireLessDeviceList.Clear();
        }
        private UdpData createWriteWirlessDataUdp(WirlessDeviceData wirlessData)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_RFLINE_READ_DEV_LIST;//----用户命令-----

            byte[] value = wirlessData.ToByteArray();
            byte len = (byte)(4 + value.Length);//---数据长度----
            byte[] crcData = new byte[10 + value.Length];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            Buffer.BlockCopy(crcData, 10, value, 0, value.Length);
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
