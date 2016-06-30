using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Data;
using System.Collections;


namespace ConfigDevice
{
    public class NetworkList
    {
        public ThreadActionTimer RefreshConnectState;//计时器执行
        private long RefreshCount = 0;
        private MySocket mySocket = MySocket.GetInstance();
        public event CallbackUIAction CallBackUI = null;//返回UI
        private CallbackFromUDP callbackRefreshNetwork;//回调类

        public NetworkList()
        {
            NetworkCtrl.InitDataTableNetwork();
            //-------------PC主动定时发送刷新包----------
            RefreshConnectState = new ThreadActionTimer(3000, new Action(RefreshNetwork));
            RefreshConnectState.Start();
            //-------------RJ45主动刷新网络包的回调----------------
            callbackRefreshNetwork = new CallbackFromUDP(callbackRefreshNetworkData);
            callbackRefreshNetwork.ActionCount = long.MaxValue;
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PC_CONNECTING, callbackRefreshNetwork);
        }

 


        /// <summary>
        /// 搜索网络
        /// </summary>
        /// <returns>返回数据表</returns>
        public void SearchNetworks()
        {
            RefreshConnectState.Stop();//----先停止刷新----
            NetworkCtrl.InitDataTableNetwork();//----初始化列表-----
            //listNetworks.Clear();//----清空---
            //-----------执行搜索网络------------            
            UdpData udp = this.createSendUdpData();
            mySocket.SendData(udp, NetworkConfig.BROADCAST_IP, SysConfig.RemotePort, new CallbackUdpAction(callbackSearchNetworks), null);           
            RefreshConnectState.Start();//---继续启动刷新------
        }
        /// <summary>
        /// 回调网络搜索
        /// </summary>
        /// <param name="udp">udp包</param>
        private void callbackSearchNetworks(UdpData udp, object[] values)
        {
            lock (SysConfig.DtNetwork)//---PC反馈速度太快必须限制反馈线程,避免出错
            {
                //----排除重复项-------
                string temp = NetworkConfig.DC_IP + "='" + udp.IP + "'";
                if (SysConfig.DtNetwork.Select(temp).Length > 0)
                    return;

                UserUdpData userData = new UserUdpData(udp);//----用户协议数据---  
                Network network = new Network(userData);//----网络对象-----
                network.NetworkIP = udp.IPPoint.Address.ToString();
                network.Port = udp.IPPoint.Port;
                //------修改已经连接网络的状态----
                if (SysConfig.ListNetworks.ContainsKey(network.NetworkIP) && 
                    SysConfig.ListNetworks[network.NetworkIP].State == NetworkConfig.STATE_CONNECTED)      
                    network=  SysConfig.ListNetworks[network.NetworkIP];//----刷新数据-----
                //-----排查网段ID冲突------------------
                temp = NetworkConfig.DC_NETWORK_ID + "='" + network.NetworkID + "'";
                DataRow[] rows = SysConfig.DtNetwork.Select(temp);
                if (rows.Length > 0)
                {
                    foreach (DataRow dr in rows)
                        dr[NetworkConfig.DC_REMARK] = NetworkConfig.ERROR_SAME_NETWORKID;
                    network.Remark = NetworkConfig.ERROR_SAME_NETWORKID;
                }
                //-----排查网段名称冲突------------------
                //temp = NetworkConfig.DC_DEVICE_NAME + "='" + network.DeviceName + "'";
                //rows = SysConfig.DtNetwork.Select(temp);
                //if (rows.Length > 0)
                //{
                //    foreach (DataRow dr in rows)
                //        dr[NetworkConfig.DC_REMARK] += NetworkConfig.ERROR_SAME_NETWORKNAME;
                //    network.Remark += NetworkConfig.ERROR_SAME_NETWORKNAME;
                //}
                //------添加到数据表----------
                SysConfig.DtNetwork.Rows.Add(new object[] { network.DeviceID, network.NetworkID, network.State, 
                network.DeviceName, network.MAC,network.NetworkIP,network.Port.ToString(),network.Remark,"",network.KindName });
                SysConfig.DtNetwork.AcceptChanges();

                if (!SysConfig.ListNetworks.ContainsKey(network.NetworkIP))
                {
                    SysConfig.ListNetworks.Add(network.NetworkIP, network);
                    network.OnCallbackUI_Action += this.CallBackUI;
                }
                else //----更新网络列表---
                {
                    NetworkCtrl.UpdateNetworkDataTable(network);//----更新网络列表-----                    
                }

                
            //    CallBackUI(new CallbackParameter(null));
            }
        }


        /// <summary>
        /// 刷新网络
        /// </summary>
        /// <returns>返回数据表</returns>
        public void callbackRefreshNetworkData(UdpData udpReply, object[] values)
        {
            lock (SysConfig.ListNetworks)
            {
                RefreshCount++;
                UdpData udpAck = createReplyRefreshUdpData(udpReply);//----根据回复包,创建回复刷新包----
                mySocket.ReplyData(udpAck, udpReply.IP, SysConfig.RemotePort);//---回复刷新---  
            }
            foreach (Network network in SysConfig.ListNetworks.Values)
            {
                if (udpReply.IP == network.NetworkIP && network.State == NetworkConfig.STATE_CONNECTED )
                {
                    network.RefreshTime = DateTime.Now;//----更新时间-----     
                    //mySocket.RemoveRJ45SendList(udpReply.PacketCodeStr);//-----(PC不用回复,所以手动删除)回复后删除RJ45主动发送列表----
                    UdpTools.ReplyDataUdp(udpReply);
                    break;
                }
            }
        }
        /// <summary>
        /// 刷新网络,3秒检查一次,并发送保持链接包
        /// </summary>
        /// <returns>返回数据表</returns>
        public void RefreshNetwork()
        {            
            lock (SysConfig.ListNetworks)
            {
                foreach (Network network in SysConfig.ListNetworks.Values)
                {
                    if (network.State == NetworkConfig.STATE_CONNECTED)
                    {
                        if (network.RefreshTime.AddSeconds(NetworkConfig.CONNECT_TIME_OUT) < DateTime.Now)
                        {
                            setConnectState(network, NetworkConfig.STATE_NOT_CONNECTED);//---变更为未链接----   
                            NetworkCtrl.RemoveNetworkDeviceData(network);//----移除设备数据-----
                        }
                        else  //----PC主动刷新网络
                            network.RefreshConnection();
                    }
                }
            }
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="network"></param>
        private void setConnectState(Network network, string state)
        {
            network.State = state;//标记状态         
            foreach (DataRow dr in SysConfig.DtNetwork.Rows)
            {
                if (dr[NetworkConfig.DC_MAC].ToString() == network.MAC)
                {
                    dr[NetworkConfig.DC_STATE] = state;
                    dr.AcceptChanges();
                    break;
                }                
            }
        }






        /// <summary>
        /// 根据接收的包生成回复刷新的包
        /// </summary>
        /// <param name="udpReply">UdpData</param>
        /// <returns>udp</returns>
        private UdpData createReplyRefreshUdpData(UdpData udpReply)
        {
            UdpData udp = new UdpData(udpReply.GetUdpData());
            udp.PacketKind[0] =PackegeSendReply.REPLY;//---更新包标识----
            udp.SendPort = SysConfig.ByteLocalPort;//----发送端口---           
            Buffer.BlockCopy(udpReply.ProtocolData, 3, udp.ProtocolData, 0, 3);//----为接收udpReply的源----
            Buffer.BlockCopy(udpReply.ProtocolData, 0, udp.ProtocolData, 3, 3);//----为接收udpReply的目标----
            //-------计算校验码--------
            byte[] temp = new byte[udp.ProtocolData.Length -4];
            Buffer.BlockCopy(udp.ProtocolData, 0, temp, 0,udp.ProtocolData.Length - 4);
            byte[] crc = CRC32.GetCheckValue(temp);
            Buffer.BlockCopy(crc, 0,udp.ProtocolData, udp.ProtocolData.Length - 4, 4);
            
            return udp;
        }

        
        /// <summary>
        /// 创建一个发送网络搜索的Udp请求包
        /// </summary>
        /// <returns></returns>
        private UdpData createSendUdpData()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0]=0x01;//----包数据类------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----
            //---------1复制到用户数据---------
            byte[] temp1 = new byte[] { 0xFF, 0xFF, 0xF0, 0xFF, 0xFF, 0xFE, 0x11 };
            byte[] temp2 = new byte[] { 0x0C, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            //---------1.1计算校验码-------------
            byte[] temp3 = new byte[18];
            Buffer.BlockCopy(temp1, 0, temp3, 0, 7);
            Buffer.BlockCopy(DeviceConfig.CMD_PC_SEARCH, 0, temp3, 7, 2);//-----命令字节---
            Buffer.BlockCopy(temp2, 0, temp3, 9, 9);
            byte[] crcCode = CRC32.GetCheckValue(temp3);
            //---------1.2计算数据-------             
            Buffer.BlockCopy(temp3, 0, udp.ProtocolData, 0, 18);
            Buffer.BlockCopy(crcCode, 0, udp.ProtocolData, 18, 4);
            Array.Resize(ref udp.ProtocolData, 22);//重新设定长度            
            udp.Length = 28 + 22 + 1;//---------计算长度--------

            return udp;
        }

        /// <summary>
        /// 清空网络
        /// </summary>
        public void ClearNetwork()
        {
            //------断开所有连接网络-------
            foreach (Network network in SysConfig.ListNetworks.Values)
                if (network.State == NetworkConfig.STATE_CONNECTED) network.DisconnectNetwork();
            SysConfig.DtDevice.Clear(); SysConfig.DtDevice.AcceptChanges();
            SysConfig.DtNetwork.Clear(); SysConfig.DtNetwork.AcceptChanges();
            SysConfig.ListNetworks.Clear();
        }

    }
}
