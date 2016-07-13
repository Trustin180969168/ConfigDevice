using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class KeyOptionData
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

        public byte OpenClosePassword = 0;
        public byte Content = 0;//内容,
        public byte PageContent = 0;//页面显示   0---空调，1---门窗
        public byte LeaveHome = 0;//离家键
        public byte GoHome = 0;//回家键
        public byte SoundAddress = 0;//音响地址
        public byte AirConditionAddress1 = 0;//空调地址1
        public byte AirConditionKind1 = 0;//空调类型1
        public byte AirConditionAddress2 = 0;//空调地址2
        public byte AirConditionKind2 = 0;//空调类型2
        public byte DoorWindowShowSetting = 0;//门窗显示设置
        public byte StandbyLight = 0;//待机背光     0~10
        public byte Luminance = 0;//亮度
        public byte PointLightLuminance = 0;//指示灯亮度
        public byte StandbyTime = 0;//待机时间
        public byte AUXAddress1 = 0;//红外线地址
        public byte AUXKind1 = 0;//类型
        public byte AUXControlKind1 = 0;//红外控制对象类型
        public byte AUXAddress2 = 0;//红外线地址
        public byte AUXKind2 = 0;//类型
        public byte AUXControlKind2 = 0;//红外控制对象类型
        public byte Volume = 0;//红外线地址
        public UInt16 Security = 0;//安防
        public byte SetSecurityDelayTime = 0;//布防延时
        public byte AlarmTime = 0;//预警时间


        public KeyOptionData(UserUdpData userData)
        {
            byte[] value = userData.Data;
            OpenClosePassword = value[0];//1.开密码;2.关
            Content = value[1];//内容,
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
            Volume = value[21];//红外线地址
            Security = ConvertTools.Bytes2ToUInt16(value[22], value[23]);//安防
            SetSecurityDelayTime = value[24];//布防延时
            AlarmTime = value[25];//预警时间 
        }

        /// <summary>
        /// 获取按键值
        /// </summary>
        /// <returns></returns>
        public byte[] GetKeyOptionValue()
        {
            byte[] value = new byte[26];

            value[0] = OpenClosePassword;//1.开密码;2.关
            value[1] = Content;//内容,
            value[2] = PageContent;//页面显示   0---空调，1---门窗
            value[3] = LeaveHome;//离家键
            value[4] = GoHome;//回家键
            value[5] = SoundAddress;//音响地址
            value[6] = AirConditionAddress1;//空调地址1
            value[7] = AirConditionKind1;//空调类型1
            value[8] = AirConditionAddress2;//空调地址2
            value[10] = AirConditionKind2;//空调类型2
            value[11] = DoorWindowShowSetting;//门窗显示设置
            value[12] = StandbyLight;//待机背光     0~10
            value[13] = Luminance;//亮度
            value[14] = PointLightLuminance;//指示灯亮度
            value[15] = StandbyTime;//待机时间
            value[16] = AUXAddress1;//红外线地址
            value[17] = AUXKind1;//类型
            value[18] = AUXControlKind1;//红外控制对象类型
            value[19] = AUXAddress2;//红外线地址
            value[20] = AUXKind2;//类型
            value[21] = AUXControlKind2;//红外控制对象类型
            value[22] = Volume;//红外线地址

            value[23] = ConvertTools.GetByteFromUInt16(Security)[0];//安防
            value[24] = ConvertTools.GetByteFromUInt16(Security)[1];//安防
            value[25] = SetSecurityDelayTime;//布防延时
            value[26] = AlarmTime;//预警时间 

            return value;
        }
    }
}
