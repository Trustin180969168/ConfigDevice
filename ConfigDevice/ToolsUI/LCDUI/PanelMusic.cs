using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class PanelMusic : UserControl
    {

        private Device device;

        public PanelMusic()
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
 
            speAmp.Value = optionData.SoundAddress;                     //---功放地址---
            lookUpEditAmp.EditValue = optionData.SoundAddress;          //---功放名称---  


        }

        /// <summary>
        /// 获取安防设置参数
        /// </summary>
        /// <param name="optionData"></param>
        public void GetOptionData(ref PanelOptionData optionData)
        {
 
            optionData.SoundAddress = (byte)speAmp.Value;
        }
 


    }
}
