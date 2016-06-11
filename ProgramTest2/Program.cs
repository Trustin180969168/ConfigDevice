using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProgramTest2
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Int16 a = 1;
            byte[] ba = BitConverter.GetBytes(a);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());




        }
    }
}
