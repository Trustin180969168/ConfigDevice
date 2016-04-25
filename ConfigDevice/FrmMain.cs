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
using System.Timers;
using System.IO;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;

namespace ConfigDevice
{
    public partial class FrmMain : Form
    {
        private MySocket socket;//---通讯对象-----
        private NetworkCtrl networkCtrl;//----网络控制-----
        private DeviceCtrl deviceCtrl;//----设备控制-----
        private PleaseWait pw;//---等待窗体

        public FrmMain()
        {
            SysCtrl.Init();//初始化配置
            networkCtrl = new NetworkCtrl();
            deviceCtrl = new DeviceCtrl();
            socket = MySocket.GetInstance();

            InitializeComponent();

            gcNetwork.DataSource = SysConfig.DtNetwork;
            gcDevices.DataSource = SysConfig.DtDevice;
            networkCtrl.CallBackUI += this.CallBackUI;
            deviceCtrl.CallBackUI += this.CallBackUI;

            //-------初始化列表字段名-------
            networkDeviceID.FieldName = NetworkConfig.DC_DEVICE_ID;
            networkDeviceName.FieldName = NetworkConfig.DC_DEVICE_NAME;
            networkID.FieldName = NetworkConfig.DC_NETWORK_ID;
            networkDeviceMac.FieldName = NetworkConfig.DC_MAC;
            networkState.FieldName = NetworkConfig.DC_STATE;
            networkRemark.FieldName = NetworkConfig.DC_REMARK;

            xh.FieldName = DeviceConfig.DC_DEVICE_NUM;
            deviceID.FieldName = DeviceConfig.DC_DEVICE_ID;
            deviceNetworkID.FieldName = DeviceConfig.DC_NETWORK_ID;
            deviceKind.FieldName = DeviceConfig.DC_KIND_NAME;
            deviceName.FieldName = DeviceConfig.DC_NAME;
            deviceMac.FieldName = DeviceConfig.DC_MAC;
            deviceState.FieldName = DeviceConfig.DC_STATE;
            deviceRemark.FieldName = DeviceConfig.DC_REMARK;

        }

        /// <summary>
        /// 加载
        /// </summary>
        private void FrmSocketClientTest_Load(object sender, EventArgs e)
        {    
            //-------设置本地IP信息---------
            foreach (IPInfo ipInfo in SysConfig.IPList.Values)
                cbxIPList.Items.Add(ipInfo.IP);
            if (cbxIPList.Items.Count > 0)
                cbxIPList.SelectedIndex = 0;

            btNetworkSearch_Click(sender, e);
        }

        /// <summary>
        /// 搜索网络
        /// </summary>
        private void btNetworkSearch_Click(object sender, EventArgs e)
        {
            pw = new PleaseWait(1);//---时间不可控,显示2秒---
            pw.Show(this);
            Action searchAction = new Action(networkCtrl.SearchNetworks);
            searchAction.BeginInvoke(null, null);                  
        }
        public void CallBackUI(object[] values)
        {
            try
            {
                if (this.InvokeRequired)
                { this.Invoke(new CallbackUIAction(CallBackUI), new object[]{values} ); }
                else
                {
                    gvNetwork.BestFitColumns();
                    gvDevices.BestFitColumns();
                    if (cbxSelectNetwork.Items.Count <= 1) 
                        initCbxSelectNetwork();
                }
            }
            catch (Exception e1) { e1.ToString(); }
        }
        

        /// <summary>
        /// 窗体关闭前退出socket
        /// </summary>
        private void FrmSocketClientTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //------断开所有连接网络-------
            foreach (NetworkData network in SysConfig.ListNetworks.Values)
                if(network.State == NetworkConfig.STATE_CONNECTED) network.DisconnectNetwork();
            socket.Close();
        }

        /// <summary>
        /// 连接网络
        /// </summary>
        private void btConnectnetwork_Click(object sender, EventArgs e)
        {
            if (gvNetwork.FocusedRowHandle == -1) return;
            DataRow dr = gvNetwork.GetDataRow(gvNetwork.FocusedRowHandle);
            if (dr[NetworkConfig.DC_STATE].ToString() == NetworkConfig.STATE_CONNECTED)
            { CommonTools.MessageShow("你已经连接了" + dr[NetworkConfig.DC_DEVICE_NAME].ToString() + "!", 2, ""); return; }
            NetworkData network = SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()];
            network.ConnectNetwork();
        }

        /// <summary>
        /// 断开网络
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDisconnectNetwork_Click(object sender, EventArgs e)
        {
            if (gvNetwork.FocusedRowHandle == -1) return;
            DataRow dr = gvNetwork.GetDataRow(gvNetwork.FocusedRowHandle);
            if (dr[NetworkConfig.DC_STATE].ToString() == NetworkConfig.STATE_NOT_CONNECTED)
            { CommonTools.MessageShow("你还未链接" + dr[NetworkConfig.DC_DEVICE_NAME].ToString() + "!", 2, ""); return; }
            NetworkData network = SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()];
            network.DisconnectNetwork();
        }

        /// <summary>
        /// 搜索设备
        /// </summary>
        private void btSearchDevices_Click(object sender, EventArgs e)
        {
            if (gvNetwork.FocusedRowHandle == -1) return;
            DataRow dr = gvNetwork.GetDataRow(gvNetwork.FocusedRowHandle);
            if (dr[NetworkConfig.DC_STATE].ToString() == NetworkConfig.STATE_NOT_CONNECTED)
            {
                CommonTools.MessageShow("你还未链接" + dr[NetworkConfig.DC_DEVICE_NAME].ToString() + "!", 2, "");
                return;
            }   
            deviceCtrl.SearchDevices(SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()]);
        }

        public void closePw(IAsyncResult asyncResult)
        {
            pw.CloseWaiting();
        }

        /// <summary>
        /// 打开RJ45发送表
        /// </summary>
        private void btRJ45Send_Click(object sender, EventArgs e)
        {
            FrmPackageInfo frm = new FrmPackageInfo();
            frm.SendKind = ENUM_PackageSendKind.RJ45_SEND;
            frm.Show();
        }

        /// <summary>
        /// 打开回调表
        /// </summary> 
        private void btPCSend_Click(object sender, EventArgs e)
        {
            FrmPackageInfo frm = new FrmPackageInfo();
            frm.SendKind = ENUM_PackageSendKind.PC_SEND;
            frm.Show();
        }

        private void btXtxx_MouseHover(object sender, EventArgs e)
        {
            btXtxx.ShowDropDown();
        }
        
        /// <summary>
        /// 打开设备
        /// </summary>
        private void gvDevices_DoubleClick(object sender, EventArgs e)
        {
            if (gvDevices.FocusedRowHandle == -1) return;
            DataRow dr = gvDevices.GetDataRow(gvDevices.FocusedRowHandle);
            DeviceData device = new BaseDevice(dr);
            if (SysConfig.ListNetworks.ContainsKey(device.NetworkIP) &&
                SysConfig.ListNetworks[device.NetworkIP].State == NetworkConfig.STATE_CONNECTED)
            {
                FrmDevice frm = SysCtrl.GetFactory(device.ByteKindID).CreateDevice(dr);
                frm.Text = device.Name;
                frm.Show(this);
            }
            else { CommonTools.MessageShow("网络链接已断开,请重新链接!", 2, ""); return; }
        }


        /// <summary>
        /// 双击链接或打开网络
        /// </summary>
        private void gvNetwork_DoubleClick(object sender, EventArgs e)
        {
            if (gvNetwork.FocusedRowHandle == -1) return;
            DataRow dr = gvNetwork.GetDataRow(gvNetwork.FocusedRowHandle);

            NetworkData network = SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()];
            if (network.State == NetworkConfig.STATE_CONNECTED)
            {
                FrmNetworkEdit frm = new FrmNetworkEdit();
                frm.NetworkEdit = network;
                frm.Show(this);
            }
            else
                network.ConnectNetwork();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            if (gvNetwork.FocusedRowHandle == -1) return;
            DataRow dr = gvNetwork.GetDataRow(gvNetwork.FocusedRowHandle);
            NetworkData network = SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()];
            FrmChangePassword frm = new FrmChangePassword();
            frm.NetworkEdit = network;
            frm.Show();
        }

        private void cbxIPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SysConfig.SetLocalIPInfo(cbxIPList.SelectedIndex);
        }

        /// <summary>
        /// 清空网络
        /// </summary>
        private void btClearNetwork_Click(object sender, EventArgs e)
        {
            this.networkCtrl.ClearNetwork();
            cbxSelectNetwork.SelectedIndex = 0;
        }

        /// <summary>
        /// 清空设备
        /// </summary>
        private void btClearDevice_Click(object sender, EventArgs e)
        {
            deviceCtrl.ClearDevices();
            cbxSelectNetwork.SelectedIndex = 0;
        }

        Dictionary<string, string> listNetworkNameID = new Dictionary<string, string>();
        private void cbxSelectNetwork_Click(object sender, EventArgs e)
        {
            initCbxSelectNetwork();         
        }

        private void initCbxSelectNetwork()
        {
            string oldSelect = cbxSelectNetwork.Text;
      
            listNetworkNameID.Clear(); cbxSelectNetwork.Items.Clear();

            cbxSelectNetwork.Items.Add("");
            //-----网络列表------------------
            foreach (NetworkData network in SysConfig.ListNetworks.Values)
            {
                cbxSelectNetwork.Items.Add(network.DeviceName);
                listNetworkNameID.Add(network.DeviceName, network.NetworkID);
            }
            cbxSelectNetwork.SelectedIndexChanged -= cbxSelectNetwork_SelectedIndexChanged;
            cbxSelectNetwork.Text = oldSelect;
            cbxSelectNetwork.SelectedIndexChanged += cbxSelectNetwork_SelectedIndexChanged;
        }

        private void cbxSelectNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSelectNetwork.Text == "")
                deviceNetworkID.ClearFilter();
            else
            {
                string networkID = listNetworkNameID[cbxSelectNetwork.Text];
                deviceNetworkID.FilterInfo = new ColumnFilterInfo(DeviceConfig.DC_NETWORK_ID + "= '" + networkID + "'");
                //        gvDevices.Columns["deviceNetworkID"].FilterInfo = new ColumnFilterInfo("["+DeviceConfig.DC_NETWORK_ID+"] = '" + networkID + "'");
            }
        }




    }
}
