using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmNetworkEdit : Form
    {
        public NetworkData NetworkEdit =null;
        DataTable dtPosition = new DataTable("Position");

        public FrmNetworkEdit()
        {
            InitializeComponent();
        }

        private void FrmNetworkEdit_Load(object sender, EventArgs e)
        { 
            initData();
        }

        /// <summary>
        /// 初始化位置列表
        /// </summary>
        private void initData()
        {
            num.FieldName = Position.DC_NUM;
            password.FieldName = Position.DC_HAS_PASSWORD;
            name.FieldName = Position.DC_NAME;
            gcPosition.DataSource = dtPosition;
            dtPosition.Columns.Add(Position.DC_NUM, System.Type.GetType("System.Int16"));
            dtPosition.Columns.Add(Position.DC_NAME, System.Type.GetType("System.String"));
            dtPosition.Columns.Add(Position.DC_HAS_PASSWORD, System.Type.GetType("System.Boolean"));
            
            foreach (Position position in NetworkEdit.ListPosition)
                dtPosition.Rows.Add(new object[] { position.Num, position.Name, position.HasPassword });

            dtPosition.AcceptChanges();
            gvPosition.BestFitColumns();
            gvPosition.RefreshData();

            edtNetworkName.Text = NetworkEdit.DeviceName;
            edtNetworkID.Text = NetworkEdit.DeviceID;
            edtNetworkIP.IP = NetworkEdit.NetworkIP;
        }

        /// <summary>
        /// 校验是否错误
        /// </summary>
        private void edtName_KeyUp(object sender, KeyEventArgs e)
        {
            DataRow dr = gvPosition.GetDataRow(gvPosition.FocusedRowHandle);
            if(dr == null) return;
            string name = dr[Position.DC_NAME].ToString();
            byte[] byteName = Encoding.GetEncoding("GB2312").GetBytes(name);
            if (byteName.Length > 12)
                CommonTools.MessageShow("名称超出长度,最多6个字!", 2, "");
        }

    }
}
