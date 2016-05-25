using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class DeviceCtrl
    {
        private Network SearchingNetwork;//---搜素的网络设备(RJ45)----
        private MySocket mySocket = MySocket.GetInstance();
        private int countNum = 0;
        public event CallbackUIAction CallBackUI = null;//返回
        private CallbackFromUDP callbackGetSearchDevices;
        private CallbackFromUDP callbackGetStopSearchDevices;
        private static object objLock = new object();
        private bool searching = false;//是否正在搜索设备,避免与其它界面冲突
        public DeviceCtrl()
        {
            callbackGetSearchDevices = new CallbackFromUDP(this.getDevices);
            callbackGetStopSearchDevices = new CallbackFromUDP(this.callbackStopSearch);
            callbackGetSearchDevices.ActionCount = long.MaxValue;//--回调次数--
            callbackGetStopSearchDevices.ActionCount = long.MaxValue;//--回调次数--
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_INF, callbackGetSearchDevices);//---回调设备-----
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_STOP_SEARCH, callbackGetStopSearchDevices);//---回调停止搜索----

        }

        /// <summary>
        /// 初始化设备数据
        /// </summary>
        private void initDataTableDevices()
        {
            if (SysConfig.DtDevice.Rows.Count == 0)
                countNum = 0;
            //----初始化状态--------------
            string temp = DeviceConfig.DC_NETWORK_ID + " = '" + SearchingNetwork.NetworkID + "' ";
            DataRow[] rows = SysConfig.DtDevice.Select(temp);
            foreach (DataRow dr in rows)
            {
                dr[DeviceConfig.DC_STATE] = DeviceConfig.STATE_ERROR;
            }
            SysConfig.DtDevice.AcceptChanges();//---初始化数据----
        }

        /// <summary>
        /// 回调界面
        /// </summary>
        private void callbackUI(object[] values)
        {
            if (CallBackUI != null)
                CallBackUI(values);
        }

        /// <summary>
        /// 搜索设备
        /// </summary>
        /// <param name="network">搜索设备</param>
        public void SearchDevices(Network network)
        {
            searching = true;
            this.SearchingNetwork = network;
            initDataTableDevices();//----初始化列表-----
            //-----------执行搜索设备------------            
            UdpData udp = this.createSearchDevices(network);
            callbackGetSearchDevices.Udp = udp;
            MySocket.GetInstance().SendData(udp, network.NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSearchDevices), null);
        }
        /// <summary>
        /// 回调设备搜索
        /// </summary>
        /// <param name="udp">udp包</param>
        private void callbackSearchDevices(UdpData udp, object[] values)
        {
            UserUdpData userData = new UserUdpData(udp);
            if (!CommonTools.BytesEuqals(userData.Command, DeviceConfig.CMD_PUBLIC_RET_START_SEARCH) ||
                userData.Data[0] != DeviceConfig.RETSTARTSEARCH_TRUE)//----RJ45应答可以,则启动搜索设备-----
                CommonTools.MessageShow("搜索设备失败!", 1, "");
        }
        /// <summary>
        /// 创建搜索设备命令包
        /// </summary>
        /// <param name="network">网络</param>
        /// <returns>UDP包</returns>
        private UdpData createSearchDevices(Network network)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { DeviceConfig.EQUIPMENT_PC, network.ByteNetworkID, DeviceConfig.EQUIPMENT_PUBLIC };//----目标信息--
            byte[] source = new byte[] { network.BytePCAddress, network.ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_START_SEARCH;//----用户命令-----
            byte len = 0x06;//---数据长度---

            //--------添加到用户数据--------
            byte[] userData = new byte[12];
            Buffer.BlockCopy(target, 0, userData, 0, 3);
            Buffer.BlockCopy(source, 0, userData, 3, 3);
            userData[6] = page;
            Buffer.BlockCopy(cmd, 0, userData, 7, 2);
            userData[9] = len;
            userData[10] = 0x00;//起始地址
            userData[11] = 0xFF;//结束地址
            byte[] crc = CRC32.GetCheckValue(userData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(userData, 0, udp.ProtocolData, 0, 12);
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, 12, 4);
            Array.Resize(ref udp.ProtocolData, 16);//重新设定长度    
            udp.Length = 28 + 16 + 1;

            return udp;

        }
        /// <summary>
        /// 获取设备
        /// </summary>
        private void getDevices(UdpData data, object[] values)
        {
            lock (objLock)
            {
                if (!searching)
                    return;
                int num = 0; bool find = false;
                //-----获取数据-----
                UserUdpData userData = new UserUdpData(data);
                DeviceData deviceData = new DeviceData(userData);
                //-----回复反馈的设备信息-------
                UdpData udpReply = createReplyUdp(data);
                UdpTools.ReplyDeviceDataUdp(data);
                //-----排查重复项-------
                string temp = DeviceConfig.DC_MAC + "='" + deviceData.MAC + "'";
                DataRow[] rows = SysConfig.DtDevice.Select(temp);
                if (rows.Length > 0)
                    find = true;
                else
                    num = ++countNum;
                //-------排查ID冲突------------------
                temp = DeviceConfig.DC_ID + " = '" + deviceData.DeviceID + "' and " + DeviceConfig.DC_NETWORK_ID + " = '" + deviceData.NetworkID + "' " +
                   " and " + DeviceConfig.DC_MAC + " <> '" + deviceData.MAC + "' ";
                rows = SysConfig.DtDevice.Select(temp);
                if (rows.Length > 0)
                {
                    foreach (DataRow dr in rows)
                    {
                        if (!dr[DeviceConfig.DC_REMARK].ToString().Contains(DeviceConfig.ERROR_SAME_DEVICE_ID))
                        {
                            dr[DeviceConfig.DC_REMARK] = DeviceConfig.ERROR_SAME_DEVICE_ID;//其他标识冲突
                            dr[DeviceConfig.DC_STATE] = DeviceConfig.STATE_ERROR;//其他标识冲突
                        }
                    }
                    deviceData.State = DeviceConfig.STATE_ERROR;//自身标识冲突
                    deviceData.Remark = DeviceConfig.ERROR_SAME_DEVICE_ID;//自身标识冲突
                }
                //-----排查名称冲突------------------
                temp = DeviceConfig.DC_NAME + "='" + deviceData.Name + "'"  + " and " + DeviceConfig.DC_MAC + " <> '" + deviceData.MAC + "' "
                    +" and " + DeviceConfig.DC_NETWORK_ID + " = '" + deviceData.NetworkID + "' ";
                rows = SysConfig.DtDevice.Select(temp);
                if (rows.Length > 0)
                {
                    foreach (DataRow dr in rows)
                    {
                        if (!dr[DeviceConfig.DC_REMARK].ToString().Contains(DeviceConfig.ERROR_SAME_DEVICE_TITLE))
                        {
                            dr[DeviceConfig.DC_REMARK] += DeviceConfig.ERROR_SAME_DEVICE_TITLE;
                            dr[DeviceConfig.DC_STATE] = DeviceConfig.STATE_ERROR;
                        }
                    }
                    deviceData.State = DeviceConfig.STATE_ERROR;
                    deviceData.Remark += DeviceConfig.ERROR_SAME_DEVICE_TITLE;
                }
                //-----排查网络ID异常-----------------
                if (deviceData.NetworkID != SearchingNetwork.NetworkID)
                {
                    deviceData.State = DeviceConfig.STATE_ERROR;
                    deviceData.Remark += DeviceConfig.ERROR_SAME_DEVICE_NETWORK_ID;
                }
                //------添加到数据表----------     
                if (!find)
                    //SysConfig.DtDevice.Rows.Add(new object[] {num.ToString(),device.DeviceID,device.NetworkID,
                    //        device.KindID, device.KindName,device.Name,device.MAC,device.State,device.Remark,"","",device.PCAddress,
                    //        device.NetworkIP,device.AddressName});
                    SysCtrl.AddDeviceData(deviceData);
                else
                    SysCtrl.UpdateDeviceData(deviceData);

            }
        }




        /// <summary>
        /// 监听设备停止
        /// </summary>
        private void callbackStopSearch(UdpData data, object[] values)
        {
            searching = false;//---搜索完毕----
            //------回复停止搜索-------               
            UdpData udpReply = createReplyUdp(data);
            mySocket.ReplyData(udpReply, data.IP, SysConfig.RemotePort);
            if (CallBackUI != null) 
                CallBackUI(new object[] { ActionKind.SearchDevice,SearchingNetwork });
        }


        /// <summary>
        /// 根据设备发送包生成回复包
        /// </summary>
        /// <param name="udpDevice">发送的包</param>
        /// <returns>设备回复包</returns>
        private UdpData createReplyUdp(UdpData udpDevice)
        {
            //---udpDevice---41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 00 00 35 00 01 55 CB 2B 02 00 00 00 C9 5C FE 3E 5C 13 11 82 FF 1B 56 FF 71 06 49 86 51 52 32 14 18 87 07 00 B5 DA B0 CB D0 D0 D6 D0 00 0F 6A 92 C2 50 
            //---udpReply----41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 00 00 35 00 02 42 1C 25 02 00 00 00 55 5C 
            //---udpDevice---41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 00 00 0D 00 01 55 CB 2B 02 00 00 00 C9 5C FE FC 5C F0 11 32 FF 04 26 70 0F 0C 48 
            //---udpReply----41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 00 00 0D 00 02 42 1C 25 02 00 00 00 55 5C 
            UdpData udpReply = new UdpData();
            udpReply.PacketCode = udpDevice.PacketCode;
            udpReply.PacketKind[0] = PackegeSendReply.REPLY;
            udpReply.PacketProperty[0] = BroadcastKind.Broadcast;
            udpReply.SendPort = SysConfig.ByteLocalPort;
            udpReply.Protocol = UserProtocol.Device;
            udpReply.ProtocolData = new byte[] { BroadcastKind.Unicast };
            udpReply.CheckCodeAdd[0] = udpDevice.ProtocolData[1];
            udpReply.Length = 30;
            return udpReply;
        }

        public void ClearDevices()
        {
            SysConfig.DtDevice.Clear();
            SysConfig.DtDevice.AcceptChanges();
        }

    }
}
