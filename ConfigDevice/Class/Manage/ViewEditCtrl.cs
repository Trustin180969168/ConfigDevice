using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Drawing;

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
            else if (controlObj is ConfigDevice.CupboardSwit)
                return new ViewCupboardControl(controlObj, gv);
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
            else if (TriggerID == SensorConfig.LG_SENSOR_AQI)
                return new ViewLogicAQI(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_TVOC)
                return new ViewLogicTVOC(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_CO2)
                return new ViewLogicCO2(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_CH2O)
                return new ViewLogicCH20(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_PM25)
                return new ViewLogicPM25(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_O2)
                return new ViewLogicO2(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_TEMP_FC)
                return new ViewLogicFireControlTemperature(device, gv);
            else if (TriggerID == SensorConfig.LG_EXT_SENSOR_SYS_LKID)
                return new ViewLogicSystemInteraction(device, gv);
            else if (TriggerID == SensorConfig.LG_EXT_SENSOR_SLF_LKID)
                return new ViewLogicInnerInteraction(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_HUMI)
                return new ViewLogicHumidity(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_LUMI)//--亮度
                return new ViewLogicLuminance(device, gv);
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
            else if (TriggerID == SensorConfig.LG_SENSOR_WIND)//---风速---
                return new ViewLogicWindy(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_RAIN)//---雨感---
                return new ViewLogicRain(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_TAMPER)//---防拆开关---
                return new ViewLogicSwitTamper(device, gv);
            else if (TriggerID == SensorConfig.LG_EXT_SENSOR_SECURITY)//---安防联动---
                return new ViewLogicSecurityInteraction(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_UW_1 || TriggerID == SensorConfig.LG_SENSOR_UW_2)//---超声波1,2---
                return new ViewLogicUltrasonicWave(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_IR)//---红外---
                return new ViewLogicIRSensor(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_SCIN_1 || TriggerID == SensorConfig.LG_SENSOR_SCIN_2||
                TriggerID == SensorConfig.LG_SENSOR_SCIN_3 || TriggerID == SensorConfig.LG_SENSOR_SCIN_4)//---短路输入---
                return new ViewLogicShortInput(device, gv);
            else if (TriggerID == SensorConfig.LG_SENSOR_SN_1_2 || TriggerID == SensorConfig.LG_SENSOR_SN_1_2_3)//--顺序输入---
                return new ViewLogicOrder(device, gv);
            else
                return new ViewLogicInvalid(device, gv); 
        }

        /// <summary>
        /// 获取控制对象
        /// </summary>
        /// <param name="cmd">指令</param>
        /// <returns></returns>
        public static string GetControlObj(CommandData cmdData)
        {
            byte kind = cmdData.TargetType;
            switch (kind)
            {
                case DeviceConfig.EQUIPMENT_CURTAIN_3CH:
                case DeviceConfig.EQUIPMENT_CURTAIN_2CH: return "电机";
                case DeviceConfig.EQUIPMENT_SWIT_4:
                case DeviceConfig.EQUIPMENT_SWIT_6:
                case DeviceConfig.EQUIPMENT_SWIT_8:
                case DeviceConfig.EQUIPMENT_TRAILING_2:
                case DeviceConfig.EQUIPMENT_TRAILING_4:
                case DeviceConfig.EQUIPMENT_TRAILING_6:
                case DeviceConfig.EQUIPMENT_TRAILING_8:

                case DeviceConfig.EQUIPMENT_TRAILING_12:
                    {
                        if (CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP_NOT) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN_CONDITION) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE_CONDITION) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP))
                            return DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME;
                        if (CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_SCENE) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_SCENE_OPEN) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_SCENE_CLOSE) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_SCENE_NOT) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP))
                            return DeviceConfig.CONTROL_OBJECT_SCENE_NAME;
                        if (CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LIST) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LIST_OPEN) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LIST_CLOSE) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LIST_NOT) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_LOOP))
                            return DeviceConfig.CONTROL_OBJECT_TIMING_NAME;
                        if (CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_ALL) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_ALL_OPEN) ||
                        CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_SW_SWIT_ALL_CLOSE))
                            return DeviceConfig.CONTROL_OBJECT_SWIT_NAME;
                    } break;
                case DeviceConfig.EQUIPMENT_AMP_MP3:
                    {
                        if (cmdData.Data[2] == (byte)AudioKind.GENERAL_BGM)
                            return DeviceConfig.CONTROL_OBJECT_BGM_NAME;
                        if (cmdData.Data[2] == (byte)AudioKind.TG_MESSAGE)
                            return DeviceConfig.CONTROL_OBJECT_MESSAGE_NAME;
                    } break;
                case DeviceConfig.EQUIPMENT_LINKID:
                    {
                        if (CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_LOGIC_WRITE_SYSLKID) ||
                                    CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_LOGIC_WRITE_SYSLKID_OPEN) ||
                                    CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_LOGIC_WRITE_SYSLKID_CLOSE))
                            return "系统联动号";
                        if (CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_LOGIC_WRITE_SLFLKID) ||
                                    CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_LOGIC_WRITE_SLFLKID_OPEN) ||
                                    CommonTools.BytesEuqals(cmdData.Cmd, DeviceConfig.CMD_LOGIC_WRITE_SLFLKID_CLOSE))
                            return "内部联动号";
                    } break;
                case DeviceConfig.EQUIPMENT_SERVER: return "服务器";
                default: return "无效";
            }
            return "无效";

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
        /// 获取设备表选择数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDevicesLookupData(string filerExpression)
        {
            //-----获取选择的设备数据---
            DataTable dtSelectDevices = new DataTable();
            dtSelectDevices = SysConfig.DtDevice.Clone();
            DataRow[] rows = SysConfig.DtDevice.Select(filerExpression);
            foreach (DataRow dr in rows)
                dtSelectDevices.Rows.Add(dr.ItemArray);
            //----默认加入虚拟设备"系统联动号","内部联动"------
            DataRow drInsert = dtSelectDevices.Rows.Add();
            drInsert[DeviceConfig.DC_KIND_ID] = DeviceConfig.EQUIPMENT_LINKID;//----系统联动号类型------
            drInsert[DeviceConfig.DC_NETWORK_ID] = 255;//----网段ID---
            drInsert[DeviceConfig.DC_ID] = (int)DeviceConfig.SpecicalID.ID_PKGNUM_PUBLIC;//-----特殊地址----- 
            drInsert[DeviceConfig.DC_NAME] = "联动号";
            drInsert[DeviceConfig.DC_KIND_NAME] = DeviceConfig.EQUIPMENT_ID_NAME[DeviceConfig.EQUIPMENT_LINKID];
            //----初始化新的设备值----
            dtSelectDevices.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));
            foreach (DataRow dr in dtSelectDevices.Rows)
                dr[ViewConfig.DC_DEVICE_VALUE] = dr[DeviceConfig.DC_KIND_ID].ToString() + "_" + dr[DeviceConfig.DC_NETWORK_ID].ToString() + "_" + dr[DeviceConfig.DC_ID].ToString();
                        
            dtSelectDevices.AcceptChanges();
            return dtSelectDevices;
        }

        /// <summary>
        /// 获取设备表选择数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDevicesSpecialPanelLookupData(string filerExpression)
        {
            //-----获取选择的设备数据---
            DataTable dtSelectDevices = new DataTable();
            dtSelectDevices = SysConfig.DtDevice.Clone();
            DataRow[] rows = SysConfig.DtDevice.Select(filerExpression);
            foreach (DataRow dr in rows)
                dtSelectDevices.Rows.Add(dr.ItemArray); 
            //----初始化新的设备值----
            dtSelectDevices.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));
            foreach (DataRow dr in dtSelectDevices.Rows)
                dr[ViewConfig.DC_DEVICE_VALUE] = dr[DeviceConfig.DC_KIND_ID].ToString() + "_" + dr[DeviceConfig.DC_NETWORK_ID].ToString() + "_" + dr[DeviceConfig.DC_ID].ToString();

            dtSelectDevices.AcceptChanges();
            return dtSelectDevices;
        }

        /// <summary>
        /// 获取设备表选择控件
        /// </summary>
        /// <returns></returns>
        public static GridViewGridLookupEdit GetDevicesLookupEdit()
        {
            GridViewGridLookupEdit gridLookupDevice = new GridViewGridLookupEdit();//设备列表选择编辑
            gridLookupDevice.Name = "gridLookupEdit";
            gridLookupDevice.NullText = "选择设备";
            gridLookupDevice.DisplayMember = DeviceConfig.DC_NAME;
            gridLookupDevice.ValueMember = ViewConfig.DC_DEVICE_VALUE;
            ////-----获取选择的设备数据---
            DataTable dtSelectDevices = SysConfig.DtDevice.Clone();
            //DataRow[] rows = SysConfig.DtDevice.Select(filerExpression);
            //foreach (DataRow dr in rows)
            //    dtSelectDevices.Rows.Add(dr.ItemArray);
            ////----初始化新的设备值----
            //dtSelectDevices.Columns.Add(ViewConfig.DC_DEVICE_VALUE, System.Type.GetType("System.String"));
            //foreach (DataRow dr in dtSelectDevices.Rows)
            //    dr[ViewConfig.DC_DEVICE_VALUE] = dr[DeviceConfig.DC_KIND_ID].ToString() +"_" + dr[DeviceConfig.DC_NETWORK_ID].ToString() +"_"+ dr[DeviceConfig.DC_ID].ToString();
            //dtSelectDevices.AcceptChanges();
            ////----初始化下拉界面------
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

            gridLookupDevice.PopupFormMinSize = new System.Drawing.Size(500, 500);
            gridLookupDevice.View.BestFitColumns();

            return gridLookupDevice;
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
            //----默认加入虚拟设备"系统联动号","内部联动"------
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

        public static GridViewTextEdit InvalidEdit = new GridViewTextEdit();//--无效编辑----
        static ViewEditCtrl()
        {
            InvalidEdit.ReadOnly = true;
            InvalidEdit.NullText = "无效";
            InvalidEdit.AllowFocused = false;
        }

        /// <summary>
        /// 设置无效
        /// </summary>
        /// <param name="gc"></param>
        public static void setGridColumnInvalid(GridColumn gc,GridView gv)
        {

            DataRow dr =gv.GetDataRow(0);
            if (gc.FieldName != "")
                dr[gc.FieldName] = SensorConfig.SENSOR_INVALID;//---内容为无效
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
        public static void setGridColumnValid(GridColumn gc, DevExpress.XtraEditors.Repository.RepositoryItem editor)
        {
            gc.ColumnEdit = editor;
            gc.AppearanceCell.BackColor = Color.LightYellow;
            gc.AppearanceCell.ForeColor = Color.Blue;
            gc.OptionsColumn.AllowEdit = true;

        }

    }


}
