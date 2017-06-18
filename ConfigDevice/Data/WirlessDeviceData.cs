using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class WirlessDeviceData
    {
        public byte Index = 0;//第几个
        public byte[] MacAddress = new byte[12];//MAC地址
        public bool Online = false;//是否在线
        public string Name = "";//设备名称

        /// <summary>
        /// MAC 字符串
        /// </summary>
        public string MacAddressStr
        {
            get { return ConvertTools.ByteToHexStr(MacAddress); }
        }

        /// <summary>
        /// 转换字符串值
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            byte[] byteArrayName = Encoding.GetEncoding("GB2312").GetBytes(Name);
            byte[] value = new byte[byteArrayName.Length + 1 + 13 + 1];
            value[0] = Index;
            Buffer.BlockCopy(MacAddress, 1, MacAddress, 0, 12);
            value[12] = Online ? (byte)1 : (byte)0;
            Buffer.BlockCopy(value, 13, byteArrayName, 0, byteArrayName.Length);
            return value;
        }

        /// <summary>
        /// 构造函数,根据字节数组映射对象
        /// </summary>
        /// <param name="data"></param>
        public WirlessDeviceData(byte[] data)
        {
            Index = data[10];//序号
            Buffer.BlockCopy(MacAddress, 0, data, 11, 12);//MAC地址
            Online = Convert.ToInt16(data[23]) == 1 ? true : false;//是否在线
            //---无线设备名称----
            byte[] byteName = new byte[data.Length - 14];
            Buffer.BlockCopy(byteName, 0, byteName, 0, data.Length - 14);
            Name = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0').Trim().Replace("", "");
        }


    }
}
