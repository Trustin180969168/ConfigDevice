﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ConfigDevice
{
    public static class ViewEditCtrl
    {         
    
        private static DataTable dtCommandDevices = new DataTable();
        /// <summary>
        /// 获取指令配置
        /// </summary>
        /// <param name="kindId">类型</param>
        /// <returns></returns>
        public static BaseViewCommandControl GetViewCommandControl(ControlObj controlObj, GridView gv)
        {
            if (controlObj is ConfigDevice.Background)
                return new ViewBackgroundControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Circuit)
                return new ViewCircuitControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Messages)
                return new ViewMessagesControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Motor)
                return new ViewMotorControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Scene)
                return new ViewSceneControl(controlObj, gv);
            else if (controlObj is ConfigDevice.ServerControlObj)
                return new ViewServerControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Swit)
                return new ViewSwitControl(controlObj, gv);
            else if (controlObj is ConfigDevice.Timing)
                return new ViewTimingControl(controlObj, gv);
            else if (controlObj is ConfigDevice.InnerInteraction)
                return new ViewInnerInteractionControl(controlObj, gv);
            else if (controlObj is ConfigDevice.OuterInteraction)
                return new ViewOuterInteractionControl(controlObj, gv);
            else return null;
        }

        /// <summary>
        /// 获取逻辑配置
        /// </summary>
        /// <param name="kindId">触发对象ID</param>
        /// <returns></returns>
        public static BaseViewLogicControl GetViewLogicControl(int TriggerID,Device device, GridView gv)
        {
            if (TriggerID == SensorConfig.LG_SENSOR_DEFAULT)
                return new ViewLogicInvalid(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_LEL)
                return new ViewLogicFlamableGasProbe(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_TEMP)
                return new ViewLogicTemperature(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_TEMP_FC)
                return new ViewLogicFireControlTemperature(device, gv);
            else if (TriggerID == SensorConfig.LG_EXT_SENSOR_SYS_LKID)
                return new ViewLogicSystemInteraction(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_HUMI)
                return new ViewLogicHumidity(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_RSP)
                return new ViewLogicRadar(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_TAMPER)
                return new ViewLogicSwitTamper(device, gv);
            else if (TriggerID == SensorConfig.LG_EXT_SENSOR_TIME_SEG)
                return new ViewLogicTime(device, gv);
            else if (TriggerID == SensorConfig.LG_EXT_SENSOR_DATE_SEG)
                return new ViewLogicDateOfMonthDay(device, gv);
            else if (TriggerID == SensorConfig.LG_EXT_SENSOR_WEEK_CYC)
                return new ViewLogicWeek(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_WIND)
                return new ViewLogicWindy(device, gv);
            else
                return new ViewLogicInvalid(device, gv); 
        }

        /// <summary>
        /// 判断是否有效时间
        /// </summary>
        /// <param name="seconds">秒数</param>
        /// <returns></returns>
        public static int getSecondsFromTimeStr(string timeStr)
        {
            try
            {
                DateTime dtValid = DateTime.Parse(timeStr);
                int validSeconds = dtValid.Hour * 60 * 60 + dtValid.Minute * 60 + dtValid.Second;           //有效秒数
                if (validSeconds > 64800)
                { CommonTools.MessageShow("运行时间不能大于18小时!", 2, ""); return 64800; }
                else
                    return validSeconds;
            }
            catch { CommonTools.MessageShow("时间格式错误!", 2, ""); return 0; }
        }


        /// <summary>
        /// 获取设备表选择控件
        /// </summary>
        /// <returns></returns>
        public static GridViewGridLookupEdit GetLogicDevicesLookupEdit()
        {
            GridViewGridLookupEdit gridLookupDevice = new GridViewGridLookupEdit();//设备列表选择编辑
            gridLookupDevice.Name = "gridLookupEdit";
            gridLookupDevice.NullText = "选择设备";
            gridLookupDevice.DisplayMember = DeviceConfig.DC_NAME;
            gridLookupDevice.ValueMember = ViewConfig.DC_DEVICE_VALUE;
            //-----获取选择的设备数据---
            DataTable dtSelectDevices = SysConfig.DtDevice.Clone();
            DataRow[] rows = SysConfig.DtDevice.Select(ViewConfig.SELECT_LOGIC_DEVICE_QUERY_CONDITION);
            foreach (DataRow dr in rows)
                dtSelectDevices.Rows.Add(dr.ItemArray);
            //----初始化新的设备值----
            dtSelectDevices.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));
            foreach (DataRow dr in dtSelectDevices.Rows)
                dr[ViewConfig.DC_DEVICE_VALUE] = dr[DeviceConfig.DC_KIND_ID].ToString() + dr[DeviceConfig.DC_NETWORK_ID].ToString() + dr[DeviceConfig.DC_ID].ToString();
            dtSelectDevices.AcceptChanges();
            //----初始化下拉界面------
            gridLookupDevice.DataSource = dtSelectDevices;
            foreach (GridColumn gc in gridLookupDevice.View.Columns)
            {
                if (gc.FieldName != DeviceConfig.DC_ID && gc.FieldName != DeviceConfig.DC_NETWORK_ID &&
                    gc.FieldName != DeviceConfig.DC_KIND_NAME && gc.FieldName != DeviceConfig.DC_NAME)
                    gc.Visible = false;
            }
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).SortIndex = 0;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).SortIndex = 1;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).VisibleIndex = 0;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).VisibleIndex = 1;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).VisibleIndex = 2;
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).VisibleIndex = 3;

            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).Caption = "设备ID";
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).Caption = "设备名称";
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).Caption = "网段";
            gridLookupDevice.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).Caption = "设备类型";
            gridLookupDevice.View.BestFitColumns();

            return gridLookupDevice;
        }

        /// <summary>
        /// 获取命令列表的设备选择
        /// </summary>
        public static GridViewGridLookupEdit GetCommandDevicesLookupEdit
        {
            get
            {
                if (dtCommandDevices.Rows.Count == 0)//----判断是否初始化-----
                   InitCommandDevicesLookupEdit(); 
                GridViewGridLookupEdit commandDevicesLookupEdit = new GridViewGridLookupEdit();//设备列表选择编辑
                commandDevicesLookupEdit.Name = "gridLookupEdit";
                commandDevicesLookupEdit.NullText = "选择设备";
                commandDevicesLookupEdit.DisplayMember = DeviceConfig.DC_NAME;
                commandDevicesLookupEdit.ValueMember = ViewConfig.DC_DEVICE_VALUE;
                //----初始化下拉界面------
                commandDevicesLookupEdit.DataSource = dtCommandDevices;
                foreach (GridColumn gc in commandDevicesLookupEdit.View.Columns)
                {
                    if (gc.FieldName != DeviceConfig.DC_ID && gc.FieldName != DeviceConfig.DC_NETWORK_ID &&
                        gc.FieldName != DeviceConfig.DC_KIND_NAME && gc.FieldName != DeviceConfig.DC_NAME)
                        gc.Visible = false;
                }
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).SortIndex = 0;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).SortIndex = 1;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).VisibleIndex = 0;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).VisibleIndex = 1;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).VisibleIndex = 2;
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).VisibleIndex = 3;

                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_ID).Caption = "设备ID";
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NAME).Caption = "设备名称";
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_NETWORK_ID).Caption = "网段";
                commandDevicesLookupEdit.View.Columns.ColumnByFieldName(DeviceConfig.DC_KIND_NAME).Caption = "设备类型";
                commandDevicesLookupEdit.View.BestFitColumns();

                return commandDevicesLookupEdit;
            }
        }
        /// <summary>
        /// 获取命令设备表选择控件
        /// </summary>
        /// <returns></returns>
        public static void InitCommandDevicesLookupEdit()
        {
            //-----获取选择的设备数据---
            dtCommandDevices.Clear();
            dtCommandDevices = SysConfig.DtDevice.Clone();
            DataRow[] rows = SysConfig.DtDevice.Select(ViewConfig.SELECT_LOGIC_DEVICE_QUERY_CONDITION);
            foreach (DataRow dr in rows)
                dtCommandDevices.Rows.Add(dr.ItemArray);
            //----默认加入虚拟设备"系统联动号"------
            DataRow drInsert = dtCommandDevices.Rows.Add();
            drInsert[DeviceConfig.DC_KIND_ID] = DeviceConfig.EQUIPMENT_LINKID;//----系统联动号类型------
            drInsert[DeviceConfig.DC_NETWORK_ID] = 255;//----网段ID---
            drInsert[DeviceConfig.DC_ID] = (int)DeviceConfig.SpecicalID.ID_PKGNUM_PUBLIC;//-----特殊地址----- 
            drInsert[DeviceConfig.DC_NAME] = "联动号";
            drInsert[DeviceConfig.DC_KIND_NAME] = DeviceConfig.EQUIPMENT_ID_NAME[DeviceConfig.EQUIPMENT_LINKID]; 
            //----初始化新的设备值----
            dtCommandDevices.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));
            foreach (DataRow dr in dtCommandDevices.Rows)
                dr[ViewConfig.DC_DEVICE_VALUE] = dr[DeviceConfig.DC_KIND_ID].ToString() +"_"+ dr[DeviceConfig.DC_NETWORK_ID].ToString() + "_" + dr[DeviceConfig.DC_ID].ToString();
            dtCommandDevices.AcceptChanges();
     
        }

    

    }


}
