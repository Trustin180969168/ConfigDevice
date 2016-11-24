using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{


    public  class WeiXinMeun
    {
        public UInt32 MenuID;//菜单ID
        public UInt16 KindID;//类型ID
        public string KindName;//类型
        public UInt16 Flag;//标志
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
            MenuID = ConvertTools.Bytes4ToUInt32(CommonTools.CopyBytes(data.Data,0,4)); 
            KindID = (UInt16)data.Data[4];
            if(MenuKind.MenuKindIDName.ContainsKey(KindID))
                KindName = MenuKind.MenuKindIDName[KindID];
            Flag = ConvertTools.Bytes2ToUInt16(data.Data[5],data.Data[6]);
            byte[] byteName = CommonTools.CopyBytes(data.Data, 8, data.Length - 9);
            Title = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0').Trim().Replace("", "");

        }

 


    }




}
