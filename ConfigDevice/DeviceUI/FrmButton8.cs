using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmButton8 : FrmDevice
    {
        private SpecialPanelKey button8; 
        private DataTable dtCircuit = new DataTable("按键选择");
        private SpecialPanelOptionData specialPanelOptionData;//---按键配置----
        private int InitSelectIndex = 0;//初始化选择配置项ID
    
        public FrmButton8(Device _device)
            : base(_device)
        {          
            InitializeComponent();

            button8 = this.DeviceEdit as SpecialPanelKey;
            button8.Circuit.CircuitCount = 8;
            list8Keys.InitKeySettingList(button8, 0, 8);//8按键配置列表            

            DataTable dt = SysConfig.DtDevice.Clone();
            DataRow[] amps = SysConfig.DtDevice.Select(DeviceConfig.DC_KIND_ID + "= '" + DeviceConfig.EQUIPMENT_AMP_MP3 + "' and " +
                DeviceConfig.DC_NETWORK_ID + " = '" + button8.NetworkID + "'");
            foreach (DataRow dr in amps)
                dt.Rows.Add(dr.ItemArray);
            //----指令配置----
            viewCommandEdit.ShowToolBar = true;
            viewCommandEdit.ShowCommandBar = true;
            keySecuritySetting.Init(button8);//---初始化安防配置 
        }

        public FrmButton8()
        {
            InitializeComponent();
        }

        private void FrmBaseDevice_Load(object sender, EventArgs e)
        {
            
            this.DeviceEdit.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            this.DeviceEdit.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件
            viewBaseSetting.DeviceEdit = this.DeviceEdit;//----配置编辑对象----
            viewBaseSetting.DeviceEdit.SearchVer();//---获取版本号-----   
            InitSelectDevice();//----初始化选择设备---
            this.panelSpecialCtrlObj.Init(0);     //---初始化环境---
            list8Keys.ReadKeyData();//--按键数据----
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
                    if (callbackParameter.Parameters != null && callbackParameter.DeviceID == button8.DeviceID)
                    {
                        if (callbackParameter.Action == ActionKind.ReadCircuit)
                        {
                            if (!hasInitLogicAndCommand)
                                initLogicAndCommand();
                        }
                        if (callbackParameter.Action == ActionKind.ReadSate)
                        {
                            InitSelectIndex = (int)callbackParameter.Parameters[0];
                            rgInitState.SelectedIndex = InitSelectIndex;
                        }
                        if (callbackParameter.Action == ActionKind.ReadOption)
                        {
                            specialPanelOptionData = callbackParameter.Parameters[0] as SpecialPanelOptionData;
                            keySecuritySetting.SetOptionData(specialPanelOptionData);//-----设置安防  
                            panelSpecialCtrlObj.SetSelectOptionData(this.rgpPanelKind.SelectedIndex,specialPanelOptionData);//----环境-----
                        }
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
            button8.Circuit.ReadRoadTitle();    //读取回路,完毕后自动读取第一个列按键
            button8.PanelCtrl.ReadKeyOption();    //读取面板配置
            button8.PanelCtrl.ReadKeyState();     //读取状态
        }

        private bool hasInitLogicAndCommand = false;
        /// <summary>
        /// 初始化逻辑和指令配置
        /// </summary>
        private void initLogicAndCommand()
        { 
            viewCommandEdit.CommmandGroups.Clear();
            foreach (int key in button8.Circuit.ListCircuitIDAndName.Keys)
                viewCommandEdit.CommmandGroups.Add(button8.Circuit.ListCircuitIDAndName[key]);    //---指令组选择---- 
            if (viewCommandEdit.NeedInit)
            {
                viewCommandEdit.InitViewCommand(button8);
                viewCommandEdit.CbxCommandGroup.SelectedIndex = 0;
            }
            else if (!viewCommandEdit.NeedInit)
                viewCommandEdit.UpdateGroupName();
            hasInitLogicAndCommand = true;
        }

        /// <summary>
        /// 选择事件
        /// </summary>
        public override void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.DeviceEdit.OnCallbackUI_Action -= this.callbackUI;//--退订回调事件
            //this.DeviceEdit.OnCallbackUI_Action -= viewBaseSetting.CallBackUI;//----退订回调事件
            //DeviceData deviceData = new DeviceData(SelectDeviceList[CbxSelectDevice.SelectedIndex]);//设备数据
            //Device DeviceSelect = FactoryDevice.CreateDevice(deviceData.ByteKindID).CreateDevice(deviceData);//--新建同类型设备对象---
            //if (button4.MAC == DeviceSelect.MAC) return;

            //hasInitLogicAndCommand = false;
            //viewBaseSetting.DeviceEdit = DeviceSelect;          //---基础配置编辑  
            //this.DeviceEdit = DeviceSelect;                         //---父类设备对象-----              
            //button4 = this.DeviceEdit as ButtonPanelKey;                   //---本界面编辑-----    
            //button4.OnCallbackUI_Action += this.callbackUI;     //--注册回调事件
            //button4.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件 
          
            //this.Text = button4.Name;                   //---界面标题----
            //this.list4Keys.InitKeySettingList(button4, 0,2);//---重新初始化按键配置控件----
            //viewBaseSetting.DeviceEdit.SearchVer();     //---获取版本号-----   
            //InitSelectDevice();                         //---初始化选择设备---
            //viewCommandEdit.NeedInit = true;            //---指令配置重新初始化,通过回调实现------      
            //keySecuritySetting.Init(button4);          //---初始化安防----
         
            //loadData();                                 //---加载数据----
            //list4Keys.ReadKeyData();

        }

 
        /// <summary>
        /// 保存
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            //---保存面板配置-------
            SpecialPanelOptionData keySettingData = new SpecialPanelOptionData(specialPanelOptionData.GetPanelOptionValue());
     
            keySecuritySetting.GetOptionData(ref keySettingData);//------安全页----

            button8.PanelCtrl.SaveKeyOption(keySettingData);
            button8.PanelCtrl.SaveKeyState(rgInitState.SelectedIndex); 
            list8Keys.SaveKeyData();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void btRefresh_Click(object sender, EventArgs e)
        {
            loadData(); 
            this.list8Keys.ReadKeyData(); 
        }

        private void FrmButton2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DeviceEdit.OnCallbackUI_Action -= this.callbackUI;//--注册回调事件
            this.DeviceEdit.OnCallbackUI_Action -= viewBaseSetting.CallBackUI;//----注册回调事件
        }

 
        /// <summary>
        /// 选择安防
        /// </summary>
        private void ceLeaveSafeSetting_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {

        }

        private void lookUpEditAmp_EditValueChanged(object sender, EventArgs e)
        {
     
        }

 

    }
}
