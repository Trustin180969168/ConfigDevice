using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    /// <summary>
    /// 用于配置按键的控制的类
    /// </summary>
    public class ButtonPanelCtrl : ControlObj
    {

        private MySocket mySocket = MySocket.GetInstance();//----通讯---
        private CallbackFromUDP getKeyOption;//---注册回调按键----
        private CallbackFromUDP getKeyState;//---注册回调按键----

        /// <summary>
        /// 按键控制
        /// </summary>
        /// <param name="device"></param>
        public ButtonPanelCtrl(Device device)
            : base(device)
        {
            getKeyOption = new CallbackFromUDP(this.getKeyOptionData);
            getKeyState = new CallbackFromUDP(this.getKeyStateData);
        }

        /// <summary>
        /// 申请读取按键参数
        /// </summary>
        public void ReadKeyOption()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_KB_WRITE_OPTIONS, deviceControled.EditHandleID, getKeyOption);//----注册回调---            
            UdpData udpSend = createReadKeyOptionUdp();
            mySocket.SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadKeyOption), null);
        }
        private void callbackReadKeyOption(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取按键参数失败!", udpReply.ReplyByte);
        }
        private UdpData createReadKeyOptionUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_KB_READ_OPTIONS;//----用户命令-----
            byte len = 4;//---数据长度----
            byte[] crcData = new byte[10];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取按键参数
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getKeyOptionData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != deviceControled.DeviceID) return;//不是本设备ID不接收.

            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----
            ButtonPanelOptionData option = new ButtonPanelOptionData(userData);
            deviceControled.CallbackUI(new CallbackParameter(ActionKind.ReadOption,deviceControled.DeviceID, option));//---回调UI---
          
        }

        /// <summary>
        /// 保存按键配置
        /// </summary>
        public void SaveKeyOption(ButtonPanelOptionData optionData)
        {
            UdpData udpSend = createSaveOptionUdp(optionData);
            mySocket.SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,new CallbackUdpAction(callbackSaveOption), null);
        }
        private void callbackSaveOption(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
            {
                CommonTools.ShowReplyInfo("保存面板配置失败!", udpReply.ReplyByte);
                ReadKeyOption();//---重新获取,回调回界面----
            }
        }
        private UdpData createSaveOptionUdp(ButtonPanelOptionData optionData)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_KB_WRITE_OPTIONS;//----用户命令----- 
            byte len = ButtonPanelOptionData.Length + 4;//---数据长度 = 第几路1 + 位置2 + 保留1 + 名称n + 校验码4-----  
            byte[] crcData = new byte[10 + ButtonPanelOptionData.Length];//10 固定长度:源+目标+命令+长度+分页
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            byte[] value = optionData.GetPanelOptionValue();//---配置数据---
            Buffer.BlockCopy(value, 0, crcData, 10, value.Length);//---配置数据---

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }


        /// <summary>
        /// 申请读取初始状态
        /// </summary>
        public void ReadKeyState()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_KB_WRITE_STARTUP_KEY_STATE, deviceControled.EditHandleID, getKeyState);//----注册回调---            
            UdpData udpSend = createReadKeyStateUdp();
            mySocket.SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadKeyState), null);
        }
        private void callbackReadKeyState(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取按键状态失败!", udpReply.ReplyByte);
        }
        private UdpData createReadKeyStateUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_KB_READ_STARTUP_KEY_STATE;//----用户命令-----
            byte len = 4;//---数据长度----
            byte[] crcData = new byte[10];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取按键参数
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getKeyStateData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != deviceControled.DeviceID) return;//不是本设备ID不接收.

            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----
            int selectIndex = (int)userData.Data[0];

            deviceControled.CallbackUI(new CallbackParameter(ActionKind.ReadSate, deviceControled.DeviceID, selectIndex));//---回调UI---
 
        }


        /// <summary>
        /// 保存按键配置
        /// </summary>
        public void SaveKeyState(int option)
        {
            UdpData udpSend = createSaveStateUdp(option);
            mySocket.SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,new CallbackUdpAction(callbackSaveKeyState), null);
        }
        private void callbackSaveKeyState(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
            {
                CommonTools.ShowReplyInfo("保存面板初始化状态失败!", udpReply.ReplyByte);
                ReadKeyState();//---重新获取,回调回界面----
            }
        }
        private UdpData createSaveStateUdp(int option)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_KB_WRITE_STARTUP_KEY_STATE;//----用户命令----- 
            byte len = 17 + 4;//---数据长度 = 第几路1 + 位置2 + 保留1 + 名称n + 校验码4-----  
            byte[] crcData = new byte[10 + 17];//10 固定长度:源+目标+命令+长度+分页
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            byte value = (byte)(option);
            byte[] values = new byte[] { value, value, value, value, value, value, value, value, value, value, 
                                         value, value, value, value, value, value, value, };
            Buffer.BlockCopy(values, 0, crcData, 10, values.Length);//---数据-----
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
