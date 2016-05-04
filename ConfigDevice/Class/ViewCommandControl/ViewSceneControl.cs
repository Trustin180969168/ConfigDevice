using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice
{
    /// <summary>
    /// 场景指令
    /// </summary>
    public class ViewSceneControl : ViewCommandControl
    {
        public ViewSceneControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            
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

        /// <summary>
        /// 生成指令数据
        /// </summary>
        /// <returns></returns>
        public override CommandData GetCommand()
        {
            return null;
        }


    }
}
