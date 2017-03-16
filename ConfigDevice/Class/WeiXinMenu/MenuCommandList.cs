using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class MenuCommandList:CommandList
    {  
        public MenuData menuData;                           //----当前读取指令的菜单
        private CallbackFromUDP getWriteEnd;                //----获取结束读取信息----

        public MenuCommandList(Device value ):base(value)
        {      
       
        }

  

        /// <summary>
        /// 获取指令数据
        /// </summary>
        public override void ReadCommandData(CommandReadObj menu, int startNum, int endNum)
        {
            menuData = menu.MenuData;
            UdpData udpSend = createReadCommandsUdp(menu.MenuData, startNum, endNum);
            callbackGetCommandData.Parameters =new object[]{ menuData.MenuID};//---回调参数----
            getWriteEnd = new CallbackFromUDP(this.getWriteEndData,new object[]{menuData.MenuID});//---回调参数---- 
            //SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_COMMAND, menuData.MenuID.ToString(), callbackGetCommandData);//---注册回调----
            //SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, menuData.MenuID.ToString(), getWriteEnd);//---注册结束回调---
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_COMMAND, HandleId, callbackGetCommandData);//---注册回调----
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, HandleId, getWriteEnd);//---注册结束回调---
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadCommands), null);
        }
        private void callbackReadCommands(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取指令失败!", udpReply.ReplyByte); 
        }
        private UdpData createReadCommandsUdp(MenuData menu, int _startNum, int _endNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_MMSG_READ_COMMAND;//----用户命令-----
           // byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_MULTI;//----用户命令-----
            byte len = 11;//---数据长度---- 
 
            byte[] crcData = new byte[10 + len - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            Buffer.BlockCopy(menu.ByteArrMenuID, 0, crcData, 10, 4);
            crcData[14] = menu.ByteKindID;
            crcData[15] = (byte)_startNum;
            crcData[16] = (byte)_endNum;

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
        public override void GetCommandData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != this.device.DeviceID) return;//不是本设备ID不接收.
            MenuData tempMenuData = new MenuData(userData);
            if ((uint)values[0]  != (uint)menuData.MenuID) return;//返回不是本菜单---
            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认----- 

            CallbackUI(new CallbackParameter(ActionKind.ReadMenuCommand, new object[] { userData }));//----界面回调------
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getWriteEndData(UdpData data, object[] values)
        {  
            UserUdpData userData = new UserUdpData(data);
            byte[] cmd = new byte[] { userData.Data[0], userData.Data[1] };//----找出回调的命令-----
            if (userData.SourceID == device.DeviceID && (int)values[0] == menuData.MenuID)
            {
                UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认----- 
            }
        }

        /// <summary>
        /// 保存指令
        /// </summary>
        /// <param name="groupIndex">命令组</param>
        /// <param name="num">命令序号</param>
        /// <param name="data">命令数据</param>
        public override void SaveCommandData(CommandReadObj readObj, int numIndex, CommandData commandData)
        {
            MenuCommandData menuCommandData = new MenuCommandData(readObj, numIndex, commandData);
            UdpData udpSend = createWriteCommandUdp(menuCommandData);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult),
                new object[] { "保存第" + (numIndex + 1).ToString() + "指令失败!" });
        }
        private UdpData createWriteCommandUdp(MenuCommandData commandData)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_MMSG_WRITE_COMMAND;//----用户命令-----
            byte len = (byte)(commandData.Len + 6);//---数据长度----

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
        public override void DelCommandData(CommandReadObj data,int startIndex,int endIndex)
        {
            UdpData udpSend = createDelCommandUdp(startIndex, endIndex);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult),
                new object[] { "删除第" + (startIndex + 1).ToString() + "指令失败!" });
        }
        private UdpData createDelCommandUdp(int startIndex, int endIndex)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_MMSG_DEL_COMMAND;//----用户命令-----
            byte len = 11;//---数据长度----

            byte[] crcData = new byte[10 + len - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            Buffer.BlockCopy(menuData.ByteArrMenuID, 0, crcData, 10, 4);
            crcData[14] = menuData.ByteKindID;
            crcData[15] = (byte)startIndex;
            crcData[16] = (byte)endIndex;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 指令测试
        /// </summary>
        /// <param name="groupIndex">指令序号</param>
        public override void TestCommands(CommandReadObj Index)
        {
            UdpData udpSend = createTestCommandsUdp(Index.Index);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackTestCommandsData), null);
        }
        private void callbackTestCommandsData(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("发送指令测试失败!", udpReply.ReplyByte);
        }
        private UdpData createTestCommandsUdp(int groupIndex)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_TEST_KEY_CMD;//----用户命令-----
            byte len = (byte)(4 + 3);//---数据长度---- 
            byte byteGroupNum = (byte)menuData.MenuID;//--组号--

            //---------生成校验码-----------
            byte[] crcData = new byte[10 + 3];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = byteGroupNum;
            crcData[11] = 1;        //功能值/开关 (0：关，1：开)
            crcData[12] = 100;      //方向值/亮度 (0~100：强置亮度，其它：使用各自指令设置的亮度) 
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
