using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class KeyList
    {
        private MySocket mySocket = MySocket.GetInstance();
        public Device device;//-----设备---
        public event CallbackUIAction OnCallbackUI_Action;      //---回调UI----
        public CallbackFromUDP callbackGetKeyData;              //---回调获取指令---- 
        private CallbackFromUDP finishGetData;                  //---完成数据读取----
        private string ObjUuid = Guid.NewGuid().ToString();     //---唯一标识对象uuid
        public KeyList(Device value)
        {
            this.device = value;
            callbackGetKeyData = new CallbackFromUDP(getKeyData);
            finishGetData = new CallbackFromUDP(finishReadData);
        }

        /// <summary>
        /// 回调UI
        /// </summary>
        /// <param name="values"></param>
        private void CallbackUI(CallbackParameter callbackParameter)
        {
            if (this.OnCallbackUI_Action != null)
                OnCallbackUI_Action(callbackParameter);
        }

        /// <summary>
        /// 申请读取按键配置
        /// </summary>
        /// <param name="startNum">按键/分组 开始=结束</param>
        public void ReadKeyData(int Num)
        {
            ReadKeyData(Num, Num);
        }
        /// <summary>
        /// 申请读取按键配置
        /// </summary>
        /// <param name="startNum">按键/分组 开始</param>
        /// <param name="endNum">按键/分组 结束</param>
        public void ReadKeyData(int startNum, int endNum)
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_CONFIG, callbackGetKeyData);  //---注册读取按键配置----
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, ObjUuid, finishGetData); //---注册读取按键配置完毕----
            UdpData udpSend = createReadKeyDataUdp(startNum, endNum);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadKeyData), null);
        }
        private void callbackReadKeyData(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取指令失败!", udpReply.ReplyByte);
        }
        private UdpData createReadKeyDataUdp(int _startNum, int _endNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_CONFIG;//----用户命令-----
            byte len = 0x06;//---数据长度---- 
            byte startNum = (byte)_startNum;
            byte endNum = (byte)_endNum;
            byte[] crcData = new byte[10 + 6 - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = startNum;
            crcData[11] = endNum;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        private void getKeyData(UdpData data, object[] values)
        {
            UdpTools.ReplyDataUdp(data);//----回复确认----- 
            UserUdpData userUdp = new UserUdpData(data);//---用户数据---
            KeyData keyData = new KeyData(userUdp);     //---按键数据---
            CallbackUI(new CallbackParameter(keyData));//----界面回调-----
        }

        /// <summary>
        /// 保存按键配置
        /// </summary>
        public void SaveKeyData(KeyData keyData)
        {
            UdpData udpSend = createSaveKeyDataUdp(keyData);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSaveKeyData), null);
        }
        private void callbackSaveKeyData(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("保存按键配置失败!", udpReply.ReplyByte);
        }
        private UdpData createSaveKeyDataUdp(KeyData keyData)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_CONFIG;//----用户命令-----
            byte len = 14+4;//---数据长度---- 
            byte byteKeyNum = keyData.KeyNum;//--按键号--
            //---------生成校验码-----------
            byte[] crcData = new byte[10+14];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            byte[] values = keyData.GetKeyDataValue();
            Buffer.BlockCopy(values, 2, crcData, 10, values.Length);
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }


        /// <summary>
        /// 完成读取 
        /// </summary>
        /// <param name="data">数据包</param>
        /// <param name="values"></param>
        private void finishReadData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            byte[] cmd = new byte[] { userData.Data[0], userData.Data[1] };//----找出回调的命令-----
            if (userData.SourceID == device.DeviceID && CommonTools.BytesEuqals(cmd, DeviceConfig.CMD_PUBLIC_WRITE_CONFIG))//不是本设备ID不接收.
            {
                UdpTools.ReplyDataUdp(data);//----回复确认-----  
                SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, ObjUuid);//---移除按键---
                SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_CONFIG);//移除读取配置
            }
        }



    }
}
