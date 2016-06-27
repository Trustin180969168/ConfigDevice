using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{ 
    public class Environment:Device
    {
        public const string CLASS_NAME = "FlammableGasProbe";
        
        public Circuit ProbeCircuit;                //回路对象
        public Light FGP_Light;                     //指示灯

        private CallbackFromUDP getStateInfo;       //----获取设置信息---- 
        private CallbackFromUDP getAdditionLogic;   //----获取附加逻辑信息---- 
        private CallbackFromUDP getWriteEnd;        //----获取结束读取信息----
        //----传感器状态对象----
        public TemperatureSensor temperatureSensor; //--温度----
        public HumiditySensor humiditySensor;       //---湿度---
        public LuminanceSensor luminanceSensor;     //---亮度----
        public AQISensor AQISensor;                 //---空气质量----
        public TVOCSensor TVOCSensor;               //---有害气体----
        public CO2Sensor CO2Sensor;                 //---二氧化碳-----
        public CH2OSensor CH2OSensor;               //---甲醛----
        public PM25Sensor PM25Sensor;               //---PM2.5----
        public O2Sensor O2Sensor;                   //---氧气-----

        public Environment(UserUdpData userUdpData)
            : base(userUdpData)
        { 
            initCallback();
            initControlObjs();
        }

        public Environment(DeviceData data)
            : base(data)
        { 
            initCallback();
            initControlObjs();
        }

        public Environment(DataRow dr)
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

            FGP_Light = new Light(this);
            ProbeCircuit = new Circuit(this,8);            

            ContrlObjs.Add("回路", ProbeCircuit);
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
                CommonTools.ShowReplyInfo("申请读取探头状态失败!", udpReply.ReplyByte);
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
            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
            UserUdpData userData = new UserUdpData(data);
       
            //------找出数据段------
            string dataStr = ConvertTools.ByteToHexStr(userData.Data);
            string dataStr1 = dataStr.Split(new string[] { "FF FF" }, StringSplitOptions.RemoveEmptyEntries)[0];
            string dataStr2 = dataStr.Split(new string[] { "FF FF" }, StringSplitOptions.RemoveEmptyEntries)[1];
            byte[] dataByte1 = ConvertTools.StrToToHexByte(dataStr1);
            byte[] dataByte2 = ConvertTools.StrToToHexByte(dataStr2);
             //----各个传感器状态-----
            this.temperatureSensor = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_TEMP, dataByte1) as TemperatureSensor;//获取温度
            this.humiditySensor = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_HUMI, dataByte1) as HumiditySensor;//获取湿度
            this.luminanceSensor = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_LUMI, dataByte1) as LuminanceSensor;//亮度
            this.AQISensor = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_AQI, dataByte1) as AQISensor;//空气质量
            this.TVOCSensor = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_TVOC, dataByte1) as TVOCSensor;//有害气体
            this.CO2Sensor = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_CO2, dataByte1) as CO2Sensor;//二氧化碳
            this.CH2OSensor = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_CH2O, dataByte1) as CH2OSensor;//甲醛
            this.PM25Sensor = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_PM25, dataByte1) as PM25Sensor;//PM2.5
            this.O2Sensor = SensorCtrl.GetSensorFromByte(SensorConfig.LG_SENSOR_O2, dataByte1) as O2Sensor;//氧气浓度
         
            CallbackUI(new CallbackParameter(FlammableGasProbe.CLASS_NAME));//----读完状态信息,回调界面----
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
            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
            UserUdpData userData = new UserUdpData(data);
            byte[] value = userData.Data;
            SetAdditionLogicData(value);
        }

        /// <summary>
        /// 设置附加动作
        /// </summary>
        /// <param name="value"></param>
        public void SetAdditionLogicData(byte[] value)
        {

            this.FGP_Light.LedAct = value[4];//动作类型
            this.FGP_Light.LedTim = ConvertTools.Bytes2ToUInt16(new byte[] { value[5], value[6] });//动作时间
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
                UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
                this.CallbackUI(new CallbackParameter(FlammableGasProbe.CLASS_NAME));//---回调UI---
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
            byte len = 14;//---数据长度----
            byte[] crcData = new byte[10 + 10];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            crcData[10] = (byte)groupNum;
            byte[] byteSeconds = new byte[2];
            Buffer.BlockCopy(byteSeconds, 0, crcData, 12, 2);
            crcData[14] = (byte)this.FGP_Light.LedAct;//----指示灯---
            byteSeconds = ConvertTools.GetByteFromUInt16(FGP_Light.LedTim);
            Buffer.BlockCopy(byteSeconds, 0, crcData, 15, 2);
            Buffer.BlockCopy(byteSeconds, 0, crcData, 18, 2);

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
            byte[] byteSeconds = new byte[2];

            Buffer.BlockCopy(byteSeconds, 0, additionValue, 2, 2);
            additionValue[4] = (byte)this.FGP_Light.LedAct;//----指示灯---
            byteSeconds = ConvertTools.GetByteFromUInt16(FGP_Light.LedTim);
            Buffer.BlockCopy(byteSeconds, 0, additionValue, 5, 2);

            Buffer.BlockCopy(byteSeconds, 0, additionValue, 8, 2);

            return additionValue;
        }
    }

}
