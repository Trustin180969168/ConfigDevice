using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public abstract class MenuView
    {
        public Control MenuSettingView;//菜单显示
        protected MenuData menuData;//菜单数据
        public abstract void InitMenuView();//初始化菜单方法


        public MenuView(Control editControl,MenuData editData)
        {
            MenuSettingView = editControl;
            menuData = editData;
        }

    }
}
