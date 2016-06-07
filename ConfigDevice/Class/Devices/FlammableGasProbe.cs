using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
 
    public class FlammableGasProbe:Device
    {


        public string ValveState = "";//---阀门状态---
        public short ElectricCurrent = 0;//---电流---
        public string ProbeState = "";//---探头状态---
        public short Templatetrue = 0;//---温度----
        public Motor Valve;//电机对象,用于阀门控制
        public Circuit ProbeCircuit;//回路对象
        private CallbackFromUDP getStateInfo;//----获取设置信息----
        private CallbackFromUDP getWriteEnd;//----获取结束读取信息----
        public bool OpenValve = false;//是否开阀门
        public bool ClearLight = false;//是否关闭指示灯
        public bool ClearBuzzer = false;//是否关闭蜂鸣器

        public FlammableGasProbe(UserUdpData userUdpData)
            : base(userUdpData)
        { 
            initCallback();
            initControlObjs();
        }

        public FlammableGasProbe(DeviceData data)
            : base(data)
        { 
            initCallback();
            initControlObjs();
        }

        public FlammableGasProbe(DataRow dr)
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
            Valve = new Motor(this);
            ProbeCircuit =new Circuit(this,8);
            ContrlObjs.Add("阀门", Valve);
            ContrlObjs.Add("回路", ProbeCircuit);
            ProbeCircuit.OnCallbackUI_Action += this.CallbackUI;
        }

        /// <summary>
        /// 注册RJ45回调
        /// </summary>
        private void initCallback()
        {
            getStateInfo = new CallbackFromUDP(this.getStateInfoData);
            getWriteEnd = new CallbackFromUDP(this.getWriteEndData);
        }

        /// <summary>
        /// 申请探头状态
        /// </summary>
        public void ReadState()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_STATE, getStateInfo);
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END,getWriteEnd);
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
        /// 获取数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getStateInfoData(UdpData data, object[] values)
        {
            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
            UserUdpData userData = new UserUdpData(data);
            //------获取状态-----
            int temp = (int)userData.Data[0];
            switch (temp)
            {
                case 0: this.ValveState = Motor.STATE_VALVE_STOP; break;
                case 1: this.ValveState = Motor.STATE_VALVE_CLOSE; break;
                case 2: this.ValveState = Motor.STATE_VALVE_OPEN; break;
                case 3: this.ValveState = Motor.STATE_VALVE_TOTAL; break;
                default: this.ValveState = ""; break;
            }
            this.ElectricCurrent = ConvertTools.Bytes2ToInt(new byte[] { userData.Data[1], userData.Data[2] });
            if ((int)userData.Data[3] == 0)
                this.ProbeState = "正常";
            else
                this.ProbeState = "触发";
            this.Templatetrue = ConvertTools.Bytes2ToInt(new byte[] { userData.Data[4],userData.Data[5]});

            CallbackUI(new CallbackParameter( this.GetType().ToString()));//----回调界面----
        }


                /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getWriteEndData(UdpData data, object[] values)
        {
            UdpTools.ReplyDeviceDataUdp(data);//----回复确认-----
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_STATE); 
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END);
        }
    }

}
