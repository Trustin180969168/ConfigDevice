using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Road6FrontDimming : Device
    {
        private const int circuitCount = 6;//回路数
        public event CallbackUIAction OnCallbackRoad_Action;   //----回调UI----
        public Circuit circuit
        {
            get { return ContrlObjs["回路"] as Circuit; }
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
            ContrlObjs.Add("回路", new Circuit(this,6));
            ContrlObjs.Add("场景", new Scene(this));
            ContrlObjs.Add("时序", new Timing(this));
            ContrlObjs.Add("全部", new Swit(this));

            circuit.OnCallbackUI_Action += this.OnCallbackRoad_Action;
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
