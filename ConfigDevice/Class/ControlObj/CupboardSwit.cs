using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    /// <summary>
    /// 专用开关
    /// </summary>
    public class CupboardSwit : ControlObj
    {
        //---与指令配合使用
        public const int COMMAND_RUN_FLAG = 1;//运行
        public const int COMMAND_STOP_FLAG = 0;//停止

        public const int ACTION_CLOSE_CUPBOARD = 1;//关柜
        public const int ACTION_OPEN_CUPBOARD = 2;//开柜

        public const string NAME_ACTION_CLOSE_CUPBOARD = "关柜";
        public const string NAME_ACTION_OPEN_CUPBOARD = "开柜";

        //---指令名称
        public const string NAME_CMD_SWIT_LOOP = "运行/停止";
        public const string NAME_CMD_SWIT_LOOP_OPEN = "运行";
        public const string NAME_CMD_SWIT_LOOP_CLOSE = "停止";
        public const string NAME_CMD_SWIT_LOOP_OPEN_CONDITION = "条件运行";
        public const string NAME_CMD_SWIT_LOOP_CLOSE_CONDITION = "条件停止";
        
        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系
        public CupboardSwit(Device _deviceCtrl)
            : base(_deviceCtrl)
        {
            Name = DeviceConfig.CONTROL_OBJECT_CUPBOARD; 
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
        /// 升降柜指令
        /// CMD_SW_SWIT_LOOP（开关回路）, 1运行，0停止
        /// CMD_SW_SWIT_LOOP_OPEN（开回路）,CMD_SW_SWIT_LOOP_CLOSE，此字节无效
        /// CMD_SW_SWIT_LOOP_OPEN_CONDITION（条件开）   1运行，0不作处理
        /// CMD_SW_SWIT_LOOP_CLOSE_CONDITION（条件关）  0停止，1不作处理 
        /// </summary>
        /// <param name="cmd">指令</param>
        /// <param name="commandActionKindId">与指令配合使用的类型值</param>
        /// <param name="actionCupboardKindId">执行动作</param>
        /// <param name="runTime">运行延时</param> 
        /// <returns></returns>
        private CommandData ControlAction(byte[] cmd, int commandActionKindId, int actionCupboardKindId,
            int value, int runDly)
        {
            CommandData cmdData = new CommandData("开关升降柜");
            cmdData.TargetId = deviceControled.ByteDeviceID;
            cmdData.TargetNet = deviceControled.ByteNetworkId;
            cmdData.TargetType = deviceControled.ByteKindID;

            cmdData.Cmd = cmd;
            cmdData.DataLen = 9;

            cmdData.Data[0] = (byte)commandActionKindId;//指令配合使用的类型值
            //cmdData.Data[1] //保留
            cmdData.Data[2] = (byte)actionCupboardKindId;//（关柜，开柜） 
            cmdData.Data[3] = (byte)value;//---类型值
            Buffer.BlockCopy(BitConverter.GetBytes(runDly), 0, cmdData.Data, 5, 2); 

            return cmdData;
        }

        /// <summary>
        /// 运行/停止->关柜
        /// </summary>
        /// <returns></returns>
        public CommandData ActionRunStopClose(int openDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP, COMMAND_RUN_FLAG, ACTION_CLOSE_CUPBOARD, 0, openDly);
        }

        /// <summary>
        /// 运行/停止->开柜
        /// </summary>
        /// <returns></returns>
        public CommandData ActionRunStopOpen(int floors, int openDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP, COMMAND_RUN_FLAG, ACTION_OPEN_CUPBOARD, floors, openDly);
        }

        /// <summary>
        /// 运行->关柜
        /// </summary>
        /// <returns></returns>
        public CommandData ActionOpenCmdCloseCupboard(int openDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_OPEN, COMMAND_RUN_FLAG, ACTION_CLOSE_CUPBOARD, 0, openDly);
        }

        /// <summary>
        /// 运行->开柜
        /// </summary>
        /// <returns></returns>
        public CommandData ActionOpenCmdOpenCupboard(int floors, int openDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_OPEN, COMMAND_RUN_FLAG, ACTION_OPEN_CUPBOARD, floors, openDly);
        }


        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public CommandData ActionStopCmdOpenCupboard()
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE, COMMAND_STOP_FLAG, ACTION_CLOSE_CUPBOARD, 0, 0);
        }

        /// <summary>
        /// 条件运行->关柜
        /// </summary>
        /// <returns></returns>
        public CommandData ActionConditionOpenCmdCloseCupboard(int openDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_OPEN_CONDITION, COMMAND_RUN_FLAG, ACTION_CLOSE_CUPBOARD, 0, openDly);
        }

        /// <summary>
        /// 条件运行->开柜
        /// </summary>
        /// <returns></returns>
        public CommandData ActionConditionOpenCmdOpenCupboard(int floors, int openDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_OPEN_CONDITION, COMMAND_RUN_FLAG, ACTION_OPEN_CUPBOARD, floors, openDly);
        }

        /// <summary>
        /// 条件停止
        /// </summary>
        /// <returns></returns>
        public CommandData ActionConditionCmdStopCupboard()
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE_CONDITION, COMMAND_STOP_FLAG, ACTION_CLOSE_CUPBOARD, 0, 0);
        }

        /// <summary>
        /// 获取执行命令数据
        /// </summary>
        /// <param name="cmd">指令</param>
        /// <param name="commandActionKindId">与指令配合使用的类型值</param>
        /// <param name="actionCupboardKindId">执行动作</param>
        /// <param name="runTime">运行延时</param> 
        /// <returns>CommandData</returns>
        public CommandData GetCommandData(byte[] cmd, int commandActionKindId, int actionCupboardKindId, int value, int runDly)
        {
            return ControlAction(cmd, commandActionKindId, actionCupboardKindId, value, runDly);
        }




    }




}
