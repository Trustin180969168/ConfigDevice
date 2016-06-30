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
    /// 湿度
    /// </summary>
    public class ViewLogicHumidity : BaseViewLogicControl
    {
        GridViewPercentEdit sensorValueEdit = new GridViewPercentEdit();//--传感器数值编辑控件---
        GridViewComboBox cbxLevelEdit = new GridViewComboBox();//---级别编辑控件--- 

        public ViewLogicHumidity(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL);                     //---加上外设(默认只有本地)---  
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL_DIFFERENT);           //---加上外设差值(默认只有本地)---     
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL);                            //---加上级别(默认只有触发值)---   
            initGridLookupDevice();    //-------初始化设备列表选择-----
            //--------触发运算选择------
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_LESS_THAN);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_GREATER_THAN);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_WITHIN);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_WITHOUT);

            sensorValueEdit.MaxValue = 99;
            //-------初始化级别编辑控件------
            foreach (string value in HumiditySensor.LEVEL_ID_NAME.Values)
                cbxLevelEdit.Items.Add(value);

        }


        /// <summary>
        /// 设置初始值
        /// </summary>
        public override void InitViewSetting()
        {
            setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            setGridColumnInvalid(dcDifferentDevice);//---默认差值无效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, sensorValueEdit);//---开始值有效
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
        /// 类型触发
        /// </summary>
        public override void KindChanged()
        {
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr.EndEdit();
            string kindName = dr[ViewConfig.DC_KIND].ToString();
            if (kindName == SensorConfig.SENSOR_VALUE_KIND_VALUE)
            {
                setGridColumnValid(dcStartValue, sensorValueEdit);
                //----自动初始化------
                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---
                setGridColumnInvalid(dcEndValue);

            }
            else if (kindName == SensorConfig.SENSOR_VALUE_KIND_LEVEL)
            {
                setGridColumnValid(dcStartValue, cbxLevelEdit);
                //----自动初始化------
                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, cbxLevelEdit.Items[0].ToString());//---开始值---
                setGridColumnInvalid(dcEndValue);

            }
            dr.EndEdit();
            OperateChanged();
        }


        /// <summary>
        /// 运算触发
        /// </summary>
        public override void OperateChanged()
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
                    if (dcEndValue.ColumnEdit != cbxLevelEdit)
                    {
                        setGridColumnValid(dcEndValue, cbxLevelEdit);
                        gvLogic.SetRowCellValue(0, dcEndValue, cbxLevelEdit.Items[0].ToString());//--开始值---     
                    }
                }
                else
                {
                    if (dcEndValue.ColumnEdit != sensorValueEdit)
                    {
                        setGridColumnValid(dcEndValue, sensorValueEdit);//----设置结束值有效----
                        gvLogic.SetRowCellValue(0, dcEndValue, 0);      //-------自动初始化------结束值---
                    }
                }

            }
            dr.EndEdit();
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
            if (triggerData.TriggerPositionID == SensorConfig.LG_SENSOR_DEV_FLAG || triggerData.TriggerPositionID == SensorConfig.LG_SENSOR_DIF_FLAG_VALUE)//----外设/外设差值---     
                getDeviceData(dr, triggerData);//----获取设备数据---           
            if (triggerData.TriggerKindID == SensorConfig.LG_SENSOR_LVL_FLAG)//---为级别--- 
            {
                triggerData.Size1 = HumiditySensor.LEVEL_NAME_ID[dr[dcStartValue.FieldName].ToString()];
                if (dcEndValue.OptionsColumn.AllowEdit)
                    triggerData.Size2 = HumiditySensor.LEVEL_NAME_ID[dr[dcEndValue.FieldName].ToString()];
            }
            else
            {
                try
                {
                    triggerData.Size1 = Convert.ToInt32(dr[dcStartValue.FieldName].ToString());
                }
                catch { triggerData.Size1 = 0; }
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
            positionChanged();//---触发选择设备----
            dr[dcTriggerKind.FieldName] = ViewConfig.TRIGGER_KIND_ID_NAME[td.TriggerKindID];//---加载会自动触发初始化,需要重新赋值,并重新执行切换----
            KindChanged();
            dr[dcOperate.FieldName] = ViewConfig.MATH_ID_NAME[td.CompareID];//---加载会自动触发初始化,需要重新赋值,并重新执行切换----
            OperateChanged();
            if (td.TriggerPositionID == SensorConfig.LG_SENSOR_DEV_FLAG || td.TriggerPositionID == SensorConfig.LG_SENSOR_DIF_FLAG_VALUE)//--外设/外设差值的情况---               
                setDeviceData(td, dr);//----设置设备数据----    
            if (td.TriggerKindID == SensorConfig.LG_SENSOR_LVL_FLAG)//---为级别类型---
            {
                dcStartValue.ColumnEdit = cbxLevelEdit;
                dr[dcStartValue.FieldName] = HumiditySensor.LEVEL_ID_NAME[td.Size1];
                if (dcEndValue.OptionsColumn.AllowEdit)
                    dr[dcEndValue.FieldName] = HumiditySensor.LEVEL_ID_NAME[td.Size2];
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




    }
}
