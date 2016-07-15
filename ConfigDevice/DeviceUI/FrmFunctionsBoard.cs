using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmFunctionsBoard : FrmDevice
    {
        private PanelKey button2;
        private DataTable dtCircuit = new DataTable("按键选择");
        private PanelOptionData button2OptionData;//---按键配置----
        private int InitSelectIndex = 0;//初始化选择配置项ID
        public FrmFunctionsBoard(Device _device)
            : base(_device)
        {          
            InitializeComponent();
            button2 = this.Device as PanelKey;
            //----指令配置----
            button2.Circuit.CircuitCount = 26;
            viewCommandEdit.ShowToolBar = true;
            viewCommandEdit.ShowCommandBar = true;
        }

        public FrmFunctionsBoard()
        {
            InitializeComponent();
        }

        private void FrmBaseDevice_Load(object sender, EventArgs e)
        {
            keySettingTools.InitKeySettingList(button2, 8, ViewConfig.LCD_CAPTION_SCENE, ViewConfig.LCD_CAPTION_LIGHT,
                ViewConfig.LCD_CAPTION_CURTAIN, ViewConfig.LCD_CAPTION_LEAVE_BACK);//---重新初始化按键配置控件----

            Device.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            Device.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件
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
                        initLogicAndCommand();
                    }
                    else if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == PanelCtrl.CLASS_NAME)//---电机回路名称--
                    {
                        if (callbackParameter.Parameters[1].ToString() == PanelCtrl.ACTION_STATE_NAME)//------状态选择------
                        {
                            InitSelectIndex = (int)callbackParameter.Parameters[2];
                        }
                        else if (callbackParameter.Parameters[1].ToString() == PanelCtrl.ACTION_OPTION_NAME)
                        {
                            button2OptionData = callbackParameter.Parameters[2] as PanelOptionData; 
   
                            tbcLight.Value = button2OptionData.Luminance;                      //---亮度----
                            ceAlarmSound.Checked = button2OptionData.AlarmHintSound;           //---预警提示音---
                            ceDoorWindowSound.Checked = button2OptionData.DoorWindowHintSound; //---门窗提示音---
                            speSecurityDelay.Value = button2OptionData.SetSecurityDelayTime;   //---布防延时---
                            speAlarmDelay.Value = button2OptionData.AlarmDelayTime;            //---预警延时---
                            speHintVolume.Value = button2OptionData.HintVolume;                //---提示音量---
                            speSpeakerAddress.Value = button2OptionData.SoundAddress;          //---功放地址---
                            ceBackSafeSetting.Items[0].CheckState = button2OptionData.RemoveSafe ? CheckState.Checked : CheckState.Unchecked;//---回家撤防---- 
                            //------安防配置---------------
                            for (int i = 0; i < button2OptionData.SaftFlags.Length; i++)
                                ceLeaveSafeSetting.Items[i].CheckState = button2OptionData.SaftFlags[i] ? CheckState.Checked : CheckState.Unchecked;
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
            button2.KeyCtrl.ReadKeyOption();    //读取面板配置
            button2.KeyCtrl.ReadKeyState();     //读取状态 
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
            button2 = this.Device as PanelKey;                   //---本界面编辑-----    
            button2.OnCallbackUI_Action += this.callbackUI;     //--注册回调事件
            button2.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件

            this.Text = button2.Name;                   //---界面标题----
            keySettingTools.InitKeySettingList(button2, 8, ViewConfig.LCD_CAPTION_SCENE, ViewConfig.LCD_CAPTION_LIGHT,
                ViewConfig.LCD_CAPTION_CURTAIN, ViewConfig.LCD_CAPTION_LEAVE_BACK);//---重新初始化按键配置控件----
            viewBaseSetting.DeviceEdit.SearchVer();     //---获取版本号-----   
            InitSelectDevice();                         //---初始化选择设备---
            viewCommandEdit.NeedInit = true;            //---指令配置重新初始化,通过回调实现------      
            loadData();                                 //---加载数据----

        }

 
        /// <summary>
        /// 保存
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            //---保存面板配置-------
            PanelOptionData keySettingData = new PanelOptionData(button2OptionData.GetKeyOptionValue());

            keySettingData.Luminance = (byte)tbcLight.Value;                      //---亮度----
            keySettingData.AlarmHintSound = ceAlarmSound.Checked;           //---预警提示音---
            keySettingData.DoorWindowHintSound = ceDoorWindowSound.Checked; //---门窗提示音---
            keySettingData.SetSecurityDelayTime = (byte)speSecurityDelay.Value;   //---布防延时---
            keySettingData.AlarmDelayTime = (byte)speAlarmDelay.Value;            //---预警延时---
            keySettingData.HintVolume = (byte)speHintVolume.Value;                //---提示音量---
            keySettingData.SoundAddress = (byte)speSpeakerAddress.Value;          //---功放地址---
            keySettingData.RemoveSafe = ceBackSafeSetting.Items[0].CheckState == CheckState.Checked ? true : false;//---回家撤防---- 
            //---安防配置---------------
            bool[] safeFlags = new bool[] { false, false, false, false, false, false, false, false, false, false,
                    false, false, false, false, false };
            for (int i = 0; i < keySettingData.SaftFlags.Length; i++)   
                safeFlags[i] = ceLeaveSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
            keySettingData.SaftFlags = safeFlags;
            //---判断是否更改,更改执行保存----
            if (!CommonTools.BytesEuqals(keySettingData.GetKeyOptionValue(), button2OptionData.GetKeyOptionValue()))
                button2.KeyCtrl.SaveKeyOption(keySettingData);


            //---保存按键配置---------
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
            if (e.Index == 15)
            {
                if (ceLeaveSafeSetting.Items[15].CheckState == CheckState.Checked)
                {
                    for (int i = 0; i < button2OptionData.SaftFlags.Length; i++)
                        ceLeaveSafeSetting.Items[i].CheckState = CheckState.Checked;
                }
                if (ceLeaveSafeSetting.Items[15].CheckState == CheckState.Unchecked)
                {
                    for (int i = 0; i < button2OptionData.SaftFlags.Length; i++)
                        ceLeaveSafeSetting.Items[i].CheckState = CheckState.Unchecked;
                }
            }
        }


    }
}
