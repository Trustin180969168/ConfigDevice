using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    class WaitingTimer
    {


        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern uint GetTickCount();
        /// <summary>
        /// 程序等待延迟执行
        /// </summary>
        /// <param name="ms"></param>
        public static void Sleep(uint ms)
        {
            uint start = GetTickCount();
            while (GetTickCount() - start < ms)
            {
                Application.DoEvents();
            }
        }


    }
}
