using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    /// <summary>
    /// 人体感应设备
    /// </summary>
    public class BodyInduction:Device
    {
        public BodyInductionLight Light;//指示灯

        public UWSensor1 UWSensor1;//-----超声波1
        public UWSensor1 UWSensor2;//-----超声波2
        public IRSensor IRSensor;//------红外
        
        public Circuit Circuit;//----回路对象---
        public SecurityObj SecurityObj;//-----安防对象-----
        private CallbackFromUDP getAdditionLogic;//----获取附加逻辑信息---- 
        private CallbackFromUDP getWriteEnd;//----获取结束读取信息----
        private CallbackFromUDP getStateInfo;//----获取设置信息---- 
        private CallbackFromUDP getConfig;//-------获取设备配置---- 

        public BodyInduction(UserUdpData userUdpData)
            : base(userUdpData)
        { 
            initCallback();
            initControlObjs();
            InitSensors();
        }

        public BodyInduction(DeviceData data)
            : base(data)
        { 
            initCallback();
            initControlObjs();
            InitSensors();
        }

        public BodyInduction(DataRow dr)
            : base(dr)
        { 
            initCallback();
            initControlObjs();
            InitSensors();
        }

        /// <summary>
        /// 初始化传感器
        /// </summary>
        public void InitSensors()
        {
            UWSensor1 = new UWSensor1(this.GetDeviceData(),0);
            UWSensor2 = new UWSensor1(this.GetDeviceData(),2);
            IRSensor = new IRSensor(this.GetDeviceData(),1);
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            Light = new BodyInductionLight(this);
            Circuit = new Circuit(this,16);
            SecurityObj = new SecurityObj(this);
            Light.OnCallbackUI_Action += this.CallbackUI;
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, Circuit);
        }

        /// <summary>
        /// 注册RJ45回调
        /// </summary>
        private void initCallback()
        {
            RemoveRJ45Callback();
            getConfig = new CallbackFromUDP(getConfigData);//----配置---
            getAdditionLogic = new CallbackFromUDP(this.getAdditionLogicData);//---逻辑附加动作---
            getStateInfo = new CallbackFromUDP(getStateInfoData);//获取短路配置
            getWriteEnd = new CallbackFromUDP(this.getWriteEndData);//--回调确认附加动作---
        }

        /// <summary>
        /// 取消对应指令所有回调订阅
        /// </summary>
        public void RemoveRJ45Callback()
        {
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PRI_WRITE_CONFIG);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_STATE);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_LOGIC_WRITE_EXACTION);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END); 
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_VER);
        }


        /// <summary>
        /// 读参数
        /// </summary>
        public void ReadConfig()
        { 
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PRI_WRITE_CONFIG,this.DeviceID,  getConfig);//----注册回调--- 
            UdpData udpSend = createReadCconfigUdp();
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort,
                new CallbackUdpAction(callbackReadConfigUdp), null);
        }
        private void callbackReadConfigUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("读取参数失败!", udpReply.ReplyByte);
        }
        private UdpData createReadCconfigUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PRI_READ_CONFIG;//----用户命令-----
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
        /// 获取配置参数
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getConfigData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != DeviceID) return;//不是本设备ID不接收.

            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----
            //----传感器ID------------
            UWSensor1.Sensitivity =  userData.Data[0];
            UWSensor2.Sensitivity =  userData.Data[2];
            IRSensor.Sensitivity = userData.Data[1];

            this.CallbackUI(new CallbackParameter(  ActionKind.ReadConfig,this.DeviceID));//---回调UI---
   
        }


        /// <summary>
        /// 写参数
        /// </summary>
        public void SaveConfig()
        {
            UdpData udpSend = createWriteConfigUdp();
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort,
                new CallbackUdpAction(callbackWriteConfigUdp), null);
        }
        private void callbackWriteConfigUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("设置参数失败!", udpReply.ReplyByte);

        }
        private UdpData createWriteConfigUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PRI_WRITE_CONFIG;//----用户命令-----
            byte len = 4 + 3;//---数据长度---- 

            byte[] crcData = new byte[10 +3];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = UWSensor1.Sensitivity;
            crcData[11] = IRSensor.Sensitivity;
            crcData[12] = UWSensor2.Sensitivity; 

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 申请读取状态
        /// </summary>
        public void ReadState()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_STATE, this.DeviceID, getStateInfo);
            UdpData udpSend = createReadStateUdp();
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadState), null);
        }
        private void callbackReadState(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取状态失败!", udpReply.ReplyByte);
        }
        private UdpData createReadStateUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_STATE;//----用户命令-----
            byte len = 0x04;//---数据长度----
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
        /// 获取状态
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getStateInfoData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != this.DeviceID) return;
            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----

            //------找出数据段------
            string dataStr = ConvertTools.ByteToHexStr(userData.Data);
            string dataStr1 = dataStr.Split(new string[] { "FF FF" }, StringSplitOptions.RemoveEmptyEntries)[0];

            byte[] dataByte1 = ConvertTools.StrToToHexByte(dataStr1);
            this.UWSensor1.SensorData = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_UW_1, dataByte1) as UWSensorData;//超声波1
            this.UWSensor2.SensorData = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_UW_2, dataByte1) as UWSensorData;//超声波2
            this.IRSensor.SensorData = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_IR, dataByte1) as IRSensorData;//红外状态
            this.IRSensor.LumSensorData = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_LUMI, dataByte1) as LuminanceSensorData;//光感4
            CallbackUI(new CallbackParameter(ActionKind.ReadSate,this.DeviceID));//----读完状态信息,回调界面----
        }


        /// <summary>
        /// 申请读取附加动作
        /// </summary>
        public void ReadAdditionLogic(int groupNum)
        {
            ReadAdditionLogic(groupNum, groupNum);
        }
        /// <summary>
        /// 申请读取附加动作
        /// </summary>
        public void ReadAdditionLogic(int startNum, int endNum)
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_LOGIC_WRITE_EXACTION, EditHandleID, getAdditionLogic);
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, EditHandleID + DeviceConfig.CMD_LOGIC_WRITE_EXACTION, getWriteEnd);
            UdpData udpSend = createReadAdditionLogicUdp(startNum, endNum);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadAdditionLogic), null);
        }
        private void callbackReadAdditionLogic(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取逻辑附加动作失败!", udpReply.ReplyByte);
        }
        private UdpData createReadAdditionLogicUdp(int startNum, int endNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_LOGIC_READ_EXACTION;//----用户命令-----
            byte len = 6;//---数据长度----
            byte[] crcData = new byte[10 + 2];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = (byte)startNum;
            crcData[11] = (byte)endNum;
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------            
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getAdditionLogicData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != this.DeviceID) return;
            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----

            byte[] value = userData.Data;
            SetAdditionLogicData(value);
        }

        /// <summary>
        /// 设置附加动作
        /// </summary>
        /// <param name="value"></param>
        public void SetAdditionLogicData(byte[] value)
        {
            this.Light.LedAct = value[1];//动作类型
            this.Light.LedTim = ConvertTools.Bytes2ToUInt16(new byte[] { value[2], value[3] });//动作时间
           
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getWriteEndData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            byte[] cmd = new byte[] { userData.Data[0], userData.Data[1] };//----找出回调的命令-----
            if (userData.SourceID == DeviceID && CommonTools.BytesEuqals(cmd, DeviceConfig.CMD_LOGIC_WRITE_EXACTION))
            {
                UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----
                this.CallbackUI(new CallbackParameter( ActionKind.ReadAdditionAciton,this.DeviceID));//---回调UI---
            }
        }


        /// <summary>
        /// 申请写附加动作
        /// </summary>
        public void SaveAdditionLogic(int groupNum)
        {
            UdpData udpSend = createWriteAdditionLogicUdp(groupNum);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackWriteAdditionLogic), null);
        }
        private void callbackWriteAdditionLogic(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请写逻辑附加动作失败!", udpReply.ReplyByte);
        }
        private UdpData createWriteAdditionLogicUdp(int groupNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_LOGIC_WRITE_EXACTION;//----用户命令-----

            byte len = 4 + 7;//---数据长度----
            byte[] crcData = new byte[10 + 7];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            crcData[10] = (byte)groupNum;
            crcData[11] = (byte)this.Light.LedAct;//----指示灯动作---
            byte[] byteSeconds = ConvertTools.GetByteFromUInt16(Light.LedTim);
            Buffer.BlockCopy(byteSeconds, 0, crcData, 12, 2);
            Buffer.BlockCopy(byteSeconds, 0, crcData, 15, 2);

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------            
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }
        /// <summary>
        /// 获取附加值
        /// </summary>
        /// <returns></returns>
        public byte[] GetAdditionValue()
        {
            byte[] additionValue = new byte[7];//----第一个为逻辑组号,不用设置
            additionValue[1] = Light.LedAct;
            additionValue[2] = ConvertTools.GetByteFromUInt16(Light.LedTim)[0];

            return additionValue;
        }
    }



}
