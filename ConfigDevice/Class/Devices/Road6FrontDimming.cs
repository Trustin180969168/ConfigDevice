using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Road6FrontDimming : DeviceData
    {

        public Road6FrontDimming(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public Road6FrontDimming(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            ContrlObjs.Add(new Circuit(this));
            ContrlObjs.Add(new Scene(this));
            ContrlObjs.Add(new Timing(this));
            ContrlObjs.Add(new Switch(this));
        }

    }


}
