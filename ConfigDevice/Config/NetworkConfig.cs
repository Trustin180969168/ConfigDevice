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
        public static readonly byte[] CMD_PC_SEARCH = new byte[] { 0x01, DeviceConfig.EQUIPMENT_PC };       //----搜索网络命令----
        public static readonly byte[] CMD_PC_CONNECT = new byte[] { 0x30, DeviceConfig.EQUIPMENT_PC };      //----链接网络命令---
        public static readonly byte[] CMD_PC_CONNECT_ACK = new byte[] { 0xB0, DeviceConfig.EQUIPMENT_PC };  //----允许连接命令----
        public static readonly byte[] CMD_PC_CONNECTING = new byte[] { 0x31, DeviceConfig.EQUIPMENT_PC };   //----RJ45或主机主动刷新连接命令----
        public static readonly byte[] CMD_PC_DISCONNECT = new byte[] { 0x32, DeviceConfig.EQUIPMENT_PC };   //----PC主动断开连接命令----
        public static readonly byte[] CMD_PC_SEARCH_ACK = new byte[] { 0x81, DeviceConfig.EQUIPMENT_PC };   //2. <RJ45-PC> RJ45上报基本信息
        public static readonly byte[] CMD_PC_CHANGEPASSWORD = new byte[] { 0x02, DeviceConfig.EQUIPMENT_PC };   //3. <PC-RJ45> 修改RJ45密码
        public static readonly byte[] CMD_PC_CHANGEPASSWORD_ACK = new byte[] { 0x82, DeviceConfig.EQUIPMENT_PC };//4. <RJ45-PC> 回应修改RJ45密码结果
        public static readonly byte[] CMD_PC_CHANGENET = new byte[] { 0x03, DeviceConfig.EQUIPMENT_PC };        //5. <PC-RJ45> 修改RJ45网络参数
        public static readonly byte[] CMD_PC_CHANGENET_ACK = new byte[] { 0x83, DeviceConfig.EQUIPMENT_PC };    //6. <RJ45-PC> 回应PC修改RJ45网络参数结果
        public static readonly byte[] CMD_PC_CHANGENAME = new byte[] { 0x04, DeviceConfig.EQUIPMENT_PC };       //7. <PC-RJ45> 修改RJ45设备名称
        public static readonly byte[] CMD_PC_CHANGENAME_ACK = new byte[] { 0x84, DeviceConfig.EQUIPMENT_PC };   //8. <RJ45-PC> 回应PC修改RJ45设备名称结果
        public static readonly byte[] CMD_PC_READ_LOCALL_NAME = new byte[] { 0x05, DeviceConfig.EQUIPMENT_PC }; //14.<PC-RJ45> 读位置名称
        public static readonly byte[] CMD_PC_WRITE_LOCALL_NAME = new byte[] { 0x85, DeviceConfig.EQUIPMENT_PC };//14.<RJ45-PC> 写位置名称


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
        public const string BROADCAST_HEX_STR = "FF FF FF FF FF FF FF FF";//广播地址

        public const string ERROR_SAME_NETWORKID = "网段ID冲突!";//网段冲突
        public const string ERROR_SAME_NETWORKNAME = "网段名称冲突!";//网段名称冲突

    }

}
