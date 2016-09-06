using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class ViewSecurity : UserControl
    {
        private SecurityObj securityObj;//-----安防控制对象-----

        public ViewSecurity()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化安防
        /// </summary>
        public void InitViewSecurity(SecurityObj obj)
        {
            securityObj = obj;
            securityObj.OnCallbackUI_Action += this.CallbackUI;
        }

        /// <summary>
        /// 显示安防配置
        /// </summary>
        public void ShowSecuritySetting()
        {
            //------安防配置---------------
            for (int i = 0; i < securityObj.SaftFlags.Length; i++)
                ceSafeSetting.Items[i].CheckState = securityObj.SaftFlags[i] ? CheckState.Checked : CheckState.Unchecked;
        }

        /// <summary>
        /// 获取安防配置
        /// </summary>
        /// <returns></returns>
        public UInt16 GetSecuritySetting()
        {
            //---安防配置---------------
            bool[] safeFlags = new bool[] { false, false, false, false, false, false, false, false, false, false,
                    false, false, false, false, false };
            for (int i = 0; i < securityObj.SaftFlags.Length; i++)
                safeFlags[i] = ceSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
            securityObj.SaftFlags = safeFlags;
            return securityObj.Security;
        }

        /// <summary>
        /// 保存安防配置
        /// </summary>
        /// <param name="groupNum"></param>
        public void SaveSecurity(int groupNum)
        {
            if(hasChangedSafeLogic())
                securityObj.SaveSafeSetting(groupNum);
        }


        /// <summary>
        /// 是否有修改安防配置
        /// </summary>
        /// <returns></returns>
        private bool hasChangedSafeLogic()
        {
            for (int i = 0; i < securityObj.SaftFlags.Length; i++)
            {
                if (ceSafeSetting.Items[i].CheckState == CheckState.Checked && !securityObj.SaftFlags[i])
                    return true;
                if (ceSafeSetting.Items[i].CheckState == CheckState.Unchecked && securityObj.SaftFlags[i])
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 读取安防配置
        /// </summary>
        public void ReadSecurity(int groupNum)
        {
            securityObj.ReadSafeSetting(groupNum);
        } 
          
        /// <summary>
        /// 回调
        /// </summary>
        private void CallbackUI(CallbackParameter callbackParameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(CallbackUI), callbackParameter);
                return;
            }
            else
            {
                //------安防配置---------------
                for (int i = 0; i < securityObj.SaftFlags.Length; i++)
                    ceSafeSetting.Items[i].CheckState = securityObj.SaftFlags[i] ? CheckState.Checked : CheckState.Unchecked;
            }
        }

    }
}
