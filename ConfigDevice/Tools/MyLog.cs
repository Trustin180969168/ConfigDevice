using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using environment = System.Environment;

namespace ConfigDevice
{
    public class MyTraceListener : TraceListener
    {
        public string FilePath { get; private set; }
        private static readonly Object lockObj = new object();
        public CallbackUIAction OnCallbackUI_Action; 

        public MyTraceListener(string filePath)
        {
            if (!File.Exists(filePath))
                File.Create(filePath).Close();            
            FilePath = filePath;
        }

        public override void Write(string message)
        {
            File.AppendAllText(FilePath, message);
        }
        public override void WriteLine(string message)
        {
            lock (lockObj)
            {
                string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss    ") + environment.NewLine + message + environment.NewLine;
                File.AppendAllText(FilePath, msg);
                if (OnCallbackUI_Action != null)
                    OnCallbackUI_Action.BeginInvoke(new CallbackParameter(ActionKind.ShowLog,null, msg),null,null);
            }
        }
        public override void Write(object o, string category)
        {
            string message = string.Empty;
            if (!string.IsNullOrEmpty(category))
            {
                message = category + "";
            }
            if (o is Exception)//如果参数对象o是与Exception类兼容,输出异常消息+堆栈,否则输出o.ToString()
            {
                var ex = (Exception)o;
                message += ex.Message + environment.NewLine;
                message += ex.StackTrace;
            }
            else if (null != o)
            {
                message += o.ToString();
            }

            WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss    ") + environment.NewLine + message + environment.NewLine);
        }
    }


}
