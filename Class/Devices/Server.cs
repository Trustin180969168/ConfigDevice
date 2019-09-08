using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Server : Device
    {

        public Server(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public Server(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        public Server(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            ContrlObjs.Add("服务器",new ServerControlObj(this));
        }

    }


}
