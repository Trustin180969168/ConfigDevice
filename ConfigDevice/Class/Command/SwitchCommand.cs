using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice
{
    /// <summary>
    /// 开关指令
    /// </summary>
    public class SwitchCommand : ViewCommand
    {
        public SwitchCommand(GridView gv)
            : base(gv)
        {

        }

        public override byte[] CreateCommand()
        {
            return null;
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        public override void InitViewSetting()
        {

        }



    }
}
