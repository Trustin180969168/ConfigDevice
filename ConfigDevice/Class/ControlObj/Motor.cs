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
        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系
        public Motor(Device _deviceCtrl)
        {
            Name = "电机";
            deviceControled = _deviceCtrl;

            if (NameAndCommand.Count == 0)
            {
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP, DeviceConfig.CMD_SW_SWIT_LOOP);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_OPEN, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_CLOSE, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_OPEN_CONDITION, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN_CONDITION);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_CLOSE_CONDITION, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE_CONDITION);
            }

        }



        /// <summary>
        /// 开关电机
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="ucFuncvol">开或关,默认0</param>
        /// 此字节与指令配合使用
        /// CM_SW_SWIT_LOOP, 1时表示开，0时表示关
        /// CMD_SW_SWIT_LOOP_OPEN,CMD_SW_SWIT_LOOP_CLOSE，此字节无效
        /// CMD_SW_SWIT_LOOP_OPEN_CONDITION   1时执开，0时不作处理
        /// CMD_SW_SWIT_LOOP_CLOSE_CONDITION  0时执关，1时不作处理
        /// <param name="MotorAction">动作</param>
        /// <param name="percent">百分比</param>
        /// <param name="usRunTime">运动时间单位S</param>
        /// <param name="usOpenDly">开延迟时间单位S</param>
        /// <param name="usCloseDly">关延时时间单位S</param>
        /// <returns></returns>
        private CommandData ControlAction(byte[] cmd, int percent, int actionIndex, 
            int usRunTime, int usOpenDly, int usCloseDly)
        {
            CommandData cmdData = new CommandData("开关电机");
            cmdData.TargetId = deviceControled.ByteDeviceID;
            cmdData.TargetNet = deviceControled.ByteNetworkId;
            cmdData.TargetType = deviceControled.ByteKindID;

            cmdData.Cmd = cmd;
            cmdData.DataLen = 9;

            cmdData.Data[0] = 0;
            cmdData.Data[1] = (byte)percent;
            cmdData.Data[2] = (byte)actionIndex;
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
        public CommandData SwitLoop(int ucStepVol,int actionIndex,  int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP,  actionIndex, ucStepVol, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 开电机
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopOpen(int ucStepVol,int actionIndex,  int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_OPEN,  actionIndex, ucStepVol, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 关电机
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopClose(int ucStepVol,int actionIndex,  int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE, actionIndex, ucStepVol, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 开执行
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopOpenCondition(int ucStepVol,int actionIndex,  int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_OPEN_CONDITION, actionIndex, ucStepVol, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 关执行
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopCloseCondition(int ucStepVol, int actionIndex, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE_CONDITION,  actionIndex, ucStepVol, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 获取执行命令数据
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="actionIndex">动作ID</param>
        /// <param name="ucStepVol">程度</param>
        /// <param name="usRunTime">运行时间</param>
        /// <param name="usOpenDly">开始延时</param>
        /// <param name="usCloseDly">关闭延时</param>
        /// <returns>CommandData</returns>
        public CommandData GetCommandData(byte[] command, int percent, int actionIndex, int usRunTime, int usOpenDly, int usCloseDly)
        {

            return ControlAction(command, percent, actionIndex, usRunTime, usOpenDly, usCloseDly);

        }

    }




}
