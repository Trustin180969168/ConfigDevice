using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class CommandReadObj
    {

        public MenuData MenuData;
        public int Index;

        public CommandReadObj(int _groupIndex)
        {  
            Index = _groupIndex;
        }

        public CommandReadObj(MenuData menuData)
        {
            MenuData = menuData;
        }



    }
}
