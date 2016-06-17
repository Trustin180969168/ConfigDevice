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
            TriggerData triggerData = new TriggerData();
            triggerData.TriggerObjectID = ViewConfig.TRIGGER_NAME_ID[dr[ViewConfig.DC_OBJECT].ToString()];  //---触发对象--- 
            triggerData.TriggerKindID = ViewConfig.TRIGGER_KIND_NAME_ID[dr[ViewConfig.DC_KIND].ToString()]; //---级别标识---
            triggerData.CompareID = (byte)ViewConfig.MATH_NAME_ID[dr[ViewConfig.DC_OPERATION].ToString()];  //---触发比较---    
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
            DataRow dr = gvLogic.GetDataRow(0);
            dr[ViewConfig.DC_OBJECT] = ViewConfig.TRIGGER_ID_NAME[td.TriggerObjectID];
            dr[dcTriggerKind.FieldName] = ViewConfig.TRIGGER_KIND_ID_NAME[td.TriggerKindID];
            dr[dcOperate.FieldName] = ViewConfig.MATH_ID_NAME[td.CompareID];
            if (td.Size1 == 0)
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
