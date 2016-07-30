using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{


    public class Background : ControlObj
    {
        public const string NAME_CMD_SWIT_ALL_MUSIC="开关音乐";
        public const string NAME_CMD_SWIT_OPEN_MUSIC="开音乐";
        public const string NAME_CMD_SWIT_CLOSE_MUSIC = "关音乐";

        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系
        public Background(Device _deviceCtrl):base(_deviceCtrl)
        {
            Name = DeviceConfig.CONTROL_OBJECT_BGM_NAME;
            if (NameAndCommand.Count == 0)
            {
                NameAndCommand.Add(NAME_CMD_SWIT_ALL_MUSIC, DeviceConfig.CMD_SW_SWIT_LOOP);
                NameAndCommand.Add(NAME_CMD_SWIT_OPEN_MUSIC, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN);
                NameAndCommand.Add(NAME_CMD_SWIT_CLOSE_MUSIC, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE);
            }
        }


        /// <summary>
        /// 获取执行命令数据
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="usRunTime">运行时间</param>
        /// <returns>CommandData</returns>
        public CommandData GetCommandData(byte[] command, int volume,int source,int playKind,int playNum,int palyTime)
        {
            CommandData cmdData = new CommandData("背景播放");
            cmdData.TargetId = deviceControled.ByteDeviceID;
            cmdData.TargetNet = deviceControled.ByteNetworkId;
            cmdData.TargetType = deviceControled.ByteKindID;

            cmdData.Cmd = command;
            cmdData.DataLen = 10;

            cmdData.Data[0] = 0;//0或者1，CMD_SW_SWIT_LOOP时 1表示开，0表示关闭CMD_SW_SWIT_LOOP_OPEN 时 与此值无关，打开音乐CMD_SW_SWIT_LOOP_CLOSE 时 与此值无关
            cmdData.Data[1] = 0;//保留
            cmdData.Data[2] = (byte)AudioKind.GENERAL_BGM;//类型
            cmdData.Data[3] = (byte)volume;//音量
            cmdData.Data[4] = (byte)source;//音源
            cmdData.Data[5] = (byte)playKind;//播放方式
            Buffer.BlockCopy(BitConverter.GetBytes(playNum), 0, cmdData.Data, 6, 2);//曲目
            Buffer.BlockCopy(BitConverter.GetBytes(palyTime), 0, cmdData.Data, 8, 2);//播放时间

            return cmdData;
        }


    }




}
