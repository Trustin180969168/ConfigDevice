using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class Cupboard : Device
    {
        public int ExclusionDeviceId;
          public Cupboard(UserUdpData userUdpData)
            : base(userUdpData)
        { 
            initCallback(); 
        }

        public Cupboard(DeviceData data)
            : base(data)
        { 
            initCallback(); 
        }

        public Cupboard(DataRow dr)
            : base(dr)
        { 
            initCallback(); 
        }
        private CallbackFromUDP getWriteEnd;//----获取结束读取信息----
        private CallbackFromUDP getConfig;//-------每参数名称----
        /// <summary>
        /// 注册RJ45回调
        /// </summary>
        private void initCallback()
        {
            RemoveRJ45Callback(); 
            getConfig = new CallbackFromUDP(this.getConfigData);//获取短路配置
            getWriteEnd = new CallbackFromUDP(this.getWriteEndData);//--结束读取---
        }

        /// <summary>
        /// 读参数
        /// </summary>
        public void ReadConfig()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_CONFIG, EditHandleID, getConfig);//----注册回调--- 
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, EditHandleID + DeviceConfig.CMD_PUBLIC_WRITE_CONFIG.ToString(), getWriteEnd);
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
            UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----  

            byte[] value = userData.Data;
            //------找出数据,并翻译------
            ExclusionDeviceId = value[1];
            CallbackUI(new CallbackParameter(ActionKind.ReadConfig, DeviceID));//----读完状态信息,回调界面----
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
            if (userData.SourceID == DeviceID && CommonTools.BytesEuqals(cmd, DeviceConfig.CMD_PUBLIC_WRITE_CONFIG))
            {
                UdpTools.ReplyDelRJ45SendUdp(data);//----回复确认-----
                CallbackUI(new CallbackParameter(ActionKind.ReadConfig, DeviceID));//----读完状态信息,回调界面----
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
            byte len = 4 + 4;//---数据长度---- 

            byte[] crcData = new byte[10 + 5];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = 0;//固化为0即可
            crcData[11] = (byte)ExclusionDeviceId;//设备id 
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
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_CONFIG); 

        }



    }
}
