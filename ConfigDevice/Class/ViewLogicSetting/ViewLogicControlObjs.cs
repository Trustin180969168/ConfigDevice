using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;

namespace ConfigDevice
{

    /// <summary>
    /// 可燃气体探头
    /// </summary>
    public class ViewLogicFlamableGasProbe:ViewLogicControl
    { 
        public ViewLogicFlamableGasProbe(Device _device, GridView gv)
            : base(_device, gv)
        {

            cbxOperate.Items.Add("等于");
            GridViewComboBox cbxStart = new GridViewComboBox();//----开始值---
            cbxStart.Items.Add("泄漏");
            cbxStart.Items.Add("正常");
            dcStartValue.ColumnEdit = cbxStart;
            setGridColumnInvalid(dcEndValue);//结束值无效
            dcValid.ColumnEdit = new GridViewTimeEdit();//时间控件
            dcInvalid.ColumnEdit = new GridViewTimeEdit();//时间控件

            gv.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---初始化第一个运算选择----
        }


        public override UdpData GetLogicData(int logicID)
        {
            return null;
        }

        public override void SetLogicData(UdpData udp)
        {
 
        }


        public override void InitViewSetting()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 温度
    /// </summary>
    public class ViewLogicTemperature : ViewLogicControl
    {
        GridViewDigitalEdit temperatureEdit = new GridViewDigitalEdit();//--温度编辑控件---
        public ViewLogicTemperature(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add("等于");
            cbxOperate.Items.Add("小于");
            cbxOperate.Items.Add("大于");
            cbxOperate.Items.Add("以内");
            cbxOperate.Items.Add("以外");
            cbxOperate.SelectedIndexChanged += new System.EventHandler(this.cbxOperate_SelectedIndexChanged);

            //-------初始化温度编辑控件------
            temperatureEdit.DisplayFormat.FormatString = "#0.0 ℃";
            temperatureEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            temperatureEdit.Mask.EditMask = "#0.0 ℃";
            temperatureEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            temperatureEdit.Mask.UseMaskAsDisplayFormat = true;
            dcStartValue.ColumnEdit = temperatureEdit;//---开始值---
            dcEndValue.ColumnEdit = temperatureEdit; //----结束值---
 
            dcValid.ColumnEdit = new GridViewTimeEdit();//时间控件
            dcInvalid.ColumnEdit = new GridViewTimeEdit();//时间控件

            gv.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---初始化第一个运算选择----
        }


        public override UdpData GetLogicData(int logicID)
        {
            return null;
        }

        public override void SetLogicData(UdpData udp)
        {

        }

        /// <summary>
        /// 运算符触发
        /// </summary> 
        private void cbxOperate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string operateName = (string)cbxOperate.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
            if (operateName == "等于" || operateName == "小于" || operateName == "大于")
                setGridColumnInvalid(dcEndValue);//---设置结束值无效----
            else
                setGridColumnValid(dcEndValue, temperatureEdit);//----设置结束值有效----
        }


        public override void InitViewSetting()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 系统联动号
    /// </summary>
    public class ViewLogicSystemInteraction : ViewLogicControl
    {
        public ViewLogicSystemInteraction(Device _device, GridView gv)
            : base(_device, gv)
        {
            cbxOperate.Items.Add("等于");

            GridViewComboBox cbxStart = new GridViewComboBox();//----开始值选择---
            cbxStart.Items.Add("打开");
            cbxStart.Items.Add("关闭");
            dcStartValue.ColumnEdit = cbxStart;

            dcEndValue.ColumnEdit = new GridViewDigitalEdit();//----结束值为数字----
            setGridColumnInvalid(dcValid);//---取消有效持续---
            setGridColumnInvalid(dcInvalid); //---取消无效持续---

            gv.SetRowCellValue(0, dcOperate, cbxOperate.Items[0].ToString());//---初始化第一个运算选择----

        }


        public override UdpData GetLogicData(int logicID)
        {
            return null;
        }

        public override void SetLogicData(UdpData udp)
        {

        }


        public override void InitViewSetting()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 无效
    /// </summary>
    public class ViewLogicInvalid : ViewLogicControl
    {
        public ViewLogicInvalid(Device _device, GridView gv)
            : base(_device, gv)
        {
            setGridColumnInvalid(dcOperate);
            setGridColumnInvalid(dcStartValue);
            setGridColumnInvalid(dcEndValue);
            setGridColumnInvalid(dcValid);
            setGridColumnInvalid(dcInvalid);
        }


        public override UdpData GetLogicData(int logicID)
        {
            return null;
        }

        public override void SetLogicData(UdpData udp)
        {

        }


        public override void InitViewSetting()
        {
            throw new NotImplementedException();
        }
    }


}
