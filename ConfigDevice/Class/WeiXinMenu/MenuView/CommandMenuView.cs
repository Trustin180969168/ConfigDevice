using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public class CommandMenuView:BaseMenuView
    {
        MenuSecurityEdit menuSecurityEdit;//---编辑界面---


        public CommandMenuView(WeiXin device, Control editControl, MenuData editData)
            : base(device, editControl, editData)
        {
            menuSecurityEdit = new MenuSecurityEdit();
            menuSecurityEdit.Dock = DockStyle.Top;
            menuSecurityEdit.Height = 300;
            editControl.Controls.Clear();
            editControl.Controls.Add(menuSecurityEdit);
    
            menuSecurityEdit.InitEdit(device, editData);
        } 

        public override void LoadEditData()
        {
            menuSecurityEdit.GetSecurityData();
        }
    }
}
