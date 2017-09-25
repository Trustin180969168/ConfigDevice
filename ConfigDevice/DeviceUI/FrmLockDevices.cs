using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice.DeviceUI
{
    public partial class FrmLockDevices : ConfigDevice.FrmDevice
    {
        private VirtualPasswordFingerMarkLock fingerMarkLock;
        private DataTable dtLockConfigData = new DataTable("指纹锁配置");

        public FrmLockDevices(Device _device)
            : base(_device)
        {
            InitializeComponent();
            fingerMarkLock = _device as VirtualPasswordFingerMarkLock;
            initFingerLockDataTable();
        }

        private void btRefrash_Click(object sender, EventArgs e)
        {
            fingerMarkLock.ReadAmplifierConfig();//---读功放配置
            fingerMarkLock.ReadLockConfig();//---读锁配置
        }

        private void FrmWirlessDevices_Load(object sender, EventArgs e)
        {
            DeviceEdit.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            DeviceEdit.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件
            viewBaseSetting.DeviceEdit = this.DeviceEdit;//----配置编辑对象---- 
            loadData();//---加载数据-----
        } 

        /// <summary>
        /// 初始化数据结构
        /// </summary>
        private void initFingerLockDataTable()
        {
            dtLockConfigData.Columns.Add(ViewConfig.DC_NUM, System.Type.GetType("System.Int16")); 
            dtLockConfigData.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            dtLockConfigData.Columns.Add(ViewConfig.DC_START_VALUE, System.Type.GetType("System.String"));
            dtLockConfigData.Columns.Add(ViewConfig.DC_END_VALUE, System.Type.GetType("System.String"));
            dtLockConfigData.Columns.Add(ViewConfig.DC_KIND_NAME, System.Type.GetType("System.String")); 

            dcRowID.FieldName = ViewConfig.DC_NUM; 
            dcOpenMusic.FieldName = ViewConfig.DC_NAME; 
            dcStartTime.FieldName = ViewConfig.DC_START_VALUE;
            dcEndTime.FieldName = ViewConfig.DC_END_VALUE;
            dcKindName.FieldName = ViewConfig.DC_KIND_NAME;

            GridViewTimeEdit timeEdit = new GridViewTimeEdit();
            timeEdit.EditMask = "HH:mm";

            dcStartTime.ColumnEdit = timeEdit;
            dcEndTime.ColumnEdit = timeEdit;
            gcLockConfigs.DataSource = dtLockConfigData;
        }


        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            viewBaseSetting.DeviceEdit.SearchVer();//---获取版本号-----   
            fingerMarkLock.ReadAmplifierConfig();//读取功放配置
            fingerMarkLock.ReadLockConfig();//读取锁配置
    
        }

        /// <summary>
        /// 回调
        /// </summary>
        private void callbackUI(CallbackParameter callbackParameter)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new CallbackUIAction(callbackUI), callbackParameter);
                }
                else
                {
                    if (callbackParameter.Action == ActionKind.ReadLockAmplifierConfig)
                    {
                        cbxAmplifier.Properties.Items.Clear();
                        foreach (LockAmplifierConfigData data in fingerMarkLock.AmplifierConfigList)
                        {
                            cbxAmplifier.Properties.Items.Add(data.DeviceID);
                        }
                        if (cbxAmplifier.Properties.Items.Count > 0)
                        {
                            cbxAmplifier.SelectedIndex = 0;
                           
                        }                        
                    }
                    if (callbackParameter.Action == ActionKind.ReadLockConfig)
                    {
                        dtLockConfigData.Rows.Clear();
                        foreach (LockConfigData configData in fingerMarkLock.ConfigList.Values)
                        { 
                            dtLockConfigData.Rows.Add(new object[] { configData.Num, configData.MusicNum,
                            configData.StartTimeStr,configData.EndTimeStr});
                        }
                        dtLockConfigData.AcceptChanges();
                    } 
                }
            }
            catch (Exception e) { CommonTools.MessageShow("执行异常!", 2, e.Message); }
        }

        /// <summary>
        /// 保存
        /// </summary> 
        private void btSave_Click(object sender, EventArgs e)
        { 
            //---保存功放配置----
            fingerMarkLock.SaveAmplifierConfig(
                new LockAmplifierConfigData(
                    cbxAmplifier.SelectedIndex,Convert.ToInt16(cbxAmplifier.Text),(int)spdtVolume.Value));

            //---保存锁配置----
            if (this.gvLockConfigs.RowCount == 0) return;
            gvLockConfigs.PostEditor();
            if (gvLockConfigs.FocusedRowHandle >= 0)
            {
                DataRow dr = gvLockConfigs.GetDataRow(gvLockConfigs.FocusedRowHandle);
                dr.EndEdit();
            }

            DataTable dtUpdate = dtLockConfigData.GetChanges(DataRowState.Modified);
            if (dtUpdate == null) return;
            foreach (DataRow dr in dtUpdate.Rows)
            {
                int index = Convert.ToInt16(dr[ViewConfig.DC_NUM]);
                LockConfigData data = fingerMarkLock.ConfigList[index];
                data.MusicNum =(byte)Convert.ToInt16(dr[ViewConfig.DC_NAME]) ;//--曲目
                DateTime dtStart = Convert.ToDateTime(dr[ViewConfig.DC_START_VALUE].ToString());//开启时间
                DateTime dtEnd = Convert.ToDateTime(dr[ViewConfig.DC_END_VALUE].ToString());//结束时间
                data.StartHour = (byte)dtStart.Hour;
                data.StartMinute = (byte)dtStart.Minute;
                data.EndHour = (byte)dtEnd.Hour;
                data.EndMinute = (byte)dtEnd.Minute;

                fingerMarkLock.SaveLockConfig(data);//
            }
        }

        private void cbxAmplifier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAmplifier.Properties.Items.Count > 0)
            {
                spdtVolume.Value = fingerMarkLock.AmplifierConfigList[0].Volume; 
            }    
        }
         

    }
}
