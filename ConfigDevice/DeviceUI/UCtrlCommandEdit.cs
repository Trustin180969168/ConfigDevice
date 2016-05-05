using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ConfigDevice.DeviceUI
{
    public partial class UCtrlCommandEdit : UserControl
    {
        public string CommandGroupName { set { this.lblGroupName.Text = value; } }
        public ComboBoxEdit CbxCommandGroup { get { return cbxGroup; } }

        public UCtrlCommandEdit()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 选择命令组
        /// </summary>
        private void cbxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
