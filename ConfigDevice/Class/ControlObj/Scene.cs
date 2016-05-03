using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Scene : ControlObj
    {
        public Scene(DeviceData _deviceCtrl)
        {
            Name = "场景";
            deviceControled = _deviceCtrl;
        }



    }




}
