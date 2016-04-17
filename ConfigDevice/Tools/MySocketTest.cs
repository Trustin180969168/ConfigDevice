using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Data;
using System.Threading;


namespace ConfigDevice
{

    public class MySocketTest
    {
        private Socket mySocket;//Socket对象
        private static readonly Object obj = new Object();//加锁对象
        private static UInt16 sendCount = 0;
        public int Available { get { return mySocket.Available; } }//当前可用的所有UDP接收数据长度
        /// <summary>
        /// 单例模式
        /// </summary>
        private MySocketTest()
        {
            IPEndPoint ipLocalPoint = new IPEndPoint(SysConfig.LocalIP, SysConfig.LocalPort);
            //定义网络类型，数据连接类型和网络协议UDP  
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            mySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            mySocket.Bind(ipLocalPoint);//绑定网络地址  
        }
        private static readonly MySocketTest instance = new MySocketTest();
        public static MySocketTest GetInstance()
        {
            return instance;
        }


        /// <summary>
        /// 获取发送次数
        /// </summary>
        /// <returns>Int16</returns>
        public static Int16 GetSendCount()
        {
            return (Int16)sendCount;
        }


        /// <summary>
        /// 发送数据,接收为封装好格式的通讯包
        /// </summary>
        /// <param name="data">发送的数据包</param>
        /// <param name="receiveIP">接收地址</param>
        /// <param name="receivePort">接收端口</param>
        /// <returns>UdpData</returns>
        public UdpData GetData(byte[] data, string receiveIP, int receivePort)
        {
            lock (obj)
            {
                IPAddress ip = getValidIP(receiveIP);
                IPEndPoint ipep = new IPEndPoint(ip, receivePort);
                EndPoint remotePoint = (EndPoint)(ipep);
                //-------发送前,把包标识码加1-------
                byte[] packageCount = BitConverter.GetBytes(++sendCount);
                Buffer.BlockCopy(packageCount, 0, data, 18, 2);

                mySocket.SendTo(data, data.Length, SocketFlags.None, remotePoint);
                return ReceiveData(remotePoint);
            }
        }

        /// <summary>
        /// 发送数据,接收为封装好格式的通讯包
        /// </summary>
        /// <param name="data">发送的数据包</param>
        /// <param name="receiveIP">接收地址</param>
        /// <param name="receivePort">接收端口</param>
        /// <returns>void</returns>
        public void SendData(byte[] data, string receiveIP, int receivePort)
        {
            lock (obj)
            {
                IPAddress ip = getValidIP(receiveIP);
                IPEndPoint ipep = new IPEndPoint(ip, receivePort);
                EndPoint remotePoint = (EndPoint)(ipep);
                //-------发送前,把包标识码加1-------
                byte[] packageCount = BitConverter.GetBytes(++sendCount);
                Buffer.BlockCopy(packageCount, 0, data, 18, 2);

                mySocket.BeginSendTo(data, 0, data.Length, SocketFlags.None, remotePoint, new AsyncCallback(SendCallback), null);
            }
        }
        /// <summary>
        /// 异步发送消息回调函数
        /// </summary>
        /// <param name="asyncResult"></param>
        public void SendCallback(IAsyncResult asyncResult)
        {
            //消息发送完成
            if (asyncResult.IsCompleted)
            {
                mySocket.EndSendTo(asyncResult);
            }
        }
     

        /// <summary>
        /// 发送数据,接收为封装好格式的通讯包列表
        /// </summary>
        /// <param name="data">发送的数据包</param>
        /// <param name="receiveIP">接收地址</param>
        /// <param name="receivePort">接收端口</param>
        /// <param name="receivePort">接收次数</param>
        /// <returns>List<UdpData></returns>
        public List<UdpData> GetData(byte[] data, string receiveIP, int receivePort, int receiveCount)
        {
            lock (obj)
            {
                IPAddress ip = getValidIP(receiveIP);
                IPEndPoint ipep = new IPEndPoint(ip, receivePort);
                EndPoint remotePoint = (EndPoint)(ipep);
                //-------发送前,把包标识码加1-------
                byte[] packageCount = BitConverter.GetBytes(++sendCount);
                Buffer.BlockCopy(packageCount, 0, data, 18, 2);
                List<UdpData> list = new List<UdpData>();
         
                mySocket.SendTo(data, data.Length, SocketFlags.None, remotePoint);
                //mySocket.BeginSendTo(data, 0, data.Length, SocketFlags.None, remotePoint, null, null);
                
                //while (receiveCount-- > 0)
                //{
                //    UdpData udpData = ReceiveData(remotePoint);
                //    if (data != null && !list.Contains(udpData)) 
                //        list.Add(udpData);

                //    Thread.Sleep(200);//等待数据返回
                //}
                //return list;

                return list;
            }
        }    

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="remotePoint">接收端</param>
        public UdpData ReceiveData(EndPoint remotePoint)
        {
            lock (obj)
            {
                byte[] data = new byte[128];
              
                if (mySocket.Available < 1) return null;//---没有数据返回空---
                int rlen = mySocket.ReceiveFrom(data, ref remotePoint);  //接收UDP数据报，引用参数remotePoint获得源地址  

                Array.Resize(ref data, rlen);//重新设定长度  
                string dataStr = ConvertTools.ByteToHexStr(data);

                if (rlen > 128) return null;//无效包数据
                if (rlen == 0) return null;//无效包数据

                UdpData udpData = new UdpData(data);//UDP数据对象     
                IPAddress ip = IPAddress.Parse(remotePoint.ToString().Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                int port = Int16.Parse(remotePoint.ToString().Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1]);
                udpData.IPPoint = new IPEndPoint(ip, port);

                return udpData;
            }
        }


        /// <summary>
        /// 关闭Socket
        /// </summary>
        public void Close()
        {
            mySocket.Close();
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


       





    }
}
