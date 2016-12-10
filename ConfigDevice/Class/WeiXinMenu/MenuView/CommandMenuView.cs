using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public class CommandMenuView:BaseMenuView
    {
        MenuSecurityEdit menuSecurityEdit;//---编辑界面---
        MenuSecurity menuSecurity;//---编辑对象

        public CommandMenuView(WeiXin device, Control editControl, MenuData editData)
            : base(device, editControl, editData)
        {
            menuSecurityEdit = new MenuSecurityEdit();
            menuSecurityEdit.Dock = DockStyle.Top;
            menuSecurityEdit.Height = 300;
            editControl.Controls.Clear();
            editControl.Controls.Add(menuSecurityEdit);
            
            menuSecurity = new MenuSecurity(device,editData);
            menuSecurityEdit.InitEdit(device, editData, menuSecurity);
            
        }

         

        public override void InitMenuView()
        {
          
        }
    }
}
