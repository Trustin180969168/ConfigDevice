using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    /// <summary>
    /// 指示灯参数
    /// </summary>
    public class LightParameter
    {
        public bool OpenHealthLight = false; //是否开启空气健康指标指示灯
        
        public LightParameter()
        {
        }

        public LightParameter(bool openLight)
        {
            OpenHealthLight = openLight;
        }
    }

    /// <summary>
    /// 指示灯
    /// </summary>
    public class Light : ControlObj
    {
        public const string CLASS_NAME = "Light";
        public const int LEL_LEDACT_OFF = 0;        //【熄灭(全灭)】
        public const int LEL_LEDACT_ON = 1;        //【点亮(红灯)】
        public const int LEL_LEDACT_GLINT = 2;        //【闪烁(红灯)】
        public const int LEL_LEDACT_NOT = 3;        //【不动作】
        public const int LEL_LEDACT_TOTAL = 4;        //【总数】
        public const int LEL_LEDACT_DEFAULT = LEL_LEDACT_ON;

        public const string STATE_LEDACT_OFF = "熄灭";        //【熄灭(全灭)】
        public const string STATE_LEDACT_ON = "点亮";        //【点亮(红灯)】
        public const string STATE_LEDACT_GLINT = "闪烁";        //【闪烁(红灯)】
        public const string STATE_LEDACT_NONE = "不动作";        //【不动作】
        public const string STATE_LEDACT_GREEN_ON="绿灯点亮";//绿灯点亮
        public const string STATE_LEDACT_ORANGE_ON = "橙灯点亮";
        public const string STATE_LEDACT_RED_ON = "红灯点亮";
        public const string STATE_LEDACT_RED_GLINT = "红灯闪烁";




        public byte LedAct = 0;               //指示灯指示动作：熄灭、闪烁等   (如:LEL_LEDACT_OFF)
        public ushort LedTim = 0;               //指示灯指示时间：单位秒         (如:10->10秒，0->无限)
        private CallbackFromUDP getParameter;//-------每参数名称----
        private CallbackFromUDP getWriteEnd;//----获取结束读取信息----

        private bool openHealthLight = false; //是否开启空气健康指标指示灯
        public bool OpenHealthLight
        {
            get { return openHealthLight; }            
        }

        public Light(Device _deviceCtrl)
        {
            Name = "指示灯";
            deviceControled = _deviceCtrl;

            getParameter = new CallbackFromUDP(getParameterData);
            getWriteEnd = new CallbackFromUDP(getWriteEndData);
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
        /// 获取指示灯参数
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getParameterData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != deviceControled.DeviceID) return;//不是本设备ID不接收.

            UdpTools.ReplyDataUdp(data);//----回复确认-----
            //----翻译数据------------
            byte value = userData.Data[1];
            if ((int)(value & 1) == 1)
                openHealthLight = true;
            else
                openHealthLight = false;
            this.deviceControled.CallbackUI(new CallbackParameter(Light.CLASS_NAME));//---回调UI---
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
                UdpTools.ReplyDataUdp(data);//----回复确认-----
            
            }
        }


        /// <summary>
        /// 写参数
        /// </summary>
        public void WriteParameter(LightParameter value)
        {
            UdpData udpSend = createWriteParameterUdp(value);
            MySocket.GetInstance().SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort,
                new CallbackUdpAction(callbackWriteParameterUdp), new object[] { value });
        }
        private void callbackWriteParameterUdp(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("设置参数失败!", udpReply.ReplyByte);
            else
                openHealthLight = true;
        }
        private UdpData createWriteParameterUdp(LightParameter value)
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
            byte len = 4 + 2;//---数据长度---- 

            byte[] crcData = new byte[10 + 2];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = 0;//保留
            if (value.OpenHealthLight)
                crcData[11] = 1;

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
