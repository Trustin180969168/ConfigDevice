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
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice
{
    public partial class FrmMain : Form
    {
        private MySocket socket;//---通讯对象-----
        private NetworkCtrl networkCtrl;//----网络控制-----
        private DeviceCtrl deviceCtrl;//----设备控制-----
        private PleaseWait pw;//---等待窗体
        private bool OneNetworkShow = false;//是否单网段显示
        private Dictionary<string, Network> listSnycNetworks = new Dictionary<string, Network>();//-----同步网络ID列表--

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
            networkKindName.FieldName = NetworkConfig.DC_KINDNAME;

            xh.FieldName = DeviceConfig.DC_NUM;
            deviceID.FieldName = DeviceConfig.DC_ID;
            deviceNetworkID.FieldName = DeviceConfig.DC_NETWORK_ID;
            deviceKind.FieldName = DeviceConfig.DC_KIND_NAME;
            deviceName.FieldName = DeviceConfig.DC_NAME;
            deviceMac.FieldName = DeviceConfig.DC_MAC;
            deviceState.FieldName = DeviceConfig.DC_STATE;
            deviceRemark.FieldName = DeviceConfig.DC_REMARK;
            image1.FieldName = DeviceConfig.DC_IMAGE1;
            
            
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
            deviceKind.FilterInfo = new ColumnFilterInfo(DeviceConfig.DC_KIND_ID + " != '" + (int)DeviceConfig.EQUIPMENT_RJ45 + "' and " +
                DeviceConfig.DC_KIND_ID + " != '" + (int)DeviceConfig.EQUIPMENT_SERVER + "'");
        }

        /// <summary>
        /// 搜索网络
        /// </summary>
        private void btNetworkSearch_Click(object sender, EventArgs e)
        {
            pw = new PleaseWait(0);//---时间不可控,显示1秒---
            pw.Show(this);
            Action searchAction = new Action(networkCtrl.SearchNetworks);
            searchAction.BeginInvoke(null, null);
        }
        public void CallBackUI(object[] values)
        {

            try
            {
                if (this.InvokeRequired)
                { this.Invoke(new CallbackUIAction(CallBackUI), new object[] { values }); }
                else
                {
                    if (values == null)
                    {
                        gvNetwork.BestFitColumns();
                    }
                    else if ((ActionKind)values[0] == ActionKind.SearchDevice)//-----搜索完设备----
                    {
                        Network network = (Network)(values[1]);
                        SyncNextNetworkID();//---执行一下同步
                        gvDevices.BestFitColumns();

                    }
                    else if ((ActionKind)values[0] == ActionKind.SyncNetworkID)//----同步网络ID完毕刷新---
                    {
                        Network network = (Network)(values[1]);
                        listSnycNetworks.Remove(network.MacAddress);//--移除需要同步ID的网络---     
                        deviceCtrl.SearchDevices(network);      //----同步后重新搜索----                  
                    }
                    else if ((ActionKind)values[0] == ActionKind.ConnectNetowrk)//---连接完网络----
                    { gvNetwork.PostEditor(); gvNetwork.RefreshData(); btSearchDevices_Click(null, null); }
                }
            }
            catch (Exception e1) { e1.ToString(); }
        }

        /// <summary>
        /// 同步下一个网络ID
        /// </summary>
        private void SyncNextNetworkID()
        {
            //----执行下一个同步网络ID----
            if (listSnycNetworks.Count > 0)
            {
                foreach (Network network1 in listSnycNetworks.Values)
                {
                    network1.SnycNetworkID();
                    break;//---执行一条退出,剩下的回调执行.
                }
            }
        }


        /// <summary>
        /// 窗体关闭前退出socket
        /// </summary>
        private void FrmSocketClientTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //------断开所有连接网络-------
            foreach (Network network in SysConfig.ListNetworks.Values)
                if (network.State == NetworkConfig.STATE_CONNECTED) network.DisconnectNetwork();
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
            Network network = SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()];
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
            Network network = SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()];
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
            Device device = new BaseDevice(dr);
            if (SysConfig.ListNetworks.ContainsKey(device.NetworkIP) &&
                SysConfig.ListNetworks[device.NetworkIP].State == NetworkConfig.STATE_CONNECTED)
            {
                FrmDevice frm = SysCtrl.GetFactoryDeviceEdit(device.ByteKindID).CreateDevice(dr);
                frm.Text = device.Name;
                frm.Show(this);
            }
            else { CommonTools.MessageShow("网络链接已断开,请重新链接!", 2, ""); return; }
        }


        /// <summary>
        /// 双击链接或打开网络
        /// </summary>
        private void gvNetwork_LinkEdit(object sender, EventArgs e)
        {
            if (gvNetwork.FocusedRowHandle == -1) return;
            DataRow dr = gvNetwork.GetDataRow(gvNetwork.FocusedRowHandle);

            Network network = SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()];
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
            Network network = SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()];
            FrmChangePassword frm = new FrmChangePassword();
            frm.NetworkEdit = network;
            frm.Show();
        }

        /// <summary>
        /// 变更IP地址
        /// </summary>
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
            if (cbxSelectNetwork.Items.Count == 0) initCbxSelectNetwork();
            cbxSelectNetwork.SelectedIndex = 0;
        }

        /// <summary>
        /// 通过ID匹配选择列表
        /// </summary>
        Dictionary<string, string> listNetworkNameID = new Dictionary<string, string>();
        private void cbxSelectNetwork_Click(object sender, EventArgs e)//初始化选择表
        {
            initCbxSelectNetwork();
        }

        /// <summary>
        /// 初始化网络选择
        /// </summary>
        private void initCbxSelectNetwork()
        {
            string oldSelect = cbxSelectNetwork.Text;
            listNetworkNameID.Clear(); cbxSelectNetwork.Items.Clear();
            cbxSelectNetwork.Items.Add("");
            //-----网络列表------------------
            foreach (Network network in SysConfig.ListNetworks.Values)
            {
                cbxSelectNetwork.Items.Add(network.DeviceName);
                listNetworkNameID.Add(network.DeviceName, network.NetworkID);
            }
            cbxSelectNetwork.SelectedIndexChanged -= cbxSelectNetwork_SelectedIndexChanged;
            cbxSelectNetwork.Text = oldSelect;
            cbxSelectNetwork.SelectedIndexChanged += cbxSelectNetwork_SelectedIndexChanged;
        }

        /// <summary>
        /// 筛选网络设备
        /// </summary>
        private void cbxSelectNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSelectNetwork.Text == "")
                deviceNetworkID.ClearFilter();
            else
            {
                string networkID = listNetworkNameID[cbxSelectNetwork.Text];
                deviceNetworkID.FilterInfo = new ColumnFilterInfo(DeviceConfig.DC_NETWORK_ID + "= '" + networkID + "' ");
                //deviceNetworkID.FilterInfo = new ColumnFilterInfo(DeviceConfig.DC_NETWORK_ID + "= '" + networkID + "' and " +
                //    DeviceConfig.DC_KIND_ID + " != '" + (int)DeviceConfig.EQUIPMENT_RJ45 + "' and " +
                //DeviceConfig.DC_KIND_ID + " != '" + (int)DeviceConfig.EQUIPMENT_SERVER + "'");
            }
        }

        /// <summary>
        /// 显示下拉
        /// </summary>
        private void btGJ_MouseHover(object sender, EventArgs e)
        {
            btGJ.ShowDropDown();
        } 

        /// <summary>
        /// 同步网络ID
        /// </summary>
        private void btSyncID_Click(object sender, EventArgs e)
        {
            if (gvNetwork.FocusedRowHandle == -1) return;
            DataRow dr = gvNetwork.GetDataRow(gvNetwork.FocusedRowHandle);
            if (dr[NetworkConfig.DC_STATE].ToString() == NetworkConfig.STATE_NOT_CONNECTED)
            { CommonTools.MessageShow("你还未链接" + dr[NetworkConfig.DC_DEVICE_NAME].ToString() + "!", 2, ""); return; }
            Network network = SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()];
            network.SnycNetworkID();
            //listSnycNetworks.Clear();//----清空同步网络列表----
            //foreach (Network network in SysConfig.ListNetworks.Values)
            //{
            //    if(network.State == NetworkConfig.STATE_CONNECTED)
            //        listSnycNetworks.Add(network.MacAddress, network);
            //}
            //SyncNextNetworkID();
        }

        /// <summary>
        /// 同步时间
        /// </summary>
        private void btSyncTime_Click(object sender, EventArgs e)
        {
            if (gvNetwork.FocusedRowHandle == -1) return;
            DataRow dr = gvNetwork.GetDataRow(gvNetwork.FocusedRowHandle);
            if (dr[NetworkConfig.DC_STATE].ToString() == NetworkConfig.STATE_NOT_CONNECTED)
            { CommonTools.MessageShow("你还未链接" + dr[NetworkConfig.DC_DEVICE_NAME].ToString() + "!", 2, ""); return; }
            Network network = SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()];
            network.SnycTime();
        }

        /// <summary>
        /// 同步数据
        /// </summary>
        private void btSyncData_Click(object sender, EventArgs e)
        {
            if (gvNetwork.FocusedRowHandle == -1) return;
            DataRow dr = gvNetwork.GetDataRow(gvNetwork.FocusedRowHandle);
            if (dr[NetworkConfig.DC_STATE].ToString() == NetworkConfig.STATE_NOT_CONNECTED)
            { CommonTools.MessageShow("你还未链接" + dr[NetworkConfig.DC_DEVICE_NAME].ToString() + "!", 2, ""); return; }
            Network network = SysConfig.ListNetworks[dr[NetworkConfig.DC_IP].ToString()];
            network.SnycData();
        }

        /// <summary>
        /// 显示IP选择
        /// </summary>
        private void btIPSelect_Click(object sender, EventArgs e)
        {
            toolStripButton1.Visible = true;
            lblIPSelect.Visible = true;
            cbxIPList.Visible = true;
        }

        /// <summary>
        /// 单网段显示
        /// </summary>
        private void btQry_Click(object sender, EventArgs e)
        {
            OneNetworkShow = !OneNetworkShow;
            if (OneNetworkShow)
            {
                btQry.CheckState = CheckState.Checked;
                btQry.ForeColor = Color.Red;
                btQry.Text = "单网段显示";
                //btQry.BackColor = Color.White; 
            }
            else
            {
                btQry.CheckState = CheckState.Unchecked;
                btQry.ForeColor = Color.Black;
                //btQry.BackColor = toolStrip2.BackColor; 
            }
            gvNetwork_Click(sender, e);
        }

        /// <summary>
        /// 选择网络
        /// </summary>
        private void gvNetwork_Click(object sender, EventArgs e)
        {
            if (gvNetwork.FocusedRowHandle == -1) return;
            DataRow dr = gvNetwork.GetDataRow(gvNetwork.FocusedRowHandle);
            if (!OneNetworkShow)
                deviceNetworkID.ClearFilter();
            else
            {
                string networkID = dr[deviceNetworkID.FieldName].ToString();
                deviceNetworkID.FilterInfo = new ColumnFilterInfo(DeviceConfig.DC_NETWORK_ID + "= '" + networkID + "' ");
            }

        }

        /// <summary>
        /// 连接后改变颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvNetwork_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string state = gvNetwork.GetRowCellDisplayText(e.RowHandle, networkState);
                if (state == NetworkConfig.STATE_CONNECTED)
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.BackColor = Color.LemonChiffon;

                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.BackColor = Color.White;

                }
            }
        }

        /// <summary>
        /// 网络选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvNetwork_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            gvNetwork_Click(sender, e);
        }
        private void gvNetwork_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            gvNetwork_Click(sender, e);
        }

        
        /// <summary>
        /// 显示发现设备
        /// </summary>
        private void gvDevices_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

            //if (e.RowHandle >= 0)
            //{
            //    //DataRow dr = gvDevices.GetDataRow(e.RowHandle);
            //    //if (dr[DeviceConfig.DC_PARAMETER1].ToString() == "" || dr[DeviceConfig.DC_PARAMETER1].ToString() == "0")
            //    //    this.linkSwit.Image = global::ConfigDevice.Properties.Resources.on;
            //    //else
            //    //    this.linkSwit.Image = global::ConfigDevice.Properties.Resources.off;
            //    //if (value == 1)
            //    //    e.Appearance.BackColor = Color.LightBlue;
            //    //else
            //    //    e.Appearance.BackColor = Color.White;
            //    gvDevices.SetRowCellValue(e.RowHandle,image1,imageCollectionDevices.Images[0]);
            //}
        }



        private void pictureEdit_Click(object sender, EventArgs e)
        {
            if (gvDevices.FocusedRowHandle == -1) return;
            DataRow dr = gvDevices.GetDataRow(gvDevices.FocusedRowHandle);
            if (dr[DeviceConfig.DC_PARAMETER1].ToString() == DeviceConfig.STATE_OPEN_LIGHT)
            {
                Device device = new BaseDevice(dr);
                device.OpenLight();
                dr[DeviceConfig.DC_IMAGE1] = ImageHelper.ImageToBytes(global::ConfigDevice.Properties.Resources.off);
                dr[DeviceConfig.DC_PARAMETER1] = DeviceConfig.STATE_CLOSE_LIGHT;

            }
            else if (dr[DeviceConfig.DC_PARAMETER1].ToString() == DeviceConfig.STATE_CLOSE_LIGHT)
            {
                Device device = new BaseDevice(dr);
                device.CloseLight();
                dr[DeviceConfig.DC_IMAGE1] = ImageHelper.ImageToBytes(global::ConfigDevice.Properties.Resources.on);
                dr[DeviceConfig.DC_PARAMETER1] =DeviceConfig.STATE_OPEN_LIGHT;
            }
            gvDevices.PostEditor();
            gvDevices.RefreshData();
        }




    }
}
