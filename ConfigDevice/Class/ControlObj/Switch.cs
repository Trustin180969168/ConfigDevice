using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Switch : ControlObj
    {
        public Switch(DeviceData _deviceCtrl)
        {
            Name = "全部";
            deviceControled = _deviceCtrl;
        }

 
    }




}
