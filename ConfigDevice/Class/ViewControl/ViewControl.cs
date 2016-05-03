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
    public abstract class  ViewControl
    {
        public ControlObj controlObj;//控制对象

        //----配置界面列表------
        public GridView ViewSetting;
        private DataTable dtSetting;
        public ViewControl(ControlObj _controlObj, GridView gv)
        {
            controlObj = _controlObj;
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
        public abstract void ResetSetting();

        
        /// <summary>
        /// 生成指令数据
        /// </summary>
        /// <returns></returns>
        public abstract CommandData GetCommand();

    }
}
