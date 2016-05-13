using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class Circuit : ControlObj
    {

        public const string NAME_CMD_SWIT_LOOP = "开关回路";
        public const string NAME_CMD_SWIT_LOOP_OPEN = "开回路";
        public const string NAME_CMD_SWIT_LOOP_CLOSE = "关回路";

        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系

        public Circuit(Device _deviceCtrl)
        {
            Name = "回路";
            deviceControled = _deviceCtrl;

            if (NameAndCommand.Count == 0)
            {
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP, DeviceConfig.CMD_SW_SWIT_LOOP);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_OPEN, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_CLOSE, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE);
            }
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
        private CommandData ControlAction(byte[] cmd, int percent,int ucLoopNum,
            int usRunTime,int usOpenDly,int usCloseDly )
        {
            CommandData cmdData = new CommandData("开关回路");
            cmdData.TargetId = deviceControled.ByteDeviceID;
            cmdData.TargetNet = deviceControled.ByteNetworkId;
            cmdData.TargetType = deviceControled.ByteKindID;

            cmdData.Cmd = cmd;
            cmdData.DataLen = 9;

            cmdData.Data[0] = 0;
            cmdData.Data[1] = (byte)percent;
            cmdData.Data[2] = (byte)ucLoopNum;
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
           return ControlAction(DeviceConfig.CMD_PUBLIC_SWIT,  ucStepVol, ucLoopNum, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 开回路
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopOpen(int ucStepVol, int ucLoopNum,
            int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_PUBLIC_SWIT_OPEN,ucStepVol, ucLoopNum, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 关回路
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopClose(int ucStepVol, int ucLoopNum,
            int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_PUBLIC_SWIT_CLOSE,  ucStepVol, ucLoopNum, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 获取命令数据
        /// </summary>
        /// <param name="command">byte[]</param>
        /// <param name="percent">亮度</param>
        /// <param name="actionIndex">回路</param>
        /// <param name="usRunTime">运行时间</param>
        /// <param name="usOpenDly">开延迟</param>
        /// <param name="usCloseDly">关延时</param>
        /// <returns>CommandData</returns>
        public CommandData GetCommandData(byte[] command, int percent, int ucLoopNum, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(command, percent, ucLoopNum, usRunTime, usOpenDly, usCloseDly);
        }

    }


}
