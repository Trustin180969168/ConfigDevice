using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class BaseDevice : DeviceData
    {

        public BaseDevice(UserUdpData userUdpData)
            : base(userUdpData)
        {

        }

        public BaseDevice(DataRow dr)
            : base(dr)
        {

        }




    }
}
