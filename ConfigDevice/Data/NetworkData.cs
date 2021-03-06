﻿using System;
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

        public int Num = 0;//位置编号
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

        public byte BytePCAddress { get { return BitConverter.GetBytes(Convert.ToInt16(PCAddress))[0]; } }
        public byte ByteDeviceId { get { return BitConverter.GetBytes(Convert.ToInt16(DeviceID))[0]; } }
        public byte ByteNetworkId { get { return BitConverter.GetBytes(Convert.ToInt16(NetworkID))[0]; } }
        public byte[] ByteMacAddress { get { return ConvertTools.StrToToHexByte(MacAddress); } }

        public DateTime RefreshTime;
        private CallbackFromUdp callbackGetPosition = new CallbackFromUdp();

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
            State = NetworkConfig.DC_NOT_CONNECTED;
            //-------MAC地址---------
            byte[] byteMac = new Byte[12];
            Buffer.BlockCopy(userUdpData.Data, 6, byteMac, 0, 12);
            MacAddress = ConvertTools.ByteToHexStr(byteMac);
            //-------设备名称---------
            byte[] byteName = new Byte[30];
            Buffer.BlockCopy(userUdpData.Data, 20, byteName, 0, 30);
            DeviceName = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0');//.Replace("网关:","");
            ListPosition = new List<Position>();


            callbackGetPosition.CallBackAction += callbackGetPositions;
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
            Port = Convert.ToInt16( dr[NetworkConfig.DC_PORT]);
            PCAddress = dr[NetworkConfig.DC_PC_ADDRESS].ToString();
            ListPosition = new List<Position>();

            callbackGetPosition.CallBackAction += callbackGetPositions;
        }


        /// <summary>
        /// 获取设备列表
        /// </summary>
        public void GetPositionList()
        {
            ListPosition.Clear();
            UdpData udpSend = createGetPositionListUdp();
            callbackGetPosition.Udp = udpSend;
            SysConfig.AddRJ45CallBackList(NetworkConfig.CMD_PC_WRITE_LOCALL_NAME, callbackGetPosition);
            mySocket.SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallBackUdpAction(callbackGetReply), new object[] { udpSend });
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
            //SysConfig.sbTest.Append(udpPosition.PacketCodeStr + "位置数据到达时间----" + DateTime.Now.ToString("mm:ss:ffff") + "\n");            
            //-----------回复RJ45,已经获取了一个设备位置-------
            UdpData udpReply = UdpTools.CreateDeviceReplyUdp(udpPosition);
            mySocket.ReplyData(udpReply, udpPosition.IP, SysConfig.RemotePort);

           // SysConfig.sbTest.Append(udpPosition.PacketCodeStr + "回复位置数据时间----" + DateTime.Now.ToString("mm:ss:ffff") + "\n");
            UserUdpData userData = new UserUdpData(udpPosition);
            byte temp = userData.Data[0];//第一个字节
            bool numHas = (int)(temp >> 7) == 1 ? true : false;//是否有密码
            int num = 0x7F & temp + 1; //序号
            byte[] byteName = CommonTools.CopyBytes(userData.Data, 1, 12);//---名称---
            string PositionName = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0');
            Position pos = new Position(num, PositionName, numHas);
            //if (!ListPosition.Contains(pos))
            ListPosition.Add(pos);

            ListPosition.Sort();
            //SysConfig.sbTest.Append(udpPosition.PacketCodeStr + "执行位置读取结束----" + DateTime.Now.ToString("mm:ss:ffff") + "\n");
        }


    }


}
