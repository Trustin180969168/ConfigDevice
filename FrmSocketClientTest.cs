using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SocketTest
{
    public partial class FrmSocketClientTest : Form
    {
        private SysCtrl sysCtrl;
        MySocket test;
        private IPAddress ip,clientIP;
        private Socket mySocket;
        private int port,clientPort;
        private IPEndPoint ipLocalPoint;
        private EndPoint remotePoint;
        private bool RunningFlag = true;
        private string cmdSeachNetworkStr = "41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 " +
             "00 00 41 13 01 42 1C 25 01 00 00 00 FF FF F0 FF FF FE 11 01 FE 0C FF FF FF FF FF FF FF FF 8F 90 BE 84 D9";


        public FrmSocketClientTest()
        {
            InitializeComponent();
            sysCtrl = new SysCtrl();
        }


        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSocketClientTest_Load(object sender, EventArgs e)
        {

            //sysCtrl.Init();//初始化配置

           
            //ip = getValidIP("192.168.1.251"); //得到本机IP，设置UDP端口号  
            //port = getValidPort("9500");
            //ipLocalPoint = new IPEndPoint(ip, port);
            
            //mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//定义网络类型，数据连接类型和网络协议UDP  
            //mySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);            /
            //mySocket.Bind(ipLocalPoint);//绑定网络地址  


            //得到客户机IP  
            //clientIP = getValidIP("255.255.255.255");
            clientIP = getValidIP("192.168.1.236");

            clientPort = getValidPort("11211");
            IPEndPoint ipep = new IPEndPoint(clientIP, clientPort);
            remotePoint = (EndPoint)(ipep);


            //启动一个新的线程，执行方法this.ReceiveHandle，  
            //以便在一个独立的进程中执行数据接收的操作  
            //RunningFlag = true;
            //Thread thread = new Thread(new ThreadStart(this.ReceiveHandle));
            //thread.Start();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSend_Click(object sender, EventArgs e)
        {
            //byte[] data = ByteConvert.StrToToHexByte(cmdSeachNetworkStr);
            //mySocket.SendTo(data, data.Length, SocketFlags.None, remotePoint);

            ////得到客户机IP  
            //clientIP = getValidIP("255.255.255.255");
            ////clientIP = getValidIP("192.168.1.236");
            //clientPort = getValidPort("11211");
            //IPEndPoint ipep = new IPEndPoint(clientIP, clientPort);
            //remotePoint = (EndPoint)(ipep);

            byte[] data = ByteConvert.StrToToHexByte(cmdSeachNetworkStr);
            test= MySocket.GetInstance();
            rtbReceive.Text = test.SendData(data, "255.255.255.255", "11211",10);
     
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

       

        //定义一个委托  
        public delegate void MyInvoke(string strRecv);
        private void ReceiveHandle()
        {
            //接收数据处理线程  
            string msg;
           
            MyInvoke myI = new MyInvoke(UpdateMsgTextBox);
            while (RunningFlag)
            {
                try
                {
                    if (mySocket == null || mySocket.Available < 1)
                    {
                        Thread.Sleep(500);
                        continue;
                    }
                    byte[] data = new byte[128];
                    //跨线程调用控件  
                    //接收UDP数据报，引用参数remotePoint获得源地址  
                    int rlen = mySocket.ReceiveFrom(data, ref remotePoint);
                    if (rlen > 128) continue;//无效包数据
                    Array.Resize(ref data, rlen);
                    UdpData udpData = UdpTools.CreateUdpData(data);//UDP数据对象
                    //msg = ByteConvert.byteToHexStr(data);
                    msg = udpData.GetUdpInfo();
                    rtbReceive.BeginInvoke(myI, new object[] { remotePoint.ToString() + " : \n" + msg });
                }
                catch (Exception e)
                { 
                
                }
      
            }
        }


        private void UpdateMsgTextBox(string msg)
        {
            //接收数据显示  
            this.rtbReceive.AppendText(msg + "\n");

        }

        /// <summary>
        /// 窗体关闭前退出socket,执行线程
        /// </summary>
        private void FrmSocketClientTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            RunningFlag = false;
            test.Close();
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            rtbReceive.Text = "";
        }





    }
}
