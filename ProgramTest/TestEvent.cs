using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramTest
{
    //定义一委托类型  
    public delegate void MyButtonEventHandler(string msg);

    public class MyButton
    {
        //定义一委托实例（事件）  
        public event MyButtonEventHandler Click;
        private string msg;

        /// <summary>  
        /// 用于触发Click事件(模拟点击按钮)  
        /// </summary>  
        public void OnClick()
        {
            if (Click != null)
            {
                Click("我点击了：" + msg);
            }
        }

        //构造函数  
        public MyButton(string msg)
        {
            this.msg = msg;
        }
    }  
}
