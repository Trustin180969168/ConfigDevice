using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class SecurityObj : ControlObj
    {
        private byte[] securityLevel = new byte[4];//----安全级别------

        public byte[] SecurityLevel
        {
            get { return securityLevel; }
            set { securityLevel = value; }
        }
        private CallbackFromUDP getSafeSetting;//---获取安全配置----

        public SecurityObj(Device _deviceCtrl)
            : base(_deviceCtrl)
        {
            getSafeSetting = new CallbackFromUDP(this.getSafeSettingData);
            Name = DeviceConfig.CONTROL_OBJECT_SECURITY;         
        }

        /// <summary>
        /// 设置安防
        /// </summary>
        /// <param name="value"></param>
        public void SetSecurity(byte[] value)
        {
            //--------安防配置------
            securityLevel = CommonTools.CopyBytes(value, 0, 4);
            byte b1 = value[0];
            byte b2 = value[1];
            int num = 0;
            for (int i = 1; i <= 128; i *= 2)
                SaftFlags[num++] = (int)(b1 & i) == i ? true : false;
            num = 8;
            for (int i = 1; i <= 64; i *= 2)
                SaftFlags[num++] = (int)(b2 & i) == i ? true : false;
        }

        /// <summary>
        /// 获取安防字节
        /// </summary>
        /// <returns></returns>
        public byte[] GetSecurityBytes()
        {
            //byte b1 = 0;
            //byte b2 = 0;
            //int num = 0;
            //for (int i = 1; i <= 128; i *= 2)
            //    if (SaftFlags[num++])
            //        b1 = (byte)(b1 | i);
            //num = 8;
            //for (int i = 1; i <= 64; i *= 2)
            //    if (SaftFlags[num++])
            //        b2 = (byte)(b2 | i);

            //return new byte[] { b1, b2 };
            return securityLevel;
            //return ConvertTools.GetByteFromUInt16(security);
        }

        //-----安防标志----目前为两个字节------
        public bool[] SaftFlags
        {
            get
            {
                byte b1 = securityLevel[0];
                byte b2 = securityLevel[1];
                int num = 0;
                bool[] safeFlags = new bool[] { false, false, false, false, false, false, false, false, false, false,
                    false, false, false, false, false };
                for (int i = 1; i <= 128; i *= 2)
                    safeFlags[num++] = (int)(b1 & i) == i ? true : false;
                num = 8;
                for (int i = 1; i <= 64; i *= 2)
                    safeFlags[num++] = (int)(b2 & i) == i ? true : false;
                return safeFlags;
            }
            set
            {
                byte b1 = 0;
                byte b2 = 0;
                int num = 0;
                for (int i = 1; i <= 128; i *= 2)
                    if (value[num++])
                        b1 = (byte)(b1 | i);
                num = 8;
                for (int i = 1; i <= 64; i *= 2)
                    if (value[num++])
                        b2 = (byte)(b2 | i);

                securityLevel[0] = b1;
                securityLevel[1] = b2;
            }
        }
         

        /// <summary>
        /// 安防动作关联
        /// </summary>
        public void ReadSafeSetting(int groupNum)
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_LOGIC_WRITE_SECURITY, deviceControled.DeviceID, getSafeSetting);
            UdpData udpSend = createReadSafeSettingUdp(groupNum);
            mySocket.SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadSafeSetting), null);
        }
        private void callbackReadSafeSetting(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取安全配置失败!", udpReply.ReplyByte);
        }
        private UdpData createReadSafeSettingUdp(int groupNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_LOGIC_READ_SECURITY;//----用户命令-----
            byte len = 4 + 2;//---数据长度----
            byte[] crcData = new byte[10 + 2];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            crcData[10] = (byte)groupNum;
            crcData[11] = (byte)groupNum;
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取安全数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getSafeSettingData(UdpData data, object[] values)
        {
            UdpTools.ReplyDataUdp(data);//----回复确认-----
            UserUdpData userData = new UserUdpData(data);           
            SetSecurity(new byte[]{userData.Data[1], userData.Data[2],userData.Data[3],userData.Data[4]}); //------找出数据,并翻译------ 
            CallbackUI(new CallbackParameter(this.GetType().Name, ActionKind.ReadSafe));//----读完状态信息,回调界面---
        }


        /// <summary>
        /// 保存安防配置
        /// </summary>
        public void SaveSafeSetting(int groupNum)
        {
            UdpData udpSend = createWriteSafeLogicUdp(groupNum);
            mySocket.SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackWriteSafeLogic), null);
        }
        private void callbackWriteSafeLogic(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请写逻辑附加动作失败!", udpReply.ReplyByte);
        }
        private UdpData createWriteSafeLogicUdp(int groupNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_LOGIC_WRITE_SECURITY;//----用户命令-----

            byte len = 4 + 9;//---数据长度----
            byte[] crcData = new byte[10 + 9];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            crcData[10] = (byte)groupNum;
            //-------安防级别----------------- 
            crcData[11] = securityLevel[0];
            crcData[12] = securityLevel[1]; 
            //---其他字节默认为0,保留-----

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
