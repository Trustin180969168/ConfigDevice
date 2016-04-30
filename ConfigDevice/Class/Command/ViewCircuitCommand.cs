using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice
{
    /// <summary>
    /// 回路指令
    /// </summary>
    public class ViewCircuitCommand : ViewCommand
    {
        public ViewCircuitCommand(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            commandObj = new CircuitCommand(CircuitCommand.CommandKindList[0]);
        }

        /// <summary>
        /// 命令字节
        /// </summary>
        /// <returns></returns>
        public override byte[] CreateCommand()
        {
            return null;
        }

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {

        }

        /// <summary>
        /// 重置
        /// </summary>
        public override void ResetSetting()
        {
            
        }


    }
}
