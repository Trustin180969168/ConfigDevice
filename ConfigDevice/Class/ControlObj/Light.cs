using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    /// <summary>
    /// 指示灯
    /// </summary>
    public class Light : ControlObj
    {
        public const int LEL_LEDACT_OFF = 0;        //【熄灭(全灭)】
        public const int LEL_LEDACT_ON = 1;        //【点亮(红灯)】
        public const int LEL_LEDACT_GLINT = 2;        //【闪烁(红灯)】
        public const int LEL_LEDACT_NOT = 3;        //【不动作】
        public const int LEL_LEDACT_TOTAL = 4;        //【总数】
        public const int LEL_LEDACT_DEFAULT = LEL_LEDACT_ON;

        public const string STATE_LEDACT_OFF = "熄灭";        //【熄灭(全灭)】
        public const string STATE_LEDACT_ON = "点亮";        //【点亮(红灯)】
        public const string STATE_LEDACT_GLINT = "闪烁";        //【闪烁(红灯)】
        public const string STATE_LEDACT_NOT = "不动作";        //【不动作】

        public byte LedAct = 0;               //指示灯指示动作：熄灭、闪烁等   (如:LEL_LEDACT_OFF)
        public ushort LedTim = 0;               //指示灯指示时间：单位秒         (如:10->10秒，0->无限)

        public Light(Device _deviceCtrl)
        {
            Name = "指示灯";
            deviceControled = _deviceCtrl;
        }
    }



}
