using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class MenuData
    {
        public UInt32 MenuID;//菜单ID
        public byte ByteKindID;//类型ID
        public string KindName;//类型
        public UInt16 Flag;//标志
        public string Title;//菜单标题

        public byte[] ByteArrMenuID
        {
            get
            {
                return  ConvertTools.GetByteFromUInt32(MenuID);
            }
        }

        public byte[] GetByteData()
        {
            byte[] byteName = Encoding.GetEncoding("GB2312").GetBytes(Title);   
            byte[] data = new byte[9+byteName.Length]; 
            byte[] byteMenuID = ConvertTools.GetByteFromUInt32(MenuID);

            Buffer.BlockCopy(byteMenuID, 0, data, 0, 4);
            data[4] = (byte)ByteKindID; 
         
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
            MenuID = 0;
            ByteKindID = MenuKind.MS_COBJ_DLE;
        }

        public MenuData(UserUdpData data)
        {
            MenuID = ConvertTools.Bytes4ToUInt32(CommonTools.CopyBytes(data.Data,0,4)); 
            ByteKindID = data.Data[4];
            if(MenuKind.MenuKindIDName.ContainsKey(ByteKindID))
                KindName = MenuKind.MenuKindIDName[ByteKindID];
            Flag = ConvertTools.Bytes2ToUInt16(data.Data[5],data.Data[6]);
            byte[] byteName = CommonTools.CopyBytes(data.Data, 8, data.DataLength - 9 - 4);
            Title = ConvertTools.ToGB2312Str(byteName);

        }

        
 

    }



}
