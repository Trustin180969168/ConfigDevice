using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Road4FrontDimming : Device
    {
        private const int circuitCount = 4;//回路数
        public Dictionary<int, string> ListCircuitIDAndName = new Dictionary<int, string>();//回路ID和名称对应表用于指令配置
        public int CircuitCount
        {
            get { return circuitCount; }
        } 
        public Road4FrontDimming(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }
                public Road4FrontDimming(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }
        public Road4FrontDimming(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, new Circuit(this,4));
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_SCENE_NAME, new Scene(this));
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_TIMING_NAME, new Timing(this));
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_SWIT_NAME, new Swit(this));

            //-----初始化列表---------
            for (int i = 1; i <= circuitCount; i++)
                ListCircuitIDAndName.Add(i, "");
        }

    }


}
