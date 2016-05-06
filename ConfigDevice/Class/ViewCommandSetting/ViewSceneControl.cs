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
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxSceneNum;//选择命令类型编辑     

        public ViewSceneControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            scene = controlObj as Scene;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcSceneNum = ViewSetting.Columns.ColumnByName("parameter1");
            dcGroup = ViewSetting.Columns.ColumnByName("parameter2");
            dcRunTime = ViewSetting.Columns.ColumnByName("parameter3");

            cbxSceneNum = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxSceneNum.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbxSceneNum.DropDownRows = 16;

            InitViewSetting();
        } 

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            dcSceneNum.Visible = true;
            dcGroup.Visible = true;
            dcRunTime.Visible = true;
            ViewSetting.Columns.ColumnByName("parameter4").Visible = false;
            ViewSetting.Columns.ColumnByName("parameter5").Visible = false;

            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE);
            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE_OPEN);
            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE_CLOSE);

            dcSceneNum.Caption = "场景";
            cbxSceneNum.Items.Add("全部");
            cbxSceneNum.Items.Add("1");
            cbxSceneNum.Items.Add("2");
            cbxSceneNum.Items.Add("3");
            cbxSceneNum.Items.Add("4");
            cbxSceneNum.Items.Add("5");
            cbxSceneNum.Items.Add("6");
            cbxSceneNum.Items.Add("7");
            cbxSceneNum.Items.Add("8");
            cbxSceneNum.Items.Add("无效");
            dcSceneNum.ColumnEdit = cbxSceneNum;

            dcGroup.Caption = "分组";
            dcGroup.ColumnEdit = edtNum;
            dcRunTime.Caption = "运行时间";
            dcRunTime.ColumnEdit = tedtTime;

            ViewSetting.SetRowCellValue(0, dcCommand, cbxCommandKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcSceneNum, cbxSceneNum.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcGroup, "1");
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


    }
}
