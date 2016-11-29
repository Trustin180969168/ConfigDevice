using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data;

namespace ConfigDevice
{

    public class WeiXin : Network
    {
        public WeiXinMenu WeiXinMenu;//---微信菜单模块
        public string Address = "";//设备地址  

        private CallbackFromUDP callbackGetAddress;
        public WeiXin(UserUdpData userData)
            : base(userData)
        {
            WeiXinMenu = new WeiXinMenu(this);
            callbackGetAddress = new CallbackFromUDP(getCallbackAddressData);
        } 

        /// <summary>
        /// 保存名称
        /// </summary>
        /// <param name="newName">新名称</param>
        public void SaveAddress(string address)
        {
            byte[] byteAddress = Encoding.GetEncoding("GB2312").GetBytes(address);
            if (byteAddress.Length > 60)
            {
                CommonTools.MessageShow("地址超出60字节!", 2, "");
                return;
            }
            UdpData saveNameUdp = createSaveAddressUdp(address);
            MySocket.GetInstance().SendData(saveNameUdp, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSaveAddress),
                new object[] { address  });
        }
        private void callbackSaveAddress(UdpData udpReply, object[] values)
        {
            string newAddress = (string)values[0]; 
            if (udpReply.ReplyByte == REPLY_RESULT.CMD_TRUE)
            {
                this.Address = newAddress;    
            }
            else
                CommonTools.ShowReplyInfo("保存名称失败!", udpReply.ReplyByte);
        }
        /// <summary>
        /// 创建修改设备ID的UDP包
        /// </summary>
        /// <returns>返回UDP包</returns>
        private UdpData createSaveAddressUdp(string address)
        {
  
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkID, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_ADDRESS;//----用户命令-----  
            byte[] byteAddress = Encoding.GetEncoding("GB2312").GetBytes(address);
            byte len = (byte)(byteAddress.Length + 4);
            byte[] crcData = new byte[10+byteAddress.Length];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            Buffer.BlockCopy(byteAddress, 0, crcData, 10, byteAddress.Length);
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
 
        }

        /// <summary>
        /// 读取地址
        /// </summary>
        public void ReadAddress()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_ADDRESS, this.DeviceID, callbackGetAddress);//回调刷新结果
            UdpData udpSend = createReadAddressUdp();
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackRequestReadAddress), null);
        }
        private void callbackRequestReadAddress(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取地址失败!", udpReply.ReplyByte);
        }
        private UdpData createReadAddressUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkID, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_ADDRESS;//----用户命令-----

            byte[] crcData = new byte[10];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = 0x04;
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取设备
        /// </summary>
        private void getCallbackAddressData(UdpData data, object[] values)
        {
            //------获取数据-----
            UserUdpData userData = new UserUdpData(data);
            if (userData.TargetID != this.DeviceID)
                return;
            byte[] byteName = CommonTools.CopyBytes(userData.Data, 0, userData.DataLength - 4);
            Address = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0').Trim().Replace("", ""); 
            CallbackUI(new CallbackParameter(ActionKind.ReadServerAddress,DeviceID, Address));//---返回UI---- 

        }

    }

}
