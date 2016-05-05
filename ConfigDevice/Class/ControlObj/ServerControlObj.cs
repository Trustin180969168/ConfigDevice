using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class ServerControlObj : ControlObj
    {
        public const string NAME_CMD_SEND_WEIXIN = "发微信";

        public ServerControlObj(DeviceData _deviceCtrl)
        {
            Name = "服务器";
            deviceControled = _deviceCtrl;
        }




    }




}
