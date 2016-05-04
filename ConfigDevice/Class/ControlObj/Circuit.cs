using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class Circuit : ControlObj
    {

        public const string NAME_CMD_SWITLOOP = "开关回路";
        public const string NAME_CMD_SWITLOOP_OPEN = "开回路";
        public const string NAME_CMD_SWITLOOP_CLOSE = "关回路";


        public Circuit(DeviceData _deviceCtrl)
        {
            Name = "回路";
            deviceControled = _deviceCtrl;
        }

        /// <summary>
        /// 开关回路
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="ucFuncvol">开或关,1表开，0表关。当指令是CMD_SW_SWIT_LOOP_OPEN/CMD_SW_SWIT_LOOP_CLOSE，此字节无效</param>
        /// <param name="ucStepVol">亮度</param>
        /// <param name="ucLoopNum">第几个回路,0表示第一个</param>
        /// <param name="usRunTime">运动时间单位S</param>
        /// <param name="usOpenDly">开延迟时间单位S</param>
        /// <param name="usCloseDly">关延时时间单位S</param>
        /// <returns></returns>
        private CommandData ControlAction(byte[] cmd,int ucFuncvol,int ucStepVol,int ucLoopNum,
            int usRunTime,int usOpenDly,int usCloseDly )
        {
            CommandData cmdData = new CommandData("开关回路");
            cmdData.TargetId = deviceControled.ByteDeviceID;
            cmdData.TargetNet = deviceControled.ByteNetworkId;
            cmdData.TargetType = deviceControled.ByteKindID;

            cmdData.Cmd = cmd;
            cmdData.Len = 9;

            cmdData.Data[0] = BitConverter.GetBytes(ucFuncvol)[0];
            cmdData.Data[1] = BitConverter.GetBytes(ucStepVol)[0];
            cmdData.Data[2] = BitConverter.GetBytes(ucLoopNum)[0];
            Buffer.BlockCopy(BitConverter.GetBytes(usRunTime), 0, cmdData.Data, 3, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(usOpenDly), 0, cmdData.Data, 5, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(usCloseDly), 0, cmdData.Data, 7, 2);

            return cmdData;
        }

        /// <summary>
        /// 开关回路
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoop(int ucStepVol, int ucLoopNum,
            int usRunTime, int usOpenDly, int usCloseDly)
        {
           return ControlAction(DeviceConfig.CMD_PUBLIC_SWIT, 1,  ucStepVol, ucLoopNum, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 开回路
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopOpen(int ucStepVol, int ucLoopNum,
            int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_PUBLIC_SWIT_OPEN,1, ucStepVol, ucLoopNum, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 关回路
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopClose(int ucStepVol, int ucLoopNum,
            int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_PUBLIC_SWIT_CLOSE, 0, ucStepVol, ucLoopNum, usRunTime, usOpenDly, usCloseDly);
        }

    }


}
