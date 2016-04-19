using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using System.Data;
using System.Management;

namespace ConfigDevice
{

    public delegate void Action();//用于公共委托类型,陆续添加带参数的
    public delegate void CallbackUdpAction(UdpData udpData,object[] values);//----回调执行UDP包----
    public delegate void CallbackUIAction(object[] values);//----回调UI界面----

    /// <summary>
    /// 回复结果
    /// </summary>
    public class REPLY_RESULT
    {
        public const byte CMD_TRUE = 0x55;     //正确
        public const byte CMD_FALSE = 0xCC;     //错误
        public const byte CMD_UNSAME = 0x11;     //不相符
        public const byte CMD_BUSY = 0x22;     //忙 
        public const byte CMD_ERR = 0x33;    //包号出错 
    }

    /// <summary>
    /// 回复结果
    /// </summary>
    public class REPLY_INFO
    {
        public const string STR_TRUE = "回复确认,正确!";     //正确
        public const string STR_FALSE = "回复确认,错误!";     //错误
        public const string STR_UNSAME = "回复确认,不相符!";     //不相符
        public const string STR_BUSY = "回复确认,忙!";     //忙 
        public const string STR_ERR = "回复确认,包出错!";    //包号出错 
    }

    /// <summary>
    /// 密码类型
    /// </summary>
    public enum PasswordKind
    {
        Manager,User
    }

    /// <summary>
    /// 发送类型
    /// </summary>
    public class PackegeSendReply
    {
        public const byte SEND = 0x01;
        public const byte REPLY = 0x02;
    }

    /// <summary>
    /// 广播方式
    /// </summary>
    public class BroadcastKind
    {
        public const byte Broadcast = 0x42;
        public const byte Multicast = 0x4D;
        public const byte Unicast = 0x55;
    }

    /// <summary>
    /// 用户协议
    /// </summary>
    public class UserProtocol
    {
        public static readonly byte[] RJ45 = new byte[4]{ 0x01, 0x00, 0x00, 0x00 };
        public static readonly byte[] Device = new byte[4] { 0x02, 0x00, 0x00, 0x00 };
        public static readonly byte[] Ignore = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
    }

    /// <summary>
    /// 数据包配置
    /// </summary>
    public class UdpDataConfig
    {
        //-------字段命名规则,用于包数据列表------
        public const string DC_XTBSF = "SysIdName";
        public const string DC_XTSBM = "SysIdCode";
        public const string DC_BBSM = "PacketCode";
        public const string DC_BSJL = "PacketKind";
        public const string DC_BSX = "PacketProperty";
        public const string DC_JSDK = "ReceivePort";
        public const string DC_YHXY = "Protocol";
        public const string DC_YHSJ = "ProtocolData";
        public const string DC_JYH = "CheckCodeAdd";
        public const string DC_SYSJ = "AllData";
        public const string DC_SJCD = "Length";

        //-----UDP数据默认规范------
        public static readonly byte[] PACKAGE_HEAD_NAME = { 0x41, 0x59, 0x4C, 0x53, 0x4F, 0x4E, 0x20, 0x73, 0x6D, 0x61, 0x72, 0x74, 0x68, 0x6F, 0x6D, 0x65 };
        public static readonly byte[] PACKAGE_HEAD_ID = { 0x00,0x00 };//------系统识别码------
        public static readonly byte[] PACKAGE_CHECK_CODE = { 0x00 };//------校验和------   

        public static readonly byte DEFAULT_PAGE = 0x11;//------分页------   

        public const int MIN_USER_DATA_SIZE = 43;//最小用户有效数据
    }

    //
    // 摘要:Command.cs
    //     用于保存系统配置
    public static class SysConfig
    {
        public static DataTable DtNetwork = new DataTable("Network");//-----网络列表-----
        public static DataTable DtDevice = new DataTable("Device");//-----设备列表-----
        public static Dictionary<string, NetworkData> ListNetworks = new Dictionary<string, NetworkData>();//-----登记网络-----   

        public static StringBuilder sbTest = new StringBuilder();
        public static readonly byte[] LOCAL_PORT = { 0x1D, 0x25 };//-----本地端口---
        public static readonly byte[] REMOTE_PORT = { 0xCB, 0x2B };//----远程端口-----      
        public static int LocalPort = BitConverter.ToInt16(LOCAL_PORT, 0);//本地端口
        public static int RemotePort = BitConverter.ToInt16(REMOTE_PORT, 0);//远程端口
        public const Int16 MAX_DATA_SIZE = 128;//若定最大128长度.
        public const Int16 MIN_DATA_SIZE = 30;//若定最小30长度.

        /// <summary>
        /// 本地默认网关
        /// </summary>
        public static IPAddress DefaultIPGateway
        {
            get
            {
                string AddressIP = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection nics = mc.GetInstances();
                foreach (ManagementObject nic in nics)
                {
                    if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                        AddressIP = (nic["DefaultIPGateway"] as String[])[0];
                }
                return IPAddress.Parse(AddressIP);
            }
        }
        /// <summary>
        /// 本地IP
        /// </summary>
        public static IPAddress LocalIP
        {
            get
            {
                string AddressIP = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection nics = mc.GetInstances();
                foreach (ManagementObject nic in nics)
                {
                    if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                        AddressIP = (nic["IPAddress"] as String[])[0];
                }
                return IPAddress.Parse(AddressIP);
            }
        }

        /// <summary>
        /// 子网掩码
        /// </summary>
        public static IPAddress SubnetMask
        {
            get
            {
                string AddressIP = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection nics = mc.GetInstances();
                foreach (ManagementObject nic in nics)
                {
                    if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                        AddressIP = (nic["IPSubnet"] as String[])[0];
                }
                return IPAddress.Parse(AddressIP);
            }
        }

        public static Dictionary<string, CallbackFromUdp> RJ45CallBackList = new Dictionary<string, CallbackFromUdp>();//----RJ45回调表-----  

        /// <summary>
        /// 添加到回调表
        /// </summary>
        /// <param name="key">命令</param>
        /// <param name="callback">回调对象</param>
        public static void AddRJ45CallBackList(byte[] key,CallbackFromUdp callback)
        {
            string keyStr = ConvertTools.ByteToHexStr(key);
            if (!RJ45CallBackList.ContainsKey(keyStr))
                RJ45CallBackList.Add(keyStr, callback);
            else
            {
                RJ45CallBackList[keyStr] = callback;  //----暂时只用于单个事件订阅,直接覆盖------
            }
        }

    }

    /// <summary>
    /// 回调对象
    /// </summary>
    public class  CallbackFromUdp
    {
        public UdpData Udp;//---包数据---
        public event CallbackUdpAction CallBackAction;//----委托操作----    
        public EndPoint RemotePoint;//---标识网络地址---
        public object[] Values;

        public CallbackUdpAction getCallBackAction
        {
            get { return CallBackAction; }
        }

        public CallbackFromUdp(UdpData udp, CallbackUdpAction callBack, EndPoint endPoint, object[] objs)
        {
            Udp = udp;
            CallBackAction = callBack;
            RemotePoint = endPoint;
            Values = objs;//回调时可能需要其他参数,
        }

        public CallbackFromUdp()
        {

        }

        public CallbackFromUdp(CallbackUdpAction callBack)
        {
            CallBackAction = callBack;
        }

        /// <summary>
        /// 执行接口
        /// </summary>
        /// <param name="udpReply">返回UDP</param>
        /// <param name="objs">参数组</param>
        public void ActionCallback(UdpData udpReply,object[] objs)
        {
            if(CallBackAction != null)
                CallBackAction.BeginInvoke(udpReply, objs, null, null);//---异步调用-----
        }

    }




}
