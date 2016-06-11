using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;
using System.Drawing;

namespace ConfigDevice
{

    /// <summary>
    /// 可燃气体探头
    /// </summary>
    public partial class ViewLogicFlamableGasProbe : BaseViewLogicControl
    {
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值---
        public ViewLogicFlamableGasProbe(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//---运算--           
            cbxStart.Items.Add("泄漏");
            cbxStart.Items.Add("正常");
            dcStartValue.ColumnEdit = cbxStart;
        }

        public override void InitViewSetting()
        {     
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效---
            setGridColumnValid(dcValid, new GridViewTimeEdit());//---持续时间----
            setGridColumnValid(dcInvalid, new GridViewTimeEdit());//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxStart.Items[0].ToString());//--默认第一个开始值---
            gvLogic.SetRowCellValue(0, dcValid, "00:00:00");//----默认为0秒
            gvLogic.SetRowCellValue(0, dcInvalid, "00:00:00");//----默认为0秒
        }
    }

    /// <summary>
    /// 雷达探头
    /// </summary>
    public class ViewLogicRadar : BaseViewLogicControl
    {
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值---
        public ViewLogicRadar(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//---运算--           
            cbxStart.Items.Add("正常");
            cbxStart.Items.Add("触发");
            dcStartValue.ColumnEdit = cbxStart;
        }

        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效---
            setGridColumnInvalid(dcValid);//---持续时间----
            setGridColumnInvalid(dcInvalid);//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxStart.Items[0].ToString());//--默认第一个开始值---

        }
    }

    /// <summary>
    /// 防拆开关
    /// </summary>
    public class ViewLogicSwitTamper : BaseViewLogicControl
    {
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值---
        public ViewLogicSwitTamper(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//---运算--           
            cbxStart.Items.Add("安全");
            cbxStart.Items.Add("被拆");
            dcStartValue.ColumnEdit = cbxStart; 
        }

        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效---
            setGridColumnInvalid(dcValid);//---持续时间----
            setGridColumnInvalid(dcInvalid);//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxStart.Items[0].ToString());//--默认第一个开始值---

        }
    }


    /// <summary>
    /// 雨感
    /// </summary>
    public class ViewLogicRain : BaseViewLogicControl
    {
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值---
        public ViewLogicRain(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//---运算--           
            cbxStart.Items.Add("有雨");
            cbxStart.Items.Add("无雨"); 
            dcStartValue.ColumnEdit = cbxStart;
        }

        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效---
            setGridColumnInvalid(dcValid);//---持续时间----
            setGridColumnInvalid(dcInvalid);//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxStart.Items[0].ToString());//--默认第一个开始值---

        }
    }




    /// <summary>
    /// 安防联动
    /// </summary>
    public class ViewLogicSecurityInteraction : BaseViewLogicControl
    {
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值---
        public ViewLogicSecurityInteraction(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//---运算--           
            cbxStart.Items.Add("系统被撤防");
            cbxStart.Items.Add("系统进入布放延时中");
            cbxStart.Items.Add("系统进入布放");
            cbxStart.Items.Add("系统触发预警");
            cbxStart.Items.Add("系统触发报警");
            cbxStart.Items.Add("本机被撤防");
            cbxStart.Items.Add("本机进入布防延时中");
            cbxStart.Items.Add("本机进入布防");
            cbxStart.Items.Add("本机触发预警");
            cbxStart.Items.Add("本机触发报警"); 

            dcStartValue.ColumnEdit = cbxStart;
        }

        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效---
            setGridColumnInvalid(dcValid);//---持续时间----
            setGridColumnInvalid(dcInvalid);//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//---默认等于----
            gvLogic.SetRowCellValue(0, dcStartValue, cbxStart.Items[0].ToString());//--默认第一个开始值---

        }
    }







    /// <summary>
    /// 时间段
    /// </summary>
    public class ViewLogicTime : BaseViewLogicControl
    { 
        private GridViewTimeEdit timeEdit = new GridViewTimeEdit();//----时间编辑---
        public ViewLogicTime(Device _device, GridView gv)
            : base(_device, gv)
        {         
            cbxOperate.Items.Add("以内");
            cbxOperate.Items.Add("以外");  
        }

        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, timeEdit);//---开始值有效
            setGridColumnValid(dcEndValue, timeEdit);//----结束值有效---
            setGridColumnInvalid(dcValid);//---持续时间----
            setGridColumnInvalid(dcInvalid);//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个级别选择----
            gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---默认第一个运算选择----
            gvLogic.SetRowCellValue(0, dcStartValue, "00:00:00");//----默认为0秒
            gvLogic.SetRowCellValue(0, dcEndValue, "00:00:00");//----默认为0秒 

        }
    }


    /// <summary>
    /// 日期段
    /// </summary>
    public class ViewLogicDateOfMonthDay : BaseViewLogicControl
    {
        private GridViewDateOfMonthDayEdit dateEdit = new GridViewDateOfMonthDayEdit();//----时间编辑---
        public ViewLogicDateOfMonthDay(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add("以内");
            cbxOperate.Items.Add("以外");
        }

        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, dateEdit);//---开始值有效
            setGridColumnValid(dcEndValue, dateEdit);//----结束值无效---
            setGridColumnInvalid(dcValid);//---持续时间----
            setGridColumnInvalid(dcInvalid);//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个级别选择----
            gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---默认第一个运算选择----
            gvLogic.SetRowCellValue(0, dcStartValue, "01-01");//----默认为1号
            gvLogic.SetRowCellValue(0, dcEndValue, "01-01");//----默认为1号

        }
    }



    /// <summary>
    /// 星期范围
    /// </summary>
    public class ViewLogicWeek : BaseViewLogicControl
    {
      //  private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit iccbWeek = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();//周循环编辑
        private GridViewWeekEdit iccbWeek = new GridViewWeekEdit();
        public ViewLogicWeek(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//---等于---
            //iccbWeek.AutoHeight = false;
            //iccbWeek.ShowAllItemCaption = "全选";
            
            //this.iccbWeek.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            //new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期一"),
            //new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期二"),
            //new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期三"),
            //new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期四"),
            //new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期五"),
            //new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期六"),
            //new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期日")});
            dcStartValue.ColumnEdit = iccbWeek;
           
        }

        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, iccbWeek);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效---
            setGridColumnInvalid(dcValid);//---持续时间----
            setGridColumnInvalid(dcInvalid);//----失效时间-----  

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个级别选择----
            gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---默认第一个运算选择----
            
        }
    }


    /// <summary>
    /// 温度
    /// </summary>
    public class ViewLogicTemperature : BaseViewLogicControl
    {
        GridViewDigitalEdit temperatureEdit = new GridViewDigitalEdit();//--温度编辑控件---
        GridViewComboBox cbxOperateLevel = new GridViewComboBox();//---级别运算--
        GridViewComboBox cbxTemperatureLevelEdit = new GridViewComboBox();//---温度级别编辑控件---
        public ViewLogicTemperature(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxKind.Items.Add(BaseViewLogicControl.TRIGGER_KIND_LEVEL);//---加上级别---
            cbxKind.SelectedIndexChanged += new System.EventHandler(this.cbxKind_SelectedIndexChanged);//---级别选择事件---

            //-------触发运算选择------
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_LESS_THAN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_MORE_THAN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_WITHIN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EXCEPT);
            cbxOperate.SelectedIndexChanged += new System.EventHandler(this.cbxOperate_SelectedIndexChanged);
            //-------级别运算选择------
            cbxOperateLevel.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);

            //-------初始化温度编辑控件------
            temperatureEdit.DisplayFormat.FormatString = "#0 ℃";
            temperatureEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            temperatureEdit.Mask.EditMask = "#0 ℃";
            temperatureEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            temperatureEdit.Mask.UseMaskAsDisplayFormat = true;
            temperatureEdit.MaxValue = 60;
            temperatureEdit.MinValue = -20;

            //-------初始化级别编辑控件------
            cbxTemperatureLevelEdit.Items.Add("寒冷");
            cbxTemperatureLevelEdit.Items.Add("小冷");
            cbxTemperatureLevelEdit.Items.Add("舒适");
            cbxTemperatureLevelEdit.Items.Add("小热");
            cbxTemperatureLevelEdit.Items.Add("酷热");

        }


        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

        /// <summary>
        /// 设置初始值
        /// </summary>
        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, temperatureEdit);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效--- 
            setGridColumnValid(dcValid, new GridViewTimeEdit());//---持续时间----
            setGridColumnValid(dcInvalid, new GridViewTimeEdit());//失效时间----- 

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
            gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---

            gvLogic.SetRowCellValue(0, dcValid, "00:00:00");//----默认为0秒
            gvLogic.SetRowCellValue(0, dcInvalid, "00:00:00");//----默认为0秒
        }

        /// <summary>
        /// 触发类型选择
        /// </summary> 
        private void cbxKind_SelectedIndexChanged(object sender, EventArgs e)
        {           
            string kindName = (string)cbxKind.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr[ViewConfig.DC_KIND] = kindName;
            dr.EndEdit();
            if (kindName == BaseViewLogicControl.TRIGGER_KIND_VALUE)
            {
                setGridColumnValid(dcOperate, cbxOperate);
                setGridColumnValid(dcStartValue, temperatureEdit);
                setGridColumnValid(dcEndValue, temperatureEdit);

                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---
                gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束值---
            }
            else if (kindName == BaseViewLogicControl.TRIGGER_KIND_LEVEL)
            {
                setGridColumnValid(dcOperate, cbxOperateLevel);
                setGridColumnValid(dcStartValue, cbxTemperatureLevelEdit);
                setGridColumnInvalid(dcEndValue);

                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, cbxTemperatureLevelEdit.Items[0].ToString());//---开始值---         
            }
        }

        /// <summary>
        /// 运算符触发
        /// </summary> 
        private void cbxOperate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string operateName = (string)cbxOperate.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            if (operateName == BaseViewLogicControl.TRIGGER_OPERATE_EQUAL || operateName == BaseViewLogicControl.TRIGGER_OPERATE_LESS_THAN ||
                operateName == BaseViewLogicControl.TRIGGER_OPERATE_MORE_THAN)
                setGridColumnInvalid(dcEndValue);//---设置结束值无效----
            else
            {
                setGridColumnValid(dcEndValue, temperatureEdit);//----设置结束值有效----
                gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---
                gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束值---
            }
        }

    }


    /// <summary>
    /// 湿度
    /// </summary>
    public class ViewLogicHumidity : BaseViewLogicControl
    {
        GridViewPercentEdit humidityEdit = new GridViewPercentEdit();//--湿度编辑控件---
        GridViewComboBox cbxOperateLevel = new GridViewComboBox();//---级别运算--
        GridViewComboBox cbxHumidityLevelEdit = new GridViewComboBox();//---湿度级别编辑控件---
        public ViewLogicHumidity(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxKind.Items.Add(BaseViewLogicControl.TRIGGER_KIND_LEVEL);//---加上级别---
            cbxKind.SelectedIndexChanged += new System.EventHandler(this.cbxKind_SelectedIndexChanged);//---级别选择事件---

            //-------触发运算选择------
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_LESS_THAN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_MORE_THAN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_WITHIN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EXCEPT);
            cbxOperate.SelectedIndexChanged += new System.EventHandler(this.cbxOperate_SelectedIndexChanged);
            //-------级别运算选择------
            cbxOperateLevel.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);

            //-------初始化温度编辑控件------
            //humidityEdit.DisplayFormat.FormatString = "#0 %RH";
            //humidityEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //humidityEdit.Mask.EditMask = "#0 %RH";
            //humidityEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //humidityEdit.Mask.UseMaskAsDisplayFormat = true;
            //humidityEdit.MaxValue = 99;
            //humidityEdit.MinValue = 0;

            //-------初始化级别编辑控件------
            cbxHumidityLevelEdit.Items.Add("干燥");
            cbxHumidityLevelEdit.Items.Add("微燥");
            cbxHumidityLevelEdit.Items.Add("适宜");
            cbxHumidityLevelEdit.Items.Add("微湿");
            cbxHumidityLevelEdit.Items.Add("潮湿");

        }


        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

        /// <summary>
        /// 设置初始值
        /// </summary>
        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, humidityEdit);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效--- 
            setGridColumnValid(dcValid, new GridViewTimeEdit());//---持续时间----
            setGridColumnValid(dcInvalid, new GridViewTimeEdit());//失效时间----- 

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
            gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---

            gvLogic.SetRowCellValue(0, dcValid, "00:00:00");//----默认为0秒
            gvLogic.SetRowCellValue(0, dcInvalid, "00:00:00");//----默认为0秒
        }

        /// <summary>
        /// 触发类型选择
        /// </summary> 
        private void cbxKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kindName = (string)cbxKind.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr[ViewConfig.DC_KIND] = kindName;
            dr.EndEdit();
            if (kindName == BaseViewLogicControl.TRIGGER_KIND_VALUE)
            {
                setGridColumnValid(dcOperate, cbxOperate);
                setGridColumnValid(dcStartValue, humidityEdit);
                setGridColumnValid(dcEndValue, humidityEdit);

                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---
                gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束值---
            }
            else if (kindName == BaseViewLogicControl.TRIGGER_KIND_LEVEL)
            {
                setGridColumnValid(dcOperate, cbxOperateLevel);
                setGridColumnValid(dcStartValue, cbxHumidityLevelEdit);
                setGridColumnInvalid(dcEndValue);

                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, cbxHumidityLevelEdit.Items[0].ToString());//---开始值---         
            }
        }

        /// <summary>
        /// 运算符触发
        /// </summary> 
        private void cbxOperate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string operateName = (string)cbxOperate.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            if (operateName == BaseViewLogicControl.TRIGGER_OPERATE_EQUAL || operateName == BaseViewLogicControl.TRIGGER_OPERATE_LESS_THAN ||
                operateName == BaseViewLogicControl.TRIGGER_OPERATE_MORE_THAN)
                setGridColumnInvalid(dcEndValue);//---设置结束值无效----
            else
            {
                setGridColumnValid(dcEndValue, humidityEdit);//----设置结束值有效----
                gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---
                gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束值---
            }
        }

    }

    
    /// <summary>
    /// 风速
    /// </summary>
    public class ViewLogicWindy : BaseViewLogicControl
    {
        GridViewDigitalEdit windyEdit = new GridViewDigitalEdit();//--风速编辑控件---
        GridViewComboBox cbxOperateLevel = new GridViewComboBox();//---级别运算--
        GridViewComboBox cbxHumidityLevelEdit = new GridViewComboBox();//---湿度级别编辑控件---
        public ViewLogicWindy(Device _device, GridView gv)
            : base(_device, gv)
        { 
            cbxKind.Items.Add(BaseViewLogicControl.TRIGGER_KIND_LEVEL);//---加上级别---
            cbxKind.SelectedIndexChanged += new System.EventHandler(this.cbxKind_SelectedIndexChanged);//---级别选择事件---

            //-------触发运算选择------
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_LESS_THAN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_MORE_THAN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_WITHIN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EXCEPT);
            cbxOperate.SelectedIndexChanged += new System.EventHandler(this.cbxOperate_SelectedIndexChanged);
            //-------级别运算选择------
            cbxOperateLevel.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);

            //-------初始化温度编辑控件------
            windyEdit.DisplayFormat.FormatString = "#0.0 米/秒";
            windyEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            windyEdit.Mask.EditMask = "#0.0 米/秒";
            windyEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            windyEdit.Mask.UseMaskAsDisplayFormat = true;
         //   windyEdit.Increment = 0.1;

            //-------初始化级别编辑控件------
            cbxHumidityLevelEdit.Items.Add("无风");
            cbxHumidityLevelEdit.Items.Add("微风");
            cbxHumidityLevelEdit.Items.Add("小风");
            cbxHumidityLevelEdit.Items.Add("大风"); 

        }


        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

        /// <summary>
        /// 设置初始值
        /// </summary>
        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, windyEdit);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效--- 
            setGridColumnValid(dcValid, new GridViewTimeEdit());//---持续时间----
            setGridColumnValid(dcInvalid, new GridViewTimeEdit());//失效时间----- 

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
            gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---

            gvLogic.SetRowCellValue(0, dcValid, "00:00:00");//----默认为0秒
            gvLogic.SetRowCellValue(0, dcInvalid, "00:00:00");//----默认为0秒
        }

        /// <summary>
        /// 触发类型选择
        /// </summary> 
        private void cbxKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kindName = (string)cbxKind.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr[ViewConfig.DC_KIND] = kindName;
            dr.EndEdit();
            if (kindName == BaseViewLogicControl.TRIGGER_KIND_VALUE)
            {
                setGridColumnValid(dcOperate, cbxOperate);
                setGridColumnValid(dcStartValue, windyEdit);
                setGridColumnValid(dcEndValue, windyEdit);

                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---
                gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束值---
            }
            else if (kindName == BaseViewLogicControl.TRIGGER_KIND_LEVEL)
            {
                setGridColumnValid(dcOperate, cbxOperateLevel);
                setGridColumnValid(dcStartValue, cbxHumidityLevelEdit);
                setGridColumnInvalid(dcEndValue);

                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, cbxHumidityLevelEdit.Items[0].ToString());//---开始值---         
            }
        }

        /// <summary>
        /// 运算符触发
        /// </summary> 
        private void cbxOperate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string operateName = (string)cbxOperate.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            if (operateName == BaseViewLogicControl.TRIGGER_OPERATE_EQUAL || operateName == BaseViewLogicControl.TRIGGER_OPERATE_LESS_THAN ||
                operateName == BaseViewLogicControl.TRIGGER_OPERATE_MORE_THAN)
                setGridColumnInvalid(dcEndValue);//---设置结束值无效----
            else
            {
                setGridColumnValid(dcEndValue, windyEdit);//----设置结束值有效----
                gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---
                gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束值---
            }
        }

    }



    /// <summary>
    /// 亮度
    /// </summary>
    public class ViewLogicLuminance : BaseViewLogicControl
    {
        GridViewDigitalEdit luminanceEdit = new GridViewDigitalEdit();//--亮度编辑控件---
        GridViewComboBox cbxOperateLevel = new GridViewComboBox();//---级别运算--
        GridViewComboBox cbxHumidityLevelEdit = new GridViewComboBox();//---湿度级别编辑控件---
        public ViewLogicLuminance(Device _device, GridView gv)
            : base(_device, gv)
        { 
            cbxKind.Items.Add(BaseViewLogicControl.TRIGGER_KIND_LEVEL);//---加上级别---
            cbxKind.SelectedIndexChanged += new System.EventHandler(this.cbxKind_SelectedIndexChanged);//---级别选择事件---

            //-------触发运算选择------
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_LESS_THAN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_MORE_THAN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_WITHIN);
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EXCEPT);
            cbxOperate.SelectedIndexChanged += new System.EventHandler(this.cbxOperate_SelectedIndexChanged);
            //-------级别运算选择------
            cbxOperateLevel.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);

            //-------初始化温度编辑控件------
            luminanceEdit.MinValue = 0;
            luminanceEdit.MaxValue = 25000;

            //-------初始化级别编辑控件------
            cbxHumidityLevelEdit.Items.Add("黑夜");
            cbxHumidityLevelEdit.Items.Add("昏暗");
            cbxHumidityLevelEdit.Items.Add("明亮");
            cbxHumidityLevelEdit.Items.Add("白天");
            cbxHumidityLevelEdit.Items.Add("日照");

        }


        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

        /// <summary>
        /// 设置初始值
        /// </summary>
        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, luminanceEdit);//---开始值有效
            setGridColumnInvalid(dcEndValue);//----结束值无效--- 
            setGridColumnValid(dcValid, new GridViewTimeEdit());//---持续时间----
            setGridColumnValid(dcInvalid, new GridViewTimeEdit());//失效时间----- 

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个运算选择----
            gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
            gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---

            gvLogic.SetRowCellValue(0, dcValid, "00:00:00");//----默认为0秒
            gvLogic.SetRowCellValue(0, dcInvalid, "00:00:00");//----默认为0秒
        }

        /// <summary>
        /// 触发类型选择
        /// </summary> 
        private void cbxKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kindName = (string)cbxKind.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            gvLogic.PostEditor();
            DataRow dr = gvLogic.GetDataRow(0);
            dr[ViewConfig.DC_KIND] = kindName;
            dr.EndEdit();
            if (kindName == BaseViewLogicControl.TRIGGER_KIND_VALUE)
            {
                setGridColumnValid(dcOperate, cbxOperate);
                setGridColumnValid(dcStartValue, luminanceEdit);
                setGridColumnValid(dcEndValue, luminanceEdit);

                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---
                gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束值---
            }
            else if (kindName == BaseViewLogicControl.TRIGGER_KIND_LEVEL)
            {
                setGridColumnValid(dcOperate, cbxOperateLevel);
                setGridColumnValid(dcStartValue, cbxHumidityLevelEdit);
                setGridColumnInvalid(dcEndValue);

                gvLogic.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---第一个运算符-----
                gvLogic.SetRowCellValue(0, dcStartValue, cbxHumidityLevelEdit.Items[0].ToString());//---开始值---         
            }
        }

        /// <summary>
        /// 运算符触发
        /// </summary> 
        private void cbxOperate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string operateName = (string)cbxOperate.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            if (operateName == BaseViewLogicControl.TRIGGER_OPERATE_EQUAL || operateName == BaseViewLogicControl.TRIGGER_OPERATE_LESS_THAN ||
                operateName == BaseViewLogicControl.TRIGGER_OPERATE_MORE_THAN)
                setGridColumnInvalid(dcEndValue);//---设置结束值无效----
            else
            {
                setGridColumnValid(dcEndValue, luminanceEdit);//----设置结束值有效----
                gvLogic.SetRowCellValue(0, dcStartValue, 0);//---开始值---
                gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束值---
            }
        }

    }



    /// <summary>
    /// 系统联动号
    /// </summary>
    public class ViewLogicSystemInteraction : BaseViewLogicControl
    {
        private GridViewComboBox cbxStart = new GridViewComboBox();//----开始值选择---
        private GridViewDigitalEdit edtNum = new GridViewDigitalEdit();//----数字------
        public ViewLogicSystemInteraction(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add(BaseViewLogicControl.TRIGGER_OPERATE_EQUAL);//----触发运算符---
            //---开始为下拉----
            cbxStart.Items.Add("打开");
            cbxStart.Items.Add("关闭");

            //-------初始化温度编辑控件------
            edtNum.DisplayFormat.FormatString = "##0 号";
            edtNum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            edtNum.Mask.EditMask = "##0 号";
            edtNum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            edtNum.Mask.UseMaskAsDisplayFormat = true;
            edtNum.MinValue = 0;
            edtNum.MaxValue = 100;
        }

        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }


        public override void InitViewSetting()
        {
            setGridColumnValid(dcKind, cbxKind);//---触发值有效----
            setGridColumnValid(dcOperate, cbxOperate); //----触发运算有效----
            setGridColumnValid(dcStartValue, cbxStart);//---开始值有效
            setGridColumnValid(dcEndValue, edtNum);//---结束为数字编辑---
            setGridColumnInvalid(dcValid);//---取消有效持续---
            setGridColumnInvalid(dcInvalid); //---取消无效持续---

            gvLogic.SetRowCellValue(0, dcKind, this.cbxKind.Items[0].ToString());//---初始化第一个触发类型选择----
            gvLogic.SetRowCellValue(0, dcOperate, this.cbxOperate.Items[0].ToString());//---第一个触发运算---
            gvLogic.SetRowCellValue(0, dcStartValue, this.cbxStart.Items[0].ToString());//---第一个开始选择运算---
            gvLogic.SetRowCellValue(0, dcEndValue, 0);//---结束范围为0---
        }
    }




    /// <summary>
    /// 无效
    /// </summary>
    public class ViewLogicInvalid : BaseViewLogicControl
    {
        public ViewLogicInvalid(Device _device, GridView gv)
            : base(_device, gv)
        {
            foreach (GridColumn gc in gvLogic.Columns)
                if (gc.VisibleIndex != 0) setGridColumnInvalid(gc);

        }


        public override TriggerData GetLogicData()
        {
            return null;
        }

        public override void SetLogicData(TriggerData td)
        {

        }


        public override void InitViewSetting()
        {
   

        }
    }


}
