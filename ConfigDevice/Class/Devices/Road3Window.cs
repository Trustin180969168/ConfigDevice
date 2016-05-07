using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Road3Window : Device
    {

        public Road3Window(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public Road3Window(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        public Road3Window(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            ContrlObjs.Add("电机",new Motor(this));
        }

    }


}
