using System;
using System.Collections.Generic;
using System.Text;

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
        public byte[] AddressID;//设备地址ID


        public abstract void RefreshData();//----刷新数据----
        public abstract void OpenLight();//----打开指示灯-----
        public abstract void CloseLight();//----关闭指示灯-----
        public abstract void OpenDiscover();//----打开发现设备---
        public abstract void CloseDiscover();//----关闭发现设备

        public byte BytePCAddress { get { return BitConverter.GetBytes(Convert.ToInt16(PCAddress))[0]; } }
        public byte ByteDeviceID { get { return BitConverter.GetBytes(Convert.ToInt16(DeviceID))[0]; } }
        public byte ByteKindID { get { return BitConverter.GetBytes(Convert.ToInt16(KindID))[0]; } }
        public byte[] ByteMacAddress { get { return ConvertTools.StrToToHexByte(MAC); } }
        public byte ByteNetworkId { get { return BitConverter.GetBytes(Convert.ToInt16(NetworkID))[0]; } }

    }
}
