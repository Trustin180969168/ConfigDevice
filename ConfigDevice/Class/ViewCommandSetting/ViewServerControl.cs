
using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ConfigDevice
{
    /// <summary>
    /// 背景指令
    /// </summary>
    public class ViewServerControl : ViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcWeiXinContent;//微信内容
        ServerControlObj server;//消息

        public ViewServerControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            server = controlObj as ServerControlObj;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcWeiXinContent = ViewSetting.Columns.ColumnByName("parameter1");

            InitViewSetting();
        } 

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            cbxCommandKind.Items.Add(ServerControlObj.NAME_CMD_SEND_WEIXIN);
            dcWeiXinContent.Caption = "微信内容";
            ViewSetting.Columns.ColumnByName("parameter2").Visible = false;
            ViewSetting.Columns.ColumnByName("parameter3").Visible = false;
            ViewSetting.Columns.ColumnByName("parameter4").Visible = false;
            ViewSetting.Columns.ColumnByName("parameter5").Visible = false;
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


        /// <summary>
        /// 设置命令数据
        /// </summary>
        /// <param name="data"></param>
        public override void SetCommandData(CommandData data)
        {
           
        }
    }
}
