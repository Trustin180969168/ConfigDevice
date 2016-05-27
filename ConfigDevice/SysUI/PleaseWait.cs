using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Runtime.InteropServices;

namespace ConfigDevice
{
    public partial class PleaseWait :Form
    {
        public string labelmsg = "";
        private int seconds = 1;
        public int ShowSeconds = -1;//--显示秒数
        public PleaseWait()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Opaque, false);
        }

        /// <summary>
        /// 等待窗体
        /// </summary>
        /// <param name="showSeconds">等待最长秒数</param>
        public PleaseWait(int showSeconds)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Opaque, false);
            ShowSeconds = showSeconds;
        }

        public bool ShowTitle {
            set
            {
                if (value) this.FormBorderStyle = FormBorderStyle.None;

                else this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            } 
        }

        public class Win32
        {
            public const Int32 AW_HOR_POSITIVE = 0x00000001; // 从左到右打开窗口
            public const Int32 AW_HOR_NEGATIVE = 0x00000002; // 从右到左打开窗口
            public const Int32 AW_VER_POSITIVE = 0x00000004; // 从上到下打开窗口
            public const Int32 AW_VER_NEGATIVE = 0x00000008; // 从下到上打开窗口
            public const Int32 AW_CENTER = 0x00000010; //若使用了AW_HIDE标志，则使窗口向内重叠；若未使用AW_HIDE标志，则使窗口向外扩展。
            public const Int32 AW_HIDE = 0x00010000; //隐藏窗口，缺省则显示窗口。
            public const Int32 AW_ACTIVATE = 0x00020000; //激活窗口。在使用了AW_HIDE标志后不要使用这个标志。
            public const Int32 AW_SLIDE = 0x00040000; //使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略。
            public const Int32 AW_BLEND = 0x00080000; //使用淡出效果。只有当hWnd为顶层窗口的时候才可以使用此标志。
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern bool AnimateWindow(
              IntPtr hwnd, // handle to window 
              int dwTime, // duration of animation 
              int dwFlags // animation type 
              );
        }
        /*淡入窗体*/
        private void Form_Load(object sender, EventArgs e)
        {
            if (labelmsg != "")
            {
                this.lblMsg.Text = labelmsg;
            }
            Application.DoEvents();
            Win32.AnimateWindow(this.Handle, 100, Win32.AW_BLEND);

            //label1.Visible = true;
            //progressBar1.Visible = true;
            //progressBar1.Minimum = 0;
            //progressBar1.Maximum = 500;
            //progressBar1.BackColor = Color.Green;
            //for (int i = 0; i < 500; i++)
            //{
            //    progressBar1.Value++;
            //    Application.DoEvents();
            //    this.label1.Text = Convert.ToString(progressBar1.Value);
            //} 
        }
        /*淡出窗体*/
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Win32.AnimateWindow(this.Handle, 1000, Win32.AW_SLIDE | Win32.AW_HIDE | Win32.AW_BLEND);
        }

        

        private void timer_Tick(object sender, EventArgs e)
        {
            if (ShowSeconds < seconds++)
            {
                Win32.AnimateWindow(this.Handle, 1000, Win32.AW_SLIDE | Win32.AW_HIDE | Win32.AW_BLEND);
                seconds = 0;
                timer.Stop();
                this.Visible = false;
            }
        }

        private void PleaseWait_Shown(object sender, EventArgs e)
        {
            timer.Start();
        }

        public void CloseWaiting()
        {
            if (this.InvokeRequired)
            { this.Invoke(new Action(CloseWaiting)); return; }
            this.Close();
        }

        /// <summary>
        /// 显示秒数
        /// </summary>
        /// <param name="seconds"></param>
        public void ShowWaittingInfo(int _seconds)
        {
            timer.Stop();
            seconds = 0;
            ShowSeconds = _seconds;
            this.Show();
            timer.Start();
        }
        /// <summary>
        /// 显示秒数
        /// </summary>
        /// <param name="seconds"></param>
        public void ShowWaittingInfo(int _seconds,string msg)
        {
            timer.Stop();
            seconds = 0;
            ShowSeconds = _seconds;
            lblMsg.Text = msg;          
            this.Show();
            timer.Start();
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public new void Hide()
        {
            this.Show();
            timer.Stop();
            Win32.AnimateWindow(this.Handle, 1000, Win32.AW_SLIDE | Win32.AW_HIDE | Win32.AW_BLEND);
            seconds = 0;

            this.Visible = false;
        }

    }
}
