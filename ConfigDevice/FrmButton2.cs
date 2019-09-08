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
        private ButtonPanelKey button2;
        private DataTable dtCircuit = new DataTable("按键选择");
        private ButtonPanelOptionData button2OptionData;//---按键配置----
        private int InitSelectIndex = 0;//初始化选择配置项ID
    
        public FrmButton2(Device _device)
            : base(_device)
        {          
            InitializeComponent();

            button2 = this.DeviceEdit as ButtonPanelKey;
            button2.Circuit.CircuitCount = 2;
            list2Keys.InitKeySettingList(button2, 0, 2);//2按键配置列表            

            DataTable dt = SysConfig.DtDevice.Clone();
            DataRow[] amps = SysConfig.DtDevice.Select(DeviceConfig.DC_KIND_ID + "= '" + DeviceConfig.EQUIPMENT_AMP_MP3 + "' and " +
                DeviceConfig.DC_NETWORK_ID + " = '" + button2.NetworkID + "'");
            foreach (DataRow dr in amps)
                dt.Rows.Add(dr.ItemArray);
            //----指令配置----
            viewCommandEdit.ShowToolBar = true;
            viewCommandEdit.ShowCommandBar = true;
            keySecuritySetting.Init(button2);//---初始化安防配置 
        }

        public FrmButton2()
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
            list2Keys.ReadKeyData();//--按键数据----
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
                    if (callbackParameter.Parameters != null && callbackParameter.DeviceID == button2.DeviceID)
                    {
                        if (callbackParameter.Action == ActionKind.ReadCircuit)
                        {
                            initLogicAndCommand();
                        }
                        if (callbackParameter.Action == ActionKind.ReadSate)
                        {
                            InitSelectIndex = (int)callbackParameter.Parameters[0];
                            rgInitState.SelectedIndex = InitSelectIndex;
                        }
                        if (callbackParameter.Action == ActionKind.ReadOption)
                        {
                            button2OptionData = callbackParameter.Parameters[0] as ButtonPanelOptionData;
                            keySecuritySetting.SetOptionData(button2OptionData);//-----设置安防
                            ceLittleLight.Checked = button2OptionData.CLoseLightWithBrightness;//---关灯微亮---
                            tbcLight.Value = button2OptionData.Luminance;                      //---亮度----

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
            button2.Circuit.ReadRoadTitle();    //读取回路,完毕后自动读取第一个列按键
            button2.PanelCtrl.ReadKeyOption();    //读取面板配置
            button2.PanelCtrl.ReadKeyState();     //读取状态
        }

        /// <summary>
        /// 初始化逻辑和指令配置
        /// </summary>
        private void initLogicAndCommand()
        { 
            //---初始化指令和逻辑配置---
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
            //this.DeviceEdit.OnCallbackUI_Action -= this.callbackUI;//--退订回调事件
            //this.DeviceEdit.OnCallbackUI_Action -= viewBaseSetting.CallBackUI;//----退订回调事件
            //DeviceData deviceData = new DeviceData(SelectDeviceList[CbxSelectDevice.SelectedIndex]);//设备数据
            //Device DeviceSelect = FactoryDevice.CreateDevice(deviceData.ByteKindID).CreateDevice(deviceData);//--新建同类型设备对象---
            //if (button2.MAC == DeviceSelect.MAC) return;

            //hasInitLogicAndCommand = false;
            //viewBaseSetting.DeviceEdit = DeviceSelect;          //---基础配置编辑  
            //this.DeviceEdit = DeviceSelect;                         //---父类设备对象-----              
            //button2 = this.DeviceEdit as ButtonPanelKey;                   //---本界面编辑-----    
            //button2.OnCallbackUI_Action += this.callbackUI;     //--注册回调事件
            //button2.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件

            //this.Text = button2.Name;                   //---界面标题----      
            //this.list2Keys.InitKeySettingList(button2, 0,2);//---重新初始化按键配置控件----
            //viewBaseSetting.DeviceEdit.SearchVer();     //---获取版本号-----   
            //InitSelectDevice();                         //---初始化选择设备---
            //viewCommandEdit.NeedInit = true;            //---指令配置重新初始化,通过回调实现------      
            //keySecuritySetting.Init(button2);          //---初始化安防----
         
            //loadData();                                 //---加载数据----
            //list2Keys.ReadKeyData();

        }

 
        /// <summary>
        /// 保存
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            //---保存面板配置-------
            ButtonPanelOptionData keySettingData = new ButtonPanelOptionData(button2OptionData.GetPanelOptionValue());
            keySettingData.CLoseLightWithBrightness = ceLittleLight.Checked;//---关灯微亮---
            keySettingData.Luminance = (byte)tbcLight.Value;                      //---亮度----  
            keySecuritySetting.GetOptionData(ref keySettingData);//------安全页----

            button2.PanelCtrl.SaveKeyOption(keySettingData);
            button2.PanelCtrl.SaveKeyState(rgInitState.SelectedIndex);
            list2Keys.SaveKeyData();  //---保存按键配置--------- 
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void btRefresh_Click(object sender, EventArgs e)
        {
            loadData(); 
            this.list2Keys.ReadKeyData(); 
        }

        private void FrmButton2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DeviceEdit.OnCallbackUI_Action -= this.callbackUI;//--注册回调事件
            this.DeviceEdit.OnCallbackUI_Action -= viewBaseSetting.CallBackUI;//----注册回调事件
        }

        /// <summary>
        /// 亮度选择
        /// </summary>
        private void tbcLight_Modified(object sender, EventArgs e)
        {
            lblLightSize.Text = (tbcLight.Value * 10).ToString() + "%";
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
