using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class VirtualPasswordFingerMarkLock:Device
    {
        private CallbackFromUDP getReadSystemConfig;        //---获取系统配置信息---- 
        private CallbackFromUDP getReadLockConfig;          //---获取锁配置信息---- 
        private CallbackFromUDP getActionResultInfo;       //----获取操作结果信息---- 
        public List<WirlessDeviceData> WireLessDeviceList = new List<WirlessDeviceData>();//无线设备列表
        public VirtualPasswordFingerMarkLock(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initGetCallback();
        } 

        public VirtualPasswordFingerMarkLock(DeviceData data)
            : base(data)
        {
            initGetCallback();
        }

        public VirtualPasswordFingerMarkLock(DataRow dr)
            : base(dr)
        {
            initGetCallback();
        }

        private void initGetCallback()
        {
            getReadSystemConfig = new CallbackFromUDP(getReadSystemConfigData);
            getReadLockConfig = new CallbackFromUDP(getReadLockConfigData);
        } 


        /// <summary>
        /// 申请读取设备
        /// 默认从0开始读取,endIndex为最后设备
        /// </summary>
        public void ReadSystemConfig()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_READ_CONFIG, this.DeviceID, getReadSystemConfig);
            UdpData udpSend = createReadSystemConfigUdp(0, 0);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSystemConfig), null);
        }
        private void callbackSystemConfig(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取配置失败!", udpReply.ReplyByte); 
        }
        private UdpData createReadSystemConfigUdp(int startIndex, int endIndex)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_CONFIG;//----用户命令-----
            byte len = 4 + 2;//---数据长度----
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
        private void getReadSystemConfigData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != this.DeviceID) return;//不是本设备ID不接收.
            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----   
            //------找出数据段------
            LockSysConfigData configData = new LockSysConfigData(new UserUdpData(data).Data);
            CallbackUI(new CallbackParameter(ActionKind.ReadLockSystemConfig, this.DeviceID, configData));//----读完状态信息,回调界面----
        }




        /// <summary>
        /// 申请读取设备
        /// 默认从0开始读取,endIndex为最后设备
        /// </summary>
        public void ReadLockConfig()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_RFLINE_READ_CFG, this.DeviceID, getReadLockConfig);
            UdpData udpSend = createReadLockConfigUdp(0, 255);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackLockConfig), null);
        }
        private void callbackLockConfig(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取锁配置失败!", udpReply.ReplyByte);
        }
        private UdpData createReadLockConfigUdp(int startIndex, int endIndex)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_CONFIG;//----用户命令-----
            byte len = 4 + 2;//---数据长度----
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
        private void getReadLockConfigData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != this.DeviceID) return;//不是本设备ID不接收.
            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----   
            //------找出数据段------
            LockConfigData configData = new LockConfigData(new UserUdpData(data).Data);
            CallbackUI(new CallbackParameter(ActionKind.ReadLockConfig, this.DeviceID, configData));//----读完状态信息,回调界面----
        } 



    }
}
