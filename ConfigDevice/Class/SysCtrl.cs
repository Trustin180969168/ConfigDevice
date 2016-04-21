using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Management;


namespace ConfigDevice
{

    //
    // 摘要:SysConfig.cs
    //     用于保存系统的基本配置信息,包括公共常量,本地IP,端口,初始化工作等
    public  class SysCtrl
    {

        /// <summary>
        /// 初始化系统配置
        /// </summary>
        public static void Init()
        {
            GetLocalIPList();
            if (SysConfig.IPList.Count > 0)
                SysConfig.SetLocalIPInfo(0);
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
        /// 刷新设备列表
        /// </summary>
        /// <param name="device">设备</param>
        public static void RefreshDevices(Device device)
        {
            string cdnStr = DeviceConfig.DC_MAC + "='" + device.MAC + "'";
            DataRow[] rows = SysConfig.DtDevice.Select(cdnStr);
            if (rows.Length == 0) return;
            DataRow dr = rows[0];
            dr[DeviceConfig.DC_DEVICE_ID] = device.DeviceID;
            dr[DeviceConfig.DC_NETWORK_ID] = device.NetworkID;
            dr[DeviceConfig.DC_KIND_NAME] = device.KindName;
            dr[DeviceConfig.DC_NAME] = device.Name;
            dr[DeviceConfig.DC_STATE] = device.State;
            dr[DeviceConfig.DC_ADDRESS] = device.AddressName;
            SysConfig.DtDevice.AcceptChanges();

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
        /// 获取抽象工厂
        /// </summary>
        /// <param name="kindId">类型</param>
        /// <returns></returns>
        public static IFactory GetFactory(byte kindId)
        {
            switch (kindId)
            {
                case DeviceConfig.EQUIPMENT_AMP_MP3:
                case DeviceConfig.EQUIPMENT_RJ45: return new FactoryBaseDevice();
                //   case DeviceConfig.EQUIPMENT_DOOR_IN_4: return new FactoryFourInput();
                default: return new FactoryBaseDevice();
            }
        }

    }
}
