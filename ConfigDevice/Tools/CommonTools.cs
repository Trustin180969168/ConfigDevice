using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ConfigDevice
{
    public class CommonTools
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern bool EndDialog(IntPtr hDlg, int nResult);
        const int WM_CLOSE = 0x10;
        /// <summary>
        /// 用于提示窗体自动关闭
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="caption">标题</param>
        /// <param name="second">秒</param>
        /// <returns></returns>
        public static DialogResult AutoCloseShow(string text, string caption, int second)
        {
            Timer time = new Timer();
            time.Interval = second * 1000;
            time.Tick += (a, b) =>
            {
                IntPtr ptr = FindWindow(null, caption);
                if (ptr != IntPtr.Zero) EndDialog(ptr, 0);
                time.Stop();
            };
            time.Start();
            MessageBox.Show(text, "提示!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return DialogResult.OK;
        }

        /// <summary>
        /// 回复结果
        /// </summary>
        public static void ShowReplyInfo(byte result)
        {
            switch (result)
            {
                case REPLY_RESULT.CMD_TRUE: MessageShow(REPLY_INFO.STR_TRUE, 1, ""); break;
                case REPLY_RESULT.CMD_FALSE: MessageShow(REPLY_INFO.STR_FALSE, 2, ""); break;
                case REPLY_RESULT.CMD_UNSAME: MessageShow(REPLY_INFO.STR_UNSAME, 2, ""); break;
                case REPLY_RESULT.CMD_BUSY: MessageShow(REPLY_INFO.STR_BUSY, 2, ""); break;
                case REPLY_RESULT.CMD_ERR: MessageShow(REPLY_INFO.STR_ERR, 2, ""); break;
                default: break;
            }

        }

        /// <summary>
        /// 回复结果
        /// </summary>
        public static void ShowReplyInfo(string msg,byte result)
        {
            switch (result)
            {
                case REPLY_RESULT.CMD_TRUE: MessageShow(msg + "\n" + REPLY_INFO.STR_TRUE, 1, ""); break;
                case REPLY_RESULT.CMD_FALSE: MessageShow(msg + "\n" + REPLY_INFO.STR_FALSE, 2, ""); break;
                case REPLY_RESULT.CMD_UNSAME: MessageShow(msg + "\n" + REPLY_INFO.STR_UNSAME, 2, ""); break;
                case REPLY_RESULT.CMD_BUSY: MessageShow(msg + "\n" + REPLY_INFO.STR_BUSY, 2, ""); break;
                case REPLY_RESULT.CMD_ERR: MessageShow(msg + "\n" + REPLY_INFO.STR_ERR, 2, ""); break;
                default:break;
            }

        }
        /// <summary> 封装messagebox  </summary>
        /// <param name="content">提示内容</param>
        /// <param name="title">标题</param>
        /// <param name="flag">提示类型标识 1:提示,2:失败,3:停止,4:询问,5:警告</param>
        /// <param name="remark">备注</param>   
        public static DialogResult MessageShow(string content, int flag, string remark)
        {
            if (remark != "")
                content = "\n" + remark;
            switch (flag)
            {
                case 1: return AutoCloseShow(content, "提示!", 1);//MessageBox.Show(content, "提示!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                case 2: return MessageBox.Show(content, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                case 3: return MessageBox.Show(content, "停止!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                case 4: return MessageBox.Show(content, "询问?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                case 5: return MessageBox.Show(content, "警告!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                default: return DialogResult.None;
            }
        }

        /// <summary>
        /// 判断两个字节数组是否相等
        /// </summary>
        /// <param name="b1">数组1</param>
        /// <param name="b2">数组2</param>
        /// <returns></returns>
        public static bool BytesEuqals(byte[] b1, byte[] b2)
        {
            if (b1.Length != b2.Length) return false;
            if (b1 == null || b2 == null) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 复制字节,返回一个新的字节数组
        /// </summary>
        /// <param name="src">源字节数组</param>
        /// <param name="start">开始位置,从0开始</param>
        /// <param name="len">复制个数</param>
        /// <returns>字节数组</returns>
        public static byte[] CopyBytes(byte[] src, int start, int len)
        {
            byte[] value= new byte[len];
            Buffer.BlockCopy(src, start, value, 0, len);
            return value;
        }



    }
}
