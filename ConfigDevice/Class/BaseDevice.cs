using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class BaseDevice : DeviceData
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

        public BaseDevice(UserUdpData userUdpData)
            : base(userUdpData)
        {
            SecurityLevel = new byte[2];
        }

        public BaseDevice(DataRow dr)
            : base(dr)
        {
            SecurityLevel = new byte[2];
        }




    }
}
