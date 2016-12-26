using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class MenuSecurityControl : MenuEditControl
    {
        MenuSecurityEdit menuSecurity; //---安防编辑---
        MenuSecurityData menuSecurityData;//---安防数据---
        public MenuSecurityControl() 
        {
            InitializeComponent(); 
        }

        /// <summary>
        /// 覆盖基类的初始化方法
        /// </summary>
        /// <param name="_device"></param>
        /// <param name="_data"></param>
        /// <param name="_edit"></param>
        public override void InitEdit(WeiXin _device, MenuData _data)
        {
            base.InitEdit(_device, _data);
            menuSecurity = new MenuSecurityEdit(_device, _data);
            menuEdit = menuSecurity;

            menuSecurity.OnCallbackUI_Action += this.callbackUI;

            viewCommandSetting.CbxCommandGroup.Visible = false;
            viewCommandSetting.InitMenuCommand(_device);
            viewCommandSetting.ReadMenuCommandData(_data); 
        }

        /// <summary>
        /// 获取安防数据
        /// </summary>
        public void GetSecurityData()
        {
            menuSecurity.ReadMenuSecurity();
        }

        private void callbackUI(CallbackParameter callbackParameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(this.callbackUI), callbackParameter);
                return;
            }
            if (callbackParameter.Action == ActionKind.ReadMenuSecurity)
            {
                menuSecurityData = callbackParameter.Parameters[0] as MenuSecurityData;
                if (menuSecurityData.MenuId == menuData.MenuID)
                {
                    if (menuSecurityData.IsSecurityHomeCancel)
                        rgSecurity.SelectedIndex = 1;
                    else if (menuSecurityData.IsSecurityAll)
                        rgSecurity.SelectedIndex = 2;
                    else if (menuSecurityData.IsSecurityOutside)
                        rgSecurity.SelectedIndex = 3;
                    else
                        rgSecurity.SelectedIndex = 0;

                }
            }
        }
         
        /// <summary>
        /// 保存菜单配置
        /// </summary>
        public override void SaveSetting()
        {
            if(menuSecurityData == null) return;
            menuSecurityData.MenuId = menuData.MenuID;
            menuSecurityData.KindId = (byte)menuData.ByteKindID;
            if (rgSecurity.SelectedIndex == 1)
                menuSecurityData.IsSecurityHomeCancel = true;
            else if (rgSecurity.SelectedIndex == 2)
                menuSecurityData.IsSecurityAll = true;
            else if (rgSecurity.SelectedIndex == 3)
                menuSecurityData.IsSecurityOutside = true;
            else
                menuSecurityData.IsSecurityNone = true;
            menuSecurity.SaveMenuSecurity(menuSecurityData);
        }
    }
}
