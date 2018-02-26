using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice 
{
    public class MenuSecurityEdit : BaseMenuEdit
    { 

        public MenuSecurityEdit(WeiXin device,MenuData data):base(device,data)
        {
        }

        /// <summary>
        /// 获取菜单数据
        /// </summary>
        public void ReadMenuSecurity()
        {
            UdpData udpSend = createReadMenuSecurityUdp();
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_SECURITY_CFG,this.MenuData.MenuID.ToString(),callbackGetEditData);//---注册回调----
            mySocket.SendData(udpSend, WeiXinDevice.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadMenuSecurity), null);
        }
        private void callbackReadMenuSecurity(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取安防配置失败!", udpReply.ReplyByte);
        }
        private UdpData createReadMenuSecurityUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { WeiXinDevice.ByteDeviceID, WeiXinDevice.ByteNetworkId, WeiXinDevice.ByteKindID };//----目标信息--
            byte[] source = new byte[] { WeiXinDevice.BytePCAddress, WeiXinDevice.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_MMSG_READ_SECURITY_CFG;//----用户命令----- 
            byte len = 12;//---数据长度---- 

            byte[] crcData = new byte[10 + len - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            byte[] byteArrMenuID = ConvertTools.GetByteFromUInt32(MenuData.MenuID); 
            Buffer.BlockCopy(byteArrMenuID, 0, crcData, 10, 4);
            crcData[14] = (byte)MenuData.ByteKindID;
             
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取编辑数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        public override void GetEditData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != WeiXinDevice.DeviceID) return;
            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----
            MenuSecurityData menuSecurityData = new MenuSecurityData(userData); 
            this.CallbackUI(new CallbackParameter(ActionKind.ReadMenuSecurity,this.WeiXinDevice.DeviceID, menuSecurityData));//----读完状态信息,回调界面----
        }

        /// <summary>
        /// 获取菜单数据
        /// </summary>
        public void SaveMenuSecurity(MenuSecurityData data)
        {
            UdpData udpSend = createSaveMenuSecurityUdp( data);
              mySocket.SendData(udpSend, WeiXinDevice.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadMenuSecurity), null);
        }
        private void callbackSaveMenuSecurity(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请写安防配置失败!", udpReply.ReplyByte);
        }
        private UdpData createSaveMenuSecurityUdp(MenuSecurityData data)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { WeiXinDevice.ByteDeviceID, WeiXinDevice.ByteNetworkId, WeiXinDevice.ByteKindID };//----目标信息--
            byte[] source = new byte[] { WeiXinDevice.BytePCAddress, WeiXinDevice.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_MMSG_WRITE_SECURITY_CFG;//----用户命令----- 
            byte len = 11;//---数据长度---- 

            byte[] crcData = new byte[10 + len - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            byte[] byteArrMenuID = ConvertTools.GetByteFromUInt32(MenuData.MenuID); 
            Buffer.BlockCopy(byteArrMenuID, 0, crcData, 10, 4);
            crcData[14] = (byte)MenuData.ByteKindID;
            crcData[15] = data.ByteSecurityKindID;//是否布防,布防类型，Bit0-1表示室外布防，Bit1-1表示全部布防
            crcData[16] = data.ByteSecurityHomeCancelID;//是否撤防，Bit0-1表示回家键撤防

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
