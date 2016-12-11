using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice 
{
    public class EnvironmentMenuView : BaseMenuView
    {
        private MenuSensorControl menuSecurityEdit;//---编辑界面---
        private MenuSensorData menuSensorData;//---传感器数据--

        public EnvironmentMenuView(WeiXin device, Control editControl, MenuData editData)
            : base(device, editControl, editData)
        {
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_BDEV_CFG);
        }
        public override void LoadEditData()
        {
         
        }

   
    }
}
