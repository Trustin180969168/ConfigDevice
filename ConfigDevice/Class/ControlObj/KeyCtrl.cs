using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    /// <summary>
    /// 用于配置按键的控制的类
    /// </summary>
    public class KeyCtrl : ControlObj
    {
        private MySocket mySocket = MySocket.GetInstance();//----通讯---
        private CallbackFromUDP getKeyOption;//---注册回调按键----


        public KeyCtrl(Device device):base(device)
        {
            getKeyOption = new CallbackFromUDP(this.getKeyOptionData);
        }

        /// <summary>
        /// 申请读取按键参数
        /// </summary>
        public void ReadKeyOption()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME, this.UUID, getKeyOption);//----注册回调---            
            UdpData udpSend = createReadKeyOptionUdp();
            mySocket.SendData(udpSend, deviceControled.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadKeyOption), null);
        }
        private void callbackReadKeyOption(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取按键参数失败!", udpReply.ReplyByte);
        }
        private UdpData createReadKeyOptionUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { deviceControled.ByteDeviceID, deviceControled.ByteNetworkId, deviceControled.ByteKindID };//----目标信息--
            byte[] source = new byte[] { deviceControled.BytePCAddress, deviceControled.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_KB_READ_OPTIONS;//----用户命令-----
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
        /// 获取每路门窗名称
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void getKeyOptionData(UdpData data, object[] values)
        {
            //UserUdpData userData = new UserUdpData(data);
            //if (userData.SourceID != deviceControled.DeviceID) return;//不是本设备ID不接收.

            //UdpTools.ReplyDataUdp(data);//----回复确认-----
            //if (finishReadRoads == true) return;

            //byte[] byteName = CommonTools.CopyBytes(userData.Data, 4, userData.DataOfLength - 4 - 4);

            //int num = userData.Data[0];
            //string roadName = Encoding.GetEncoding("GB2312").GetString(byteName);
            //if (ListCircuitIDAndName.ContainsKey(num + 1)) ListCircuitIDAndName[num + 1] = roadName;
            //if (num + 1 == this.circuitCount)
            //    finishReadRoads = true;//---表示回路已经读取完毕----
        }


    }
}
