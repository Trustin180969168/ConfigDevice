using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;
using System.Drawing;

namespace ConfigDevice
{
    public abstract class BaseViewLogicControl
    {
        public const string TRIGGER_KIND_VALUE = "触发值";
        public const string TRIGGER_KIND_LEVEL = "级别";
        public const string TRIGGER_KIND_PERIPHERAL = "外设";

        public const string TRIGGER_OPERATE_EQUAL = "等于";
        public const string TRIGGER_OPERATE_LESS_THAN = "小于";
        public const string TRIGGER_OPERATE_MORE_THAN = "大于";
        public const string TRIGGER_OPERATE_WITHIN = "以内";
        public const string TRIGGER_OPERATE_EXCEPT = "以外"; 

        public Device deviceTrigger;//触发设备对象
        public GridView gvLogic;//逻辑列表

        protected GridViewComboBox cbxOperate = new GridViewComboBox();//运算选择
        protected GridViewComboBox cbxKind = new GridViewComboBox();//触发类型
        protected GridViewTextEdit InvalidEdit = new GridViewTextEdit();//无效编辑
        protected GridColumn dcTriggerObj;//触发对象
        protected GridColumn dcTriggerPosition;//触发位置
        protected GridColumn dcTriggerKind;//触发类型
        protected GridColumn dcDifferentDevice;//差异设备
        protected GridColumn dcOperate;//触发运算
        protected GridColumn dcStartValue;//初始值
        protected GridColumn dcEndValue;//结束值
        protected GridColumn dcValid;//持续时间
        protected GridColumn dcInvalid;//无效时间

        public abstract TriggerData GetLogicData();//获取udp指令
        public abstract void SetLogicData(TriggerData td);//获取逻辑条件 
        public abstract void InitViewSetting();       // 初始化配置界面

        public BaseViewLogicControl(Device _device, GridView gv)
        {
            this.deviceTrigger = _device;
            this.gvLogic = gv;
            InvalidEdit.ReadOnly = true;
            InvalidEdit.NullText = "无效";
            InvalidEdit.AllowFocused = false; 

            dcTriggerObj = gv.Columns.ColumnByFieldName(ViewConfig.DC_OBJECT);//触发对象
            dcTriggerKind = gv.Columns.ColumnByFieldName(ViewConfig.DC_KIND);//触发类型
            dcOperate = gv.Columns.ColumnByFieldName(ViewConfig.DC_OPERATION);//运算操作
            dcStartValue = gv.Columns.ColumnByFieldName(ViewConfig.DC_START_VALUE);//初始值
            dcEndValue = gv.Columns.ColumnByFieldName(ViewConfig.DC_END_VALUE);//结束值 
            dcValid = gv.Columns.ColumnByFieldName(ViewConfig.DC_VALID_TIME);//有效值
            dcInvalid = gv.Columns.ColumnByFieldName(ViewConfig.DC_INVALID_TIME);//无效值
            
            dcTriggerKind.ColumnEdit = this.cbxKind;//---触发类型
            cbxOperate.Items.Clear();//----清空触发运算
            dcOperate.ColumnEdit = this.cbxOperate;//---触发运算符 ,统一为下拉选择----  
            
        }

        /// <summary>
        /// 设置无效
        /// </summary>
        /// <param name="gc"></param>
        protected void setGridColumnInvalid(GridColumn gc)
        {
            DataRow dr = gvLogic.GetDataRow(0);
            if(gc.FieldName != "")
                dr[gc.FieldName] = null;
            dr.EndEdit();
            gc.ColumnEdit = InvalidEdit;            
            gc.AppearanceCell.BackColor = Color.Gainsboro;//灰色
            gc.AppearanceCell.ForeColor = Color.Black;
            gc.OptionsColumn.AllowEdit = false;
        }

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





    }
}