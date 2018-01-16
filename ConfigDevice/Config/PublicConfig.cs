using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using System.Data;
using System.Management;
using System.Windows.Forms;

namespace ConfigDevice
{

    public delegate void Action();//用于公共委托类型,陆续添加带参数的
    public delegate void CallbackUdpAction(UdpData udpData,params object[] values);//----回调执行UDP包----
   // public delegate void CallbackUdpAction(UdpData udpData, CallbackParameter callbackParameter);//----回调执行UDP包----
    public delegate void CallbackUIAction(CallbackParameter callbackParameter);//----回调UI界面---- 
    public delegate void SyncCommandSetting(ViewCommandTools viewCommandTools);//---同步指令设置
    public delegate void DeleteCommandData(int cmdNum);//----删除命令-----
    public delegate void ChangePosition(int cmdNum);//----换位置----- 

    /// <summary>
    /// 回复结果码
    /// </summary>
    public class REPLY_RESULT
    {
        public const byte CMD_TRUE = 0x55;     //正确
        public const byte CMD_FALSE = 0xCC;     //错误
        public const byte CMD_UNSAME = 0x11;     //不相符
        public const byte CMD_BUSY = 0x22;     //忙 
        public const byte CMD_ERR = 0x33;    //包号出错 

        public const byte PC_CHANGENET_ACK_TRUE = 0;          // 修改网络正确
        public const byte PC_CHANGENET_ACK_PAS_ER = 0x1;          // 输入密码错误
        public const byte PC_CHANGENET_ACK_NET_ER = 0x2;         // 网络参数错误
        public const byte PC_CHANGENET_ACK_TOTAL = 0x3;          // 总数
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
    /// 执行类型
    /// </summary>
    public enum ActionKind
    {
        SearchDevice,
        SearchNetwork,
        ConnectNetowrk,
        DisConnectNetwork,
        SyncNetworkID,
        SaveDeviceID,
        SaveDeviceName,
        GetVer,
        ReadInf,//公共读取功能
        SaveNetworkPosition,//保存网络位置
        SaveNetworkParameter,//保存网络参数
        ReadCircuit,//读取回路
        ReadSafe,//获取安防
        ReadConfig,//获取参数配置
        ReadOption,//读取选择配置
        ReadData,//读取数据
        ReadAdditionAciton,//获取附加动作
        ReadSate,//读取状态
        ReadPower,//读取功率
        ReadLogicConfig,//读取逻辑配置
        ReadCommand,//读取指令
        ReadMenuCommand,//读取指令
        ReadMenu,//读取菜单
        ReadServerAddress,//读取服务器位置
        ReadMenuSecurity,//读取菜单安防配置
        ReadMenuSensor,//读取传感器配置
        ReadWirlessDevice,//读取无线设备
        WirteWirlessDevice,//写无线设备
        DelWirlessDevice,//删除无线设备
        AddWirlessDevice,//增加无线设备
        ShowLog,//日志操作
        AddDelClearWirlessDevice,//增加,删除,清除设备操作
        ReadLockAmplifierConfig,//获取锁系统参数配置
        ReadLockConfig,//获取锁参数配置
        None
        
    }



    public class CallbackParameter
    { 
                 public ActionKind Action = ActionKind.None;
        public String DeviceID;
        public object[] Parameters; 
        /// <summary>
        /// 回调构造参数
        /// </summary>
        /// <param name="_ActionName">动作</param>
        /// <param name="_Parameters">回调参数</param> 
        public CallbackParameter(ActionKind _ActionName,String _deviceID, params object[] _Parameters)
        {
            DeviceID = _deviceID;
            Action = _ActionName;
            Parameters = _Parameters;
        }

        /// <summary>
        /// 回调构造参数
        /// </summary> 
        /// <param name="_Parameters">回调参数</param>
        //public CallbackParameter(object[] _Parameters)
        //{
        //    Action = ActionKind.None;
        //    Parameters = _Parameters;
        //}

        /// <summary>
        /// 回调构造参数
        /// </summary> 
        /// <param name="_Parameters">回调参数</param>
        //public CallbackParameter(params object[] _Parameters)
        //{
        //    Parameters = _Parameters;
        //}
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
        public const byte Broadcast = 0x42;//广播
        public const byte Multicast = 0x4D;//组播
        public const byte Unicast = 0x55;//单播
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

    /// <summary>
    /// IP信息
    /// </summary>
    public class IPInfo
    {
        public string IP = "";
        public string Gateway = "";
        public string SubnetMask = "";

        public IPInfo(string _ip, string _gateway, string _subnetMask)
        {
            IP = _ip;
            Gateway = _gateway;
            SubnetMask = _subnetMask;
        }
    }

    //
    // 摘要:Command.cs
    //     用于保存系统配置
    public static class SysConfig
    {
        public static readonly string ConfigPath = Application.StartupPath + "\\data\\config\\";

        static SysConfig()
        {
            LocalPort = (UInt16)PortTools.GetAvailablePort();//初始化本地端口
        }

        public static DataTable DtNetwork = new DataTable("Network");//-----网络列表-----
        public static DataTable DtDevice = new DataTable("Device");//-----设备列表-----
        public static Dictionary<string, Network> ListNetworks = new Dictionary<string, Network>();//-----登记网络-----   

        public static StringBuilder sbTest = new StringBuilder();

        public static UInt16 LocalPort = 0;//本地端口
        public static byte[] ByteLocalPort { get { return BitConverter.GetBytes(LocalPort); } }
        public static readonly UInt16 RemotePort = 11211;//远程端口
        public static byte[] ByteRemotePort { get { return BitConverter.GetBytes(RemotePort); } }


        public static string LogFile;//日志文件

        public const Int16 MAX_DATA_SIZE = 256;//若定最大256长度.
        public const Int16 MIN_DATA_SIZE = 30;//若定最小30长度.
        public static Dictionary<int, IPInfo> IPList = new Dictionary<int, IPInfo>();

        private static bool limitMouseWheel = false;//是否允许鼠标滚动
        public static bool LimitMouseWheel
        {
            get { return SysConfig.limitMouseWheel; }
            set { SysConfig.limitMouseWheel = value; }
        }


        private static IPAddress defaultIPGateway;
        /// <summary>
        /// 本地默认网关
        /// </summary>
        public static IPAddress DefaultIPGateway
        {
            get
            {
                return defaultIPGateway;
            }
        }

        private static IPAddress localIP;
        /// <summary>
        /// 本地IP
        /// </summary>
        public static IPAddress LocalIP
        {
            get
            {
                return localIP;
            }
        }

        private static IPAddress subnetMask;
        /// <summary>
        /// 子网掩码
        /// </summary>
        public static IPAddress SubnetMask
        {
            get
            {
                return subnetMask;
            }
        }


        /// <summary>
        /// 设置本地地址信息
        /// </summary>
        /// <param name="index"></param>
        public static void SetLocalIPInfo(int index)
        {
            SysConfig.localIP = IPAddress.Parse(SysConfig.IPList[index].IP);
            SysConfig.subnetMask = IPAddress.Parse(SysConfig.IPList[index].SubnetMask);
            SysConfig.defaultIPGateway = IPAddress.Parse(SysConfig.IPList[index].Gateway);
        }

        public static Dictionary<string, CallbackFromUDP> RJ45CallBackList = new Dictionary<string, CallbackFromUDP>();//----RJ45回调表-----  

        /// <summary>

        public static void Edit_Enter(object sender, EventArgs e)
        {
            LimitMouseWheel = true;
        }
        public static void Edit_Leave(object sender, EventArgs e)
        {
            LimitMouseWheel = false;
        }

    }

    /// <summary>
    /// 回调对象
    /// </summary>
    public class CallbackFromUDP
    {
        public UdpData Udp;//---包数据---
        public event CallbackUdpAction CallBackAction;//----委托操作----    
        public EndPoint RemotePoint;//---标识网络地址---
        public object[] Parameters;
        public long ActionCount = long.MaxValue; 
        public CallbackUdpAction GetCallBackAction
        {
            get { return CallBackAction; }
        }

        public CallbackFromUDP(UdpData udp, CallbackUdpAction callBack, EndPoint endPoint, object[] objs)
        {
            Udp = udp;
            if (callBack != null)
                CallBackAction = callBack;
            RemotePoint = endPoint;
            if (objs != null)
                Parameters = objs;//回调时可能需要其他参数,
        }

        public CallbackFromUDP()
        {

        }

        public CallbackFromUDP(CallbackUdpAction callBack)
        {
            CallBackAction = callBack;
        }

        public CallbackFromUDP(CallbackUdpAction callBack, object[] objs)
        {
            CallBackAction = callBack;
            Parameters = objs;
        }

        /// <summary>
        /// 执行接口
        /// </summary>
        /// <param name="udpReply">返回UDP</param>
        /// <param name="objs">参数组</param>
        public void ActionCallback(UdpData udpReply,object[] objs)
        {
            if (CallBackAction != null)
                CallBackAction.BeginInvoke(udpReply, objs, null, null);//---异步调用-----

        }

 

    }




}
