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
        public LogicList logicList; //---逻辑配置----
        private Circuit circuit;    //---回路----
        private int currentLogicNum; //---当前逻辑----
        private LookupIDAndNameTable dtIDName = new LookupIDAndNameTable();
        public int LogicItemIndex { get { return lookUpEdit.ItemIndex; } }
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
        /// 获取指令数据
        /// </summary>
        public void ReturnLogicData(CallbackParameter parameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(ReturnLogicData), parameter);
                return;
            }
            LogicData logicData = parameter.Parameters[0] as LogicData;
            imageComboBoxEdit.SelectedIndex = logicData.Logic4KindID;
            currentLogicNum = logicData.Logic4KindID;
            viewLogicTools1.SetTriggerData(logicData.TriggerList[0]);
            viewLogicTools2.SetTriggerData(logicData.TriggerList[1]);
            viewLogicTools3.SetTriggerData(logicData.TriggerList[2]);
            viewLogicTools4.SetTriggerData(logicData.TriggerList[3]);
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
            logicList.OnCallbackUI_Action += this.ReturnLogicData;//命令的执行的界面回调
            NeedInit = false;
            lookUpEdit.ItemIndex = 0;
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
            ReadLogicList(LogicItemIndex); 
        }

        private void edtTriggerActionName_DoubleClick(object sender, EventArgs e)
        {
            lookUpEdit.ShowPopup();
        }

        private void btSaveTrigger_Click(object sender, EventArgs e)
        {
            SaveLogicName();
            SaveLogicData();
        }

        /// <summary>
        /// 保存逻辑列表名称
        /// </summary>
        public void SaveLogicName()
        {
            Circuit.SaveRoadSetting(lookUpEdit.ItemIndex,edtTriggerActionName.Text);//----保存回路名称---            
        }

        /// <summary>
        /// 保存逻辑数据
        /// </summary>
        public void SaveLogicData()
        {
            if (viewLogicTools1.HasChanged || viewLogicTools2.HasChanged || viewLogicTools3.HasChanged ||
                viewLogicTools4.HasChanged || currentLogicNum != imageComboBoxEdit.SelectedIndex)
            {
                saveLogicData();
            }
        }
        /// <summary>
        /// 保存逻辑数据
        /// </summary>
        /// <param name="groupNum">序号</param>
        /// <param name="changed">是否改</param>
        public void SaveLogicData(int groupNum,bool changed)
        {
            if (viewLogicTools1.HasChanged || viewLogicTools2.HasChanged || viewLogicTools3.HasChanged ||
                viewLogicTools4.HasChanged || currentLogicNum != imageComboBoxEdit.SelectedIndex || changed)
            {

                byte[] logicValue = GetLogicData();
                logicValue[0] = (byte)groupNum;
                logicList.SaveLogicData(logicValue);
            }
        }
        private void saveLogicData()
        {
            byte[] logicValue = GetLogicData();
            logicValue[0] = (byte)lookUpEdit.ItemIndex;
            logicList.SaveLogicData(logicValue);
        }
        public byte[] GetLogicData()
        {
            byte[] logicValue = new byte[2 + 31 * 4];
            logicValue[1] = (byte)imageComboBoxEdit.SelectedIndex;
            byte[] value1 = viewLogicTools1.GetTriggerData().Value();
            byte[] value2 = viewLogicTools2.GetTriggerData().Value();
            byte[] value3 = viewLogicTools3.GetTriggerData().Value();
            byte[] value4 = viewLogicTools4.GetTriggerData().Value();

            Buffer.BlockCopy(value1, 0, logicValue, 2, 31);
            Buffer.BlockCopy(value2, 0, logicValue, 33, 31);
            Buffer.BlockCopy(value3, 0, logicValue, 64, 31);
            Buffer.BlockCopy(value4, 0, logicValue, 95, 31);
            return logicValue;
        }


        /// <summary>
        /// 读取逻辑列表
        /// </summary>
        public void ReadLogicList(int num)
        {
            ClearTrggerData();
            logicList.ReadLogicData(num); 
        }
        /// <summary>
        /// 清空触发列表
        /// </summary>
        public void ClearTrggerData()
        {
            viewLogicTools1.ClearTriggerView();
            viewLogicTools2.ClearTriggerView();
            viewLogicTools3.ClearTriggerView();
            viewLogicTools4.ClearTriggerView();

            viewLogicTools1.GoUp = this.ChangeUp;    //---向上----
            viewLogicTools1.GoDown = this.ChangeDown;//---向下----
            viewLogicTools2.GoUp = this.ChangeUp;    //---向上----
            viewLogicTools2.GoDown = this.ChangeDown;//---向下----
            viewLogicTools3.GoUp = this.ChangeUp;    //---向上----
            viewLogicTools3.GoDown = this.ChangeDown;//---向下----
            viewLogicTools4.GoUp = this.ChangeUp;    //---向上----
            viewLogicTools4.GoDown = this.ChangeDown;//---向下----
        }
        public void ReadLogicList()
        {
            logicList.ReadLogicData((int)lookUpEdit.ItemIndex + 1);
        }

        /// <summary>
        /// 刷新触发器
        /// </summary> 
        private void btRefreshTrigger_Click(object sender, EventArgs e)
        {
            ReadLogicList();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            plToolBar.Visible = true;
        }

        /// <summary>
        /// 向上更换触发
        /// </summary>
        /// <param name="triggerNum">触发ID</param>
        public void ChangeUp(int triggerNum)
        {
            if (triggerNum == 1) return;
            int changeNum = triggerNum - 1;    
            ViewLogicTools souChangeLogicTools = getViewLogicTools(triggerNum);
            ViewLogicTools desChangeLogicTools = getViewLogicTools(changeNum);
            TriggerData souChangedTriggerData = souChangeLogicTools.GetTriggerData();//---源触发数据---
            TriggerData desChangedTriggerData = desChangeLogicTools.GetTriggerData();//----目标触发数据-----
            souChangeLogicTools.SetTriggerData(desChangedTriggerData);//----交换数据---
            desChangeLogicTools.SetTriggerData(souChangedTriggerData);//----交换数据-----

        }

        /// <summary>
        /// 向下更换触发
        /// </summary>
        /// <param name="triggerNum">触发ID</param>
        public void ChangeDown(int triggerNum)
        {
            if (triggerNum == 4) return;
            int changeNum = triggerNum + 1;
            ViewLogicTools souChangeLogicTools = getViewLogicTools(triggerNum);
            ViewLogicTools desChangeLogicTools = getViewLogicTools(changeNum);
            TriggerData souChangedTriggerData = souChangeLogicTools.GetTriggerData();//---源触发数据---
            TriggerData desChangedTriggerData = desChangeLogicTools.GetTriggerData();//----目标触发数据-----
            souChangeLogicTools.SetTriggerData(desChangedTriggerData);//----交换数据---
            desChangeLogicTools.SetTriggerData(souChangedTriggerData);//----交换数据-----

        }


        /// <summary>
        /// 获取触发数据
        /// </summary>
        /// <param name="Num">触发条件序号</param>
        /// <returns></returns>
        private ViewLogicTools getViewLogicTools(int TriggerNum)
        {

            ViewLogicTools value = null;
            foreach (Control viewlogic in plLogicList.Controls)
            {
                value = (viewlogic as ViewLogicTools);
                if (value.Num == TriggerNum)
                    return value;
            }
            return value;
        }







    }
}
