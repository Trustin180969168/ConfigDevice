using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ConfigDevice
{
    /// <summary>
    /// 时序指令
    /// </summary>
    public class ViewTimingControl : ViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcTimeNum;//时序序号
        GridColumn dcGroup;//分组
        GridColumn dcRunCount;//运行次数
        Timing time;//时序
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxActionKind;//选择命令类型编辑     

        public ViewTimingControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            time = controlObj as Timing;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcTimeNum = ViewSetting.Columns.ColumnByName("parameter1");
            dcGroup = ViewSetting.Columns.ColumnByName("parameter2");
            dcRunCount = ViewSetting.Columns.ColumnByName("parameter3");

            cbxActionKind = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxActionKind.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            InitViewSetting();
        } 

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            dcTimeNum.Visible = true;
            dcGroup.Visible = true;
            dcRunCount.Visible = true;
            ViewSetting.Columns.ColumnByName("parameter4").Visible = false;
            ViewSetting.Columns.ColumnByName("parameter5").Visible = false;

            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE);
            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE_OPEN);
            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE_CLOSE);
            dcCommand.ColumnEdit = cbxCommandKind;

            dcTimeNum.Name = "时序";
            dcTimeNum.ColumnEdit = edtNum;
            dcGroup.Name = "分组";
            dcGroup.ColumnEdit = edtNum;
            dcRunCount.Name = "运行时间";
            dcRunCount.ColumnEdit = edtNum;
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
