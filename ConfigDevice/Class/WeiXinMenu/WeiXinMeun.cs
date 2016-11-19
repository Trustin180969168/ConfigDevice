using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{


    public abstract class WeiXinMeun
    {
        public Int32 MenuID;//菜单ID
        public Int16 KindID;//类型ID
        public string KindName;//类型
        public Int16 Flag;//标志
        public string Title;//菜单标题

        public MenuData WeiXinMenuData
        {
            get
            {
                MenuData menuData = new MenuData();
                menuData.MenuID = MenuID;
                menuData.KindID = KindID;
                menuData.KindName = KindName;
                menuData.Flag = Flag;
                menuData.Title = Title; 

                return menuData;
            }
        }
         

        public WeiXinMeun(UserUdpData data)
        {
            MenuID = ConvertTools.Bytes4ToInt32(CommonTools.CopyBytes(data.Data,0,4)); 
            KindID = (Int16)data.Data[4];
            if(MenuKind.MenuKindIDName.ContainsKey(KindID))
                KindName = MenuKind.MenuKindIDName[KindID];
            Flag = ConvertTools.Bytes2ToInt16(data.Data[5],data.Data[6]);
            byte[] byteName = CommonTools.CopyBytes(data.Data, 8, data.Length - 9);
            Title = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0').Trim().Replace("", "");

        }

 


    }




}
