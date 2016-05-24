﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Road8FrontDimming : Device
    {
        private const int circuitCount =8;//回路数
        public Dictionary<int, string> ListCircuitIDAndName = new Dictionary<int, string>();//回路ID和名称对应表用于指令配置
        public int CircuitCount
        {
            get { return circuitCount; }
        } 

        public Road8FrontDimming(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public Road8FrontDimming(DeviceData data)
            : base(data)
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
            ContrlObjs.Add("回路", new Circuit(this,8));
            ContrlObjs.Add("场景", new Scene(this));
            ContrlObjs.Add("时序", new Timing(this));
            ContrlObjs.Add("全部", new Swit(this));

            //-----初始化列表---------
            for (int i = 1; i <= circuitCount; i++)
                ListCircuitIDAndName.Add(i, "");
        }

    }


}
