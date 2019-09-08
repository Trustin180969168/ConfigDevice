using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class SystemInteraction : Device
    {

        public SystemInteraction(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public SystemInteraction(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        public SystemInteraction(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            ContrlObjs.Add("系统联动号", new OuterInteraction(this));
            ContrlObjs.Add("内部联动号", new InnerInteraction(this));
        }

    }


}
