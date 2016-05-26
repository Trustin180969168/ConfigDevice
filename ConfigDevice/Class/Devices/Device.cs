using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public abstract class Device
    {
        public string DeviceID = "";//设备ID
        public string NetworkID = "";//网段ID
        public string KindID;//设备类型ID
        public string KindName = "";//设备类型名称
        public string Name = "";//设备名称
        public string MAC = "";//设备机身码
        public string State = "";//设备状态
        public string Remark = "";//设备提示
        public string SoftwareVer = "";//软件版本
        public string HardwareVer = "";//硬件版本
        public string PCAddress = "";//PC通讯地址
        public string NetworkIP = "";//设备通讯IP地址
        public string AddressName = "";//设备地址
        public byte[] ByteAddressID;//字节设备地址ID
        public string AddressID = "";//设备地址ID 
    
        public Dictionary<string, ControlObj> ContrlObjs = new Dictionary<string, ControlObj>();//控制对象列表

        public byte BytePCAddress { get { return BitConverter.GetBytes(Convert.ToInt16(PCAddress))[0]; } }
        public byte ByteDeviceID { get { return BitConverter.GetBytes(Convert.ToInt16(DeviceID))[0]; } }
        public byte ByteKindID { get { return BitConverter.GetBytes(Convert.ToInt16(KindID))[0]; } }
        public byte[] ByteMacAddress { get { return ConvertTools.StrToToHexByte(MAC); } }
        public byte ByteNetworkId { get { return BitConverter.GetBytes(Convert.ToInt16(NetworkID))[0]; } } 

        protected MySocket mySocket = MySocket.GetInstance();
        public event CallbackUIAction OnCallbackUI_Action;   //----回调UI----
        public CallbackFromUDP callbackVer;//----回调版本号----
        public CallbackFromUDP callbackSaveID;//----回调保存ID号-----
        public CallbackFromUDP callbackSaveName;//--回调保存名称-----
        public CallbackFromUDP callbackRefresh;//---回调刷新----

        public Device(UserUdpData userUdpData)
        {
            DeviceID = Convert.ToInt16(userUdpData.Source[0]).ToString();
            NetworkID = Convert.ToInt16(userUdpData.Source[1]).ToString();
            PCAddress = Convert.ToInt16(userUdpData.Target[0]).ToString();
            NetworkIP = userUdpData.IP;
            ByteAddressID = CommonTools.CopyBytes(userUdpData.Data, 12, 2);//-------设备位置---------
            AddressID = ConvertTools.ByteToHexStr(ByteAddressID);//设备ID
            //-------计算位置名称-------
            byte byteNum = ByteAddressID[0];
            int num = 0x7F & byteNum; //序号       
            if (userUdpData.Source[2] != DeviceConfig.EQUIPMENT_RJ45 && userUdpData.Source[2] != DeviceConfig.EQUIPMENT_SERVER)
            {
                Network network = SysConfig.ListNetworks[userUdpData.IP];
                if (num <= network.ListPosition.Count - 1)
                    AddressName = network.ListPosition[num].Name;
            }
            //-------设备名称---------
            byte[] byteName = new Byte[userUdpData.DataOfLength - 12 - 2 - 4];
            if (userUdpData.DataOfLength > 16)
                Buffer.BlockCopy(userUdpData.Data, 14, byteName, 0, userUdpData.DataOfLength - 12 - 2 - 4);//12:MAC,2:位置,4:校验码
            int i = 0;
            foreach (byte b in byteName)
            {
                if (Convert.ToInt16(b) == 0)
                    break;
                else i++;
            }
            Array.Resize(ref byteName, i);//重新设定长度
            Name = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0');
            //-------MAC地址---------
            byte[] byteMac = new Byte[12];
            Buffer.BlockCopy(userUdpData.Data, 0, byteMac, 0, 12);
            MAC = ConvertTools.ByteToHexStr(byteMac);
            //-------设备类型---------
            KindID = Convert.ToInt16(userUdpData.Source[2]).ToString();
            if (DeviceConfig.EQUIPMENT_ID_NAME.ContainsKey(userUdpData.Source[2]))
                KindName = DeviceConfig.EQUIPMENT_ID_NAME[userUdpData.Source[2]];
            State = DeviceConfig.STATE_RIGHT;

            InitCallback();
        }

        /// <summary>
        /// 回调UI
        /// </summary>
        /// <param name="values"></param>
        public void CallbackUI(object[] values)
        {
            if (this.OnCallbackUI_Action != null)
                OnCallbackUI_Action(values);
        }

        /// <summary>
        /// 注册RJ45回调, 
        /// </summary>
        public void InitCallback()
        {
            callbackVer = new CallbackFromUDP(getVer);
            callbackRefresh = new CallbackFromUDP(getRefreshDevice);
            callbackSaveID = new CallbackFromUDP(getResultDevice); 
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Device(DataRow dr)
        {
            DeviceID = dr[DeviceConfig.DC_ID].ToString();
            NetworkID = dr[DeviceConfig.DC_NETWORK_ID].ToString();
            KindID = dr[DeviceConfig.DC_KIND_ID].ToString();
            KindName = dr[DeviceConfig.DC_KIND_NAME].ToString();
            Name = dr[DeviceConfig.DC_NAME].ToString();
            MAC = dr[DeviceConfig.DC_MAC].ToString();
            SoftwareVer = dr[DeviceConfig.DC_SOFTWARE_VER].ToString();
            HardwareVer = dr[DeviceConfig.DC_HARDWARE_VER].ToString();
            PCAddress = dr[DeviceConfig.DC_PC_ADDRESS].ToString();
            NetworkIP = dr[DeviceConfig.DC_NETWORK_IP].ToString();
            AddressName = dr[DeviceConfig.DC_ADDRESS_NAME].ToString();
            AddressID = dr[DeviceConfig.DC_ADDRESS_ID].ToString();            
            ByteAddressID =ConvertTools.StrToToHexByte( dr[DeviceConfig.DC_ADDRESS_ID].ToString());
            State = dr[DeviceConfig.DC_STATE].ToString();

            InitCallback();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Device(DeviceData data)
        {
            DeviceID = data.DeviceID;
            NetworkID = data.NetworkID;
            KindID = data.KindID;
            KindName = data.KindName;
            Name = data.Name;
            MAC = data.MAC;
            SoftwareVer = data.SoftwareVer;
            HardwareVer = data.HardwareVer;
            PCAddress = data.PCAddress;
            NetworkIP = data.NetworkIP;
            AddressName = data.AddressName;
            AddressID = data.AddressID;
            if(AddressID != "")
                ByteAddressID = ConvertTools.StrToToHexByte(data.AddressID);
            State = data.State;

            InitCallback();
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns></returns>
        public DeviceData GetDeviceData()
        {
            DeviceData data = new DeviceData();
            data.DeviceID = DeviceID;
            data.NetworkID = NetworkID;
            data.KindID = KindID;
            data.KindName = KindName;
            data.Name = Name;
            data.MAC = MAC;
            data.SoftwareVer = SoftwareVer;
            data.HardwareVer = HardwareVer;
            data.PCAddress = PCAddress;
            data.NetworkIP = NetworkIP;
            data.AddressID = AddressID;
            data.ByteAddressID = ByteAddressID;
            data.AddressName = AddressName;
            data.State = State;
            data.Remark = Remark;
       

            return data;
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        public void SearchVer()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_VER, callbackVer);//注册返回版本号
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
            string name = this.Name;
            //-----获取数据-----
            UserUdpData userData = new UserUdpData(data);
            Device device = new BaseDevice(userData);
            if (DeviceID == device.DeviceID)
            {
                //------获取版本号------                
                byte[] temp1 = new byte[20]; byte[] temp2 = new byte[20];
                Buffer.BlockCopy(userData.Data, 0, temp1, 0, 20);
                SoftwareVer = Encoding.GetEncoding("ASCII").GetString(temp1).TrimEnd('\0');
                Buffer.BlockCopy(userData.Data, 20, temp2, 0, 20);
                HardwareVer = Encoding.GetEncoding("ASCII").GetString(temp2).TrimEnd('\0');
            }
                //------回复反馈的设备信息-------
                UdpTools.ReplyDeviceDataUdp(data);
   
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
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
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
        /// 保存设备ID以及设备名称
        /// </summary>
        /// <param name="DeviceData">新设备数据</param> 
        public void SaveDeviceIDAndName(DeviceData data)
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_INF,MAC, callbackSaveID,1);//回调返回的一个设备结果
            callbackSaveID.Values = new object[] { data };//---保存回调参数----
            UdpData udp = createSaveDeviceIDUdp(data.DeviceID);
            MySocket.GetInstance().SendData(udp, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSaveDeviceID), new object[] { udp });
        }
        /// <summary>
        /// 修改设备ID
        /// </summary>
        /// <param name="ID">设备ID</param>
        public void SaveDeviceID(string newID)
        { 
            callbackSaveID.ActionCount = 1;//---只允许回调一次---
            UdpData udp = createSaveDeviceIDUdp(newID);
            MySocket.GetInstance().SendData(udp, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSaveDeviceID), new object[] { udp });            
        }
        private void callbackSaveDeviceID(UdpData udp, object[] values)
        {
            UdpData sendUdp = (UdpData)values[0];
            if (udp.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("修改设备ID失败!", udp.ReplyByte);          
        }
        /// <summary>
        /// 创建修改设备ID的UDP包
        /// </summary>
        /// <returns></returns>
        private UdpData createSaveDeviceIDUdp(string newID)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)------
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议-----

            byte[] target = new byte[] { DeviceConfig.EQUIPMENT_PC, ByteNetworkId, ByteKindID };//----目标信息----
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_ASSIGN_ID;//----用户命令-----
            byte len = 0x12;//---数据长度---

            //--------添加到校验数据--------
            byte[] crcData = new byte[24];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;

            Buffer.BlockCopy(ByteMacAddress, 0, crcData, 10, 12);//---MAC地址---
            crcData[22] = ConvertTools.GetByteFrom8BitNumStr(newID);//---新的ID----
            crcData[23] = ByteNetworkId;

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, 24);
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, 24, 4);
            Array.Resize(ref udp.ProtocolData, 28);//重新设定长度    
            udp.Length = 28 + 28 + 1;

            return udp;
        }
        /// <summary>
        /// 获取修改结果
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values">回调参数</param>
        private void getResultDevice(UdpData data, object[] values)
        {
            //-----获取数据-----
            UserUdpData userData = new UserUdpData(data);
            Device resultDevice = new BaseDevice(userData);//----获取返回的设备信息----
            DeviceData deviceData = values[0] as DeviceData;//---收回需要修改的设备信息---
            if (resultDevice.MAC == this.MAC)
            {
                if (resultDevice.DeviceID == deviceData.DeviceID)//---相同则成功----
                {
                    this.DeviceID = resultDevice.DeviceID;//---新ID-----    
                    UdpTools.ReplyDeviceDataUdp(data);//---回复UDP----
                    SaveDeviceName(deviceData.Name, deviceData.ByteAddressID, deviceData.AddressName);//---保存设备名称------                  
                }
                else
                {
                    this.DeviceID = resultDevice.DeviceID;//---恢复原来的设备ID---
                    CommonTools.MessageShow("设备ID修改失败!", 2, "");
                }
            }else//-----RJ45的数据会执行多个回调,若mac地址未匹配,要继续接收回调,直到回调成功.
                callbackSaveID.ActionCount = 1;//--继续接收回调数据----

        }


        /// <summary>
        /// 保存名称
        /// </summary>
        /// <param name="newName">新名称</param>
        public void SaveDeviceName(string newName, byte[] position, string newPos)
        {
            UdpData saveNameUdp = createSaveDeviceNameUdp(newName, position);
            MySocket.GetInstance().SendData(saveNameUdp, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSaveDeviceName),
                new object[] { saveNameUdp, newName, newPos, position });
        }
        private void callbackSaveDeviceName(UdpData udpReply, object[] values)
        {
            UdpData udpSend = (UdpData)values[0];
            string newName = (string)values[1];
            string newPos = (string)values[2];
            byte[] newBytePostion = (byte[])values[3];
            if (udpReply.ReplyByte == REPLY_RESULT.CMD_TRUE)
            {
                this.Name = newName; this.AddressName = newPos; this.ByteAddressID = newBytePostion;
                SysCtrl.UpdateDeviceData(this.GetDeviceData());
                CallbackUI(new object[] { ActionKind.SaveDeviceName, this });
            }
            else
                CommonTools.ShowReplyInfo("保存名称失败!", udpReply.ReplyByte);
        }
        /// <summary>
        /// 创建修改设备ID的UDP包
        /// </summary>
        /// <returns>返回UDP包</returns>
        private UdpData createSaveDeviceNameUdp(string newName, byte[] position)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;//-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_NAME;//----用户命令-----      
            byte[] byteName = new byte[30];
            byte[] temp = Encoding.GetEncoding("GB2312").GetBytes(newName);      //---------新名称-------------
            Buffer.BlockCopy(temp, 0, byteName, 0, temp.Length);
            byte len = (byte)(2 + byteName.Length + 4);//---数据长度 =地址长度2 + 名称长度30 + 校验码4--
            //--------添加到校验数据--------
            byte[] crcData = new byte[10 + 2 + byteName.Length];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            Buffer.BlockCopy(position, 0, crcData, 10, 2);
            Buffer.BlockCopy(byteName, 0, crcData, 12, byteName.Length);

            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }


        /// <summary>
        /// 刷新基本信息
        /// </summary>
        public void RefreshData()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_INF,callbackRefresh);//回调刷新结果
            UdpData udpSend = createRefreshUdp(); 
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackRefreshDevice), new object[] { udpSend });
        }
        private void callbackRefreshDevice(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("刷新失败!", udpReply.ReplyByte);
        }
        private UdpData createRefreshUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_INF;//----用户命令-----

            byte[] crcData = new byte[10];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = 0x04;
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }
        /// <summary>
        /// 获取设备
        /// </summary>
        private void getRefreshDevice(UdpData data, object[] values)
        {
            //------获取数据-----
            UserUdpData userData = new UserUdpData(data);
            Device device = new BaseDevice(userData);
            //------回复反馈的设备信息-------
            //UdpTools.ReplyDeviceDataUdp(data);//----由主界面接收反馈----

            if (device.MAC == this.MAC)
            {
                this.DeviceID = device.DeviceID;
                this.NetworkID = device.NetworkID;
                this.KindID = device.KindID;
                this.KindName = device.KindName;
                this.Name = device.Name;
                this.State = device.State;
                this.PCAddress = device.PCAddress;
                this.NetworkIP = device.NetworkIP;
                this.AddressName = device.AddressName;

                CallbackUI(null);//---返回UI----
                SysCtrl.UpdateDeviceData(this.GetDeviceData());//--刷新----
            }
        }


        /// <summary>
        /// 根据指令创建UDP
        /// </summary>
        /// <param name="cmd">指令</param>
        /// <returns></returns>
        private UdpData createCommandUdp(byte[] _cmd)
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播/广播/组播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            if (this.ByteKindID == DeviceConfig.EQUIPMENT_SERVER || this.ByteKindID == DeviceConfig.EQUIPMENT_RJ45)
                Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议----
            else
                Buffer.BlockCopy(UserProtocol.Device, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkId, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkId, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = _cmd;//----用户命令-----

            byte[] crcData = new byte[10];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = 0x04;
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 通信设备灯开
        /// </summary>
        public void OpenLight()
        {
            UdpData udpSend = createCommandUdp(DeviceConfig.CMD_PUBLIC_UART_LED_ENABLE);
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackResult),
                new object[] { udpSend, "打开通信设备灯失败!", "打开通信设备灯!" });
        }

        /// <summary>
        /// 通信设备灯关
        /// </summary>
        public void CloseLight()
        {
            UdpData udpSend = createCommandUdp(DeviceConfig.CMD_PUBLIC_UART_LED_DISABLE);
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackResult),
                new object[] { udpSend, "关闭通信设备灯失败!", "关闭通信设备灯!" });
        }

        /// <summary>
        /// 打开发现设备
        /// </summary>
        public void OpenDiscover()
        {
            UdpData udpSend = createCommandUdp(DeviceConfig.CMD_PUBLIC_DISCOVER_ENABLE);
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackResult),
                new object[] { udpSend, "开启发现设备失败!", "开启发现设备!" });
        }

        /// <summary>
        /// 关闭发现设备
        /// </summary>
        public void CloseDiscover()
        {
            UdpData udpSend = createCommandUdp(DeviceConfig.CMD_PUBLIC_DISCOVER_DISABLE);
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackResult),
                new object[] { udpSend, "关闭发现设备失败!", "关闭发现设备!" });
        }

        /// <summary>
        /// 返回执行结果
        /// </summary>
        /// <param name="udpReply">返回包</param>
        /// <param name="values">参数组</param>
        private void callbackResult(UdpData udpReply, object[] values)
        {
            UdpData udpSend = (UdpData)values[0];
            string error = (string)values[1];
            string success = (string)values[2];
            if (udpSend.PacketCodeStr == udpReply.PacketCodeStr)
            {
                if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                { CommonTools.ShowReplyInfo(error, udpReply.ReplyByte); return; }
                else
                    CommonTools.MessageShow(success, 1, "");
            }
        }


    }
}
