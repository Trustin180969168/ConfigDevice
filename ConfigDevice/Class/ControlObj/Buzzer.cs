
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    /// <summary>
    /// 蜂鸣器
    /// </summary>
    public class Buzzer : ControlObj
    {
        public const int LEL_BUZACT_CLOSE = 0;        //【关闭蜂鸣器】
        public const int LEL_BUZACT_OPEN = 1;        //【打开蜂鸣器】
        public const int LEL_BUZACT_NONE = 2;        //【不动作】
        public const int LEL_BUZACT_TOTAL = 3;        //【总数】
        public const int LEL_BUZACT_DEFAULT = LEL_BUZACT_OPEN;

        public const string STATE_BUZACT_CLOSE = "关闭";        //【关闭蜂鸣器】
        public const string STATE_BUZACT_OPEN = "打开";        //【打开蜂鸣器】
        public const string STATE_BUZACT_NONE = "不动作";        //【不动作】

        public byte BuzAct = 0;               //蜂鸣器动作类型：打开、关闭等   (如:LEL_BUZACT_CLOSE)
        public ushort BuzTim = 0;               //蜂鸣器动作时间：单位秒         (如:10->10秒，0->无限) 

        public Buzzer(Device _deviceCtrl):base(_deviceCtrl)
        {
            Name = "蜂鸣器"; 
        }

    }


}
