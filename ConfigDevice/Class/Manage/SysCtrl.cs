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
            InitDtDevice();
            InitDataTableNetwork();
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
        /// 初始化网络数据
        /// </summary>
        public static void InitDataTableNetwork()
        {
            //----初始化表结构-------
            if (SysConfig.DtNetwork.Columns.Count == 0)
            {
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_DEVICE_ID, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_NETWORK_ID, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_STATE, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_DEVICE_NAME, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_MAC, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_IP, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_PORT, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_REMARK, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_PC_ADDRESS, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_KINDNAME, System.Type.GetType("System.String"));

            }
            SysConfig.DtNetwork.Clear(); SysConfig.DtNetwork.AcceptChanges();//---初始化数据----
        }

        /// <summary>
        /// 获取指令配置
        /// </summary>
        /// <param name="kindId">类型</param>
        /// <returns></returns>
        public static ViewCommandControl GetViewCommandControl(ControlObj controlObj, GridView gv)
        {
            //string typeName = controlObj.GetType().ToString();
            //switch (typeName)
            //{
            //    case "ConfigDevice.Background": return new ViewBackgroundControl(controlObj, gv);
            //    case "ConfigDevice.Circuit": return new ViewCircuitControl(controlObj, gv);
            //    case "ConfigDevice.Messages": return new ViewMessagesControl(controlObj, gv);
            //    case "ConfigDevice.Motor": return new ViewMotorControl(controlObj, gv);
            //    case "ConfigDevice.Scene": return new ViewSceneControl(controlObj, gv);
            //    case "ConfigDevice.Server": return new ViewServerControl(controlObj, gv);
            //    case "ConfigDevice.Swit": return new ViewSwitControl(controlObj, gv);
            //    case "ConfigDevice.Timing": return new ViewTimingControl(controlObj, gv);

            //    default: return null;
            //}

            if (controlObj is ConfigDevice.Background)
                return new ViewBackgroundControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Circuit)
                return new ViewCircuitControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Messages)
                return new ViewMessagesControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Motor)
                return new ViewMotorControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Scene)
                return new ViewSceneControl(controlObj, gv);
            else if (controlObj is ConfigDevice.ServerControlObj)
                return new ViewServerControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Swit)
                return new ViewSwitControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Timing)
                return new ViewTimingControl(controlObj, gv);
            
            
            else return null;

        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void InitDtDevice()
        {
            //----初始化表结构-------
            if (SysConfig.DtDevice.Columns.Count == 0)
            {
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_NUM, System.Type.GetType("System.Int16"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_ID, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_NETWORK_ID, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_KIND_ID, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_NAME, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_MAC, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_STATE, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_REMARK, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_SOFTWARE_VER, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_HARDWARE_VER, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PC_ADDRESS, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_NETWORK_IP, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_ADDRESS_NAME, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_ADDRESS_ID, System.Type.GetType("System.String"));

                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PARAMETER1, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PARAMETER2, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PARAMETER3, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PARAMETER4, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PARAMETER5, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_IMAGE1, System.Type.GetType("System.Byte[]"));
            }
        }

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="deviceData">设备数据</param>
        public static void AddDeviceData(DeviceData device)
        {             
            int num = SysConfig.DtDevice.Select(DeviceConfig.DC_KIND_ID + 
                " not in ('" + (int)DeviceConfig.EQUIPMENT_RJ45 + "', '"+ (int)DeviceConfig.EQUIPMENT_SERVER + "')" ).Length+1;
            if (device.ByteKindID == DeviceConfig.EQUIPMENT_SERVER || device.ByteKindID == DeviceConfig.EQUIPMENT_RJ45)
                num = 1000 + num;
            DataRow drInsert = SysConfig.DtDevice.Rows.Add(new object[] {num.ToString(),device.DeviceID,device.NetworkID,
                            device.KindID, device.KindName,device.Name,device.MAC,device.State,device.Remark,"","",device.PCAddress,
                            device.NetworkIP,device.AddressName,device.AddressID});
            drInsert[DeviceConfig.DC_PARAMETER1] = DeviceConfig.STATE_OPEN_LIGHT;
            drInsert[DeviceConfig.DC_IMAGE1] = ImageHelper.ImageToBytes(global::ConfigDevice.Properties.Resources.on);

            drInsert.AcceptChanges();
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="deviceData">设备数据</param>
        public static void RemoveNetworkDeviceData(Network network)
        {
            DataTable temp = SysConfig.DtDevice.Copy();
            foreach (DataRow dr in temp.Rows)
            {
                if (dr[DeviceConfig.DC_NETWORK_IP].ToString() == network.NetworkIP)
                    dr.Delete();
            }
            temp.AcceptChanges();
            SysConfig.DtDevice = temp;
            //temp.AcceptChanges();
            //SysConfig.DtDevice = temp.Copy();
            //int delIndex = -1;
            //foreach (DataRow dr in SysConfig.DtDevice.Rows)
            //{
            //    if (dr[DeviceConfig.DC_MAC].ToString() == network.MAC)
            //    { delIndex = SysConfig.DtDevice.Rows.IndexOf(dr); break; }
            //}
            //if (delIndex != -1)
            //    SysConfig.DtDevice.Rows.RemoveAt(delIndex);
        }

        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="device">设备</param>
        public static void UpdateDeviceData(DeviceData device)
        {
            string temp = DeviceConfig.DC_MAC + "='" + device.MAC + "'";
            DataRow[] rows = SysConfig.DtDevice.Select(temp);
            foreach (DataRow dr in SysConfig.DtDevice.Rows)
            {
                if (dr[DeviceConfig.DC_MAC].ToString() == device.MAC)
                {
                    dr.BeginEdit();

                    dr[DeviceConfig.DC_ID] = device.DeviceID;
                    dr[DeviceConfig.DC_NETWORK_ID] = device.NetworkID;
                    dr[DeviceConfig.DC_KIND_ID] = device.KindID;
                    dr[DeviceConfig.DC_KIND_NAME] = device.KindName;
                    dr[DeviceConfig.DC_NAME] = device.Name;
                    dr[DeviceConfig.DC_STATE] = device.State;
                    dr[DeviceConfig.DC_REMARK] = device.Remark;
                    dr[DeviceConfig.DC_PC_ADDRESS] = device.PCAddress;
                    dr[DeviceConfig.DC_NETWORK_IP] = device.NetworkIP;
                    dr[DeviceConfig.DC_ADDRESS_NAME] = device.AddressName;

                    dr.EndEdit();
                    dr.AcceptChanges();
                    return;
                }
            }

        }

        /// <summary>
        /// 注册接收RJ45回调数据
        /// </summary>
        /// <param name="key">回调键</param>
        /// <param name="callback">回调函数</param>
        /// <param name="actionCount">回调次数</param>
        //public static void AddRJ45CallBackList(byte[] _key, CallbackFromUDP callback, long actionCount)
        //{
        //    lock (SysConfig.RJ45CallBackList)
        //    {
        //        callback.ActionCount = actionCount;
        //        string key = ConvertTools.ByteToHexStr(_key);
        //        if (!SysConfig.RJ45CallBackList.ContainsKey(key))
        //            SysConfig.RJ45CallBackList.Add(key, callback);
        //        else
        //        {
        //            SysConfig.RJ45CallBackList[key] = callback;  //----暂时只用于单个事件订阅,所以直接覆盖------
        //        }
        //    }
        //}
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
        /// 移除回调列表,所有指令类型的回调对象
        /// </summary>
        /// <param name="_key">指令头</param>
        //public static void RemoveRJ45CallBackList(byte[] _key)
        //{
        //    lock (SysConfig.RJ45CallBackList)
        //    {
        //        string key = ConvertTools.ByteToHexStr(_key);
        //        List<string> delList = new List<string>();
        //        foreach (string keyStr in SysConfig.RJ45CallBackList.Keys)
        //        {
        //            if (keyStr.StartsWith(key))
        //                delList.Add(keyStr);
        //        }
        //        foreach (string delKey in delList)
        //            SysConfig.RJ45CallBackList.Remove(delKey);
        //    }
        //}

        /// <summary>
        /// 添加到回调表
        /// </summary>
        /// <param name="key">命令</param>
        /// <param name="callback">回调对象</param>
        public static void AddRJ45CallBackList(byte[] key, CallbackFromUDP callback)
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
