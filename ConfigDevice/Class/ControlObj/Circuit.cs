using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class Circuit : ControlObj
    {   
        public Circuit(DeviceData _deviceCtrl)
        {
            Name = "回路";
            deviceControled = _deviceCtrl;
        }

        public byte ucFuncVol;//开或关
        public byte ucStepVol;//亮度
        public byte ucLoopNum;//第几个回路,0表示第一个
        public byte usRunTime;//运动时间单位S
        public byte usOpenDly;//开延迟时间单位S
        public byte usCloseDly;//关延时时间单位S

        /// <summary>
        /// 开关回路
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoop()
        {
            CommandData cmdData = new CommandData("开关回路");
            cmdData.TargetId = deviceControled.ByteDeviceID;
            cmdData.TargetNet = deviceControled.ByteNetworkId;
            cmdData.TargetType = deviceControled.ByteKindID;

  //          cmdData.Cmd = DeviceConfig.cmd_plublic_
            return cmdData;
        }





    }


}
