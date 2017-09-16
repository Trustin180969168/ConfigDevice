using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class WirlessTransform : Device
    {
        private CallbackFromUDP getReadDevList;//---获取无效设备信息---- 
        private CallbackFromUDP getActionResultInfo;       //----获取操作结果信息---- 
        public List<WirlessDeviceData> WireLessDeviceList = new List<WirlessDeviceData>();//无线设备列表
        public WirlessTransform(UserUdpData userUdpData)
            : base(userUdpData)
        {
            getReadDevList = new CallbackFromUDP(getReadDevListData);
            getActionResultInfo = new CallbackFromUDP(getActionResultData);
        }

        public WirlessTransform(DeviceData data)
            : base(data)
        {
            getReadDevList = new CallbackFromUDP(getReadDevListData);
            getActionResultInfo = new CallbackFromUDP(getActionResultData);
        }

        public WirlessTransform(DataRow dr)
            : base(dr)
        {
            getReadDevList = new CallbackFromUDP(getReadDevListData);
            getActionResultInfo = new CallbackFromUDP(getActionResultData);
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
            byte[] crcData = new byte[10+len-4];
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
            WirlessDeviceData wirlessDeviceData = new WirlessDeviceData(new UserUdpData(data));
            WireLessDeviceList.Add(wirlessDeviceData);
            CallbackUI(new CallbackParameter(ActionKind.ReadWirlessDevice, this.DeviceID, wirlessDeviceData));//----读完状态信息,回调界面----
        }




        /// <summary>
        /// 申请修改设备
        /// 默认从0开始读取,endIndex为最后设备
        /// </summary>
        public void SaveWirlessData(WirlessDeviceData wirlessData)
        {
            UdpData udpSend = createWriteWirlessDataUdp(wirlessData);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackWriteWirlessDataUdp), wirlessData);
        }
        private void callbackWriteWirlessDataUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("保存无线设备失败!", udpReply.ReplyByte);
            else
                CallbackUI(new CallbackParameter(ActionKind.WirteWirlessDevice, this.DeviceID, values[0] as WirlessDeviceData));//----读完状态信息,回调界面----

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
            byte[] cmd = DeviceConfig.CMD_RFLINE_WRITE_DEV_LIST;//----用户命令-----

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

        /// <summary>
        /// 增加,删除,清除,操作枚举
        /// </summary>
        public enum WirlessDataActionKind
        {
            ADD = 1,
            DEL = 0,
            CLEAR = 2
        }

        /// <summary>
        /// 申请删除设备 
        /// </summary>
        public void DelWirlessData(WirlessDeviceData wirlessData)
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_RFLINE_WRITE_DEVAC_RSL, this.DeviceID, getActionResultInfo);
            UdpData udpSend = createActionWirlessDataUdp(wirlessData, WirlessDataActionKind.DEL);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackDelActionWirlessDataUdp), wirlessData);
        }
        private void callbackDelActionWirlessDataUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请删除设备失败!", udpReply.ReplyByte);
            //else
            //    CallbackUI(new CallbackParameter(ActionKind.DelWirlessDevice, this.DeviceID, values[0] as WirlessDeviceData));//---告诉界面,删除成功
        }


        /// <summary>
        /// 申请增加设备 
        /// </summary>
        public void AddWirlessData(WirlessDeviceData wirlessDeviceData)
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_RFLINE_WRITE_DEVAC_RSL, this.DeviceID, getActionResultInfo);
            UdpData udpSend = createActionWirlessDataUdp(wirlessDeviceData, WirlessDataActionKind.ADD);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackAddActionWirlessDataUdp), null);
        }
        private void callbackAddActionWirlessDataUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请增加设备失败!", udpReply.ReplyByte);
            //else
            //    CallbackUI(new CallbackParameter(ActionKind.AddWirlessDevice, this.DeviceID));//---告诉界面,增加成功

        }


        /// <summary>
        /// 申请清除设备 
        /// </summary>
        public void ClearWirlessData(WirlessDeviceData wirlessDeviceData)
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_RFLINE_WRITE_DEVAC_RSL, this.DeviceID, getActionResultInfo);
            UdpData udpSend = createActionWirlessDataUdp(wirlessDeviceData, WirlessDataActionKind.CLEAR);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackClearActionWirlessDataUdp), null);
        }
        private void callbackClearActionWirlessDataUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请清除设备失败!", udpReply.ReplyByte);
            //else
            //    CallbackUI(new CallbackParameter(ActionKind.AddWirlessDevice, this.DeviceID));//---告诉界面,增加成功 
        }

        private UdpData createActionWirlessDataUdp(WirlessDeviceData wirlessData, WirlessDataActionKind flag)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_RFLINE_WRITE_DEVAC;//----用户命令-----


            byte len = (byte)(4 + 14);//---数据长度----
            byte[] crcData = new byte[10 + len - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            crcData[10] = wirlessData.Index;
            crcData[11] = (byte)flag;
            Buffer.BlockCopy(wirlessData.MacAddress, 0, crcData, 12, 12);
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
        private void getActionResultData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != this.DeviceID) return;//不是本设备ID不接收.
            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----   
            //------找出数据段------
            WirlessActionResultData resultData = new WirlessActionResultData(userData.Data);
            CallbackUI(new CallbackParameter(ActionKind.AddDelClearWirlessDevice, this.DeviceID, resultData));//----读完状态信息,回调界面----
        }
    }
}
