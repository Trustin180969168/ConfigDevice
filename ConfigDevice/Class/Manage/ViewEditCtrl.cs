using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice
{

    public static class ViewEditCtrl
    {

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


    }


}
