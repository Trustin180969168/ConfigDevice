using System;
using System.Collections.Generic;
using System.Text; 
using System.Data;
using System.Collections;

namespace ConfigDevice
{
    /// <summary>
    /// 逻辑快速配置
    /// </summary>
    public class LogicQuickSetting
    {
        public DataTable dtLogicData = new DataTable("LogicData");

        public string SettingName = "";//逻辑配置名称
        public string FileName = "";//配置文件名称
        public LogicQuickSetting(string name)
        {
            this.SettingName = name; 
            dtLogicData.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            dtLogicData.Columns.Add(ViewConfig.DC_LOGIC_VALUE, System.Type.GetType("System.String"));
            dtLogicData.Columns.Add(ViewConfig.DC_LOGIC_ADDITION_VALUE, System.Type.GetType("System.String"));

            //--------初始化配置文件--------
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(SysConfig.ConfigPath);
            di.Create();
            FileName = SysConfig.ConfigPath + SettingName+".xml";
            System.IO.FileInfo fi = new System.IO.FileInfo(FileName);
            if (!fi.Exists)
                dtLogicData.WriteXml(FileName, XmlWriteMode.WriteSchema);
            else
                dtLogicData.ReadXml(FileName);
        }         

        /// <summary>
        /// 保存逻辑数据到本地
        /// </summary>
        /// <param name="num">配置号</param>.
        /// <param name="name">名称</param>
        /// <param name="logicDataStr">逻辑数据</param>
        /// <param name="additionDataStr">附加动作数据</param>
        public void SaveLogicLocalSetting(int index,string name,byte[] logicData,byte[] additionData)
        { 
            //--------初始化数据,补充缺失的行-----------
            int addCount = index - (dtLogicData.Rows.Count - 1);
            while (addCount > 0)
            {

                dtLogicData.Rows.Add("", "", "");
                addCount--;
            }
            dtLogicData.AcceptChanges();
            //-------保存到配置文件--------
            DataRow dr = dtLogicData.Rows[index];
            dr[ViewConfig.DC_NAME] = name;
            dr[ViewConfig.DC_LOGIC_VALUE] = ConvertTools.ByteToHexStr(logicData);
            dr[ViewConfig.DC_LOGIC_ADDITION_VALUE] = ConvertTools.ByteToHexStr(additionData);
            dr.EndEdit();
            dtLogicData.AcceptChanges();
            dtLogicData.WriteXml(FileName, XmlWriteMode.WriteSchema);

        }

        /// <summary>
        /// 删除本地逻辑
        /// </summary>
        /// <param name="index"></param>
        public void DelLogicLocalSetting(int index)
        {
            if (index > dtLogicData.Rows.Count - 1) return;
            dtLogicData.Rows.RemoveAt(index);
            dtLogicData.AcceptChanges();
            dtLogicData.WriteXml(FileName, XmlWriteMode.WriteSchema);
        }

        /// <summary>
        /// 获取逻辑数据
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns></returns>
        public byte[] GetLogicData(int index)
        {
            byte[] logicData = new byte[126];
            if (index <= dtLogicData.Rows.Count - 1 && index >= 0)
            {
                DataRow dr = dtLogicData.Rows[index];
                logicData = ConvertTools.StrToToHexByte(dr[ViewConfig.DC_LOGIC_VALUE].ToString());
            }
            return logicData;
        }

        /// <summary>
        /// 获取逻辑附加数据
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns></returns>
        public byte[] GetLogicAdditionData(int index)
        {
            byte[] additionData = new byte[10];
            if (index <= dtLogicData.Rows.Count - 1)
            {
                DataRow dr = dtLogicData.Rows[index];
                additionData = ConvertTools.StrToToHexByte(dr[ViewConfig.DC_LOGIC_ADDITION_VALUE].ToString());
            }
            return additionData;
        }

        public ArrayList  GetLogicQuickNameList()
        {
            ArrayList names = new ArrayList();
            foreach (DataRow dr in dtLogicData.Rows)
            {
                names.Add(dr[ViewConfig.DC_NAME].ToString());
            }
            return names;
        }
 
    }


}
