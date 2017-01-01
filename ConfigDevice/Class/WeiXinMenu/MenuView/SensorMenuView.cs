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
            if ((editControl.Tag as MenuData).ByteKindID != MenuKind.MS_COBJ_ENV)
            {
                menuSensorControl = new MenuSensorControl();
                menuSensorControl.Dock = DockStyle.Top;
                menuSensorControl.Height = 300;
                menuSensorControl.Name = "menuSensorControl";
                editControl.Controls.Clear();
                editControl.Controls.Add(menuSensorControl);
            }
            else
                menuSensorControl = editControl.Controls["menuSensorControl"] as MenuSensorControl;

            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_BDEV_CFG);
            menuSensorControl.InitEdit(device, editData);
        }
        public override void LoadEditData()
        {
            menuSensorControl.GetSensorData();
        }



        public override void SaveEdit()
        {
            menuSensorControl.SaveSetting();
        }
    }
}
