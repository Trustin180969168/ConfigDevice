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
            dcMotorAction.Visible = true;
            dcPercent.Visible = true;
            dcRunTime.Visible = true;
            dcOpenDelay.Visible = true;
            dcCloseDelay.Visible = true;
            ViewSetting.Columns.ColumnByName("parameter1").VisibleIndex = 6;
            ViewSetting.Columns.ColumnByName("parameter2").VisibleIndex = 7;
            ViewSetting.Columns.ColumnByName("parameter3").VisibleIndex = 8;
            ViewSetting.Columns.ColumnByName("parameter4").VisibleIndex = 9;
            ViewSetting.Columns.ColumnByName("parameter5").VisibleIndex = 10;

            cbxCommandKind.Items.Add(Motor.NAME_CMD_SWIT_LOOP);
            cbxCommandKind.Items.Add(Motor.NAME_CMD_SWIT_LOOP_OPEN);
            cbxCommandKind.Items.Add(Motor.NAME_CMD_SWIT_LOOP_CLOSE);
            cbxCommandKind.Items.Add(Motor.NAME_CMD_SWIT_LOOP_OPEN_CONDITION);
            cbxCommandKind.Items.Add(Motor.NAME_CMD_SWIT_LOOP_CLOSE_CONDITION);

            dcMotorAction.Caption = "电机动作";
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_FRONT_1);
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_BACK_1);
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_FRONT_2);
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_BACK_2);
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_FRONT_3);
            cbxActionKind.Items.Add(Motor.NAME_ACTION_ROAD_BACK_3);
            dcMotorAction.ColumnEdit = cbxActionKind;

        //            public static readonly byte[] CMD_SW_SWIT_LOOP = new byte[] { 0x20, CMD_TYPE_SWITCH };//回路开关
        //public static readonly byte[] CMD_SW_SWIT_LOOP_OPEN = new byte[] { 0x21, CMD_TYPE_SWITCH };//回路开
        //public static readonly byte[] CMD_SW_SWIT_LOOP_CLOSE = new byte[] { 0x22, CMD_TYPE_SWITCH };//回路关
        //public static readonly byte[] CMD_SW_SWIT_LOOP_NOT = new byte[] { 0x23, CMD_TYPE_SWITCH };//回路取反
        //public static readonly byte[] CMD_SW_SWIT_LOOP_OPEN_CONDITION = new byte[] { 0x30, CMD_TYPE_SWITCH };//回路带条件开
        //public static readonly byte[] CMD_SW_SWIT_LOOP_CLOSE_CONDITION = new byte[] { 0x31, CMD_TYPE_SWITCH };//回路带条件关


            dcPercent.Caption = "程度";
            dcPercent.ColumnEdit = edtPercentNum;
            dcRunTime.Caption = "运行时间";
            dcRunTime.ColumnEdit = tedtTime;
            dcOpenDelay.Caption = "开延迟";
            dcOpenDelay.ColumnEdit = tedtTime;
            dcCloseDelay.Caption = "关延迟";
            dcCloseDelay.ColumnEdit = tedtTime;

            ViewSetting.SetRowCellValue(0, dcCommand, cbxCommandKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcMotorAction, cbxActionKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcPercent, "1");
            ViewSetting.SetRowCellValue(0, dcRunTime, "00:00:00");
            ViewSetting.SetRowCellValue(0, dcOpenDelay, "00:00:00");
            ViewSetting.SetRowCellValue(0, dcCloseDelay, "00:00:00");

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

        /// <summary>
        /// 设置指令数据
        /// </summary>
        /// <param name="data"></param>
        public override void SetCommandData(CommandData data)
        {
            
            int cmdIndex = (int)data.Data[0];
            ViewSetting.SetRowCellValue(0, dcMotorAction, cbxActionKind.Items[cmdIndex].ToString());//---电机动作---
            ViewSetting.SetRowCellValue(0, dcPercent, (int)data.Data[1]);//---程度----

            byte[] byteRunTime = CommonTools.CopyBytes(data.Data, 3, 2);
            byte[] byteOpenDelayTime = CommonTools.CopyBytes(data.Data, 5, 2);
            byte[] byteCloseDelayTime = CommonTools.CopyBytes(data.Data, 7, 2);
            ViewSetting.SetRowCellValue(0, dcRunTime, BitConverter.ToInt16(byteRunTime,0));//---运行时间----   
            ViewSetting.SetRowCellValue(0, dcOpenDelay, BitConverter.ToInt16(byteOpenDelayTime,0));//---开延时时间---- 
            ViewSetting.SetRowCellValue(0, dcRunTime, BitConverter.ToInt16(byteCloseDelayTime,0));//---关延时间---- 
        
        }

    }
}
