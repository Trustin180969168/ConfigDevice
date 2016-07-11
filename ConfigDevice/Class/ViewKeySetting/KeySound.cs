using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ConfigDevice
{
    public class KeySound : BaseKeySetting
    {
        public KeySound(GridView _gvKeyData)
            : base()
        {
            gvKeyData = _gvKeyData;
            cbxControlKind.Items.Add("开关");
            cbxControlKind.Items.Add("开");
            cbxControlKind.Items.Add("关");
            dcControlKind = gvKeyData.Columns.ColumnByFieldName(ViewConfig.DC_CONTROL_KIND);//控制类型
        }

        /// <summary>
        /// 获取按键数据
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override KeyData GetKeyData(DataRow dr)
        {
            KeyData keyData = new KeyData();




            return keyData;
        }

        /// <summary>
        /// 设置按键数据
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override void SetKeyData(KeyData keyData,DataRow drKeyData)
        {


        }

        

    }
}
