using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Threading;


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
        public void Init()
        {


            //启动一个新的线程，执行方法this.ReceiveHandle，  
            //以便在一个独立的进程中执行数据接收的操作  
            //RunningFlag = true;
            //Thread thread = new Thread(new ThreadStart(this.ReceiveHandle));
            //thread.Start();




            //string[] arr = SysConfig.LocalIP.ToString().Split(".");
            //int value = Convert.ToInt16(arr[3].ToString());
            //int hValue = (value >> 8) & 0xFF;
            //int lValue = value & 0xFF;
            //byte[] data = new byte[] { (byte)hValue, (byte)lValue };
        }


        /// <summary>
        /// 刷新设备列表
        /// </summary>
        /// <param name="device">设备</param>
        public static void RefreshDevices(Device device)
        {
            string cdnStr = DeviceConfig.DC_MAC + "='" + device.MAC + "'";
            DataRow[] rows = SysConfig.DtDevice.Select(cdnStr);
            if (rows.Length == 0)        return;
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



    }
}
