using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class ViewLogicSetting : UserControl
    {
        public bool NeedInit = true;
        public LogicList logicList;//---逻辑配置----

        public ViewLogicSetting()
        {
            InitializeComponent();
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic4OR);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic4AND);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic3OR_1AND);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic3AND_1OR);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic2OR_2AND_OR);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic2OR_2AND_AND);
         
            NeedInit = true;
        }

        /// <summary>
        /// 初始化逻辑列表
        /// </summary>
        /// <param name="triggers"></param>
        public void InitLogicList(Device device,params string[] triggers)
        {
            foreach (Control viewlogic in plLogicList.Controls)
                (viewlogic as ViewLogicTools).InitTriggerList(triggers);
            logicList = new LogicList(device);
            logicList.OnCallbackUI_Action += this.returnLogicData;//命令的执行的界面回调
            NeedInit = false;
        }

        /// <summary>
        /// 获取指令数据
        /// </summary>
        /// <param name="udpResult"></param>
        /// <param name="values"></param>
        private void returnLogicData(CallbackParameter parameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(returnLogicData), parameter);
                return;
            }
 
 
 
        }

    }
}
