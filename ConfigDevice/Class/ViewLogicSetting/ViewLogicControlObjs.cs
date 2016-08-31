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
    /// 可燃气体探头
    /// </summary>
    public class ViewLogicFlamableGasProbe : BaseViewLogicControl
    { 
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值---        
        public ViewLogicFlamableGasProbe(Device _device, GridView gv)
            : base(_device, gv)
        {

            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//---运算--           
            cbxStart.Items.Add(FlamableGasProbeSensor.LEVEL_ID_NAME[0]);
            cbxStart.Items.Add(FlamableGasProbeSensor.LEVEL_ID_NAME[1]);
            dcStartValue.ColumnEdit = cbxStart;

            cbxKind.Items.Clear();//----清空触发类型(探头只有等级)---
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL);//--触发类型(等级)---- 
            //-------初始化设备列表选择-----
            initGridLookupDevice();

            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL);//---添加外设--- 
        }
           

        public override void InitViewSetting()
        {
            setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcTriggerPosition, cbxPosition);//---触发位置有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效---
            setGridColumnValid(dcContinueTime, this.ContinueTimeEdit);//---持续时间----
            setGridColumnValid(dcRecoverTime, this.RecoverTimeEdit);//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, SensorConfig.LG_MATH_NAME_EQUAL_TO);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxStart.Items[0].ToString());//--默认第一个开始值---
            gvLogic.SetRowCellValue(0, dcContinueTime, "00:00:00");//----默认为0秒
            gvLogic.SetRowCellValue(0, dcRecoverTime, "00:00:00");//----默认为0秒
        }


        /// <summary>
        /// 获取逻辑配置数据
        /// </summary>
        /// <returns>TriggerData</returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
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
            //--------泄漏/正常--------------
            string size1Str = dr[dcStartValue.FieldName].ToString(); 
            triggerData.Size1 =  FlamableGasProbeSensor.LEVEL_NAME_ID[size1Str];//----获取等级值---
            triggerData.Size2 = triggerData.Size1;//----强制与开始值相同------
            //-----有效持续,无效持续------            
            int validSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcContinueTime.FieldName].ToString());        //有效秒数
            int invalidSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcRecoverTime.FieldName].ToString());    //无效秒数       
            triggerData.ValidSeconds = (UInt16)validSeconds;
            triggerData.InvalidSeconds = (UInt16)invalidSeconds;
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcContinueTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(triggerData.ValidSeconds).ToLongTimeString();//----异常同样显示到界面---
            dr[dcRecoverTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(triggerData.InvalidSeconds).ToLongTimeString();//----异常同样显示到界面---

            dr.EndEdit();
            dr.AcceptChanges();//----再次修改才保存-----
            gvLogic.RefreshData();          

            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行--- 
            if (td.TriggerPositionID == SensorConfig.LG_SENSOR_DEV_FLAG)//--外设/差值的情况---
            {
                positionChanged();//---触发选择设备----
                string deviceValue = td.DeviceKindID.ToString() + "_" + td.DeviceNetworkID.ToString() + "_" +td.DeviceID.ToString();
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
            dr[dcStartValue.FieldName] = FlamableGasProbeSensor.LEVEL_ID_NAME[td.Size1];
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcContinueTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.ValidSeconds).ToLongTimeString();  //----有效持续---
            dr[dcRecoverTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.InvalidSeconds).ToLongTimeString();//----无效持续---
            dr.EndEdit();
            dr.AcceptChanges();
        }

        public override void KindChanged()
        { 
        }

        public override void OperateChanged()
        { 
        }
    }


    /// <summary>
    /// 短路输入
    /// </summary>
    public class ViewLogicShortInput : BaseViewLogicControl
    {
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值---        
        
        public ViewLogicShortInput(Device _device, GridView gv)
            : base(_device, gv)
        {

            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//---运算--           
            cbxStart.Items.Add("1 " + Short4Sensor.LEVEL_ID_NAME[0]);
            cbxStart.Items.Add("2 " + Short4Sensor.LEVEL_ID_NAME[1]);
            dcStartValue.ColumnEdit = cbxStart;

            cbxKind.Items.Clear();//----清空触发类型(只有等级)---
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL);//--触发类型(等级)---- 
            //-------初始化设备列表选择-----
            initGridLookupDevice();

            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL);//---添加外设--- 
        }


        public override void InitViewSetting()
        {
            setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcTriggerPosition, cbxPosition);//---触发位置有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效---
            setGridColumnValid(dcContinueTime, this.ContinueTimeEdit);//---持续时间----
            setGridColumnValid(dcRecoverTime, this.RecoverTimeEdit);//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, SensorConfig.LG_MATH_NAME_EQUAL_TO);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxStart.Items[0].ToString());//--默认第一个开始值---
            gvLogic.SetRowCellValue(0, dcContinueTime, "00:00:00");//----默认为0秒
            gvLogic.SetRowCellValue(0, dcRecoverTime, "00:00:00");//----默认为0秒
        }


        /// <summary>
        /// 获取逻辑配置数据
        /// </summary>
        /// <returns>TriggerData</returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
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
            //--------泄漏/正常--------------
            string size1Str = dr[dcStartValue.FieldName].ToString();
            triggerData.Size1 = size1Str == "1 " + Short4Sensor.LEVEL_ID_NAME[0] ? 0 : 1;//----获取等级值---
            triggerData.Size2 = triggerData.Size1;//----强制与开始值相同------
            //-----有效持续,无效持续------            
            int validSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcContinueTime.FieldName].ToString());        //有效秒数
            int invalidSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcRecoverTime.FieldName].ToString());    //无效秒数       
            triggerData.ValidSeconds = (UInt16)validSeconds;
            triggerData.InvalidSeconds = (UInt16)invalidSeconds;
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcContinueTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(triggerData.ValidSeconds).ToLongTimeString();//----异常同样显示到界面---
            dr[dcRecoverTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(triggerData.InvalidSeconds).ToLongTimeString();//----异常同样显示到界面---

            dr.EndEdit();
            dr.AcceptChanges();//----再次修改才保存-----
            gvLogic.RefreshData();

            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行--- 
            if (td.TriggerPositionID == SensorConfig.LG_SENSOR_DEV_FLAG)//--外设/差值的情况---
            {
                positionChanged();//---触发选择设备----
                string deviceValue = td.DeviceKindID.ToString() + "_" + td.DeviceNetworkID.ToString() + "_" + td.DeviceID.ToString();
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
            dr[dcStartValue.FieldName] = cbxStart.Items[td.Size1];
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcContinueTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.ValidSeconds).ToLongTimeString();  //----有效持续---
            dr[dcRecoverTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.InvalidSeconds).ToLongTimeString();//----无效持续---
            dr.EndEdit();
            dr.AcceptChanges();
        }

        public override void KindChanged()
        {
        }

        public override void OperateChanged()
        {
        }
    }


    /// <summary>
    /// 空气质量
    /// </summary>
    public class ViewLogicAQI : BaseViewLogicControl
    {
        private GridViewComboBox cbxLevelEdit = new GridViewComboBox();//----开始值---        
        public ViewLogicAQI(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL);                     //---加上外设(默认只有本地)---
            cbxKind.Items.Clear();
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL);                            //---加上级别(默认只有触发值)---   
            initGridLookupDevice();    //-------初始化设备列表选择-----
            //--------触发运算选择------
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_LESS_THAN);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_GREATER_THAN);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_WITHIN);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_WITHOUT);

            //-------初始化级别编辑控件------
            foreach (string value in AQISensor.LEVEL_ID_NAME.Values)
                cbxLevelEdit.Items.Add(value);
        }


        public override void InitViewSetting()
        {
            setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            setGridColumnInvalid(dcDifferentDevice);//---默认差值无效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxLevelEdit);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效--- 
            setGridColumnValid(dcContinueTime, ContinueTimeEdit);//---持续时间----
            setGridColumnValid(dcRecoverTime, RecoverTimeEdit);//失效时间----- 

            gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, SensorConfig.LG_MATH_NAME_EQUAL_TO);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxLevelEdit.Items[0].ToString());//--默认第一个开始值---
            gvLogic.SetRowCellValue(0, dcContinueTime, "00:00:00");//----默认为0秒
            gvLogic.SetRowCellValue(0, dcRecoverTime, "00:00:00");//----默认为0秒
        }


        /// <summary>
        /// 获取逻辑配置数据
        /// </summary>
        /// <returns>TriggerData</returns>
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
                triggerData.Size1 = AQISensor.LEVEL_NAME_ID[dr[dcStartValue.FieldName].ToString()];
                if (dcEndValue.OptionsColumn.AllowEdit)
                    triggerData.Size2 = AQISensor.LEVEL_NAME_ID[dr[dcEndValue.FieldName].ToString()];
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
            int validSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcContinueTime.FieldName].ToString());        //有效秒数
            int invalidSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcRecoverTime.FieldName].ToString());    //无效秒数       
            triggerData.ValidSeconds = (UInt16)validSeconds;
            triggerData.InvalidSeconds = (UInt16)invalidSeconds;
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcContinueTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(triggerData.ValidSeconds).ToLongTimeString();//----异常同样显示到界面---
            dr[dcRecoverTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(triggerData.InvalidSeconds).ToLongTimeString();//----异常同样显示到界面---

            dr.EndEdit();
            gvLogic.RefreshData();
            dr.AcceptChanges();//----再次修改才保存-----

            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
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
                dr[dcStartValue.FieldName] = AQISensor.LEVEL_ID_NAME[td.Size1];
                if (dcEndValue.OptionsColumn.AllowEdit)
                    dr[dcEndValue.FieldName] = AQISensor.LEVEL_ID_NAME[td.Size2];
            }
            else
            {
                dr[dcStartValue.FieldName] = td.Size1;//---为触发值类型
                if (dcEndValue.OptionsColumn.AllowEdit)
                    dr[dcEndValue.FieldName] = td.Size2;
            }
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcContinueTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.ValidSeconds).ToLongTimeString();  //----有效持续---
            dr[dcRecoverTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.InvalidSeconds).ToLongTimeString();//----无效持续---
            dr.EndEdit();
            dr.AcceptChanges();
        }


        public override void KindChanged()
        {
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

                if (dcEndValue.ColumnEdit != cbxLevelEdit)
                {
                    setGridColumnValid(dcEndValue, cbxLevelEdit);
                    gvLogic.SetRowCellValue(0, dcEndValue, cbxLevelEdit.Items[0].ToString());//--开始值---     
                }

            }
            dr.EndEdit();
        }
    }



    /// <summary>
    /// 雷达探头
    /// </summary>
    public class ViewLogicRadar : BaseViewLogicControl
    {
        private GridViewComboBox cbxLevelEdit = new GridViewComboBox();//----开始值---        
        public ViewLogicRadar(Device _device, GridView gv)
            : base(_device, gv)
        {

            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//---运算--    
            dcStartValue.ColumnEdit = cbxLevelEdit;

            cbxKind.Items.Clear();//----清空触发类型(探头只有等级)---
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL);//--触发类型(等级)---- 
            //-------初始化设备列表选择-----
            initGridLookupDevice();
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL);//---添加外设--- 
            //-------初始化级别编辑控件------
            foreach (string value in RadarSensor.LEVEL_ID_NAME.Values)
                cbxLevelEdit.Items.Add(value);
        }


        public override void InitViewSetting()
        {
            setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcTriggerPosition, cbxPosition);//---触发位置有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxLevelEdit);//---触发级别----
            setGridColumnInvalid(dcEndValue);//----结束值无效---
            setGridColumnInvalid(dcContinueTime);//---触发时间----
            setGridColumnValid(dcRecoverTime,RecoverTimeEdit);//----恢复时间-----  

            gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, SensorConfig.LG_MATH_NAME_EQUAL_TO);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxLevelEdit.Items[0].ToString());//--默认第一个开始值---      
            gvLogic.SetRowCellValue(0, dcRecoverTime, "00:00:00");//----默认为0秒

        }


        /// <summary>
        /// 获取逻辑配置数据
        /// </summary>
        /// <returns>TriggerData</returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
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
            //--------级别--------------
            string size1Str = dr[dcStartValue.FieldName].ToString();
            triggerData.Size1 = RadarSensor.LEVEL_NAME_ID[size1Str];//----获取等级值---
            triggerData.Size2 = 0;//----无效------
            //-----有效持续,无效持续------             
            int invalidSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcRecoverTime.FieldName].ToString());    //恢复时间      
            triggerData.InvalidSeconds = (UInt16)invalidSeconds; //恢复时间      
            string nowDateStr = DateTime.Now.ToShortDateString(); 
            dr[dcRecoverTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(triggerData.InvalidSeconds).ToLongTimeString();//----异常同样显示到界面---
            triggerData.ValidSeconds = 0;//----触发时间无效,设置为0

            dr.EndEdit();
            dr.AcceptChanges();//----再次修改才保存-----
            gvLogic.RefreshData();

            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行--- 

            if (td.TriggerPositionID == SensorConfig.LG_SENSOR_DEV_FLAG)//--外设/差值的情况---
            {
                positionChanged();//---触发选择设备----
                string deviceValue = td.DeviceKindID.ToString() + td.DeviceNetworkID.ToString() + td.DeviceID.ToString();
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
            try
            {
                dr[dcStartValue.FieldName] = RadarSensor.LEVEL_ID_NAME[td.Size1];
            }
            catch { dr[dcStartValue.FieldName] = RadarSensor.LEVEL_ID_NAME[0]; }

            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcRecoverTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.InvalidSeconds).ToLongTimeString();//----无效持续---
            dr.EndEdit();
            dr.AcceptChanges();
        }

        public override void KindChanged()
        {
        }

        public override void OperateChanged()
        {
        }
    }



    /// <summary>
    /// 防拆开关
    /// </summary>
    public class ViewLogicSwitTamper : BaseViewLogicControl
    {
       private GridViewComboBox cbxLevelEdit = new GridViewComboBox();//----开始值---        
       public ViewLogicSwitTamper(Device _device, GridView gv)
            : base(_device, gv)
        {

            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//---运算--    
            dcStartValue.ColumnEdit = cbxLevelEdit;

            cbxKind.Items.Clear();//----清空触发类型(探头只有等级)---
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL);//--触发类型(等级)---- 
            //-------初始化设备列表选择-----
            initGridLookupDevice();
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL);//---添加外设--- 
            //-------初始化级别编辑控件------
            foreach (string value in SwitTamperSensor.LEVEL_ID_NAME.Values)
                cbxLevelEdit.Items.Add(value);
        }


        public override void InitViewSetting()
        {
            setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcTriggerPosition, cbxPosition);//---触发位置有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxLevelEdit);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效---
            setGridColumnInvalid(dcContinueTime);//---持续时间----
            setGridColumnInvalid(dcRecoverTime);//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, SensorConfig.LG_MATH_NAME_EQUAL_TO);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxLevelEdit.Items[0].ToString());//--默认第一个开始值---

        }


        /// <summary>
        /// 获取逻辑配置数据
        /// </summary>
        /// <returns>TriggerData</returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
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
            //--------级别--------------
            string size1Str = dr[dcStartValue.FieldName].ToString();
            triggerData.Size1 = SwitTamperSensor.LEVEL_NAME_ID[size1Str];//----获取等级值---
            triggerData.Size2 = 0;//----无效------
            triggerData.ValidSeconds = 0;//--无效强制为0
            triggerData.InvalidSeconds = 0;//---无效强制为0
            dr.EndEdit();
            dr.AcceptChanges();//----再次修改才保存-----
            gvLogic.RefreshData();

            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行--- 
            
            if (td.TriggerPositionID == SensorConfig.LG_SENSOR_DEV_FLAG)//--外设/差值的情况---
            {
                positionChanged();//---触发选择设备----
                string deviceValue = td.DeviceKindID.ToString() +"_"+ td.DeviceNetworkID.ToString() + "_"+td.DeviceID.ToString();
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
            try
            {
                dr[dcStartValue.FieldName] = SwitTamperSensor.LEVEL_ID_NAME[td.Size1];
            }
            catch { dr[dcStartValue.FieldName] = SwitTamperSensor.LEVEL_ID_NAME[0]; }

            dr.EndEdit();
            dr.AcceptChanges();
        }

        public override void KindChanged()
        {
        }

        public override void OperateChanged()
        {
        }
    }


    /// <summary>
    /// 雨感
    /// </summary>
    public class ViewLogicRain : BaseViewLogicControl
    {
        private GridViewComboBox cbxLevelEdit = new GridViewComboBox();//----开始值---        
        public ViewLogicRain(Device _device, GridView gv)
            : base(_device, gv)
        {

            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//---运算--    
            dcStartValue.ColumnEdit = cbxLevelEdit;

            cbxKind.Items.Clear();//----清空触发类型(探头只有等级)---
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL);//--触发类型(等级)---- 
            //-------初始化设备列表选择-----
            initGridLookupDevice();
            cbxPosition.Items.Add(SensorConfig.SENSOR_POSITION_PERIPHERAL);//---添加外设--- 
            //-------初始化级别编辑控件------
            foreach (string value in RainSensor.LEVEL_ID_NAME.Values)
                cbxLevelEdit.Items.Add(value);
        }


        public override void InitViewSetting()
        {
            setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcTriggerPosition, cbxPosition);//---触发位置有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxLevelEdit);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效---
            setGridColumnValid(dcContinueTime, this.ContinueTimeEdit);//---持续时间----
            setGridColumnValid(dcRecoverTime, this.RecoverTimeEdit);//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, SensorConfig.LG_MATH_NAME_EQUAL_TO);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxLevelEdit.Items[0].ToString());//--默认第一个开始值---
            gvLogic.SetRowCellValue(0, dcContinueTime, "00:00:00");//----默认为0秒
            gvLogic.SetRowCellValue(0, dcRecoverTime, "00:00:00");//----默认为0秒
        }


        /// <summary>
        /// 获取逻辑配置数据
        /// </summary>
        /// <returns>TriggerData</returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
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
            //--------级别--------------
            string size1Str = dr[dcStartValue.FieldName].ToString();
            triggerData.Size1 = RainSensor.LEVEL_NAME_ID[size1Str];//----获取等级值---
            triggerData.Size2 = 0;//----无效------
            //-----有效持续,无效持续------            
            int validSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcContinueTime.FieldName].ToString());        //有效秒数
            int invalidSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcRecoverTime.FieldName].ToString());    //无效秒数       
            triggerData.ValidSeconds = (UInt16)validSeconds;
            triggerData.InvalidSeconds = (UInt16)invalidSeconds;
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcContinueTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(triggerData.ValidSeconds).ToLongTimeString();//----异常同样显示到界面---
            dr[dcRecoverTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(triggerData.InvalidSeconds).ToLongTimeString();//----异常同样显示到界面---

            dr.EndEdit();
            dr.AcceptChanges();//----再次修改才保存-----
            gvLogic.RefreshData();

            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行--- 
            
            if (td.TriggerPositionID == SensorConfig.LG_SENSOR_DEV_FLAG)//--外设/差值的情况---
            {
                positionChanged();//---触发选择设备----
                string deviceValue = td.DeviceKindID.ToString() + "_" + td.DeviceNetworkID.ToString() + "_" + td.DeviceID.ToString();
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
            try
            {
                dr[dcStartValue.FieldName] = RainSensor.LEVEL_ID_NAME[td.Size1];
            }
            catch {  dr[dcStartValue.FieldName] = RainSensor.LEVEL_ID_NAME[0];}
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcContinueTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.ValidSeconds).ToLongTimeString();  //----有效持续---
            dr[dcRecoverTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.InvalidSeconds).ToLongTimeString();//----无效持续---
            dr.EndEdit();
            dr.AcceptChanges();
        }

        public override void KindChanged()
        {
        }

        public override void OperateChanged()
        {
        }
    }




    /// <summary>
    /// 安防联动
    /// </summary>
    public class ViewLogicSecurityInteraction : BaseViewLogicControl
    {

 
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值选择---
        private GridViewDigitalEdit edtNum = new GridViewDigitalEdit();//----数字------
        private int circuitCount = 0;//回路数
        public ViewLogicSecurityInteraction(Device _device, GridView gv)
            : base(_device, gv)
        {
            setGridColumnValid(dcTriggerPosition, cbxPosition);//-------设置触发位置有效---
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//----触发 运算符---
            //---开始为下拉----
            foreach (string value in SecuritySensor.LEVEL_ID_NAME.Values)
                cbxStart.Items.Add(value);
            cbxStart.SelectedIndexChanged += CbxSecurityKindChanged;
            cbxStart.DropDownRows = SecuritySensor.LEVEL_ID_NAME.Count;
            circuitCount = (deviceTrigger.ContrlObjs[DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME] as Circuit).CircuitCount;//----获取回路数----

        }



        public override void InitViewSetting()
        {
            //setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            //setGridColumnValid(dcTriggerPosition, cbxPosition);//---触发位置有效----

            setGridColumnInvalid(dcTriggerPosition);//---触发位置无效
            setGridColumnInvalid(dcDifferentDevice);//---触发设备无效----
            setGridColumnInvalid(dcTriggerKind);//---触发类型----       

            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效 
            setGridColumnInvalid(dcEndValue);//---结束值无效---
            setGridColumnInvalid(dcContinueTime);//---取消有效持续---
            setGridColumnInvalid(dcRecoverTime); //---取消无效持续---

            //gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---触发位置默认本地----
            //gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个触发类型选择----
            gvLogic.SetRowCellValue(0, dcOperate, this.cbxOperate.Items[0].ToString());//---第一个触发运算---
            gvLogic.SetRowCellValue(0, dcStartValue, this.cbxStart.Items[0].ToString());//---第一个开始选择运算---

        }

        public void CbxSecurityKindChanged(object sender, EventArgs e)
        {
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr.EndEdit();
            string securityKindName = dr[dcStartValue.FieldName].ToString();
            if (securityKindName == SensorConfig.LG_NAME_SAF_SYST_DI || securityKindName == SensorConfig.LG_NAME_SAF_SYST_EN_DLY ||
                securityKindName == SensorConfig.LG_NAME_SAF_SYST_EN || securityKindName == SensorConfig.LG_NAME_SAF_SYST_WAR ||
                securityKindName == SensorConfig.LG_NAME_SAF_SYST_ALM)
                setGridColumnInvalid(dcEndValue);//---结束值无效----
            else
            {
                dr[dcEndValue.FieldName] = "";//---清空选择----
                dr.EndEdit();
                switch (securityKindName)
                {     
                    case SensorConfig.LG_NAME_SAF_SELF_DI: setGridColumnValid(dcEndValue, new GridViewMultipleCheckEdit("被撤防者:逻辑动作", circuitCount)); break;//---结束值有效  
                    case SensorConfig.LG_NAME_SAF_SELF_EN_DLY: setGridColumnValid(dcEndValue, new GridViewMultipleCheckEdit("被布防者:逻辑动作", circuitCount)); break;//---结束值有效  
                    case SensorConfig.LG_NAME_SAF_SELF_EN: setGridColumnValid(dcEndValue, new GridViewMultipleCheckEdit("被布防者:逻辑动作", circuitCount)); break;//---结束值有效  
                    case SensorConfig.LG_NAME_SAF_SELF_WAR: setGridColumnValid(dcEndValue, new GridViewMultipleCheckEdit("触发者:逻辑动作", circuitCount)); break;//---结束值有效  
                    case SensorConfig.LG_NAME_SAF_SELF_ALM: setGridColumnValid(dcEndValue, new GridViewMultipleCheckEdit("触发者:逻辑动作", circuitCount)); break;//---结束值有效  

                    default: setGridColumnValid(dcEndValue, new GridViewMultipleCheckEdit(securityKindName, circuitCount)); break;
                }
            }
        }


        /// <summary>
        /// 获取逻辑数据
        /// </summary>
        /// <returns></returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
            TriggerData triggerData = GetInitTriggerData(dr);//----初始化触发数据----
            triggerData.CompareID = SensorConfig.LG_MATH_EQUAL_AND_TRUE2;//系统联动号为5的比较符号值
            //--------关闭/打开--------------
            string size1Str = dr[dcStartValue.FieldName].ToString();
            triggerData.Size1 = SecuritySensor.LEVEL_NAME_ID[size1Str];
            if (dcEndValue.OptionsColumn.AllowEdit)
            {
                string endValueStr = dr[dcEndValue.FieldName].ToString();//----选择---
                Int32 endValue = 0;
                if (endValueStr != "")
                {
                    string[] arrValueStr = endValueStr.Replace(" ", "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);          
                    foreach (string valueStr in arrValueStr)
                        endValue = endValue | (int)Math.Pow(2.0, Convert.ToDouble(valueStr) - 1.0);
                }
                triggerData.Size2 = (int)endValue;
            }
            else
                triggerData.Size2 = Int32.MinValue;//无效强制为0x80000000

            triggerData.InvalidSeconds = 0;//---强制为0
            triggerData.ValidSeconds = 0;//----强制为0
            dr.AcceptChanges();//----再次修改才保存-----

            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行---
            dr[dcDifferentDevice.FieldName] = SensorConfig.SENSOR_INVALID;  //---差异设备无效---
            dr[dcTriggerPosition.FieldName] = SensorConfig.SENSOR_INVALID;  //---触发位置-----
            dr[dcTriggerKind.FieldName] = SensorConfig.SENSOR_INVALID;      //---触发级别---

            //-----安防号操作----
            dr[dcStartValue.FieldName] = SecuritySensor.LEVEL_ID_NAME[td.Size1];
            CbxSecurityKindChanged(null,null);//----设置结束值是否有效---
            if (dcEndValue.OptionsColumn.AllowEdit)//---结束值有效----
            {
                Int32 endValue = td.Size2;
                int i = 1;
                string endValueStr = "";
                for (int n = 1; n <= circuitCount; n++)
                {
                    if ((endValue & i) == i)
                        endValueStr += ", " + n;
                    i = i * 2;
                }
                if (endValueStr != "")
                {
                    endValueStr = endValueStr.Substring(1);
                    dr[dcEndValue.FieldName] = endValueStr.Trim();
                }
            }

            dr.EndEdit();
            dr.AcceptChanges();
        }

        public override void KindChanged()
        {

        }

        public override void OperateChanged()
        {

        }

    }



    /// <summary>
    /// 时间段
    /// </summary>
    public class ViewLogicTime : BaseViewLogicControl
    { 
        private GridViewTimeEdit timeEdit = new GridViewTimeEdit();//----时间编辑---
        public ViewLogicTime(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_WITHIN);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_WITHOUT);

            timeEdit.Mask.EditMask = "HH:mm";
            dcStartValue.ColumnEdit = timeEdit;
            dcEndValue.ColumnEdit = timeEdit;

            cbxKind.Items.Clear();//----清空触发类型(探头只有等级)---
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_VALUE);//--触发类型(值)---- 

             
        }

        public override void InitViewSetting()
        {
            //setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            //setGridColumnValid(dcTriggerPosition, cbxPosition);//---触发位置有效----

            setGridColumnInvalid(dcTriggerPosition);//---触发位置无效
            setGridColumnInvalid(dcDifferentDevice);//---触发设备无效----
            setGridColumnInvalid(dcTriggerKind);//---触发类型----            

            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, timeEdit);//---开始值有效
            setGridColumnValid(dcEndValue, timeEdit);//----结束值有效---
            setGridColumnInvalid(dcContinueTime);//---持续时间----
            setGridColumnInvalid(dcRecoverTime);//----失效时间-----   

            //gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---初始化第一个运算选择----
            //gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----   
            gvLogic.SetRowCellValue(0, dcOperate, SensorConfig.LG_MATH_NAME_WITHIN);//---默认以内----
            gvLogic.SetRowCellValue(0, dcStartValue,"00:00");//--默认第一个开始值---
            gvLogic.SetRowCellValue(0, dcEndValue, "00:00");//--默认第一个开始值--- 

        }

        /// <summary>
        /// 获取逻辑配置数据
        /// </summary>
        /// <returns>TriggerData</returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
            TriggerData triggerData = GetInitTriggerData(dr);//----初始化触发数据----
            //--------开始秒数,结束秒数-------------- 
            DateTime dtStartTime = DateTime.Parse(dr[dcStartValue.FieldName].ToString());
            DateTime dtEndTime = DateTime.Parse(dr[dcEndValue.FieldName].ToString());

            byte[] startSeconds = new byte[] { 0, (byte)dtStartTime.Minute, (byte)dtStartTime.Hour, 0 };
            byte[] endSeconds = new byte[] { 0, (byte)dtEndTime.Minute, (byte)dtEndTime.Hour, 0 };
            triggerData.Size1 = ConvertTools.Bytes4ToInt(startSeconds);
            triggerData.Size2 = ConvertTools.Bytes4ToInt(endSeconds);
            triggerData.ValidSeconds = 0;//--无效强制为0
            triggerData.InvalidSeconds = 0;//---无效强制为0
            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行--- 
            dr[dcDifferentDevice.FieldName] = SensorConfig.SENSOR_INVALID;  //---差异设备无效---
            dr[dcTriggerPosition.FieldName] = SensorConfig.SENSOR_INVALID;  //---触发位置-----
            dr[dcTriggerKind.FieldName] = SensorConfig.SENSOR_INVALID;      //---触发级别---

            string nowDateStr = DateTime.Now.ToShortDateString();
            byte[] dtStartTime = ConvertTools.GetByteFromInt32(td.Size1);
            byte[] dtEndTime = ConvertTools.GetByteFromInt32(td.Size2);
            int dtStartSeconds = (int)dtStartTime[2] * 60 * 60 + (int)dtStartTime[1] * 60;
            int dtEndSeconds = (int)dtEndTime[2] * 60 * 60 + (int)dtEndTime[1] * 60;

            dr[dcStartValue.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(dtStartSeconds).ToLongTimeString();  //---开始时间---
            dr[dcEndValue.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(dtEndSeconds).ToLongTimeString();//----结束时间---
            dr.EndEdit();
            dr.AcceptChanges();
        }


        public override void KindChanged()
        {
        }

        public override void OperateChanged()
        {
        }
    }


    /// <summary>
    /// 日期段
    /// </summary>
    public class ViewLogicDateOfMonthDay : BaseViewLogicControl
    {
        private GridViewDateOfMonthDayEdit dateEdit = new GridViewDateOfMonthDayEdit();//----时间编辑---
        public ViewLogicDateOfMonthDay(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_WITHIN);
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_WITHOUT);

            dcStartValue.ColumnEdit = dateEdit;
            dcEndValue.ColumnEdit = dateEdit;

            cbxKind.Items.Clear();//----清空触发类型(探头只有等级)---
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_VALUE);//--触发类型(值)---- 
        }



        public override void InitViewSetting()
        {
            //setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            //setGridColumnValid(dcTriggerPosition, cbxPosition);//---触发位置有效----

            setGridColumnInvalid(dcTriggerPosition);//---触发位置无效
            setGridColumnInvalid(dcDifferentDevice);//---触发设备无效----
            setGridColumnInvalid(dcTriggerKind);//---触发类型----       

            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, dateEdit);//---开始值有效
            setGridColumnValid(dcEndValue, dateEdit);//----结束值有效---
            setGridColumnInvalid(dcContinueTime);//---持续时间----
            setGridColumnInvalid(dcRecoverTime);//----失效时间-----   

            //gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---初始化第一个运算选择----
            //gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----   
            gvLogic.SetRowCellValue(0, dcOperate, SensorConfig.LG_MATH_NAME_WITHIN);//---默认以内---- 
            gvLogic.SetRowCellValue(0, dcStartValue, "01-01");//----默认为1号
            gvLogic.SetRowCellValue(0, dcEndValue, "01-01");//----默认为1号

        }


        /// <summary>
        /// 获取逻辑配置数据
        /// </summary>
        /// <returns>TriggerData</returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
            TriggerData triggerData = GetInitTriggerData(dr);//----初始化触发数据----  
            //--------开始秒数,结束秒数-------------- 
            DateTime dtStartTime = DateTime.Parse(dr[dcStartValue.FieldName].ToString());
            DateTime dtEndTime = DateTime.Parse(dr[dcEndValue.FieldName].ToString());

            byte[] startDate = new byte[] {(byte)dtStartTime.Day, (byte)dtStartTime.Month,0,0 };
            byte[] endDate = new byte[] {(byte)dtEndTime.Day, (byte)dtEndTime.Month,0,0 };
            triggerData.Size1 = ConvertTools.Bytes4ToInt(startDate);
            triggerData.Size2 = ConvertTools.Bytes4ToInt(endDate);
            triggerData.ValidSeconds = 0;//--无效强制为0
            triggerData.InvalidSeconds = 0;//---无效强制为0
            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行--- 
            dr[dcDifferentDevice.FieldName] = SensorConfig.SENSOR_INVALID;  //---差异设备无效---
            dr[dcTriggerPosition.FieldName] = SensorConfig.SENSOR_INVALID;  //---触发位置-----
            dr[dcTriggerKind.FieldName] = SensorConfig.SENSOR_INVALID;      //---触发级别---
            string nowDateStr = DateTime.Now.ToShortDateString();
            byte[] dtStartTime = ConvertTools.GetByteFromInt32(td.Size1);
            byte[] dtEndTime = ConvertTools.GetByteFromInt32(td.Size2);

            DateTime dtStartValue = DateTime.Parse(dtStartTime[1].ToString() + "-" + dtStartTime[0].ToString());
            DateTime dtEndValue = DateTime.Parse(dtEndTime[1].ToString() + "-" + dtEndTime[0].ToString());

            dr[dcStartValue.FieldName] = dtStartValue;  //---开始时间---
            dr[dcEndValue.FieldName] = dtEndValue;      //----结束时间---
            dr.EndEdit();
            dr.AcceptChanges();
        }

        public override void KindChanged()
        {
             
        }

        public override void OperateChanged()
        {
 
        }
    }


    /// <summary>
    /// 星期范围
    /// </summary>
    public class ViewLogicWeek : BaseViewLogicControl
    {
        private GridViewWeekEdit iccbWeek = new GridViewWeekEdit();
        public ViewLogicWeek(Device _device, GridView gv)
            : base(_device, gv)
        {         
            setGridColumnValid(dcTriggerPosition, cbxPosition);//-------设置触发位置有效---
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//----触发 运算符---            
        } 

        public override void InitViewSetting()
        {
            //setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            //setGridColumnValid(dcTriggerPosition, cbxPosition);//---触发位置有效----

            setGridColumnInvalid(dcTriggerPosition);//---触发位置无效
            setGridColumnInvalid(dcDifferentDevice);//---触发设备无效----
            setGridColumnInvalid(dcTriggerKind);//---触发类型----       

            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, iccbWeek);//---开始值有效
            setGridColumnInvalid(dcEndValue);//---结束为无效---
            setGridColumnInvalid(dcContinueTime);//---取消有效持续---
            setGridColumnInvalid(dcRecoverTime); //---取消无效持续---

            //gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---触发位置默认本地----
            //gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个触发类型选择----
            gvLogic.SetRowCellValue(0, dcOperate, this.cbxOperate.Items[0].ToString());//---第一个触发运算---
            gvLogic.SetRowCellValue(0, dcStartValue, "");//---第一个开始选择运算---
            
        }


        /// <summary>
        /// 获取逻辑数据
        /// </summary>
        /// <returns></returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
            TriggerData triggerData = GetInitTriggerData(dr);//----初始化触发数据----
            triggerData.CompareID = SensorConfig.LG_MATH_EQUAL_AND_TRUE;  //等于("与"运算后如果为"真") (只判断[slSiz1])           <- if (val1 &  slSiz1)
            string weekValue = dr[dcStartValue.FieldName].ToString();//----选择的周结果---
            byte value = 0;
            if (weekValue != "")
            {
                string[] weeks = weekValue.Replace(" ","").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);             
                foreach(string week in weeks)
                {
                    if (week == "一") value = (byte)(value | 1);
                    else if (week == "二") value = (byte)(value | 2);
                    else if (week == "三") value = (byte)(value | 4);
                    else if (week == "四") value = (byte)(value | 8);
                    else if (week == "五") value = (byte)(value | 16);
                    else if (week == "六") value = (byte)(value | 32);
                    else if (week == "日") value = (byte)(value | 64);
                }
            }
            triggerData.Size1 =  (int)value;
            triggerData.ValidSeconds = 0;//--无效强制为0
            triggerData.InvalidSeconds = 0;//---无效强制为0
            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行--- 
            dr[dcDifferentDevice.FieldName] = SensorConfig.SENSOR_INVALID;  //---差异设备无效---
            dr[dcTriggerPosition.FieldName] = SensorConfig.SENSOR_INVALID;  //---触发位置-----
            dr[dcTriggerKind.FieldName] = SensorConfig.SENSOR_INVALID;      //---触发级别---

            byte[] weekByteValue = ConvertTools.GetByteFromInt32(td.Size1);
            byte byteWeeks = weekByteValue[0];
            string weekValue = "";
            if ((int)(byteWeeks & 1) == 1) weekValue += ", 一";
            if ((int)(byteWeeks & 2) == 2) weekValue += ", 二";
            if ((int)(byteWeeks & 4) == 4) weekValue += ", 三";
            if ((int)(byteWeeks & 8) == 8) weekValue += ", 四";
            if ((int)(byteWeeks & 16) == 16) weekValue += ", 五";
            if ((int)(byteWeeks & 32) == 32) weekValue += ", 六";
            if ((int)(byteWeeks & 64) == 64) weekValue += ", 日";
            if (weekValue != "")
            {
                weekValue = weekValue.Substring(1);
                dr[dcStartValue.FieldName] = weekValue.Trim();
            }

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


    /// <summary>
    /// 系统联动号
    /// </summary>
    public  class ViewLogicSystemInteraction : BaseViewLogicControl
    {
        public string[] LevelValues = { SensorConfig.LG_SYSLKID_NAME_ACT_OFF,SensorConfig.LG_SYSLKID_NAME_ACT_ON };
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值选择---
        private GridViewDigitalEdit edtNum = new GridViewDigitalEdit();//----数字------
        public ViewLogicSystemInteraction(Device _device, GridView gv)
            : base(_device, gv)
        {
            setGridColumnValid(dcTriggerPosition, cbxPosition);//-------设置触发位置有效---
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//----触发 运算符---
            //---开始为下拉----
            cbxStart.Items.Add(LevelValues[0]);
            cbxStart.Items.Add(LevelValues[1]);
            //---初始化温度编辑控件------
            edtNum.DisplayFormat.FormatString = "#0 号";
            edtNum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            edtNum.Mask.EditMask = "#0 号";
            edtNum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edtNum.Mask.UseMaskAsDisplayFormat = true;
            edtNum.MinValue = 0;
            edtNum.MaxValue = (int)DeviceConfig.SpecicalID.ID_PKGNUM_PUBLIC - 1;
        }


        public override void InitViewSetting()
        {
            //setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            //setGridColumnValid(dcTriggerPosition, cbxPosition);//---触发位置有效----

            setGridColumnInvalid(dcTriggerPosition);//---触发位置无效
            setGridColumnInvalid(dcDifferentDevice);//---触发设备无效----
            setGridColumnInvalid(dcTriggerKind);//---触发类型----       

            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效
            setGridColumnValid(dcEndValue, edtNum);//---结束为数字编辑---
            setGridColumnInvalid(dcContinueTime);//---取消有效持续---
            setGridColumnInvalid(dcRecoverTime); //---取消无效持续---

            //gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---触发位置默认本地----
            //gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个触发类型选择----
            gvLogic.SetRowCellValue(0, dcOperate, this.cbxOperate.Items[0].ToString());//---第一个触发运算---
            gvLogic.SetRowCellValue(0, dcStartValue, this.cbxStart.Items[0].ToString());//---第一个开始选择运算---
            gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束范围为0---
        }

        /// <summary>
        /// 获取逻辑数据
        /// </summary>
        /// <returns></returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
            TriggerData triggerData = GetInitTriggerData(dr);//----初始化触发数据----
            triggerData.CompareID = SensorConfig.LG_MATH_EQUAL_TO2;//系统联动号为5的比较符号值
            //--------关闭/打开--------------
            string size1Str = dr[dcStartValue.FieldName].ToString(); 
            triggerData.Size1 =  FindLevelIndex(LevelValues,size1Str); 
            triggerData.Size2 = Convert.ToInt16(dr[dcEndValue.FieldName]);
            triggerData.ValidSeconds = 0;//--无效强制为0
            triggerData.InvalidSeconds = 0;//---无效强制为0

            dr.AcceptChanges();//----再次修改才保存-----
            return triggerData;
        }        

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行---
            dr[dcDifferentDevice.FieldName] = SensorConfig.SENSOR_INVALID;  //---差异设备无效---
            dr[dcTriggerPosition.FieldName] = SensorConfig.SENSOR_INVALID;  //---触发位置-----
            dr[dcTriggerKind.FieldName] = SensorConfig.SENSOR_INVALID;      //---触发级别---

           //-----联动号操作----
            dr[dcStartValue.FieldName] = LevelValues[td.Size1];
            dr[dcEndValue.FieldName] = td.Size2;//联动号

            dr.EndEdit();
            dr.AcceptChanges();
        }

        public override void KindChanged()
        {
        }

        public override void OperateChanged()
        {
        }
    }

    /// <summary>
    /// 内部联动
    /// </summary>
    public partial class ViewLogicInnerInteraction : BaseViewLogicControl
    {

        public string[] LevelValues = { SensorConfig.LG_SYSLKID_NAME_ACT_OFF, SensorConfig.LG_SYSLKID_NAME_ACT_ON };
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值选择---
        private GridViewDigitalEdit edtNum = new GridViewDigitalEdit();//----数字------
        public ViewLogicInnerInteraction(Device _device, GridView gv)
            : base(_device, gv)
        {
            setGridColumnValid(dcTriggerPosition, cbxPosition);//-------设置触发位置有效---
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//----触发 运算符---
            //---开始为下拉----
            cbxStart.Items.Add(LevelValues[0]);
            cbxStart.Items.Add(LevelValues[1]);
            //---初始化温度编辑控件------
            edtNum.DisplayFormat.FormatString = "#0 号";
            edtNum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            edtNum.Mask.EditMask = "#0 号";
            edtNum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edtNum.Mask.UseMaskAsDisplayFormat = true;
            edtNum.MinValue = 0;
            edtNum.MaxValue = (int)DeviceConfig.SpecicalID.ID_PKGNUM_PUBLIC - 1;
        }


        public override void InitViewSetting()
        {
            //setGridColumnValid(dcTriggerKind, cbxKind);//---触发值有效----
            //setGridColumnValid(dcTriggerPosition, cbxPosition);//---触发位置有效----

            setGridColumnInvalid(dcTriggerPosition);//---触发位置无效
            setGridColumnInvalid(dcDifferentDevice);//---触发设备无效----
            setGridColumnInvalid(dcTriggerKind);//---触发类型----       

            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效
            setGridColumnValid(dcEndValue, edtNum);//---结束为数字编辑---
            setGridColumnInvalid(dcContinueTime);//---取消有效持续---
            setGridColumnInvalid(dcRecoverTime); //---取消无效持续---

            //gvLogic.SetRowCellValue(0, dcTriggerPosition, this.cbxPosition.Items[0].ToString());//---触发位置默认本地----
            //gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个触发类型选择----
            gvLogic.SetRowCellValue(0, dcOperate, this.cbxOperate.Items[0].ToString());//---第一个触发运算---
            gvLogic.SetRowCellValue(0, dcStartValue, this.cbxStart.Items[0].ToString());//---第一个开始选择运算---
            gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束范围为0---
        }

        /// <summary>
        /// 获取逻辑数据
        /// </summary>
        /// <returns></returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
            TriggerData triggerData = GetInitTriggerData(dr);//----初始化触发数据----
            dr[dcDifferentDevice.FieldName] = SensorConfig.SENSOR_INVALID;  //---差异设备无效---
            dr[dcTriggerPosition.FieldName] = SensorConfig.SENSOR_INVALID;  //---触发位置-----
            dr[dcTriggerKind.FieldName] = SensorConfig.SENSOR_INVALID;      //---触发级别---

            triggerData.CompareID = SensorConfig.LG_MATH_EQUAL_TO2;//系统联动号为5的比较符号值
            //--------关闭/打开--------------
            string size1Str = dr[dcStartValue.FieldName].ToString();
            triggerData.Size1 = FindLevelIndex(LevelValues, size1Str);
            triggerData.Size2 = Convert.ToInt16(dr[dcEndValue.FieldName]);
            triggerData.ValidSeconds = 0;//--无效强制为0
            triggerData.InvalidSeconds = 0;//---无效强制为0
            dr.AcceptChanges();//----再次修改才保存-----

            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行---
            //-----联动号操作----
            dr[dcStartValue.FieldName] = LevelValues[td.Size1];
            dr[dcEndValue.FieldName] = td.Size2;//联动号

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
    
    /// <summary>
    /// 无效
    /// </summary>
    public partial class ViewLogicInvalid : BaseViewLogicControl
    {
        public ViewLogicInvalid(Device _device, GridView gv)
            : base(_device, gv)
        {
            foreach (GridColumn gc in gvLogic.Columns)
                if (gc.VisibleIndex != 0) setGridColumnInvalid(gc);

        } 


        public override void InitViewSetting()
        {
   

        }
        public override TriggerData GetLogicData()
        {
            TriggerData triggerData = new TriggerData();
            return triggerData;
        }

        public override void SetLogicData(TriggerData td)
        {

        }


        public override void KindChanged()
        {
        }

        public override void OperateChanged()
        {
        }
    }


}
