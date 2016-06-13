using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class LogicList
    {
        private MySocket mySocket = MySocket.GetInstance();
        public Device device;//-----设备---
        public event CallbackUIAction OnCallbackUI_Action;   //----回调UI----
        public CallbackFromUDP callbackGetLogicData;      //---回调获取指令----
        public const int LogicCount = 4;//----逻辑配置数---
        private CallbackFromUDP finishGetData;//-------完成数据读取----
        public LogicList(Device value)
        {
            this.device = value;
            callbackGetLogicData = new CallbackFromUDP(getLogicData);
            finishGetData = new CallbackFromUDP(finishReadData);
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_LOGIC_WRITE_CONFIG, callbackGetLogicData);//---注册回调----
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
        /// 申请读取逻辑配置
        /// </summary>
        /// <param name="startNum">按键/分组 开始=结束</param>
        public void ReadLogicData(int Num)
        {
            ReadLogicData(Num, Num);
        }
        /// <summary>
        /// 申请读取逻辑配置
        /// </summary>
        /// <param name="startNum">按键/分组 开始</param>
        /// <param name="endNum">按键/分组 结束</param>
        public void ReadLogicData( int startNum, int endNum)
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, device.DeviceID, finishGetData);//---注册回调----
            UdpData udpSend = createReadLogicUdp( startNum, endNum);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadLogicData), null);
        }
        private void callbackReadLogicData(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取指令失败!", udpReply.ReplyByte);
        }
        private UdpData createReadLogicUdp( int _startNum, int _endNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_LOGIC_READ_CONFIG;//----用户命令-----
            byte len = 0x06;//---数据长度---- 
            byte startNum = (byte)_startNum;
            byte endNum = (byte)_endNum;
            byte[] crcData = new byte[10 + 6 - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = startNum;
            crcData[11] = endNum;

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
        public void getLogicData(UdpData data, object[] values)
        {
            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
            UserUdpData userData = new UserUdpData(data);
            LogicData logicData = new LogicData(userData);
            CallbackUI(new CallbackParameter( logicData ));//----界面回调------
        }


        /// <summary>
        /// 保存逻辑配置
        /// </summary>
        /// <param name="startNum">按键/分组 开始=结束</param>
        public void SaveLogicData(int groupNum,int logicNum,byte[] values)
        {
            UdpData udpSend = createSaveLogicUdp(groupNum, logicNum, values);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSaveLogicData), null);
        }
        private void callbackSaveLogicData(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("保存逻辑配置失败!", udpReply.ReplyByte);
        }
        private UdpData createSaveLogicUdp(int groupNum, int logicNum, byte[] values)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_LOGIC_WRITE_CONFIG;//----用户命令-----
            byte len = (byte)(4 + 2 + 31 * LogicList.LogicCount);//---数据长度---- 
            byte byteGroupNum = (byte)groupNum;//--组号--
            byte byteLogicNum = (byte)logicNum;//--逻辑号---
            //---------生成校验码-----------
            byte[] crcData = new byte[10 + (4 + 2 + 31 * LogicList.LogicCount) - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = byteGroupNum;
            crcData[11] = byteLogicNum;
            Buffer.BlockCopy(values, 0, crcData, 12, values.Length);
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------

            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }



        /// <summary>
        /// 保存指令
        /// </summary>
        /// <param name="groupIndex">命令组</param>
        /// <param name="num">命令序号</param>
        /// <param name="data">命令数据</param>
        public void SaveLogicData(int startNum,int logicID,TriggerData[] triggers)
        {
            UdpData udpSend = createWriteLogicUdp(startNum, logicID, triggers);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult),
                new object[] { "保存逻辑触发失败!" });
        }
        private UdpData createWriteLogicUdp(int startNum, int logicID, TriggerData[] triggers)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议---- 

            return udp;
        }

        /// <summary>
        /// 完成读取 
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void finishReadData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != device.DeviceID) return;//不是本设备ID不接收.

            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----  
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, device.DeviceID);

        }
    }
}
