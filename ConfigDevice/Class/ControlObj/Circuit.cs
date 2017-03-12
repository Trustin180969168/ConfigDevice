using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class Circuit : ControlObj
    {
        public const string CLASS_NAME = "Circuit";
        public const string NAME_CMD_SWIT_LOOP = "开关回路";
        public const string NAME_CMD_SWIT_LOOP_OPEN = "开回路";
        public const string NAME_CMD_SWIT_LOOP_CLOSE = "关回路";
        private int circuitCount = 1;
        private MySocket mySocket = MySocket.GetInstance();

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
            set { circuitCount = value; }
        }
        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系

        public Circuit(Device _deviceCtrl,int _circuitCount):base(_deviceCtrl)
        {
            Name = DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME; 
            circuitCount = _circuitCount;
            //-----初始化列表---------
            for (int i = 1; i <= circuitCount; i++)
                ListCircuitIDAndName.Add(i, ""); 

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
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME, this.deviceControled.EditHandleID, getRoadTitles);//----注册回调---
            //----注册结束回调,统一个界面多个读取完毕,这里不能根据句柄,要加上本类名---
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, this.deviceControled.EditHandleID + DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME + CallbackUuid, finishGetRoadTitles);
            UdpData udpSend = createReadRoadTitleUdp();
            mySocket.SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadRoadTitle), null);
        }
        private void callbackReadRoadTitle(UdpData udpReply, object[] values)
        {
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
            crcData[11] = (byte)circuitCount;//结束回路

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

            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----
            if (finishReadRoads == true) return;
           
            byte[] byteName = CommonTools.CopyBytes(userData.Data, 4, userData.DataOfLength - 4 - 4);

            int num = userData.Data[0];
            string roadName = Encoding.GetEncoding("GB2312").GetString(byteName);
            if (ListCircuitIDAndName.ContainsKey(num + 1)) ListCircuitIDAndName[num + 1] = roadName.Replace(" ", "");
            if (num + 1 == this.circuitCount) 
                finishReadRoads = true;//---表示回路已经读取完毕----
        }

        /// <summary>
        /// 完成读取每路门窗名称
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void finishGetRoadTitlesData(UdpData data, object[] values)
        { 
            UserUdpData userData = new UserUdpData(data);
            byte[] cmd = new byte[] { userData.Data[0], userData.Data[1] };//----找出回调的命令-----
            if (userData.SourceID == deviceControled.DeviceID && CommonTools.BytesEuqals(cmd, DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME))           
            {
                UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----  
                this.deviceControled.CallbackUI( new CallbackParameter(ActionKind.ReadCircuit, deviceControled.DeviceID));//---回调UI---
                CallbackUI(new CallbackParameter(ActionKind.ReadCircuit, deviceControled.DeviceID));//---回调
 
                //SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME, this.EditHandleID);
                //SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, this.EditHandleID);
            }
        }


        /// <summary>
        /// 保存回路配置
        /// </summary>
        public void SaveRoadSetting(int roadNum, string roadName)
        {
            UdpData udpSend = createSaveRoadSettingUdp(roadNum, roadName);
            mySocket.SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,
                 new CallbackUdpAction(callbackSaveRoadSetting), null);
        }
        private void callbackSaveRoadSetting(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("保存回路失败!", udpReply.ReplyByte);
            else
                ReadRoadTitle();
        }
        private UdpData createSaveRoadSettingUdp(int roadNum, string roadName)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME;//----用户命令-----
            //---------计算长度------------------
            //---------新名称-------------
            byte[] value = Encoding.GetEncoding("GB2312").GetBytes(roadName);
            byte[] byteName = new byte[32];
            Buffer.BlockCopy(value, 0, byteName, 0, value.Length);
            byte len = (byte)(1 + 2 + 1 + byteName.Length + 4);//---数据长度 = 第几路1 + 位置2 + 保留1 + 名称n + 校验码4-----   

            byte[] crcData = new byte[len - 4 + 10];//10 固定长度:源+目标+命令+长度+分页
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = (byte)roadNum;
            crcData[13] = 2;//11,12为位置,直接填0x0,0x0,忽略, 13为位置, 直接填0x02.
            Buffer.BlockCopy(byteName, 0, crcData, 14, byteName.Length);

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }


        /// <summary>
        /// 保存按键回路配置
        /// </summary>
        public void SaveKeyRoadSetting(int roadNum, string roadName)
        {
            byte[] value = Encoding.GetEncoding("GB2312").GetBytes(roadName);
            if (value.Length > 12)
            {
                CommonTools.MessageShow("回路名称过长!", 2, "按键名称的GB2312字节不能大于12");
                return;
            }
            UdpData udpSend = createSaveKeyRoadSettingUdp(roadNum, roadName);
            mySocket.SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,
                 new CallbackUdpAction(callbackSaveRoadSetting), null);
        } 
        private UdpData createSaveKeyRoadSettingUdp(int roadNum, string roadName)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME;//----用户命令-----
            //---------计算长度------------------
            //---------新名称-------------
            byte[] value = Encoding.GetEncoding("GB2312").GetBytes(roadName);
            byte[] byteName = new byte[12];
            Buffer.BlockCopy(value, 0, byteName, 0, value.Length);
            byte len = (byte)(1 + 2 + 1 + byteName.Length + 4);//---数据长度 = 第几路1 + 位置2 + 保留1 + 名称n + 校验码4-----   

            byte[] crcData = new byte[len - 4 + 10];//10 固定长度:源+目标+命令+长度+分页
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = (byte)roadNum;
            crcData[13] = 2;//11,12为位置,直接填0x0,0x0,忽略, 13为位置, 直接填0x02.
            Buffer.BlockCopy(byteName, 0, crcData, 14, byteName.Length);

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }


    }


}
