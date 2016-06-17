using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace ConfigDevice 
{
    /// <summary>
    /// 可燃气体探头
    /// </summary>
    public partial class ViewLogicFlamableGasProbe : BaseViewLogicControl
    {
        /// <summary>
        /// 获取逻辑配置数据
        /// </summary>
        /// <returns>TriggerData</returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
            TriggerData triggerData = GetInitTriggerData(dr);//----初始化触发数据----
            //--------泄漏/正常--------------
            string size1Str = dr[dcStartValue.FieldName].ToString();
            if (size1Str == ViewLogicFlamableGasProbe.VALUE1)
                triggerData.Size1 = 0;
            else if (size1Str == ViewLogicFlamableGasProbe.VALUE2)
                triggerData.Size1 = 1;
            triggerData.Size2 = 0;//----无效------
            //-----有效持续,无效持续------
            DateTime dtValid = DateTime.Parse(dr[dcValid.FieldName].ToString());
            DateTime dtInvalid = DateTime.Parse(dr[dcInvalid.FieldName].ToString());
            int validSeconds = dtValid.Hour * 60 * 60 + dtValid.Minute * 60 + dtValid.Second;           //有效秒数
            int invalidSeconds = dtInvalid.Hour * 60 * 60 + dtInvalid.Minute * 60 + dtInvalid.Second;   //无效秒数
            triggerData.ValidSeconds = (UInt16)validSeconds;
            triggerData.InvalidSeconds = (UInt16)invalidSeconds;

            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        { 
            DataRow dr = this.GetInitDataRow(td);//---初始化行---
            if (td.Size1 == 0)//-----获取触发值----
                dr[dcStartValue.FieldName] = ViewLogicFlamableGasProbe.VALUE1;
            else if (td.Size1 == 1)
                dr[dcStartValue.FieldName] = ViewLogicFlamableGasProbe.VALUE2;
            string nowDateStr = DateTime.Now.ToShortDateString();
            dr[dcValid.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.ValidSeconds).ToLongTimeString();//----有效持续---
            dr[dcInvalid.FieldName] = DateTime.Parse(nowDateStr).AddSeconds(td.InvalidSeconds).ToLongTimeString();//----无效持续---

            dr.EndEdit();
            dr.AcceptChanges();
        }
    }

    /// <summary>
    /// 系统联动号
    /// </summary>
    public partial class ViewLogicSystemInteraction : BaseViewLogicControl
    {
        /// <summary>
        /// 获取逻辑数据
        /// </summary>
        /// <returns></returns>
        public override TriggerData GetLogicData()
        {
            DataRow dr = gvLogic.GetDataRow(0);
            TriggerData triggerData = GetInitTriggerData(dr);//----初始化触发数据----
            triggerData.CompareID = 5;//系统联动号为5的比较符号值
            //--------泄漏/正常--------------
            string size1Str = dr[dcStartValue.FieldName].ToString();
            if (size1Str == ViewLogicSystemInteraction.VALUE1)
                triggerData.Size1 = 0;
            else if (size1Str == ViewLogicSystemInteraction.VALUE2)
                triggerData.Size1 = 1;
            triggerData.Size2 =Convert.ToInt16(dr[dcEndValue.FieldName]) ;

            return triggerData;
        }

        /// <summary>
        /// 设置逻辑数据
        /// </summary>
        /// <param name="td"></param>
        public override void SetLogicData(TriggerData td)
        {
            DataRow dr = this.GetInitDataRow(td);//---初始化行---
            if (td.Size1 == 0)//-----联动号操作----
                dr[dcStartValue.FieldName] = ViewLogicSystemInteraction.VALUE1;
            else if (td.Size1 == 1)
                dr[dcStartValue.FieldName] = ViewLogicSystemInteraction.VALUE2;
            dr[this.dcEndValue.FieldName] = td.Size2;//联动号

            dr.EndEdit();
            dr.AcceptChanges();
        }

    }



    /// <summary>
    /// 无效
    /// </summary>
    public partial class ViewLogicInvalid : BaseViewLogicControl
    {
        public override TriggerData GetLogicData()
        {
            TriggerData triggerData = new TriggerData();
            return triggerData;
        }

        public override void SetLogicData(TriggerData td)
        {

        }

    }


}
