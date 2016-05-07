using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class DeviceData
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
        public byte[] AddressID;//设备地址ID

        public byte BytePCAddress { get { return BitConverter.GetBytes(Convert.ToInt16(PCAddress))[0]; } }
        public byte ByteDeviceID { get { return BitConverter.GetBytes(Convert.ToInt16(DeviceID))[0]; } }
        public byte ByteKindID { get { return BitConverter.GetBytes(Convert.ToInt16(KindID))[0]; } }
        public byte[] ByteMacAddress { get { return ConvertTools.StrToToHexByte(MAC); } }
        public byte ByteNetworkId { get { return BitConverter.GetBytes(Convert.ToInt16(NetworkID))[0]; } }

        public DeviceData()
        {
        }

        public DeviceData(UserUdpData userUdpData)
        {
            DeviceID = Convert.ToInt16(userUdpData.Source[0]).ToString();
            NetworkID = Convert.ToInt16(userUdpData.Source[1]).ToString();
            PCAddress = Convert.ToInt16(userUdpData.Target[0]).ToString();
            NetworkIP = userUdpData.IP;
            AddressID = CommonTools.CopyBytes(userUdpData.Data, 12, 2);//-------设备位置---------
            //-------计算位置名称-------
            byte byteNum = AddressID[0];
            int num = 0x7F & byteNum; //序号          
            Network network = SysConfig.ListNetworks[userUdpData.IP];
            if (num <= network.ListPosition.Count - 1)
                AddressName = network.ListPosition[num].Name;
            //-------设备名称---------
            byte[] byteName = new Byte[32];
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

        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public DeviceData(DataRow dr)
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
            AddressName = dr[DeviceConfig.DC_ADDRESS].ToString();
            State = dr[DeviceConfig.DC_STATE].ToString();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeviceData(DeviceData value)
        {
            DeviceID = value.DeviceID;
            NetworkID = value.NetworkID;
            KindID = value.KindID;
            KindName = value.KindName;
            Name = value.Name;
            MAC = value.MAC;
            SoftwareVer = value.SoftwareVer;
            HardwareVer = value.HardwareVer;
            PCAddress = value.PCAddress;
            NetworkIP = value.NetworkIP;
            AddressName = value.AddressName;
            State = value.State;
        }




    }
}
