using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class Button2 : Device
    {
        public Circuit Circuit;//回路对象
        public Button2(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public Button2(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }

        public Button2(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            Circuit = new Circuit(this, 2);
        }

    }


}
