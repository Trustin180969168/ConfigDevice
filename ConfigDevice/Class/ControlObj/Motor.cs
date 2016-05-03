using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Motor : ControlObj
    {
        public Motor(DeviceData _deviceCtrl)
        {
            Name = "电机";
            deviceControled = _deviceCtrl;
        }

    }




}
