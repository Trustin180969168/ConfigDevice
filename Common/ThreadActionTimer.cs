using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Threading;

namespace ConfigDevice
{
    public class ThreadActionTimer
    {
        private System.Timers.Timer myTimer;//----定时器,实现异步线程------
        private Action excute;//----执行内容----
        private int doing = 0;//----用于控制线程按设定的秒数顺序执行---
        private System.Timers.Timer countTimer;//----用于计算执行时间----
        private int seconds = 0;//----执行的秒数-----
        private int limitedSeconds = -1;//----限制执行的秒数----

        public int LimitedSeconds
        {
            get { return limitedSeconds; }
        }

        /// <summary>
        /// 定时执行线程
        /// </summary>
        /// <param name="interval">执行间隔</param>
        /// <param name="action">执行程序</param>
        public ThreadActionTimer(int interval,Action action)
        {
            myTimer = new System.Timers.Timer();
            myTimer.Interval = interval;
            excute = action;
            myTimer.Elapsed += this.Timer_Elaspsed;
            myTimer.Enabled = true;
            myTimer.Stop();

            countTimer = new System.Timers.Timer();
            countTimer.Interval = 1000;
            countTimer.Elapsed += this.TimerCount_Elaspsed;
            countTimer.Enabled = true;
            countTimer.Stop();
        }

        /// <summary>
        /// 定时执行线程
        /// </summary>
        /// <param name="interval">执行间隔</param>
        /// <param name="action">执行程序</param>
        /// <param name="senconds">执行时间</param>
        public ThreadActionTimer(int interval, Action action, int senconds):this(interval,action)
        {
            limitedSeconds = senconds;
        }

        /// <summary>
        /// 按照时间间隔执行
        /// </summary>
        public void Timer_Elaspsed(object sender, ElapsedEventArgs e)
        {
            if (Interlocked.Exchange(ref doing, 1) == 0)
            {
                excute();//---执行-----    
                Interlocked.Exchange(ref doing, 0); 
            }
        }

        /// <summary>
        /// 每秒监听超时
        /// </summary>
        public void TimerCount_Elaspsed(object sender, ElapsedEventArgs e)
        {
            if (limitedSeconds == -1) { seconds++; return; }
            if (++seconds > limitedSeconds)//----超过时间自动停止-----
                Stop();
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        public void Start()
        {
            seconds = 0;
            myTimer.Start();
            countTimer.Start();
        }

        /// <summary>
        /// 关闭执行
        /// </summary>
        public void Stop()
        {
            myTimer.Stop();
            countTimer.Stop();
        }


    }
}
