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
            Address = ConvertTools.ToGB2312Str(byteName); 
            CallbackUI(new CallbackParameter(ActionKind.ReadServerAddress,DeviceID, Address));//---返回UI---- 

        }


        /// <summary>
        /// 获取版本号
        /// </summary>
        public override void SearchVer()
        {
            callbackVer = new CallbackFromUDP(getVer);
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_VER,this.DeviceID, callbackVer);//注册返回版本号
            UdpData udpSearch = createSearchVerUdp();
            MySocket.GetInstance().SendData(udpSearch, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSearchVer), null);
        }
        private void callbackSearchVer(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("获取版本号失败!", udpReply.ReplyByte);
        }
        /// <summary>
        /// 创建读取VER的UDP包
        /// </summary>
        /// <returns></returns>
        private UdpData createSearchVerUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)------
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_MMSG_READ_VER;//----用户命令-----
            byte len = 0x04;//---数据长度---
            //--------添加到用户数据--------
            byte[] userData = new byte[10];
            Buffer.BlockCopy(target, 0, userData, 0, 3);
            Buffer.BlockCopy(source, 0, userData, 3, 3);
            userData[6] = page;
            Buffer.BlockCopy(cmd, 0, userData, 7, 2);
            userData[9] = len;
            byte[] crc = CRC32.GetCheckValue(userData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(userData, 0, udp.ProtocolData, 0, 10);
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, 10, 4);
            Array.Resize(ref udp.ProtocolData, 14);//重新设定长度    
            udp.Length = 28 + 14 + 1;

            return udp;
        }
        private void getVer(UdpData data, object[] values)
        {
            //------回复反馈的设备信息-------
            UdpTools.ReplyDelRJ45SendUdp(data);
            string name = this.Name;
            //-----获取数据-----
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID == this.DeviceID)
            {
                //------获取版本号------                
                byte[] temp1 = new byte[20]; byte[] temp2 = new byte[20];
                Buffer.BlockCopy(userData.Data, 0, temp1, 0, 20);
                SoftwareVer = Encoding.GetEncoding("ASCII").GetString(temp1).TrimEnd('\0');
                Buffer.BlockCopy(userData.Data, 20, temp2, 0, 20);
                HardwareVer = Encoding.GetEncoding("ASCII").GetString(temp2).TrimEnd('\0');

                CallbackUI(new CallbackParameter(ActionKind.GetVer, this.DeviceID));
            }
       
        }

      
        /// <summary>
        /// 创建申请连接网络申请的UDP
        /// </summary>
        /// <param name="network">网络数据</param>
        /// <returns>UDP</returns>
        protected override UdpData createChangePasswordUdpData(string pw, PasswordKind kind)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = 0x01;//----包数据类------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC, ByteKindID };//----目标信息--
            byte[] source = new byte[] { DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PC_CHANGENET;//----用户命令-----
            byte len = 0x20;//---数据长度---

            //---管理员密码---密码:1234 => 0x21,0x43,0xFF,0xFF
            string str1 = pw.Substring(0, 1); string str2 = pw.Substring(1, 1);
            string str3 = pw.Substring(2, 1); string str4 = pw.Substring(3, 1);
            byte[] mangerPw; byte[] userPw;
            if (kind == PasswordKind.Manager)
            {
                mangerPw = ConvertTools.StrToToHexByte(str2 + str1 + str4 + str3 + "FFFF");
                userPw = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };//---用户密码(0xFF代表忽略)---
            }
            else
            {
                mangerPw = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };//---(0xFF代表忽略)--- 
                userPw = ConvertTools.StrToToHexByte(str2 + str1 + str4 + str3 + "FFFF");
            }
            byte[] mac = ByteMacAddress; //---MAC----
            //--------添加到用户数据--------
            byte[] crcData = new byte[38];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            Buffer.BlockCopy(managerPassword, 0, crcData, 10, 4);
            Buffer.BlockCopy(userPassword, 0, crcData, 14, 4);
            Buffer.BlockCopy(mangerPw, 0, crcData, 18, 4);
            Buffer.BlockCopy(userPw, 0, crcData, 22, 4);
            Buffer.BlockCopy(mac, 0, crcData, 26, 12);

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
