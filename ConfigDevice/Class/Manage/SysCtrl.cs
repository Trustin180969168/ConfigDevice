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
            dr[DeviceConfig.DC_ID] = device.DeviceID;
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
        /// 获取抽象设备编辑
        /// </summary>
        /// <param name="kindId">类型</param>
        /// <returns></returns>
        public static IFactoryDeviceEdit GetFactoryDeviceEdit(byte kindId)
        {
            switch (kindId)
            {
                case DeviceConfig.EQUIPMENT_AMP_MP3:
                case DeviceConfig.EQUIPMENT_RJ45: return new FactoryBaseDeviceEdit();
                case DeviceConfig.EQUIPMENT_DOOR_IN_4: return new FactoryDoor4InputEdit();
                default: return new FactoryBaseDeviceEdit();
            }
        }

        /// <summary>
        /// 获取抽象设备
        /// </summary>
        /// <param name="kindId">类型</param>
        /// <returns></returns>
        public static IFactoryDevice CreateDevice(byte kindId)
        {
            switch (kindId)
            {
                case DeviceConfig.EQUIPMENT_AMP_MP3: return new FactoryAmplifier();
                case DeviceConfig.EQUIPMENT_DOOR_IN_4: return new FactoryDoorInput4();
                case DeviceConfig.EQUIPMENT_CURTAIN_3CH: return new FactoryRoad3Window();
                case DeviceConfig.EQUIPMENT_SWIT_4: return new FactoryRoad4Relay();//4路继电器
                case DeviceConfig.EQUIPMENT_SWIT_8: return new FactoryRoad8Relay();//6路继电器
                case DeviceConfig.EQUIPMENT_SWIT_6: return new FactoryRoad6Relay();//8路继电器
                case DeviceConfig.EQUIPMENT_TRAILING_2: return new FactoryRoad2FrontDimming();//2路前沿调光
                case DeviceConfig.EQUIPMENT_TRAILING_4: return new FactoryRoad4FrontDimming();//4路前沿调光
                case DeviceConfig.EQUIPMENT_TRAILING_6: return new FactoryRoad6FrontDimming();//6路前沿调光
                case DeviceConfig.EQUIPMENT_TRAILING_8: return new FactoryRoad8FrontDimming();//8路前沿调光
                case DeviceConfig.EQUIPMENT_TRAILING_12: return new FactoryRoad12FrontDimming();//12路前沿调光
                case DeviceConfig.EQUIPMENT_SERVER: return new FactoryServers();//12路前沿调光

                default: return new FactoryBaseDevice();
            }
        }


        /// <summary>
        /// 获取指令配置
        /// </summary>
        /// <param name="kindId">类型</param>
        /// <returns></returns>
        public static ViewCommandControl GetViewCommandControl(ControlObj controlObj, GridView gv)
        {
            string typeName = controlObj.GetType().ToString();
            switch (typeName)
            {
                case "ConfigDevice.Background": return new ViewBackgroundControl(controlObj, gv);
                case "ConfigDevice.Circuit": return new ViewCircuitControl(controlObj, gv);
                case "ConfigDevice.Messages": return new ViewMessagesControl(controlObj, gv);
                case "ConfigDevice.Motor": return new ViewMotorControl(controlObj, gv);
                case "ConfigDevice.Scene": return new ViewSceneControl(controlObj, gv);
                case "ConfigDevice.Server": return new ViewServerControl(controlObj, gv);
                case "ConfigDevice.Swit": return new ViewSwitControl(controlObj, gv);
                case "ConfigDevice.Timing": return new ViewTimingControl(controlObj, gv);

                default: return null;
            }
        }

    }
}
