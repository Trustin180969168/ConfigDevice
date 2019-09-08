﻿using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using DevExpress.XtraGrid.Columns;
using System.Drawing;

namespace ConfigDevice
{
    /// <summary>
    /// 回路指令
    /// </summary>
    public abstract class  BaseViewCommandControl
    {
        public ControlObj controlObj;//控制对象
        protected DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxCommandKind;//选择命令类型编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit tedtTime;//时间类型编辑
        protected GridViewDigitalEdit edtNum;//数字编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit edtPercentNum;//百分比
        protected DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit meEdit;//下拉文本框
        protected GridViewTextEdit InvalidEdit = new GridViewTextEdit();//无效编辑

        public const string  NAME_INVALID_VALUE = "无效";
        //----配置界面列表------
        public GridView ViewSetting;
        public BaseViewCommandControl(ControlObj _controlObj, GridView gv)
        {    
            //----长文本编辑控件------------------
            this.meEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            //----命令编辑控件----
            this.cbxCommandKind = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxCommandKind.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbxCommandKind.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;      
            //----时间编辑控件----
            tedtTime = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            tedtTime.DisplayFormat.FormatString = "d";
            tedtTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            tedtTime.Mask.EditMask = "HH:mm:ss";
            tedtTime.Mask.UseMaskAsDisplayFormat = true;
            tedtTime.Leave += new System.EventHandler(this.time_Leave);
            tedtTime.Leave += new System.EventHandler(SysConfig.Edit_Leave);
            tedtTime.Enter += new System.EventHandler(SysConfig.Edit_Enter);
            //----数字编辑控件-----------
            edtNum = new GridViewDigitalEdit();
            edtNum.AutoHeight = false;
            edtNum.Mask.EditMask = "d";
            edtNum.Name = "edtNum";
            edtNum.MinValue = 0;
            edtNum.MaxValue = 200;
            //----百分比编辑控件-------
            edtPercentNum = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            edtPercentNum.AutoHeight = false;
            edtPercentNum.Mask.EditMask = "P0";
            edtPercentNum.Mask.UseMaskAsDisplayFormat = true;
            edtPercentNum.MaxValue = new decimal(new int[] { 100, 0, 0, 0 });
            edtPercentNum.MinValue = new decimal(new int[] { 0, 0, 0, 0 });
            edtPercentNum.Name = "edtPercentNum";
            edtPercentNum.Leave += new System.EventHandler(SysConfig.Edit_Leave);
            edtPercentNum.Enter += new System.EventHandler(SysConfig.Edit_Enter);
            
            controlObj = _controlObj;
            ViewSetting = gv;
            ViewSetting.Columns.ColumnByName("command").ColumnEdit = cbxCommandKind;
            ViewSetting.Columns.ColumnByFieldName(DeviceConfig.DC_ID).ColumnEdit = edtNum;
            ViewSetting.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).ColumnEdit = edtNum;
            ViewSetting.FocusedColumn = ViewSetting.Columns[3];

            setGridColumnValid(ViewSetting.Columns.ColumnByName("parameter1"), edtNum);
            setGridColumnValid(ViewSetting.Columns.ColumnByName("parameter2"), edtNum);
            setGridColumnValid(ViewSetting.Columns.ColumnByName("parameter3"), edtNum);
            setGridColumnValid(ViewSetting.Columns.ColumnByName("parameter4"), edtNum);
            setGridColumnValid(ViewSetting.Columns.ColumnByName("parameter5"), edtNum);
        }

        /// <summary>
        /// 时间校验
        /// </summary>
        protected virtual void time_Leave(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 初始化配置界面
        /// </summary>
        public abstract void InitViewSetting();

        /// <summary>
        /// 重置
        /// </summary>
        public abstract void ResetSetting();

        
        /// <summary>
        /// 生成指令数据
        /// </summary>
        /// <returns></returns>
        public abstract CommandData GetCommand();

        /// <summary>
        /// 配置指令数据
        /// </summary>
        /// <param name="data">指令数据</param>
        public abstract void SetCommandData(CommandData data);

        /// <summary>
        /// 设置生效
        /// </summary>
        /// <param name="gc"></param>
        /// <param name="editor"></param>
        protected void setGridColumnValid(GridColumn gc, DevExpress.XtraEditors.Repository.RepositoryItem editor)
        {
            gc.ColumnEdit = editor;
            gc.AppearanceCell.BackColor = Color.LightYellow;
            gc.AppearanceCell.ForeColor = Color.Blue;
            gc.OptionsColumn.AllowEdit = true;

        }

        /// <summary>
        /// 设置无效
        /// </summary>
        /// <param name="gc"></param>
        protected void setGridColumnInvalid(GridColumn gc)
        {
            DataRow dr = ViewSetting.GetDataRow(0);
            if (gc.FieldName != "")
                dr[gc.FieldName] = SensorConfig.SENSOR_INVALID;//---内容为无效
            dr.EndEdit();
            gc.ColumnEdit = InvalidEdit;
            gc.AppearanceCell.BackColor = Color.Gainsboro;//灰色
            gc.AppearanceCell.ForeColor = Color.Black;
            gc.OptionsColumn.AllowEdit = false;
        }

    }
}