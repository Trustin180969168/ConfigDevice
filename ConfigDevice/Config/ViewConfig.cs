using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public static class ViewConfig
    {
        //---逻辑列表-----
        public const string DC_OBJECT = "Object";//触发对象
        public const string DC_OPERATION = "Operation";//运算操作
        public const string DC_START_VALUE = "StartValue";//开始值
        public const string DC_END_VALUE = "EndValue";//结束值
        public const string DC_VALID_TIME = "ValidTime";//有效就
        public const string DC_INVALID_TIME = "InvalidTime";//无效时间 

        //------触发对象------
            public const string TRIGGER_TEMPERATURE = "温度";
            public const string TRIGGER_FLAMMABLE_GAS_PROBE = "可燃气体探头";
            public const string TRIGGER_SYSTEM_INTERACTION = "系统联动号";
            public const string TRIGGER_INVALID = "无效";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
            //public const string TEMPERATURE = "温度";
        
    }

    /// <summary>
    /// 下拉控件
    /// </summary>
    public class GridViewComboBox:DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    {
        public GridViewComboBox():base()
        {
            TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
        }
    }

    /// <summary>
    /// 时间控件
    /// </summary>
    public class GridViewTimeEdit : DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit
    {
        public GridViewTimeEdit()
            : base()
        {
            DisplayFormat.FormatString = "d";
            DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            Mask.EditMask = "HH:mm:ss";
            Mask.UseMaskAsDisplayFormat = true;
            Leave += new System.EventHandler(SysConfig.Edit_Leave);
            Enter += new System.EventHandler(SysConfig.Edit_Enter);
        }
    }

    /// <summary>
    /// 月日类型控件
    /// </summary>
    public class GridViewDateOfMonthDayEdit : DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit
    {
        public GridViewDateOfMonthDayEdit()
            : base()
        {
            DisplayFormat.FormatString = "d";
            DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            Mask.EditMask = "MM-dd";
            Mask.UseMaskAsDisplayFormat = true;
            Leave += new System.EventHandler(SysConfig.Edit_Leave);
            Enter += new System.EventHandler(SysConfig.Edit_Enter);
        }
    }

    /// <summary>
    /// 数字类型控件
    /// </summary>
    public class GridViewDigitalEdit : DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    {
        public GridViewDigitalEdit()
            : base()
        {
            AutoHeight = false;
            Mask.EditMask = "d";
            Leave += new System.EventHandler(SysConfig.Edit_Leave);
            Enter += new System.EventHandler(SysConfig.Edit_Enter);
        }
    }


    /// <summary>
    /// 百分比控件
    /// </summary>
    public class GridViewPercentEdit : DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    {
        public GridViewPercentEdit()
            : base()
        {
            AutoHeight = false;
            Mask.EditMask = "P0";
            Mask.UseMaskAsDisplayFormat = true;
            MaxValue = new decimal(new int[] { 100, 0, 0, 0 });
            MinValue = new decimal(new int[] { 0, 0, 0, 0 });
            Leave += new System.EventHandler(SysConfig.Edit_Leave);
            Enter += new System.EventHandler(SysConfig.Edit_Enter);
        }
    }

    /// <summary>
    /// 文本编辑控件
    /// </summary>
    public class GridViewTextEdit : DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    {
        public GridViewTextEdit()
            : base()
        {
            AutoHeight = false;
        }
    }
    /*
        protected DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit tedtTime;//时间类型编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit tedtTimeOfMD;//日期类型编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit edtNum;//数字编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit edtPercentNum;//百分比


 

            //----时间编辑控件


            //----月日编辑控件


            //----数字编辑控件-----------


            //----百分比编辑控件-------



    */







}
