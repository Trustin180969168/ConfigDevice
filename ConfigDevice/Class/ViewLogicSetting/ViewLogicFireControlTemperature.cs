﻿using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;
using System.Drawing;
namespace ConfigDevice
{
    /// <summary>
    /// 消防温度
    /// </summary>
    public class ViewLogicFireControlTemperature : BaseViewLogicControl
    {
        GridViewDigitalEdit temperatureEdit = new GridViewDigitalEdit();//--温度编辑控件---
        GridViewComboBox cbxOperateLevel = new GridViewComboBox();//---操作运算--
        GridViewComboBox cbxTemperatureLevelEdit = new GridViewComboBox();//---温度级别编辑控件--- 
        DataTable dtSelectDevices = new DataTable(); //选择设备列表
        public ViewLogicFireControlTemperature(Device _device, GridView gv)
            : base(_device, gv)
        {
            setGridColumnValid(dcTriggerPosition, cbxPosition);                                 //-------设置触发位置有效---
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL);                     //----加上外设(默认只有本地)---  
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT);           //----加上外设差值(默认只有本地)---  
            cbxPosition.SelectedIndexChanged += new System.EventHandler(this.cbxPosition_SelectedIndexChanged);//---位置选择事件
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL);                            //---加上级别(默认只有触发值)---
            cbxKind.SelectedIndexChanged += new System.EventHandler(this.cbxKind_SelectedIndexChanged);//---级别选择事件---
            dcTriggerKind.ColumnEdit = cbxKind;
            initLookupEdit();//---初始化选择设备列表
            //--------触发运算选择------
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_LESS_THAN);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_GREATER_THAN);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_WITHIN);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_WITHOUT);
            cbxOperate.SelectedIndexChanged += new System.EventHandler(this.cbxOperate_SelectedIndexChanged);
            //-------级别运算选择------
            cbxOperateLevel.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);
            //-------初始化温度编辑控件------
            temperatureEdit.DisplayFormat.FormatString = "#0 ℃";
            temperatureEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            temperatureEdit.Mask.EditMask = "#0 ℃";
            temperatureEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            temperatureEdit.Mask.UseMaskAsDisplayFormat = true;
            temperatureEdit.MaxValue = 60;
            temperatureEdit.MinValue = -20;
            //-------初始化级别编辑控件------
            cbxTemperatureLevelEdit.Items.Add(SensorConfig.TEMPFC_NAME_LV_NORMAL);//正常
            cbxTemperatureLevelEdit.Items.Add(SensorConfig.TEMPFC_NAME_LV_HIGH);//高温
            cbxTemperatureLevelEdit.Items.Add(SensorConfig.TEMPFC_NAME_LV_FIRE);//大火

        }

        /// <summary>
        /// 设置初始值
        /// </summary>
        public override void InitViewSetting()
        {
            setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            setGridColumnInvalid(dcDifferentDevice);//---默认差值无效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, temperatureEdit);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效--- 
            setGridColumnValid(dcValid, ValidTimeEdit);//---持续时间----
            setGridColumnValid(dcInvalid, InvalidTimeEdit);//失效时间----- 
            gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---触发位置默认本地----
            gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
            gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---
            gvLogic.SetRowCellValue(0, dcValid, "00:00:00");//----默认为0秒
            gvLogic.SetRowCellValue(0, dcInvalid, "00:00:00");//----默认为0秒
        }

        /// <summary>
        /// 初始化设备列表选择
        /// </summary>
        private void initLookupEdit()
        {
            //-----获取选择的设备数据---
            dtSelectDevices = SysConfig.DtDevice.Clone();
            DataRow[] rows = SysConfig.DtDevice.Select(ViewConfig.SELECT_LOGIC_DEVICE_QUERY_CONDITION);
            foreach (DataRow dr in rows)
                dtSelectDevices.Rows.Add(dr.ItemArray);
            //----初始化新的设备值----
            dtSelectDevices.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));
            foreach (DataRow dr in dtSelectDevices.Rows)
                dr[ViewConfig.DC_DEVICE_VALUE] = dr[DeviceConfig.DC_KIND_ID].ToString() + dr[DeviceConfig.DC_NETWORK_ID].ToString() + dr[DeviceConfig.DC_ID].ToString();

            dtSelectDevices.AcceptChanges();
            lookupDevice.DataSource = dtSelectDevices;

            lookupDevice.EditValueChanged += this.lookUpEdit_EditValueChanged;
        }

        /// <summary>
        /// 选择切换
        /// </summary> 
        private void lookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr.EndEdit();
            int i = lookupDevice.GetDataSourceRowIndex(ViewConfig.DC_DEVICE_VALUE, dr[ViewConfig.DC_DEVICE_VALUE].ToString());
            DataRow drSelect = dtSelectDevices.Rows[i];
            dr[ViewConfig.DC_DEVICE_ID] = drSelect[DeviceConfig.DC_ID];
            dr[ViewConfig.DC_DEVICE_KIND_ID] = drSelect[DeviceConfig.DC_KIND_ID];
            dr[ViewConfig.DC_DEVICE_NETWORK_ID] = drSelect[DeviceConfig.DC_NETWORK_ID];
            dr.EndEdit();
        }

        /// <summary>
        /// 位置选择
        /// </summary> 
        private void cbxPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            string positionName = (string)cbxPosition.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr[ViewConfig.DC_POSITION] = positionName;
            dr.EndEdit();
            //------根据触发位置值,选择触发类型编辑-----
            if (positionName == SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT)
            {
                gvLogic.SetRowCellValue(0, dcTriggerKind, cbxKind.Items[0].ToString());//---第一个运算符-----
                RemoveKindName(SensorConfig.SENSOR_VALUE_KIND_LEVEL);
                setGridColumnValid(dcDifferentDevice, lookupDevice);
            }
            else
            {
                AddKindName(SensorConfig.SENSOR_VALUE_KIND_LEVEL);
                setGridColumnInvalid(dcDifferentDevice);
            }
        }

        /// <summary>
        /// 触发类型选择
        /// </summary> 
        private void cbxKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            kindChanged();
        }
        private void kindChanged()
        {
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr.EndEdit();
            int index = Convert.ToInt16(ViewConfig.TRIGGER_KIND_NAME_ID[ dr[ViewConfig.DC_KIND].ToString()]);
            string kindName = (string)cbxKind.Items[index];
            if (kindName == SensorConfig.SENSOR_VALUE_KIND_VALUE)
            {
                setGridColumnValid(dcOperate, cbxOperate);
                setGridColumnValid(dcStartValue, temperatureEdit);
                setGridColumnValid(dcEndValue, temperatureEdit);

                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---
                gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束值---
            }
            else if (kindName == SensorConfig.SENSOR_VALUE_KIND_LEVEL)
            {
                setGridColumnValid(dcOperate, cbxOperateLevel);
                setGridColumnValid(dcStartValue, cbxTemperatureLevelEdit);
                setGridColumnInvalid(dcEndValue);

                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, cbxTemperatureLevelEdit.Items[0].ToString());//---开始值---         
            }
            operateChanged();
        }

        /// <summary>
        /// 运算符触发
        /// </summary> 
        private void cbxOperate_SelectedIndexChanged(object sender, EventArgs e)
        {
            operateChanged();
        }
        private void operateChanged()
        {
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr.EndEdit();
            string operateName = dr[this.dcOperate.FieldName].ToString();
            if (operateName == SensorConfig.LG_MATH_NAME_EQUAL_TO || operateName == SensorConfig.LG_MATH_NAME_LESS_THAN ||
                operateName == SensorConfig.LG_MATH_NAME_GREATER_THAN)
                setGridColumnInvalid(dcEndValue);//---设置结束值无效----
            else
            {
                setGridColumnValid(dcEndValue, temperatureEdit);//----设置结束值有效----
                gvLogic.SetRowCellValue(0, dcStartValue, 0);    //---开始值---
                gvLogic.SetRowCellValue(0, dcEndValue, 0);      //---结束值---
            }
        }


        /// <summary>
        /// 获取触发数据
        /// </summary>
        /// <returns>触发数据</returns>
        public override TriggerData GetLogicData()
        {
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr.EndEdit();
            TriggerData triggerData = GetInitTriggerData(dr);//----初始化触发数据----
            if (triggerData.TriggerPositionID == SensorConfig.LG_SENSOR_DIF_FLAG)//----外设差值---
            {
                triggerData.DeviceID = (byte)dr[ViewConfig.DC_DEVICE_ID];
                triggerData.DeviceKindID = (byte)dr[ViewConfig.DC_DEVICE_KIND_ID];
                triggerData.DeviceNetworkID = (byte)dr[ViewConfig.DC_DEVICE_NETWORK_ID];
            }
            if (triggerData.TriggerKindID == SensorConfig.LG_SENSOR_LVL_FLAG)//---为级别---
            {
                if (dr[dcStartValue.FieldName].ToString() == SensorConfig.TEMPFC_NAME_LV_NORMAL)
                    triggerData.Size1 = 0;
                else if (dr[dcStartValue.FieldName].ToString() == SensorConfig.TEMPFC_NAME_LV_HIGH)
                    triggerData.Size1 = 1;
                else if (dr[dcStartValue.FieldName].ToString() == SensorConfig.TEMPFC_NAME_LV_FIRE)
                    triggerData.Size1 = 2;
            }
            else
                triggerData.Size1 = Convert.ToInt32(dr[ViewConfig.DC_START_VALUE].ToString());
            if (dcEndValue.OptionsColumn.AllowEdit)//---有效则添加到结束值-----
                triggerData.Size2 = Convert.ToInt32(dr[ViewConfig.DC_END_VALUE].ToString());
            //-----有效持续,无效持续------            
            int validSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcValid.FieldName].ToString());        //有效秒数
            int invalidSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcInvalid.FieldName].ToString());    //无效秒数       
            triggerData.ValidSeconds = (UInt16)validSeconds;
            triggerData.InvalidSeconds = (UInt16)invalidSeconds;
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcValid.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(triggerData.ValidSeconds).ToLongTimeString();//----异常同样显示到界面---
            dr[dcInvalid.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(triggerData.InvalidSeconds).ToLongTimeString();//----异常同样显示到界面---

            dr.EndEdit();
            gvLogic.RefreshData();

            return triggerData;
        }

        /// <summary>
        /// 设置数据到列表
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行---
            if (dr[dcTriggerPosition.FieldName].ToString() == SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT)//--外设差值的情况---
            {
                string deviceValue = td.DeviceKindID.ToString() + td.DeviceNetworkID.ToString() + td.DeviceID.ToString();
                DataRow[] rows = dtSelectDevices.Select(ViewConfig.DC_DEVICE_VALUE + "='" + deviceValue + "'");
                if (rows.Length <= 0)//----选择设备没有,则手动加上----
                {
                    DataRow drInsert = dtSelectDevices.Rows.Add();
                    drInsert[DeviceConfig.DC_NAME] = "无效名称";
                    drInsert[DeviceConfig.DC_KIND_ID] = (int)td.DeviceKindID;
                    drInsert[DeviceConfig.DC_KIND_NAME] = DeviceConfig.EQUIPMENT_ID_NAME[td.DeviceKindID];
                    drInsert[ViewConfig.DC_DEVICE_VALUE] = deviceValue;
                    drInsert[DeviceConfig.DC_NETWORK_ID] = (int)td.DeviceNetworkID;
                    drInsert[DeviceConfig.DC_ID] = (int)td.DeviceID;
                    drInsert.EndEdit();
                    dtSelectDevices.AcceptChanges();
                }
                dr[ViewConfig.DC_DEVICE_VALUE] = deviceValue;//---触发设备列表选择---
            }
            if (td.TriggerKindID == SensorConfig.LG_SENSOR_LVL_FLAG)//---为级别类型---
            {
                dcStartValue.ColumnEdit = cbxTemperatureLevelEdit;
                if (td.Size1 == 0)
                    dr[dcStartValue.FieldName] = SensorConfig.TEMPFC_NAME_LV_NORMAL;
                else if (td.Size1 == 1)
                    dr[dcStartValue.FieldName] = SensorConfig.TEMPFC_NAME_LV_HIGH;
                else if (td.Size1 == 2)
                    dr[dcStartValue.FieldName] = SensorConfig.TEMPFC_NAME_LV_FIRE;
            }
            else
                dr[dcStartValue.FieldName] = td.Size1;//---为触发值类型
            kindChanged();//---执行级别触发----

            if (dcEndValue.OptionsColumn.AllowEdit)
                dr[dcEndValue.FieldName] = td.Size2;

            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcValid.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.ValidSeconds).ToLongTimeString();  //----有效持续---
            dr[dcInvalid.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.InvalidSeconds).ToLongTimeString();//----无效持续---
            dr.EndEdit();
            dr.AcceptChanges();
        }

    }
}