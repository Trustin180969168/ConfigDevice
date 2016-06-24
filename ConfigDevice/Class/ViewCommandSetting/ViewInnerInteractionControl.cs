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
    /// 内部联动号
    /// </summary>
    public class ViewInnerInteractionControl : BaseViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcInteractiveNum;//联动号
        GridColumn dcCircuitNum;//回路
        GridColumn dcRunTime;//运行时间
        GridColumn dcOpenDelay;//开延迟
        GridColumn dcCloseDelay;//关延迟
        InnerInteraction inner;//控制对象

        public ViewInnerInteractionControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            inner = controlObj as InnerInteraction;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcInteractiveNum = ViewSetting.Columns.ColumnByName("parameter1");
            dcCircuitNum = ViewSetting.Columns.ColumnByName("parameter2");
            dcRunTime = ViewSetting.Columns.ColumnByName("parameter3");
            dcOpenDelay = ViewSetting.Columns.ColumnByName("parameter4");
            dcCloseDelay = ViewSetting.Columns.ColumnByName("parameter5");
                   
            edtNum.DisplayFormat.FormatString = "##0 号";
            edtNum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            edtNum.Mask.EditMask = "##0 号";
            edtNum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edtNum.Mask.UseMaskAsDisplayFormat = true;
            edtNum.MinValue = 0;
            edtNum.MaxValue = (int)DeviceConfig.SpecicalID.ID_PKGNUM_PUBLIC - 1;
            dcInteractiveNum.ColumnEdit = edtNum;

            InitViewSetting();
        }

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            dcCommand.Visible = true;
            dcInteractiveNum.Visible = true;
            dcCircuitNum.Visible = false;
            dcRunTime.Visible = true;
            dcOpenDelay.Visible = true;
            dcCloseDelay.Visible = true;
            ViewSetting.Columns.ColumnByName("parameter1").VisibleIndex = 6;
            ViewSetting.Columns.ColumnByName("parameter3").VisibleIndex = 7;
            ViewSetting.Columns.ColumnByName("parameter4").VisibleIndex = 8;
            ViewSetting.Columns.ColumnByName("parameter5").VisibleIndex = 9;

            cbxCommandKind.Items.Add(InnerInteraction.NAME_CMD_LOGIC_WRITE_SLFLKID);
            cbxCommandKind.Items.Add(InnerInteraction.NAME_CMD_LOGIC_WRITE_SLFLKID_OPEN); 
            cbxCommandKind.Items.Add(InnerInteraction.NAME_CMD_LOGIC_WRITE_SLFLKID_CLOSE);

            dcInteractiveNum.Caption = "联动号";
            dcCircuitNum.Caption = "回路";
            dcCircuitNum.ColumnEdit = edtNum;
            dcRunTime.Caption = "运行时间";
            dcRunTime.ColumnEdit = tedtTime;
            dcOpenDelay.Caption = "开延迟";
            dcOpenDelay.ColumnEdit = tedtTime;
            dcCloseDelay.Caption = "关延迟";
            dcCloseDelay.ColumnEdit = tedtTime;
 
            ViewSetting.SetRowCellValue(0, dcInteractiveNum,"0"); 
            ViewSetting.SetRowCellValue(0, dcRunTime, "00:00:00");
            ViewSetting.SetRowCellValue(0, dcOpenDelay, "00:00:00");
            ViewSetting.SetRowCellValue(0, dcCloseDelay, "00:00:00");  

        }

        /// <summary>
        /// 时间校验
        /// </summary>
        protected override void time_Leave(object sender, EventArgs e)
        {
            //----------计算时间-------------------
            DataRow dr = ViewSetting.GetDataRow(0);
            DateTime dtRunTime = DateTime.Parse(dr[dcRunTime.FieldName].ToString());
            DateTime dtOpenDelay = DateTime.Parse(dr[dcOpenDelay.FieldName].ToString());
            DateTime dtCloseDelay = DateTime.Parse(dr[dcCloseDelay.FieldName].ToString());

            int runTimeSeconds = dtRunTime.Hour * 60 * 60 + dtRunTime.Minute * 60 + dtRunTime.Second;//运行秒数
            int openDelaySeconds = dtOpenDelay.Hour * 60 * 60 + dtOpenDelay.Minute * 60 + dtOpenDelay.Second;//开延迟秒数
            int closeDelaySeconds = dtCloseDelay.Hour * 60 * 60 + dtCloseDelay.Minute * 60 + dtCloseDelay.Second;//关延迟秒数

            if (runTimeSeconds > 64800)
                CommonTools.MessageShow("运行时间不能大于18小时!", 2, "");
            if (openDelaySeconds > 64800)
                CommonTools.MessageShow("开延迟不能大于18小时!", 2, "");
            if (closeDelaySeconds > 64800)
                CommonTools.MessageShow("关延迟不能大于18小时!", 2, "");
        }

        /// <summary>
        /// 生成指令数据
        /// </summary>
        /// <returns></returns>
        public override CommandData GetCommand()
        {
            ViewSetting.PostEditor();
            DataRow dr = ViewSetting.GetDataRow(0);
            byte[] Command = InnerInteraction.NameAndCommand[dr[dcCommand.FieldName].ToString()];//----命令------- 
            int actionIndex = 0;
            if(dr[dcCommand.FieldName].ToString() == InnerInteraction.NAME_CMD_LOGIC_WRITE_SLFLKID_OPEN)
                actionIndex = 1;//-----动作,开关,开,关,只有开为非零-----       
            int interactionNum = Convert.ToInt16(dr[dcInteractiveNum.FieldName]);//----系统联动号------- 
            //----------计算时间-------------------
            DateTime dtRunTime = DateTime.Parse(dr[dcRunTime.FieldName].ToString());
            DateTime dtOpenDelay = DateTime.Parse(dr[dcOpenDelay.FieldName].ToString());
            DateTime dtCloseDelay = DateTime.Parse(dr[dcCloseDelay.FieldName].ToString());

            int runTimeSeconds = dtRunTime.Hour * 60 * 60 + dtRunTime.Minute * 60 + dtRunTime.Second;//运行秒数
            int openDelaySeconds = dtOpenDelay.Hour * 60 * 60 + dtOpenDelay.Minute * 60 + dtOpenDelay.Second;//开延迟秒数
            int closeDelaySeconds = dtCloseDelay.Hour * 60 * 60 + dtCloseDelay.Minute * 60 + dtCloseDelay.Second;//关延迟秒数

            if (runTimeSeconds > 64800)
            { CommonTools.MessageShow("运行时间不能大于18小时!", 2, ""); return null; }
            if (openDelaySeconds > 64800)
            { CommonTools.MessageShow("开延迟不能大于18小时!", 2, ""); return null; }
            if (closeDelaySeconds > 64800)
            { CommonTools.MessageShow("关延迟不能大于18小时!", 2, ""); return null; }

            return inner.GetCommandData(Command, actionIndex,interactionNum, runTimeSeconds, openDelaySeconds, closeDelaySeconds);
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
            foreach (string key in InnerInteraction.NameAndCommand.Keys)
            {
                if (CommonTools.BytesEuqals(data.Cmd, InnerInteraction.NameAndCommand[key]))
                { cmdName = key; break; }
            }
            ViewSetting.SetRowCellValue(0, dcCommand, cmdName);//---命令名称---          
            ViewSetting.SetRowCellValue(0, dcInteractiveNum,(int)data.Data[1]);//---系统联动号,Data[2]为回路号,不用显示---
   
            byte[] byteRunTime = CommonTools.CopyBytes(data.Data, 3, 2);
            byte[] byteOpenDelayTime = CommonTools.CopyBytes(data.Data, 5, 2);
            byte[] byteCloseDelayTime = CommonTools.CopyBytes(data.Data, 7, 2);

            int runTime = ConvertTools.Bytes2ToInt16(byteRunTime);
            int openDelayTime = ConvertTools.Bytes2ToInt16(byteOpenDelayTime);
            int closeDelayTime = ConvertTools.Bytes2ToInt16(byteCloseDelayTime);

            string nowDateStr = DateTime.Now.ToShortDateString();
            DataTable dt = ViewSetting.GridControl.DataSource as DataTable;
            DataRow dr = dt.Rows[0];

            dr[dcRunTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(runTime).ToLongTimeString();
            dr[dcOpenDelay.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(openDelayTime).ToLongTimeString();
            dr[dcCloseDelay.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(closeDelayTime).ToLongTimeString();


        }





    }
}
