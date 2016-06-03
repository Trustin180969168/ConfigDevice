using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;
using System.Drawing;

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
        public const int LG_SENSOR_TEMP = 1;          //内部条件:温度   (含有外设,含有等级)
        public const int LG_SENSOR_HUMI = 2;          //内部条件:湿度   (含有外设,含有等级)
        public const int LG_SENSOR_RAIN = 3;          //内部条件:雨感   (含有外设         )
        public const int LG_SENSOR_WIND = 4;          //内部条件:风速   (含有外设,含有等级)
        public const int LG_SENSOR_LUMI = 5;          //内部条件:亮度   (含有外设,含有等级)
        public const int LG_SENSOR_LEL = 6;          //内部条件:可燃气探头  (含有外设         )
        public const int LG_SENSOR_RSP = 7;          //内部条件:雷达    (含有外设         )
        public const int LG_SENSOR_TAMPER = 8;          //内部条件:防拆开关 (含有外设         )
        public const int LG_EXT_SENSOR_SYS_LKID = 9;          //外部条件:系统联动
        public const int LG_EXT_SENSOR_SECURITY = 10;         //外部条件:安防联动
        public const int LG_EXT_SENSOR_TIME_SEG = 11;         //外部条件:时间段
        public const int LG_EXT_SENSOR_DATE_SEG = 12;         //外部条件:日期段
        public const int LG_EXT_SENSOR_WEEK_CYC = 13;         //外部条件:周循环
        public const int LG_DEV_SENSOR_TEMP = 14;           //消防温控    (含有外设,含有等级)
        public const int LG_SENSOR_TOTAL = 15;              //总数(★★数量以后会不断增加,必须在最尾处增加★★)
        public const int LG_SENSOR_DEV_FLAG = 0x8000;       //[外设]传感器[标志位]->如:本设备,外设
        public const int LG_SENSOR_LVL_FLAG = 0x4000;       //传感器[级别][标志位]->如:温度,27℃(数值),舒适(级别)
        public const int LG_SENSOR_MASK = 0xBFFF;           //[同一个]传感器[掩码]->如:本设备的温度传感器,外设的温度传感器
        public const int LG_SENSOR_TYP_MASK = 0x3FFF;       //[同类型]传感器[掩码]->如:温度,湿度
        public const int LG_SENSOR_END_MARK = 0xFFFF;       //传感器结束符
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

        protected GridViewComboBox cbxOperate = new GridViewComboBox();//运算选择
        protected GridViewComboBox cbxKind = new GridViewComboBox();//触发类型
        protected GridViewTextEdit InvalidEdit = new GridViewTextEdit();//无效编辑
        protected GridColumn dcTriggerObj;//触发对象
        protected GridColumn dcKind;//触发类型
        protected GridColumn dcOperate;//触发运算
        protected GridColumn dcStartValue;//初始值
        protected GridColumn dcEndValue;//结束值
        protected GridColumn dcValid;//持续时间
        protected GridColumn dcInvalid;//无效时间

        public abstract UdpData GetLogicData(int logicID);//获取udp指令
        public abstract void SetLogicData(UdpData udp);//获取逻辑条件 
        public abstract void InitViewSetting();       /// 初始化配置界面

        public ViewLogicControl(Device _device, GridView gv)
        {

            this.deviceTrigger = _device;
            this.gvLogic = gv;
            InvalidEdit.ReadOnly = true;
            InvalidEdit.NullText = "无效";

            dcTriggerObj = gv.Columns.ColumnByFieldName(ViewConfig.DC_OBJECT);//触发对象
            dcKind = gv.Columns.ColumnByFieldName(ViewConfig.DC_KIND);//触发类型
            dcOperate = gv.Columns.ColumnByFieldName(ViewConfig.DC_OPERATION);//运算操作
            dcStartValue = gv.Columns.ColumnByFieldName(ViewConfig.DC_START_VALUE);//初始值
            dcEndValue = gv.Columns.ColumnByFieldName(ViewConfig.DC_END_VALUE);//结束值 
            dcValid = gv.Columns.ColumnByFieldName(ViewConfig.DC_VALID_TIME);//有效值
            dcInvalid = gv.Columns.ColumnByFieldName(ViewConfig.DC_INVALID_TIME);//无效值

            //----初始化界面-----
            foreach (GridColumn gc in gv.Columns)               
                    gc.Visible = true; 
            cbxOperate.Items.Clear();//清空触发运算
            dcOperate.ColumnEdit = this.cbxOperate;//触发运算符 ,统一为下拉选择       
            cbxKind.Items.Clear();//清空触发类型
            dcKind.ColumnEdit = this.cbxKind;//触发类型
        }

        /// <summary>
        /// 设置无效
        /// </summary>
        /// <param name="gc"></param>
        protected void setGridColumnInvalid(GridColumn gc)
        {
            DataRow dr = gvLogic.GetDataRow(0);
            dr[gc.FieldName] = null;
            dr.EndEdit();
            gc.ColumnEdit = InvalidEdit;
            gc.AppearanceCell.BackColor = Color.Gainsboro;//灰色
            gc.AppearanceCell.ForeColor = Color.Black;
        }

        /// <summary>
        /// 设置生效
        /// </summary>
        /// <param name="gc"></param>
        /// <param name="editor"></param>
        protected void setGridColumnValid(GridColumn gc, DevExpress.XtraEditors.Repository.RepositoryItem editor)
        {
            gc.ColumnEdit = editor;
            gc.AppearanceCell.BackColor = Color.LightYellow;
            gc.AppearanceCell.ForeColor = Color.Blue;
        }





    }
}