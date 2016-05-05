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
        Motor motor;//控制对象
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxActionKind;//选择命令类型编辑     

        public ViewMotorControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            motor = controlObj as Motor;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcMotorAction = ViewSetting.Columns.ColumnByName("parameter1");
            dcPercent = ViewSetting.Columns.ColumnByName("parameter2");
            dcRunTime = ViewSetting.Columns.ColumnByName("parameter3");
            dcOpenDelay = ViewSetting.Columns.ColumnByName("parameter4");
            dcCloseDelay = ViewSetting.Columns.ColumnByName("parameter5");

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
            dcMotorAction.Visible = true;
            dcPercent.Visible = true;
            dcRunTime.Visible = true;
            dcOpenDelay.Visible = true;
            dcCloseDelay.Visible = true;

            cbxCommandKind.Items.Add(Motor.NAME_CMD_SWIT_LOOP);
            cbxCommandKind.Items.Add(Motor.NAME_CMD_SWIT_LOOP_OPEN);
            cbxCommandKind.Items.Add(Motor.NAME_CMD_SWIT_LOOP_CLOSE);
            cbxCommandKind.Items.Add(Motor.NAME_CMD_SWIT_LOOP_OPEN_CONDITION);
            cbxCommandKind.Items.Add(Motor.NAME_CMD_SWIT_LOOP_CLOSE_CONDITION);
            dcCommand.ColumnEdit = cbxCommandKind;

            dcMotorAction.Caption = "电机动作";
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_FRONT_1);
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_BACK_1);
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_FRONT_2);
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_BACK_2);
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_FRONT_3);
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_BACK_3);
            dcMotorAction.ColumnEdit = cbxActionKind;

            dcPercent.Caption = "程度";
            dcPercent.ColumnEdit = edtNum;
            dcRunTime.Caption = "运行时间";
            dcRunTime.ColumnEdit = tedtTime;
            dcOpenDelay.Caption = "开延迟";
            dcOpenDelay.ColumnEdit = tedtTime;
            dcCloseDelay.Caption = "关延迟";
            dcCloseDelay.ColumnEdit = tedtTime;

            ViewSetting.BestFitColumns();

        }

        /// <summary>
        /// 生成指令数据
        /// </summary>
        /// <returns></returns>
        public override CommandData GetCommand()
        {
            //string actionStr = (string)ViewSetting.GetRowCellValue(0, dcMotorAction);
            //MotorAction action = MotorAction.Road1Front;
            //switch (actionStr)
            //{
            //    case Motor.NAME_ACTION_ROAD_FRONT_1: action = MotorAction.Road1Front; break;
            //    case Motor.NAME_ACTION_ROAD_BACK_1: action = MotorAction.Road1Back; break;
            //    case Motor.NAME_ACTION_ROAD_FRONT_2: action = MotorAction.Road2Front; break;
            //    case Motor.NAME_ACTION_ROAD_BACK_2: action = MotorAction.Road2Back; break;
            //    case Motor.NAME_ACTION_ROAD_FRONT_3: action = MotorAction.Road3Front; break;
            //    case Motor.NAME_ACTION_ROAD_BACK_3: action = MotorAction.Road3Back; break;
            //    default: action = MotorAction.Road1Front; break;
            //}

            //string cmdStr =(string)ViewSetting.GetRowCellValue(0, dcCommand);

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
