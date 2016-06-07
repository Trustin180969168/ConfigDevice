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
        /// <param name="kindId">触发对象名称</param>
        /// <returns></returns>
        public static ViewLogicControl GetViewLogicControl(string TriggerName,Device device, GridView gv)
        {

            if (TriggerName == ViewConfig.SENSOR_INVALID)
                return new ViewLogicInvalid(device, gv);
            else if (TriggerName == ViewConfig.SENSOR_FLAMMABLE_GAS_PROBE)
                return new ViewLogicFlamableGasProbe(device, gv);
            else if (TriggerName == ViewConfig.SENSOR_TEMPERATURE)
                return new ViewLogicTemperature(device, gv);
            else if (TriggerName == ViewConfig.SENSOR_SYSTEM_INTERACTION)
                return new ViewLogicSystemInteraction(device, gv);
            else if (TriggerName == ViewConfig.SENSOR_HUMIDITY)
                return new ViewLogicHumidity(device, gv);
            else if (TriggerName == ViewConfig.SENSOR_RADAR)
                return new ViewLogicRadar(device, gv);
            else if (TriggerName == ViewConfig.SENSOR_SWIT_TAMPER)
                return new ViewLogicSwitTamper(device, gv);
            else if (TriggerName == ViewConfig.SENSOR_TIME)
                return new ViewLogicTime(device, gv);
            else if (TriggerName == ViewConfig.SENSOR_DATE)
                return new ViewLogicDateOfMonthDay(device, gv);
            else if (TriggerName == ViewConfig.SENSOR_WEEK)
                return new ViewLogicWeek(device, gv);
            else if (TriggerName == ViewConfig.SENSOR_WINDY)
                return new ViewLogicWindy(device, gv);
            else
                return new ViewLogicInvalid(device, gv); 

        }
    }


}
