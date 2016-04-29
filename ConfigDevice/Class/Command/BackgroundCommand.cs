using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice
{
    /// <summary>
    /// 背景指令
    /// </summary>
    public class BackgroundCommand : ViewCommand
    {
        public BackgroundCommand(GridView gv)
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
