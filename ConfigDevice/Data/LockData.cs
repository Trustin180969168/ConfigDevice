using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    /// <summary>
    /// 锁的功放配置
    /// </summary>
    public class LockAmplifierConfigData
    {
        public byte Num = 0;//第几个编号,(因固化为0，接收方无需关心)
        public byte DeviceID;//设备ID
        public byte Volume;//音量
        public byte[] Retain = new byte[12];//保留

        public LockAmplifierConfigData(byte[] data)
        {
        }

        public LockAmplifierConfigData(int num, int deviceID, int volume)
        {
            Num = (byte)num;
            DeviceID = (byte)deviceID;
            Volume = (byte)volume;
        }

        public byte[] GetValue()
        {
            byte[] value = new byte[16];
            value[0] = (byte)Num;
            value[1] = (byte)DeviceID;
            value[2] = (byte)Volume;
            Buffer.BlockCopy(Retain, 0, value, 3, 12);

            return value;
        }
    }

    /// <summary>
    /// 锁的配置信息
    /// </summary>
    public class LockConfigData
    {
        public byte Num = 0;//第几个行 (从0开始计数) (0第1行)(第2行开始为主人提示音)
        public byte[] Retain1 = new byte[2];//保留
        public byte MusicNum;//提示音曲目（0无提示音）
        //例如：08:00-22:00
        //如果起始时间与结束时间相等，
        //则表示没有时限限制
        public byte StartHour;//时限起始时间-时
        public byte StartMinute;//时限起始时间-分
        public byte EndHour;//时限结束时间-时
        public byte EndMinute;//时限结束时间-分
        public byte[] Retain2 = new byte[8];//保留

        public string StartTimeStr
        {
            get {

                int hour = (int)StartHour;
                string hourStr = "00";
                if (hour >= 10)
                    hourStr = hour.ToString();
                else
                    hourStr = "0" + hour;


                int minute = (int)StartMinute;
                string minuteStr = "00";
                if (minute >= 10)
                    minuteStr = minute.ToString();
                else
                    minuteStr = "0" + minute;
                return hourStr + ":" + minuteStr;
            }
        }


        public string EndTimeStr
        {
            get
            { 
                int hour = (int)EndHour;
                string hourStr = "00";
                if (hour >= 10)
                    hourStr = hour.ToString();
                else
                    hourStr = "0" + hour; 

                int minute = (int)EndMinute;
                string minuteStr = "00";
                if (minute >= 10)
                    minuteStr = minute.ToString();
                else
                    minuteStr = "0" + minute;
                return hourStr + ":" + minuteStr;
            }
        }

        public  LockConfigData(byte[] value)
        {
            Num = value[0];
            Retain1 = new byte[] { value[1], value[2] };
            MusicNum = value[3];
            StartHour = value[4];
            StartMinute = value[5];
            EndHour = value[6];
            EndMinute = value[7];
            Retain2 = CommonTools.CopyBytes(value, 8, 8);            
        }

        public byte[] GetValue()
        {
            byte[] value = new byte[16];
            value[0] = (byte)Num;
            value[1] = Retain1[0];
            value[2] = Retain1[1];
            value[3] = (byte)MusicNum;
            value[4] = StartHour;
            value[5] = StartMinute;
            value[6] = EndHour;
            value[7] = EndMinute;  
            Buffer.BlockCopy(Retain2, 0, value, 8, 8);

            return value;
        }
    }
}
