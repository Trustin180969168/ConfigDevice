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
    /// 场景指令
    /// </summary>
    public class ViewSceneControl : BaseViewCommandControl
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
            dcCommand.Visible = true;
            dcSceneNum.Visible = true;
            dcGroup.Visible = true;
            dcRunTime.Visible = true;    
            ViewSetting.Columns.ColumnByName("parameter4").Visible = false;
            ViewSetting.Columns.ColumnByName("parameter5").Visible = false;
            ViewSetting.Columns.ColumnByName("parameter1").VisibleIndex = 6;
            ViewSetting.Columns.ColumnByName("parameter2").VisibleIndex = 7;
            ViewSetting.Columns.ColumnByName("parameter3").VisibleIndex = 8;

            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE);
            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE_OPEN);
            cbxCommandKind.Items.Add(Scene.NAME_CMD_SW_SWIT_SCENE_CLOSE);

            dcSceneNum.Caption = DeviceConfig.CONTROL_OBJECT_SCENE_NAME;
            dcSceneNum.ColumnEdit = edtNum;

            dcGroup.Caption = "分组";
            dcGroup.ColumnEdit = edtNum;
            dcRunTime.Caption = "运行时间";
            dcRunTime.ColumnEdit = tedtTime;

            ViewSetting.SetRowCellValue(0, dcCommand, cbxCommandKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcSceneNum, "1");
            ViewSetting.SetRowCellValue(0, dcGroup, "1");
            ViewSetting.SetRowCellValue(0, dcRunTime, "00:00:00");

            //ViewSetting.BestFitColumns();
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
            ViewSetting.PostEditor();
            DataRow dr = ViewSetting.GetDataRow(0);
            byte[] sceneCommand = Scene.NameAndCommand[dr[dcCommand.FieldName].ToString()];//-----回路命令-----------------            
            int groupNum = Convert.ToInt16(dr[dcGroup.FieldName].ToString());//-----分组----           
            int sceneNum = Convert.ToInt16(dr[dcSceneNum.FieldName]); //----------场景-----------------        
            //----------计算时间-------------------
            DateTime dtRunTime = DateTime.Parse(dr[dcRunTime.FieldName].ToString()); 

            int runTimeSeconds = dtRunTime.Hour * 60 * 60 + dtRunTime.Minute * 60 + dtRunTime.Second;//运行秒数
        
            if (runTimeSeconds > 64800)
            { CommonTools.MessageShow("运行时间不能大于18小时!", 2, ""); return null; } 

            CommandData result = scene.GetCommandData(sceneCommand, groupNum, sceneNum, runTimeSeconds);
            result.NetworkIP = dr[DeviceConfig.DC_NETWORK_IP].ToString();
            result.PCAddress = dr[DeviceConfig.DC_PC_ADDRESS].ToString();
            return result;
        }


        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data">指令数据</param>
        public override void SetCommandData(CommandData data)
        {
            string cmdName = "";
            foreach (string key in Scene.NameAndCommand.Keys)
            {
                if (CommonTools.BytesEuqals(data.Cmd, Scene.NameAndCommand[key]))
                { cmdName = key; break; }
            }
            ViewSetting.SetRowCellValue(0, dcCommand, cmdName);//---命令名称---

            int groupIndex = (int)data.Data[2];
            int sceneIndex = (int)data.Data[3];
            //ViewSetting.SetRowCellValue(0, dcCircuit, cbxCircuitNum.Items[cmdIndex].ToString());//--回路----
            ViewSetting.SetRowCellValue(0, dcGroup, groupIndex);//--分组----
            ViewSetting.SetRowCellValue(0, dcSceneNum, sceneIndex == 0 ? 1 : sceneIndex);//---场景----

            byte[] byteRunTime = CommonTools.CopyBytes(data.Data, 4, 2);  
            int runTime = ConvertTools.Bytes2ToInt16(byteRunTime); 

            string nowDateStr = DateTime.Now.ToShortDateString();
            DataTable dt = ViewSetting.GridControl.DataSource as DataTable;
            DataRow dr = dt.Rows[0];

            dr[dcRunTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(runTime).ToLongTimeString();//----运行时间---
       
        }
    }
}
