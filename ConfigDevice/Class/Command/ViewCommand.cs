using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;

namespace ConfigDevice
{
    /// <summary>
    /// 回路指令
    /// </summary>
    public abstract class  ViewCommand : CommandData
    {

        public byte TargetId;//目标ID
        public byte TargetNet;//目标网段
        public byte TargetType;//目标类型

        public byte[] Cmd = new byte[2];//命令
        public byte Len;//长度
        public byte[] Data = new byte[30];//数据最长30字节

        //----配置界面列表------
        public GridView ViewSetting;
        private DataTable dtSetting;
        public ViewCommand(string name, GridView gv)
            : base(name)
        {
            dtSetting = new DataTable(); 
            ViewSetting = gv;
            ViewSetting.GridControl.DataSource = dtSetting;
            InitViewSetting();
        }

        /// <summary>
        /// 初始化配置界面
        /// </summary>
        public abstract void InitViewSetting();

        /// <summary>
        /// 重置
        /// </summary>
        public void ResetSetting()
        {
            ViewSetting.GridControl.DataSource = dtSetting;
            ViewSetting.Columns.Clear();
        }

    }
}
