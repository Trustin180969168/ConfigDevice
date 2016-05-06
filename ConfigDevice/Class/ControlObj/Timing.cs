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

        public Timing(DeviceData _deviceCtrl)
        {
            Name = "时序";
            deviceControled = _deviceCtrl;
        }


    }





}
