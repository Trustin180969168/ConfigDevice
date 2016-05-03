using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Timing : ControlObj
    {
        public Timing(DeviceData _deviceCtrl)
        {
            Name = "时序";
            deviceControled = _deviceCtrl;
        }


    }





}
