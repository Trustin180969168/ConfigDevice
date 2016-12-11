using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public class CommandMenuView:BaseMenuView
    {
        MenuSecurityControl menuSecurityEdit;//---编辑界面---


        public CommandMenuView(WeiXin device, Control editControl, MenuData editData)
            : base(device, editControl, editData)
        {
            menuSecurityEdit = new MenuSecurityControl();
            menuSecurityEdit.Dock = DockStyle.Top;
            menuSecurityEdit.Height = 300;
            editControl.Controls.Clear();
            editControl.Controls.Add(menuSecurityEdit);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_COMMAND);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_SECURITY_CFG);
    
            menuSecurityEdit.InitEdit(device, editData);
        } 

        public override void LoadEditData()
        {
            menuSecurityEdit.GetSecurityData();
        }

   
    }
}
