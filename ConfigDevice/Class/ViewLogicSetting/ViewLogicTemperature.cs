using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;
using System.Drawing;
namespace ConfigDevice
{
    /// <summary>
    /// 温度
    /// </summary>
    public class ViewLogicTemperature : BaseViewLogicControl
    {
        GridViewDigitalEdit temperatureEdit = new GridViewDigitalEdit();//--温度编辑控件---
        GridViewComboBox cbxOperateLevel = new GridViewComboBox();//---操作运算--
        GridViewComboBox cbxTemperatureLevelEdit = new GridViewComboBox();//---温度级别编辑控件--- 
        GridViewGridLookupEdit gridLookupDevice;//---查找设备列表---
        DataTable dtSelectDevices;//---选择的设备列表---
        public ViewLogicTemperature(Device _device, GridView gv)
            : base(_device, gv)
        {
            setGridColumnValid(dcTriggerPosition, cbxPosition);                                 //-------设置触发位置有效---
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL);                     //----加上外设(默认只有本地)---  
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT);           //----加上外设差值(默认只有本地)---  
            cbxPosition.SelectedIndexChanged += new System.EventHandler(this.cbxPosition_SelectedIndexChanged);//---位置选择事件
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL);                            //---加上级别(默认只有触发值)---
            cbxKind.SelectedIndexChanged += new System.EventHandler(this.cbxKind_SelectedIndexChanged);//---级别选择事件---
            dcTriggerKind.ColumnEdit = cbxKind;
            //-------初始化设备列表选择-----
            gridLookupDevice = ViewEditCtrl.GetLogicDevicesLookupEdit();
            gridLookupDevice.EditValueChanged += this.lookUpEdit_EditValueChanged;
            dtSelectDevices = gridLookupDevice.DataSource as DataTable;
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
            foreach (string value in  TemperatureSensor.LEVEL_ID_NAME.Values)
                cbxTemperatureLevelEdit.Items.Add(value);

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
        /// 选择切换
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
        /// 位置选择
        /// </summary> 
        private void cbxPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            positionChanged();
        }

        private void positionChanged()
        {
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            string positionName = dr[ViewConfig.DC_POSITION].ToString();
            dr.EndEdit();
            //------根据触发位置值,选择触发类型编辑-----
            if (positionName == SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT || positionName == SensorConfig.SENSOR_POSITION_PERIPHERAL)
            {
                gvLogic.SetRowCellValue(0, dcTriggerKind, cbxKind.Items[0].ToString());//---第一个运算符-----
                RemoveKindName(SensorConfig.SENSOR_VALUE_KIND_LEVEL);//-----外设差值,只有触发值----
                setGridColumnValid(dcDifferentDevice, gridLookupDevice);//------选择设备有效----
            }
            else
            {
                AddKindName(SensorConfig.SENSOR_VALUE_KIND_LEVEL);
                setGridColumnInvalid(dcDifferentDevice);
            }
            kindChanged();//---执行触发等级切换---
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
            string kindName = dr[ViewConfig.DC_KIND].ToString();
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
                string kindName = dr[ViewConfig.DC_KIND].ToString();
                if (kindName == SensorConfig.SENSOR_VALUE_KIND_LEVEL)
                {
                    setGridColumnValid(dcEndValue, cbxTemperatureLevelEdit);
                    gvLogic.SetRowCellValue(0, dcEndValue, cbxTemperatureLevelEdit.Items[0].ToString());//---开始值---     
                }
                else
                {
                    setGridColumnValid(dcEndValue, temperatureEdit);//----设置结束值有效----
                    gvLogic.SetRowCellValue(0, dcEndValue, 0);      //---结束值---
                }

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
            if (triggerData.TriggerPositionID == SensorConfig.LG_SENSOR_DEV_FLAG)//----外设/差值---
            {
                try
                {
                    triggerData.DeviceID = (byte)Convert.ToInt16(dr[ViewConfig.DC_DEVICE_ID].ToString());
                    triggerData.DeviceKindID = (byte)Convert.ToInt16(dr[ViewConfig.DC_DEVICE_KIND_ID].ToString());
                    triggerData.DeviceNetworkID = (byte)Convert.ToInt16(dr[ViewConfig.DC_DEVICE_NETWORK_ID].ToString());
                }
                catch { triggerData.DeviceID = 0; triggerData.DeviceKindID = 0; triggerData.DeviceNetworkID = 0; }
            }
            if (triggerData.TriggerKindID == SensorConfig.LG_SENSOR_LVL_FLAG)//---为级别--- 
            {
                triggerData.Size1 = TemperatureSensor.LEVEL_NAME_ID[dr[dcStartValue.FieldName].ToString()];
                if (dcEndValue.OptionsColumn.AllowEdit)
                    triggerData.Size2 = TemperatureSensor.LEVEL_NAME_ID[dr[dcEndValue.FieldName].ToString()];
            }
            else
            {
                triggerData.Size1 = Convert.ToInt32(dr[dcStartValue.FieldName].ToString());
                if (dcEndValue.OptionsColumn.AllowEdit)//---有效则添加到结束值-----
                    triggerData.Size2 = Convert.ToInt32(dr[ViewConfig.DC_END_VALUE].ToString());
            }

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
            dr.AcceptChanges();//----再次修改才保存-----

            return triggerData;
        }

        /// <summary>
        /// 设置数据到列表
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行---      
            if (td.TriggerPositionID == SensorConfig.LG_SENSOR_DEV_FLAG)//--外设差值的情况---
            {
                positionChanged();//---触发选择设备----
                string deviceValue = td.DeviceKindID.ToString() + td.DeviceNetworkID.ToString() + td.DeviceID.ToString();
                if (deviceValue == "000")//----没有保存差异设备----
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
            kindChanged();//---执行级别触发---
            dr[dcOperate.FieldName] = ViewConfig.MATH_ID_NAME[td.CompareID];  //---级别触发后,初始化了运算符,所以重新赋值----
            operateChanged();//---执行运算触发---
            if (td.TriggerKindID == SensorConfig.LG_SENSOR_LVL_FLAG)//---为级别类型---
            {
                dcStartValue.ColumnEdit = cbxTemperatureLevelEdit;
                dr[dcStartValue.FieldName] = TemperatureSensor.LEVEL_ID_NAME[td.Size1];
                if (dcEndValue.OptionsColumn.AllowEdit)
                    dr[dcEndValue.FieldName] = TemperatureSensor.LEVEL_ID_NAME[td.Size2];
            }
            else
            {
                dr[dcStartValue.FieldName] = td.Size1;//---为触发值类型
                if (dcEndValue.OptionsColumn.AllowEdit)
                    dr[dcEndValue.FieldName] = td.Size2;
            }
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcValid.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.ValidSeconds).ToLongTimeString();  //----有效持续---
            dr[dcInvalid.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.InvalidSeconds).ToLongTimeString();//----无效持续---
            dr.EndEdit();
            dr.AcceptChanges();
        }


        public override void KindChanged()
        {
            throw new NotImplementedException();
        }

        public override void OperateChanged()
        {
            throw new NotImplementedException();
        }
    }
}
