using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice 
{
    public abstract class BaseMenuEdit
    {
        public WeiXin WeiXinDevice;//微信推送设备 
        public MenuData MenuData;//菜单信息
        public event CallbackUIAction OnCallbackUI_Action;   //----回调UI----
        public CallbackFromUDP callbackGetEditData;//---回调获取指令---- 
        public MySocket mySocket = MySocket.GetInstance();

        public BaseMenuEdit(WeiXin device, MenuData data)
        {
            WeiXinDevice = device;
            MenuData = data;
            callbackGetEditData = new CallbackFromUDP(GetEditData);
        } 

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="values"></param>
        public abstract void GetEditData(UdpData data, object[] values);


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
