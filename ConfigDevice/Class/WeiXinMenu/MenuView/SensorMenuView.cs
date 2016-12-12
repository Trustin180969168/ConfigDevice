using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice 
{
    public class SensorMenuView : BaseMenuView
    {
        private MenuSensorControl menuSensorControl;//---编辑界面--- 

        public SensorMenuView(WeiXin device, Control editControl, MenuData editData)
            : base(device, editControl, editData)
        {
            menuSensorControl = new MenuSensorControl();
            menuSensorControl.Dock = DockStyle.Top;
            menuSensorControl.Height = 300;
            editControl.Controls.Clear();
            editControl.Controls.Add(menuSensorControl);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_BDEV_CFG);

            menuSensorControl.InitEdit(device, editData);
        }
        public override void LoadEditData()
        {
          //  menuSensorControl.GetSensorData();
        }

   
    }
}
