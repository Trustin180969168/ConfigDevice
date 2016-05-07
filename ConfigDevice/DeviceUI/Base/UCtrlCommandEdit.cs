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
        private int commandCount = 0;
        public UCtrlCommandEdit()
        {
            InitializeComponent();
            int addCount = 5;
            while (addCount-- > 0)
                addViewCommandSetting();
        }

        /// <summary>
        /// 选择命令组,则刷新数据
        /// </summary>
        private void RequestCommandData(object sender, EventArgs e)
        {
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
            {
                (view as ViewCommandTools).Checked = syncEdit;
            }
        }



        /// <summary>
        /// 添加指令配置
        /// </summary>
        private ViewCommandTools addViewCommandSetting()
        {            
            ViewCommandTools viewNew = new ViewCommandTools(++commandCount);
            xscCommands.Controls.Add(viewNew);
            viewNew.Dock = DockStyle.Top;
            viewNew.SyncCommandEdit += this.SyncCommandSetting;
            (viewNew as Control).BringToFront();
            return viewNew;
        }


        /// <summary>
        /// 同步编辑
        /// </summary>
        public void SyncCommandSetting(ViewCommandTools value)
        {
            foreach (Control view in xscCommands.Controls)
            {
                ViewCommandTools syncView = view as ViewCommandTools;
                if (syncView.Num == value.Num) continue;      
                if (!syncView.Checked) continue;
                syncView.SyncCommandEdit -= this.SyncCommandSetting;//-----避免循环或者混乱回调,先退后订-----
                syncView.SyncCommandSettingEdit(value);
                syncView.SyncCommandEdit += this.SyncCommandSetting;
            }
        }


        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            addViewCommandSetting();
        }






    }
}
