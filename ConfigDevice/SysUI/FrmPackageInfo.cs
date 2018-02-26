using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ConfigDevice
{

    public enum ENUM_PackageSendKind
    {
        PC_SEND,RJ45_SEND
    }
   
    public partial class FrmPackageInfo : Form
    {

        private ENUM_PackageSendKind sendKind = ENUM_PackageSendKind.PC_SEND;
        private ThreadActionTimer getDataTimer;
        private DataTable dtPackage = new DataTable("Package");
        private MySocket mySocket = MySocket.GetInstance();

        public ENUM_PackageSendKind SendKind
        {
            get { return sendKind; }
            set
            {
                sendKind = value;
                if (value == ENUM_PackageSendKind.PC_SEND)
                    Text = "PC发送包列表";
                else
                    Text = "RJ45发送包列表";
            }
        }

        public FrmPackageInfo()
        {
            InitializeComponent();
            packageId.FieldName = UdpDataConfig.DC_BBSM;
            packageKindID.FieldName = UdpDataConfig.DC_BSJL;
            packageProperty.FieldName = UdpDataConfig.DC_BSX;
            packagePort.FieldName = UdpDataConfig.DC_JSDK;
            packageUserProtocol.FieldName = UdpDataConfig.DC_YHXY;
            packageUserData.FieldName = UdpDataConfig.DC_YHSJ;
            packageAdd.FieldName = UdpDataConfig.DC_JYH;
            initTable();

            gcPackage.DataSource = dtPackage;

        }

        private void FrmPackageInfo_Load(object sender, EventArgs e)
        {
            getData();
            getDataTimer = new ThreadActionTimer(5000, new Action(getData));
            getDataTimer.Start();
        }

        /// <summary>
        /// 初始化表
        /// </summary>
        private void initTable()
        {
            if (dtPackage.Columns.Count == 0)
            {
                dtPackage.Columns.Add(UdpDataConfig.DC_BBSM, System.Type.GetType("System.String"));
                dtPackage.Columns.Add(UdpDataConfig.DC_BSJL, System.Type.GetType("System.String"));
                dtPackage.Columns.Add(UdpDataConfig.DC_BSX, System.Type.GetType("System.String"));
                dtPackage.Columns.Add(UdpDataConfig.DC_JSDK, System.Type.GetType("System.String"));
                dtPackage.Columns.Add(UdpDataConfig.DC_YHXY, System.Type.GetType("System.String"));
                dtPackage.Columns.Add(UdpDataConfig.DC_YHSJ, System.Type.GetType("System.String"));
                dtPackage.Columns.Add(UdpDataConfig.DC_JYH, System.Type.GetType("System.String"));
            }

        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private void getData()
        {
            lock (dtPackage)
            {
                dtPackage.Clear();
                if (sendKind == ENUM_PackageSendKind.PC_SEND)
                {
                    CallbackFromUDP[] states = new CallbackFromUDP[mySocket.PCCallBackList.Count];
                    mySocket.PCCallBackList.Values.CopyTo(states, 0);
                    foreach (CallbackFromUDP state in states)
                    {
                        UdpData udp = state.Udp;
                        dtPackage.Rows.Add(new object[] { udp.PacketCodeStr,ConvertTools.ByteToHexStr(udp.PacketKind), 
                        ConvertTools.ByteToHexStr(udp.PacketProperty), ConvertTools.ByteToHexStr(udp.SendPort),
                        ConvertTools.ByteToHexStr(udp.Protocol),ConvertTools.ByteToHexStr(udp.ProtocolData),
                        ConvertTools.ByteToHexStr(udp.CheckCodeAdd)});
                    }
                }
                else
                {
                    UdpData[] udps = new UdpData[mySocket.RJ45SendList.Count];
                    mySocket.RJ45SendList.Values.CopyTo(udps, 0);
                    foreach (UdpData udp in udps)
                    {
                        dtPackage.Rows.Add(new object[] { udp.PacketCodeStr,ConvertTools.ByteToHexStr(udp.PacketKind), 
                        ConvertTools.ByteToHexStr(udp.PacketProperty),ConvertTools.ByteToHexStr(udp.SendPort),
                        ConvertTools.ByteToHexStr(udp.Protocol),ConvertTools.ByteToHexStr(udp.ProtocolData),
                        ConvertTools.ByteToHexStr(udp.CheckCodeAdd)});
                    }
                }
                dtPackage.AcceptChanges(); 
                this.Invoke(new Action(callbackUI));
            }

        }

        private void callbackUI()
        {
            gvPackage.BestFitColumns();
        }

        private void FrmPackageInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            getDataTimer.Stop();
            Thread.Sleep(500);
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            getDataTimer.Stop();
        }

        private void btAutoRelesh_Click(object sender, EventArgs e)
        {
            getDataTimer.Start();
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            getDataTimer.Stop();
            if (sendKind == ENUM_PackageSendKind.PC_SEND)
            {               
                mySocket.PCCallBackList.Clear();
                dtPackage.Rows.Clear();
            }
            else
            {
                mySocket.RJ45SendList.Clear();
                dtPackage.Rows.Clear();
            }
            getDataTimer.Start();
        }


    }
}
