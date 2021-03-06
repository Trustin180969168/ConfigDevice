﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Data;
using System.Threading;
using System.IO;


namespace ConfigDevice
{



    /// <summary>
    /// 单例服务类
    /// </summary>
    public class MySocket
    {
        private Socket mySocket;//Socket对象
        private static readonly Object obj = new Object();//加锁对象
        private static readonly Object lockReceive = new Object();//加锁对象
        private static UInt16 sendCount = 0;
        private static Dictionary<string, CallbackFromUdp> pcCallBackList = new Dictionary<string, CallbackFromUdp>();//---PC回调列表----
        private static Dictionary<string, UdpData> rj45SendList = new Dictionary<string, UdpData>();//---RJ45发送列表---- 

        public int Available { get { return mySocket.Available; } }//当前可用的所有UDP接收数据长度
        private static Thread receiveThread;//----接收线程-----   

        /// <summary>
        /// 获取发送列表
        /// </summary>
        public  Dictionary<string, UdpData> RJ45SendList { get { return rj45SendList; } }

        /// <summary>
        /// 获取发送列表
        /// </summary>
        public Dictionary<string, CallbackFromUdp> PCCallBackList { get { return pcCallBackList; } }


        /// <summary>
        /// 单例模式
        /// </summary>
        private MySocket()
        {
            IPEndPoint ipLocalPoint = new IPEndPoint(SysConfig.LocalIP, SysConfig.LocalPort);
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); //定义网络类型，数据连接类型和网络协议UDP  
            mySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);//允许广播
            mySocket.Bind(ipLocalPoint);//绑定网络地址  
            receiveThread = new Thread(new ThreadStart(receiveHandle));
            receiveThread.Start();
        }
        private static readonly MySocket instance = new MySocket();
        public static MySocket GetInstance()
        {
            return instance;
        }



        /// <summary>
        /// 删除发送列表
        /// </summary>
        /// <param name="key"></param>
        public void RemoveRJ45SendList(string key)
        {
            lock (rj45SendList)
            {
                rj45SendList.Remove(key);
            }
        }

        /// <summary>
        /// 加RJ45发送列表
        /// </summary>
        /// <param name="key"></param>
        public void AddRJ45SendList(string key, UdpData udp)
        {
            lock (rj45SendList)
            {
                rj45SendList.Add(key, udp);
            }
        }

        /// <summary>
        /// 删除回调列表
        /// </summary>
        /// <param name="key"></param>
        public void RemovePCCallbackList(string key)
        {
            lock (pcCallBackList)
            {
                pcCallBackList.Remove(key);
            }
        }

        /// <summary>
        /// 加回调列表
        /// </summary>
        /// <param name="key">包标识</param>
        /// <param name="state">回调内容</param>
        public void AddPCCallbackList(string key, CallbackFromUdp state)
        {
            lock (pcCallBackList)
            {
                pcCallBackList.Add(key, state);
            }
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
        /// 回复包,不用返回
        /// </summary>
        /// <param name="udp">回复数据</param>
        /// <param name="receiveIP">接收IP</param>
        /// <param name="receivePort">接收接口</param>
        public void ReplyData(UdpData udp, string receiveIP, int receivePort)
        {
            //lock (obj)
            //{
                IPAddress ip = getValidIP(receiveIP);
                IPEndPoint ipep = new IPEndPoint(ip, receivePort);
                EndPoint remotePoint = (EndPoint)(ipep);
                CallbackFromUdp state = new CallbackFromUdp(udp, null, null,null);//创建返回结果
                mySocket.BeginSendTo(udp.GetUdpData(), 0, udp.Length, SocketFlags.None, remotePoint, new AsyncCallback(SendCallback), state);//--异步发送--- 

            //}
        }

        /// <summary>
        /// 发送包,不用收回复
        /// </summary>
        /// <param name="udp">恢复数据</param>
        /// <param name="receiveIP">接收IP</param>
        /// <param name="receivePort">接收接口</param>
        public void SendData(UdpData udp, string receiveIP, int receivePort)
        {
            //lock (obj)
            //{
                IPAddress ip = getValidIP(receiveIP);
                IPEndPoint ipep = new IPEndPoint(ip, receivePort);
                EndPoint remotePoint = (EndPoint)(ipep);
                //-------发送前,把包标识码加1-------
                byte[] packageCount = BitConverter.GetBytes(++sendCount);
                Buffer.BlockCopy(packageCount, 0, udp.PacketCode, 0, 2);
         
                mySocket.BeginSendTo(udp.GetUdpData(), 0, udp.Length, SocketFlags.None, remotePoint, new AsyncCallback(SendCallback), null);//--异步发送--- 
            //}
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">发送的数据包</param>
        /// <param name="receiveIP">接收地址</param>
        /// <param name="receivePort">接收端口</param>
        /// <param name="receivePort">接收参数</param>
        /// <returns>void</returns>
        public void SendData(UdpData udp, string receiveIP, int receivePort, CallBackUdpAction callback, object[] objs)
        {
            //lock (obj)
            //{
                IPAddress ip = getValidIP(receiveIP);
                IPEndPoint ipep = new IPEndPoint(ip, receivePort);
                EndPoint remotePoint = (EndPoint)(ipep);
                udp.IPPoint = new IPEndPoint(SysConfig.LocalIP, SysConfig.LocalPort);//发送的IP和端口
                SendData(udp, remotePoint, callback, objs);    
            //}
        }

        /// <summary>
        /// 发送数据,接收为封装好格式的通讯包
        /// </summary>
        /// <param name="data">发送的数据包</param>
        /// <param name="remotePoint">网络地址</param>
        /// <returns>void</returns>
        public void SendData(UdpData udp, EndPoint remotePoint, CallBackUdpAction callback, object[] objs)
        {
            //-------发送前,把包标识码加1-------
            byte[] packageCount = BitConverter.GetBytes(++sendCount);
            Buffer.BlockCopy(packageCount, 0, udp.PacketCode, 0, 2);
            CallbackFromUdp state = new CallbackFromUdp(udp, callback, remotePoint, objs);//创建返回结果
            mySocket.BeginSendTo(udp.GetUdpData(), 0, udp.Length, SocketFlags.None, remotePoint, new AsyncCallback(SendCallback), state);//--异步发送--- 
        }

        /// <summary>
        /// 异步发送消息回调函数
        /// </summary>
        /// <param name="asyncResult"></param>
        public void SendCallback(IAsyncResult asyncResult)
        {
            //消息发送完成,添加到回复表中
            if (asyncResult.IsCompleted)
            {
                if (asyncResult.AsyncState != null)
                {
                    CallbackFromUdp state = (CallbackFromUdp)asyncResult.AsyncState;
                    if (state.Udp.PacketKind[0] == PackegeSendReply.SEND)//----发送包添加到回复列表---
                        AddPCCallbackList(state.Udp.PacketCodeStr, state);
                    else
                        RemoveRJ45SendList(state.Udp.PacketCodeStr);//----回复包移除对相应数据接收列表
                }
                mySocket.EndSendTo(asyncResult);
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="remotePoint">接收端</param>
        private  UdpData ReceiveData(EndPoint remotePoint)
        {
            byte[] data = new byte[SysConfig.MAX_DATA_SIZE];
            int rlen = mySocket.ReceiveFrom(data, ref remotePoint);  //接收UDP数据报，引用参数remotePoint获得源地址  
            if (rlen > SysConfig.MAX_DATA_SIZE) return null;//无效包数据
            if (rlen < SysConfig.MIN_DATA_SIZE) return null;//无效包数据
            Array.Resize(ref data, rlen);//重新设定长度

            UdpData udpData = new UdpData(data);//UDP数据对象     
            IPAddress ip = IPAddress.Parse(remotePoint.ToString().Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0]);
            int port = Int16.Parse(remotePoint.ToString().Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1]);
            udpData.IPPoint = new IPEndPoint(ip, port);//------获取得到远程IP和端口-------

            return udpData;
        }

        /// <summary>
        /// 接收线程,UDP包具有不可靠性,发送和接收不一致,包接收要统一操作
        /// </summary>
        private void receiveHandle()
        {
            //lock (pcCallBackList)
            //{
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, SysConfig.RemotePort);//获取所有地址为自远程端口的值
                EndPoint remotePoint = (EndPoint)(ipep);
                CallbackFromUdp state;//回调对象
                while (true)
                {
                    if (mySocket == null || mySocket.Available < 1)//----等待数据接收-----
                    { Thread.Sleep(20); continue; }
                    UdpData udpReceive = ReceiveData(remotePoint);//-----接收UDP数据------
                    if (udpReceive.PacketKind[0] == PackegeSendReply.REPLY)//------回复的UDP-----
                    {
                        if (pcCallBackList.ContainsKey(udpReceive.PacketCodeStr))//----找出对应的回复----
                            state = pcCallBackList[udpReceive.PacketCodeStr];
                        else
                        {
                            RemovePCCallbackList(udpReceive.PacketCodeStr);//---删除无用的回复包-----
                            continue;//----没有对应请求,获取下一个udp -------                         
                        }
                        if (crcUdpData(udpReceive)) //---校验是否是错误的包(目前只判断回复包)-------- 
                        {                           
                            state.ActionCallback(udpReceive, state.Values);//----开启异步线程回调----                               
                            if (!isBroadCastData(state.Udp)) //-----如果发送的是广播包,不能删除-----
                                RemovePCCallbackList(udpReceive.PacketCodeStr);//---删除已经回调的----------
                        }
                        else//----错误重发----
                        {
                            RemovePCCallbackList(udpReceive.PacketCodeStr);
                            SendData(state.Udp, state.RemotePoint, state.getCallBackAction, state.Values);
                        }
                    }
                    else if (udpReceive.PacketKind[0] == PackegeSendReply.SEND)//-------添加到RJ45设备发送表---------
                    {
                        UserUdpData userData = new UserUdpData(udpReceive);//----从UDP协议包中分离出用户协议数据-----
                        AddRJ45SendList(udpReceive.PacketCodeStr, udpReceive);//包标识和命令作为键

                        byte[] key = findKey(userData.Command);
                        if (key != null)//------是否存在被动回调列表----
                        {
                            state = SysConfig.RJ45CallBackList[key];   
                            state.ActionCallback(udpReceive, state.Values);//----开启异步线程回调----        
                        }
                    }
                }
            //}
        }

        private byte[] findKey(byte[] keyValue)
        {
            foreach(byte[] key in SysConfig.RJ45CallBackList.Keys)
                if (CommonTools.BytesEuqals(key, keyValue))
                    return key;
            return null;
        }

        /// <summary>
        /// 判断是否广播包
        /// </summary>
        /// <param name="data">udp包</param>
        /// <returns></returns>
        private bool isBroadCastData(UdpData data)
        {
            UserUdpData userData = new UserUdpData(data);
            if (userData.Target[0] == 0xFF)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据CRC校验码,判断UDP包错误
        /// </summary>
        /// <param name="udp"></param>
        /// <returns></returns>
        private bool crcUdpData(UdpData udp)
        {
            if (udp.Length < UdpDataConfig.MIN_USER_DATA_SIZE) return true;//---回复包直接通过--
            UserUdpData userData = new UserUdpData(udp);//----从UDP协议包中分离出用户协议数据-----
            //string temp1 = ConvertTools.ByteToHexStr(udp.ProtocolData);
            //string temp2 = ConvertTools.ByteToHexStr(userData.Data);
            byte[]temp = new byte[udp.ProtocolData.Length - 4];
            Buffer.BlockCopy(udp.ProtocolData, 0, temp, 0, temp.Length);
            byte[] result = CRC32.GetCheckValue(temp);
            if (CommonTools.BytesEuqals(userData.CrcCode, result))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 关闭Socket
        /// </summary>
        public void Close()
        {            
            receiveThread.Abort();
            mySocket.Close();
        }


        /// <summary>
        /// 测试IP是否有效
        /// </summary>
        /// <param name="ip">IP地址</param>
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
        /// 获取接收列表副本
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, UdpData> GetCopySendListData(byte[] cmd, byte networkID)
        {
            lock (rj45SendList)
            {
                Dictionary<string, UdpData> copyList = new Dictionary<string, UdpData>();
                foreach (string key in rj45SendList.Keys)
                {
                    UdpData udp = rj45SendList[key];
                    UserUdpData userData = new UserUdpData(udp);
                    if (CommonTools.BytesEuqals(userData.Command, cmd) && userData.Target[1] == networkID)
                        copyList.Add(key, udp);
                }
                return copyList;
            }
        }

    }
}
