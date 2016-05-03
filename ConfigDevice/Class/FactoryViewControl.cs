using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice
{
    public interface ICreateViewControl
    {
        ViewControl CreateViewControl(ControlObj controlObj, GridView gv);
    }

    /// <summary>
    /// 回路
    /// </summary>
    public class FactoryViewCircuit : ICreateViewControl
    {
        #region ICreateViewControl 成员
        public ViewControl CreateViewControl(ControlObj controlObj, GridView gv)
        {
            return new ViewCircuitControl(controlObj as Circuit, gv);
        }
        #endregion
    }


    /// <summary>
    /// 电机
    /// </summary>
    public class FactoryViewMotor : ICreateViewControl
    {
        #region ICreateViewControl 成员
        public ViewControl CreateViewControl(ControlObj controlObj, GridView gv)
        {
            return new ViewMotorControl(controlObj as Motor, gv);
        }
        #endregion
    }


}
