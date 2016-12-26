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
            if (editControl.Controls.Count == 0)
                editControl.Tag = MenuKind.MS_COBJ_DLE; 

            if (editData.ByteKindID == MenuKind.MS_COBJ_CMD)
                return new CommandMenuView(device,editControl,editData);
            else if (editData.ByteKindID == MenuKind.MS_COBJ_ENV)
                return new SensorMenuView(device, editControl, editData);
            return null;
        }



    }
}
