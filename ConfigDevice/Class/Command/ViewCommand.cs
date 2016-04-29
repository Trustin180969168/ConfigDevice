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
        //----配置界面列表------
        public GridView ViewSetting;
        private DataTable dtSetting;
        public ViewCommand(GridView gv)
            : base()
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



    }
}
