using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace ConfigDevice
{

    //**********************设备编辑***************************
    public interface IFactoryDeviceEdit
    {
        FrmDevice CreateDevice(DataRow data);
    }

    /// <summary>
    /// 一般设备,如:功放
    /// </summary>
    public class FactoryBaseDeviceEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            DeviceData device = new BaseDevice(data);
            return new FrmBaseDevice(device);
        }
        #endregion
    }

    /// <summary>
    /// 门输入4
    /// </summary>
    public class FactoryDoor4InputEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            DoorInput4 input4 = new DoorInput4(data);
            return new FrmFourInput(input4);
        }
        #endregion
    }


    //**********************设备***************************

    public interface IFactoryDevice
    {
        DeviceData CreateDevice(DataRow data);
    }
    /// <summary>
    /// 功放
    /// </summary>
    public class FactoryBaseDevice : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new BaseDevice(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 功放
    /// </summary>
    public class FactoryAmplifier : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new Amplifier(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 门输入4
    /// </summary>
    public class FactoryDoorInput4 : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new DoorInput4(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 3路窗帘
    /// </summary>
    public class FactoryRoad3Window : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new Road3Window(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 4路继电器
    /// </summary>
    public class FactoryRoad4Relay : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new Road4Relay(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 6路继电器
    /// </summary>
    public class FactoryRoad6Relay : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new Road6Relay(data);
            return device;
        }

        #endregion
    }


    /// <summary>
    /// 2路前沿调光
    /// </summary>
    public class FactoryRoad2FrontDimming : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new Road2FrontDimming(data);
            return device;
        }

        #endregion
    }


    /// <summary>
    /// 4路前沿调光
    /// </summary>
    public class FactoryRoad4FrontDimming : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new Road4FrontDimming(data);
            return device;
        }

        #endregion
    }


    /// <summary>
    /// 6路前沿调光
    /// </summary>
    public class FactoryRoad6FrontDimming : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new Road6FrontDimming(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 8路前沿调光
    /// </summary>
    public class FactoryRoad8FrontDimming : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new Road8FrontDimming(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 12路前沿调光
    /// </summary>
    public class FactoryRoad12FrontDimming : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new Road12FrontDimming(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 8路继电器
    /// </summary>
    public class FactoryRoad8Relay : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new Road8Relay(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 服务器
    /// </summary>
    public class FactoryServers : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public DeviceData CreateDevice(DataRow data)
        {
            DeviceData device = new Server(data);
            return device;
        }

        #endregion
    }

}
