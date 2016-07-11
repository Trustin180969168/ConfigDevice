using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public abstract class ControlObj
    {
        public string Name = "";//控制对象名称    
        public Device deviceControled;//被控制设备
        public event CallbackUIAction OnCallbackUI_Action;   //----回调UI----
        public string UUID = Guid.NewGuid().ToString();//--对象唯一UUID-----

        /// <summary>
        /// 回调UI
        /// </summary>
        /// <param name="values"></param>
        public void CallbackUI(CallbackParameter callbackParameter)
        {
            if (this.OnCallbackUI_Action != null)
                OnCallbackUI_Action(callbackParameter);
        }



         
    }
  


}
