using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Management;
using DevExpress.XtraGrid.Views.Grid;


namespace ConfigDevice
{

    //
    // 摘要:SysConfig.cs
    //     用于保存系统的基本配置信息,包括公共常量,本地IP,端口,初始化工作等
    public class SysCtrl
    {
     
        /// <summary>
        /// 初始化系统配置
        /// </summary>
        public static void Init()
        {
            GetLocalIPList();
            if (SysConfig.IPList.Count > 0)
                SysConfig.SetLocalIPInfo(0);
            DeviceCtrl.InitDtDevice();
            NetworkCtrl.InitDataTableNetwork();
        }

        /// <summary>
        /// 获取本地IP地址
        /// </summary>
        /// <returns>返回列表</returns>
        public static void GetLocalIPList()
        {
            string AddressIP = string.Empty;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection nics = mc.GetInstances();
            foreach (ManagementObject nic in nics)
            {
                int i = 0;
                if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                {
                    try
                    {
                        IPInfo ipInfo = new IPInfo((nic["IPAddress"] as String[])[0], (nic["DefaultIPGateway"] as String[])[0],
                            (nic["IPSubnet"] as String[])[0]);
                        SysConfig.IPList.Add(i++, ipInfo);
                    }
                    catch { continue; }
                }
            }
        }


        /// <summary>
        /// 测试IP是否有效
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private IPAddress getValidIP(string ip)
        {
            IPAddress lip = null;

            try
            {
                //是否为空 
                if (!IPAddress.TryParse(ip, out lip))
                {
                    throw new ArgumentException(
                    "IP无效，不能启动DUP");
                }
            }
            catch (Exception e)
            {
                //ArgumentException,  
                //FormatException,  
                //OverflowException 
                Console.WriteLine("无效的IP：" + e.ToString());
                return null;
            }
            return lip;
        }



        /// <summary>
        /// 测试端口号是否有效 
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private int getValidPort(string port)
        {
            int lport;
            try
            {
                //是否为空 
                if (port == "")
                {
                    throw new ArgumentException(
                    "端口号无效，不能启动DUP");
                }
                lport = System.Convert.ToInt32(port);
            }
            catch (Exception e)
            {
                //ArgumentException,  
                //FormatException,  
                //OverflowException 
                Console.WriteLine("无效的端口号：" + e.ToString());

                return -1;
            }
            return lport;
        }

        /// <summary> 
        /// 获得广播地址 
        /// </summary> 
        /// <param name="ipAddress">IP地址</param> 
        /// <param name="subnetMask">子网掩码</param> 
        /// <returns>广播地址</returns> 
        public static string GetBroadcast(string ipAddress, string subnetMask)
        {

            byte[] ip = IPAddress.Parse(ipAddress).GetAddressBytes();
            byte[] sub = IPAddress.Parse(subnetMask).GetAddressBytes();

            // 广播地址=子网按位求反 再 或IP地址 
            for (int i = 0; i < ip.Length; i++)
            {
                ip[i] = (byte)((~sub[i]) | ip[i]);
            }
            return new IPAddress(ip).ToString();
        }


        /// <summary>
        /// 注册接收RJ45回调数据
        /// </summary>
        /// <param name="key">回调键</param>
        /// <param name="callback">回调函数</param>
        /// <param name="actionCount">回调次数</param>
        public static void AddRJ45CallBackList(byte[] _key, string uuid, CallbackFromUDP callback, long actionCount)
        {
            lock (SysConfig.RJ45CallBackList)
            {
                callback.ActionCount = actionCount;
                string key = ConvertTools.ByteToHexStr(_key) + "_" + uuid;
                if (!SysConfig.RJ45CallBackList.ContainsKey(key))
                    SysConfig.RJ45CallBackList.Add(key, callback);
                else
                {
                    SysConfig.RJ45CallBackList[key] = callback;  //----暂时只用于单个事件订阅,所以直接覆盖------
                }
            }
        }

        /// <summary>
        /// 注册接收RJ45回调数据
        /// </summary>
        /// <param name="key">回调键</param>
        /// <param name="callback">回调函数</param>
        /// <param name="actionCount">回调次数</param>
        public static void AddRJ45CallBackList(byte[] _key, string uuid, CallbackFromUDP callback)
        {
            lock (SysConfig.RJ45CallBackList)
            {
                string key = ConvertTools.ByteToHexStr(_key) + "_" + uuid;
                if (!SysConfig.RJ45CallBackList.ContainsKey(key))
                    SysConfig.RJ45CallBackList.Add(key, callback);
                else
                {
                    SysConfig.RJ45CallBackList[key] = callback;  //----暂时只用于单个事件订阅,所以直接覆盖------
                }
            }
        }

        /// <summary>
        /// 移除回调列表,所有指令类型的回调对象
        /// </summary>
        /// <param name="_key">指令头</param>
        public static void RemoveRJ45CallBackList(byte[] _key,string id)
        {
            lock (SysConfig.RJ45CallBackList)
            {  
                if (SysConfig.RJ45CallBackList.ContainsKey(_key + "_" + id))
                    SysConfig.RJ45CallBackList.Remove(_key + "_" + id);
            }
        }

        /// <summary>
        /// 移除回调列表,所有指令类型的回调对象
        /// </summary>
        /// <param name="_key">指令头</param>
        public static void RemoveRJ45CallBackList(byte[] _key)
        {
            lock (SysConfig.RJ45CallBackList)
            {
                string key = ConvertTools.ByteToHexStr(_key);
                List<string> delList = new List<string>();
                foreach (string keyStr in SysConfig.RJ45CallBackList.Keys)
                {
                    if (keyStr.StartsWith(key))// && keyStr.Length>_key.Length)
                        delList.Add(keyStr);
                }
                foreach (string delKey in delList)
                    SysConfig.RJ45CallBackList.Remove(delKey);
            }
        }

        /// <summary>
        /// 添加到回调表
        /// </summary>
        /// <param name="key">命令</param>
        /// <param name="callback">回调对象</param>
        public static void AddRJ45CallBackList(byte[] key, CallbackFromUDP callback)
        {
            lock (SysConfig.RJ45CallBackList)
            {
                string keyStr = ConvertTools.ByteToHexStr(key);
                if (!SysConfig.RJ45CallBackList.ContainsKey(keyStr))
                    SysConfig.RJ45CallBackList.Add(keyStr, callback);
                else
                {
                    SysConfig.RJ45CallBackList[keyStr] = callback;  //----暂时只用于单个事件订阅,所以直接覆盖------

                }
            }
        }
    }
}
