using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Road12Relay : Device
    {

        private const int circuitCount = 12;//回路数
        public int CircuitCount
        {
            get { return circuitCount; }
        } 
        public Road12Relay(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }
       
        public Road12Relay(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }

        public Road12Relay(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, new Circuit(this,8));
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_SCENE_NAME, new Scene(this));
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_TIMING_NAME, new Timing(this));
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_SWIT_NAME, new Swit(this));


        }

    }


}
