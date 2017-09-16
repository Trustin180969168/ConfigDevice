using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class WirlessDeviceData
    {
        public byte Index = 0;//第几个
        public byte State = 0;//是否在线
        public byte[] MacAddress = new byte[12];//MAC地址
        public byte DeviceType = 0;//设备类型
        public string Name = "";//设备名称

        /// <summary>
        /// MAC 字符串
        /// </summary>
        public string MacAddressStr
        {
            get { return ConvertTools.ByteToHexStr(MacAddress); }
            set {
                MacAddress = ConvertTools.StrToToHexByte(value);   
            }
        }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool OnLine
        {
            get { return Convert.ToInt16(State) == 1 ? true : false; }//是否在线 
            set
            {
                State = (byte)(value ? 1 : 0);
            }
        }


        /// <summary>
        /// 转换字符串值
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            byte[] byteArrayName = Encoding.GetEncoding("GB2312").GetBytes(Name);
            byte[] value = new byte[byteArrayName.Length + 15];

            value[0] = Index;
            value[1] = State;
            Buffer.BlockCopy(MacAddress, 0, value, 2, 12);
            value[14] = DeviceType;
            Buffer.BlockCopy(byteArrayName, 0, value, 15, byteArrayName.Length);
            return value;
        }

        /// <summary>
        /// 构造函数,根据字节数组映射对象
        /// </summary>
        /// <param name="data"></param>
        public WirlessDeviceData(UserUdpData userUdpData)
        {
            byte[] data = userUdpData.Data;
            Index = data[0];//序号
            State = data[1];//是否在线 
            MacAddress = CommonTools.CopyBytes(data, 2, 12);
            DeviceType = data[14];//设备类型
            //---无线设备名称----
            byte[] byteName = CommonTools.CopyBytes(data, 15, data.Length - 15);
            Name = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0').Trim().Replace(" ", "");
        }


              /// <summary>
        /// 构造函数,根据字节数组映射对象
        /// </summary>
        /// <param name="data"></param>
        public WirlessDeviceData()
        {
        }
    }
}
