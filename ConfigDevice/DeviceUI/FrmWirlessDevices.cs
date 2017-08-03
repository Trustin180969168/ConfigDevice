using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice.DeviceUI
{
    public partial class FrmWirlessDevices : ConfigDevice.FrmDevice
    {

        private WirlessTransform wirlessTransform;
        private DataTable dtWirlessData = new DataTable("无线设备选择"); 
   
        public FrmWirlessDevices(Device _device)
            : base(_device)
        {
            InitializeComponent();
            wirlessTransform = _device as WirlessTransform;
            initWirelessDataTable();
        }

        private void btRefrash_Click(object sender, EventArgs e)
        {
            wirlessTransform.ReadDevList(0, 15);//---读取列表
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
        private void initWirelessDataTable()
        {
            dtWirlessData.Columns.Add(ViewConfig.DC_NUM, System.Type.GetType("System.Int16"));
            dtWirlessData.Columns.Add(ViewConfig.DC_MAC, System.Type.GetType("System.String"));
            dtWirlessData.Columns.Add(ViewConfig.DC_ADD, System.Type.GetType("System.String"));
            dtWirlessData.Columns.Add(ViewConfig.DC_DELETE, System.Type.GetType("System.String"));
            dtWirlessData.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));

            //---默认增加16行
            for (int i = 1; i <= 16; i++)
                dtWirlessData.Rows.Add(new Object[] { i});

            dcRowID.FieldName = ViewConfig.DC_NUM;
            dcMAC.FieldName = ViewConfig.DC_MAC;
            dcDelete.FieldName = ViewConfig.DC_DELETE;
            dcDeviceName.FieldName = ViewConfig.DC_NAME;
            dcDeviceName.FieldName = ViewConfig.DC_ADD;

            gcWirlessDevices.DataSource = dtWirlessData;
        }


        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            viewBaseSetting.DeviceEdit.SearchVer();//---获取版本号-----    
            wirlessTransform.ReadDevList(0, 15);//---读取列表
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
                    if (callbackParameter.Action == ActionKind.ReadWirlessDevice)
                    {
                        WirlessDeviceData data = callbackParameter.Parameters[0] as WirlessDeviceData;
                        if (data.Index > 15) return;
                        DataRow drDevice = dtWirlessData.Rows[data.Index];
                        drDevice.BeginEdit();
                        drDevice[ViewConfig.DC_NUM] = data.Index;
                        if(data.MacAddressStr.Replace("00","").Trim()!="")
                            drDevice[ViewConfig.DC_MAC] = data.MacAddressStr;
                        drDevice[ViewConfig.DC_NAME] = data.Name;
                        drDevice.EndEdit();
                        dtWirlessData.AcceptChanges();
                    }
                    if (callbackParameter.Action == ActionKind.WirteWirlessDevice)
                    {
                        WirlessDeviceData data = callbackParameter.Parameters[0] as WirlessDeviceData;
                        if (data.Index > 15) return;
                        DataRow drDevice = dtWirlessData.Rows[data.Index];
                        drDevice.AcceptChanges();
                    }
                    if (callbackParameter.Action == ActionKind.AddWirlessDevice)
                    {
                        WirlessDeviceData data = callbackParameter.Parameters[0] as WirlessDeviceData;
                        if (data.Index > 15) return;
                        DataRow drDevice = dtWirlessData.Rows[data.Index];
                        drDevice.AcceptChanges();
                    }
                    if (callbackParameter.Action == ActionKind.DelWirlessDevice)
                    {
                        WirlessDeviceData data = callbackParameter.Parameters[0] as WirlessDeviceData;
                        if (data.Index > 15) return;
                        DataRow drDevice = dtWirlessData.Rows[data.Index];
                        drDevice.BeginEdit();
                        drDevice[ViewConfig.DC_MAC] = "";
                        drDevice[ViewConfig.DC_NAME] = ""; 
                        drDevice.EndEdit();
                        drDevice.AcceptChanges();
                    }
                }
            }
            catch (Exception e) { CommonTools.MessageShow("执行异常!", 2, e.Message); }
        }

        /// <summary>
        /// 删除
        /// </summary> 
        private void linkEdit_Click(object sender, EventArgs e)
        {
            if(gvWirlessDevices.FocusedRowHandle == -1)return;
            int index = gvWirlessDevices.FocusedRowHandle;
            WirlessDeviceData data = wirlessTransform.WireLessDeviceList[index];
            wirlessTransform.DelWirlessData(data);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvWirlessDevices.FocusedRowHandle == -1) return;
                int index = gvWirlessDevices.FocusedRowHandle;
                WirlessDeviceData data = wirlessTransform.WireLessDeviceList[index];
                wirlessTransform.AddWirlessData(data);
            }
            catch (Exception e1) { CommonTools.MessageShow("增加失败！", 2, e1.Message); }
        }

        private void btSave_Click(object sender, EventArgs e)
        { 
            if (this.gvWirlessDevices.RowCount == 0) return;
            gvWirlessDevices.PostEditor();
            if (gvWirlessDevices.FocusedRowHandle >= 0)
            {
                DataRow dr = gvWirlessDevices.GetDataRow(gvWirlessDevices.FocusedRowHandle);
                dr.EndEdit();
            }

            DataTable dtUpdate = dtWirlessData.GetChanges(DataRowState.Modified);
            if (dtUpdate == null) return;
            foreach (DataRow dr in dtUpdate.Rows)
            {
                int index = Convert.ToInt16(dr[ViewConfig.DC_NUM]);
                WirlessDeviceData data = wirlessTransform.WireLessDeviceList[index];
                data.Name = dr[ViewConfig.DC_NAME].ToString();
                wirlessTransform.SaveWirlessData(data);
            }
        }

    }
}
