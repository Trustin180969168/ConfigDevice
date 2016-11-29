﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class MenuData
    {
        public UInt32 MenuID;//菜单ID
        public UInt16 KindID;//类型ID
        public string KindName;//类型
        public UInt16 Flag;//标志
        public string Title;//菜单标题


        public byte[] GetByteData()
        {
            byte[] byteName = Encoding.GetEncoding("GB2312").GetBytes(Title);   
            byte[] data = new byte[9+byteName.Length]; 
            byte[] byteMenuID = ConvertTools.GetByteFromUInt32(MenuID);

            Buffer.BlockCopy(byteMenuID, 0, data, 0, 4);
            data[4] = (byte)KindID; 
         
            Buffer.BlockCopy(byteName, 0, data, 8, byteName.Length);
            return data;

        }

        public string GetMenuCode()
        {            
            int[] level = new int[3];
            int result = WeiXinMenu.GetMemuLevel((int)MenuID, level);//获取level的层级关系
            if (result < 0) return "";
            string menuCode = level[0].ToString() + "." + level[1].ToString() + "." + level[2].ToString();

            return menuCode;
        }

        public string GetMenuParentCode()
        {

            int[] level = new int[3];
            int result = WeiXinMenu.GetMemuLevel((int)MenuID, level);//获取level的层级关系
            if (result < 0) return "";
            string menuCode = level[0].ToString() + "." + level[1].ToString();

            return menuCode;
        }

        public MenuData()
        {
        }

        public MenuData(UserUdpData data)
        {
            MenuID = ConvertTools.Bytes4ToUInt32(CommonTools.CopyBytes(data.Data,0,4)); 
            KindID = (UInt16)data.Data[4];
            if(MenuKind.MenuKindIDName.ContainsKey(KindID))
                KindName = MenuKind.MenuKindIDName[KindID];
            Flag = ConvertTools.Bytes2ToUInt16(data.Data[5],data.Data[6]);
            byte[] byteName = CommonTools.CopyBytes(data.Data, 8, data.DataLength - 9 - 4);
            Title = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0').Trim().Replace("", "");

        }

        
 

    }



}
