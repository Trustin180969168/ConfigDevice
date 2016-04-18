using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;

namespace ConfigDevice
{
    /*
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
            if (this.Num < other.Num)
                return -1;
            else
                return 1;
        }

        #endregion
    }

    public class NetworkData
    {
        public string DeviceID = "";//设备ID
        public string NetworkID = "";//网络ID
        public string State = "";//连接状态
        public string DeviceName = "";//设备名称
        public string MacAddress = "";//物理地址
        public string NetworkIP = "";//网络设备RJ45的IP
        public string PCAddress = "";//网络设备RJ45的PC通信地址
        public string Remark = "";//备注
        public int Port;//对方的发送端口
        public List<Position> ListPosition; //设备位置列表
        private MySocket mySocket = MySocket.GetInstance();
        private byte[] managerPassword;//管理员密码
        private byte[] userPassword;//用户密码

        public byte BytePCAddress { get { return BitConverter.GetBytes(Convert.ToInt16(PCAddress))[0]; } }
        public byte ByteDeviceId { get { return BitConverter.GetBytes(Convert.ToInt16(DeviceID))[0]; } }
        public byte ByteNetworkId { get { return BitConverter.GetBytes(Convert.ToInt16(NetworkID))[0]; } }
        public byte[] ByteMacAddress { get { return ConvertTools.StrToToHexByte(MacAddress); } }

        public DateTime RefreshTime;
        private CallbackFromUdp callbackGetPosition;
        private CallbackFromUdp callbackGetVer;
        public string SoftwareVer = "";//软件版本
        public string HardwareVer = "";//硬件版本
        public CallbackUIAction CallbackUI;
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
        public NetworkData(UserUdpData userUdpData)
        {
            DeviceID = Convert.ToInt16(userUdpData.Source[0]).ToString();
            NetworkID = Convert.ToInt16(userUdpData.Source[1]).ToString();
            State = NetworkConfig.STATE_NOT_CONNECTED;
            //-------MAC地址---------
            byte[] byteMac = new Byte[12];
            Buffer.BlockCopy(userUdpData.Data, 6, byteMac, 0, 12);
            MacAddress = ConvertTools.ByteToHexStr(byteMac);
            //-------设备名称---------
            byte[] byteName = new Byte[30];
            Buffer.BlockCopy(userUdpData.Data, 20, byteName, 0, 30);
            DeviceName = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0');//.Replace("网关:","");
            ListPosition = new List<Position>();

            regeditRJ45Callback();
        }

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

        /// <summary>
        /// 注册RJ45回调
        /// </summary>
        private void regeditRJ45Callback()
        {
            callbackGetPosition = new CallbackFromUdp(callbackGetPositions);
          callbackGetVer = new CallbackFromUdp(getVer);
        }


        /// <summary>
        /// 获取设备列表
        /// </summary>
        public void GetPositionList()
        {
            ListPosition.Clear();
            SysConfig.AddRJ45CallBackList(NetworkConfig.CMD_PC_WRITE_LOCALL_NAME, callbackGetPosition);//-----避免回调被覆盖或冲突,执行时先重新绑定一次----   
            UdpData udpSend = createGetPositionListUdp();
            callbackGetPosition.Udp = udpSend;         
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackGetReply), new object[] { udpSend });
        }
        private void callbackGetReply(UdpData udpReply, object[] values)
        {
            UdpData udpSend = (UdpData)values[0];
            if (udpSend.PacketCodeStr == udpReply.PacketCodeStr && udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
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
            udp.SendPort = SysConfig.LOCAL_PORT;//--发送端口---
            udp.Protocol = UserProtocol.RJ45;//---用户协议-------

            byte[] target = new byte[] { ByteDeviceId, ByteNetworkId, DeviceConfig.EQUIPMENT_RJ45 };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = NetworkConfig.CMD_PC_READ_LOCALL_NAME;//----用户命令-----
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
            UdpData udpReply = UdpTools.CreateDeviceReplyUdp(udpPosition);
            mySocket.ReplyData(udpReply, udpPosition.IP, SysConfig.RemotePort);

            UserUdpData userData = new UserUdpData(udpPosition);
            byte value = userData.Data[0];//第一个字节
            bool numHas = (int)(value >> 7) == 1 ? true : false;//是否有密码
            int num = 0x7F & value + 1; //序号,从位置从1开始
            byte[] byteName = CommonTools.CopyBytes(userData.Data, 1, 12);//---名称---
            string PositionName = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0');
            Position pos = new Position(num, PositionName, numHas);
            if (ListPosition.Count < num + 1)//----有则修改,无则添加
            { ListPosition.Add(pos); ListPosition.Sort(); }
            else
            {
                ListPosition[pos.Num].Name = pos.Name;
                ListPosition[pos.Num].HasPassword = pos.HasPassword;
            }            
        }

        /// <summary>
        /// 保存位置列表信息
        /// </summary>
        /// <param name="_position">位置</param>
        /// <param name="_name">名称</param>
        public void SavePositionList(Position pos, CallbackUIAction callbackUI)
        {
            UdpData udpSend = createSavePositionUdp(pos);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackGetSavePositionReply), new object[] { udpSend, pos, callbackUI });
        }
        private void callbackGetSavePositionReply(UdpData udpReply, object[] values)
        {
            UdpData udpSend = (UdpData)values[0];
            Position pos = (Position)values[1];
            CallbackUIAction callbackUI = (CallbackUIAction)values[2];
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("保存位置- " + pos.Name + " 失败!", udpReply.ReplyByte);//----错误则提示---- 
            else
            {
                ListPosition[pos.Num - 1] = pos;
                callbackUI(new object[] { pos });//----返回界面结果----
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
            udp.SendPort = SysConfig.LOCAL_PORT;//--发送端口---
            udp.Protocol = UserProtocol.RJ45;//---用户协议-------

            byte[] target = new byte[] { ByteDeviceId, ByteNetworkId, DeviceConfig.EQUIPMENT_RJ45 };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//-----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = NetworkConfig.CMD_PC_WRITE_LOCALL_NAME;//----用户命令-----
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
        /// 获取版本号
        /// </summary>
        public void SearchVer()
        {
            SysConfig.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_VER, callbackGetVer);//-----避免回调被覆盖或冲突,执行时先重新绑定一次----   
            UdpData udpSearch = createSearchVerUdp();
            MySocket.GetInstance().SendData(udpSearch, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSearchVer), new object[] { udpSearch });
        }
        private void callbackSearchVer(UdpData udpReply, object[] values)
        {
            UdpData sendUdp = (UdpData)values[0];
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("获取版本号失败!", udpReply.ReplyByte);
        }
        private void getVer(UdpData data, object[] values)
        {
            //-----获取数据-----
            UserUdpData userData = new UserUdpData(data);
            DeviceData device = new DeviceData(userData);
            if (DeviceID == device.DeviceID)
            {
                //-----获取版本号------                
                byte[] temp1 = new byte[20]; byte[] temp2 = new byte[20];
                Buffer.BlockCopy(userData.Data, 0, temp1, 0, 20);
                SoftwareVer = Encoding.GetEncoding("ASCII").GetString(temp1).TrimEnd('\0');
                Buffer.BlockCopy(userData.Data, 20, temp2, 0, 20);
                HardwareVer = Encoding.GetEncoding("ASCII").GetString(temp2).TrimEnd('\0');

                //------回复反馈的设备信息-------
                UdpData udpReply = UdpTools.CreateDeviceReplyUdp(data);
                mySocket.ReplyData(udpReply, data.IP, SysConfig.RemotePort);
            }
            else
            {     //------回复反馈的设备信息-------
                UdpData udpReply = UdpTools.CreateDeviceReplyUdp(data);
                mySocket.ReplyData(udpReply, data.IP, SysConfig.RemotePort);
            }
            if (CallbackUI != null)
                CallbackUI(null);

        }
        /// <summary>
        /// 创建读取VER的UDP包
        /// </summary>
        /// <returns></returns>
        private UdpData createSearchVerUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)------
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { ByteDeviceId, ByteNetworkId, DeviceConfig.EQUIPMENT_RJ45 };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_VER;//----用户命令-----
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
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("获取版本号失败!", udpReply.ReplyByte);
            else
                this.DeviceName = newName;
        }
        /// <summary>
        /// 保存网络名称
        /// </summary>
        /// <returns>返回UDP</returns>
        private UdpData createSaveNetworkNameUdp(string newName)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)------
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { ByteDeviceId, ByteNetworkId, DeviceConfig.EQUIPMENT_RJ45 };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_VER;//----用户命令-----
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
                //string temp = ConvertTools.ByteToHexStr(udpReceive.GetUdpData());
                UserUdpData userData = new UserUdpData(udpReceive);
                if (CommonTools.BytesEuqals(userData.Command, NetworkConfig.CMD_PC_CONNECT_ACK))//---为连接成功-----
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
                    GetPositionList(); //----------获取位置列表---------

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
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { ByteDeviceId, ByteNetworkId, DeviceConfig.EQUIPMENT_RJ45 };//----目标信息--
            byte[] source = new byte[] { 0xFF, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = NetworkConfig.CMD_PC_CONNECT;//----用户命令-----
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
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackDisconnectNetwork), new object[] {  });

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
                if (CommonTools.BytesEuqals(userData.Command, NetworkConfig.CMD_PC_DISCONNECT))//---成功回复断开连接-----
                {
                    PCAddress = "";
                    State = NetworkConfig.STATE_NOT_CONNECTED;//---标记为未链接----
                    NetworkCtrl.UpdateNetworkDataTable(this);//---更新列表信息------
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
            Buffer.BlockCopy(SysConfig.LOCAL_PORT, 0, udp.SendPort, 0, 2);//----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { ByteDeviceId, ByteNetworkId, DeviceConfig.EQUIPMENT_RJ45 };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
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
   
    }


}
