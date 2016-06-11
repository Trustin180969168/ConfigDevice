using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmDevice : Form,IMessageFilter   
    {
        public Device Device;
        public ToolStripComboBox CbxSelectDevice { get { return (cbxSelectDevice as ToolStripComboBox); } }
        public Dictionary<int, DataRow> SelectDeviceList = new Dictionary<int, DataRow>();//---列表---
        public bool loaded = false;//是否加载完毕

        public FrmDevice(Device _device)
        {
            this.Device = _device;      
            InitializeComponent();
      
        }

        public FrmDevice()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
        }

        /// <summary>
        /// 回调
        /// </summary>
        /// <param name="callbackParameter"></param>
        public virtual void CallbackUI(CallbackParameter callbackParameter)
        {

        }

        /// <summary>
        /// 初始化列表
        /// </summary>
        public void InitSelectDevice()
        {
            if (SelectDeviceList.Count > 0) return;
            string temp = DeviceConfig.DC_KIND_ID + "='" + Device.KindID + "'";
            DataRow[] rows = SysConfig.DtDevice.Select(temp);
            int i = 0;
            foreach (DataRow dr in rows)
            {
                cbxSelectDevice.Items.Add("设备ID:"+dr[DeviceConfig.DC_ID].ToString()+
                    "  设备类型:"+dr[DeviceConfig.DC_KIND_NAME].ToString() + "  设备名称:"+dr[DeviceConfig.DC_NAME].ToString());           
                SelectDeviceList.Add(i++, dr);
            }
            //cbxSelectDevice.Text = Device.Name;
        }

        /// <summary>
        /// 供子类覆盖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void cbxSelectDevice_SelectedIndexChanged(object sender, EventArgs e)
        {


            
        }

        /// <summary>
        /// 设置选择列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public void SetSelectDevice( )
        {
            for (int i = 0; i < CbxSelectDevice.Items.Count; i++)
            {
                if (CbxSelectDevice.Items[0].ToString().Contains(this.Device.Name) &&
                    CbxSelectDevice.Items[0].ToString().Contains(this.Device.DeviceID))
                { CbxSelectDevice.SelectedIndex = i; break; }
            }
        }


        #region IMessageFilter 成员

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 522)
            {
                if (SysConfig.LimitMouseWheel)
                    return true;
            }
            return false;
        }

        #endregion

        /// <summary>
        /// 单击后启动事件订阅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxSelectDevice_Click(object sender, EventArgs e)
        {
            if (!loaded)
            {
                loaded = true;
                cbxSelectDevice.SelectedIndexChanged += new EventHandler(cbxSelectDevice_SelectedIndexChanged);
            }
        }
    }
}
