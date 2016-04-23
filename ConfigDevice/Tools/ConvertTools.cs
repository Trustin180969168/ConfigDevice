using System;
using System.Collections.Generic;

using System.Text;

namespace ConfigDevice
{

    //
    // 摘要:ByteConvert.cs
    //     字节转换工具
    //
    public class ConvertTools
    {
        /// <summary> 
        /// 字符串转16进制字节数组 
        /// </summary> 
        /// <param name="hexString"></param> 
        /// <returns></returns> 
        public static byte[] StrToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary> 
        /// 字节数组转16进制字符串 
        /// </summary> 
        /// <param name="bytes"></param> 
        /// <returns></returns> 
        public static string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += " "+bytes[i].ToString("X2")  ;
                }
                returnStr = returnStr.Substring(1);
            }
            return returnStr;
        }

        /// <summary> 
        /// 从汉字转换到16进制 
        /// </summary> 
        /// <param name="s"></param> 
        /// <param name="charset">编码,如"utf-8","gb2312"</param> 
        /// <param name="fenge">是否每字符用逗号分隔</param> 
        /// <returns></returns> 
        public static string ToHex(string s, string charset, bool fenge)
        {
            if ((s.Length % 2) != 0)
            {
                s += " ";//空格 
                //throw new ArgumentException("s is not valid chinese string!"); 
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            byte[] bytes = chs.GetBytes(s);
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X}", bytes[i]);
                if (fenge && (i != bytes.Length - 1))
                {
                    str += string.Format("{0}", ",");
                }
            }
            return str.ToLower();
        }

        ///<summary> 
        /// 从16进制转换成汉字 
        /// </summary> 
        /// <param name="hex"></param> 
        /// <param name="charset">编码,如"utf-8","gb2312"</param> 
        /// <returns></returns> 
        public static string UnHex(string hex, string charset)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格 
            }
            // 需要将 hex 转换成 byte 数组。 
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message. 
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            return chs.GetString(bytes);
        }


        /// <summary>
        /// 字节转10进制
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Bytes4ToInt(byte[] b)
        {            
           return System.BitConverter.ToInt32(b, 0); 
        }

        /// <summary>
        /// 十进制字符串转16进制字符串
        /// </summary>
        /// <param name="intValueStr">10进制字符串</param>
        /// <returns>string</returns>
        public static string IntStrToHexStr(string intValueStr)
        {
            int intValue = Convert.ToInt16(intValueStr);
            string hexStr = "";
            hexStr = intValue.ToString("X");
            if (hexStr.Length == 1) hexStr = "0" + hexStr;
            return hexStr;
        }

        /// <summary>
        /// 根据一个8位的整数,翻译成一个字节
        /// </summary>
        /// <param name="num">8位整数字符串</param>
        /// <returns>一个字节</returns>
        public static byte GetByteFrom8BitNumStr(string num)
        {
            byte value = BitConverter.GetBytes(Convert.ToInt16(num))[0];
            return value;
        }

        /// <summary>
        /// 根据一个8位的整数,翻译成一个字节
        /// </summary>
        /// <param name="num">8位整数字符串</param>
        /// <returns>一个字节</returns>
        public static byte GetByteFrom8BitNum(int num)
        {
            byte value = BitConverter.GetBytes(num)[0];
            return value;
        }

        /// <summary>
        /// 根据一个16位的整数,翻译成两个字节的数组
        /// </summary>
        /// <param name="num">16位整数</param>
        /// <returns>两个字节</returns>
        public static byte[] GetByteFrom16BitInt(int num)
        {
            byte[] value = BitConverter.GetBytes(Convert.ToInt16(num));
            return value;
        }
    }
}
