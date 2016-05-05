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
        
        //----配置界面列表------
        public GridView ViewSetting;
        private DataTable dtSetting;
        public ViewCommandControl(ControlObj _controlObj, GridView gv)
        {
            this.cbxCommandKind = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxCommandKind.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbxCommandKind.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

            this.tedtTime = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.edtNum = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.edtNum.AutoHeight = false;
            this.edtNum.Mask.EditMask = "d";
            this.edtNum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.edtNum.Name = "edtNum";            

            controlObj = _controlObj;
            dtSetting = new DataTable();
            ViewSetting = gv;
            ViewSetting.GridControl.DataSource = dtSetting;
            InitViewSetting();
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

    }
}
