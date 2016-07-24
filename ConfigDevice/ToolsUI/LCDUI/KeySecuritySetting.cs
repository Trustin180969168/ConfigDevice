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
            lookUpEditAmp.Properties.DataSource = dt;
            lookUpEditAmp.Properties.DisplayMember = DeviceConfig.DC_NAME;
            lookUpEditAmp.Properties.ValueMember = DeviceConfig.DC_ID;
        }

        /// <summary>
        /// 设置安防参数
        /// </summary>
        /// <param name="optionData"></param>
        public void SetOptionData(PanelOptionData optionData)
        {
            ceAlarmSound.Checked = optionData.AlarmHintSound;           //---预警提示音---
            ceDoorWindowSound.Checked = optionData.DoorWindowHintSound; //---门窗提示音---
            speSecurityDelay.Value = optionData.SetSecurityDelayTime;   //---布防延时---
            speAlarmDelay.Value = optionData.AlarmDelayTime;            //---预警延时---
          //  speHintVolume.Value = optionData.Volume;                //---提示音量---
          //  speAmp.Value = optionData.SoundAddress;                     //---功放地址---
          //  lookUpEditAmp.EditValue = optionData.SoundAddress;          //---功放名称---
            ceBackSafeSetting.Items[0].CheckState = optionData.RemoveSafe ? CheckState.Checked : CheckState.Unchecked;//---回家撤防---- 
            //------安防配置---------------
            for (int i = 0; i < optionData.SaftFlags.Length; i++)
                ceLeaveSafeSetting.Items[i].CheckState = optionData.SaftFlags[i] ? CheckState.Checked : CheckState.Unchecked; //---离家设防


        }

        /// <summary>
        /// 获取安防设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void GetOptionData(ref PanelOptionData optionData)
        {
            bool[] safeFlags = new bool[] { false, false, false, false, false, false, false, false, false, false,
                    false, false, false, false, false };
            for (int i = 0; i < optionData.SaftFlags.Length; i++)
                safeFlags[i] = ceLeaveSafeSetting.Items[i].CheckState == CheckState.Checked ? true : false;
            optionData.SaftFlags = safeFlags;                                                                   //---离家设防
            optionData.RemoveSafe = ceBackSafeSetting.Items[0].CheckState == CheckState.Checked ? true : false; //---回家撤防---- 
            optionData.SoundAddress = (byte)speAmp.Value;
        }


 
        /// <summary>
        /// 显示音量
        /// </summary> 
        private void speHintVolume_ValueChanged(object sender, EventArgs e)
        {
            lblSoundValue.Text = speHintVolume.Value.ToString();
        }



    }
}
