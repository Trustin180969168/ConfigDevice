﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public class CommandMenuView:BaseMenuView
    {
        MenuSecurityControl menuSecurityControl;//---编辑界面---


        public CommandMenuView(WeiXin device, Control editControl, MenuData editData)
            : base(device, editControl, editData)
        {
            menuSecurityControl = new MenuSecurityControl();
            menuSecurityControl.Dock = DockStyle.Fill;
            if (Int32.Parse(editControl.Tag.ToString()) !=  MenuKind.MS_COBJ_CMD)
            {
                editControl.Controls.Clear();
                editControl.Controls.Add(menuSecurityControl);
            }
            //---清空回调----
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_COMMAND);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_MMSG_WRITE_SECURITY_CFG);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END);
    
            menuSecurityControl.InitEdit(device, editData);
        } 

        public override void LoadEditData()
        {
            menuSecurityControl.GetSecurityData();
        }



        public override void SaveEdit()
        {
            menuSecurityControl.SaveSetting();
        }
    }
}
