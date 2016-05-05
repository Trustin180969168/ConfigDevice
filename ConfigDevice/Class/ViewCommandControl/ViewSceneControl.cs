using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ConfigDevice
{
    /// <summary>
    /// 场景指令
    /// </summary>
    public class ViewSceneControl : ViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcSceneNum;//场景
        GridColumn dcGroup;//分组
        GridColumn dcRunTime;//运行时间
        Scene scene;//场景
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxActionKind;//选择命令类型编辑     

        public ViewSceneControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            scene = controlObj as Scene;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcSceneNum = ViewSetting.Columns.ColumnByName("parameter1");
            dcGroup = ViewSetting.Columns.ColumnByName("parameter2");
            dcRunTime = ViewSetting.Columns.ColumnByName("parameter3");

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
            dcSceneNum.Visible = true;
            dcGroup.Visible = true;
            dcRunTime.Visible = true;
            ViewSetting.Columns.ColumnByName("parameter4").Visible = false;
            ViewSetting.Columns.ColumnByName("parameter5").Visible = false;

            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE);
            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE_OPEN);
            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE_CLOSE);
            dcCommand.ColumnEdit = cbxCommandKind;

            dcSceneNum.Name = "场景";
            cbxActionKind.Items.Add("全部");
            cbxActionKind.Items.Add("1");
            cbxActionKind.Items.Add("2");
            cbxActionKind.Items.Add("3");
            cbxActionKind.Items.Add("4");
            cbxActionKind.Items.Add("5");
            cbxActionKind.Items.Add("6");
            cbxActionKind.Items.Add("7");
            cbxActionKind.Items.Add("8");
            cbxActionKind.Items.Add("无效");
            dcSceneNum.ColumnEdit = cbxActionKind;

            dcGroup.Name = "分组";
            dcGroup.ColumnEdit = edtNum;
            dcRunTime.Name = "运行时间";
            dcRunTime.ColumnEdit = tedtTime;

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
