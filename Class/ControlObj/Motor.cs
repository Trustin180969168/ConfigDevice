﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Motor : ControlObj
    {
        public const string CLASS_NAME = "Motor";
        public const string ACTION_NAME_CURRENT_CURRENT = "Current";//---执行获取当前电流----
        public const string ACTION_NAME_CURRENT_ACTION = "Action";//---执行获取当前动作----
        public const string ACTION_NAME_PARAMETER = "Parameter";//---执行获取参数----- 
        /// <summary>
        /// 默认3路电机
        /// </summary>
        private int circuitCount = 3;
        public int CircuitCount
        {
            get { return circuitCount; }
            set { circuitCount = value; }
        }
        private byte[] configData;//----保存配置数据------
        public byte[] StateData;//----保存状态数据------
        private byte[] currentData;//----保持电流数据------
        private byte[] actionData;//----保持电流数据------

        public MotorParameterData[] ParameterDatas;//---参数数据--
        public int[] CurrentDatas;//---当前状态数据--
        //-----阀门定义-----
        public const int LEL_VALACT_CLOSE = 0;        //【关阀门】
        public const int LEL_VALACT_OPEN = 1;        //【开阀门】
        public const int LEL_VALACT_NOT = 2;        //【不动作】
        public const int LEL_VALACT_TOTAL = 3;        //【总数】
        public const int LEL_VALACT_DEFAULT = LEL_VALACT_CLOSE;
        public byte ValAct = 0;               //阀门  动作类型：关阀门、开阀门 (如:LEL_VALACT_CLOSE)
        public ushort ValTim = 0;               //阀门  动作时间：单位秒         (如:10->10秒，0->强制为1秒)

        public const string STATE_VALVE_STOP = "停止";
        public const string STATE_VALVE_CLOSE = "关阀门";
        public const string STATE_VALVE_OPEN = "开阀门";
        public const string STATE_VALVE_NONE = "不动作";
        public const string STATE_VALVE_TOTAL = "总数";
        public Int16 MaxStopCE = 0;//卡停电流
        public Int16 MaxRunTime = 0;//最大运行秒数
        //public Int16 Road1MaxStopCE = 0;//卡停电流
        //public Int16 Road1MaxRunTime = 0;//最大运行秒数
        //public Int16 Road2MaxStopCE = 0;//卡停电流
        //public Int16 Road2MaxRunTime = 0;//最大运行秒数
        //public Int16 Road3MaxStopCE = 0;//卡停电流
        //public Int16 Road3MaxRunTime = 0;//最大运行秒数

        //public Int16 Road1Current = 0;//1路当前电流  
        //public Int16 Road2Current = 0;//2路当前电流
        //public Int16 Road3Current = 0;//3路当前电流

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

        //---电机动作名称--- 
        public const string NAME_ACTION_STOP = "停转";
        public const string NAME_ACTION_FRONT = "正转";
        public const string NAME_ACTION_BACK = "反转";
        public const string NAME_ACTION_TEST = "测试";


        //---电机动作ID-----
        public const int ACTION_RUN = 1;
        public const int ACTION_STOP = 0;
        public const int ACTION_TEST = 1;
        public const int ACTION_ROAD_FRONT = 0;
        public const int ACTION_ROAD_BACK = 1;  

        public static Dictionary<string, byte[]> NameAndCommand = new Dictionary<string, byte[]>(); //名称与命令的对应关系
        private CallbackFromUDP getValveParameter;//-------阀门参数--- 
        private CallbackFromUDP getMotorParameter;//-------电机参数---- 
        private CallbackFromUDP getWriteEnd;//----获取结束读取信息----
        private CallbackFromUDP getMotorCurrent;//----获取电机电流----
        private CallbackFromUDP getMotorAction;//----获取电机电流----

        public Motor(Device _deviceCtrl, int _circuitCount)
            : this(_deviceCtrl)
        {
            circuitCount = _circuitCount;
            configData = new byte[circuitCount * 4];
            StateData = new byte[circuitCount * 2];
            currentData = new byte[circuitCount * 2];
            actionData = new byte[circuitCount * 2];

            ParameterDatas = new MotorParameterData[circuitCount];
            for (int i = 0; i < ParameterDatas.Length; i++)
                ParameterDatas[i] = new MotorParameterData();

            CurrentDatas = new int[circuitCount];
        }

        public Motor(Device _deviceCtrl)
            : base(_deviceCtrl)
        {
            Name = "电机";
            configData = new byte[circuitCount * 4];
            StateData = new byte[circuitCount * 2];
            currentData = new byte[circuitCount * 2];
            actionData = new byte[circuitCount * 2];
            if (NameAndCommand.Count == 0)
            {
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP, DeviceConfig.CMD_SW_SWIT_LOOP);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_OPEN, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_CLOSE, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_OPEN_CONDITION, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN_CONDITION);
                NameAndCommand.Add(NAME_CMD_SWIT_LOOP_CLOSE_CONDITION, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE_CONDITION);
            }
            getValveParameter = new CallbackFromUDP(getValveParameterData);//---阀门参数----
            getMotorParameter = new CallbackFromUDP(getMotorParameterData);//---电机参数(最大电流,最长启动时间)----
            getMotorCurrent = new CallbackFromUDP(getMotorCurrentData);//---电机当前电流----
            getWriteEnd = new CallbackFromUDP(getWriteEndData);//----获取电机参数读取结束---
            getMotorAction = new CallbackFromUDP(getMotorCurrentActionData);//----获取电机当前动作-----

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


        ///CMD_SW_SWIT_LOOP, 1时表示开，0时表示关
        ///CMD_SW_SWIT_LOOP_OPEN,CMD_SW_SWIT_LOOP_CLOSE，此字节无效
        ///CMD_SW_SWIT_LOOP_OPEN_CONDITION   1时执开，0时不作处理
        ///CMD_SW_SWIT_LOOP_CLOSE_CONDITION  0时执关，1时不作处理
        /// <summary>
        /// 开关电机
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoop(int ucStepVol, int actionIndex, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP, actionIndex, ucStepVol, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 开电机
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopOpen(int ucStepVol, int actionIndex, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_OPEN, actionIndex, ucStepVol, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 关电机
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopClose(int ucStepVol, int actionIndex, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE, actionIndex, ucStepVol, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 开执行
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopOpenCondition(int ucStepVol, int actionIndex, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_OPEN_CONDITION, actionIndex, ucStepVol, usRunTime, usOpenDly, usCloseDly);
        }

        /// <summary>
        /// 关执行
        /// </summary>
        /// <returns></returns>
        public CommandData SwitLoopCloseCondition(int ucStepVol, int actionIndex, int usRunTime, int usOpenDly, int usCloseDly)
        {
            return ControlAction(DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE_CONDITION, actionIndex, ucStepVol, usRunTime, usOpenDly, usCloseDly);
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
        /// 保持阀门参数
        /// </summary>
        public void SaveValveParameter(Int16 second, Int16 ec, UInt32 flag)
        {
            UdpData udpSend = createSaveValveParameterUdp(second, ec, flag);
            MySocket.GetInstance().SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,
                new CallbackUdpAction(callbackSaveValveParameterUdp), new object[] { second, ec, flag });
        }
        private void callbackSaveValveParameterUdp(UdpData udpReply, object[] values)
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
        private UdpData createSaveValveParameterUdp(Int16 second, Int16 ec, UInt32 flag)
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

        /// <summary>
        /// 保持电机参数
        /// </summary>
        public void SaveMotorParameter()
        {
            UdpData udpSend = createSaveMotorParameterUdp();
            MySocket.GetInstance().SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,
                new CallbackUdpAction(callbackSaveMotorParameterUdp), null);
        }
        private void callbackSaveMotorParameterUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("保存电机参数失败!", udpReply.ReplyByte);
            else //----错误则重新读取-----
                this.ReadMotorParameter();
        }
        private UdpData createSaveMotorParameterUdp()
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
            byte len = (byte)(4 + 1 + circuitCount * 2 * 2);//---数据长度:动作模式+crc校验+回路数*2个参数*每个参数大小2----


            byte[] crcData = new byte[10 + 1 + circuitCount * 2 * 2];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            for (int i = 0; i < ParameterDatas.Length; i++)
            {
                MotorParameterData parameterData = ParameterDatas[i];
                Buffer.BlockCopy(parameterData.GetValue(), 0, crcData, i * 4 + 11, 4);
            }

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
        public void ReadMotorParameter()
        {
            finishReadParameter = false;
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_CONFIG, deviceControled.EditHandleID, getMotorParameter);//----注册回调---
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, deviceControled.EditHandleID + DeviceConfig.CMD_PUBLIC_WRITE_CONFIG, getWriteEnd);
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
        /// 读参数
        /// </summary>
        public void ReadValveParameter()
        {
            finishReadParameter = false;
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_CONFIG, deviceControled.EditHandleID, getValveParameter);//----注册回调---
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, deviceControled.EditHandleID + DeviceConfig.CMD_PUBLIC_WRITE_CONFIG, getWriteEnd);
            UdpData udpSend = createReadParameterUdp();
            MySocket.GetInstance().SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,
                new CallbackUdpAction(callbackReadParameterUdp), null);
        }
        /// <summary>
        /// 获取阀门参数
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getValveParameterData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != deviceControled.DeviceID) return;//不是本设备ID不接收.

            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----
            //----翻译数据------------
            this.MaxRunTime = ConvertTools.Bytes2ToInt16(new byte[] { userData.Data[1], userData.Data[2] });//---最大运行时间---
            this.MaxStopCE = ConvertTools.Bytes2ToInt16(new byte[] { userData.Data[3], userData.Data[4] });//----最大停止电流---

            OpenValve = (int)(userData.Data[5] & 1) == 1 ? true : false;//---是否开阀门
            ClearLight = (int)(userData.Data[5] & 2) == 2 ? true : false;//---是否关闭指示灯
            ClearBuzzer = (int)(userData.Data[5] & 4) == 4 ? true : false;//---是否关闭蜂鸣器     
            finishReadParameter = true;

        }

        /// <summary>
        /// 获取电机参数
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getMotorParameterData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != deviceControled.DeviceID) return;//不是本设备ID不接收.

            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----

            Buffer.BlockCopy(userData.Data, 1, configData, 0, configData.Length);//----复制到配置数据中,便于扩展------
            for (int i = 0; i < circuitCount; i++)
            {
                int j = i * 4;
                ParameterDatas[i] = new MotorParameterData(
                    ConvertTools.Bytes2ToInt16(new byte[] { configData[j], configData[j + 1] }),
                    ConvertTools.Bytes2ToInt16(new byte[] { configData[j + 2], configData[j + 3] })
                    );
            }

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
                UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----
                this.deviceControled.CallbackUI(new CallbackParameter(ActionKind.ReadConfig, deviceControled.DeviceID));//---回调UI---
            }
        }

        /// <summary>
        /// 操作电机
        /// </summary>
        /// <param name="roadNum">第几路</param>
        /// <param name="actionNum">动作id</param>
        public void MotorAction(int roadNum, string actionName)
        {
            if (actionName == Motor.NAME_ACTION_TEST)
            {
                TestMotorAction(roadNum);
                return;
            }
            int actionId = 0;
            switch (actionName)
            {
                case Motor.NAME_ACTION_STOP: actionId = Motor.ACTION_STOP; break;
                case Motor.NAME_ACTION_FRONT:
                case Motor.NAME_ACTION_BACK: actionId = Motor.ACTION_RUN; break;
                default: break;
            }

            int actionRoadId = 0;
            switch (actionName)
            {
                case Motor.NAME_ACTION_STOP:
                case Motor.NAME_ACTION_FRONT: actionRoadId = roadNum * 2; break;
                case Motor.NAME_ACTION_BACK: actionRoadId = roadNum * 2 + 1; break;
                default: break;
            }
            MotorAction(actionRoadId, actionId);
        }

        /// <summary>
        /// 操作电机
        /// </summary>
        ///  
        public void MotorAction(int goDirection, int actionNum)
        {
            UdpData udpSend = createMotorActionUdp(goDirection, actionNum);
            MySocket.GetInstance().SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadRoadTitle), null);
        }
        private void callbackReadRoadTitle(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("执行运转操作失败!", udpReply.ReplyByte);
        }
        private UdpData createMotorActionUdp(int goDirection, int actionNum)
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
            crcData[10] = (byte)goDirection;//起始回路为第一回路
            crcData[11] = (byte)actionNum;//结束回路

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 电机测试
        /// </summary>
        /// <param name="roadNum">回路(0为第一回路)</param>
        public void TestMotorAction(int roadNum)
        {
            UdpData udpSend = createTestMotorActionUdp(roadNum);
            MySocket.GetInstance().SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackTestMotorAction), null);
        }
        private void callbackTestMotorAction(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("执行运转操作失败!", udpReply.ReplyByte);
        }
        private UdpData createTestMotorActionUdp(int roadNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_SW_TEST_LOOP;//----用户命令-----
            byte len = 1 + 4;//---数据长度----
            byte[] crcData = new byte[11];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = (byte)roadNum;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }


        /// <summary>
        /// 读电机电流状态
        /// </summary>
        public void ReadMotorCurrent()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_WINDOWS_WRITE_POWER, deviceControled.EditHandleID, getMotorCurrent);//----注册回调--- 
            UdpData udpSend = createReadCurrentUdp();
            MySocket.GetInstance().SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,
                new CallbackUdpAction(callbackReadCurrentUdp), null);
        }
        private void callbackReadCurrentUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("读取状态失败!", udpReply.ReplyByte);
        }
        private UdpData createReadCurrentUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_WINDOWS_READ_POWER;//----用户命令-----
            byte len = 4;//---数据长度---- 

            byte[] crcData = new byte[10];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }
        /// <summary>
        /// 获取电机状态
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getMotorCurrentData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != deviceControled.DeviceID) return;//不是本设备ID不接收.

            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----
            //----翻译数据------------ 
            for (int i = 0; i < circuitCount; i++)
            {
                CurrentDatas[i] = ConvertTools.Bytes2ToInt16(new byte[] { userData.Data[i * 2], userData.Data[i * 2 + 1] });
            }
            Buffer.BlockCopy(userData.Data, 0, currentData, 0, currentData.Length);//----复制到数据中,便于扩展------ 
            this.deviceControled.CallbackUI(new CallbackParameter(ActionKind.ReadPower, deviceControled.DeviceID));//---回调UI---
        }


        /// <summary>
        /// 读当前动作
        /// </summary>
        public void ReadMotorCurrentAction()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_STATE, deviceControled.EditHandleID, getMotorAction);//----注册回调--- 
            UdpData udpSend = createReadMotorCurrentActionUdp();
            MySocket.GetInstance().SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,
                new CallbackUdpAction(callbackReadCurrentActionUdp), null);
        }
        private void callbackReadCurrentActionUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("读取动作状态失败!", udpReply.ReplyByte);
        }
        private UdpData createReadMotorCurrentActionUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_STATE;//----用户命令-----
            byte len = 4;//---数据长度---- 

            byte[] crcData = new byte[10];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }
        /// <summary>
        /// 获取当前电流
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getMotorCurrentActionData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != deviceControled.DeviceID) return;//不是本设备ID不接收.
            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----
            //----翻译数据------------ 
            //public const string NAME_ACTION_ROAD_FRONT_1 = "1路正转";
            //public const string NAME_ACTION_ROAD_BACK_1 = "1路反转";
            //public const string NAME_ACTION_ROAD_FRONT_2 = "2路正转";
            //public const string NAME_ACTION_ROAD_BACK_2 = "2路反转";
            //public const string NAME_ACTION_ROAD_FRONT_3 = "3路正转";
            //public const string NAME_ACTION_ROAD_BACK_3 = "3路反转";

            //第1路正转状态，0人为停转，1正转自停，2正转卡停，≥3正转
            //第1路反转状态，0人为停转，1反转自停，2反转卡停，≥3反转
            //第2路正转状态，0人为停转，1正转自停，2正转卡停，≥3正转
            //第2路反转状态，0人为停转，1反转自停，2反转卡停，≥3反转
            //......
            //......
            byte[] value = userData.Data;
            Int16[] actionValue = new Int16[circuitCount * 2];
            for (int i = 0; i < circuitCount * 2; i++)
            {
                actionValue[i] = (Int16)value[i];
            }
            string[] actionResult = new string[circuitCount];
            for (int i = 0; i < circuitCount; i++)
            {
                int j = i * 2;
                actionResult[i] = "";
                if (actionValue[j] == 0 && actionValue[j + 1] == 0) { actionResult[i] = "停转"; continue; }
                if (actionValue[j] == 1 && actionValue[j + 1] == 0) { actionResult[i] = "正转自停"; continue; }
                if (actionValue[j] == 2 && actionValue[j + 1] == 0) { actionResult[i] = "正转卡停"; continue; }
                if (actionValue[j] == 3 && actionValue[j + 1] == 0) { actionResult[i] = "正转"; continue; }
                if (actionValue[j] == 0 && actionValue[j + 1] == 1) { actionResult[i] = "反转自停"; continue; }
                if (actionValue[j] == 0 && actionValue[j + 1] == 2) { actionResult[i] = "反转卡停"; continue; }
                if (actionValue[j] == 0 && actionValue[j + 1] == 3) { actionResult[i] = "反转"; continue; }
            }

            actionData = CommonTools.CopyBytes(userData.Data, 0, actionData.Length);//---保存所有动作----
            this.deviceControled.CallbackUI(new CallbackParameter(ActionKind.ReadSate, deviceControled.DeviceID,
                actionResult));//---回调UI---
        }

    }



}