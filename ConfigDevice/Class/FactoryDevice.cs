using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace ConfigDevice
{
    public interface IFactory
    {
        FrmDevice CreateDevice(DataRow data);
    }

    /// <summary>
    /// 一般设备
    /// </summary>
    public class FactoryBaseDevice : IFactory
    {
        #region IFactory 成员
        FrmDevice IFactory.CreateDevice(DataRow data)
        {
            DeviceData device = new BaseDevice(data);
            return new FrmBaseDevice(device);
        }

        #endregion

    }

    /// <summary>
    /// 门输入4
    /// </summary>
    public class FactoryFourInput : IFactory
    {
        #region IFactory 成员
        FrmDevice IFactory.CreateDevice(DataRow data)
        {
            DoorInput4 input4 = new DoorInput4(data);
            return new FrmFourInput(input4);
        }

        #endregion
    }

}
