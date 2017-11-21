using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    /// <summary>
    /// 面板传感器配置
    /// </summary>
    public class ControlObjectInfo
    {
        public UInt16 ObjectKind = 0;//控制对象
        public byte DeviceNetworkID = 0;//设备1网段
        public byte DeviceID = 0;//设备1ID

        public ControlObjectInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value"></param>
        public ControlObjectInfo(byte[] value)
        {
            ObjectKind = ConvertTools.Bytes2ToUInt16(value[0], value[1]);
            DeviceNetworkID = value[2];
            DeviceID = value[3];
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <returns></returns>
        public byte[] GetValue()
        {
            byte[] values = new byte[4];
            values[0] = ConvertTools.GetByteFromUInt16(ObjectKind)[0];
            values[1] = ConvertTools.GetByteFromUInt16(ObjectKind)[1];
            values[2] = this.DeviceNetworkID;
            values[3] = this.DeviceID;

            return values;
        }

    }

    public class SpecialPanelOptionData : ButtonPanelOptionData
    {
        /*
 
=====================Content:======================
            bit0 --- 场景1
            bit1 --- 场景2
            bit2 --- 灯光1
            bit3 --- 灯光2
            bit4 --- 窗帘1
            bit5 --- 窗帘2
            bit6 --- 空调1
            bit7 --- 窗帘2
            bit8 --- 音响
 
========================= 离家键  数据位值1表示开，0表示关（液晶面板长键2键才进入布防状态）======
            Bit0--- 室外布防 
            Bit1--- 全部布防
            Bit2--- 门窗显示
            Bit3--- 门窗提示音
            Bit4--- 布防前需要输入密码标志
         
==========================GoHome回家键==============================
            Bit 0--- 回家键撤防（液晶面板如果是开启撤防功能，当有布防时就会弹出撤防菜单，不需要长按键）
            Bit1--- 预警报提示音
            Bit2--- 红外线开关
            Bit3--- 时间屏保
            Bit4--- 门窗显示 关窗提示音
            Bit5--- 门窗显示 锁窗提示音
            Bit6--- 指示灯熄灭时微亮(廖超庭:2015-3-19加入)

=====================DoorWindowShowSetting  门窗显示设置 ======================
            Bit0~bit1   0 关窗锁窗(第一页显示关窗，第二页显示锁窗)
                        1 锁窗关窗(第一页显示锁窗，第二页显示关窗)
                        2 关窗    (只为示关窗)
                        3 锁窗    (只显示锁窗)
            Bit7        1    全部房间显示，
                        0    只显示本房间
            原来显示分为：全部显示、只显示没关没锁，现在固定为只显示没关没锁。
 
        */
        public const string KEY_WKMD_0 = "默认工作模式(全部按键为通用键)";//◆当面板为[通用+专用面板]时，用作[按键功能分类]配置参数(廖超庭:2017-10-27修改)◆
        public const string KEY_WKMD_1 = "2键台面清洗通用键   + 3键通用键";// ◆当面板为[通用+专用面板]时，用作[按键功能分类]配置参数(廖超庭:2017-10-27修改)◆
        public const string KEY_WKMD_2 = "2键上下升降柜专用键 + 3键通用键";// ◆当面板为[通用+专用面板]时，用作[按键功能分类]配置参数(廖超庭:2017-10-27修改)◆
        public const string KEY_WKMD_3 = "1键新风键           + 3键通用键";// ◆当面板为[通用+专用面板]时，用作[按键功能分类]配置参数(廖超庭:2017-10-27修改)◆
        public const string KEY_WKMD_4 = "2键3D抽油烟键       + 3键通用键";// ◆当面板为[通用+专用面板]时，用作[按键功能分类]配置参数(廖超庭:2017-10-27修改)◆



        //-------最多2个专用控制对象数据-----
        public ControlObjectInfo[] ControlObjects = new ControlObjectInfo[2];

        /// <summary>
        /// 门窗显示
        /// </summary>
        public bool DoorWindowShowing
        {
            get { return ((LeaveHome & 4) == 4) ? true : false; }
            set
            {
                if (value)
                    LeaveHome = (byte)(LeaveHome | 4);//0000 0100
                else
                    LeaveHome = (byte)(LeaveHome & 251);//1111 1011
            }
        }






        /// <summary>
        /// 门窗显示内容
        /// </summary>
        public int DoorWindowShowAllID
        {
            get { return ((DoorWindowShowSetting & 128) == 128) ? 1 : 0; }
            set
            {
                if (value == 1)
                    DoorWindowShowSetting = (byte)(DoorWindowShowSetting | 128);//1000 0000
                else
                    DoorWindowShowSetting = (byte)(DoorWindowShowSetting & 127);//0111 1111
            }
        }

        /// <summary>
        /// 门窗显示模式
        /// </summary>
        public int DoorWindowShowModelID
        {
            get
            {
                return DoorWindowShowSetting & 3; // 0000 0011

            }
            set
            {

                DoorWindowShowSetting = (byte)(DoorWindowShowSetting & 252 | value);//1111 1100  

            }
        }

        /// <summary>
        /// 面板传感器设定
        /// </summary>
        public void SetPanenlSensor(ControlObjectInfo controlObjectInfo, int sensorNum)
        {
            this.ControlObjects[sensorNum].ObjectKind = controlObjectInfo.ObjectKind;
            this.ControlObjects[sensorNum].DeviceNetworkID = controlObjectInfo.DeviceNetworkID;
            this.ControlObjects[sensorNum].DeviceID = controlObjectInfo.DeviceID;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userData"></param>
        public SpecialPanelOptionData(UserUdpData userData)
            : this(userData.Data)
        {
            length = 62;
        }

        public SpecialPanelOptionData(byte[] value)
            : base(value)
        {
            length = 62;

            PagePassword = ConvertTools.Bytes2ToUInt16(value[0], value[1]);//密码设置页
            PageContent = value[2];//页面显示   0---空调，1---门窗
            LeaveHome = value[3];//离家键
            GoHome = value[4];//回家键
            SoundAddress = value[5];//音响地址
            AirConditionAddress1 = value[6];//空调地址1
            AirConditionKind1 = value[7];//空调类型1
            AirConditionAddress2 = value[8];//空调地址2
            AirConditionKind2 = value[9];//空调类型2
            DoorWindowShowSetting = value[10];//门窗显示设置
            StandbyLight = value[11];//待机背光     0~10
            Luminance = value[12];//亮度
            PointLightLuminance = value[13];//指示灯亮度
            StandbyTime = value[14];//待机时间
            AUXAddress1 = value[15];//红外线地址
            AUXKind1 = value[16];//类型
            AUXControlKind1 = value[17];//红外控制对象类型
            AUXAddress2 = value[18];//红外线地址
            AUXKind2 = value[19];//类型
            AUXControlKind2 = value[20];//红外控制对象类型
            Volume = value[21];//提示音量

            Security = ConvertTools.Bytes2ToUInt16(value[22], value[23]);//安防
            byte b1 = value[22];
            byte b2 = value[23];
            int num = 0;
            for (int i = 1; i <= 128; i *= 2)
                SaftFlags[num++] = (int)(b1 & i) == i ? true : false;
            num = 8;
            for (int i = 1; i <= 64; i *= 2)
                SaftFlags[num++] = (int)(b2 & i) == i ? true : false;

            SetSecurityDelayTime = value[24];//布防延时
            AlarmDelayTime = value[25];//预警时间 
            //-----传感器列表-----
            for (int i = 0; i < 8; i++)
                ControlObjects[i] = new ControlObjectInfo(CommonTools.CopyBytes(value, i * 4 + 30, 4));//----26到30为保留字节---

        }

        /// <summary>
        /// 获取按键值
        /// </summary>
        /// <returns></returns>
        public override byte[] GetPanelOptionValue()
        {
            byte[] value = new byte[length];

            value[0] = ConvertTools.GetByteFromUInt16(PagePassword)[0];//页面密码
            value[1] = ConvertTools.GetByteFromUInt16(PagePassword)[1];//页面密码
            value[2] = PageContent;//页面显示   0---空调，1---门窗
            value[3] = LeaveHome;//离家键
            value[4] = GoHome;//回家键
            value[5] = SoundAddress;//音响地址
            value[6] = AirConditionAddress1;//空调地址1
            value[7] = AirConditionKind1;//空调类型1
            value[8] = AirConditionAddress2;//空调地址2
            value[9] = AirConditionKind2;//空调类型2
            value[10] = DoorWindowShowSetting;//门窗显示设置
            value[11] = StandbyLight;//待机背光     0~10
            value[12] = Luminance;//亮度
            value[13] = PointLightLuminance;//指示灯亮度
            value[14] = StandbyTime;//待机时间
            value[15] = AUXAddress1;//红外线地址
            value[16] = AUXKind1;//类型
            value[17] = AUXControlKind1;//红外控制对象类型
            value[18] = AUXAddress2;//红外线地址
            value[19] = AUXKind2;//类型
            value[20] = AUXControlKind2;//红外控制对象类型
            value[21] = Volume;//红外线地址

            value[22] = ConvertTools.GetByteFromUInt16(Security)[0];//安防
            value[23] = ConvertTools.GetByteFromUInt16(Security)[1];//安防
            value[24] = SetSecurityDelayTime;//布防延时
            value[25] = AlarmDelayTime;//预警时间 
            //-----传感器数组-----
            for (int i = 0; i < 8; i++)
                Buffer.BlockCopy(ControlObjects[i].GetValue(), 0, value, i * 4 + 30, 4);
            return value;
        }
    }
}
