using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Road6FrontDimming : Device
    {
        private const int circuitCount = 6;//回路数
        public Circuit circuit
        {
            get { return ContrlObjs[DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME] as Circuit; }
        }
        public int CircuitCount
        {
            get { return circuitCount; }
        } 
        public Road6FrontDimming(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }
                public Road6FrontDimming(DeviceData data)
            : base(data)
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
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, new Circuit(this,6));
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_SCENE_NAME, new Scene(this));
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_TIMING_NAME, new Timing(this));
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_SWIT_NAME, new Swit(this));
 
        }

        /// <summary>
        /// 是否完成回路的读取
        /// </summary>
        private bool finishReadRoads = false;
        public bool FinishReadRoads
        {
            get { return finishReadRoads; }
        }
 
    }


}
