
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
    public class ViewServerControl : ViewCommandControl
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
            dcEmailContent.ColumnEdit = meEdit;

            InitViewSetting();
        } 

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            dcCommand.Visible = true;
            cbxCommandKind.Items.Add(ServerControlObj.NAME_CMD_SEND_WEIXIN);
            dcEmailContent.Caption = "Email内容";
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
            foreach (string key in Motor.NameAndCommand.Keys)
            {
                if (CommonTools.BytesEuqals(data.Cmd, Motor.NameAndCommand[key]))
                { cmdName = key; break; }
            }
            ViewSetting.SetRowCellValue(0, dcCommand, cmdName);//---命令名称---
            ViewSetting.SetRowCellValue(0, dcEmailContent, Encoding.Unicode.GetString(data.Data));//---Email内容----

        }
    }
}
