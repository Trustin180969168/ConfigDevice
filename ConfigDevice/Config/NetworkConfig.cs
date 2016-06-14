using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class CONNECT_RESULT
    {
        public const byte ALLOW_CONNECT = 0x1;
        public const byte NOT_ALLOW_CONNECT = 0x3;
    }


    public class NetworkConfig
    {



        //------定义全局对应--------
        public const string DC_DEVICE_ID = "DeviceID";//设备ID
        public const string DC_KINDNAME = "KindName";//设备类型名称
        public const string DC_NETWORK_ID = "NetworkID";//网段ID
        public const string DC_STATE = "State";//连接状态
        public const string DC_DEVICE_NAME = "DeviceName";//设备名称
        public const string DC_MAC = "MAC";//物理ID
        public const string DC_PORT = "Port";//端口
        public const string DC_IP = "IP";//IP地址
        public const string STATE_CONNECTED = "已连接";//物理ID
        public const string STATE_NOT_CONNECTED = "未连接";//物理ID
        public const string DC_PC_ADDRESS = "PCAddress";//与PC的通信地址
        public const string DC_REMARK = "Remark";//备注

        public const int CONNECT_TIME_OUT = 25;//超时10秒当作未连接
        public const string BROADCAST_IP = "255.255.255.255";//广播地址 

        public const string ERROR_SAME_NETWORKID = "网段ID冲突!";//网段冲突
        public const string ERROR_SAME_NETWORKNAME = "网段名称冲突!";//网段名称冲突

    }

}
