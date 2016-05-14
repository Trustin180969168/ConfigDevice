using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ConfigDevice
{
    /// <summary>
    /// 开关指令
    /// </summary>
    public class ViewSwitControl : ViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcRunTime;//运行时间
        Swit swit;//开关
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxActionKind;//选择命令类型编辑     

        public ViewSwitControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            swit = controlObj as Swit;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcRunTime = ViewSetting.Columns.ColumnByName("parameter1");

            cbxActionKind = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxActionKind.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            InitViewSetting();
        } 

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            dcCommand.Visible = true;
            dcRunTime.Visible = true;
            ViewSetting.Columns.ColumnByName("parameter2").Visible = false;
            ViewSetting.Columns.ColumnByName("parameter3").Visible = false;
            ViewSetting.Columns.ColumnByName("parameter4").Visible = false;
            ViewSetting.Columns.ColumnByName("parameter5").Visible = false;
            
            cbxCommandKind.Items.Add(Swit.NAME_CMD_SW_SWIT_ALL);
            cbxCommandKind.Items.Add(Swit.CMD_SW_SWIT_ALL_OPEN);
            cbxCommandKind.Items.Add(Swit.CMD_SW_SWIT_ALL_CLOSE);

            dcRunTime.Caption = "运行时间";
            dcRunTime.ColumnEdit = tedtTime;

            ViewSetting.SetRowCellValue(0, dcCommand, cbxCommandKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcRunTime, "00:00:00");

            ViewSetting.BestFitColumns();
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
        /// 获取命令数据
        /// </summary>
        /// <param name="data"></param>
        public override void SetCommandData(CommandData data)
        {
           
        }
    }
}
