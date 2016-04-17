using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Threading;


namespace SocketTest
{

    //
    // 摘要:SysConfig.cs
    //     用于保存系统的基本配置信息,包括公共常量,本地IP,端口,初始化工作等
    public class SysCtrl
    {         
        //----------网络搜索指令------------------
        private static string cmdSearchNetworkStr = "";
        public static string CmdSearchNetworkStr { get { return cmdSearchNetworkStr; } }

        private static string portId = " 1C 25 ";//端口
        public static string PortID{get {return portId;}}

        public Socket mySocket;
        public bool RunningFlag = false;
        public IPAddress ip;
        public int port = 9500;


        
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
