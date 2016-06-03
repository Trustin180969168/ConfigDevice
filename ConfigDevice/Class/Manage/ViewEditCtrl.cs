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
        public static ViewCommandControl GetViewCommandControl(ControlObj controlObj, GridView gv)
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

            if (TriggerName == ViewConfig.TRIGGER_INVALID)
                return new ViewLogicInvalid(device, gv);
            if (TriggerName == ViewConfig.TRIGGER_FLAMMABLE_GAS_PROBE)
                return new ViewLogicFlamableGasProbe(device, gv);
            if (TriggerName == ViewConfig.TRIGGER_TEMPERATURE)
                return new ViewLogicTemperature(device, gv);
            if (TriggerName == ViewConfig.TRIGGER_SYSTEM_INTERACTION)
                return new ViewLogicSystemInteraction(device, gv);
            else
                return new ViewLogicInvalid(device, gv); 

        }
    }


}
