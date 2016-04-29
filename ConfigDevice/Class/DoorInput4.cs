using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class DoorInput4 : DeviceData
    {
        public const string AREAR_DOOR_WINDOW_OPEN = "门窗开";
        public const string AREAR_DOOR_WINDOW_CLOSE= "门窗关";
        public const string AREAR_DOOR_ALARM_OPEN = "警报";
        public const string AREAR_DOOR_ALARM_CLOSE = "撤销警报";//区域-门窗关

        private byte[] securityLevel;//----安全级别------
        private byte physicalShieldingPorts;//----物理端口屏蔽------
        private byte Road1;//---第1路--
        private byte Road2;//---第2路--
        private byte Road3;//---第3路--
        private byte Road4;//---第4路--

        public bool RoadShield1 = false;//安防屏蔽
        public int RoadMusicNum1 = 0;//第几曲,1为第一曲
        public string RoadTitle1 = "";//门窗名称

        public bool RoadShield2 = false;//安防屏蔽
        public int RoadMusicNum2 = 0;//第几曲,1为第一曲
        public string RoadTitle2 = "";//门窗名称

        public bool RoadShield3 = false;//安防屏蔽
        public int RoadMusicNum3 = 0;//第几曲,1为第一曲
        public string RoadTitle3 = "";//门窗名称

        public bool RoadShield4 = false;//安防屏蔽
        public int RoadMusicNum4 = 0;//第几曲,1为第一曲
        public string RoadTitle4 = "";//门窗名称

        private CallbackFromUDP getSettingInfo;//----获取设置信息----
        private CallbackFromUDP getRoadTitles;//-------每路门窗名称----

        public bool[] SecurityLevelValue = new bool[] { false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false };//安全级别值翻译为bool
        public bool[] PhysicalShieldingPortsValue = new bool[] { false, false, false, false };//屏蔽物理端口

        public DoorInput4(UserUdpData userUdpData)
            : base(userUdpData)
        {
            securityLevel = new byte[2];
            initCallback();
        }

        public DoorInput4(DataRow dr)
            : base(dr)
        {
            securityLevel = new byte[2];
            initCallback();
        }


        /// <summary>
        /// 注册RJ45回调
        /// </summary>
        private void initCallback()
        {
            getSettingInfo = new CallbackFromUDP(getSettingInfoData);
            getRoadTitles = new CallbackFromUDP(getRoadTitlesData);
            SysConfig.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_CONFIG, getSettingInfo);
            SysConfig.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME, getRoadTitles);
        }


        /// <summary>
        /// 读取配置信息
        /// </summary>
        public void ReadSettingInfo()
        {
            UdpData udpSend = createReadSettingUdp();
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadSettingInfo), new object[] { udpSend });
        }
        private void callbackReadSettingInfo(UdpData udpReply, object[] values)
        {
            UdpData udpSend = (UdpData)values[0];
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取配置失败!", udpReply.ReplyByte);
        }
        private UdpData createReadSettingUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_CONFIG;//----用户命令-----
            byte len = 0x04;//---数据长度----
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
        /// 申请读取回路名称
        /// </summary>
        public void ReadRoadTitle()
        {
            UdpData udpSend = createReadRoadTitleUdp();
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadRoadTitle), new object[] { udpSend });
        }
        private void callbackReadRoadTitle(UdpData udpReply, object[] values)
        {
            UdpData udpSend = (UdpData)values[0];
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取回路名称失败!", udpReply.ReplyByte);
        }
        private UdpData createReadRoadTitleUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_LOOP_NAME;//----用户命令-----
            byte len = 0x06;//---数据长度----
            byte[] crcData = new byte[12];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = 0;//起始回路为第一回路
            crcData[11] = 0xFF;//结束回路

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
        public void getSettingInfoData(UdpData data, object[] values)
        {
            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
            UserUdpData userData = new UserUdpData(data);
            securityLevel = CommonTools.CopyBytes(userData.Data, 0, 2);//安防级别
            physicalShieldingPorts = userData.Data[2];  //----屏蔽物理端口
            Road1 = userData.Data[3];//----第1路----
            Road2 = userData.Data[4];//----第2路----
            Road3 = userData.Data[5];//----第3路----
            Road4 = userData.Data[6];//----第4路----         
            TranslateValue();//----翻译数据----
            callbackUI(null);//----回调界面----
        }

        /// <summary>
        /// 获取每路门窗名称
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        public void getRoadTitlesData(UdpData data, object[] values)
        {
           
            UserUdpData userData = new UserUdpData(data);         
            byte[] byteName = CommonTools.CopyBytes(userData.Data, 4, userData.DataOfLength - 4 - 4);
            
            int num = userData.Data[0];
            switch (num)
            {
                case 0: RoadTitle1 = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0'); break;
                case 1: RoadTitle2 = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0'); break;
                case 2: RoadTitle3 = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0'); break;
                case 3: RoadTitle4 = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0'); break;
            }
            callbackUI(null);//----回调界面-----
            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
        }

        /// <summary>
        /// 翻译数据
        /// </summary>
        private void TranslateValue()
        {
            //获取一个字节中的每一位的值，需要分别与128 64 32 16 8 4 2 1相与&运算
            //假设字节为byte1 
            //bit8 = byte1 & 128 == 128 ? 1 : 0; 
            //bit7 = byte1 & 64 == 64 ? 1 : 0; 
            //bit6 = byte1 & 32 == 32 ? 1 : 0; 
            //bit5 = byte1 & 16 == 16 ? 1 : 0; 
            //bit4 = byte1 & 8 == 8 ? 1 : 0; 
            //bit3 = byte1 & 4 == 4 ? 1 : 0; 
            //bit2 = byte1 & 2 == 2 ? 1 : 0; 
            //bit1 = byte1 & 1 == 1 ? 1 : 0;
            //------ 设置安全级别---------
            byte b1 = securityLevel[0];
            byte b2 = securityLevel[1];
            int num = 0;
            for (int i = 1; i <= 128; i *= 2)
                SecurityLevelValue[num++] = (int)(b1 & i) == i ? true : false;
            num = 8;
            for (int i = 1; i <= 64; i *= 2)
                SecurityLevelValue[num++] = (int)(b2 & i) == i ? true : false;
            //-------设置屏蔽端口-----
            num = 0;
            for (int i = 1; i <= 8; i *= 2)
                PhysicalShieldingPortsValue[num++] = (int)(physicalShieldingPorts & i) == i ? true : false;

            RoadShield1 = (int)(Road1 >> 7) == 1 ? true : false;//是否屏蔽
            RoadShield2 = (int)(Road2 >> 7) == 1 ? true : false;//是否屏蔽
            RoadShield3 = (int)(Road3 >> 7) == 1 ? true : false;//是否屏蔽
            RoadShield4 = (int)(Road4 >> 7) == 1 ? true : false;//是否屏蔽
            RoadMusicNum1 = (int)(Road1 & 127);//第几首
            RoadMusicNum2 = (int)(Road2 & 127);
            RoadMusicNum3 = (int)(Road3 & 127);
            RoadMusicNum4 = (int)(Road4 & 127);

        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public void SaveSetting()
        {
            UdpData udpSend = createSaveSettingUdp();
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult), new object[] {  "申请保存配置失败!" });
        }
        private UdpData createSaveSettingUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_CONFIG;//----用户命令-----
            byte len = 0x0B;
            //-------安防级别-----------------
            byte b1 = 0;
            byte b2 = 0;
            int num = 0;
            for (int i = 1; i <= 128; i *= 2)
                if (SecurityLevelValue[num++])
                    b1 = (byte)(b1 | i);
            num = 8;
            for (int i = 1; i <= 64; i *= 2)
                if (SecurityLevelValue[num++])
                    b2 = (byte)(b2 | i);
            securityLevel[0] = b1;
            securityLevel[1] = b2;
            //-------屏蔽物理端口-----------------
            num = 0; physicalShieldingPorts = 0;
            for (int i = 1; i <= 8; i *= 2)
                if (PhysicalShieldingPortsValue[num++])
                    physicalShieldingPorts = (byte)(physicalShieldingPorts | i);
            //-------门窗配置--------------------
            Road1 = 0;
            if (RoadShield1) Road1 = (byte)(Road1 | 128);
            Road1 = (byte)(Road1 | RoadMusicNum1);
            Road2 = 0;
            if (RoadShield2) Road2 = (byte)(Road2 | 128);
            Road2 = (byte)(Road2 | RoadMusicNum2);
            Road3 = 0;
            if (RoadShield3) Road3 = (byte)(Road3 | 128);
            Road3 = (byte)(Road3 | RoadMusicNum3);
            Road4 = 0;
            if (RoadShield4) Road4 = (byte)(Road4 | 128);
            Road4 = (byte)(Road4 | RoadMusicNum4);

            byte[] crcData = new byte[len - 4 + 10];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            Buffer.BlockCopy(securityLevel, 0, crcData, 10, 2);
            crcData[12] = physicalShieldingPorts;
            crcData[13] = Road1;
            crcData[14] = Road2;
            crcData[15] = Road3;
            crcData[16] = Road4;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }


        /// <summary>
        /// 保存回路配置
        /// </summary>
        public void SaveRoadSetting()
        {
            UdpData udpSend = createSaveRoadSettingUdp(0, RoadTitle1);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult), new object[] {  "保存回路1 " + RoadTitle1 + "失败!" });

            udpSend = createSaveRoadSettingUdp(1, RoadTitle2);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult), new object[] { "保存回路2 " + RoadTitle2 + "失败!" });

            udpSend = createSaveRoadSettingUdp(2, RoadTitle3);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult), new object[] {  "保存回路3 " + RoadTitle3 + "失败!" });

            udpSend = createSaveRoadSettingUdp(3, RoadTitle4);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult), new object[] {  "保存回路4 " + RoadTitle4 + "失败!" });
        }
        private UdpData createSaveRoadSettingUdp(int roadNum,string roadName)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME;//----用户命令-----
            //---------计算长度------------------
            //---------新名称-------------
            byte[] value = Encoding.GetEncoding("GB2312").GetBytes(roadName);
            byte[] byteName = new byte[32];
            Buffer.BlockCopy(value, 0, byteName, 0, value.Length);
            byte len = (byte)(1 + 2 + 1 + byteName.Length + 4);//---数据长度 = 第几路1 + 位置2 + 保留1 + 名称n + 校验码4-----   

            byte[] crcData = new byte[len - 4 + 10];//10 固定长度:源+目标+命令+长度+分页
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = (byte)roadNum;
            crcData[13] = 2;//11,12为位置,直接填0x0,0x0,忽略, 13为位置, 直接填0x02.
            Buffer.BlockCopy(byteName, 0, crcData, 14, byteName.Length);

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
