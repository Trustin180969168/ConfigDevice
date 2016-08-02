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
        private LCDPanelKey panelKey;
        private DataTable dtCircuit = new DataTable("按键选择");
        private LCDPanelOptionData panelOptionData;//---面板配置---- 
        public FrmFunctionsBoard(Device _device)
            : base(_device)
        {          
            InitializeComponent();
            panelKey = this.Device as LCDPanelKey;
            panelKey.Circuit.CircuitCount = 26;
            viewCommandEdit.ShowToolBar = true;
            viewCommandEdit.ShowCommandBar = true;

            pageJcsz.Tag = false;
            pageJmpz.Tag = false;
            pageLight.Tag = false;
            pageLock.Tag = false;
            pageMusic.Tag = false;
            pageScene.Tag = false;
            pageLeaveBack.Tag = true;
            pageCurTain.Tag = false;
            listLeaveBack.InitKeySettingList(panelKey, 24, 2);
            listScene.InitKeySettingList(panelKey, 0, 8);//---场景---
            listLight.InitKeySettingList(panelKey, 8, 8);
            listCurtain.InitKeySettingList(panelKey, 16, 8);
            listEnvironment.Init();     //----初始化环境---
            panelMusic.Init(panelKey);//音乐面板
            keySecuritySetting.Init(panelKey);
        }

 

        private void FrmBaseDevice_Load(object sender, EventArgs e)
        {
            //keySettingTools.InitKeySettingList(button2, 8, ViewConfig.LCD_CAPTION_SCENE, ViewConfig.LCD_CAPTION_LIGHT,
            //    ViewConfig.LCD_CAPTION_CURTAIN, ViewConfig.LCD_CAPTION_LEAVE_BACK);//---重新初始化按键配置控件----

            Device.OnCallbackUI_Action += this.callbackUI;//--注册回调事件
            Device.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件
            viewBaseSetting.DeviceEdit = this.Device;//----配置编辑对象----
            viewBaseSetting.DeviceEdit.SearchVer();//---获取版本号-----   
            InitSelectDevice();//----初始化选择设备---
            listLeaveBack.ReadKeyData();//----读取数据---
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
                    else if (callbackParameter.Parameters != null && callbackParameter.Parameters[0].ToString() == LCDPanelCtrl.CLASS_NAME)//---电机回路名称--
                    {
                        if (callbackParameter.Parameters[1].ToString() == LCDPanelCtrl.ACTION_STATE_NAME)//------状态选择------
                        {
                   
                        }
                        else if (callbackParameter.Parameters[1].ToString() == LCDPanelCtrl.ACTION_OPTION_NAME)
                        {
                            panelOptionData = callbackParameter.Parameters[2] as LCDPanelOptionData;
                            keySecuritySetting.SetOptionData(panelOptionData);

                            tbcSleep.Value = panelOptionData.StandbyLight;//---待机亮度
                            tbcRun.Value = panelOptionData.Luminance;//---运行亮度
                            tbcLight.Value = panelOptionData.PointLightLuminance;//---指示灯亮度
                            speSleepTime.Value = panelOptionData.StandbyTime;//---待机时间
                            ceTimeScreenProtect.Checked = panelOptionData.OpenScreenProtect;//---开启时间屏保
                            ceRedLine.Checked = panelOptionData.OpenRedLine;//---开启红外线
                            cbxShowContent.SelectedIndex = panelOptionData.DoorWindowShowAllID;//---门窗显示设置
                            cbxShowModel.SelectedIndex = panelOptionData.DoorWindowShowModelID;//---全部房间显示设置
                            ceIfOpenMusic.Checked = panelOptionData.NotCloseWindowPlayMusic;//---没关，没锁门是否播放音乐----
                            listEnvironment.SetOptionData(ref panelOptionData);//----环境-----
                            panelMusic.SetOptionData(ref panelOptionData);//----音乐面板----
                            //------密码页配置---------------
                            for (int i = 0; i < celPassword.Items.Count; i++)
                                this.celPassword.Items[i].CheckState = panelOptionData.SaftPageFlags[i] ? CheckState.Checked : CheckState.Unchecked; //---密码页
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
            panelKey.Circuit.ReadRoadTitle();    //读取回路,完毕后自动读取第一个列按键
            panelKey.PanelCtrl.ReadKeyOption();    //读取面板配置 
        }

        /// <summary>
        /// 初始化逻辑和指令配置
        /// </summary>
        private void initLogicAndCommand()
        {
            viewCommandEdit.CommmandGroups.Clear();
            foreach (int key in panelKey.Circuit.ListCircuitIDAndName.Keys)
                viewCommandEdit.CommmandGroups.Add(panelKey.Circuit.ListCircuitIDAndName[key]);    //---指令组选择---- 
            if (viewCommandEdit.NeedInit)
            {
                viewCommandEdit.InitViewCommand(panelKey);
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
            if (panelKey.MAC == DeviceSelect.MAC) return;

            viewBaseSetting.DeviceEdit = DeviceSelect;          //---基础配置编辑  
            this.Device = DeviceSelect;                         //---父类设备对象-----              
            panelKey = this.Device as LCDPanelKey;                   //---本界面编辑-----    
            panelKey.OnCallbackUI_Action += this.callbackUI;     //--注册回调事件
            panelKey.OnCallbackUI_Action += viewBaseSetting.CallBackUI;//----注册回调事件

            this.Text = panelKey.Name;                   //---界面标题----
            viewBaseSetting.DeviceEdit.SearchVer();     //---获取版本号-----   
            InitSelectDevice();                         //---初始化选择设备---
            viewCommandEdit.NeedInit = true;            //---指令配置重新初始化,通过回调实现------ 
            //----各个页面设置未加载----
            pageJcsz.Tag = false;
            pageJmpz.Tag = false;
            pageLight.Tag = false;
            pageLock.Tag = false;
            pageMusic.Tag = false;
            pageScene.Tag = false;
            pageLeaveBack.Tag = false;
            pageCurTain.Tag = false;
            listLeaveBack.InitKeySettingList(panelKey, 24, 2);//----离家,回家
            listScene.InitKeySettingList(panelKey, 0, 8);//---场景---
            listLight.InitKeySettingList(panelKey, 8, 8);//----灯光-----
            listCurtain.InitKeySettingList(panelKey, 16, 8);//---窗帘
            listEnvironment.Init();     //----初始化环境---
            panelMusic.Init(panelKey);//音乐面板
            keySecuritySetting.Init(panelKey);
            
            loadData();//---加载数据(回路名称,及面板配置)-----
            xtcPage_SelectedPageChanged(sender, null);//---加载当前页数据---
        }

 
        /// <summary>
        /// 保存
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {

            //---系统配置配置-------
            LCDPanelOptionData panelOptionDataValue = new LCDPanelOptionData(panelOptionData.GetPanelOptionValue());
            panelOptionDataValue.PointLightLuminance = (byte)tbcLight.Value;        //---指示灯亮度----
            panelOptionDataValue.Luminance = (byte)this.tbcRun.Value;       //---运行亮度
            panelOptionDataValue.StandbyLight = (byte)this.tbcSleep.Value;    //---待机亮度
            panelOptionDataValue.StandbyTime = (byte)this.speSleepTime.Value;//---待机时间
            panelOptionDataValue.OpenScreenProtect = ceTimeScreenProtect.Checked;//---开启时间屏保
            panelOptionDataValue.OpenRedLine = ceRedLine.Checked;//---开启时间屏保
            panelOptionDataValue.DoorWindowShowAllID = cbxShowContent.SelectedIndex;//---门窗显示设置
            panelOptionDataValue.DoorWindowShowModelID = cbxShowModel.SelectedIndex;//---全部房间显示设置
            panelOptionDataValue.NotCloseWindowPlayMusic = ceIfOpenMusic.Checked;//---没关，没锁门是否播放音乐
            //------密码页配置---------------
            bool[] flags = new bool[] { false, false, false, false, false, false, false, false, 
            false, false, false, false, false, false, false, false};
            for (int i = 0; i < celPassword.Items.Count; i++)
                flags[i] = celPassword.Items[i].CheckState == CheckState.Checked ? true : false;
            panelOptionDataValue.SaftPageFlags = flags;
            listEnvironment.GetOptionData(ref panelOptionDataValue);//---环境页
            keySecuritySetting.GetOptionData(ref panelOptionDataValue);//------安全页----

            //---判断是否更改,更改执行保存----
            if (!CommonTools.BytesEuqals(panelOptionDataValue.GetPanelOptionValue(), panelOptionData.GetPanelOptionValue()))
                panelKey.PanelCtrl.SaveKeyOption(panelOptionDataValue);

            //*************************按键列表**************************
            listCurtain.SaveKeyData();//---窗帘页---
            listScene.SaveKeyData();//---场景页----
            listLight.SaveKeyData();//----灯光页----
            listLeaveBack.SaveKeyData();//---离家回家页----
            listLeaveBack.SaveKeyData();//---保存按键配置---------
            
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void btRefresh_Click(object sender, EventArgs e)
        {            
            panelKey.Circuit.ReadRoadTitle();    //读取回路,完毕后自动读取第一个列按键
            panelKey.PanelCtrl.ReadKeyOption();    //读取面板配置
            //-----触发刷新----
            pageJcsz.Tag = false;
            pageJmpz.Tag = false;
            pageLight.Tag = false;
            pageLock.Tag = false;
            pageMusic.Tag = false;
            pageScene.Tag = false;
            pageLeaveBack.Tag = false;
            pageCurTain.Tag = false;
                        
            xtcPage_SelectedPageChanged(sender, null);
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
            lblRunLum.Text = (tbcLight.Value * 10).ToString() + "%";
        }
 


        private void keyBaseSetting1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


        private void xtcPage_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (xtcPage.SelectedTabPageIndex)
            {
                case 0: if (!(bool)(this.pageLeaveBack.Tag))
                        this.listLeaveBack.ReadKeyData(); pageLeaveBack.Tag = true; break;
                case 1: if (!(bool)(pageScene.Tag))
                        listScene.ReadKeyData(); pageScene.Tag = true; break;
                case 2: if (!(bool)(pageLight.Tag))
                        listLight.ReadKeyData(); pageLight.Tag = true; break;
                case 3: if (!(bool)pageCurTain.Tag)
                        listCurtain.ReadKeyData(); pageCurTain.Tag = true; break;

                //case 5: if (!(bool)pageEnvironment.Tag)
                //    {
                //        listEnvironment.SetOptionData(panelOptionData);
                //        pageEnvironment.Tag = true;
                //    } break;
                default: break;
            }
        }

        private void tbcLight_ValueChanged(object sender, EventArgs e)
        {
            lblPointLight.Text = tbcLight.Value.ToString();
        }

        private void tbcRun_ValueChanged(object sender, EventArgs e)
        {
            lblRunLum.Text = tbcRun.Value.ToString();
        }

        private void tbcSleep_ValueChanged(object sender, EventArgs e)
        {
            lblSleepLum.Text = tbcSleep.Value.ToString();
        }


    }
}
