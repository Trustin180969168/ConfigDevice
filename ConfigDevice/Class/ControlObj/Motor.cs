using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Motor : ControlObj
    {

        public const string NAME_CMD_SWIT_LOOP = "开关电机";
        public const string NAME_CMD_SWIT_LOOP_OPEN = "开电机";
        public const string NAME_CMD_SWIT_LOOP_CLOSE = "关电机";
        public const string NAME_CMD_SWIT_LOOP_OPEN_CONDITION = "开执行";
        public const string NAME_CMD_SWIT_LOOP_CLOSE_CONDITION = "关执行";

        public const string NAME_ACTION_ROAD_FRONT_1 = "1路正转";
        public const string NAME_ACTION_ROAD_BACK_1 = "1路反转";
        public const string NAME_ACTION_ROAD_FRONT_2 = "2路正转";
        public const string NAME_ACTION_ROAD_BACK_2 = "2路反转";
        public const string NAME_ACTION_ROAD_FRONT_3 = "3路正转";
        public const string NAME_ACTION_ROAD_BACK_3 = "3路反转";

        public Motor(Device _deviceCtrl)
        {
            Name = "电机";
            deviceControled = _deviceCtrl;
        }


        /// <summary>
        /// 开关电机
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="ucFuncvol">开或关</param>
        /// 此字节与指令配合使用
        /// CM_SW_SWIT_LOOP, 1时表示开，0时表示关
        /// CMD_SW_SWIT_LOOP_OPEN,CMD_SW_SWIT_LOOP_CLOSE，此字节无效
        /// CMD_SW_SWIT_LOOP_OPEN_CONDITION   1时执开，0时不作处理
        /// CMD_SW_SWIT_LOOP_CLOSE_CONDITION  0时执关，1时不作处理
        /// <param name="MotorAction">动作</param>
        /// <param name="ucStepVol">程度</param>
        /// <param name="usRunTime">运动时间单位S</param>
        /// <param name="usOpenDly">开延迟时间单位S</param>
        /// <param name="usCloseDly">关延时时间单位S</param>
        /// <returns></returns>
        private CommandData ControlAction(byte[] cmd, int ucFuncvol, int ucStepVol, MotorAction action,
            int usRunTime, int usOpenDly, int usCloseDly)
        {
            CommandData cmdData = new CommandData("开关电机");
            cmdData.TargetId = deviceControled.ByteDeviceID;
            cmdData.TargetNet = deviceControled.ByteNetworkId;
            cmdData.TargetType = deviceControled.ByteKindID;

            cmdData.Cmd = cmd;
            cmdData.Len = 9;

            cmdData.Data[0] = BitConverter.GetBytes(ucFuncvol)[0];
            cmdData.Data[1] = BitConverter.GetBytes(ucStepVol)[0];
            cmdData.Data[2] = BitConverter.GetBytes((int)action)[0];
            Buffer.BlockCopy(BitConverter.GetBytes(usRunTime), 0, cmdData.Data, 3, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(usOpenDly), 0, cmdData.Data, 5, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(usCloseDly), 0, cmdData.Data, 7, 2);

            return cmdData;
        }


        ///CM_SW_SWIT_LOOP, 1时表示开，0时表示关
        ///CMD_SW_SWIT_LOOP_OPEN,CMD_SW_SWIT_LOOP_CLOSE，此字节无效
        ///CMD_SW_SWIT_LOOP_OPEN_CONDITION   1时执开，0时不作处理
        ///CMD_SW_SWIT_LOOP_CLOSE_CONDITION  0时执关，1时不作处理
        /// <summary>
        /// 开关电机
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoop(MotorAction action, int ucStepVol, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP, 1, ucStepVol, action, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 开电机
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopOpen(MotorAction action, int ucStepVol, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_OPEN, 1, ucStepVol, action, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 关电机
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopClose(MotorAction action, int ucStepVol, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE, 0, ucStepVol, action, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 开执行
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopOpenCondition(MotorAction action, int ucStepVol, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_OPEN_CONDITION, 1, ucStepVol, action, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 关执行
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopCloseCondition(MotorAction action, int ucStepVol, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE_CONDITION, 0, ucStepVol, action, usRunTime, usOpenDly, usCloseDly);
        }

    }




}
