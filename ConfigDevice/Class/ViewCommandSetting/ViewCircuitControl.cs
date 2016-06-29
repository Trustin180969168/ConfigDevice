using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Reflection;
using System.Data;
using System.Threading;

namespace ConfigDevice
{
   
    /// <summary>
    /// 回路指令
    /// </summary>
    public class ViewCircuitControl : BaseViewCommandControl
    {
        GridColumn dcCommand;//指令
        GridColumn dcCircuit;//回路
        GridColumn dcPercent;//亮度
        GridColumn dcRunTime;//运行时间
        GridColumn dcOpenDelay;//开延迟
        GridColumn dcCloseDelay;//关延迟
        Circuit circuit;//回路
        DataTable dtCircuit = new DataTable("回路列表选择");
        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookupEdit;//回路选择
        public ViewCircuitControl(ControlObj controlObj, GridView gv)
            : base(controlObj, gv)
        {
            circuit = controlObj as Circuit;
            dcCommand = ViewSetting.Columns.ColumnByName("command");
            dcCircuit = ViewSetting.Columns.ColumnByName("parameter1");
            dcPercent = ViewSetting.Columns.ColumnByName("parameter2");
            dcRunTime = ViewSetting.Columns.ColumnByName("parameter3");
            dcOpenDelay = ViewSetting.Columns.ColumnByName("parameter4");
            dcCloseDelay = ViewSetting.Columns.ColumnByName("parameter5");

            //-----初始化回路选择----
            dtCircuit.Columns.Add(DeviceConfig.DC_ID, System.Type.GetType("System.String"));
            dtCircuit.Columns.Add(DeviceConfig.DC_NAME, System.Type.GetType("System.String"));
            foreach (int key in circuit.ListCircuitIDAndName.Keys)
                dtCircuit.Rows.Add(key, circuit.ListCircuitIDAndName[key]);
            circuit.OnCallbackUI_Action += this.CallBackUI;
            circuit.ReadRoadTitle();//----获取回路数据-----

            //------初始化回路列表选择-------
            lookupEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lookupEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DeviceConfig.DC_ID, "回路", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DeviceConfig.DC_NAME, "回路名称", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None)});
            lookupEdit.Name = "lookupEdit";
            lookupEdit.DisplayMember = DeviceConfig.DC_ID;
            lookupEdit.ValueMember = DeviceConfig.DC_ID;
            lookupEdit.ShowFooter = false;
            lookupEdit.ShowHeader = false;        
            
            InitViewSetting();
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
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
            dcCommand.Visible = true;
            dcCircuit.Visible = true;
            dcPercent.Visible = true;
            dcRunTime.Visible = true;
            dcOpenDelay.Visible = true;
            dcCloseDelay.Visible = true;

            ViewSetting.Columns.ColumnByName("parameter1").VisibleIndex = 6;
            ViewSetting.Columns.ColumnByName("parameter2").VisibleIndex = 7;
            ViewSetting.Columns.ColumnByName("parameter3").VisibleIndex = 8;
            ViewSetting.Columns.ColumnByName("parameter4").VisibleIndex = 9;
            ViewSetting.Columns.ColumnByName("parameter5").VisibleIndex = 10;

            cbxCommandKind.Items.Add(Circuit.NAME_CMD_SWIT_LOOP);
            cbxCommandKind.Items.Add(Circuit.NAME_CMD_SWIT_LOOP_OPEN);
            cbxCommandKind.Items.Add(Circuit.NAME_CMD_SWIT_LOOP_CLOSE);

            dcCircuit.Caption = "回路";      
            Type type = controlObj.deviceControled.GetType(); //获取类型
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("CircuitCount"); //获取指定名称的属性
            lookupEdit.DataSource = dtCircuit;
          //  Thread.Sleep(500);//等待回路数据接收
            dcCircuit.ColumnEdit = lookupEdit;
            

            dcPercent.Caption = "亮度";
            dcPercent.ColumnEdit = edtPercentNum;
            dcRunTime.Caption = "运行时间";
            dcRunTime.ColumnEdit = tedtTime;
            dcOpenDelay.Caption = "开延迟";
            dcOpenDelay.ColumnEdit = tedtTime;
            dcCloseDelay.Caption = "关延迟";
            dcCloseDelay.ColumnEdit = tedtTime;

            ViewSetting.SetRowCellValue(0, dcCommand, cbxCommandKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcCircuit, "1");
            ViewSetting.SetRowCellValue(0, dcPercent, "0");
            ViewSetting.SetRowCellValue(0, dcRunTime, "00:00:00");
            ViewSetting.SetRowCellValue(0, dcOpenDelay, "00:00:00");
            ViewSetting.SetRowCellValue(0, dcCloseDelay, "00:00:00");

           // ViewSetting.BestFitColumns();

        }

        /// <summary>
        /// 读取后返回结果
        /// </summary>
        /// <param name="values"></param>
        public void CallBackUI(CallbackParameter callbackParameter)
        {
            try
            {
                dtCircuit.Rows.Clear();
                foreach (int key in circuit.ListCircuitIDAndName.Keys)
                    dtCircuit.Rows.Add(key, circuit.ListCircuitIDAndName[key]);
                dtCircuit.AcceptChanges();
                lookupEdit.DropDownRows = dtCircuit.Rows.Count;
                lookupEdit.DataSource = dtCircuit;
                lookupEdit.BestFit();
                dcCircuit.ColumnEdit = lookupEdit;             
                circuit.OnCallbackUI_Action -= this.CallBackUI;
            }
            catch (Exception e1) { e1.ToString(); }
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
            byte[] circuitCommand = Circuit.NameAndCommand[dr[dcCommand.FieldName].ToString()];//-----回路命令-----------------            
            int percent = Convert.ToInt16(dr[dcPercent.FieldName].ToString());//-----亮度----           
            int circuitIndex =Convert.ToInt16(dr[dcCircuit.FieldName])-1; //----------回路-----------------        
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
            
            CommandData result = circuit.GetCommandData(circuitCommand, percent, circuitIndex, runTimeSeconds, openDelaySeconds, closeDelaySeconds);
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
            foreach (string key in Circuit.NameAndCommand.Keys)
            {
                if (CommonTools.BytesEuqals(data.Cmd, Circuit.NameAndCommand[key]))
                { cmdName = key; break; }
            }
            ViewSetting.SetRowCellValue(0, dcCommand, cmdName);//---命令名称---

            int cmdIndex = (int)data.Data[2];
            //ViewSetting.SetRowCellValue(0, dcCircuit, cbxCircuitNum.Items[cmdIndex].ToString());//--回路----
            ViewSetting.SetRowCellValue(0, dcCircuit, cmdIndex+1);//--回路----
            ViewSetting.SetRowCellValue(0, dcPercent, (int)data.Data[1]);//---亮度----

            byte[] byteRunTime = CommonTools.CopyBytes(data.Data, 3, 2);
            byte[] byteOpenDelayTime = CommonTools.CopyBytes(data.Data, 5, 2);
            byte[] byteCloseDelayTime = CommonTools.CopyBytes(data.Data, 7, 2);

            int runTime = ConvertTools.Bytes2ToInt16(byteRunTime);
            int openDelayTime = ConvertTools.Bytes2ToInt16(byteOpenDelayTime);
            int closeDelayTime = ConvertTools.Bytes2ToInt16(byteCloseDelayTime);  

            string nowDateStr = DateTime.Now.ToShortDateString();
            DataTable dt = ViewSetting.GridControl.DataSource as DataTable;
            DataRow dr = dt.Rows[0];

            dr[dcRunTime.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(runTime).ToLongTimeString();//----运行时间---
            dr[dcOpenDelay.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(openDelayTime).ToLongTimeString();//----开延迟---
            dr[dcCloseDelay.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(closeDelayTime).ToLongTimeString();//----关延迟---

        
        }



    }
}
