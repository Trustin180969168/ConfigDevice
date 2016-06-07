
using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;

namespace ConfigDevice
{
    /// <summary>
    /// 背景指令
    /// </summary>
    public class ViewServerControl : BaseViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcEmailContent;//Email内容
        ServerControlObj server;//消息

        public ViewServerControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            server = controlObj as ServerControlObj;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcEmailContent = ViewSetting.Columns.ColumnByName("parameter1");
            
            InitViewSetting();
        }

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            dcCommand.Visible = true;
            dcEmailContent.Visible = true;
            ViewSetting.Columns.ColumnByName("parameter1").VisibleIndex = 6;
            cbxCommandKind.Items.Add(ServerControlObj.NAME_CMD_SEND_WEIXIN);

            dcEmailContent.Caption = "Email内容";
            ViewSetting.SetRowCellValue(0, dcCommand, cbxCommandKind.Items[0].ToString());
 

           // ViewSetting.BestFitColumns();
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
            ViewSetting.PostEditor();
            DataRow dr = ViewSetting.GetDataRow(0);
            byte[]  Command = ServerControlObj.NameAndCommand[dr[dcCommand.FieldName].ToString()];//-----命令-----------------     
            return server.GetCommandData( Command,dr[dcEmailContent.FieldName].ToString() );
        }


        /// <summary>
        /// 设置命令数据
        /// </summary>
        /// <param name="data"></param>
        public override void SetCommandData(CommandData data)
        {
            //---找出对应的指令---------
            string cmdName = "";
            foreach (string key in ServerControlObj.NameAndCommand.Keys)
            {
                if (CommonTools.BytesEuqals(data.Cmd, ServerControlObj.NameAndCommand[key]))
                { cmdName = key; break; }
            }
            ViewSetting.SetRowCellValue(0, dcCommand, cmdName);//---命令名称---
            ViewSetting.SetRowCellValue(0, dcEmailContent, Encoding.Unicode.GetString(data.Data));//---Email内容----

        }
    }
}
