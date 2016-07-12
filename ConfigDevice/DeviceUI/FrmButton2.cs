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

            //-----初始化回路选择----
            //dtCircuit.Columns.Add(ViewConfig.DC_NUM, System.Type.GetType("System.String"));
            //dtCircuit.Columns.Add(ViewConfig.DC_ID, System.Type.GetType("System.String")); 
            //for (int i = 0; i < button2.Circuit.CircuitCount; i++)
            //    dtCircuit.Rows.Add(i+1,i + 1 + "按键", "");
            //num.FieldName = ViewConfig.DC_ID;
            //name.FieldName = ViewConfig.DC_NAME;
            //gcCircuit.DataSource = dtCircuit;



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
            keySettingTools.InitKeySettingList(button2); 
          
            this.Device.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            this.Device.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件
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
                        //foreach (int key in button2.Circuit.ListCircuitIDAndName.Keys)
                        //    dtCircuit.Rows[key - 1][name.FieldName] = button2.Circuit.ListCircuitIDAndName[key];
                        //dtCircuit.AcceptChanges();
                        //gcCircuit.Refresh();
                        //gvCircuit.RefreshData();
                        initLogicAndCommand();
                        //foreach (int key in button2.Circuit.ListCircuitIDAndName.Keys)
                        //    keySettingTools.SetKeyName(key,button2.Circuit.ListCircuitIDAndName[key]);
                      
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
            keySettingTools.ReadKeyData(0, 1);
        }

        /// <summary>
        /// 初始化逻辑和指令配置
        /// </summary>
        private void initLogicAndCommand()
        {
            viewCommandEdit.CommmandGroups.Clear();
            foreach (int key in button2.Circuit.ListCircuitIDAndName.Keys)
                viewCommandEdit.CommmandGroups.Add(button2.Circuit.ListCircuitIDAndName[key]);    //---指令组选择---- 
            if (viewCommandEdit.NeedInit)
            {
                viewCommandEdit.InitViewCommand(button2);
                viewCommandEdit.CbxCommandGroup.SelectedIndex = 0;
            }
            else if (!viewCommandEdit.NeedInit)
                viewCommandEdit.UpdateGroupName();
        }


        /// <summary>
        /// 选择事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Device.OnCallbackUI_Action -= this.callbackUI;//--退订回调事件
            this.Device.OnCallbackUI_Action -= viewBaseSetting.CallBackUI;//----退订回调事件
            DeviceData deviceData = new DeviceData(SelectDeviceList[CbxSelectDevice.SelectedIndex]);//设备数据
            Device DeviceSelect = FactoryDevice.CreateDevice(deviceData.ByteKindID).CreateDevice(deviceData);//--新建同类型设备对象---
            if (button2.MAC == DeviceSelect.MAC) return;
 
            viewBaseSetting.DeviceEdit = DeviceSelect;          //---基础配置编辑  
            this.Device = DeviceSelect;                         //---父类设备对象-----              
            button2 = this.Device as Button2;                   //---本界面编辑-----    
            button2.OnCallbackUI_Action += this.callbackUI;     //--注册回调事件
            button2.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件

            this.Text = button2.Name;                   //---界面标题----
            viewBaseSetting.DeviceEdit.SearchVer();     //---获取版本号-----   
            InitSelectDevice();                         //---初始化选择设备---
            viewCommandEdit.NeedInit = true;            //---指令配置重新初始化,通过回调实现------ 
            loadData();                                 //---加载数据-----
        }

 
        /// <summary>
        /// 保存
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            //gvCircuit.PostEditor();
            //DataRow drCurrent = gvCircuit.GetDataRow(gvCircuit.FocusedRowHandle);
            //if (drCurrent != null)
            //    drCurrent.EndEdit();
            //DataTable dtModify = dtCircuit.GetChanges(DataRowState.Modified);
            //if (dtModify == null) return;
            //foreach (DataRow dr in dtModify.Rows)
            //    button2.Circuit.SaveRoadSetting(Convert.ToInt16(dr[ViewConfig.DC_NUM].ToString()) - 1, dr[ViewConfig.DC_NAME].ToString());//--保存回路名称---
            //dtModify.AcceptChanges();//---提交修改---

            keySettingTools.SaveKeyData();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void btRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void FrmButton2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Device.OnCallbackUI_Action -= this.callbackUI;//--注册回调事件
            this.Device.OnCallbackUI_Action -= viewBaseSetting.CallBackUI;//----注册回调事件
        }


    }
}
