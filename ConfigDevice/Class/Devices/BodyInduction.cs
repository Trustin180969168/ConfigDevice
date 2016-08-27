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
        private CallbackFromUDP getConfig;//-------获取设备配置---- 
        public UWSensor UWSensor1;//-----超声波1
        public UWSensor UWSensor2;//-----超声波2
        public IRSensor IRSensor;//------红外
        public Circuit Circuit;//----回路对象---

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
            UWSensor1 = new UWSensor(this.GetDeviceData());
            UWSensor2 = new UWSensor(this.GetDeviceData());
            IRSensor = new IRSensor(this.GetDeviceData());
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            Light = new BodyInductionLight(this);
            Circuit = new Circuit(this,16);            

            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, Circuit);
        }

        /// <summary>
        /// 注册RJ45回调
        /// </summary>
        private void initCallback()
        {
            RemoveRJ45Callback();
            getConfig = new CallbackFromUDP(getParameterData); 
        }

        /// <summary>
        /// 取消对应指令所有回调订阅
        /// </summary>
        public void RemoveRJ45Callback()
        {
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PRI_WRITE_CONFIG);  

        }


        /// <summary>
        /// 读参数
        /// </summary>
        public void ReadConfig()
        { 
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PRI_WRITE_CONFIG,  getConfig);//----注册回调--- 
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
        /// 获取指示灯参数
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getParameterData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != DeviceID) return;//不是本设备ID不接收.

            UdpTools.ReplyDataUdp(data);//----回复确认-----
            //----传感器ID------------
            UWSensor1.Sensitivity =  userData.Data[0];
            UWSensor2.Sensitivity =  userData.Data[1];
            IRSensor.Sensitivity = userData.Data[2];

            this.CallbackUI(new CallbackParameter(this.GetType().Name, ActionKind.ReadConfig));//---回调UI---
   
        }


        /// <summary>
        /// 写参数
        /// </summary>
        public void WriteParameter(LightParameter value)
        {
            UdpData udpSend = createWriteParameterUdp(value);
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort,
                new CallbackUdpAction(callbackWriteParameterUdp), new object[] { value });
        }
        private void callbackWriteParameterUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("设置参数失败!", udpReply.ReplyByte);

        }
        private UdpData createWriteParameterUdp(LightParameter value)
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
            crcData[11] = UWSensor2.Sensitivity;
            crcData[12] = IRSensor.Sensitivity;

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
