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
            dcCommand = ViewSetting.Columns["command"];
            dcCircuit = ViewSetting.Columns["parameter1"];
            dcPercent = ViewSetting.Columns["parameter2"];
            dcRunTime = ViewSetting.Columns["parameter3"];
            dcOpenDelay = ViewSetting.Columns["parameter4"];
            dcCloseDelay = ViewSetting.Columns["parameter5"];

            cbxCircuitNum = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxCircuitNum.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
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

            cbxCommandKind.Items.Add(Circuit.NAME_CMD_SWITLOOP);
            cbxCommandKind.Items.Add(Circuit.NAME_CMD_SWITLOOP_OPEN);
            cbxCommandKind.Items.Add(Circuit.NAME_CMD_SWITLOOP_CLOSE);
            dcCommand.ColumnEdit = cbxCommandKind;


            dcCircuit.Name = "回路";      
            Type type = controlObj.deviceControled.GetType(); //获取类型
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("CircuitCount"); //获取指定名称的属性
            int count = (int)propertyInfo.GetValue(controlObj.deviceControled, null); //获取属性值
            for (int i = 1; i <= count; i++)
                cbxCircuitNum.Items.Add(i.ToString());
            dcCircuit.ColumnEdit = cbxCircuitNum;


            dcPercent.Name = "亮度";
            dcPercent.ColumnEdit = edtNum;            
            dcRunTime.Name = "运行时间";
            dcRunTime.ColumnEdit = tedtTime;
            dcOpenDelay.Name = "开延迟";
            dcOpenDelay.ColumnEdit = tedtTime;
            dcCloseDelay.Name = "关延迟";
            dcCloseDelay.ColumnEdit = tedtTime;
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
