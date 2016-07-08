using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmButton2 : FrmDevice
    {
        private Button2 button2;
        private DataTable dtCircuit = new DataTable("按键选择");
        public FrmButton2(Device _device)
            : base(_device)
        {          
            InitializeComponent();
            button2 = this.Device as Button2;
            this.Device.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            this.Device.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件
            //-----初始化回路选择----
            dtCircuit.Columns.Add(ViewConfig.DC_ID, System.Type.GetType("System.String"));
            dtCircuit.Columns.Add(ViewConfig.DC_NAME, System.Type.GetType("System.String"));
            for (int i = 0; i < button2.Circuit.CircuitCount; i++)
                dtCircuit.Rows.Add(i + 1 + "按键", "");
            num.FieldName = ViewConfig.DC_ID;
            name.FieldName = ViewConfig.DC_NAME;
            gcCircuit.DataSource = dtCircuit;
            //----指令配置----
            viewCommandEdit.ShowToolBar = true;
            viewCommandEdit.ShowCommandBar = true;
        }

        public FrmButton2()
        {
            InitializeComponent();
        }

        private void FrmBaseDevice_Load(object sender, EventArgs e)
        {
            viewBaseSetting.DeviceEdit = this.Device;//----配置编辑对象----
            viewBaseSetting.DeviceEdit.SearchVer();//---获取版本号-----   
            InitSelectDevice();//----初始化选择设备---
            loadData();//---加载数据-----
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
                    if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == Circuit.CLASS_NAME)//---电机回路名称--
                    {                       
                        foreach (int key in button2.Circuit.ListCircuitIDAndName.Keys)
                            dtCircuit.Rows[key - 1][name.FieldName] = button2.Circuit.ListCircuitIDAndName[key];
                        dtCircuit.AcceptChanges();
                        initLogicAndCommand();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            button2.Circuit.ReadRoadTitle();
        }

        /// <summary>
        /// 初始化逻辑和指令配置
        /// </summary>
        private void initLogicAndCommand()
        {
            foreach (int key in button2.Circuit.ListCircuitIDAndName.Keys)
            {
                viewCommandEdit.CommmandGroups.Add(button2.Circuit.ListCircuitIDAndName[key]);    //---指令组选择---- 
            }
            if (viewCommandEdit.NeedInit)
            {
                viewCommandEdit.InitViewCommand(button2);
                viewCommandEdit.CbxCommandGroup.SelectedIndex = 0;
            }
            else if (!viewCommandEdit.NeedInit)
                viewCommandEdit.UpdateGroupName();

        }

        /// <summary>
        /// 更新组名
        /// </summary>
        public void UpdateGroupName()
        {
            viewCommandEdit.NeedInit = true;
            int index = this.viewCommandEdit.CbxCommandGroup.SelectedIndex;
            for(int i = 0;i< button2.Circuit.CircuitCount;i++)
                viewCommandEdit.CbxCommandGroup.Items[i] = button2.Circuit.ListCircuitIDAndName[i + 1];
            viewCommandEdit.CbxCommandGroup.SelectedIndex = index;
            viewCommandEdit.NeedInit = false;
        }


        /// <summary>
        /// 选择事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Device DeviceSelect = new BaseDevice(SelectDeviceList[CbxSelectDevice.SelectedIndex]);
            if (Device.MAC == DeviceSelect.MAC) return;
            //this.Close();
            //FrmDevice frm = SysCtrl.GetFactory(DeviceSelect.ByteKindID).CreateDevice(DeviceSelect);
            //frm.Text = DeviceSelect.Name;
            //frm.Show();

            DeviceSelect.OnCallbackUI_Action += this.callbackUI;
            DeviceSelect.OnCallbackUI_Action += viewBaseSetting.CallBackUI;
            viewBaseSetting.DeviceEdit = DeviceSelect;
            Device = DeviceSelect;
            this.Text = Device.Name;
            Device.SearchVer();
            button2 = DeviceSelect as Button2;
            button2.Circuit.ReadRoadTitle();
        }

 
        /// <summary>
        /// 保存
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            gvCircuit.PostEditor();
            DataRow drCurrent = gvCircuit.GetDataRow(gvCircuit.FocusedRowHandle);
            if (drCurrent != null)
                drCurrent.EndEdit();
            DataTable dtModify = dtCircuit.GetChanges(DataRowState.Modified);
            if (dtModify == null) return;
            foreach (DataRow dr in dtModify.Rows)
                button2.Circuit.SaveRoadSetting(Convert.ToInt16(dr[ViewConfig.DC_ID].ToString()) - 1, dr[ViewConfig.DC_NAME].ToString());//--保存回路名称---
            dtModify.AcceptChanges();//---提交修改---
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void btRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }


    }
}
