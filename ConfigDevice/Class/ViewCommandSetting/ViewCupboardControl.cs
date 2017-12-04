using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace ConfigDevice
{
    /// <summary>
    /// 升降电柜指令
    /// </summary>
    public class ViewCupboardControl : BaseViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcAction;//动作
        GridColumn dcFloors;//层数
        GridColumn dcRunDelay;//运行延时 
        CupboardSwit cupboardSwit;//控制对象
        RepositoryItemComboBox cbxActionKind;//选择命令类型编辑     

        public ViewCupboardControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            cupboardSwit = controlObj as CupboardSwit;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcAction = ViewSetting.Columns.ColumnByName("parameter1");
            dcFloors = ViewSetting.Columns.ColumnByName("parameter2");
            dcRunDelay = ViewSetting.Columns.ColumnByName("parameter3");

            cbxActionKind = new RepositoryItemComboBox();
            cbxActionKind.TextEditStyle = TextEditStyles.DisableTextEditor;

            InitViewSetting();
        }

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            dcCommand.Visible = true;
            dcAction.Visible = true;
            dcFloors.Visible = true;
            dcRunDelay.Visible = true;
            ViewSetting.Columns.ColumnByName("parameter1").VisibleIndex = 6;
            ViewSetting.Columns.ColumnByName("parameter2").VisibleIndex = 7;
            ViewSetting.Columns.ColumnByName("parameter3").VisibleIndex = 8;

            cbxCommandKind.Items.Add(CupboardSwit.NAME_CMD_SWIT_LOOP);
            cbxCommandKind.Items.Add(CupboardSwit.NAME_CMD_SWIT_LOOP_OPEN);
            cbxCommandKind.Items.Add(CupboardSwit.NAME_CMD_SWIT_LOOP_CLOSE);
            cbxCommandKind.Items.Add(CupboardSwit.NAME_CMD_SWIT_LOOP_OPEN_CONDITION);
            cbxCommandKind.Items.Add(CupboardSwit.NAME_CMD_SWIT_LOOP_CLOSE_CONDITION);

            dcAction.Caption = "升降柜动作";
            cbxActionKind.Items.Add(CupboardSwit.ACTION_OPEN_CUPBOARD);
            cbxActionKind.Items.Add(CupboardSwit.ACTION_CLOSE_CUPBOARD);
            cbxActionKind.SelectedIndexChanged += this.cbxActionKind_SelectedIndexChanged;
            dcAction.ColumnEdit = cbxActionKind;

            dcFloors.Caption = "层数";
            dcFloors.ColumnEdit = edtNum;
            dcRunDelay.Caption = "运行延迟";
            dcRunDelay.ColumnEdit = tedtTime;

            ViewSetting.SetRowCellValue(0, dcCommand, cbxCommandKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcAction, cbxActionKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcFloors, "0");
            ViewSetting.SetRowCellValue(0, dcRunDelay, "00:00:00");


        }

        /// <summary>
        /// 时间校验
        /// </summary>
        protected override void time_Leave(object sender, EventArgs e)
        {
            //----------计算时间-------------------
            DataRow dr = ViewSetting.GetDataRow(0);
            DateTime dtRunDelay = DateTime.Parse(dr[dcRunDelay.FieldName].ToString());

            int runTimeSeconds = dtRunDelay.Hour * 60 * 60 + dtRunDelay.Minute * 60 + dtRunDelay.Second;//运行秒数

            if (runTimeSeconds > 64800)
                CommonTools.MessageShow("运行时间不能大于18小时!", 2, "");

        }

        /// <summary>
        /// 生成指令数据
        /// </summary>
        /// <returns></returns>
        public override CommandData GetCommand()
        {
            CommandData result = new CommandData();
            ViewSetting.PostEditor();
            DataRow dr = ViewSetting.GetDataRow(0);
            byte[] cupboardCommand = CupboardSwit.NameAndCommand[dr[dcCommand.FieldName].ToString()];//-----电机命令-----------------            
            int floors = Convert.ToInt16(dr[dcFloors.FieldName].ToString());//-----层数---- 
            //----------计算时间-------------------
            DateTime dtRunDelay = DateTime.Parse(dr[dcRunDelay.FieldName].ToString());
            int RunDelaySeconds = dtRunDelay.Hour * 60 * 60 + dtRunDelay.Minute * 60 + dtRunDelay.Second;//运行秒数
            if (RunDelaySeconds > 64800)
            { CommonTools.MessageShow("运行时间不能大于18小时!", 2, ""); return null; }
            string actionName = "";
            //---1.运行/停止----
            if (CommonTools.BytesEuqals(cupboardCommand, DeviceConfig.CMD_SW_SWIT_LOOP))
            {
                if (actionName == CupboardSwit.NAME_ACTION_OPEN_CUPBOARD)
                    result = cupboardSwit.ActionRunStopOpen(floors, RunDelaySeconds);
                else
                    result = cupboardSwit.ActionRunStopClose();
            }
            //---2.运行----
            else if (CommonTools.BytesEuqals(cupboardCommand, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN))
            {
                if (actionName == CupboardSwit.NAME_ACTION_OPEN_CUPBOARD)
                    result = cupboardSwit.ActionOpenCmdOpenCupboard(floors, RunDelaySeconds);
                else
                    result = cupboardSwit.ActionOpenCmdCloseCupboard();
            }
            //---3.停止----
            else if (CommonTools.BytesEuqals(cupboardCommand, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE))
            {
                result = cupboardSwit.ActionStopCmdOpenCupboard();
            }
            //---4.条件运行----
            else if (CommonTools.BytesEuqals(cupboardCommand, DeviceConfig.CMD_SW_SWIT_LOOP_OPEN_CONDITION))
            {
                if (actionName == CupboardSwit.NAME_ACTION_OPEN_CUPBOARD)
                    result = cupboardSwit.ActionConditionOpenCmdOpenCupboard(floors, RunDelaySeconds);
                else
                    result = cupboardSwit.ActionConditionOpenCmdCloseCupboard();
            }
            //---5.条件停止----
            else if (CommonTools.BytesEuqals(cupboardCommand, DeviceConfig.CMD_SW_SWIT_LOOP_CLOSE_CONDITION))
            {
                result = cupboardSwit.ActionStopCmdOpenCupboard();
            }
            result.NetworkIP = dr[DeviceConfig.DC_NETWORK_IP].ToString();
            result.PCAddress = dr[DeviceConfig.DC_PC_ADDRESS].ToString();
            return result;

        }

        /// <summary>
        /// 根据选择开柜,关柜,设置列
        /// </summary>
        private void cbxActionKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewSetting.PostEditor();
            DataRow dr = ViewSetting.GetDataRow(0);
            string name = dr[this.dcAction.FieldName].ToString();
            switch (name)
            {
                case CupboardSwit.NAME_ACTION_OPEN_CUPBOARD:
                    setGridColumnValid(dcFloors, edtNum);
                    setGridColumnValid(dcRunDelay, tedtTime);
                    //----自动初始化------
                    this.ViewSetting.SetRowCellValue(0, dcFloors, 0);//---开始值---
                    this.ViewSetting.SetRowCellValue(0, dcRunDelay, "00:00:00");//---开始值---
                    break;
                case CupboardSwit.NAME_ACTION_CLOSE_CUPBOARD:
                    setGridColumnInvalid(dcFloors);
                    setGridColumnInvalid(dcRunDelay);
                    break;
                default: break;
            }
            dr.EndEdit();
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
            //---找出对应的指令---------
            string cmdName = "";
            foreach (string key in CupboardSwit.NameAndCommand.Keys)
            {
                if (CommonTools.BytesEuqals(data.Cmd, CupboardSwit.NameAndCommand[key]))
                { cmdName = key; break; }
            }
            ViewSetting.SetRowCellValue(0, dcCommand, cmdName);//---命令名称---

            int actionIndex = (int)data.Data[2];//---动作---
            if (actionIndex == 1)//关柜
                ViewSetting.SetRowCellValue(0, dcAction, CupboardSwit.NAME_ACTION_CLOSE_CUPBOARD);
            else if (actionIndex == 2)//开柜
                ViewSetting.SetRowCellValue(0, dcAction, CupboardSwit.NAME_ACTION_OPEN_CUPBOARD);
            else
                ViewSetting.SetRowCellValue(0, dcAction, "");
 
            byte[] byteOpenDelayTime = CommonTools.CopyBytes(data.Data, 5, 2);  
            int openDelayTime = ConvertTools.Bytes2ToInt16(byteOpenDelayTime); 

            string nowDateStr = DateTime.Now.ToShortDateString();
            DataTable dt = ViewSetting.GridControl.DataSource as DataTable;
            DataRow dr = dt.Rows[0];
            dr[dcRunDelay.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(openDelayTime).ToLongTimeString();

        }





    }
}
