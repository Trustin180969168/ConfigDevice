using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class BaseDevice : Device
    {

        public BaseDevice(UserUdpData userUdpData)
            : base(userUdpData)
        {

        }

        public BaseDevice(DeviceData data)
            : base(data)
        {

        }

        public BaseDevice(DataRow dr)
            : base(dr)
        {

        }




    }
}
