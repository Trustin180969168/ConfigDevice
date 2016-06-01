using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class FlammableGasProbe:Device
    {
                public FlammableGasProbe(UserUdpData userUdpData)
            : base(userUdpData)
        { 
            initCallback();
        }

        public FlammableGasProbe(DeviceData data)
            : base(data)
        { 
            initCallback();
        }

        public FlammableGasProbe(DataRow dr)
            : base(dr)
        { 
            initCallback();
        }

        /// <summary>
        /// 注册RJ45回调
        /// </summary>
        private void initCallback()
        {
 

        }
    }

}
