using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Server : ControlObj
    {
        public Server(DeviceData _deviceCtrl)
        {
            Name = "服务器";
            deviceControled = _deviceCtrl;
        }




    }




}
