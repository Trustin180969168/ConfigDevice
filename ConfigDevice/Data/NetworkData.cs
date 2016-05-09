using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
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
        public string SoftwareVer = "";//软件版本
        public string HardwareVer = "";//硬件版本

        public byte BytePCAddress { get { return BitConverter.GetBytes(Convert.ToInt16(PCAddress))[0]; } }
        public byte ByteDeviceID { get { return BitConverter.GetBytes(Convert.ToInt16(DeviceID))[0]; } }
        public byte ByteNetworkID { get { return BitConverter.GetBytes(Convert.ToInt16(NetworkID))[0]; } }
        public byte[] ByteMacAddress { get { return ConvertTools.StrToToHexByte(MacAddress); } }


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

        }




    }
}
