using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public interface IFactory
    {
        FrmDevice CreateDevice(DeviceData device);
    }

    /// <summary>
    /// 一般设备
    /// </summary>
    public class FactoryBaseDevice : IFactory
    {
        #region IFactory 成员
        FrmDevice IFactory.CreateDevice(DeviceData data)
        {
            return new FrmBaseDevice(data);
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
