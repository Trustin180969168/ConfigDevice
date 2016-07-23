using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;
using System.Drawing;
using DevExpress.XtraRichTextEdit;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraEditors.Controls;

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
        protected GridViewGridLookupEdit gridLookupDevice;//---查找设备列表---
        protected DataTable dtSelectDevices;//---选择的设备列表---

        protected GridColumn dcTriggerObj;//触发对象
        protected GridColumn dcTriggerPosition;//触发位置
        protected GridColumn dcTriggerKind;//触发类型
        protected GridColumn dcDifferentDevice;//差异设备
        protected GridColumn dcOperate;//触发运算
        protected GridColumn dcStartValue;//初始值
        protected GridColumn dcEndValue;//结束值
        protected GridColumn dcValid;//持续时间
        protected GridColumn dcInvalid;//无效时间

        public abstract TriggerData GetLogicData();         //获取udp指令
        public abstract void SetLogicData(TriggerData td);  //获取逻辑条件 
        public abstract void InitViewSetting();             // 初始化配置界面
        public abstract void KindChanged();                  //类型选择
        public abstract void OperateChanged();               //运算选择
 
        public BaseViewLogicControl(Device _device, GridView gv)
        {
            this.deviceTrigger = _device;
            this.gvLogic = gv;
            InvalidEdit.ReadOnly = true;
            InvalidEdit.NullText = "无效";
            InvalidEdit.AllowFocused = false;
            //-------获取列操作对象--------
            dcTriggerObj = gv.Columns.ColumnByFieldName(ViewConfig.DC_OBJECT);//触发对象
            dcTriggerPosition = gv.Columns.ColumnByFieldName(ViewConfig.DC_POSITION);//触发位置
            dcTriggerKind = gv.Columns.ColumnByFieldName(ViewConfig.DC_KIND);//触发类型
            dcDifferentDevice = gv.Columns.ColumnByFieldName(ViewConfig.DC_DEVICE_VALUE);//差异设备            
            dcOperate = gv.Columns.ColumnByFieldName(ViewConfig.DC_OPERATION);//运算操作
            dcStartValue = gv.Columns.ColumnByFieldName(ViewConfig.DC_START_VALUE);//初始值
            dcEndValue = gv.Columns.ColumnByFieldName(ViewConfig.DC_END_VALUE);//结束值 
            dcValid = gv.Columns.ColumnByFieldName(ViewConfig.DC_VALID_TIME);//有效值
            dcInvalid = gv.Columns.ColumnByFieldName(ViewConfig.DC_INVALID_TIME);//无效值
            //--------配置编辑控件-------            
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_VALUE);//---默认添加触发值---
            dcTriggerKind.ColumnEdit = this.cbxKind;                //---触发类型 
            dcOperate.ColumnEdit = this.cbxOperate;                 //---触发运算符 ,统一为下拉选择----   
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_LOCAL);//---默认位置是本地----
            dcTriggerPosition.ColumnEdit = cbxPosition;             //---触发编辑---- 
            //--------默认生效选择-------
            setGridColumnValid(dcTriggerPosition, cbxPosition);     //---设置触发位置有效---
            setGridColumnInvalid(dcDifferentDevice);                //---默认差异设备列无效--- 
            setGridColumnValid(dcTriggerKind, cbxKind);             //---默认触发类型有效-----
            setGridColumnValid(dcOperate, cbxOperate);              //---默认运算类型有效-----
            setGridColumnInvalid(dcValid);                          //---默认有效持续无效-----
            setGridColumnInvalid(dcInvalid);                        //---默认无效持续无效----
            //--------默认触发事件------
            cbxPosition.SelectedIndexChanged += new System.EventHandler(this.cbxPosition_SelectedIndexChanged);//---位置选择事件
            cbxKind.SelectedIndexChanged += new System.EventHandler(this.cbxKind_SelectedIndexChanged);//---级别选择事件---
            cbxOperate.SelectedIndexChanged += new System.EventHandler(this.cbxOperate_SelectedIndexChanged);//----运算符触发---
            ValidTimeEdit.Leave += this.ValidTimeEdit_Leave;        //---有效触发----
            InvalidTimeEdit.Leave += this.InvalidTimeEdit_Leave;    //---无效触发----
        }

        /// <summary>
        /// 位置选择
        /// </summary> 
        private void cbxPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            positionChanged();
        }

        /// <summary>
        /// 触发类型选择
        /// </summary> 
        private void cbxKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            KindChanged();
        }

        /// <summary>
        /// 运算符触发
        /// </summary> 
        private void cbxOperate_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperateChanged();
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

        /// <summary>
        /// 初始化设备选择, 不是每个设备都需要外设, 为提高效率, 根据需要执行此方法.
        /// </summary>
        protected void initGridLookupDevice()
        {
            gridLookupDevice = ViewEditCtrl.GetDevicesLookupEdit(ViewConfig.SELECT_LOGIC_DEVICE_QUERY_CONDITION);
            dtSelectDevices = gridLookupDevice.DataSource as DataTable;
            gridLookupDevice.EditValueChanged += this.lookUpEdit_EditValueChanged;
        }
        /// <summary>
        /// 选择设备切换
        /// </summary> 
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr.EndEdit();
            //int i = lookupDevice.GetDataSourceRowIndex(ViewConfig.DC_DEVICE_VALUE, dr[ViewConfig.DC_DEVICE_VALUE].ToString());
            int i = gridLookupDevice.GetIndexByKeyValue(dr[ViewConfig.DC_DEVICE_VALUE].ToString());
            DataRow drSelect = dtSelectDevices.Rows[i];
            dr[ViewConfig.DC_DEVICE_ID] = drSelect[DeviceConfig.DC_ID];
            dr[ViewConfig.DC_DEVICE_KIND_ID] = drSelect[DeviceConfig.DC_KIND_ID];
            dr[ViewConfig.DC_DEVICE_NETWORK_ID] = drSelect[DeviceConfig.DC_NETWORK_ID];
            dr.EndEdit();
        }


        /// <summary>
        /// 位置触发
        /// </summary>
        protected virtual void positionChanged()
        {
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            string positionName = dr[ViewConfig.DC_POSITION].ToString();
            dr.EndEdit();
            //------根据触发位置值,选择触发类型编辑-----
            if (positionName == SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT || positionName == SensorConfig.SENSOR_POSITION_PERIPHERAL)
            {      
                if(positionName ==  SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT )
                    RemoveKindName(SensorConfig.SENSOR_VALUE_KIND_LEVEL);//-----外设差值,只有触发值----
                else
                    AddKindName(SensorConfig.SENSOR_VALUE_KIND_LEVEL);//-----外设有等级,若特殊情况,请覆盖positionChanged()----             
                gvLogic.SetRowCellValue(0, dcTriggerKind, cbxKind.Items[0].ToString());//---自动初始化第一个触发类型选择-----
                setGridColumnValid(dcDifferentDevice, gridLookupDevice);//------选择设备有效----
            }
            else
            {
                AddKindName(SensorConfig.SENSOR_VALUE_KIND_LEVEL);
                setGridColumnInvalid(dcDifferentDevice);
            }
            KindChanged();//---执行触发等级切换---
        }

        /// <summary>
        /// 设置设备数据
        /// </summary>
        /// <param name="td">触发数据</param>
        /// <param name="dr">行</param>
        protected void setDeviceData(TriggerData td, DataRow dr)
        {
            string deviceValue = td.DeviceKindID.ToString() +"_" + td.DeviceNetworkID.ToString() +"_" +td.DeviceID.ToString();
            if (deviceValue == "0_0_0")//----没有保存差异设备----
                dr[ViewConfig.DC_DEVICE_VALUE] = null;
            else
            {
                DataRow[] rows = dtSelectDevices.Select(ViewConfig.DC_DEVICE_VALUE + "='" + deviceValue + "'");
                if (rows.Length <= 0)//----选择设备列表没有,则手动加上----
                {
                    DataRow drInsert = dtSelectDevices.Rows.Add();
                    drInsert[DeviceConfig.DC_NAME] = ViewConfig.NAME_INVALID_DEVICE;
                    drInsert[DeviceConfig.DC_KIND_ID] = (int)td.DeviceKindID;
                    drInsert[DeviceConfig.DC_KIND_NAME] = DeviceConfig.EQUIPMENT_ID_NAME[td.DeviceKindID];
                    drInsert[ViewConfig.DC_DEVICE_VALUE] = deviceValue;
                    drInsert[DeviceConfig.DC_NETWORK_ID] = (int)td.DeviceNetworkID;
                    drInsert[DeviceConfig.DC_ID] = (int)td.DeviceID;
                    drInsert.EndEdit();
                    dtSelectDevices.AcceptChanges();
                }
                dr[ViewConfig.DC_DEVICE_VALUE] = deviceValue;
                dr[ViewConfig.DC_DEVICE_ID] = td.DeviceID;
                dr[ViewConfig.DC_DEVICE_NETWORK_ID] = td.DeviceNetworkID;
                dr[ViewConfig.DC_DEVICE_KIND_ID] = td.DeviceKindID;
                dr.EndEdit();
            }
        }

        /// <summary>
        /// 获取设备数据触发数据
        /// </summary>
        /// <param name="dr">行</param>
        /// <param name="triggerData">触发数据</param>
        protected void getDeviceData(DataRow dr, TriggerData triggerData)
        {
            try
            {
                triggerData.DeviceID = (byte)Convert.ToInt16(dr[ViewConfig.DC_DEVICE_ID].ToString());
                triggerData.DeviceKindID = (byte)Convert.ToInt16(dr[ViewConfig.DC_DEVICE_KIND_ID].ToString());
                triggerData.DeviceNetworkID = (byte)Convert.ToInt16(dr[ViewConfig.DC_DEVICE_NETWORK_ID].ToString());
            }
            catch { triggerData.DeviceID = 0; triggerData.DeviceKindID = 0; triggerData.DeviceNetworkID = 0; }
        }

    }
}