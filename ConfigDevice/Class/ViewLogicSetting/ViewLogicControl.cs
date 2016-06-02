using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice
{
    public abstract class ViewLogicControl
    {

        // 逻辑关系 ----------------------------------------------------------------------------------------------
        public const int LG_4OR = 0;          //第1种逻辑关系:4个条件[或]
        public const int LG_4AND = 1;          //第2种逻辑关系:4个条件[与]
        public const int LG_3OR_1AND = 2;          //第3种逻辑关系:3个条件[或],再1个条件[与]
        public const int LG_3AND_1OR = 3;          //第4种逻辑关系:3个条件[与],再1个条件[或]
        public const int LG_2OR_2AND_OR = 4;          //第5种逻辑关系:2个条件[或],2个条件[与],再两者[或]
        public const int LG_2OR_2AND_AND = 5;          //第5种逻辑关系:2个条件[或],2个条件[与],再两者[与]
        public const int LG_TOTAL = 6;
        public const int LG_DEFAULT = LG_4OR;


        // 数学关系 ----------------------------------------------------------------------------------------------
        public const int LG_MATH_EQUAL_TO = 0;          //等于(只判断[slSiz1])                                  <- if (val1 == slSiz1)
        public const int LG_MATH_LESS_THAN = 1;          //小于(只判断[slSiz1])                                  <- if (val1 <  slSiz1)
        public const int LG_MATH_GREATER_THAN = 2;          //大于(只判断[slSiz1])                                  <- if (val1 >  slSiz1)
        public const int LG_MATH_WITHIN = 3;          //以内(范围内)(包括两边的数值) (判断[slSiz1]和[slSiz2]) <- if (val1 >= slSiz1) && if (val1 <= slSiz2)
        public const int LG_MATH_WITHOUT = 4;          //以外(范围外)                 (判断[slSiz1]和[slSiz2]) <- if (val1 <  slSiz1) || if (val1 >  slSiz2)
        public const int LG_MATH_EQUAL_TO2 = 5;          //等于(判断[slSiz1]和[slSiz2])                          <- if (val2 == slSiz2)  { if (val1 == slSiz1) }
        public const int LG_MATH_EQUAL_AND_TRUE = 6;          //等于("与"运算后如果为"真") (只判断[slSiz1])           <- if (val1 &  slSiz1)
        public const int LG_MATH_TOTAL = 7;
        public const int LG_MATH_DEFAULT = LG_MATH_EQUAL_TO;


        // 设备[内部条件][外部条件]类型 --------------------------------------------------------------------------
        public const int LG_SENSOR_VOID = 0;          //无效(★不能改动★)
        public const int LG_SENSOR_TEMP = 1;          //内部条件:温度
        public const int LG_SENSOR_HUMI = 2;          //内部条件:湿度
        public const int LG_SENSOR_RAIN = 3;          //内部条件:雨感
        public const int LG_SENSOR_WIND = 4;          //内部条件:风速
        public const int LG_SENSOR_LUMI = 5;          //内部条件:亮度
        public const int LG_SENSOR_LEL = 6;          //内部条件:可燃气探头
        public const int LG_SENSOR_RSP = 7;          //内部条件:雷达
        public const int LG_SENSOR_TAMPER = 8;          //内部条件:防拆开关
        public const int LG_EXT_SENSOR_SYS_LKID = 9;          //外部条件:系统联动
        public const int LG_EXT_SENSOR_SECURITY = 10;         //外部条件:安防联动
        public const int LG_EXT_SENSOR_TIME_SEG = 11;         //外部条件:时间段
        public const int LG_EXT_SENSOR_DATE_SEG = 12;         //外部条件:日期段
        public const int LG_EXT_SENSOR_WEEK_CYC = 13;         //外部条件:周循环
        public const int LG_DEV_SENSOR_TEMP = 14;         //外部条件:温度(外部设备传感器)
        public const int LG_SENSOR_TOTAL = 15;         //总数(★★数量以后会不断增加,必须在最尾处增加★★)
        public const int LG_SENSOR_DEFAULT = LG_SENSOR_VOID;


        //系统联动[ucCmp ]值:           LG_MATH_EQUAL_TO2
        //系统联动[slSiz2]值:           0~LG_LINKAGE_NUM-1
        //系统联动[slSiz1]值
        public const int LG_SYSLKID_ACT_OFF = 0;          //关闭
        public const int LG_SYSLKID_ACT_ON = 1;          //打开
        public const int LG_SYSLKID_ACT_TOTAL = 2;          //总数
        public const int LG_SYSLKID_ACT_DEFAULT = LG_SYSLKID_ACT_ON;


        //安防联动[ucCmp ]值:           LG_MATH_EQUAL_AND_TRUE  → 下面的[slSiz1]值为单选bit ─┐
        //安防联动[slSiz1]值 ←────────────────────────────────┘
        public const int LG_SAF_SYST_DI = 0;          //系统被撤防         (系统全部的安防标志全部被清除)   ──┐
        public const int LG_SAF_SYST_EN_DLY = 1;          //系统进入布防延时中 (系统安防标志只要任一个被置位)       │
        public const int LG_SAF_SYST_EN = 2;          //系统进入布防       (系统安防标志只要任一个被置位)       ├←┐
        public const int LG_SAF_SYST_WAR = 3;          //系统触发预警                                            │  │
        public const int LG_SAF_SYST_ALM = 4;          //系统触发报警                                        ──┘  │补充说明:系统安防都是由[本机/它机]触发的
        public const int LG_SAF_SELF_DI = 5;          //本机被撤防         (本机关联的安防标志全部被清除)   ──┐  ↑
        public const int LG_SAF_SELF_EN_DLY = 6;          //本机进入布防延时中 (本机关联的安防标志只要任一个被置位) │  │
        public const int LG_SAF_SELF_EN = 7;          //本机进入布防       (本机关联的安防标志只要任一个被置位) ├─┘
        public const int LG_SAF_SELF_WAR = 8;          //本机触发预警                                            │
        public const int LG_SAF_SELF_ALM = 9;          //本机触发报警                                        ──┘
        public const int LG_SAF_TOTAL = 10;         //总数
        public const int LG_SAF_DEFAULT = LG_SAF_SELF_ALM;

        public Device deviceTrigger;//触发设备对象
        public GridView gvLogic;//逻辑列表
        public abstract UdpData GetLogicData(int logicID);//获取udp指令
        public abstract void SetLogicData(UdpData udp);//获取逻辑条件

        protected DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxSelectKind;//选择编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit tedtTime;//时间类型编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit tedtTimeOfMD;//日期类型编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit edtNum;//数字编辑
        protected DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit edtPercentNum;//百分比

        public ViewLogicControl(Device _device, GridView gv)
        {
            this.deviceTrigger = _device;
            this.gvLogic = gv;
            //----命令编辑控件
            cbxSelectKind = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cbxSelectKind.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbxSelectKind.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

            //----时间编辑控件
            tedtTime = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            tedtTime.DisplayFormat.FormatString = "d";
            tedtTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            tedtTime.Mask.EditMask = "HH:mm:ss";
            tedtTime.Mask.UseMaskAsDisplayFormat = true;
            tedtTime.Leave += new System.EventHandler(SysConfig.Edit_Leave);
            tedtTime.Enter += new System.EventHandler(SysConfig.Edit_Enter);

            //----月日编辑控件
            tedtTimeOfMD = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            tedtTimeOfMD.DisplayFormat.FormatString = "d";
            tedtTimeOfMD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            tedtTimeOfMD.Mask.EditMask = "MM-dd";
            tedtTimeOfMD.Mask.UseMaskAsDisplayFormat = true;
            tedtTimeOfMD.Leave += new System.EventHandler(SysConfig.Edit_Leave);
            tedtTimeOfMD.Enter += new System.EventHandler(SysConfig.Edit_Enter);

            //----数字编辑控件-----------
            edtNum = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            edtNum.AutoHeight = false;
            edtNum.Mask.EditMask = "d";
            edtNum.Name = "edtNum";
            edtNum.Leave += new System.EventHandler(SysConfig.Edit_Leave);
            edtNum.Enter += new System.EventHandler(SysConfig.Edit_Enter);

            //----百分比编辑控件-------
            edtPercentNum = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            edtPercentNum.AutoHeight = false;
            edtPercentNum.Mask.EditMask = "P0";
            edtPercentNum.Mask.UseMaskAsDisplayFormat = true;
            edtPercentNum.MaxValue = new decimal(new int[] { 100, 0, 0, 0 });
            edtPercentNum.MinValue = new decimal(new int[] { 0, 0, 0, 0 });
            edtPercentNum.Name = "edtPercentNum";
            edtPercentNum.Leave += new System.EventHandler(SysConfig.Edit_Leave);
            edtPercentNum.Enter += new System.EventHandler(SysConfig.Edit_Enter);
        }
    }
}