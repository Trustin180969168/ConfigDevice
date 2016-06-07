using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ConfigDevice
{
    public partial class ViewCommandSetting : UserControl
    {
        public string CommandGroupName { set { this.lblGroupName.Text = value; } }
        public ToolStripComboBox CbxCommandGroup { get { return cbxGroup; } }
        /// <summary>
        /// 是否显示组选择
        /// </summary>
        public bool ShowGroupCtrl
        {
            set
            {
                lblGroupName.Visible = value;
                cbxGroup.Visible = value;
                if (!value) 
                    plCommandToolbar.Width -= 300; 
                else
                    plCommandToolbar.Width += 300; 
            }
        }

        public List<string> CommmandGroups = new List<string>();
        public bool NeedInit = true;
        private bool syncEdit = false;
        private int commandCount = 0;

        public int CommandCount
        {
            get { return commandCount; }
        }
        public CommandList CommandEdit;

        public ViewCommandSetting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取指令数据
        /// </summary>
        /// <param name="udpResult"></param>
        /// <param name="values"></param>
        private void returnCommandData(CallbackParameter parameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(returnCommandData), parameter);
                return;
            }
            UserUdpData userData = (UserUdpData)parameter.Parameters[0];
            CommandData commandData = new CommandData(userData);
            //----暂时不用多一条的情况
            //while (commandCount < commandData.ucCmdNum + 2)
            //    addViewCommandSetting();
            while (commandCount < commandData.ucCmdNum + 1)
                addViewCommandSetting();
            foreach (Control ctrl in xscCommands.Controls)
            {
                ViewCommandTools viewCommand = ctrl as ViewCommandTools;
                if (viewCommand.Num - 1 == commandData.ucCmdNum)
                {
                    viewCommand.SetCommandData(userData);
                    break;
                }
            }
        }

        /// <summary>
        /// 获取指令
        /// </summary>
        public void ReadCommandData()
        {
            if (!NeedInit)
            {
                if (edtBeginNum.Value > edtEndNum.Value) return;
                int count = (int)edtEndNum.Value;
                while (count > commandCount)
                    addViewCommandSetting();
                while (count < commandCount)
                    removeViewCommandSetting();
                //------清空------
                foreach (Control view in xscCommands.Controls)
                {
                    ViewCommandTools commandView = view as ViewCommandTools;
                    if (commandView.Num > count)
                        continue;
                    else if (commandView.Num >= edtBeginNum.Value && commandView.Num <= count)
                        commandView.CleanCommandSetting();
                }
                //------隐藏不符合查询的条目----
                foreach (Control view in xscCommands.Controls)
                {
                    ViewCommandTools commandView = view as ViewCommandTools;
                    if (commandView.Num < edtBeginNum.Value)
                        commandView.Visible = false;
                    else
                        commandView.Visible = true;
                }
                //while (xscCommands.Controls.Count > 0)
                //    removeViewCommandSetting();
                AddDefaultNullCommand();//----默认添加一条空指令
                CommandEdit.ReadCommandData(cbxGroup.SelectedIndex, (int)edtBeginNum.Value - 1, (int)edtEndNum.Value - 1);//序号从0开始
            }
        }

        /// <summary>
        /// 添加默认空指令
        /// </summary>
        private void AddDefaultNullCommand()
        {
            if (commandCount == 0)
                addViewCommandSetting();
            else
            {
                foreach (Control ctrl in xscCommands.Controls)
                {
                    ViewCommandTools viewCommand = ctrl as ViewCommandTools;
                    if (viewCommand.Num == commandCount)
                    {
                        if (!viewCommand.IsNull)
                            addViewCommandSetting();
                        break;
                    }
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
            viewNew.DelCommandData += this.DelCommandData;
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
      
            cbxGroup.SelectedIndex = -1;
            cbxGroup.Items.Clear();
            foreach (string groupStr in CommmandGroups)
                cbxGroup.Items.Add(groupStr);

            int addCount = (int)edtEndNum.Value;//----指令的加载个数
            CommandEdit = new CommandList(device);
            CommandEdit.OnCallbackUI_Action += this.returnCommandData;//命令的执行的界面回调
            NeedInit = false;//---标记初始化完毕
            AddDefaultNullCommand();//----默认保留一条空指令便于添加-----
            if (CommmandGroups.Count > 0)
            {
                cbxGroup.SelectedIndex = 0;//执行读取
                cbxGroup.Text = CommmandGroups[0];//选择第一组/键      
            }
          
        }

        /// <summary>
        /// 更新组名
        /// </summary>
        public void UpdateGroupName()
        {
            NeedInit = true;//---更新名称,不用执行指令配置列表
            int i = 0; int index = cbxGroup.SelectedIndex;
            if (cbxGroup.Items.Count == CommmandGroups.Count)
            {             
                foreach (string groupStr in CommmandGroups)
                    cbxGroup.Items[i++] = groupStr;
                cbxGroup.Text = CommmandGroups[index];              
            }
            else
            {
                cbxGroup.Items.Clear();
                foreach (string groupStr in CommmandGroups)
                    cbxGroup.Items.Add(groupStr);
                cbxGroup.SelectedIndex = 0;//执行读取
                cbxGroup.Text = CommmandGroups[0];
            }
            NeedInit = false;
        }

        /// <summary>
        /// 回调
        /// </summary>
        private void callbackUI(object[] values)
        {

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

        /// <summary>
        /// 保存指令
        /// </summary>
        private void btSaveCommands_Click(object sender, EventArgs e)
        {
            foreach (Control view in xscCommands.Controls)
            {
                ViewCommandTools commandView = view as ViewCommandTools;
                if (!commandView.HasChanged) continue;
                CommandData command =  commandView.GetCommandData();
                if (command == null) continue;
                command.ucCmdType=0;
                command.ucCmdKey =  cbxGroup.SelectedIndex;
                command.ucCmdNum = commandView.Num - 1;
                CommandEdit.SaveCommandData(command);
                commandView.DataCommandSetting.AcceptChanges();
            }
        }
        
        /// <summary>
        /// 删除命令
        /// </summary>
        /// <param name="cmdNum">命令编号</param>
        public void DelCommandData(int cmdNum)
        {
            CommandEdit.DelCommandData(cbxGroup.SelectedIndex, cmdNum, cmdNum);
        }

        /// <summary>
        /// 添加指令
        /// </summary>
        private void btAdd_Click(object sender, EventArgs e)
        {
            this.addViewCommandSetting();
        }


    }
}
