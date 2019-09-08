﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class NetworkCtrl
    {
        private object lockUpdateObj = new object();
        private NetworkCtrl() { }
        private static readonly NetworkCtrl instance = new NetworkCtrl();
        public static NetworkCtrl GetInstance()
        {
            return instance;
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
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_KIND_ID, System.Type.GetType("System.String"));
                SysConfig.DtNetwork.Columns.Add(NetworkConfig.DC_KINDNAME, System.Type.GetType("System.String"));
            }
            SysConfig.DtNetwork.Clear(); SysConfig.DtNetwork.AcceptChanges();//---初始化数据----
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="deviceData">设备数据</param>
        public void RemoveNetworkDeviceData(Network network)
        {
            lock (this.lockUpdateObj)
            {
                DataTable temp = SysConfig.DtDevice.Copy();
                foreach (DataRow dr in temp.Rows)
                {
                    if (dr[DeviceConfig.DC_NETWORK_IP].ToString() == network.NetworkIP)
                        dr.Delete();
                }
                temp.AcceptChanges();
                SysConfig.DtDevice = temp;
            }
        }

        /// <summary>
        /// 根据数据更新网络设备表
        /// </summary>
        /// <param name="network">RJ45</param>
        public void UpdateNetworkDataTable(Network network)
        { 
            lock (lockUpdateObj)
            {
                foreach (DataRow dr in SysConfig.DtNetwork.Rows)
                {
                    if (dr[NetworkConfig.DC_MAC].ToString() == network.MAC)
                    {
                        dr[NetworkConfig.DC_DEVICE_ID] = network.DeviceID;
                        dr[NetworkConfig.DC_NETWORK_ID] = network.NetworkID;
                        dr[NetworkConfig.DC_STATE] = network.State;
                        dr[NetworkConfig.DC_DEVICE_NAME] = network.DeviceName;
                        dr[NetworkConfig.DC_PORT] = network.Port;
                        dr[NetworkConfig.DC_IP] = network.NetworkIP;
                        dr[NetworkConfig.DC_PC_ADDRESS] = network.PCAddress;

                        dr.AcceptChanges();
                        break;
                    }
                }
            } 
        } 

    }
}