using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice 
{
    public class MenuCtrl
    {

        public static BaseMenuView CreateMenuSettingView(WeiXin device, Control editControl, MenuData editData)
        {
            if (editData.KindID == MenuKind.MS_COBJ_CMD)
                return new CommandMenuView(device,editControl,editData);
            //else if (editData.KindID == MenuKind.MS_COBJ_ENV)
            //    return new EnvironmentMenuView(device, editControl, editData);
            return null;
        }



    }
}
