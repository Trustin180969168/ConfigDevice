using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
 
    public class Radar:Device
    {
        public const string CLASS_NAME = "Radar";
        public const string ACTION_ADDITION = "Addition";//---附加动作回调
        public const string ACTION_SAFE = "Safe";//---安全配置回调
        public const string ACTION_STATE = "State";//---状态---

        private byte[] securityLevel = new byte[4];//----安全级别------
        public bool[] SaftFlags  =  new bool[] { false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false };//---安防标志位------
        public Circuit Circuit;//回路对象
        private CallbackFromUDP getStateInfo;//----获取设置信息---- 
        private CallbackFromUDP getAdditionLogic;//----获取附加逻辑信息---- 
        private CallbackFromUDP getWriteEnd;//----获取结束读取信息----
        private CallbackFromUDP getSafeSetting;//---获取安全配置----

        public RadarSensor RadarSensorObj;//----雷达----
        public SwitTamperSensor SwitTamperObj;//--防拆开关----
        public Buzzer Buzzer;//蜂鸣器
        public Light Light;//指示灯

        public Radar(UserUdpData userUdpData)
            : base(userUdpData)
        { 
            initCallback();
            initControlObjs();
        }

        public Radar(DeviceData data)
            : base(data)
        { 
            initCallback();
            initControlObjs();
        }

        public Radar(DataRow dr)
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
            Buzzer = new Buzzer(this);
            Light = new Light(this);
            Circuit = new Circuit(this,16);            

            ContrlObjs.Add("回路", Circuit);
        }

        /// <summary>
        /// 注册RJ45回调
        /// </summary>
        private void initCallback()
        {
            RemoveRJ45Callback();
            getStateInfo = new CallbackFromUDP(this.getStateInfoData);
            getAdditionLogic = new CallbackFromUDP(this.getAdditionLogicData);
            getWriteEnd = new CallbackFromUDP(this.getWriteEndData);
            getSafeSetting = new CallbackFromUDP(this.getSafeSettingData);
        }

        /// <summary>
        /// 申请探头状态
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
            UdpTools.ReplyDataUdp(data);//----回复确认-----
            UserUdpData userData = new UserUdpData(data);
       
            //------找出数据段------
            string dataStr = ConvertTools.ByteToHexStr(userData.Data);
            string dataStr1 = dataStr.Split(new string[] { "FF FF" }, StringSplitOptions.RemoveEmptyEntries)[0];

            byte[] dataByte1 = ConvertTools.StrToToHexByte(dataStr1);
            this.RadarSensorObj = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_RSP, dataByte1) as RadarSensor;//获取雷达
            this.SwitTamperObj = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_TAMPER, dataByte1) as SwitTamperSensor;//防拆开关
 
            CallbackUI(new CallbackParameter(Radar.CLASS_NAME,ACTION_STATE));//----读完状态信息,回调界面----
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
            UdpTools.ReplyDataUdp(data);//----回复确认-----
            UserUdpData userData = new UserUdpData(data);
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

            CallbackUI(new CallbackParameter(Radar.CLASS_NAME, ACTION_SAFE));//----读完状态信息,回调界面----
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
            this.Light.LedAct = value[1];//动作类型
            this.Light.LedTim = ConvertTools.Bytes2ToUInt16(new byte[] { value[2], value[3] });//动作时间
            this.Buzzer.BuzAct = value[4];//动作类型
            this.Buzzer.BuzTim = ConvertTools.Bytes2ToUInt16(new byte[] { value[5], value[6] });//动作时间
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
                this.CallbackUI(new CallbackParameter(Radar.CLASS_NAME,ACTION_ADDITION));//---回调UI---
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
            crcData[11] = (byte)this.Light.LedAct;//----指示灯---
            byte[] byteSeconds = ConvertTools.GetByteFromUInt16(Light.LedTim);
            Buffer.BlockCopy(byteSeconds, 0, crcData, 12, 2);
            crcData[14] = (byte)this.Buzzer.BuzAct;//----蜂鸣器---
            byteSeconds = ConvertTools.GetByteFromUInt16(this.Buzzer.BuzTim);
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
        /// 取消对应指令所有回调订阅
        /// </summary>
        public void RemoveRJ45Callback()
        {
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_STATE);
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
            byte[] additionValue = new byte[10];



            return additionValue;
        }
    }

}
