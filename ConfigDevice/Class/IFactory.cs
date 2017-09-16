using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public interface IFactory
    {
         FrmDevice CreateDevice(DeviceData device);
    }
}
