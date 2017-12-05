
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Road4Relay : Device
    {
        private const int circuitCount = 4;//回路数
        public Circuit circuit
        {
            get { return ContrlObjs[DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME] as Circuit; }
        }
        public int CircuitCount
        {
            get { return circuitCount; }
        }
        public Road4Relay(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }
        public Road4Relay(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }
        public Road4Relay(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        /// <summary>
        /// 是否完成回路的读取
        /// </summary>
        private bool finishReadRoads = false;
        public bool FinishReadRoads
        {
            get { return finishReadRoads; }
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
        }

        /// <summary>
        /// 申请读取回路名称
        /// </summary>
        public void ReadRoadTitle()
        {
            circuit.ReadRoadTitle();
        }


    }


}
