using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Data;
using System.Collections;


namespace ConfigDevice
{
    public class NetworkCtrl
    {
        public ThreadActionTimer RefreshConnectState;//计时器执行
        private long RefreshCount = 0;
        private MySocket mySocket = MySocket.GetInstance();
        private static object objLock = new object();//----刷新网络跟断开网络同样具有删除网络功能,避免线程冲突---
        private int AvaliableSize = 43;
        public CallBackUIAction CallBackUI = null;//返回UI
        private CallbackFromUdp callbackRefreshNetwork;//回调类

        public NetworkCtrl()
        {
            initDataTableNetwork();
            //-------------PC主动定时发送刷新包----------
            RefreshConnectState = new ThreadActionTimer(3000, new Action(RefreshNetwork));
            RefreshConnectState.Start();
            //-------------RJ45主动刷新网络包的回调----------------
            callbackRefreshNetwork = new CallbackFromUdp();
            callbackRefreshNetwork.CallBackAction += callbackRefreshNetworkData;
            SysConfig.AddRJ45CallBackList(NetworkConfig.CMD_PC_CONNECTING, callbackRefreshNetwork);
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
                NetworkData network = new NetworkData(userData);//----网络对象-----
                network.NetworkIP = udp.IPPoint.Address.ToString();
                network.Port = udp.IPPoint.Port;
                //------修改已经连接网络的状态----
                foreach (NetworkData connectNetwork in SysConfig.ListConnectedNetworks.Values)
                {
                    if (network.MacAddress == connectNetwork.MacAddress)
                        network.State = NetworkConfig.DC_CONNECTED;
                }
                //-----排查网段ID冲突------------------
                temp = NetworkConfig.DC_NETWORK_ID + "='" + network.NetworkID + "'";
                DataRow[] rows = SysConfig.DtNetwork.Select(temp);
                if (rows.Length > 0)
                {
                    foreach (DataRow dr in rows)
                        dr[NetworkConfig.DC_REMARK] = NetworkConfig.ERROR_SAME_NETWORKID;
                    network.Remark =NetworkConfig.ERROR_SAME_NETWORKID;
                }

                //------添加到数据表----------
                SysConfig.DtNetwork.Rows.Add(new object[] { network.DeviceID, network.NetworkID, network.State, 
                network.DeviceName, network.MacAddress,network.NetworkIP,network.Port.ToString(),network.Remark });

                SysConfig.DtNetwork.AcceptChanges();
                CallBackUI(null);
            }
        }

        /// <summary>
        /// 搜索网络
        /// </summary>
        /// <returns>返回数据表</returns>
        public void SearchNetworks()
        {
            RefreshConnectState.Stop();//----先停止刷新----
            initDataTableNetwork();//----初始化列表-----
            //listNetworks.Clear();//----清空---
            //-----------执行搜索网络------------            
            UdpData udp = this.createSendUdpData();
            mySocket.SendData(udp, NetworkConfig.BROADCAST_IP, SysConfig.RemotePort, new CallBackUdpAction(callbackSearchNetworks), null);           
            RefreshConnectState.Start();//---继续启动刷新------
        }

        /// <summary>
        /// 初始化网络数据
        /// </summary>
        private static void initDataTableNetwork()
        {
            //----初始化表结构-------
            if (SysConfig.DtNetwork.Columns.Count == 0)
            {
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_DEVICE_ID, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_NETWORK_ID, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_STATE, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_DEVICE_NAME, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_MAC, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_IP, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_PORT, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_REMARK, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_PC_ADDRESS, System.Type.GetType("System.String"));
            }
            SysConfig.DtNetwork.Clear(); SysConfig.DtNetwork.AcceptChanges();//---初始化数据----
        }

        /// <summary>
        /// 刷新网络
        /// </summary>
        /// <returns>返回数据表</returns>
        public void callbackRefreshNetworkData(UdpData udpReply, object[] values)
        {
            lock (SysConfig.ListConnectedNetworks)
            {
                RefreshCount++;
                UdpData udpAck = createReplyRefreshUdpData(udpReply);//----根据回复包,创建回复刷新包----
                mySocket.ReplyData(udpAck, udpReply.IP, SysConfig.RemotePort);//---回复刷新---  
            }
            foreach (NetworkData network in SysConfig.ListConnectedNetworks.Values)
            {
                if (udpReply.IP == network.NetworkIP)
                {
                    network.RefreshTime = DateTime.Now;//----更新时间-----     
                    mySocket.RemoveRJ45SendList(udpReply.PacketCodeStr);//-----回复后删除RJ45主动发送列表----
                    break;
                }
            }
        }

        /// <summary>
        /// 刷新网络
        /// </summary>
        /// <returns>返回数据表</returns>
        public void RefreshNetwork()
        {            
            lock (SysConfig.ListConnectedNetworks)
            {
                //-------移除超时连接---------
                ArrayList akeys = new ArrayList(SysConfig.ListConnectedNetworks.Keys);
                for (int i = 0; i < akeys.Count; i++)
                {
                    NetworkData network = SysConfig.ListConnectedNetworks[(string)akeys[i]];
                    if (network.RefreshTime.AddSeconds(NetworkConfig.CONNECT_TIME_OUT) < DateTime.Now)
                    {
                        setConnectState(network, NetworkConfig.DC_NOT_CONNECTED);//---变更为未链接----
                        SysConfig.ListConnectedNetworks.Remove(network.NetworkID);
                        i++;
                    }
                    else
                    {
                        UdpData udp = createRefrashNetworkUdpData(network);
                        mySocket.SendData(udp, network.NetworkIP, SysConfig.RemotePort);
                    }
                }

            }

        }

        /// <summary>
        /// 找出一个返回刷新的包
        /// </summary>
        private UdpData findRefreshUdp(NetworkData network)
        {
            //---------RJ45包会有多界面,多线程的操作情况,RJ45主动包的所有操作都必须加锁,避免数据不一致性
            lock (mySocket.RJ45SendList)
            {
                UdpData result = null;
                foreach (UdpData udp in mySocket.RJ45SendList.Values)
                {
                    if (udp.Length < AvaliableSize) continue;
                    UserUdpData userData = new UserUdpData(udp);
                    if (userData.IP == network.NetworkIP && CommonTools.BytesEuqals(userData.Command, NetworkConfig.CMD_PC_CONNECTING))
                    {
                        result = udp;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 连接网络
        /// </summary>
        /// <network>链接的网络</network>
        /// <returns>返回数据表</returns>
        public void ConnectNetwork(NetworkData network)
        {            
            //----------判断是否已经连接--------
            if (SysConfig.ListConnectedNetworks.ContainsKey(network.NetworkID))         
                return;
            FrmNetworkPW frmPW = new FrmNetworkPW();
            string pw = ""; frmPW.NetworkName = network.DeviceName;
            if (frmPW.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                pw = frmPW.PassWord;
            else
                return;
            //-----------执行链接网络------------
            UdpData udpSend = createConnectNetworkUdpData(network, pw);
            mySocket.SendData(udpSend, network.NetworkIP, SysConfig.RemotePort, new CallBackUdpAction(callbackConnectNetwork), new object[] { network }); 
            
        }
        /// <summary>
        /// 回调网络搜索
        /// </summary>
        /// <param name="udp">udp包</param>
        private void callbackConnectNetwork(UdpData udpReceive,object[] values)
        {
            lock (SysConfig.ListConnectedNetworks)
            {
                //string temp = ConvertTools.ByteToHexStr(udpReceive.GetUdpData());
                UserUdpData userData = new UserUdpData(udpReceive);
                if (CommonTools.BytesEuqals(userData.Command, NetworkConfig.CMD_PC_CONNECT_ACK))//---为连接成功-----
                {
                    byte result = userData.Data[0];
                    if (result == CONNECT_RESULT.NOT_ALLOW_CONNECT)
                    { CommonTools.MessageShow("密码错误!", 2, ""); return; }

                    NetworkData network = (NetworkData)values[0];

                    string pcAddress = userData.Data[13].ToString();
                    network.PCAddress = pcAddress;
                    network.RefreshTime = DateTime.Now;
                    network.State = NetworkConfig.DC_CONNECTED;
                    network.GetPositionList(); //----------获取位置列表---------
                    updateNetworkDataTable(network);//---更新列表信息------
                    SysConfig.ListConnectedNetworks.Add(network.NetworkID, network);//保存到连接列表----                  

                    return;
                }
                else
                    return;
            }
        }


        /// <summary>
        /// 断开网络
        /// </summary>
        /// <network>链接的网络</network>
        /// <returns>返回数据表</returns>
        public void DisconnectNetwork(NetworkData network)
        {
            //----------判断是否已经连接--------
            if (!SysConfig.ListConnectedNetworks.ContainsKey(network.NetworkID))
                return;
            //-----------执行链接网络------------
            UdpData udpSend = createDisconnectNetworkUdpData(network);
            mySocket.SendData(udpSend, network.NetworkIP, SysConfig.RemotePort, new CallBackUdpAction(callbackDisconnectNetwork), new object[] { network });

        }
        /// <summary>
        /// 创建断开网络的UDP包
        /// </summary>
        /// <param name="network">网络</param>
        /// <returns>UDP包数据</returns>
        private UdpData createDisconnectNetworkUdpData(NetworkData network)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { network.ByteDeviceId, network.ByteNetworkId, DeviceConfig.EQUIPMENT_RJ45};//----目标信息--
            byte[] source = new byte[] { network.BytePCAddress, network.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = NetworkConfig.CMD_PC_DISCONNECT;//----用户命令-----
            byte len = 0x04;//---数据长度---

            //--------添加到用户数据--------
            byte[] userData = new byte[10];
            Buffer.BlockCopy(target, 0, userData, 0, 3);
            Buffer.BlockCopy(source, 0, userData, 3, 3);
            userData[6] = page;
            Buffer.BlockCopy(cmd, 0, userData, 7, 2);
            userData[9] = len;
            byte[] crc = CRC32.GetCheckValue(userData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(userData, 0, udp.ProtocolData, 0, 10);
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, 10, 4);
            Array.Resize(ref udp.ProtocolData, 14);//重新设定长度    
            udp.Length = 28 + 14 + 1;

            return udp;

        }


        /// <summary>
        /// 回调断开网络
        /// </summary>
        /// <param name="udp">udp包</param>
        private void callbackDisconnectNetwork(UdpData udpReceive, object[] values)
        {
            lock (SysConfig.ListConnectedNetworks)
            {
                UserUdpData userData = new UserUdpData(udpReceive);
                if (CommonTools.BytesEuqals(userData.Command, NetworkConfig.CMD_PC_DISCONNECT))//---成功回复断开连接-----
                {
                    NetworkData network = (NetworkData)values[0];
                    network.PCAddress = "";
                    network.State = NetworkConfig.DC_NOT_CONNECTED;//---标记为未链接----
                    updateNetworkDataTable(network);//---更新列表信息------
                    SysConfig.ListConnectedNetworks.Remove(network.NetworkID);//保存到连接列表----

                    return;
                }
                else
                    return;
            }
        }


        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="network"></param>
        private void setConnectState(NetworkData network, string state)
        {
            network.State = NetworkConfig.DC_CONNECTED;//标记状态         
            foreach (DataRow dr in SysConfig.DtNetwork.Rows)
            {
                if (dr[NetworkConfig.DC_MAC].ToString() == network.MacAddress)
                {
                    dr[NetworkConfig.DC_STATE] = state;
                    dr.AcceptChanges();
                    break;
                }                
            }
        }

        /// <summary>
        /// 根据数据更新网络设备表
        /// </summary>
        /// <param name="network">RJ45</param>
        public void updateNetworkDataTable(NetworkData network)
        {
            foreach (DataRow dr in SysConfig.DtNetwork.Rows)
            {
                if (dr[NetworkConfig.DC_MAC].ToString() == network.MacAddress)
                {
                    dr[NetworkConfig.DC_DEVICE_ID] = network.DeviceID;
                    dr[NetworkConfig.DC_NETWORK_ID] = network.NetworkID;
                    dr[NetworkConfig.DC_STATE] = network.State;
                    dr[NetworkConfig.DC_DEVICE_NAME] = network.DeviceName;
                    dr[NetworkConfig.DC_PORT] = network.Port;
                    dr[NetworkConfig.DC_IP] = network.NetworkIP;
                    dr[NetworkConfig.DC_PC_ADDRESS] = network.PCAddress;

                    dr.AcceptChanges();
                    break;
                }
            }
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="network"></param>
        private void setPCAddress(NetworkData network, string pcAddress)
        {
            network.State = NetworkConfig.DC_CONNECTED;//标记状态         
            foreach (DataRow dr in SysConfig.DtNetwork.Rows)
            {
                if (dr[NetworkConfig.DC_MAC].ToString() == network.MacAddress)
                {
                    dr[NetworkConfig.DC_PC_ADDRESS] = pcAddress;
                    dr.AcceptChanges();
                    break;
                }                
            }
        }

        /// <summary>
        /// 根据设备ID获取地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private string getRemoteIP_Str(string deviceID)
        {
            string localIP_Str = SysConfig.LocalIP.ToString();
            int index = localIP_Str.LastIndexOf(".");
            string tempStr = localIP_Str.Substring(0, index + 1 );
            return tempStr + deviceID;
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
            udp.SendPort = SysConfig.LOCAL_PORT;//----发送端口---           
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
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----
            //---------1复制到用户数据---------
            byte[] temp1 = new byte[] { 0xFF, 0xFF, 0xF0, 0xFF, 0xFF, 0xFE, 0x11 };
            byte[] temp2 = new byte[] { 0x0C, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            //---------1.1计算校验码-------------
            byte[] temp3 = new byte[18];
            Buffer.BlockCopy(temp1, 0, temp3, 0, 7);
            Buffer.BlockCopy(NetworkConfig.CMD_PC_SEARCH, 0, temp3, 7, 2);//-----命令字节---
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
        /// 创建申请连接网络申请的UDP
        /// </summary>
        /// <param name="network">网络数据</param>
        /// <returns>UDP</returns>
        private UdpData createConnectNetworkUdpData(NetworkData network, string pw)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = 0x01;//----包数据类------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性----
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { network.ByteDeviceId, network.ByteNetworkId, DeviceConfig.EQUIPMENT_RJ45 };//----目标信息--
            byte[] source = new byte[] { 0xFF, network.ByteNetworkId, DeviceConfig.EQUIPMENT_PC};//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = NetworkConfig.CMD_PC_CONNECT;//----用户命令-----
            byte len = 0x1A;//---数据长度---
            //---管理员密码---密码:1234 => 0x21,0x43,0xFF,0xFF
            string str1 =   pw.Substring(0, 1);            string str2 =  pw.Substring(1, 1);
            string str3 =   pw.Substring(2, 1);            string str4 =  pw.Substring(3, 1);
            byte[] mangerPw = ConvertTools.StrToToHexByte(str2 + str1 + str4 + str3 + "FFFF");
            byte[] userPw = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };//---用户密码---
            byte[] temp = new byte[] { 0x8F, 0x90 };//----保留----
            byte[] mac = network.ByteMacAddress; //---MAC----

            //--------添加到用户数据--------
            byte[] userData = new byte[32];
            Buffer.BlockCopy(target, 0, userData, 0, 3);
            Buffer.BlockCopy(source, 0, userData, 3, 3);
            userData[6] = page;
            Buffer.BlockCopy(cmd, 0, userData, 7, 2);
            userData[9] = len;
            Buffer.BlockCopy(mangerPw, 0, userData, 10, 4);
            Buffer.BlockCopy(userPw, 0, userData, 14, 4);
            Buffer.BlockCopy(temp, 0, userData, 18, 2);
            Buffer.BlockCopy(mac, 0, userData, 20, 12);

            byte[] crc = CRC32.GetCheckValue(userData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(userData, 0, udp.ProtocolData, 0, 32);
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, 32, 4);
            Array.Resize(ref udp.ProtocolData, 36);//重新设定长度    
            udp.Length = 28 + 36 + 1;

            return udp;

        }


        /// <summary>
        /// 创建刷新网络的UDP包
        /// </summary>
        /// <param name="network">网络数据</param>
        /// <returns>UDP</returns>
        private UdpData createRefrashNetworkUdpData(NetworkData network)
        {            
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { network.ByteDeviceId, network.ByteNetworkId, DeviceConfig.EQUIPMENT_RJ45 };//----目标信息--
            byte[] source = new byte[] { network.BytePCAddress, network.ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = NetworkConfig.CMD_PC_CONNECTING;//----用户命令-----
            byte len = 0x04;//---数据长度---

            //--------添加到用户数据--------
            byte[] userData = new byte[10];
            Buffer.BlockCopy(target, 0, userData, 0, 3);
            Buffer.BlockCopy(source, 0, userData, 3, 3);
            userData[6] = page;
            Buffer.BlockCopy(cmd, 0, userData, 7, 2);
            userData[9] = len;
            byte[] crc = CRC32.GetCheckValue(userData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(userData, 0, udp.ProtocolData, 0, 10);
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, 10, 4);
            Array.Resize(ref udp.ProtocolData, 14);//重新设定长度    
            udp.Length = 28 + 14 + 1;            

            return udp;

        }

    }
}
