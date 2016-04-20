using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    /// <summary>
    /// 抽象工厂
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
    /// 简单工厂
    /// </summary>
    public class FactoryDevice
    {
        /// <summary>
        /// 根据设备类型创建设备对象
        /// </summary>
        /// <param name="kind">设备类型ID</param>   
        /// <returns>设备</returns>
        public static FrmDevice CreateDevice(byte kind,UserUdpData userData)
        {
            switch (kind)
            {
                case DeviceConfig.EQUIPMENT_AMP_MP3:
                    return new FrmAmplifier(new DeviceData(userData));

                default: return null; 
            }
        }







    }

}
