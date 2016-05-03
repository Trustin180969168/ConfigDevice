﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Road4Relay : DeviceData
    {

        public Road4Relay(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public Road4Relay(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            ContrlObjs.Add("回路", new Circuit(this));
            ContrlObjs.Add("场景", new Scene(this));
            ContrlObjs.Add("时序", new Timing(this));
            ContrlObjs.Add("全部", new Switch(this));
        }

    }


}
