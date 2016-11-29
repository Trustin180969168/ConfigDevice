using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class WeiXinSecurity
    {
        public MenuData WeiXinMenuData;//编辑的菜单内容
        private WeiXin device;//微信设备
        public CallbackFromUDP callbackSecurityData;//---回调获取指令----
        public event CallbackUIAction OnCallbackUI_Action;   //----回调UI----
        private string ObjUuid = Guid.NewGuid().ToString();//唯一标识对象uuid
        private MySocket mySocket = MySocket.GetInstance();

        public void InitWeiXinSecurity(MenuData _menuData,WeiXin _weiXinDevice)
        {
            this.WeiXinMenuData = _menuData;
            this.device = _weiXinDevice;
        }

        public WeiXinSecurity()
        {
            callbackSecurityData = new CallbackFromUDP(getSecurityData, new object[] { ObjUuid });
        }

        /// <summary>
        /// 获取菜单安防数据
        /// </summary>
        public void ReadSecurityData()
        {
            UdpData udpSend = createReadSecurityUdp();
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_MEMU_NAME, ObjUuid, callbackSecurityData);//---注册回调----
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadMenus), null);
        }
        private void callbackReadMenus(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取菜单失败!", udpReply.ReplyByte);
        }
        private UdpData createReadSecurityUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_MMSG_READ_MEMU_NAME;//----用户命令----- 
            byte len = 12;//---数据长度---- 

            byte[] byteMenuID = ConvertTools.GetByteFromUInt32( WeiXinMenuData.MenuID);
            byte byteKindID = (byte)WeiXinMenuData.KindID;
            byte[] crcData = new byte[10 + len - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            Buffer.BlockCopy(byteMenuID, 0, crcData, 10, 4);//菜单ID
            crcData[14] = byteKindID; //菜单类型ID 
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
        public void getSecurityData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if ((string)values[0] != ObjUuid) return;//不是本菜单ID不接收.
            UdpTools.ReplyDataUdp(data);//----回复确认----- 

            OnCallbackUI_Action(new CallbackParameter(ActionKind.ReadSafe, this.WeiXinMenuData.MenuID, new object[] { userData }));//----界面回调------
        }

        /// <summary>
        /// 保存/增加菜单
        /// </summary>
        /// <param name="groupIndex">命令组</param>
        /// <param name="num">命令序号</param>
        /// <param name="data">命令数据</param>
        public void SaveMenuData(MenuData menuData)
        {
            UdpData udpSend = createWriteMenuUdp(menuData);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult),
                new object[] { "保存第" + (menuData.MenuID).ToString() + "菜单失败!" });
        }
        private UdpData createWriteMenuUdp(MenuData menuData)
        {
            UdpData udp = new UdpData();
            byte[] value = menuData.GetByteData();
            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_MMSG_WRITE_MEMU_NAME;//----用户命令-----
            byte len = (byte)(value.Length + 4);//---数据长度----

            byte[] crcData = new byte[10 + value.Length];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            Buffer.BlockCopy(value, 0, crcData, 10, value.Length);//----命令数据-----
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
