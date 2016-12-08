using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public abstract class BaseMenuView
    {
        public Control MenuSettingView;//菜单显示
        public WeiXin weixinDevice;//微信推送设备
        protected MenuData menuData;//菜单数据
        public abstract void InitMenuView();//初始化菜单方法


        public BaseMenuView(WeiXin device,Control editControl,MenuData editData)
        {
            weixinDevice = device;
            MenuSettingView = editControl;
            menuData = editData;
        }

    }
}
