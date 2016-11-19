using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
 
    public class Short4:Device
    {
        public const string CLASS_NAME = "Short4";
        public const string ACTION_ADDITION = "Addition";//---附加动作回调
        public const string ACTION_SAFE = "Safe";//---安全配置回调
        public const string ACTION_STATE = "State";//---状态---
        public const string ACTION_CONFIG = "Config";//---配置参数---

        private byte[] securityLevel = new byte[4];//----安全级别------
        public bool[] SaftFlags  =  new bool[] { false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false };//---安防标志位------
        public Circuit Circuit;//回路对象
        private CallbackFromUDP getStateInfo;//----获取设置信息---- 
        private CallbackFromUDP getAdditionLogic;//----获取附加逻辑信息---- 
        private CallbackFromUDP getWriteEnd;//----获取结束读取信息----
        private CallbackFromUDP getSafeSetting;//---获取安全配置----
        private CallbackFromUDP getConfig;//-------每参数名称----
        private CallbackFromUDP getWriteEnd2;//----获取结束读取信息----
        //------逻辑传感器----
        public Short4Sensor Short4Sensor1;//----短路输入1----
        public Short4Sensor Short4Sensor2;//----短路输入2----
        public Short4Sensor Short4Sensor3;//----短路输入3----
        public Short4Sensor Short4Sensor4;//----短路输入4----
        //------短路输入参数------
        public byte ShortConfigRoad1 = 0;//短路输出1：上电初始电平 (如: SC_OUT_LOW)
        public byte ShortConfigRoad2 = 0;//短路输出2：上电初始电平 (如: SC_OUT_LOW)
        public byte ShortConfigRoad3 = 0;//短路输出3：上电初始电平 (如: SC_OUT_LOW)
        public byte ShortConfigRoad4 = 0;//短路输出4：上电初始电平 (如: SC_OUT_LOW)
        //------控制对象------
        public ShortInput Short4CtrlObj1;//---短路控制1--
        public ShortInput Short4CtrlObj2;//---短路控制2--
        public ShortInput Short4CtrlObj3;//---短路控制3--
        public ShortInput Short4CtrlObj4;//---短路控制4--

        public Short4(UserUdpData userUdpData)
            : base(userUdpData)
        { 
            initCallback();
            initControlObjs();
        }

        public Short4(DeviceData data)
            : base(data)
        { 
            initCallback();
            initControlObjs();
        }

        public Short4(DataRow dr)
            : base(dr)
        { 
            initCallback();
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {  
            Circuit = new Circuit(this,16);        
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, Circuit);

            Short4CtrlObj1 = new ShortInput(this);
            Short4CtrlObj2 = new ShortInput(this);
            Short4CtrlObj3 = new ShortInput(this);
            Short4CtrlObj4 = new ShortInput(this);

        }

        /// <summary>
        /// 注册RJ45回调
        /// </summary>
        private void initCallback()
        {
            RemoveRJ45Callback();
            getStateInfo = new CallbackFromUDP(this.getStateInfoData);
            getAdditionLogic = new CallbackFromUDP(this.getAdditionLogicData);
            getWriteEnd = new CallbackFromUDP(this.getWriteEndData);//--结束读取---
            getWriteEnd2 = new CallbackFromUDP(this.getWriteEndData2);//---结束读取---
            getSafeSetting = new CallbackFromUDP(this.getSafeSettingData);//--安防配置----
            getConfig = new CallbackFromUDP(this.getConfigData);//获取短路配置
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
            if (userData.SourceID != this.DeviceID) return;//不是本设备ID不接收.
            UdpTools.ReplyDataUdp(data);//----回复确认-----  
       
            //------找出数据段------
            string dataStr = ConvertTools.ByteToHexStr(userData.Data);
            string dataStr1 = dataStr.Split(new string[] { "FF FF" }, StringSplitOptions.RemoveEmptyEntries)[0];

            byte[] dataByte1 = ConvertTools.StrToToHexByte(dataStr1);
            this.Short4Sensor1 = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_SCIN_1, dataByte1) as Short4Sensor;//短路输入1
            this.Short4Sensor2 = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_SCIN_2, dataByte1) as Short4Sensor;//短路输入2
            this.Short4Sensor3 = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_SCIN_3, dataByte1) as Short4Sensor;//短路输入3
            this.Short4Sensor4 = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_SCIN_4, dataByte1) as Short4Sensor;//短路输入4
            CallbackUI(new CallbackParameter(ActionKind.ReadSate,this.DeviceID));//----读完状态信息,回调界面----
        }


        /// <summary>
        /// 安防动作关联
        /// </summary>
        public void ReadSafeSetting(int groupNum)
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_LOGIC_WRITE_SECURITY, this.DeviceID, getSafeSetting);
            UdpData udpSend = createReadSafeSettingUdp(groupNum);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadSafeSetting), null);
        }
        private void callbackReadSafeSetting(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取安全配置失败!", udpReply.ReplyByte);
        }
        private UdpData createReadSafeSettingUdp(int groupNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_LOGIC_READ_SECURITY;//----用户命令-----
            byte len = 4 + 2;//---数据长度----
            byte[] crcData = new byte[10 + 2];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            crcData[10] = (byte)groupNum;
            crcData[11] = (byte)groupNum;
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取安全数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getSafeSettingData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != this.DeviceID) return;//不是本设备ID不接收.
            UdpTools.ReplyDataUdp(data);//----回复确认-----  

            //------找出数据,并翻译------
            securityLevel = CommonTools.CopyBytes(userData.Data, 1, 4);
            byte b1 = securityLevel[0];
            byte b2 = securityLevel[1];
            int num = 0;
            for (int i = 1; i <= 128; i *= 2)
                SaftFlags[num++] = (int)(b1 & i) == i ? true : false;
            num = 8;
            for (int i = 1; i <= 64; i *= 2)
                SaftFlags[num++] = (int)(b2 & i) == i ? true : false;

            CallbackUI(new CallbackParameter(ActionKind.ReadSafe, DeviceID));//----读完状态信息,回调界面----
        }


        /// <summary>
        /// 保存安防配置
        /// </summary>
        public void SaveSafeSetting(int groupNum)
        {
            UdpData udpSend = createWriteSafeLogicUdp(groupNum);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackWriteSafeLogic), null);
        }
        private void callbackWriteSafeLogic(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请写逻辑附加动作失败!", udpReply.ReplyByte);
        }
        private UdpData createWriteSafeLogicUdp(int groupNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_LOGIC_WRITE_SECURITY;//----用户命令-----

            byte len = 4 + 9;//---数据长度----
            byte[] crcData = new byte[10 + 9];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            crcData[10] = (byte)groupNum;
            //-------安防级别-----------------
            byte b1 = 0;
            byte b2 = 0;
            int num = 0;
            for (int i = 1; i <= 128; i *= 2)
                if (SaftFlags[num++])
                    b1 = (byte)(b1 | i);
            num = 8;
            for (int i = 1; i <= 64; i *= 2)
                if (SaftFlags[num++])
                    b2 = (byte)(b2 | i);
            securityLevel[0] = b1;
            securityLevel[1] = b2;
            Buffer.BlockCopy(securityLevel, 0, crcData, 11, 4);
            //---其他字节默认为0,保留-----

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------            
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
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
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_LOGIC_WRITE_EXACTION, this.DeviceID, getAdditionLogic);
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, this.DeviceID, getWriteEnd); 
            UdpData udpSend = createReadAdditionLogicUdp(startNum,endNum);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadAdditionLogic), null);
        }
        private void callbackReadAdditionLogic(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取逻辑附加动作失败!", udpReply.ReplyByte);
        }
        private UdpData createReadAdditionLogicUdp(int startNum,int endNum)
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
            UdpTools.ReplyDataUdp(data);//----回复确认-----

            byte[] value = userData.Data;
            SetAdditionLogicData(value);
        }

        /// <summary>
        /// 设置附加动作
        /// </summary>
        /// <param name="value"></param>
        public void SetAdditionLogicData(byte[] value)
        {
            Short4CtrlObj1.ucScOutAct = value[1];//---高低电平
            Short4CtrlObj1.usScOutDly = ConvertTools.Bytes2ToUInt16(value[2], value[3]);//---延迟时间---
            Short4CtrlObj1.usScOutTim = ConvertTools.Bytes2ToUInt16(value[4], value[5]);//---动作时间---
            Short4CtrlObj2.ucScOutAct = value[6];//---高低电平
            Short4CtrlObj2.usScOutDly = ConvertTools.Bytes2ToUInt16(value[7], value[8]);//---延迟时间---
            Short4CtrlObj2.usScOutTim = ConvertTools.Bytes2ToUInt16(value[9], value[10]);//---动作时间---
            Short4CtrlObj3.ucScOutAct = value[11];//---高低电平
            Short4CtrlObj3.usScOutDly = ConvertTools.Bytes2ToUInt16(value[12], value[13]);//---延迟时间---
            Short4CtrlObj3.usScOutTim = ConvertTools.Bytes2ToUInt16(value[14], value[15]);//---动作时间---
            Short4CtrlObj4.ucScOutAct = value[16];//---高低电平
            Short4CtrlObj4.usScOutDly = ConvertTools.Bytes2ToUInt16(value[17], value[18]);//---延迟时间---
            Short4CtrlObj4.usScOutTim = ConvertTools.Bytes2ToUInt16(value[19], value[20]);//---动作时间---
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
                UdpTools.ReplyDataUdp(data);//----回复确认-----
                this.CallbackUI(new CallbackParameter(ActionKind.ReadAdditionAciton,DeviceID));//---回调UI---
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

            byte len = 4+21;//---数据长度----
            byte[] crcData = new byte[10 + 1 + 5 * 4];//----1:第几个逻辑组,5:附加数据长度
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            crcData[10] = (byte)groupNum;
            Buffer.BlockCopy(Short4CtrlObj1.GetValue(), 0, crcData, 11, 5);//---短路输入1
            Buffer.BlockCopy(Short4CtrlObj2.GetValue(), 0, crcData, 16, 5);//---短路输入2
            Buffer.BlockCopy(Short4CtrlObj3.GetValue(), 0, crcData, 21, 5);//---短路输入3
            Buffer.BlockCopy(Short4CtrlObj4.GetValue(), 0, crcData, 26, 5);//---短路输入4

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
        public void ReadConfig()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_CONFIG, DeviceID, getConfig);//----注册回调---
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, DeviceID + AddressID, getWriteEnd2);//-----此处不用deviceID区别,避免冲突
            UdpData udpSend = createReadParameterUdp();
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort,
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

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
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
        /// 获取指示灯参数
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getConfigData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != this.DeviceID) return;//不是本设备ID不接收.
            UdpTools.ReplyDataUdp(data);//----回复确认-----  

            byte[] value = userData.Data;
            //------找出数据,并翻译------
            this.ShortConfigRoad1 = value[1];
            this.ShortConfigRoad2 = value[2];
            this.ShortConfigRoad3 = value[3];
            this.ShortConfigRoad4 = value[4];
            CallbackUI(new CallbackParameter(ActionKind.ReadConfig,DeviceID));//----读完状态信息,回调界面----
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getWriteEndData2(UdpData data, object[] values)
        {

            UserUdpData userData = new UserUdpData(data);
            byte[] cmd = new byte[] { userData.Data[0], userData.Data[1] };//----找出回调的命令-----
            if (userData.SourceID == DeviceID && CommonTools.BytesEuqals(cmd, DeviceConfig.CMD_LOGIC_WRITE_EXACTION))
            {
                UdpTools.ReplyDataUdp(data);//----回复确认-----
                this.CallbackUI(new CallbackParameter(ActionKind.ReadAdditionAciton,DeviceID));//---回调UI---
            }
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
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_CONFIG;//----用户命令-----
            byte len = 4 + 5;//---数据长度---- 

            byte[] crcData = new byte[10 + 5];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = 0;//保留
            crcData[11] = ShortConfigRoad1;//----配置1
            crcData[12] = ShortConfigRoad2;//----配置2
            crcData[13] = ShortConfigRoad3;//----配置3
            crcData[14] = ShortConfigRoad4;//----配置4
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 取消对应指令所有回调订阅
        /// </summary>
        public void RemoveRJ45Callback()
        {
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_STATE);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_LOGIC_WRITE_EXACTION);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_CONFIG);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_VER);
 
        }

        /// <summary>
        /// 获取附加值
        /// </summary>
        /// <returns></returns>
        public byte[] GetAdditionValue()
        {
            byte[] additionValue = new byte[21];//----第一个为逻辑组号,不用设置

       
            byte[] temp = Short4CtrlObj1.GetValue();
            Buffer.BlockCopy(temp, 0, additionValue, 1, 5);
            temp = Short4CtrlObj2.GetValue();
            Buffer.BlockCopy(temp, 0, additionValue, 6, 5);
            temp = Short4CtrlObj3.GetValue();
            Buffer.BlockCopy(temp, 0, additionValue, 11, 5);
            temp = Short4CtrlObj4.GetValue();
            Buffer.BlockCopy(temp, 0, additionValue, 16, 5);

            return additionValue;
        }
    }

}
