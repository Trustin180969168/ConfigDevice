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

        }

        /// <summary>
        /// 显示安防配置
        /// </summary>
        public void ShowSecuritySetting()
        {
            //------安防配置---------------
            for (int i = 0; i < securityObj.SaftFlags.Length; i++)
                ceLeaveSafeSetting.Items[i].CheckState = securityObj.SaftFlags[i] ? CheckState.Checked : CheckState.Unchecked;

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
                safeFlags[i] = ceLeaveSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
            securityObj.SaftFlags = safeFlags;
            return securityObj.Security;
        }

    }
}
