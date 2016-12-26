﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice 
{
    public  class MenuEditControl :UserControl
    { 

        protected WeiXin device;
        protected MenuData menuData;
        protected BaseMenuEdit menuEdit;
        public virtual void SaveSetting() { }

        public virtual void InitEdit(WeiXin _device, MenuData _data)
        {
            device = _device;
            menuData = _data;
        }

    }
}