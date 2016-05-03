using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Road8FrontDimming : DeviceData
    {

        public Road8FrontDimming(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public Road8FrontDimming(DataRow dr)
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
            ContrlObjs.Add(new Motor(this));
        }

    }


}
