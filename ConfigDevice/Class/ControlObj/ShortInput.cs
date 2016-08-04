
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{



    /// <summary>
    /// 短路输入
    /// </summary>
    public class ShortInput : ControlObj
    {
        public byte ucScOutAct;             //短路输出1：低电平、高电平      (如: SC_OUTACT_LOW)
        public UInt16 usScOutDly = 0;             //动作延时 ：单位-秒             (如:10->10秒，0->无延时)
        public UInt16 usScOutTim = 0;             //动作时间 ：单位-秒             (如:10->10秒，0->无限)

        public ShortInput(Device _deviceCtrl)
            : base(_deviceCtrl)
        {
            Name = "短路输入";
        }

  

        /// <summary>
        /// 获取逻辑值
        /// </summary>
        /// <returns></returns>
        public byte[] GetValue()
        {
            byte[] value = new byte[5];
            value[0] = ucScOutAct;
            value[1] = ConvertTools.GetByteFromUInt16(usScOutDly)[0];
            value[2] = ConvertTools.GetByteFromUInt16(usScOutDly)[1];
            value[3] = ConvertTools.GetByteFromUInt16(usScOutTim)[0];
            value[4] = ConvertTools.GetByteFromUInt16(usScOutTim)[1];

            return value;
        }

    }




}
