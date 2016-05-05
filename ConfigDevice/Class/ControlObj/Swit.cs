using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Swit : ControlObj
    {
        public const string NAME_CMD_SW_SWIT_ALL = "开关全部";
         public const string CMD_SW_SWIT_ALL_OPEN = "开全部";
         public const string CMD_SW_SWIT_ALL_CLOSE = "关全部";

        public Swit(DeviceData _deviceCtrl)
        {
            Name = "全部";
            deviceControled = _deviceCtrl;
        }

 
    }




}
