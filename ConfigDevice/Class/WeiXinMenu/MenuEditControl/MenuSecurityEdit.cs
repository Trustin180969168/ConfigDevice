using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class WeiXinSecurityEdit : MenuEditControl
    {
        MenuSecurity menuSecurity; //---安防编辑---
        public WeiXinSecurityEdit() 
        {
            InitializeComponent(); 
        }

        /// <summary>
        /// 覆盖基类的初始化方法
        /// </summary>
        /// <param name="_device"></param>
        /// <param name="_data"></param>
        /// <param name="_edit"></param>
        public override void InitEdit(WeiXin _device, MenuData _data, BaseMenuEdit _edit)
        {
            base.InitEdit(_device, _data, _edit);
            menuSecurity = new MenuSecurity(_device, menuData);
            menuSecurity.OnCallbackUI_Action += this.callbackUI;
            menuSecurity.ReadMenuSecurity();
        }


        private void callbackUI(CallbackParameter callbackParameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(this.callbackUI), callbackParameter);
                return;
            }
        }


        
    }
}
