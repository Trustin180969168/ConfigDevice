using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class MenuList
    {
        private MySocket mySocket = MySocket.GetInstance();
        public Device device;//-----设备---
        public event CallbackUIAction OnCallbackUI_Action;   //----回调UI----
        public CallbackFromUDP callbackGetMenuData;//---回调获取指令----
        private CallbackFromUDP getWriteEnd;//----获取结束读取信息----
        private string ObjUuid = Guid.NewGuid().ToString();//唯一标识对象uuid

        public MenuList(Device value)
        {
            this.device = value;
            callbackGetMenuData = new CallbackFromUDP(getMenuData);
            getWriteEnd = new CallbackFromUDP(this.getWriteEndData);
        }


        /// <summary>
        /// 删除回调
        /// </summary>
        public void RemoveCallBack()
        {
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_MEMU_NAME);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, ObjUuid);
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
        /// 获取菜单数据
        /// </summary>
        public void ReadMenuData(int startNum, int endNum)
        {
            UdpData udpSend = createReadMenusUdp(startNum, endNum);
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_MEMU_NAME, callbackGetMenuData);//---注册回调----
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, ObjUuid, getWriteEnd);//---注册结束回调---
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackReadMenus), null);
        }
        private void callbackReadMenus(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取菜单失败!", udpReply.ReplyByte);
        }
        private UdpData createReadMenusUdp(int _startNum, int _endNum)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_MMSG_READ_MEMU_NAME;//----用户命令----- 
            byte len = 12;//---数据长度---- 

            byte startNum = (byte)_startNum;
            byte endNum = (byte)_endNum;
            byte[] crcData = new byte[10 + len - 4];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            byte[] byteStartNum = ConvertTools.GetByteFromUInt32(startNum);//开始菜单
            byte[] byteEndNum = ConvertTools.GetByteFromUInt32(endNum);//结束菜单
            Buffer.BlockCopy(byteStartNum, 0, crcData, 10, 4);
            Buffer.BlockCopy(byteEndNum, 0, crcData, 14, 4);
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
        public void getMenuData(UdpData data, object[] values)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.SourceID != this.device.DeviceID) return;//不是本设备ID不接收.
            UdpTools.ReplyDataUdp(data);//----回复确认----- 

            CallbackUI(new CallbackParameter(ActionKind.ReadMenu,this.device.DeviceID, new object[] { userData }));//----界面回调------
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
            if (userData.SourceID == device.DeviceID &&
                CommonTools.BytesEuqals(cmd, DeviceConfig.CMD_PUBLIC_WRITE_COMMAND))
            {
                UdpTools.ReplyDataUdp(data);//---回复确认----- 
            }
        }

        /// <summary>
        /// 保存/增加菜单
        /// </summary>
        /// <param name="groupIndex">命令组</param>
        /// <param name="num">命令序号</param>
        /// <param name="data">命令数据</param>
        public void SaveMenuData(MenuData menuData)
        {
            UdpData udpSend = createWriteMenuUdp(menuData);
            mySocket.SendData(udpSend, device.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult),
                new object[] { "保存第" + (menuData.MenuID).ToString() + "菜单失败!" });
        }
        private UdpData createWriteMenuUdp(MenuData menuData)
        {
            UdpData udp = new UdpData();
            byte[] value = menuData.GetByteData();
            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { device.ByteDeviceID, device.ByteNetworkId, device.ByteKindID };//----目标信息--
            byte[] source = new byte[] { device.BytePCAddress, device.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_MMSG_WRITE_MEMU_NAME;//----用户命令-----
            byte len = (byte)(value.Length + 4);//---数据长度----

            byte[] crcData = new byte[10 + value.Length];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len; 

            Buffer.BlockCopy(value, 0, crcData, 10, value.Length);//----命令数据-----
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
