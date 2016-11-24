using System;
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
            byte[] data = new byte[8+byteName.Length]; 
            byte[] byteMenuID = ConvertTools.GetByteFromUInt32(MenuID);

            Buffer.BlockCopy(data, 0, byteMenuID, 0, 4);
            data[4] = (byte)KindID;
            data[5] = (byte)Flag;
            Buffer.BlockCopy(data, 6, byteName, 0, byteName.Length);
            return data;

        }

    }



}
