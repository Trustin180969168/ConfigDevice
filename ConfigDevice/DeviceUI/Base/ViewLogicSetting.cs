using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;

namespace ConfigDevice
{
    public partial class ViewLogicSetting : UserControl
    {
        public bool NeedInit = true;
        public LogicList logicList;//---逻辑配置----
        private Circuit circuit;//--回路----
        private LookupIDAndNameTable dtIDName = new LookupIDAndNameTable();
        public int LogicNum { get { return lookUpEdit.ItemIndex + 1; } }
        public string LogicName { get { return edtTriggerActionName.Text; } }
        public Circuit Circuit
        {
            get { return circuit; }
            set { circuit = value; if (circuit != null) circuit.OnCallbackUI_Action += this.callbackUI; }
        }
        public LookUpEdit LookUpEdit { get { return this.lookUpEdit; } }

        /// <summary>
        /// 显示标题
        /// </summary>
        public bool ShowToolBar
        {
            set
            {
                plToolbarCtrl.Visible = value;
            }
        }

        public bool ShowLogicListCaption
        {
            set
            { 
                   gcCtrl.ShowCaption =value; 
            }
        }

        public ViewLogicSetting()
        {
            InitializeComponent();
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic4OR);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic4AND);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic3OR_1AND);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic3AND_1OR);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic2OR_2AND_OR);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic2OR_2AND_AND);

            //-------------回路查询选择------
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_ID, "回路", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None));
            lookUpEdit.Properties.Columns.Add(new LookUpColumnInfo(ViewConfig.DC_NAME, 380));

            lookUpEdit.Properties.Name = "lookupEdit";
            lookUpEdit.Properties.DisplayMember = ViewConfig.DC_NAME;
            lookUpEdit.Properties.ValueMember = ViewConfig.DC_ID;
            lookUpEdit.Properties.ShowFooter = false;
            lookUpEdit.Properties.ShowHeader = false;
            lookUpEdit.Properties.DataSource = dtIDName;

            NeedInit = true;
        }


        /// <summary>
        /// 回调
        /// </summary>
        private void callbackUI(CallbackParameter callbackParameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(callbackUI), callbackParameter);
                return;
            } 
            //--------------读取完逻辑名称列表-----
            if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Circuit.CLASS_NAME)
            {
                dtIDName.Rows.Clear();
                foreach (int key in  Circuit.ListCircuitIDAndName.Keys)
                {
                    dtIDName.Rows.Add(new object[] { key, Circuit.ListCircuitIDAndName[key] });
                } 
                lookUpEdit.Properties.DataSource = dtIDName; 
            }
        }


        /// <summary>
        /// 初始化逻辑列表
        /// </summary>
        /// <param name="triggers"></param>
        public void InitLogicList(Device device, params string[] triggers)
        {
            foreach (Control viewlogic in plLogicList.Controls)
                (viewlogic as ViewLogicTools).InitTriggerList(triggers);
            logicList = new LogicList(device);
            logicList.OnCallbackUI_Action += this.returnLogicData;//命令的执行的界面回调
            NeedInit = false;
            lookUpEdit.ItemIndex = 0;
        }

        /// <summary>
        /// 获取指令数据
        /// </summary>
        /// <param name="udpResult"></param>
        /// <param name="values"></param>
        private void returnLogicData(CallbackParameter parameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(returnLogicData), parameter);
                return;
            }
            LogicData logicData = parameter.Parameters[0] as LogicData;
            imageComboBoxEdit.SelectedIndex = logicData.Logic4KindID;
            viewLogicTools1.SetLogicData(logicData.TriggerList[0]);
            viewLogicTools2.SetLogicData(logicData.TriggerList[1]);
            viewLogicTools3.SetLogicData(logicData.TriggerList[2]);
            viewLogicTools4.SetLogicData(logicData.TriggerList[3]);
        }


        /// <summary>
        /// 逻辑配置组选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            lblNum.Text = lookUpEdit.EditValue.ToString() + "、";
            edtTriggerActionName.Text = lookUpEdit.Text;
            ReadLogicList(LogicNum); 
        }

 

        private void edtTriggerActionName_DoubleClick(object sender, EventArgs e)
        {
            lookUpEdit.ShowPopup();
        }

        private void btSaveTrigger_Click(object sender, EventArgs e)
        {
            SaveLogicList();
        }

        /// <summary>
        /// 保存逻辑列表
        /// </summary>
        public void SaveLogicList()
        {
            Circuit.SaveRoadSetting( LogicNum - 1,edtTriggerActionName.Text);//----保存回路名称---
        }

        /// <summary>
        /// 读取逻辑列表
        /// </summary>
        public void ReadLogicList(int num)
        { 
            logicList.ReadLogicData(num - 1); 
        }
        public void ReadLogicList()
        {
            logicList.ReadLogicData(lookUpEdit.ItemIndex + 1);
        }

        /// <summary>
        /// 刷新触发器
        /// </summary> 
        private void btRefreshTrigger_Click(object sender, EventArgs e)
        {
            ReadLogicList();
        }

    }
}
