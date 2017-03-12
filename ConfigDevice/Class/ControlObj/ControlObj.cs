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
        protected string objUuid =  Guid.NewGuid().ToString();
        protected MySocket mySocket = MySocket.GetInstance();//socket通讯对象
        public string  CallbackUuid { get {return deviceControled.EditHandleID + objUuid;} }//---获取编辑的UUID
        /// <summary>
        /// 回调UI
        /// </summary>
        /// <param name="values"></param>
        public void CallbackUI(CallbackParameter callbackParameter)
        {
            if (this.OnCallbackUI_Action != null)
                OnCallbackUI_Action(callbackParameter);
        }

        public ControlObj(Device device)
        {
            deviceControled = device;
        }

         
    }
  
 


}
