using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace ConfigDevice
{
    public partial class FrmNetworkEdit : Form
    {
        public Network NetworkEdit = null;
        DataTable dtPosition = new DataTable("Position");
        private Dictionary<int, string> listNetworkKey = new Dictionary<int, string>();//保存对应关系

        public FrmNetworkEdit()
        {
            InitializeComponent();

            num.FieldName = Position.DC_NUM;
            password.FieldName = Position.DC_HAS_PASSWORD;
            name.FieldName = Position.DC_NAME;
            gcPosition.DataSource = dtPosition;
            dtPosition.Columns.Add(Position.DC_NUM, System.Type.GetType("System.Int16"));
            dtPosition.Columns.Add(Position.DC_NAME, System.Type.GetType("System.String"));
            dtPosition.Columns.Add(Position.DC_HAS_PASSWORD, System.Type.GetType("System.Boolean"));
        }

        private void FrmNetworkEdit_Load(object sender, EventArgs e)
        {
            NetworkEdit.CallbackUI += this.callbackUI;
            initCbxNetwork();
            initData();          
        }

        /// <summary>
        /// 初始化位置列表
        /// </summary>
        private void initData()
        {
            dtPosition.Clear(); dtPosition.AcceptChanges();
            foreach (Position position in NetworkEdit.ListPosition)
                dtPosition.Rows.Add(new object[] { position.Num, position.Name, position.HasPassword });

            dtPosition.AcceptChanges();
            gvPosition.BestFitColumns();
            gvPosition.RefreshData();

            edtNetworkName.Text = NetworkEdit.DeviceName;
            edtNetworkID.Text = NetworkEdit.NetworkID;
            edtNetworkIP.IP = NetworkEdit.NetworkIP;
            edtMask.IP = SysConfig.SubnetMask.ToString();
            edtGateway.IP = SysConfig.DefaultIPGateway.ToString();

            if (NetworkEdit.State == NetworkConfig.STATE_CONNECTED)
                NetworkEdit.SearchVer();
            else
            { edtSoftwareVer.Text = ""; edtHarewareVer.Text = ""; }
                
        }

        private void initCbxNetwork()
        {
            //-----网络列表------------------
            int i = 0;
            foreach (Network network in SysConfig.ListNetworks.Values)
            {
                cbxNetwork.Items.Add(network.DeviceName);
                listNetworkKey.Add(i++, network.NetworkIP);
            }
            cbxNetwork.Text = NetworkEdit.DeviceName;
        }

        /// <summary>
        /// 保存位置
        /// </summary>
        private void btSavePosition_Click(object sender, EventArgs e)
        {
            gvPosition.PostEditor();
            DataRow drEdit = gvPosition.GetDataRow(gvPosition.FocusedRowHandle);
            drEdit.EndEdit();
            DataTable dtModify = dtPosition.GetChanges(DataRowState.Modified);
            if (dtModify == null) return;
            foreach (DataRow dr in dtModify.Rows)
            {
                Position pos = new Position(Convert.ToInt16(dr[Position.DC_NUM]), dr[Position.DC_NAME].ToString(), Convert.ToBoolean(dr[Position.DC_HAS_PASSWORD]));
                NetworkEdit.SavePositionList(pos, this.callBackSavePosition);
            }
        }

        /// <summary>
        /// 回调结果
        /// </summary>
        /// <param name="pos">保存位置</param>
        private void callBackSavePosition(object[] values)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(this.callBackSavePosition), new object[] { values });
                return;
            }
            Position pos = values[0] as Position;
            dtPosition.Rows[pos.Num - 1].AcceptChanges();

        }

        private void callbackUI(object[] values)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(this.callbackUI), new object[] { values });
                return;
            }
            edtSoftwareVer.Text = NetworkEdit.SoftwareVer;
            edtHarewareVer.Text = NetworkEdit.HardwareVer;
        }

        /// <summary>
        /// 校验是否错误
        /// </summary>
        private void edtName_Leave(object sender, EventArgs e)
        {
            DataRow dr = gvPosition.GetDataRow(gvPosition.FocusedRowHandle);
            if (dr == null) return;
            string name = dr[Position.DC_NAME].ToString();
            byte[] byteName = Encoding.GetEncoding("GB2312").GetBytes(name);
            if (byteName.Length > 12)
                CommonTools.MessageShow("\"" + name + "\"名称超出长度,最多6个字!", 2, "");
        }

        /// <summary>
        /// 校验网络名称
        /// </summary>
        private void edtNetworkName_Leave(object sender, EventArgs e)
        {
            string name = edtNetworkName.Text;
            byte[] byteName = Encoding.GetEncoding("GB2312").GetBytes(name);
            if (byteName.Length > 30)
                CommonTools.MessageShow("\"" + name + "\"名称超出长度,最多30个字!", 2, "");
        }

        /// <summary>
        /// 保存基本信息
        /// </summary>
        private void btSaveInfo_Click(object sender, EventArgs e)
        {
            NetworkEdit.SaveNetworkName(edtNetworkName.Text);
            NetworkEdit.SaveNetworkParameter(edtNetworkIP.ByteIP, edtGateway.ByteIP, edtMask.ByteIP,
                ConvertTools.GetByteFrom8BitNumStr(edtNetworkID.Text));
        }

        /// <summary>
        /// 保存名称
        /// </summary>
        private void btSaveName_Click(object sender, EventArgs e)
        {
            NetworkEdit.SaveNetworkName(edtNetworkName.Text);
        }

        private void btSavePagameter_Click(object sender, EventArgs e)
        {
            NetworkEdit.SaveNetworkParameter(edtNetworkIP.ByteIP, edtGateway.ByteIP, edtMask.ByteIP,
                         ConvertTools.GetByteFrom8BitNumStr(edtNetworkID.Text));
        }

        private void btSaveInfo_MouseHover(object sender, EventArgs e)
        {
            btSaveInfo.ShowDropDown();
        }

        private void cbxNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cbxNetwork.SelectedIndex;
            string key = listNetworkKey[i];
            NetworkEdit = SysConfig.ListNetworks[key];

            initData();
        }



    }
}
