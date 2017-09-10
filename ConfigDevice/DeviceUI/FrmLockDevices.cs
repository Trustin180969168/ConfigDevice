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

        private WirlessTransform wirlessTransform;
        private DataTable dtWirlessData = new DataTable("无线设备选择");

        public FrmLockDevices(Device _device)
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
            dtWirlessData.Columns.Add(ViewConfig.DC_ADD, System.Type.GetType("System.String"));
            dtWirlessData.Columns.Add(ViewConfig.DC_DELETE, System.Type.GetType("System.String"));
            dtWirlessData.Columns.Add(ViewConfig.DC_CLEAR, System.Type.GetType("System.String"));
            dtWirlessData.Columns.Add(ViewConfig.DC_MAC, System.Type.GetType("System.String"));
            dtWirlessData.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            dtWirlessData.Columns.Add(ViewConfig.DC_DEVICE_KIND, System.Type.GetType("System.String"));
            dtWirlessData.Columns.Add(ViewConfig.DC_START_VALUE, System.Type.GetType("System.String"));

            //---默认增加16行
            //for (int i = 1; i <= 16; i++)
            //    dtWirlessData.Rows.Add(new Object[] { i });

            dcRowID.FieldName = ViewConfig.DC_NUM;
            dcAdd.FieldName = ViewConfig.DC_ADD;
            dcDelete.FieldName = ViewConfig.DC_DELETE;
            dcClear.FieldName = ViewConfig.DC_CLEAR;
            dcMAC.FieldName = ViewConfig.DC_MAC;
            dcDeviceName.FieldName = ViewConfig.DC_NAME;
            dcDeviceType.FieldName = ViewConfig.DC_DEVICE_KIND;
            dcOnline.FieldName = ViewConfig.DC_START_VALUE;

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
                        DataRow drEdit = findEditRowByMac(data.MacAddressStr);
                        if (drEdit == null)
                            drEdit = dtWirlessData.Rows.Add();
                        drEdit.BeginEdit();
                        drEdit[ViewConfig.DC_NUM] = dtWirlessData.Rows.Count + 1;
                        drEdit[ViewConfig.DC_MAC] = data.MacAddressStr;
                        drEdit[ViewConfig.DC_NAME] = data.Name;
                        drEdit[ViewConfig.DC_START_VALUE] = data.Online ? 1 : 0;
                        drEdit[ViewConfig.DC_DEVICE_KIND] = data.DeviceType; 
                        drEdit.EndEdit();
               
                        dtWirlessData.AcceptChanges();
                        gvWirlessDevices.BestFitColumns();
                    }
                    if (callbackParameter.Action == ActionKind.WirteWirlessDevice)
                    {
                        WirlessDeviceData data = callbackParameter.Parameters[0] as WirlessDeviceData;
                        DataRow drEdit = findEditRowByMac(data.MacAddressStr);
                        if (drEdit == null) return;
                        drEdit.AcceptChanges();
                    }
                    if (callbackParameter.Action == ActionKind.AddDelClearWirlessDevice)
                    {
                        WirlessActionResultData resultData = callbackParameter.Parameters[0] as WirlessActionResultData;

                        if (resultData.ActionResult == WirlessActionResult.ClearSuccess || resultData.ActionResult == WirlessActionResult.DeleteSuccess)
                        {
                            WirlessActionResultData data = callbackParameter.Parameters[0] as WirlessActionResultData;
                            DataRow drEdit = findEditRowByMac(data.MacAddressStr);
                            if (drEdit != null)
                                dtWirlessData.Rows.Remove(drEdit);
                        }else           if (resultData.ActionResult == WirlessActionResult.AddSuccess)
                        {
                            WirlessActionResultData data = callbackParameter.Parameters[0] as WirlessActionResultData;
                            DataRow drEdit = findEditRowByMac(data.MacAddressStr);  
                            if (drEdit == null)
                                drEdit = dtWirlessData.Rows.Add();
                            drEdit.BeginEdit();
                            drEdit[ViewConfig.DC_NUM] = dtWirlessData.Rows.Count + 1;
                            drEdit[ViewConfig.DC_MAC] = data.MacAddressStr;
                            drEdit[ViewConfig.DC_NAME] = data.Name;
                            drEdit[ViewConfig.DC_START_VALUE] = 1;
                            drEdit[ViewConfig.DC_DEVICE_KIND] = data.Kind;
                            drEdit.EndEdit();

                            dtWirlessData.AcceptChanges();
                            gvWirlessDevices.BestFitColumns();
                        }else if (resultData.ActionResult == WirlessActionResult.AddFailure)
                            CommonTools.MessageShow("添加设备失败!",1,"");
                        else if(resultData.ActionResult == WirlessActionResult.DeleteFailure)
                            CommonTools.MessageShow("删除设备失败!", 1, "");
                        else if (resultData.ActionResult == WirlessActionResult.ClearFailure)
                            CommonTools.MessageShow("清除设备失败!", 1, "");
                    }
                }
            }
            catch (Exception e) { CommonTools.MessageShow("执行异常!", 2, e.Message); }
        }

        /// <summary>
        /// 根据MAC找到对应的编辑行
        /// </summary>
        /// <returns></returns>
        private DataRow findEditRowByMac(  string macStr)
        {
            DataRow drEdit = null;
            foreach (DataRow drDevice in dtWirlessData.Rows)
            {
                if (drDevice[ViewConfig.DC_MAC].ToString() == macStr)
                {
                    drEdit = drDevice;
                    break;
                }
            }
            return drEdit;
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
        //private void linkAdd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (gvWirlessDevices.FocusedRowHandle == -1) return;
        //        int index = gvWirlessDevices.FocusedRowHandle;
        //        WirlessDeviceData data = wirlessTransform.WireLessDeviceList[index];
        //        wirlessTransform.AddWirlessData(data);
        //    }
        //    catch (Exception e1) { CommonTools.MessageShow("增加失败！", 2, e1.Message); }
        //}

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

        /// <summary>
        /// 在线颜色切换
        /// </summary>
        private void gvWirlessDevices_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string state = gvWirlessDevices.GetRowCellDisplayText(e.RowHandle, dcOnline);
                if (Convert.ToInt16(state) > 0)
                {
                    e.Appearance.ForeColor = Color.Black;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Gray; 
                }
            }
        }

        private void btAddDevice_Click(object sender, EventArgs e)
        { 
            wirlessTransform.AddWirlessData();
        }

    }
}
