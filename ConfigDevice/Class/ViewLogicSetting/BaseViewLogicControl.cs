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

        public Device deviceTrigger;//触发设备对象
        public GridView gvLogic;//逻辑列表

        protected GridViewComboBox cbxOperate = new GridViewComboBox();//运算选择
        protected GridViewComboBox cbxKind = new GridViewComboBox();//触发类型
        protected GridViewComboBox cbxPosition = new GridViewComboBox();//触发位置
        protected GridViewTextEdit InvalidEdit = new GridViewTextEdit();//无效编辑
        protected GridViewTimeEdit ValidTimeEdit = new GridViewTimeEdit();//有效触发时间编辑
        protected GridViewTimeEdit InvalidTimeEdit = new GridViewTimeEdit();//无效触发时间编辑
        protected GridViewLookupEdit lookupDevice = new GridViewLookupEdit();//设备选择编辑
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
            ValidTimeEdit.Leave += this.ValidTimeEdit_Leave;
            InvalidTimeEdit.Leave += this.InvalidTimeEdit_Leave;

            dcTriggerObj = gv.Columns.ColumnByFieldName(ViewConfig.DC_OBJECT);//触发对象
            dcTriggerPosition = gv.Columns.ColumnByFieldName(ViewConfig.DC_POSITION);//触发位置
            dcTriggerKind = gv.Columns.ColumnByFieldName(ViewConfig.DC_KIND);//触发类型
            dcDifferentDevice = gv.Columns.ColumnByFieldName(ViewConfig.DC_DEVICE_VALUE);//差异设备            
            dcOperate = gv.Columns.ColumnByFieldName(ViewConfig.DC_OPERATION);//运算操作
            dcStartValue = gv.Columns.ColumnByFieldName(ViewConfig.DC_START_VALUE);//初始值
            dcEndValue = gv.Columns.ColumnByFieldName(ViewConfig.DC_END_VALUE);//结束值 
            dcValid = gv.Columns.ColumnByFieldName(ViewConfig.DC_VALID_TIME);//有效值
            dcInvalid = gv.Columns.ColumnByFieldName(ViewConfig.DC_INVALID_TIME);//无效值

            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_VALUE);//---默认添加触发值---
            dcTriggerKind.ColumnEdit = this.cbxKind;                //---触发类型
            cbxOperate.Items.Clear();                               //----清空触发运算
            dcOperate.ColumnEdit = this.cbxOperate;                 //---触发运算符 ,统一为下拉选择----  
            cbxPosition.Items.Clear();                              //---清空触发位置
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_LOCAL);//---默认位置是本地----
            dcTriggerPosition.ColumnEdit = cbxPosition;             //---触发编辑---- 
            setGridColumnInvalid(dcDifferentDevice);                //---默认差异设备列无效---        
            //------初始化设备选择控件-----
            lookupDevice.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DeviceConfig.DC_NAME, "设备名称", 120, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DeviceConfig.DC_ID, "设备ID", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DeviceConfig.DC_NETWORK_ID, "网段ID", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DeviceConfig.DC_KIND_NAME, "设备类型", 120, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.Ascending)//---默认升序排序--
            });
            lookupDevice.PopupWidth = 500;
            lookupDevice.Name = "lookupEdit";
            lookupDevice.NullText = "选择设备";
            lookupDevice.DisplayMember = DeviceConfig.DC_NAME;
            lookupDevice.ValueMember = ViewConfig.DC_DEVICE_VALUE;
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

        /// <summary>
        /// 创建触发数据,并初始化
        /// </summary>
        /// <param name="dr">行</param>
        /// <returns>TriggerData</returns>
        protected static TriggerData GetInitTriggerData(DataRow dr)
        {
            TriggerData triggerData = new TriggerData();
            triggerData.TriggerObjectID = ViewConfig.TRIGGER_NAME_ID[dr[ViewConfig.DC_OBJECT].ToString()];  //---触发对象--- 
            triggerData.TriggerPositionID = ViewConfig.TRIGGER_POSITION_NAME_ID[dr[ViewConfig.DC_POSITION].ToString()];  //---触发位置--- 
            triggerData.TriggerKindID = ViewConfig.TRIGGER_KIND_NAME_ID[dr[ViewConfig.DC_KIND].ToString()]; //---级别标识---
            triggerData.CompareID = (byte)ViewConfig.MATH_NAME_ID[dr[ViewConfig.DC_OPERATION].ToString()];  //---触发比较---    
            return triggerData;
        }

        /// <summary>
        /// 获取行,并初始化
        /// </summary>
        /// <param name="td">TriggerData</param>
        /// <returns>DataRow</returns>
        protected DataRow GetInitDataRow(TriggerData td)
        {
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr.EndEdit();
            dr[dcTriggerObj.FieldName] = ViewConfig.TRIGGER_ID_NAME[td.TriggerObjectID];                //---触发对象---
            dr[dcTriggerPosition.FieldName] = ViewConfig.TRIGGER_POSITION_ID_NAME[td.TriggerPositionID];//---触发位置-----
            dr[dcTriggerKind.FieldName] = ViewConfig.TRIGGER_KIND_ID_NAME[td.TriggerKindID];            //---触发级别---
            dr[dcOperate.FieldName] = ViewConfig.MATH_ID_NAME[td.CompareID];                            //---比较运算----

            gvLogic.PostEditor();
            dr.EndEdit();
   
            return dr;
        }

        /// <summary>
        /// 时间离开校验
        /// </summary>
        private void ValidTimeEdit_Leave(object sender, EventArgs e)
        {
            try
            {
                gvLogic.PostEditor();
                DataRow dr = gvLogic.GetDataRow(0);
                dr.EndEdit();
                DateTime dtValid = DateTime.Parse(dr[dcValid.FieldName].ToString());
                int validSeconds = dtValid.Hour * 60 * 60 + dtValid.Minute * 60 + dtValid.Second;           //有效秒数
                if (validSeconds > 64800)
                    CommonTools.MessageShow("触发时间不能大于18小时!", 2, "");    
            }
            catch { CommonTools.MessageShow("时间格式错误!", 2, ""); }
        }

        /// <summary>
        /// 时间离开校验
        /// </summary>
        private void InvalidTimeEdit_Leave(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = gvLogic.GetDataRow(0);
                DateTime dtInvalid = DateTime.Parse(dr[dcInvalid.FieldName].ToString());
                int invalidSeconds = dtInvalid.Hour * 60 * 60 + dtInvalid.Minute * 60 + dtInvalid.Second;           //无效秒数
                if (invalidSeconds > 64800)
                    CommonTools.MessageShow("恢复时间不能大于18小时!", 2, "");
            }
            catch { CommonTools.MessageShow("时间格式错误!", 2, ""); }
        }

        /// <summary>
        /// 移除触发级别
        /// </summary>
        /// <param name="kindName">级别名称</param>
        protected void RemoveKindName(string kindName)
        {
            for (int i = 0; i < cbxKind.Items.Count; i++)
                if (cbxKind.Items[i].ToString() == kindName)
                {
                    cbxKind.Items.RemoveAt(i);
                    break;
                }
        }


        /// <summary>
        /// 添加触发级别
        /// </summary>
        /// <param name="kindName">级别名称</param>
        protected void AddKindName(string kindName)
        {
            bool found = false;
            for (int i = 0; i < cbxKind.Items.Count; i++)
                if (cbxKind.Items[i].ToString() == kindName)
                {
                    found = true;
                    break;
                }


            if (!found)
                cbxKind.Items.Add(kindName);
        }

        /// <summary>
        /// 获取级别对应的序号,默认为0
        /// </summary>
        /// <param name="levels">等级列表</param>
        /// <param name="value">等级值</param>
        /// <returns></returns>
        protected int FindLevelIndex(string[] levels, string value)
        {
            int index = 0;
            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i] == value)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
}