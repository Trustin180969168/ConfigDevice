﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class KeySettingTools : UserControl
    {
        public bool NeedInit = true;
        public KeyList keyList; //---逻辑配置----          
        public Circuit KeyCircuit;//---按键回路,由调用者(设备提供)----
        private DataTable dtKeyData;//---按键列表
        private int showCount = 2;//---显示按键个数----
        private GridViewComboBox cbxControlObj;//---灯的下拉选择--
        private GridViewComboBox cbxLightControlKind;//---灯的下拉选择--
        private GridViewComboBox cbxElseControlKind;//----其他的下拉选择---
        private BaseKeySetting keySetting;//---按键配置对象---

        public bool ShowToolbar
        {
            set
            {
                tsPages.Visible = value;
            }

        }

        public KeySettingTools()
        {
            InitializeComponent();

            dtKeyData = new DataTable("按键列表");
            cbxControlObj = new GridViewComboBox();//---灯的下拉选择--
            cbxLightControlKind = new GridViewComboBox();//---灯的下拉选择--
            cbxElseControlKind = new GridViewComboBox();//----其他的下拉选择--- 

            dtKeyData.Columns.Add(ViewConfig.DC_NUM, System.Type.GetType("System.Int16"));          //----按键编号
            dtKeyData.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));        //---名称---
            dtKeyData.Columns.Add(ViewConfig.DC_CONTROL_OBJ, System.Type.GetType("System.String")); //---控制对象
            dtKeyData.Columns.Add(ViewConfig.DC_CONTROL_KIND, System.Type.GetType("System.String"));//---控制类型
            dtKeyData.Columns.Add(ViewConfig.DC_COMMUNICATE_KIND, System.Type.GetType("System.String"));//---通讯模式
            dtKeyData.Columns.Add(ViewConfig.DC_DIRECTION_MAX, System.Type.GetType("System.String"));//方向最大值
            dtKeyData.Columns.Add(ViewConfig.DC_DIRECTION_MIN, System.Type.GetType("System.String"));//方向最小值
            dtKeyData.Columns.Add(ViewConfig.DC_DIRECTION_STEP, System.Type.GetType("System.String")); //方向步进
            dtKeyData.Columns.Add(ViewConfig.DC_RELEVANCE_NUM, System.Type.GetType("System.String"));//关联号
            dtKeyData.Columns.Add(ViewConfig.DC_MUTEX_NUM, System.Type.GetType("System.String"));   //互斥号 

            dcNum.FieldName = ViewConfig.DC_NUM;
            dcName.FieldName = ViewConfig.DC_NAME;
            dcCtrlObj.FieldName = ViewConfig.DC_CONTROL_OBJ;
            dcCtrlKind.FieldName = ViewConfig.DC_CONTROL_KIND;
            dcComunication.FieldName = ViewConfig.DC_COMMUNICATE_KIND;
            dcDectMax.FieldName = ViewConfig.DC_DIRECTION_MAX;
            dcDectMin.FieldName = ViewConfig.DC_DIRECTION_MIN;
            dcStep.FieldName = ViewConfig.DC_DIRECTION_STEP;
            dcRelevance.FieldName = ViewConfig.DC_RELEVANCE_NUM;
            dcMutex.FieldName = ViewConfig.DC_MUTEX_NUM;
            
            cbxLightControlKind.Items.Add(ViewConfig.KEY_CONTROL_KIND_NAME_OPEN_CLOSE);
            cbxLightControlKind.Items.Add(ViewConfig.KEY_CONTROL_KIND_NAME_OPEN);
            cbxLightControlKind.Items.Add(ViewConfig.KEY_CONTROL_KIND_NAME_CLOSE);
            cbxLightControlKind.Items.Add(ViewConfig.KEY_CONTROL_KIND_NAME_LOOP_LIGHT);
            cbxLightControlKind.Items.Add(ViewConfig.KEY_CONTROL_KIND_NAME_OPEN_CLOSE_LOOP_LIGHT);
            cbxLightControlKind.Items.Add(ViewConfig.KEY_CONTROL_KIND_NAME_OPEN_LOOP_LIGHT);
            cbxLightControlKind.Items.Add(ViewConfig.KEY_CONTROL_KIND_NAME_CLOSE_LOOP_LIGHT);
            cbxElseControlKind.Items.Add(ViewConfig.KEY_CONTROL_KIND_NAME_OPEN_CLOSE);
            cbxElseControlKind.Items.Add(ViewConfig.KEY_CONTROL_KIND_NAME_OPEN);
            cbxElseControlKind.Items.Add(ViewConfig.KEY_CONTROL_KIND_NAME_CLOSE);
        }

        /// <summary>
        /// 初始化配置按键配置列表
        /// </summary>
        /// <param name="deviceControled">设备对象</param>
        /// <param name="_showCount">显示个数</param>
        /// <param name="deviceControled">分页标题</param>
        public void InitKeySettingList(Device deviceControled,int _showCount, params string[] pageTitles)
        {
            showCount = _showCount;
            tsPages.Items.Clear();
            for (int i = 0; i < pageTitles.Length; i++)
            {
                ToolStripButton tsb = new ToolStripButton(pageTitles[i]);
                tsb.Tag = i*8;
                tsb.Click += this.PageSelect;
                tsb.Checked = false;

                if(pageTitles[i] == ViewConfig.LCD_CAPTION_SCENE)
                    tsb.Image = global::ConfigDevice.Properties.Resources.scene;
                else if (pageTitles[i] == ViewConfig.LCD_CAPTION_LIGHT)
                    tsb.Image = global::ConfigDevice.Properties.Resources.lights;
                else if (pageTitles[i] == ViewConfig.LCD_CAPTION_CURTAIN)
                    tsb.Image = global::ConfigDevice.Properties.Resources.curtain;
                else if (pageTitles[i] == ViewConfig.LCD_CAPTION_LEAVE_BACK)
                    tsb.Image = global::ConfigDevice.Properties.Resources.Leave_Back; 
                this.tsPages.Items.Add(tsb);
            }

            //dtKeyData.Rows.Clear();
            //for (int i = 0; i < showCount; i++)
            //    dtKeyData.Rows.Add(i+1);
            dtKeyData.AcceptChanges();

            keySetting = new BaseKeySetting(gvKeyData);//---按键配置对象---
            keyList = new KeyList(deviceControled);
            keyList.OnCallbackUI_Action += this.ReturnKeyData;

            cbxControlObj.Items.Add(ViewConfig.KEY_TYPE_NAME_LIGHT);
            cbxControlObj.Items.Add(ViewConfig.KEY_TYPE_NAME_SOUND);
            cbxControlObj.Items.Add(ViewConfig.KEY_TYPE_NAME_CURTAIN);
            cbxControlObj.Items.Add(ViewConfig.KEY_TYPE_NAME_HELP);
            dcCtrlObj.ColumnEdit = cbxControlObj;

            gcKeyData.DataSource = dtKeyData;

            KeyCircuit = deviceControled.ContrlObjs[DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME] as Circuit;
            KeyCircuit.OnCallbackUI_Action += ReturnKeyName;

        }

        /// <summary>
        /// 按键页选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageSelect(object sender, EventArgs e)
        {
            foreach (ToolStripItem tsi in tsPages.Items)
                (tsi as ToolStripButton).Checked = false;
            ToolStripButton tsb = (sender as ToolStripButton);
            tsb.Checked = true;   
            int startNum = (int)tsb.Tag;
            ReadKeyData(startNum, startNum + showCount - 1);
        }

        /// <summary>
        /// 读取并显示按键数据
        /// </summary>
        public void ReturnKeyData(CallbackParameter parameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(ReturnKeyData), parameter); 
                return;
            }
            KeyData keyData = parameter.Parameters[0] as KeyData;
            int index = keyData.KeyNum % 8;

            while (dtKeyData.Rows.Count < index+1)
                dtKeyData.Rows.Add(index+1);

            DataRow dr = gvKeyData.GetDataRow(index);//---获取分页的对应行
            if (dr != null)
            {
                keySetting.SetKeyData(keyData, dr);//---赋值到行----
                //----添加名称------- 
                dr[ViewConfig.DC_NAME] = KeyCircuit.ListCircuitIDAndName[keyData.KeyNum + 1];
                dr.AcceptChanges();

                gvKeyData.RefreshData();
                gvKeyData.BestFitColumns();

                if (index == 0)
                {
                    string controlObj = dr[dcCtrlObj.FieldName].ToString();
                    if (controlObj == ViewConfig.KEY_TYPE_NAME_LIGHT)
                        dcCtrlKind.ColumnEdit = cbxLightControlKind;
                    else
                        dcCtrlKind.ColumnEdit = cbxElseControlKind;
                }
            }

        }

        /// <summary>
        /// 读取并显示按键名称
        /// </summary>
        public void ReturnKeyName(CallbackParameter parameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(ReturnKeyName), parameter);
                return;
            }
            else
            {
                if (parameter.Parameters != null && parameter.Parameters[0].ToString() == Circuit.CLASS_NAME)//---回路名称--
                {
                    NeedInit = false;//----回路读取完毕为初始化完毕----       
                    if (tsPages.Items.Count == 0) return;
                    PageSelect(tsPages.Items[0], null);//---默认自动读取第一个----
                   // keyList.ReadKeyData(0, showCount - 1);
                }
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshData()
        {
            foreach (ToolStripItem tsi in tsPages.Items)
                if ((tsi as ToolStripButton).Checked)
                {
                    PageSelect(tsi, null);
                    break;
                }
        }

        /// <summary>
        /// 申请读取按键配置
        /// </summary>
        /// <param name="startKey">开始按键</param>
        /// <param name="endKey">结束按键</param>
        public void ReadKeyData(int startKey,int endKey)
        {
            dtKeyData.Rows.Clear();
            keyList.ReadKeyData(startKey, endKey);//---读取按键配置----
        }

        /// <summary>
        /// 保存按键配置
        /// </summary>
        public void SaveKeyData()
        {
            gvKeyData.PostEditor();
            DataRow drCurrent = gvKeyData.GetDataRow(gvKeyData.FocusedRowHandle);
            if (drCurrent != null)
                drCurrent.EndEdit();
                        
            DataTable dtModify = dtKeyData.GetChanges(DataRowState.Modified);
            if (dtModify == null) return;
            //----保存配置信息------
            foreach (DataRow dr in dtModify.Rows)
                keyList.SaveKeyData(keySetting.GetKeyData(dr));
            //----保存名称-----
            foreach (DataRow dr in dtModify.Rows)
            {
                string changeName = dr[ViewConfig.DC_NAME].ToString();
                int changeID = Convert.ToInt16(dr[ViewConfig.DC_NUM]);
                if(KeyCircuit.ListCircuitIDAndName[changeID] != changeName)
                    KeyCircuit.SaveRoadSetting(changeID - 1, changeName);//--保存回路名称---
            }
            dtKeyData.AcceptChanges();
        }


        /// <summary>
        /// 切换控制类型编辑
        /// </summary>
        private void gvKeyData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvKeyData.GetDataRow(gvKeyData.FocusedRowHandle);
            if (dr != null)
            {
                string controlObj = dr[dcCtrlObj.FieldName].ToString();
                if (controlObj == ViewConfig.KEY_TYPE_NAME_LIGHT)
                    dcCtrlKind.ColumnEdit = cbxLightControlKind;
                else
                    dcCtrlKind.ColumnEdit = cbxElseControlKind;
            }
        }

    }
}