using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;

namespace ConfigDevice
{
    public abstract class BaseKeySetting
    {
        protected GridView gvKeyData;                                       //---编辑类别
        protected GridViewComboBox cbxControlKind = new GridViewComboBox(); //---选择控制类型
        protected GridColumn dcControlKind;                                 //---控制类型

        /// <summary>
        /// 获取按键数据
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public abstract KeyData GetKeyData(DataRow dr);

        /// <summary>
        /// 设置按键数据
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public abstract void SetKeyData(KeyData keyData, DataRow drKeyData);

        /// <summary>
        /// 设置按键数据
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public GridViewComboBox ControlObjSelect
        {
            get { return cbxControlKind; }
        }
    }
}
