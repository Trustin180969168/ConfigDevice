using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice.ToolsUI.LCDUI
{
    public partial class KeySecuritySetting : UserControl
    {

        private Device device;

        public KeySecuritySetting()
        {
            InitializeComponent();
        }
 
        public void Init(Device _device)
        {
            device = _device;
            DataTable dt = SysConfig.DtDevice.Clone();
            DataRow[] amps = SysConfig.DtDevice.Select(DeviceConfig.DC_KIND_ID + "= '" + DeviceConfig.EQUIPMENT_AMP_MP3 + "' and " +
                DeviceConfig.DC_NETWORK_ID + " = '" + device.NetworkID + "'");
            foreach (DataRow dr in amps)
                dt.Rows.Add(dr.ItemArray);

            lookUpEditAmp.Properties.Columns.Clear();
            lookUpEditAmp.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DeviceConfig.DC_NAME, "功放", 120));
            lookUpEditAmp.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DeviceConfig.DC_ID, "地址", 120));
            lookUpEditAmp.Properties.DataSource = dt; 
            lookUpEditAmp.Properties.DisplayMember = DeviceConfig.DC_NAME;
            lookUpEditAmp.Properties.ValueMember = DeviceConfig.DC_ID; 
 
            lookUpEditAmp.Properties.DropDownRows = dt.Rows.Count; 
           
        }

        /// <summary>
        /// 设置安防参数
        /// </summary>
        /// <param name="optionData"></param>
        public void SetOptionData(LCDPanelOptionData optionData)
        {
            ceAlarmSound.Checked = optionData.AlarmHintSound;           //---预警提示音---
            ceDoorWindowSound.Checked = optionData.DoorWindowHintSound; //---门窗提示音---
            speSecurityDelay.Value = optionData.SetSecurityDelayTime;   //---布防延时---
            speAlarmDelay.Value = optionData.AlarmDelayTime;            //---预警延时---
            chkDoorWindowShow.Checked = optionData.DoorWindowShowing;//---门窗显示
            speHintVolume.Value = optionData.Volume;                //---提示音量---
            speAmp.Value = optionData.SoundAddress;                     //---功放地址---
            lookUpEditAmp.EditValue = (Int16)optionData.SoundAddress;          //---功放名称---
        
            ceBackSafeSetting.Items[0].CheckState = optionData.RemoveSafe ? CheckState.Checked : CheckState.Unchecked;//---回家撤防---- 
            //------安防配置---------------
            for (int i = 0; i < optionData.SaftFlags.Length; i++)
                ceLeaveSafeSetting.Items[i].CheckState = optionData.SaftFlags[i] ? CheckState.Checked : CheckState.Unchecked; //---离家设防

        }

        /// <summary>
        /// 设置二按键安防参数
        /// </summary>
        /// <param name="optionData"></param>
        public void SetOptionData(ButtonPanelOptionData button2OptionData)
        {
            ceAlarmSound.Checked = button2OptionData.AlarmHintSound;           //---预警提示音---
            ceDoorWindowSound.Checked = button2OptionData.DoorWindowHintSound; //---门窗提示音---
            speSecurityDelay.Value = button2OptionData.SetSecurityDelayTime;   //---布防延时---
            speAlarmDelay.Value = button2OptionData.AlarmDelayTime;            //---预警延时---
            speHintVolume.Value = button2OptionData.Volume;                //---提示音量---
            speAmp.Text = button2OptionData.SoundAddress.ToString();          //---功放地址---
            lookUpEditAmp.Text = button2OptionData.SoundAddress.ToString();         //---功放地址---
            ceBackSafeSetting.Items[0].CheckState = button2OptionData.RemoveSafe ? CheckState.Checked : CheckState.Unchecked;//---回家撤防---- 
            //------安防配置---------------
            for (int i = 0; i < button2OptionData.SaftFlags.Length; i++)
                ceLeaveSafeSetting.Items[i].CheckState = button2OptionData.SaftFlags[i] ? CheckState.Checked : CheckState.Unchecked;
        }


        /// <summary>
        /// 获取二按键安防设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void GetOptionData(ref ButtonPanelOptionData button2OptionData)
        {
            
            button2OptionData.AlarmHintSound = ceAlarmSound.Checked;           //---预警提示音---
            button2OptionData.DoorWindowHintSound = ceDoorWindowSound.Checked; //---门窗提示音---
            button2OptionData.SetSecurityDelayTime = (byte)speSecurityDelay.Value;   //---布防延时---
            button2OptionData.AlarmDelayTime = (byte)speAlarmDelay.Value;            //---预警延时--- 
            button2OptionData.Volume = (byte)speHintVolume.Value;                //---提示音量--- 
            button2OptionData.SoundAddress = (byte)speAmp.Value;          //---功放地址--- 
            button2OptionData.RemoveSafe = ceBackSafeSetting.Items[0].CheckState == CheckState.Checked ? true : false;//---回家撤防----  

            //---安防配置---------------
            bool[] safeFlags = new bool[] { false, false, false, false, false, false, false, false, false, false,
                    false, false, false, false, false };
            for (int i = 0; i < button2OptionData.SaftFlags.Length; i++)
                safeFlags[i] = ceLeaveSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
            button2OptionData.SaftFlags = safeFlags;
        }

        /// <summary>
        /// 获取安防设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void GetOptionData(ref LCDPanelOptionData optionData)
        {
            //optionData.AlarmHintSound = ceAlarmSound.Checked;//预警提示音
            //optionData.DoorWindowHintSound = ceDoorWindowSound.Checked;//门窗提示音
            ////-------安防配置-----
            //bool[] safeFlags = new bool[] { false, false, false, false, false, false, false, false, false, false,
            //        false, false, false, false, false };
            //for (int i = 0; i < ceLeaveSafeSetting.Items.Count - 1; i++)
            //    safeFlags[i] = ceLeaveSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
            //optionData.SaftFlags = safeFlags;                                                                   //---离家设防
            //optionData.RemoveSafe = ceBackSafeSetting.Items[0].CheckState == CheckState.Checked ? true : false; //---回家撤防---- 
            //optionData.SoundAddress = (byte)speAmp.Value;
            optionData.AlarmHintSound = ceAlarmSound.Checked;           //---预警提示音---
            optionData.DoorWindowHintSound = ceDoorWindowSound.Checked; //---门窗提示音---
            optionData.SetSecurityDelayTime = (byte)speSecurityDelay.Value;   //---布防延时---
            optionData.AlarmDelayTime = (byte)speAlarmDelay.Value;            //---预警延时---
            optionData.DoorWindowShowing = chkDoorWindowShow.Checked;//---门窗显示
            optionData.Volume = (byte)speHintVolume.Value;                //---提示音量--- 
            optionData.SoundAddress = (byte)speAmp.Value;          //---功放地址---
            optionData.RemoveSafe = ceBackSafeSetting.Items[0].CheckState == CheckState.Checked ? true : false;//---回家撤防----  

            //---安防配置---------------
            bool[] safeFlags = new bool[] { false, false, false, false, false, false, false, false, false, false,
                    false, false, false, false, false };
            for (int i = 0; i < optionData.SaftFlags.Length; i++)
                safeFlags[i] = ceLeaveSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
            optionData.SaftFlags = safeFlags;
        }


        /// <summary>
        /// 获取安防设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void SetOptionData(ref SpecialPanelOptionData optionData)
        {
            //optionData.AlarmHintSound = ceAlarmSound.Checked;//预警提示音
            //optionData.DoorWindowHintSound = ceDoorWindowSound.Checked;//门窗提示音
            ////-------安防配置-----
            //bool[] safeFlags = new bool[] { false, false, false, false, false, false, false, false, false, false,
            //        false, false, false, false, false };
            //for (int i = 0; i < ceLeaveSafeSetting.Items.Count - 1; i++)
            //    safeFlags[i] = ceLeaveSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
            //optionData.SaftFlags = safeFlags;                                                                   //---离家设防
            //optionData.RemoveSafe = ceBackSafeSetting.Items[0].CheckState == CheckState.Checked ? true : false; //---回家撤防---- 
            //optionData.SoundAddress = (byte)speAmp.Value;
            optionData.AlarmHintSound = ceAlarmSound.Checked;           //---预警提示音---
            optionData.DoorWindowHintSound = ceDoorWindowSound.Checked; //---门窗提示音---
            optionData.SetSecurityDelayTime = (byte)speSecurityDelay.Value;   //---布防延时---
            optionData.AlarmDelayTime = (byte)speAlarmDelay.Value;            //---预警延时---
            optionData.DoorWindowShowing = chkDoorWindowShow.Checked;//---门窗显示
            optionData.Volume = (byte)speHintVolume.Value;                //---提示音量--- 
            optionData.SoundAddress = (byte)speAmp.Value;          //---功放地址---
            optionData.RemoveSafe = ceBackSafeSetting.Items[0].CheckState == CheckState.Checked ? true : false;//---回家撤防----  

            //---安防配置---------------
            bool[] safeFlags = new bool[] { false, false, false, false, false, false, false, false, false, false,
                    false, false, false, false, false };
            for (int i = 0; i < optionData.SaftFlags.Length; i++)
                safeFlags[i] = ceLeaveSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
            optionData.SaftFlags = safeFlags;
        }
 
        /// <summary>
        /// 显示音量
        /// </summary> 
        private void speHintVolume_ValueChanged(object sender, EventArgs e)
        {
            lblSoundValue.Text = speHintVolume.Value.ToString();
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
                    for (int i = 0; i < ceLeaveSafeSetting.Items.Count - 1; i++)
                        ceLeaveSafeSetting.Items[i].CheckState = CheckState.Checked;
                }
                if (ceLeaveSafeSetting.Items[15].CheckState == CheckState.Unchecked)
                {
                    for (int i = 0; i < ceLeaveSafeSetting.Items.Count - 1; i++)
                        ceLeaveSafeSetting.Items[i].CheckState = CheckState.Unchecked;
                }
            }
        }

        /// <summary>
        /// 选择功放
        /// </summary>
        private void lookUpEditAmp_EditValueChanged(object sender, EventArgs e)
        {
            speAmp.EditValue = lookUpEditAmp.EditValue;
        }

    }
}
