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

    public partial class ViewCommandEdit : UserControl
    {
        public string CommandGroupName { set { this.lblGroupName.Text = value; } }
        public ComboBoxEdit CbxCommandGroup { get { return cbxGroup; } }
        public List<string> CommmandGroups = new List<string>();
        public bool NeedInit = true;
        private bool syncEdit = false;
        private int commandCount = 0;
        public int CommandCount
        {
            get { return commandCount; }
        }
        public Command CommandEdit; 

        public ViewCommandEdit()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 选择命令组,则刷新数据
        /// </summary>
        private void RequestCommandData(object sender, EventArgs e)
        {
            if (cbxGroup.SelectedIndex == -1) return;
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
        
        /// <summary>
        /// 初始化指令配置
        /// </summary>
        public void InitViewCommand(Device device)
        {       
            cbxGroup.Properties.Items.Clear();
            foreach (string groupStr in CommmandGroups)
                cbxGroup.Properties.Items.Add(groupStr);

            foreach (Control ctrl in xscCommands.Controls)
                xscCommands.Controls.Remove(ctrl);
            int addCount = (int)edtEndNum.Value;
            while (addCount-- > 0)
                addViewCommandSetting();

            CommandEdit = new Command(device);
            CommandEdit.OnCallbackUI_Action += this.callbackUI;

            cbxGroup.SelectedIndex = 0;
            NeedInit = false;
        }


        /// <summary>
        /// 更新组名
        /// </summary>
        public void UpdateGroupName()
        {
            int i = 0;
            if (cbxGroup.Properties.Items.Count == CommmandGroups.Count)
                foreach (string groupStr in CommmandGroups)
                    cbxGroup.Properties.Items[i++] = groupStr;
            else
            {
                cbxGroup.Properties.Items.Clear();
                foreach (string groupStr in CommmandGroups)
                    cbxGroup.Properties.Items.Add(groupStr);
            }
        }


        /// <summary>
        /// 回调
        /// </summary>
        private void callbackUI(object[] values)
        {

        }

        /// <summary>
        /// 获取指令
        /// </summary>
        public void ReadCommandData()
        {
            int count = (int)edtEndNum.Value;
            while (count < commandCount)
                addViewCommandSetting();
            while (count > commandCount)
                xscCommands.Controls.RemoveAt(--commandCount);
            
            CommandEdit.ReadCommandData(cbxGroup.SelectedIndex, 1, (int)edtEndNum.Value);
        }
    }
}
