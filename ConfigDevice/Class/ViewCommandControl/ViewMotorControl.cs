using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using DevExpress.XtraGrid.Columns;

namespace ConfigDevice
{
    /// <summary>
    /// 电机指令
    /// </summary>
    public class ViewMotorControl : ViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcMotorAction;//动作
        GridColumn dcPercent;//程度
        GridColumn dcRunTime;//运行时间
        GridColumn dcOpenDelay;//开延迟
        GridColumn dcCloseDelay;//关延迟

        protected DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxActionKind;//选择命令类型编辑     

        public ViewMotorControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            dcCommand = ViewSetting.Columns["command"];
            dcMotorAction = ViewSetting.Columns["parameter1"];
            dcPercent = ViewSetting.Columns["parameter2"];
            dcRunTime = ViewSetting.Columns["parameter3"];
            dcOpenDelay = ViewSetting.Columns["parameter4"];
            dcCloseDelay = ViewSetting.Columns["parameter5"];

            cbxActionKind = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxActionKind.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            dcCommand.Visible = true;
            dcMotorAction.Visible = true;
            dcPercent.Visible = true;
            dcRunTime.Visible = true;
            dcOpenDelay.Visible = true;
            dcCloseDelay.Visible = true;

            cbxCommandKind.Items.Add("开关电机");
            cbxCommandKind.Items.Add("开电机");
            cbxCommandKind.Items.Add("关电机");
            cbxCommandKind.Items.Add("开执行");
            cbxCommandKind.Items.Add("关执行");
            dcCommand.ColumnEdit = cbxCommandKind;

            dcMotorAction.Name = "电机动作";
            cbxActionKind.Items.Add("1路正转");
            cbxActionKind.Items.Add("1路反转");
            cbxActionKind.Items.Add("2路正转");
            cbxActionKind.Items.Add("2路反转");
            cbxActionKind.Items.Add("3路正转");
            cbxActionKind.Items.Add("3路反转");
            dcMotorAction.ColumnEdit = cbxActionKind; 

            dcPercent.Name = "程度";
            dcPercent.ColumnEdit = edtNum;
            dcRunTime.Name = "运行时间";
            dcRunTime.ColumnEdit = tedtTime;
            dcOpenDelay.Name = "开延迟";
            dcOpenDelay.ColumnEdit = tedtTime;
            dcCloseDelay.Name = "关延迟";
            dcCloseDelay.ColumnEdit = tedtTime;

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
        /// 重置
        /// </summary>
        public override void ResetSetting()
        { 
        }
    }
}
