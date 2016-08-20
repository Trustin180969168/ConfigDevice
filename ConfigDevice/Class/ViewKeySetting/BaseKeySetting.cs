using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;

namespace ConfigDevice
{
    public  class BaseKeySetting
    {

        protected GridViewPercentEdit percentEdit;  //---百分比编辑控件
        protected GridViewDigitalEdit digitalEdit;  //---数字编辑控件

        protected GridColumn dcControlKind;                                     //---控制类型
        protected GridColumn dcDirectionMax;                                    //---方向最大值
        protected GridColumn dcDirectionMin;                                    //---方向最小值
        protected GridColumn dcDirectionStep;                                   //---方向步进
        protected GridColumn dcRelevance;                                       //---关联号
        protected GridColumn dcMutex;                                           //---互斥号

 
        /// <summary>
        /// 设置按键配置
        /// </summary>
        /// <param name="gv"></param>
        public BaseKeySetting(GridView gv)
        {
            percentEdit = new GridViewPercentEdit();  //---百分比编辑控件
            digitalEdit = new GridViewDigitalEdit();  //---数字编辑控件
            digitalEdit.MinValue = 0;
            digitalEdit.MaxValue = 255;

            dcControlKind = gv.Columns.ColumnByFieldName(ViewConfig.DC_CONTROL_KIND);
            dcDirectionMax = gv.Columns.ColumnByFieldName(ViewConfig.DC_DIRECTION_MAX);
            dcDirectionMin = gv.Columns.ColumnByFieldName(ViewConfig.DC_DIRECTION_MIN);
            dcDirectionStep = gv.Columns.ColumnByFieldName(ViewConfig.DC_DIRECTION_STEP);
            dcRelevance = gv.Columns.ColumnByFieldName(ViewConfig.DC_RELEVANCE_NUM);
            dcMutex = gv.Columns.ColumnByFieldName(ViewConfig.DC_MUTEX_NUM);                
       
            dcDirectionMax.ColumnEdit = percentEdit;
            dcDirectionMin.ColumnEdit = percentEdit;
            dcDirectionStep.ColumnEdit = percentEdit;
            dcRelevance.ColumnEdit = digitalEdit;
            dcMutex.ColumnEdit = digitalEdit;
        }

        /// <summary>
        /// 获取按键类型名称
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected string GetKeyKindName(KeyData keyData)
        {
            string keyKindValue = "";
            int hi4 = (keyData.KeyKind & 0xf0) >> 4;
            int lo4 = keyData.KeyKind & 0x0f;

            if (keyData.FunctionDataMinValue == 0 && keyData.FunctionDataMaxValue == 1
                && hi4 == (int)DeviceConfig.KeyKind.KEY_TYPE_NULL && lo4 != (int)DeviceConfig.KeyKind.KEY_TYPE_NULL)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_LOOP_LIGHT;//循环调光
            else if (keyData.FunctionDataMinValue == 0 && keyData.FunctionDataMaxValue == 1
                && hi4 == (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN && lo4 != (int)DeviceConfig.KeyKind.KEY_TYPE_NULL)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_OPEN_CLOSE_LOOP_LIGHT;//开关+循环调光
            else if (keyData.FunctionDataMinValue == 0 && keyData.FunctionDataMaxValue == 1
                && hi4 == (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN && lo4 == (int)DeviceConfig.KeyKind.KEY_TYPE_NULL)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_OPEN_CLOSE;//开关
            else if (keyData.FunctionDataMinValue == 0 && keyData.FunctionDataMaxValue == 1
                && hi4 != (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_OPEN_CLOSE;//开关

            else if (keyData.FunctionDataMinValue == 0 && keyData.FunctionDataMaxValue == 0
                && hi4 == (int)DeviceConfig.KeyKind.KEY_TYPE_NULL && lo4 != (int)DeviceConfig.KeyKind.KEY_TYPE_NULL)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_LOOP_LIGHT;//"循环调光"
            else if (keyData.FunctionDataMinValue == 0 && keyData.FunctionDataMaxValue == 0
                && hi4 == (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN && lo4 != (int)DeviceConfig.KeyKind.KEY_TYPE_NULL)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_CLOSE_LOOP_LIGHT;// "关+循环调光";
            else if (keyData.FunctionDataMinValue == 0 && keyData.FunctionDataMaxValue == 0
                && hi4 == (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN && lo4 == (int)DeviceConfig.KeyKind.KEY_TYPE_NULL)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_CLOSE;//"关";
            else if (keyData.FunctionDataMinValue == 0 && keyData.FunctionDataMaxValue == 0
                && hi4 != (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_CLOSE;//"关";

            else if (keyData.FunctionDataMinValue == 1 && keyData.FunctionDataMaxValue == 1
                && hi4 == (int)DeviceConfig.KeyKind.KEY_TYPE_NULL && lo4 != (int)DeviceConfig.KeyKind.KEY_TYPE_NULL)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_LOOP_LIGHT;//循环调光
            else if (keyData.FunctionDataMinValue == 1 && keyData.FunctionDataMaxValue == 1
                && hi4 == (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN && lo4 != (int)DeviceConfig.KeyKind.KEY_TYPE_NULL)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_OPEN_LOOP_LIGHT;// "开+循环调光";
            else if (keyData.FunctionDataMinValue == 1 && keyData.FunctionDataMaxValue == 1
                && hi4 == (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN && lo4 == (int)DeviceConfig.KeyKind.KEY_TYPE_NULL)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_OPEN;// "开";
            else if (keyData.FunctionDataMinValue == 1 && keyData.FunctionDataMaxValue == 1
                && hi4 != (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN)
                keyKindValue = ViewConfig.KEY_CONTROL_KIND_NAME_OPEN;// "开";

            return keyKindValue;
        }


        
        /// <summary>
        /// 获取按键类型名称
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected void InitKeyDataByKindName(DataRow dr,KeyData keyData)
        {
            string keyKindValue = dr[ViewConfig.DC_CONTROL_KIND].ToString();
            int hi4 = 0;
            int lo4 = 0;
            switch (keyKindValue)
            {
                case ViewConfig.KEY_CONTROL_KIND_NAME_OPEN_CLOSE: //"开关";
                    keyData.FunctionDataMinValue = 0; keyData.FunctionDataMaxValue = 1;
                    hi4 = (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN; lo4 = (int)DeviceConfig.KeyKind.KEY_TYPE_NULL;                
                    break;
                case ViewConfig.KEY_CONTROL_KIND_NAME_OPEN: //"开";
                    keyData.FunctionDataMinValue = 1; keyData.FunctionDataMaxValue = 1;
                    hi4 = (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN; lo4 = (int)DeviceConfig.KeyKind.KEY_TYPE_NULL;
                    break;
                case ViewConfig.KEY_CONTROL_KIND_NAME_CLOSE: //"关";
                    keyData.FunctionDataMinValue = 0; keyData.FunctionDataMaxValue = 0;
                    hi4 = (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN; lo4 = (int)DeviceConfig.KeyKind.KEY_TYPE_NULL;
                    break;
                case ViewConfig.KEY_CONTROL_KIND_NAME_LOOP_LIGHT:// "循环调光";
                    keyData.FunctionDataMinValue = 0; keyData.FunctionDataMaxValue = 1;
                    hi4 = (int)DeviceConfig.KeyKind.KEY_TYPE_NULL; lo4 = (int)DeviceConfig.KeyKind.KEY_TYPE_HIT;
                    break;
                case ViewConfig.KEY_CONTROL_KIND_NAME_OPEN_CLOSE_LOOP_LIGHT:// "开关+循环调光";
                    keyData.FunctionDataMinValue = 0; keyData.FunctionDataMaxValue = 1;
                    hi4 = (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN; lo4 = (int)DeviceConfig.KeyKind.KEY_TYPE_HIT;
                    break;
                case ViewConfig.KEY_CONTROL_KIND_NAME_OPEN_LOOP_LIGHT: //"开+循环调光";
                    keyData.FunctionDataMinValue = 1; keyData.FunctionDataMaxValue = 1;
                    hi4 = (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN; lo4 = (int)DeviceConfig.KeyKind.KEY_TYPE_HIT;
                    break;
                case ViewConfig.KEY_CONTROL_KIND_NAME_CLOSE_LOOP_LIGHT:// "关+循环调光";
                    keyData.FunctionDataMinValue = 0; keyData.FunctionDataMaxValue = 0;
                    hi4 = (int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN; lo4 = (int)DeviceConfig.KeyKind.KEY_TYPE_HIT;
                    break;

                default: break;
            }
            keyData.KeyNum = (byte)(Convert.ToInt16(dr[ViewConfig.DC_NUM]) - 1);//---按键序号
            keyData.KeyKind = (byte)(((hi4 << 4) & 0xF0) | (lo4 & 0x0F));//----控制类型---
            keyData.ControlObj = ViewConfig.KEY_TYPE_NAME_ID[dr[ViewConfig.DC_CONTROL_OBJ].ToString()];//---控制对象---
            keyData.DirectionMaxValue = (byte)Convert.ToInt16(dr[ViewConfig.DC_DIRECTION_MAX]);//方向最大值
            keyData.DirectionMinValue = (byte)Convert.ToInt16(dr[ViewConfig.DC_DIRECTION_MIN]);//方向最小值
            keyData.DirectionDataFloatingStep = (byte)Convert.ToInt16(dr[ViewConfig.DC_DIRECTION_STEP]);//---步进---
            keyData.RelevanceNum = (byte)Convert.ToInt16(dr[ViewConfig.DC_RELEVANCE_NUM]);//---关联号---
            keyData.MutexNum = (byte)Convert.ToInt16(dr[ViewConfig.DC_MUTEX_NUM]);//---互斥号---

        }

        /// <summary>
        /// 获取按键数据
        /// </summary>
        /// <param name="dr">获取按键数据</param>
        /// <returns></returns>
        public  KeyData GetKeyData(DataRow dr)
        {
            KeyData keyData = new KeyData();
            this.InitKeyDataByKindName(dr, keyData);
            return keyData;
        }

        /// <summary>
        /// 设置按键数据
        /// </summary>
        /// <param name="keyData">返回的按键数据</param>
        /// <param name="drKeyData">数据行</param>
        /// <returns></returns>
        public void SetKeyData(KeyData keyData, DataRow drKeyData)
        {
            drKeyData[ViewConfig.DC_NUM] = (int)keyData.KeyNum + 1;         //---第几个按键
            if(ViewConfig.KEY_TYPE_ID_NAME.ContainsKey(keyData.ControlObj))
                drKeyData[ViewConfig.DC_CONTROL_OBJ] = ViewConfig.KEY_TYPE_ID_NAME[keyData.ControlObj];  //控制对象(设备类型)
            drKeyData[ViewConfig.DC_CONTROL_KIND] = this.GetKeyKindName(keyData);//操作类型

            drKeyData[ViewConfig.DC_DIRECTION_MAX] = (int)keyData.DirectionMaxValue;//方向最大值
            drKeyData[ViewConfig.DC_DIRECTION_MIN] = (int)keyData.DirectionMinValue;//方向最小值
            drKeyData[ViewConfig.DC_DIRECTION_STEP] = (int)keyData.DirectionDataFloatingStep;//方向步进
            drKeyData[ViewConfig.DC_RELEVANCE_NUM] = (int)keyData.RelevanceNum;//关联号
            drKeyData[ViewConfig.DC_MUTEX_NUM] = (int)keyData.MutexNum;//互斥号 

            drKeyData.AcceptChanges();
        }
        
    }
}
