using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Reflection;

namespace ConfigDevice
{
   
    /// <summary>
    /// 回路指令
    /// </summary>
    public class ViewCircuitControl : ViewCommandControl
    {

        GridColumn dcCommand;//指令
        GridColumn dcCircuit;//回路
        GridColumn dcPercent;//亮度
        GridColumn dcRunTime;//运行时间
        GridColumn dcOpenDelay;//开延迟
        GridColumn dcCloseDelay;//关延迟
        Circuit circuit;//回路
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxCircuitNum;//选择回路编辑   

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

            cbxCircuitNum = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxCircuitNum.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            InitViewSetting();
        } 

        /// <summary>
        /// 初始化界面配置
        /// </summary>
        public override void InitViewSetting()
        {
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

            cbxCommandKind.Items.Add(Circuit.NAME_CMD_SWITLOOP);
            cbxCommandKind.Items.Add(Circuit.NAME_CMD_SWITLOOP_OPEN);
            cbxCommandKind.Items.Add(Circuit.NAME_CMD_SWITLOOP_CLOSE);

            dcCircuit.Caption = "回路";      
            Type type = controlObj.deviceControled.GetType(); //获取类型
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("CircuitCount"); //获取指定名称的属性
            int count = (int)propertyInfo.GetValue(controlObj.deviceControled, null); //获取属性值
            for (int i = 1; i <= count; i++)
                cbxCircuitNum.Items.Add(i.ToString());
            dcCircuit.ColumnEdit = cbxCircuitNum;

            dcPercent.Caption = "亮度";
            dcPercent.ColumnEdit = edtPercentNum;
            dcRunTime.Caption = "运行时间";
            dcRunTime.ColumnEdit = tedtTime;
            dcOpenDelay.Caption = "开延迟";
            dcOpenDelay.ColumnEdit = tedtTime;
            dcCloseDelay.Caption = "关延迟";
            dcCloseDelay.ColumnEdit = tedtTime;

            ViewSetting.SetRowCellValue(0, dcCommand, cbxCommandKind.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcCircuit, cbxCircuitNum.Items[0].ToString());
            ViewSetting.SetRowCellValue(0, dcPercent, "0");
            ViewSetting.SetRowCellValue(0, dcRunTime, "00:00:00");
            ViewSetting.SetRowCellValue(0, dcOpenDelay, "00:00:00");
            ViewSetting.SetRowCellValue(0, dcCloseDelay, "00:00:00");

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
