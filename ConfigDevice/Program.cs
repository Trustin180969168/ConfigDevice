using System;
using System.Collections.Generic;

using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ConfigDevice
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            initLog();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);       
            Application.Run(new FrmMain());
      
        }

        private static void initLog()
        {
            Trace.Listeners.Clear();  //清除系统监听器 (就是输出到Console的那个)
            string logPath = Application.StartupPath +  "\\log\\";
            string logFile = logPath + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            String[] logFiles = Directory.GetFiles(logPath);
            foreach (string s in logFiles)
            {
                FileInfo fileInfo = new FileInfo(s);
                if (fileInfo.CreationTime < DateTime.Now.AddDays(-7))
                    File.Delete(s);
            }
            Trace.Listeners.Add(new MyTraceListener(logFile)); //添加MyTraceListener实例
        }
    }
}
