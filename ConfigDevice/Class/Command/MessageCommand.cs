﻿using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice
{
    /// <summary>
    /// 消息指令
    /// </summary>
    public class MessageCommand : ViewCommand
    {
        public MessageCommand(string name, GridView gv)
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
