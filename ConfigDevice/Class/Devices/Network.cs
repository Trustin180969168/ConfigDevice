using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Threading;

namespace ConfigDevice
{
    /*
     * 
     * 用户数据: 
        目标: FF FF FE	
        源头: FC 38 F0		-FC:设备ID(公共地址) 38:网段ID(公共网段) F0:PC类型
        分页: 11		
        命令: 81 FE	
        长度: 36（4+n）
        数据: 00 00 00 00 00 00 -保留
              33 FF D2 05 42 53 33 34 31 66 21 43 -物理ID
              00 00 -设备位置	      
              D0 C2 D1 F4 B9 E2 B7 BF 00 00 
              00 00 00 00 00 00 00 00 00 00
              00 00 00 00 00 00 00 00 00 00  -30个字节为设备名称
              7E F3 A0 A8 -CRC校验  
    * 
    */
    public class Position : IComparable<Position>
    {
        public const string DC_NUM = "Num";
        public const string DC_NAME = "Name";
        public const string DC_HAS_PASSWORD = "HasPassword";

        public int Num = 1;//位置编号,从1开始
        public string Name = "";//位置名称
        public bool HasPassword;//是否有密码

        public Position(int num, string name, bool has)
        {
            Num = num;
            Name = name;
            HasPassword = has;
        }
        public Position()
        {

        }

        #region IComparable<Position> 成员

        int IComparable<Position>.CompareTo(Position other)
        {
            if (other == null) return -1;
            if (this.Num < other.Num)
                return -1;
            else
                return 1;
        }

        #endregion
    }

    public class Network:Device
    {

        public string DeviceName = "";//设备名称

        public int Port;//对方的发送端口
        public List<Position> ListPosition; //设备位置列表
        private byte[] managerPassword;//管理员密码
        private byte[] userPassword;//用户密码
        public string DNS1 = "";//DNSIP地址1
        public string DNS2 = "";//DNSIP地址2
        public byte ByteNetworkID { get { return BitConverter.GetBytes(Convert.ToInt16(NetworkID))[0]; } }
        public DateTime RefreshTime;
        private CallbackFromUDP callbackGetPosition; 
        private CallbackFromUDP getWriteEnd;//----获取结束读取信息----
        /// <summary>
        /// 获取终端点
        /// </summary>
        /// <returns></returns>
        public EndPoint GetEndPoint()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(NetworkIP), Port);
            EndPoint remotePoint = (EndPoint)(ipep);
            return remotePoint;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Network(UserUdpData userUdpData) 
        {
            DeviceID = Convert.ToInt16(userUdpData.Source[0]).ToString();
            NetworkID = Convert.ToInt16(userUdpData.Source[1]).ToString();
            KindID = Convert.ToInt16(userUdpData.Source[2]).ToString();
            KindName = DeviceConfig.EQUIPMENT_ID_NAME[userUdpData.Source[2]];
            State = NetworkConfig.STATE_NOT_CONNECTED;
            //-------MAC地址---------
            byte[] byteMac = new Byte[12];
            Buffer.BlockCopy(userUdpData.Data, 6, byteMac, 0, 12);
            MAC = ConvertTools.ByteToHexStr(byteMac);
            //-------设备名称---------
            byte[] byteName = new Byte[30];
            Buffer.BlockCopy(userUdpData.Data, 20, byteName, 0, 30);
            DeviceName = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0').Replace(" ","").Replace("","");
            ListPosition = new List<Position>();

            getWriteEnd = new CallbackFromUDP(this.getWriteEndData);

            regeditRJ45Callback();
        }

        /*
        /// <summary>
        /// 构造函数
        /// </summary>
        public NetworkData(DataRow dr)
        {
            DeviceID = dr[NetworkConfig.DC_DEVICE_ID].ToString();
            NetworkID = dr[NetworkConfig.DC_NETWORK_ID].ToString();
            State = dr[NetworkConfig.DC_STATE].ToString();
            MacAddress = dr[NetworkConfig.DC_MAC].ToString();
            DeviceName = dr[NetworkConfig.DC_DEVICE_NAME].ToString();
            NetworkIP = dr[NetworkConfig.DC_IP].ToString();
            Port = Convert.ToInt16(dr[NetworkConfig.DC_PORT]);
            PCAddress = dr[NetworkConfig.DC_PC_ADDRESS].ToString();
            ListPosition = new List<Position>();

            regeditRJ45Callback();//-------注册RJ45回调------
        }

         */ 
         
        /// <summary>
        /// 注册RJ45回调
        /// </summary>
        private void regeditRJ45Callback()
        {
            callbackGetPosition = new CallbackFromUDP(callbackGetPositions);
 
          
        }

        /// <summary>
        /// 获取设备列表
        /// </summary>
        public void GetPositionList()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PC_WRITE_LOCALL_NAME, NetworkID, callbackGetPosition);//-----避免回调被覆盖或冲突,执行时先重新绑定一次----   
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END, NetworkID, getWriteEnd);//---注册结束回调---
            ListPosition.Clear();
            UdpData udpSend = createGetPositionListUdp();
            callbackGetPosition.Udp = udpSend;         
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackGetReply), null);
        }
        private void callbackGetReply(UdpData udpReply, object[] values)
        {
            if ( udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("获取设备列表失败!", udpReply.ReplyByte);//----错误则提示----
        }
        /// <summary>
        /// 创建获取设备列表包
        /// </summary>
        /// <returns></returns>
        private UdpData createGetPositionListUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类------
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性----
            udp.SendPort = SysConfig.ByteLocalPort;//--发送端口---
            udp.Protocol = UserProtocol.RJ45;//---用户协议-------

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkID, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PC_READ_LOCALL_NAME;//----用户命令-----
            byte len = 0x6;//---数据长度---
            byte startNum = 0;
            byte endNum = 0x1F;
            //--------添加到校验数据--------
            byte[] crcData = new byte[12];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = startNum;
            crcData[11] = endNum;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, 12);
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, 12, 4);
            Array.Resize(ref udp.ProtocolData, 16);//重新设定长度    
            udp.Length = 28 + 16 + 1;

            return udp;
        }

        /// <summary>
        /// 回调搜索地址列表
        /// </summary>
        /// <param name="udpPosition">地址包</param>
        /// <param name="values">参数组</param>
        private void callbackGetPositions(UdpData udpPosition, object[] values)
        {
            //-----------回复RJ45,已经获取了一个设备位置-------
            UserUdpData userData = new UserUdpData(udpPosition);
            if (userData.NetworkID != this.NetworkID) return;
            UdpTools.ReplyDataUdp(udpPosition);           
            byte value = userData.Data[0];//第一个字节
            bool numHas = (int)(value >> 7) == 1 ? true : false;//是否有密码
            int num = 0x7F & value + 1; //序号,从位置从1开始
            byte[] byteName = CommonTools.CopyBytes(userData.Data, 1, 12);//---名称---
            string PositionName = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0');
            Position pos = new Position(num, PositionName, numHas);

            if (ListPosition.Count < num + 1)//----有则修改,无则添加
            {
                ListPosition.Add(pos);
                try  { ListPosition.Sort(); }     catch { }
            }
            else
            {
                ListPosition[pos.Num].Name = pos.Name;
                ListPosition[pos.Num].HasPassword = pos.HasPassword;
            }
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
            if (userData.SourceID == this.NetworkID && CommonTools.BytesEuqals(cmd, DeviceConfig.CMD_PC_WRITE_LOCALL_NAME))
            {
                UdpTools.ReplyDataUdp(data);//----回复确认----- 
            }
        }

        /// <summary>
        /// 保存位置列表信息
        /// </summary>
        /// <param name="_position">位置</param>
        /// <param name="_name">名称</param>
        public void SavePositionList(Position pos)
        {
            UdpData udpSend = createSavePositionUdp(pos);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackGetSavePositionReply), new object[] {  pos });
        }
        private void callbackGetSavePositionReply(UdpData udpReply, object[] values)
        {
            Position pos = (Position)values[0]; 
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("保存位置- " + pos.Name + " 失败!", udpReply.ReplyByte);//----错误则提示---- 
            else
            {
                ListPosition[pos.Num - 1] = pos;
                this.CallbackUI(new CallbackParameter(ActionKind.SaveNetworkPosition,DeviceID, pos));//----返回界面结果----
            }
        }
        /// <summary>
        /// 创建获取修改位置包
        /// </summary>
        /// <returns></returns>
        private UdpData createSavePositionUdp(Position pos)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类------
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性----
            udp.SendPort = SysConfig.ByteLocalPort;//--发送端口---
            udp.Protocol = UserProtocol.RJ45;//---用户协议-------

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkID, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//-----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PC_WRITE_LOCALL_NAME;//----用户命令-----
            //---位置------
            byte num = (byte)(pos.Num - 1);
            if(pos.HasPassword)//--是否有密码----
                 num = (byte)(0x80 | num);
            //----------------名称---------
            byte[] tempName = Encoding.GetEncoding("GB2312").GetBytes(pos.Name);
            byte[] byteName = new byte[12];//----最多12个字节,不足补0
            Buffer.BlockCopy(tempName, 0, byteName, 0, tempName.Length);
            byte len = (byte)(1 + byteName.Length + 4);//---数据长度 = 地址1 + 名称长度12 + 校验码4 ---
            //---------添加到校验数据--------
            byte[] crcData = new byte[10 + 1 + byteName.Length];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = num;
            Buffer.BlockCopy(byteName, 0, crcData, 11, byteName.Length);
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

       

        /// <summary>
        /// 保存网络信息
        /// </summary>
        /// <param name="newName"></param>
        /// <param name="newIP"></param>
        /// <param name="gateWay"></param>
        /// <param name="mask"></param>
        /// <param name="_networkID"></param>
        public void SaveNetworkInfo(string newName, byte[] newIP, byte[] gateWay, byte[] mask, byte _networkID)
        {
            
        }


        /// <summary>
        /// 修改名称
        /// </summary>
        /// <param name="newName"></param>
        public void SaveNetworkName(string newName)
        {
            UdpData udpSend = createSaveNetworkNameUdp(newName);
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSaveNetworkName), new object[] { udpSend, newName });
        }
        private void callbackSaveNetworkName(UdpData udpReply, object[] values)
        {
            UdpData sendUdp = (UdpData)values[0];
            string newName = (string)values[1];
            if ((int)udpReply.ProtocolData[10] != 0)
                CommonTools.ShowReplyInfo("保存名称失败!", udpReply.ReplyByte);
            else
            {
                this.DeviceName = newName;
                SysConfig.ListNetworks[this.NetworkIP].DeviceName = newName;
                NetworkCtrl.UpdateNetworkDataTable(this);
                //CommonTools.MessageShow("保存成功!", 1, "");
            }
        }
        /// <summary>
        /// 保存网络名称
        /// </summary>
        /// <returns>返回UDP</returns>
        private UdpData createSaveNetworkNameUdp(string newName)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC, ByteKindID };//----目标信息--
            byte[] source = new byte[] { DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PC_CHANGENAME;//----用户命令-----
            byte[] position = new byte[] { 0, 0 };//设备位置
            //---------新名称-------------           
            byte[] value = Encoding.GetEncoding("GB2312").GetBytes(newName);
            byte[] byteName = new byte[30];
            Buffer.BlockCopy(value, 0, byteName, 0, value.Length);
            byte len = (byte)(4 + 12 + 2 + byteName.Length + 4);//4:管理员密码,12:MAC,2:位置,名称长度,4:校验码
            //--------添加到用户数据--------
            byte[] crcData = new byte[10 + 4 + 12 + 2 + byteName.Length];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            Buffer.BlockCopy(managerPassword, 0, crcData, 10, 4);
            Buffer.BlockCopy(ByteMacAddress, 0, crcData, 14, 12);
            Buffer.BlockCopy(position, 0, crcData, 26, 2);
            Buffer.BlockCopy(byteName, 0, crcData, 28, byteName.Length);
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------         

            //---------拼接到包中---------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 连接网络
        /// </summary>
        /// <network>链接的网络</network>
        /// <returns>返回数据表</returns>
        public void ConnectNetwork()
        {
            //----------判断是否已经连接--------
            if (this.State == NetworkConfig.STATE_CONNECTED)
                return;
            FrmNetworkPW frmPW = new FrmNetworkPW();
            string pw = ""; frmPW.NetworkName = DeviceName;
            if (frmPW.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                pw = frmPW.PassWord;
            else
                return;    
            //-----------执行链接网络------------
            UdpData udpSend = createConnectNetworkUdpData(pw);
            string str1 = pw.Substring(0, 1); string str2 = pw.Substring(1, 1);
            string str3 = pw.Substring(2, 1); string str4 = pw.Substring(3, 1);
            byte[] mangerPw = ConvertTools.StrToToHexByte(str2 + str1 + str4 + str3 + "FFFF");//---管理员密码
            byte[] userPw = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };//---用户密码---
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackConnectNetwork),
                new object[] { mangerPw, userPw });


        }
        /// <summary>
        /// 回调网络搜索
        /// </summary>
        /// <param name="udp">udp包</param>
        private void callbackConnectNetwork(UdpData udpReceive, object[] values)
        {
            lock (SysConfig.ListNetworks)
            {
                UserUdpData userData = new UserUdpData(udpReceive);
                if (CommonTools.BytesEuqals(userData.Command, DeviceConfig.CMD_PC_CONNECT_ACK))//---为连接成功-----
                {
                    byte result = userData.Data[0];
                    if (result == CONNECT_RESULT.NOT_ALLOW_CONNECT)
                    { CommonTools.MessageShow("密码错误!", 2, ""); return; }

                    string pcAddress = userData.Data[13].ToString();
                    PCAddress = pcAddress;
                    RefreshTime = DateTime.Now;
                    State = NetworkConfig.STATE_CONNECTED;
                    managerPassword = (byte[])(values[0]);
                    userPassword = (byte[])(values[1]);
                    NetworkCtrl.UpdateNetworkDataTable(this);//---更新列表信息------
                    int i =SysConfig.ListNetworks.Count;
                    GetPositionList(); //----------获取位置列表---------
                    DeviceCtrl.AddDeviceData(GetDeviceData());//---添加到设备数据----
                    CallbackUI(new CallbackParameter(ActionKind.ConnectNetowrk, DeviceID,this));
                    return;
                }
                else
                    return;
            }
        }
        /// <summary>
        /// 创建申请连接网络申请的UDP
        /// </summary>
        /// <param name="network">网络数据</param>
        /// <returns>UDP</returns>
        private UdpData createConnectNetworkUdpData(string pw)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = 0x01;//----包数据类------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----
            byte[] target;
            //if (ByteKindID == DeviceConfig.EQUIPMENT_SERVER)
            //    target = new byte[] { 0xFD, ByteNetworkID, ByteKindID };//----目标信息--;
            //else
                target = new byte[] { ByteDeviceID, ByteNetworkID, ByteKindID };//----目标信息--
            byte[] source = new byte[] { 0xFF, ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PC_CONNECT;//----用户命令-----
            byte len = 0x1A;//---数据长度---
            //---管理员密码---密码:1234 => 0x21,0x43,0xFF,0xFF
            string str1 = pw.Substring(0, 1); string str2 = pw.Substring(1, 1);
            string str3 = pw.Substring(2, 1); string str4 = pw.Substring(3, 1);
            byte[] mangerPw = ConvertTools.StrToToHexByte(str2 + str1 + str4 + str3 + "FFFF");
            byte[] userPw = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };//---用户密码---
            byte[] temp = new byte[] { 0x8F, 0x90 };//----保留----
            byte[] mac = ByteMacAddress; //---MAC----

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
        /// 断开网络
        /// </summary>
        /// <network>链接的网络</network>
        /// <returns>返回数据表</returns>
        public void DisconnectNetwork()
        {
            //----------判断是否已经连接--------
            if (State == NetworkConfig.STATE_NOT_CONNECTED)
                return;
            //-----------执行链接网络------------
            UdpData udpSend = createDisconnectNetworkUdpData();
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackDisconnectNetwork), null);

        }

        /// <summary>
        /// 回调断开网络
        /// </summary>
        /// <param name="udp">udp包</param>
        private void callbackDisconnectNetwork(UdpData udpReceive, object[] values)
        {
            lock (SysConfig.ListNetworks)
            {
                UserUdpData userData = new UserUdpData(udpReceive);
                if (CommonTools.BytesEuqals(userData.Command, DeviceConfig.CMD_PC_DISCONNECT))//---成功回复断开连接-----
                {
                    PCAddress = "";
                    State = NetworkConfig.STATE_NOT_CONNECTED;//---标记为未链接----
                    NetworkCtrl.UpdateNetworkDataTable(this);//---更新列表信息------
                    NetworkCtrl.RemoveNetworkDeviceData(this);//----移除设备数据-----
                    CallbackUI(new CallbackParameter(ActionKind.DisConnectNetwork,this.DeviceID,this ));
                    return;
                }
                else
                    return;
            }
        }

        /// <summary>
        /// 创建断开网络的UDP包
        /// </summary>
        /// <param name="network">网络</param>
        /// <returns>UDP包数据</returns>
        private UdpData createDisconnectNetworkUdpData()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkID, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PC_DISCONNECT;//----用户命令-----
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
        /// 改密码
        /// </summary>
        /// <param name="newPassword">新密码</param>
        /// <param name="newPassword">密码类型</param>
        public void ChangePassword(string newPassword,PasswordKind kind)
        {
            //-----------执行链接网络------------
            UdpData udpSend = createChangePasswordUdpData(newPassword,kind);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackChangePassword), null);
        }
        private void callbackChangePassword(UdpData udpReply, object[] values)
        {
            int result = (int)udpReply.ProtocolData[10];
            if (result == 0)
                CommonTools.MessageShow("密码修改成功!", 1, "");
            else
                CommonTools.MessageShow("密码修改失败!", 1, "");
        }
        /// <summary>
        /// 创建申请连接网络申请的UDP
        /// </summary>
        /// <param name="network">网络数据</param>
        /// <returns>UDP</returns>
        private UdpData createChangePasswordUdpData(string pw, PasswordKind kind)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = 0x01;//----包数据类------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC, ByteKindID };//----目标信息--
            byte[] source = new byte[] { DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PC_CHANGEPASSWORD;//----用户命令-----
            byte len = 0x20;//---数据长度---

            //---管理员密码---密码:1234 => 0x21,0x43,0xFF,0xFF
            string str1 = pw.Substring(0, 1); string str2 = pw.Substring(1, 1);
            string str3 = pw.Substring(2, 1); string str4 = pw.Substring(3, 1);
            byte[] mangerPw; byte[] userPw;
            if (kind == PasswordKind.Manager)
            {
                mangerPw = ConvertTools.StrToToHexByte(str2 + str1 + str4 + str3 + "FFFF");
                userPw = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };//---用户密码(0xFF代表忽略)---
            }
            else
            {
                mangerPw = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };//---(0xFF代表忽略)--- 
                userPw = ConvertTools.StrToToHexByte(str2 + str1 + str4 + str3 + "FFFF");
            }
            byte[] mac = ByteMacAddress; //---MAC----
            //--------添加到用户数据--------
            byte[] crcData = new byte[38];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            Buffer.BlockCopy(managerPassword, 0, crcData, 10, 4);
            Buffer.BlockCopy(userPassword, 0, crcData, 14, 4);
            Buffer.BlockCopy(mangerPw, 0, crcData, 18, 4);
            Buffer.BlockCopy(userPw, 0, crcData, 22, 4);
            Buffer.BlockCopy(mac, 0, crcData, 26, 12);

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;

        }

        /// <summary>
        /// 确认密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassword(string pw,PasswordKind kind)
        {
            //---管理员密码---密码:1234 => 0x21,0x43,0xFF,0xFF
            string str1 = pw.Substring(0, 1); string str2 = pw.Substring(1, 1);
            string str3 = pw.Substring(2, 1); string str4 = pw.Substring(3, 1);
            byte[] bytePw = ConvertTools.StrToToHexByte(str2 + str1 + str4 + str3 + "FFFF");

            if (kind == PasswordKind.Manager)
                return CommonTools.BytesEuqals(bytePw, managerPassword);
            else
                return CommonTools.BytesEuqals(bytePw, userPassword);
        }

        /// <summary>
        /// 主动发送刷新包
        /// </summary>
        public void RefreshConnection()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkID, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PC_CONNECTING;//----用户命令-----
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
        
            mySocket.SendData(udp, NetworkIP, SysConfig.RemotePort);
        }

        /// <summary>
        /// 保存网络参数
        /// </summary>
        public void SaveNetworkParameter(byte[] newIP, byte[] gateWay, byte[] mask, byte _networkID,byte[] dns1,byte[] dns2)
        {
            UdpData udpSend = createSaveNetworkParameter(newIP, gateWay, mask, _networkID,dns1,dns2);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackChangeParameter), 
                new object[] { udpSend, newIP, _networkID  });
        }
        private void callbackChangeParameter(UdpData udpReply, object[] values)
        {
            //  byte[] mac = CommonTools.CopyBytes(udpReply.ProtocolData, 11, 12);
            if (udpReply.ProtocolData[10] == REPLY_RESULT.PC_CHANGENET_ACK_TRUE)
            {
                string newNetworkID = Convert.ToInt16(values[2]).ToString();
                IPAddress newIP = new IPAddress(values[1] as byte[]);
                //-----同步网络ID------
                if (NetworkID != newNetworkID)
                {
                    this.NetworkID = newNetworkID;
                    this.SnycNetworkID();
                }
                //-----同步网络IP-----
                if (this.NetworkIP != newIP.ToString())
                {
                    SysConfig.ListNetworks.Remove(this.NetworkIP);//---删除旧网络对象---      
                    this.NetworkIP = newIP.ToString();
                    SysConfig.ListNetworks.Add(this.NetworkIP, this);//---重新添加到对象列表-- 
                }
                NetworkCtrl.UpdateNetworkDataTable(this);//---更新网络列表--
                
                //CommonTools.MessageShow("参数修改成功!", 1, "");
            }
            else
                CommonTools.MessageShow("参数修改失败!", 2, "");
        }
        /// <summary>
        /// 创建申请连接网络申请的UDP
        /// </summary>
        /// <param name="network">网络数据</param>
        /// <returns>UDP</returns>
        private UdpData createSaveNetworkParameter(byte[] newIP,byte[] gateWay, byte[] mask, byte _networkID,byte[] dns1,byte[] dns2)
        {
            UdpData udp = new UdpData();
        
            udp.PacketKind[0] = 0x01;//----包数据类------
            udp.PacketProperty[0] = BroadcastKind.Broadcast;//----包属性----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC, ByteKindID };//----目标信息--
            byte[] source = new byte[] { DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PC };//----源信息----
                 
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PC_CHANGENET;//----用户命令-----
            byte len = 0x2B;//---数据长度---
            byte[] temp = new byte[] { 0x47, 0x53 };    //----保留----      
            //--------添加到用户数据--------
            byte[] crcData = new byte[49];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            Buffer.BlockCopy(managerPassword, 0, crcData, 10, 4);
            Buffer.BlockCopy(newIP, 0, crcData, 14, 4);
            Buffer.BlockCopy(temp, 0, crcData, 18, 2);
            Buffer.BlockCopy(gateWay, 0, crcData, 20, 4);
            Buffer.BlockCopy(mask, 0, crcData, 24, 4);
            crcData[28] = _networkID;
            Buffer.BlockCopy(ByteMacAddress, 0, crcData, 29, 12);
            Buffer.BlockCopy(dns1, 0, crcData, 41, 4);
            Buffer.BlockCopy(dns2, 0, crcData, 45, 4);
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }


        /// <summary>
        /// 同步ID
        /// </summary>
        public void SnycNetworkID()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = 0x01;//----包数据类------
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议-----
            byte[] target = new byte[] { DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PC };//----源信息----

            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_NET_ID;//----用户命令-----
            byte len = 0x05;//---数据长度---
            //--------添加到用户数据--------
            byte[] crcData = new byte[11];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            crcData[10] = ByteNetworkID;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            mySocket.SendData(udp, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSyncID), null);
        }
        private void callbackSyncID(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("同步网络ID失败!", udpReply.ReplyByte);
            else
                CallbackUI(new CallbackParameter(ActionKind.SyncNetworkID, DeviceID,this ));
        }


        /// <summary>
        /// 同步时间
        /// </summary>
        public void SnycTime()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = 0x01;//----包数据类------
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议-----
            byte[] target = new byte[] { DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC, DeviceConfig.EQUIPMENT_PUBLIC };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//----源信息----

            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_TIME;//----用户命令-----
            byte len = 0x0B;//---数据长度---
            //--------添加到用户数据--------
            byte[] crcData = new byte[17];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            DateTime now = DateTime.Now;
            crcData[10] = (byte)now.Year;
            crcData[11] = (byte)now.Month;
            crcData[12] = (byte)now.Day;
            crcData[13] = (byte)now.DayOfWeek;
            crcData[14] = (byte)now.Hour;
            crcData[15] = (byte)now.Minute;
            crcData[16] = (byte)now.Second;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            mySocket.SendData(udp, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult), new object[] { "同步时间失败!" });

        }
        

        /// <summary>
        /// 同步数据
        /// </summary>
        public void SnycData()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = 0x01;//----包数据类------
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议-----
            byte[] target = new byte[] { ByteDeviceID, ByteNetworkID, DeviceConfig.EQUIPMENT_PUBLIC };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//----源信息----

            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_RESET_HOST;//----用户命令-----
            byte len = 0x0C;//---数据长度---
            //--------添加到用户数据--------
            byte[] crcData = new byte[11];
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

            mySocket.SendData(udp, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(UdpTools.CallbackRequestResult), new object[] { "同步数据失败!" });

        }


        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns></returns>
        public override DeviceData GetDeviceData()
        {
            DeviceData data = new DeviceData();

            data.DeviceID = DeviceID;
            data.NetworkID = NetworkID;
            data.KindID = KindID;
            data.KindName = KindName;
            data.Name = DeviceName;
            data.MAC = MAC;
            data.SoftwareVer = SoftwareVer;
            data.HardwareVer = HardwareVer;
            data.PCAddress = PCAddress;
            data.NetworkIP = NetworkIP;
            data.AddressName = "";
            data.State = State;
            data.Remark = Remark;

            return data;
        }

}















}
