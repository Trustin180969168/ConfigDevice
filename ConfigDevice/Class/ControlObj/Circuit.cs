﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class Circuit : ControlObj
    {
        public const string NAME_CMD_SWIT_LOOP = "开关回路";
        public const string NAME_CMD_SWIT_LOOP_OPEN = "开回路";
        public const string NAME_CMD_SWIT_LOOP_CLOSE = "关回路";
        private int circuitCount = 1;
        private MySocket mySocket = MySocket.GetInstance();
        public event CallbackParameterUIAction OnCallbackRoad_Action;   //----回调UI----
        public Dictionary<int, string> ListCircuitIDAndName = new Dictionary<int, string>();//回路ID和名称对应表用于指令配置
        private CallbackFromUDP getRoadTitles;//-------每回路名称----
        private CallbackFromUDP finishGetRoadTitles;//-------完成读取回路名称----

        /// <summary>
        /// 是否完成回路的读取
        /// </summary>
        private bool finishReadRoads = false;
        public bool FinishReadRoads
        {
            get { return finishReadRoads; }
        }

        /// <summary>
        /// 回路数
        /// </summary>
        public int CircuitCount
        {
            get { return circuitCount; }
        }
        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系

        public Circuit(Device _deviceCtrl,int _circuitCount)
        {
            Name = "回路"; 
            circuitCount = _circuitCount;
            //-----初始化列表---------
            for (int i = 1; i <= circuitCount; i++)
                ListCircuitIDAndName.Add(i, "");
            deviceControled = _deviceCtrl;

            if (NameAndCommand.Count == 0)
            {
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP, DeviceConfig.CMD_SW_SWIT_LOOP);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_OPEN, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_CLOSE, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE);
            }
            getRoadTitles = new CallbackFromUDP(getRoadTitlesData);
            finishGetRoadTitles = new CallbackFromUDP(finishGetRoadTitlesData);
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

        /// <summary>
        /// 申请读取回路名称
        /// </summary>
        public void ReadRoadTitle()
        {
            finishReadRoads = false;
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME, deviceControled.DeviceID, getRoadTitles);//----注册回调---
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, deviceControled.DeviceID, finishGetRoadTitles);//----注册回调---
            UdpData udpSend = createReadRoadTitleUdp();
            mySocket.SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadRoadTitle), new object[] { udpSend });
        }
        private void callbackReadRoadTitle(UdpData udpReply, object[] values)
        {
            UdpData udpSend = (UdpData)values[0];
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取回路名称失败!", udpReply.ReplyByte);
        }
        private UdpData createReadRoadTitleUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_LOOP_NAME;//----用户命令-----
            byte len = 0x06;//---数据长度----
            byte[] crcData = new byte[12];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = 0;//起始回路为第一回路
            crcData[11] = 0xFF;//结束回路

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }
        /// <summary>
        /// 获取每路门窗名称
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getRoadTitlesData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != deviceControled.DeviceID) return;//不是本设备ID不接收.

            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
            if (finishReadRoads == true) return;
           
            byte[] byteName = CommonTools.CopyBytes(userData.Data, 4, userData.DataOfLength - 4 - 4);

            int num = userData.Data[0];
            string roadName = Encoding.GetEncoding("GB2312").GetString(byteName);
            if (ListCircuitIDAndName.ContainsKey(num + 1)) ListCircuitIDAndName[num + 1] = roadName;

        }

        /// <summary>
        /// 完成读取每路门窗名称
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void finishGetRoadTitlesData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != deviceControled.DeviceID) return;//不是本设备ID不接收.

            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----        
            if (!finishReadRoads)
            {
                if (OnCallbackRoad_Action != null)
                    OnCallbackRoad_Action(null);
            }
            finishReadRoads = true;
        }
    }


}
