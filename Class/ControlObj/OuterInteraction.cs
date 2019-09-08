﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class OuterInteraction : ControlObj
    {
        public const string NAME_CMD_LOGIC_WRITE_SYSLKID = "开关";
        public const string NAME_CMD_LOGIC_WRITE_SYSLKID_OPEN = "开";
        public const string NAME_CMD_LOGIC_WRITE_SYSLKID_CLOSE = "关";

        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系
        public OuterInteraction(Device _deviceCtrl)
            : base(_deviceCtrl)
        {
            Name = "系统联动"; 
            if (NameAndCommand.Count == 0)
            {
                NameAndCommand.Add(NAME_CMD_LOGIC_WRITE_SYSLKID, DeviceConfig.CMD_LOGIC_WRITE_SYSLKID);
                NameAndCommand.Add(NAME_CMD_LOGIC_WRITE_SYSLKID_OPEN, DeviceConfig.CMD_LOGIC_WRITE_SYSLKID_OPEN);
                NameAndCommand.Add(NAME_CMD_LOGIC_WRITE_SYSLKID_CLOSE, DeviceConfig.CMD_LOGIC_WRITE_SYSLKID_CLOSE);
            }
        }


        /// <summary>
        /// 内部联动
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="actionIndex">开关     (0关， 非零开)</param>
        /// <param name="interactionNum">联动号</param>
        /// <param name="usRunTime">运行时间</param>
        /// <param name="usOpenDly">开延迟</param>
        /// <param name="usCloseDly">关延迟</param>
        /// <returns></returns>
        public CommandData GetCommandData(byte[] cmd, int actionIndex, int interactionNum,
            int usRunTime, int usOpenDly, int usCloseDly)
        {
            CommandData cmdData = new CommandData("系统联动");
            cmdData.TargetId = deviceControled.ByteDeviceID;
            cmdData.TargetNet = deviceControled.ByteNetworkId;
            cmdData.TargetType = deviceControled.ByteKindID;

            cmdData.Cmd = cmd;
            cmdData.DataLen = 9;

            cmdData.Data[0] = (byte)actionIndex;
            cmdData.Data[1] = (byte)interactionNum;
            cmdData.Data[2] = 0;//---目前固定为0----
            Buffer.BlockCopy(BitConverter.GetBytes(usRunTime), 0, cmdData.Data, 3, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(usOpenDly), 0, cmdData.Data, 5, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(usCloseDly), 0, cmdData.Data, 7, 2);

            return cmdData;
        }




    }




}