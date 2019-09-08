using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    /// <summary>
    /// 传感器
    /// </summary>
    public abstract class Sensor
    {
        private SensorStateData sensorData;//----传感器状态

        public SensorStateData SensorData
        {
            get { return sensorData; }
            set { sensorData = value; }
        }
        public abstract void SetSensorStateData(byte[] data);//----设置传感器状态数据
        public DeviceData DeviceData;//设备数据
        public byte Sensitivity;//灵敏度
        protected MySocket mySocket = MySocket.GetInstance();
        protected Int16 code = 0;//传感器编号

        protected Int16 Code
        {
            get { return code; } 
        }

        /// <summary>
        /// 开启测试
        /// </summary>
        /// <param name="code"></param>
        public virtual void OpenTest(int code)
        {
            test(true);
        }

        /// <summary>
        /// 关闭测试
        /// </summary>
        /// <param name="code"></param>
        public virtual void CloseTest(int code)
        {
            test(false);
        }

        /// <summary>
        /// 测试
        /// </summary>
        private  void test(bool openTest)
        {
            UdpData udpSend = createTestUdp(openTest);
            mySocket.SendData(udpSend, DeviceData.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackTestData), null);
        }
        private void callbackTestData(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("测试失败!", udpReply.ReplyByte);
        }
        private UdpData createTestUdp(bool openTest)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { DeviceData.ByteDeviceID, DeviceData.ByteNetworkId, DeviceData.ByteKindID };//----目标信息--
            byte[] source = new byte[] { DeviceData.BytePCAddress, DeviceData.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PRI_TEST;//----用户命令-----
            byte len = 6;//---数据长度---- 
            byte byteGroupNum = (byte)(Convert.ToInt16(code));//--组号--

            //---------生成校验码-----------
            byte[] crcData = new byte[10 + 2];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = (byte)(code);     //传感器编号(0~2)
            crcData[11] = (byte)(openTest ? 1 : 0);     //  开1，关0 
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }
    }


    /// <summary>
    /// 超声波类1
    /// </summary>
    public class UWSensor1 : Sensor
    {
        public UWSensor1(DeviceData deviceData,Int16 _code):base()
        {
            code = _code;
            this.DeviceData = deviceData; 
        }

        public UWSensor1(Int16 _code)
            : base()
        {
            code = _code;
            SensorData = new UWSensorData();
        }

        public override void SetSensorStateData(byte[] data)
        {
            SensorData = new UWSensorData(data); 
        }

    }

    /// <summary>
    /// 超声波类2
    /// </summary>
    public class UWSensor2 : Sensor
    {
        public UWSensor2(DeviceData deviceData, Int16 _code)
            : base()
        {
            code = _code;
            this.DeviceData = deviceData;
        }

        public UWSensor2(Int16 _code)
            : base()
        {
            code = _code;
            SensorData = new UWSensorData();
        }

        public override void SetSensorStateData(byte[] data)
        {
            SensorData = new UWSensorData(data);
        }

    }


    /// <summary>
    /// 红外线类
    /// </summary>
    public class IRSensor : Sensor
    {
        public LuminanceSensorData LumSensorData;

        public IRSensor(DeviceData deviceData, Int16 _code)
            : base()
        {
            code = _code;
            this.DeviceData = deviceData;
            SensorData = new IRSensorData();
            LumSensorData = new LuminanceSensorData();
        }

        public IRSensor(Int16 _code)
            : base()
        {
            code=_code;
            SensorData = new IRSensorData();
            LumSensorData = new LuminanceSensorData();
        }

        public override void SetSensorStateData(byte[] data)
        {
            SensorData = new IRSensorData(data);
            LumSensorData = new LuminanceSensorData(data);
        }
 
    }





}
