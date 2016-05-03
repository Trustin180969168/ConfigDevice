using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Background : ControlObj
    {
        public Background(DeviceData _deviceCtrl)
        {
            Name = "背景";
            deviceControled = _deviceCtrl;
        }

    }




}
