using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;

namespace ConfigDevice
{
    /// <summary>
    /// 回路指令
    /// </summary>
    public abstract class  ViewCommandControl
    {
        public ControlObj controlObj;//控制对象
        protected DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxCommandKind;//选择命令类型编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit tedtTime;//时间类型编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edtNum;//数字编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit edtPercentNum;//百分比
        
        //----配置界面列表------
        public GridView ViewSetting;
        public ViewCommandControl(ControlObj _controlObj, GridView gv)
        {    

            //----命令编辑控件
            this.cbxCommandKind = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxCommandKind.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbxCommandKind.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
      
            //----时间编辑控件
            tedtTime = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            tedtTime.DisplayFormat.FormatString = "d";
            tedtTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            tedtTime.Mask.EditMask = "HH:mm:ss";
            tedtTime.Mask.UseMaskAsDisplayFormat = true;
            tedtTime.Leave += new System.EventHandler(this.timeTest_Leave);

            //----数字编辑控件-----------
            edtNum = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            edtNum.AutoHeight = false;
            edtNum.Mask.EditMask = "d";
            edtNum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edtNum.Name = "edtNum";

            //----百分比编辑控件-------
            edtPercentNum = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            edtPercentNum.AutoHeight = false;
            edtPercentNum.Mask.EditMask = "P0";
            edtPercentNum.Mask.UseMaskAsDisplayFormat = true;
            edtPercentNum.MaxValue = new decimal(new int[] { 100, 0, 0, 0 });
            edtPercentNum.MinValue = new decimal(new int[] { 0, 0, 0, 0 });
            edtPercentNum.Name = "edtPercentNum";

            controlObj = _controlObj;
            ViewSetting = gv;
            ViewSetting.Columns.ColumnByName("command").ColumnEdit = cbxCommandKind;
        }

        /// <summary>
        /// 时间校验
        /// </summary>
        protected virtual void timeTest_Leave(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 初始化配置界面
        /// </summary>
        public abstract void InitViewSetting();

        /// <summary>
        /// 重置
        /// </summary>
        public abstract void ResetSetting();

        
        /// <summary>
        /// 生成指令数据
        /// </summary>
        /// <returns></returns>
        public abstract CommandData GetCommand();

        /// <summary>
        /// 配置指令数据
        /// </summary>
        /// <param name="data">指令数据</param>
        public abstract void SetCommandData(CommandData data);

    }
}
