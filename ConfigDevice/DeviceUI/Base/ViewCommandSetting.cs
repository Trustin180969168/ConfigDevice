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

    public partial class ViewCommandSetting : UserControl
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
        public CommandCtrl CommandEdit;

        public ViewCommandSetting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitViewCommandSetting(Device DeviceEdit)
        {
            CommandEdit = new CommandCtrl(DeviceEdit);
            CommandEdit.OnCallbackUI_Action += returnCommandData;
        }



        /// <summary>
        /// 获取指令数据
        /// </summary>
        /// <param name="udpResult"></param>
        /// <param name="values"></param>
        private void returnCommandData(object[] values)
        {
            CommandData commandData = (CommandData)values[0];
            foreach (Control ctrl in xscCommands.Controls)
            {
                ViewCommandTools viewCommand = ctrl as ViewCommandTools;
                if (viewCommand.Num + 1 == commandData.ucCmdNum)
                {
                    viewCommand.SetCommandData(commandData);
                    return;
                }
            }
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
        /// 移除指令配置
        /// </summary>
        private void removeViewCommandSetting()
        {
            xscCommands.Controls.RemoveAt(0); --commandCount;
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

            int addCount = (int)edtEndNum.Value;

            CommandEdit = new CommandCtrl(device);
            CommandEdit.OnCallbackUI_Action += this.callbackUI;

            cbxGroup.SelectedIndex = 0;//执行读取
            cbxGroup.Text = CommmandGroups[0];
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
                cbxGroup.SelectedIndex = 0;//执行读取
                cbxGroup.Text = CommmandGroups[0];
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
            while (count > commandCount)
                addViewCommandSetting();
            while (count < commandCount)
                removeViewCommandSetting();

            CommandEdit.ReadCommandData(cbxGroup.SelectedIndex, 0, (int)edtEndNum.Value-1);//序号从0开始
        }


        /// <summary>
        /// 刷新指令
        /// </summary>
        private void btRefresh_Click(object sender, EventArgs e)
        {
            ReadCommandData();
        }
        private void cbxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxGroup.SelectedIndex == -1)
            { cbxGroup.Text = CommmandGroups[0]; cbxGroup.SelectedIndex = 0; }//----由选择框获取指令---
            else
                ReadCommandData();
        }

        private void edtEndNum_ValueChanged(object sender, EventArgs e)
        {
            ReadCommandData();
        }

    }
}
