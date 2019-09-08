﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class Amplifier : Device
    {

        public Amplifier(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public Amplifier(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }

        public Amplifier(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_BGM_NAME,new Background(this));
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_MESSAGE_NAME,new Messages(this));
        }

    }


}