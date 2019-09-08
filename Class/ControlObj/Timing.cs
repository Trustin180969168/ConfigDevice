using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Timing : ControlObj
    {
        public const string NAME_CMD_SW_SWIT_TIMING = "开关时序";
        public const string NAME_CMD_SW_SWIT_TIMING_OPEN = "开时序";
        public const string NAME_CMD_SW_SWIT_TIMING_CLOSE = "关时序";

        public Timing(Device _deviceCtrl)
            : base(_deviceCtrl)
        {
            Name = DeviceConfig.CONTROL_OBJECT_TIMING_NAME; 
        }


    }





}
