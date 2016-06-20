using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Motor : ControlObj
    {
        public const string CLASS_NAME = "Motor";

        //-----阀门定义-----
        public const int LEL_VALACT_CLOSE  =    0;        //【关阀门】
	    public const int LEL_VALACT_OPEN   =    1;        //【开阀门】
	    public const int LEL_VALACT_NOT    =    2;        //【不动作】
	    public const int LEL_VALACT_TOTAL  =    3;        //【总数】
	    public const int LEL_VALACT_DEFAULT =   LEL_VALACT_CLOSE;
	    public byte  ValAct = 0;               //阀门  动作类型：关阀门、开阀门 (如:LEL_VALACT_CLOSE)
	    public ushort ValTim = 0;               //阀门  动作时间：单位秒         (如:10->10秒，0->强制为1秒)

        public const string STATE_VALVE_STOP = "停止";
        public const string STATE_VALVE_CLOSE = "关阀门";
        public const string STATE_VALVE_OPEN = "开阀门";
        public const string STATE_VALVE_NONE = "不动作";
        public const string STATE_VALVE_TOTAL = "总数";
        public Int16 MaxStopCE = 0;//卡停电流
        public Int16 MaxRunTime = 0;//最大运行秒数
        public bool OpenValve = false;//是否开阀门
        public bool ClearLight = false;//是否关闭指示灯
        public bool ClearBuzzer = false;//是否关闭蜂鸣器 
        public string ValveState = "";//---阀门状态---
        public short ValveElectricCurrent = 0;//---电流---
        //--------电机及公共部分-------
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

        public const int ACTION_RUN = 1;
        public const int ACTION_STOP = 0;
        public const int ACTION_ROAD_FRONT_1 = 0;
        public const int ACTION_ROAD_BACK_1 = 1;
        public const int ACTION_ROAD_FRONT_2 = 2;
        public const int ACTION_ROAD_BACK_2 = 3;
        public const int ACTION_ROAD_FRONT_3 = 4;
        public const int ACTION_ROAD_BACK_3 = 5; 


        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系
        private CallbackFromUDP getParameter;//-------每参数名称----
        private CallbackFromUDP getWriteEnd;//----获取结束读取信息----
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
            getParameter = new CallbackFromUDP(getParameterData);          
            
            getWriteEnd = new CallbackFromUDP(this.getWriteEndData);
            getWriteEnd.Values = new object[] { CLASS_NAME,this.UUID };
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


        /// <summary>
        /// 写参数
        /// </summary>
        public void WriteParameter(Int16 second, Int16 ec, UInt32 flag)
        {
            UdpData udpSend = createWriteParameterUdp(second, ec, flag);
            MySocket.GetInstance().SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,
                new CallbackUdpAction(callbackWriteParameterUdp), new object[] { second, ec, flag });
        }
        private void callbackWriteParameterUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("设置参数失败!", udpReply.ReplyByte);
            else
            {
                MaxRunTime = (Int16)values[0];
                MaxStopCE = (Int16)values[1];
                UInt32 flag = (UInt32)values[2];

                OpenValve = (int)(flag & 1) == 1 ? true : false;//---是否开阀门
                ClearLight = (int)(flag & 2) == 2 ? true : false;//---是否关闭指示灯
                ClearBuzzer = (int)(flag & 4) == 4 ? true : false;//---是否关闭蜂鸣器
            }
        }
        private UdpData createWriteParameterUdp(Int16 second, Int16 ec, UInt32 flag)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_CONFIG;//----用户命令-----
            byte len = 4 + 1 + 8;//---数据长度----
            byte[] runMaxTime = ConvertTools.GetByteFromInt16(second);
            byte[] maxEC = ConvertTools.GetByteFromInt16(ec);

            byte[] crcData = new byte[19];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            Buffer.BlockCopy(runMaxTime, 0, crcData, 11, 2);
            Buffer.BlockCopy(maxEC, 0, crcData, 13, 2);
            byte[] byteFlag = ConvertTools.GetByteFromUInt32(flag);
            Buffer.BlockCopy(byteFlag, 0, crcData, 15, 4);

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }



        private bool finishReadParameter = false;//---判断是否读取完参数----
        /// <summary>
        /// 读参数
        /// </summary>
        public void ReadParameter()
        {
            finishReadParameter = false;
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_CONFIG, this.UUID, getParameter);//----注册回调---
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, this.UUID, getWriteEnd); 
            UdpData udpSend = createReadParameterUdp();
            MySocket.GetInstance().SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,
                new CallbackUdpAction(callbackReadParameterUdp), null);
        }
        private void callbackReadParameterUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("读取参数失败!", udpReply.ReplyByte);
        }
        private UdpData createReadParameterUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_CONFIG;//----用户命令-----
            byte len = 4 + 2;//---数据长度---- 

            byte[] crcData = new byte[12];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = 0;
            crcData[11] = 0;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取阀门参数
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getParameterData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != deviceControled.DeviceID && (string)values[0] != this.GetType().Name) return;//不是本设备ID不接收.

            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
            //----翻译数据------------
            this.MaxRunTime = ConvertTools.Bytes2ToInt16(new byte[] { userData.Data[1], userData.Data[2] });//---最大运行时间---
            this.MaxStopCE = ConvertTools.Bytes2ToInt16(new byte[] { userData.Data[3], userData.Data[4] });//----最大停止电流---

            OpenValve = (int)(userData.Data[5] & 1) == 1 ? true : false;//---是否开阀门
            ClearLight = (int)(userData.Data[5] & 2) == 2 ? true : false;//---是否关闭指示灯
            ClearBuzzer = (int)(userData.Data[5] & 4) == 4 ? true : false;//---是否关闭蜂鸣器     
            finishReadParameter = true;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getWriteEndData(UdpData data, object[] values)
        {
            if (!finishReadParameter) return;//---未执行完读取阀门参数,不执行.
            UserUdpData userData = new UserUdpData(data);
            byte[] cmd = new byte[] { userData.Data[0], userData.Data[1] };//----找出回调的命令-----
            if (userData.SourceID == deviceControled.DeviceID && CommonTools.BytesEuqals(cmd, DeviceConfig.CMD_PUBLIC_WRITE_CONFIG)) 
            {
                UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
                this.deviceControled.CallbackUI(new CallbackParameter(Motor.CLASS_NAME));//---回调UI---
            }
        }

        /// <summary>
        /// 操作电机
        /// </summary>
        public void MotorAction(int goDirection, int stop)
        {
            UdpData udpSend = createMotorActionUdp(goDirection, stop);
            MySocket.GetInstance().SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadRoadTitle), null);
        }
        private void callbackReadRoadTitle(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("执行运转操作失败!", udpReply.ReplyByte);
        }
        private UdpData createMotorActionUdp(int goDirection, int stop)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_SIMPLE_SWIT;//----用户命令-----
            byte len = 0x06;//---数据长度----
            byte[] crcData = new byte[12];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = 0;//起始回路为第一回路
            crcData[11] = (byte)stop;//结束回路

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
