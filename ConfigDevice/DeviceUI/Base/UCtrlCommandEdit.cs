using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ConfigDevice.DeviceUI
{
    public partial class UCtrlCommandEdit : UserControl
    {
        public string CommandGroupName { set { this.lblGroupName.Text = value; } }
        public ComboBoxEdit CbxCommandGroup { get { return cbxGroup; } }
        private bool syncEdit = false;
        private int commandCount = 5;
        public UCtrlCommandEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择命令组
        /// </summary>
        private void cbxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            RequestCommandData();
        }
        private void edtBeginNum_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                RequestCommandData();
        }
        /// <summary>
        /// 申请获取指令数据
        /// </summary>
        public void RequestCommandData()
        {

        }
        /// <summary>
        /// 获取指令数据
        /// </summary>
        /// <param name="udpResult"></param>
        /// <param name="values"></param>
        private void returnCommandData(UdpData udpResult, object[] values)
        {

        }

        /// <summary>
        /// 同步编辑
        /// </summary>
        private void btSyncEdit_Click(object sender, EventArgs e)
        {
            if (!syncEdit)
            {
                syncEdit = !syncEdit;
                btSyncEdit.Image = global::ConfigDevice.Properties.Resources.check;
            }
            else
            {
                syncEdit = !syncEdit;
                btSyncEdit.Image = global::ConfigDevice.Properties.Resources.uncheck;

            }
            foreach (Control view in xscCommands.Controls)
                (view as ViewCommandTools).Checked = syncEdit;
        }

        /// <summary>
        /// 测试添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ViewCommandTools view = new ViewCommandTools(++commandCount);
            xscCommands.Controls.Add(view);
            view.Dock = DockStyle.Top;
            int i = commandCount;
            foreach (Control view1 in xscCommands.Controls)
                (view1 as ViewCommandTools).Num = i--;
        }






    }
}
