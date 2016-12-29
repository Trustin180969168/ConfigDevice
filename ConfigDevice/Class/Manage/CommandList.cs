using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public abstract class CommandList
    {
        protected MySocket mySocket = MySocket.GetInstance();
        public Device device;//-----设备---
        public event CallbackUIAction OnCallbackUI_Action;   //----回调UI----
        public CallbackFromUDP callbackGetCommandData;      //---回调获取指令----
        protected string objUuid = Guid.NewGuid().ToString();//唯一标识对象uuid
        

        public  CommandList(Device value)
        {
            this.device = value;
            callbackGetCommandData = new CallbackFromUDP(GetCommandData);
        }
        /// <summary>
        /// 回调UI
        /// </summary>
        /// <param name="values"></param>
        public void CallbackUI(CallbackParameter callbackParameter)
        {
            if (this.OnCallbackUI_Action != null)
                OnCallbackUI_Action(callbackParameter);
        }

        /// <summary>
        /// 获取指令数据
        /// </summary>
        public abstract void ReadCommandData(CommandReadObj data, int startNum, int endNum);

        /// <summary>
        /// 获取指令
        /// </summary>
        /// <param name="data">UDP数据</param>
        /// <param name="values">参数列表</param>
        public abstract void GetCommandData(UdpData data, object[] values);

        /// <summary>
        /// 保存指令
        /// </summary>
        /// <param name="commandData">指令内容</param>
        public abstract void SaveCommandData(CommandReadObj readObj, int index, CommandData commandData);


        /// <summary>
        /// 删除指令
        /// </summary>
        /// <param name="data">指令对象</param>
        /// <param name="startIndex">开始</param>
        /// <param name="endIndex">结束</param>
        public abstract void DelCommandData(CommandReadObj data, int startIndex, int endIndex);


        /// <summary>
        /// 测试指令
        /// </summary>
        /// <param name="data">指令对象</param>
        public abstract void TestCommands(CommandReadObj data);






    }
}
