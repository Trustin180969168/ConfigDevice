﻿using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice
{
    /// <summary>
    /// 回路指令
    /// </summary>
    public class CircuitCommand : ViewCommand
    {
        public CircuitCommand(string name, GridView gv)
            : base(name, gv)
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
