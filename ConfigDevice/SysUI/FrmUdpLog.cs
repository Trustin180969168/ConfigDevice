using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ConfigDevice
{

    public partial class FrmUdpLog : Form
    {
        private MyTraceListener myTraceListener;
        private LockObject lockObject = new LockObject();
        public FrmUdpLog()
        {
            InitializeComponent();
        }

        private void FrmUdpLog_Load(object sender, EventArgs e)
        {
            myTraceListener = new MyTraceListener(SysConfig.LogFile); 
            myTraceListener.OnCallbackUI_Action += this.CallbackUI; 
            Trace.Listeners.Add(myTraceListener); //添加MyTraceListener实例
        }

        /// <summary>
        /// 回调
        /// </summary>
        public void CallbackUI(CallbackParameter callbackParameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(CallbackUI), callbackParameter);
                return;
            }
            if (lockObject.Locked)
                return;
            if (callbackParameter.Action != ActionKind.ShowLog)
                return;
            lock (lockObject)
            {
                lockObject.Locked = true;
                string msg = callbackParameter.Parameters[0] as String;
                mdtUpdLog.Text = msg + mdtUpdLog.Text + "\n\r";
                lockObject.Locked = false;
            }
        }

        private void FrmUdpLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            myTraceListener.OnCallbackUI_Action -= this.CallbackUI;
        }

    }

    public class LockObject
    {
        public bool Locked = false;
    }


}
