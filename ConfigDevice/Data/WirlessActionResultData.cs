using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public enum WirlessActionResult
    {
        DeleteSuccess = 0,
        DeleteFailure = 0x80,

        AddSuccess = 1,
        AddFailure = 0x81,

        ClearSuccess = 2,
        ClearFailure = 0x82,

        None = -1
    }

    public class WirlessActionResultData
    {
        public byte Standby;//备用
        public WirlessActionResult ActionResult = WirlessActionResult.None;//操作结果
        public byte[] MAC = new byte[12];//地址
        public byte Kind;//设备类型
        public string Name;//设备名称

        /// <summary>
        /// MAC 字符串
        /// </summary>
        public string MacAddressStr
        {
            get { return ConvertTools.ByteToHexStr(MAC); }
        }

        public WirlessActionResultData(byte[] data)
        {
           int resultFlag = Convert.ToInt16(data[1]);
            switch (resultFlag)
            {
                case 0: ActionResult = WirlessActionResult.DeleteSuccess; break;
                case 0x80: ActionResult = WirlessActionResult.DeleteFailure; break;
                case 1: ActionResult = WirlessActionResult.AddSuccess; break;
                case 0x81: ActionResult = WirlessActionResult.AddFailure; break;
                case 2: ActionResult = WirlessActionResult.ClearSuccess; break;
                case 0x82: ActionResult = WirlessActionResult.ClearFailure; break;
                default: break;
            }
            Buffer.BlockCopy(data, 2, MAC, 0, 12);
            Kind = data[14];
            byte[] byteName = CommonTools.CopyBytes(data, 15, data.Length - 15);
            Name = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0').Trim().Replace(" ", "");

        }

    }
}
