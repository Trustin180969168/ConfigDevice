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
                    returnStr += " " + bytes[i].ToString("X2");
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
        /// <param name="b">4个字节</param>
        /// <returns></returns>
        public static Int32 Bytes4ToInt32(params byte[] b)
        {
            return System.BitConverter.ToInt32(b, 0);
        }

        /// <summary>
        /// 字节转10进制
        /// </summary>
        /// <param name="b">4个字节</param>
        /// <returns></returns>
        public static UInt32 Bytes4ToUInt32(params byte[] b)
        {
            return System.BitConverter.ToUInt32(b, 0);
        }


        /// <summary>
        /// 字节转10进制
        /// </summary>
        /// <param name="b">两个字节</param>
        /// <returns></returns>
        public static short Bytes2ToInt16(params byte[] b)
        {
            return System.BitConverter.ToInt16(b, 0);
        }

        /// <summary>
        /// 字节转10进制
        /// </summary>
        /// <param name="b">两个字节</param>
        /// <returns></returns>
        public static UInt16 Bytes2ToUInt16(params byte[] b)
        {
            return System.BitConverter.ToUInt16(b, 0);
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
        /// 根据一个32位以下的整数,取首字节
        /// </summary>
        /// <param name="num">32位整数</param>
        /// <returns>一个字节</returns>
        public static byte GetByteFromIntNum(int num)
        {
            byte value = BitConverter.GetBytes(num)[0];
            return value;
        }

        /// <summary>
        /// 根据一个16位的整数,翻译成两个字节的数组
        /// </summary>
        /// <param name="num">16位整数</param>
        /// <returns>两个字节</returns>
        public static byte[] GetByteFromInt16(int num)
        {
            byte[] value = BitConverter.GetBytes(Convert.ToInt16(num));
            return value;
        }

        /// <summary>
        /// 根据一个16位无符号整数,翻译成两个字节的数组
        /// </summary>
        /// <param name="num">16位无符号整数</param>
        /// <returns>两个字节</returns>
        public static byte[] GetByteFromUInt16(UInt16 num)
        {
            byte[] value = BitConverter.GetBytes(Convert.ToUInt16(num));
            return value;
        }


        /// <summary>
        /// 根据一个32位的有符号整数,翻译成4个字节的数组
        /// </summary>
        /// <param name="num">32位有符号整数</param>
        /// <returns>4个字节</returns>
        public static byte[] GetByteFromInt32(int num)
        {
            byte[] value = BitConverter.GetBytes(Convert.ToInt32(num));
            return value;
        }

        /// <summary>
        /// 根据一个32位的无符号整数,翻译成4个字节的数组
        /// </summary>
        /// <param name="num">32位无符号整数</param>
        /// <returns>4个字节</returns>
        public static byte[] GetByteFromUInt32(uint num)
        {
            byte[] value = BitConverter.GetBytes(num);
            return value;
        }


        /// <summary>
        /// 汉字转换为Unicode编码
        /// </summary>
        /// <param name="str">要编码的汉字字符串</param>
        /// <returns>Unicode编码的的字符串</returns>
        public static string ToUnicode(string str)
        {
            byte[] bts = Encoding.Unicode.GetBytes(str);
            string r = "";
            for (int i = 0; i < bts.Length; i += 2) r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
            return r;
        }

        /// <summary>
        /// 字节转GB2312字符串
        /// </summary>
        /// <param name="data">字符数组</param>
        /// <returns>GB2312字符串</returns>
        public static string ToGB2312Str(byte[] data)
        {
            string tempName = Encoding.GetEncoding("GB2312").GetString(data).Trim();
            if(tempName.IndexOf('\0') != -1)
                tempName = tempName.Substring(0, tempName.IndexOf('\0'));
            return tempName.Trim();
        }
    }
}
