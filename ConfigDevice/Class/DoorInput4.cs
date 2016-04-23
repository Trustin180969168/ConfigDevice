using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class DoorInput4 : DeviceData
    {
        public byte[] SecurityLevel;//----安全级别------
        public byte PhysicalShieldingPorts;//----物理端口屏蔽------
        public byte Road1;//---第1路--
        public byte Road2;//---第2路--
        public byte Road3;//---第3路--
        public byte Road4;//---第4路--

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

        //public bool[] SecurityLevelValue = new bool[] { false, false, false, false, false, false, false, false, false, false };//安全级别值翻译为bool
        //public bool[] PhysicalShieldingPortsValue = new bool[] { false, false, false, false };//屏蔽物理端口

        public DoorInput4(UserUdpData userUdpData)
            : base(userUdpData)
        {
            SecurityLevel = new byte[2];
            initCallback();
        }

        public DoorInput4(DataRow dr)
            : base(dr)
        {
            SecurityLevel = new byte[2];
            initCallback();
        }


        /// <summary>
        /// 注册RJ45回调
        /// </summary>
        private void initCallback()
        {            
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
        /// 获取配置信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        public void getSettingInfoData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            SecurityLevel = CommonTools.CopyBytes(userData.Data, 0, 2);//安防级别
            PhysicalShieldingPorts = userData.Data[2];  //----屏蔽物理端口
            Road1 = userData.Data[3];
            Road2 = userData.Data[4];
            Road3 = userData.Data[5];
            Road4 = userData.Data[6];
        }

        /// <summary>
        /// 获取每路门窗名称
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        public void getRoadTitlesData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            SecurityLevel = CommonTools.CopyBytes(userData.Data, 0, 2);//安防级别
            PhysicalShieldingPorts = userData.Data[2];  //----屏蔽物理端口
            
            


            Road1 = userData.Data[3];
            Road2 = userData.Data[4];
            Road3 = userData.Data[5];
            Road4 = userData.Data[6];



        }




    }
}
