using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Timers;

namespace ConfigDevice
{
    public partial class FrmTestSocket : Form
    {
        private IPEndPoint ipLocalPoint;
        private EndPoint RemotePoint;
        private Socket mySocket;
        private bool RunningFlag = false;
        private Thread thread;
        private System.Timers.Timer timer;
        private int seconds = 0;
        private TestObj testObj;

        public class TestObj
        {
            int i = 0;
            public TestObj(int _i) { i = _i; }

            public void ShowI()
            {
                CommonTools.MessageShow(i.ToString(), 1, "");
            }
        }

        public Dictionary<string, TestObj> listTestObj = new Dictionary<string, TestObj>();

        public FrmTestSocket()
        {
            InitializeComponent();
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += this.TimerCount_Elaspsed;
            timer.Enabled = false;            
        }

        public void TimerCount_Elaspsed(object sender, ElapsedEventArgs e)
        {
            seconds++;
        }
        private void UpdateMsgTextBox(string msg)
        {
            rtbReceive.Text +=msg + "\n";
        }

        private void init()
        {
            ipLocalPoint = new IPEndPoint(SysConfig.LocalIP, SysConfig.LocalPort+1);

            //定义网络类型，数据连接类型和网络协议UDP  
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            mySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            //绑定网络地址  
            mySocket.Bind(ipLocalPoint);

            //得到客户机IP  
            IPAddress ip = IPAddress.Parse("255.255.255.255");           
            IPEndPoint ipep = new IPEndPoint(ip, SysConfig.RemotePort);
            RemotePoint = (EndPoint)(ipep);

            //启动一个新的线程，执行方法this.ReceiveHandle，  
            //以便在一个独立的进程中执行数据接收的操作  
            RunningFlag = true;
            thread = new Thread(new ThreadStart(this.ReceiveHandle));
            thread.Start(); 
        }

        //定义一个委托 
        public delegate void MyInvoke(string strRecv);
        private void ReceiveHandle()
        {
            //接收数据处理线程  
            byte[] data = new byte[128];
            MyInvoke myI = new MyInvoke(UpdateMsgTextBox);
            while (RunningFlag)
            {

                if (mySocket == null || mySocket.Available < 1)
                {
                    Thread.Sleep(200);
                    continue;
                }
                //跨线程调用控件  
                //接收UDP数据报，引用参数RemotePoint获得源地址  
                int rlen = mySocket.ReceiveFrom(data, ref RemotePoint); 
               
                UdpData udp = new UdpData(data);
                IPAddress ip = IPAddress.Parse(RemotePoint.ToString().Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                int port = Int16.Parse(RemotePoint.ToString().Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1]);
                udp.IPPoint = new IPEndPoint(ip, port);
                
                
                byte[] data2 = new byte[128];UdpData udp2 = new UdpData();
                if (MySocketTest.GetInstance().Available > 1)
                { 
                     udp2 = MySocketTest.GetInstance().ReceiveData(RemotePoint); 
                }
                this.rtbReceive.BeginInvoke(myI, new object[] { RemotePoint.ToString() + " : " + udp.GetUdpInfo() 
                +"\n ******upd2*******\n"+udp2.GetUdpInfo()          });

            }
        }  

        private void FrmTestSocket_Load(object sender, EventArgs e)
        {
            init();
            int i =0;
            listTestObj.Add(i++.ToString(), new TestObj(i));
            listTestObj.Add(i++.ToString(), new TestObj(i));
            listTestObj.Add(i++.ToString(), new TestObj(i));
            listTestObj.Add(i++.ToString(), new TestObj(i));
            listTestObj.Add(i++.ToString(), new TestObj(i));
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            rtbReceive.Text = "";
            byte[] cmdSearch = ConvertTools.StrToToHexByte(" 41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 00 00 41 13 01 42 1D 25 01 00 00 00 FF FF F0 FF FF FE 11 01 FE 0C FF FF FF FF FF FF FF FF 8F 90 BE 84 D9");
            //List<UdpData> listData = MySocketTest.GetInstance().GetData(cmdSearch, "255.255.255.255", SysConfig.RemotePort, 20);
         
            mySocket.SendTo(cmdSearch, cmdSearch.Length, SocketFlags.None, RemotePoint);
            //得到客户机IP  
            IPAddress ip = IPAddress.Parse("255.255.255.255");
            IPEndPoint ipep = new IPEndPoint(ip, SysConfig.RemotePort);
            RemotePoint = (EndPoint)(ipep);
        }

        private void FrmTestSocket_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
           // MySocketTest.GetInstance().Close();
            mySocket.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            testObj = listTestObj["3"];
            listTestObj.Clear();
            testObj.ShowI();
        }









    }
}
