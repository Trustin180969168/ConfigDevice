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
            cbxCommandKind.Items.Add(Swit.NAME_CMD_SW_SWIT_ALL_CLOSE);
            cbxCommandKind.Items.Add(Swit.NAME_CMD_SW_SWIT_ALL_OPEN);

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
        /// 时间校验
        /// </summary>
        protected override void timeTest_Leave(object sender, EventArgs e)
        {
            //----------计算时间-------------------
            DataRow dr = ViewSetting.GetDataRow(0);
            DateTime dtRunTime = DateTime.Parse(dr[dcRunTime.FieldName].ToString());
            int runTimeSeconds = dtRunTime.Hour * 60 * 60 + dtRunTime.Minute * 60 + dtRunTime.Second;//运行秒数
            if (runTimeSeconds > 64800)
                CommonTools.MessageShow("运行时间不能大于18小时!", 2, "");
        }

        /// <summary>
        /// 生成指令数据
        /// </summary>
        /// <returns></returns>
        public override CommandData GetCommand()
        {
            ViewSetting.PostEditor();
            DataRow dr = ViewSetting.GetDataRow(0);
            byte[] switCommand = Swit.NameAndCommand[dr[dcCommand.FieldName].ToString()];//-----开关全部命令-------
            //----------计算时间-------------------
            DateTime dtRunTime = DateTime.Parse(dr[dcRunTime.FieldName].ToString());
            int runTimeSeconds = dtRunTime.Hour * 60 * 60 + dtRunTime.Minute * 60 + dtRunTime.Second;//运行秒数
            if (runTimeSeconds > 64800)
            { CommonTools.MessageShow("运行时间不能大于18小时!", 2, ""); return null; }
            return swit.GetCommandData(switCommand, runTimeSeconds);
        }


        /// <summary>
        /// 获取命令数据
        /// </summary>
        /// <param name="data"></param>
        public override void SetCommandData(CommandData data)
        {
            //---找出对应的指令---------
            string cmdName = "";
            foreach (string key in Swit.NameAndCommand.Keys)
            {
                if (CommonTools.BytesEuqals(data.Cmd, Swit.NameAndCommand[key]))
                { cmdName = key; break; }
            }
            ViewSetting.SetRowCellValue(0, dcCommand, cmdName);//---命令名称---
            byte[] byteRunTime = CommonTools.CopyBytes(data.Data,2, 2);
            int runTime = ConvertTools.Bytes2ToInt(byteRunTime);//---运行时间--
            string nowDateStr = DateTime.Now.ToShortDateString();
            DataTable dt = ViewSetting.GridControl.DataSource as DataTable;
            DataRow dr = dt.Rows[0];

            dr[dcRunTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(runTime).ToLongTimeString();
        }

    }
}
