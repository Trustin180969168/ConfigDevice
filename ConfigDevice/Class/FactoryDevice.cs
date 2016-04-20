using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    /// <summary>
    /// 一般设备
    /// </summary>
    public class FactoryBaseDevice : IFactory
    {
        #region IFactory 成员
        FrmDevice IFactory.CreateDevice(DeviceData data)
        {
            return new FrmFourInput(data);
        }

        #endregion

    }

    /// <summary>
    /// 门输入
    /// </summary>
    public class FactoryFourInput : IFactory
    {
        #region IFactory 成员
        FrmDevice IFactory.CreateDevice(DeviceData data)
        {
            return new FrmFourInput(data);
        }

        #endregion
    }

}
