using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Scene : ControlObj
    {

        public const string NAME_CMD_SW_SWIT_SCENE = "开关场景";
        public const string NAME_CMD_SW_SWIT_SCENE_OPEN = "开场景";
        public const string NAME_CMD_SW_SWIT_SCENE_CLOSE = "关场景";

        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系
        public Scene(Device _deviceCtrl)
            : base(_deviceCtrl)
        {
            Name = DeviceConfig.CONTROL_OBJECT_SWIT_NAME; 
            if (NameAndCommand.Count == 0)
            {
                NameAndCommand.Add(NAME_CMD_SW_SWIT_SCENE, DeviceConfig.CMD_SW_SWIT_SCENE);
                NameAndCommand.Add(NAME_CMD_SW_SWIT_SCENE_OPEN, DeviceConfig.CMD_SW_SWIT_SCENE_OPEN);
                NameAndCommand.Add(NAME_CMD_SW_SWIT_SCENE_CLOSE, DeviceConfig.CMD_SW_SWIT_SCENE_CLOSE);
            }
        }

        /// <summary>
        /// 获取执行命令数据
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="flag">操作标记</param>
        /// <param name="groupNum">第几组</param>
        /// <param name="num">第几个场景</param>
        /// <param name="usRunTime">运行时间</param>
        /// <returns>CommandData</returns>
        public CommandData ControlAction(byte[] command, byte flag, int groupNum, int num, int usRunTime)
        {
            CommandData cmdData = new CommandData("开关场景");
            cmdData.TargetId = deviceControled.ByteDeviceID;
            cmdData.TargetNet = deviceControled.ByteNetworkId;
            cmdData.TargetType = deviceControled.ByteKindID;

            cmdData.Cmd = command;
            cmdData.DataLen = 6;

            cmdData.Data[0] = (byte)flag;//开或关，1表示开，0表示关,当指令是CMD_SW_SWIT_ALL_OPEN/CMD_SW_SWIT_ALL_CLOSE，此字节无效
            cmdData.Data[1] = 0;//保留
            cmdData.Data[2] = (byte)groupNum;//第几组
            cmdData.Data[3] = (byte)num;//第几个场景
            Buffer.BlockCopy(BitConverter.GetBytes(usRunTime), 0, cmdData.Data, 4, 2);

            return cmdData;
        }

        /// <summary>
        /// 开关场景
        /// </summary>
        /// <returns></returns>
        public CommandData SwitScene(int groupNum, int num, int usRunTime)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_SCENE, 0, groupNum, num, usRunTime);
        }

        /// <summary>
        /// 开场景
        /// </summary>
        /// <returns></returns>
        public CommandData OpenScene(int groupNum, int num, int usRunTime)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_SCENE_OPEN, 0, groupNum, num, usRunTime);
        }

        /// <summary>
        /// 关场景
        /// </summary>
        /// <returns></returns>
        public CommandData CloseScene(int groupNum, int num, int usRunTime)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_SCENE_CLOSE, 0, groupNum, num, usRunTime);
        }


        /// <summary>
        /// 获取执行命令数据
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="flag">操作标记</param>
        /// <param name="groupNum">第几组</param>
        /// <param name="num">第几个场景</param>
        /// <param name="usRunTime">运行时间</param>
        /// <returns>CommandData</returns> 
        public CommandData GetCommandData(byte[] command,  int groupNum, int num, int usRunTime)   {
            return ControlAction(command, 0, groupNum, num, usRunTime);
        }
    }




}
