using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;

namespace ConfigDevice
{
    /// <summary>
    /// 超声波
    /// </summary>
    public class ViewLogicUltrasonicWave : BaseViewLogicControl
    {
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值---        
        public ViewLogicUltrasonicWave(Device _device, GridView gv)
            : base(_device, gv)
        {

            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//---运算--           
            cbxStart.Items.Add(UWSensorData.LEVEL_ID_NAME[0]);
            cbxStart.Items.Add(UWSensorData.LEVEL_ID_NAME[1]);
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
             
            string size1Str = dr[dcStartValue.FieldName].ToString();
            triggerData.Size1 = UWSensorData.LEVEL_NAME_ID[size1Str];//----获取等级值---
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
            dr[dcStartValue.FieldName] = UWSensorData.LEVEL_ID_NAME[td.Size1];
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
    /// 红外
    /// </summary>
    public class ViewLogicIRSensor : BaseViewLogicControl
    {
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值---        
        public ViewLogicIRSensor(Device _device, GridView gv)
            : base(_device, gv)
        {

            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//---运算--           
            cbxStart.Items.Add(IRSensorData.LEVEL_ID_NAME[0]);
            cbxStart.Items.Add(IRSensorData.LEVEL_ID_NAME[1]);
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
            triggerData.Size1 = IRSensorData.LEVEL_NAME_ID[size1Str];//----获取等级值---
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
            dr[dcStartValue.FieldName] = IRSensorData.LEVEL_ID_NAME[td.Size1];
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
    /// 顺序触发
    /// </summary>
    public class ViewLogicOrder : BaseViewLogicControl
    {
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值---        
        public ViewLogicOrder(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxStart.Items.Add(OrderSensorData.LEVEL_ID_NAME[0]);
            cbxStart.Items.Add(OrderSensorData.LEVEL_ID_NAME[1]);
            dcStartValue.ColumnEdit = cbxStart;
            cbxKind.Items.Clear();//----清空触发类型(只有等级)---
            cbxKind.Items.Add(SensorConfig.SENSOR_VALUE_KIND_LEVEL);//--触发类型(等级)---- 
            cbxOperate.Items.Add(SensorConfig.LG_MATH_NAME_EQUAL_TO);//----触发 运算符---        

        }

        public override void InitViewSetting()
        {
 

            setGridColumnInvalid(dcTriggerPosition);//---触发位置无效
            setGridColumnInvalid(dcDifferentDevice);//---触发设备无效---- 

            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效
            setGridColumnInvalid(dcEndValue);//-结束值无效----
            setGridColumnInvalid(dcContinueTime);//---触发时间为无效---
            setGridColumnValid(dcRecoverTime, RecoverTimeEdit);//----恢复数据有效----

            gvLogic.SetRowCellValue(0, dcTriggerKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择---- 
            gvLogic.SetRowCellValue(0, dcOperate, SensorConfig.LG_MATH_NAME_EQUAL_TO);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxStart.Items[0].ToString());//--默认第一个开始值--- 
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
            
            string size1Str = dr[dcStartValue.FieldName].ToString();
            triggerData.Size1 = OrderSensorData.LEVEL_NAME_ID[size1Str];//----获取等级值---
            triggerData.Size2 = triggerData.Size1;//----强制与开始值相同------
            //-----有效持续,无效持续------             
            int invalidSeconds = ViewEditCtrl.getSecondsFromTimeStr(dr[dcRecoverTime.FieldName].ToString());    //恢复秒数       
            triggerData.ValidSeconds = 0;//-----强制为0
            triggerData.InvalidSeconds = (UInt16)invalidSeconds;
            string nowDateStr = DateTime.Now.ToShortDateString(); 
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
            dr[dcDifferentDevice.FieldName] = SensorConfig.SENSOR_INVALID;  //---差异设备无效---
            dr[dcTriggerPosition.FieldName] = SensorConfig.SENSOR_INVALID;  //---触发位置-----
            dr[dcStartValue.FieldName] = OrderSensorData.LEVEL_ID_NAME[td.Size1];

            string nowDateStr = DateTime.Now.ToShortDateString(); 
            dr[dcRecoverTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.InvalidSeconds).ToLongTimeString();//----无效持续---
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
