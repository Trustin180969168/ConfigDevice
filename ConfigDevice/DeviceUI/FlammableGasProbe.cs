using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice.DeviceUI
{
    public partial class FlammableGasProbe : ConfigDevice.FrmDevice
    {
        private bool autoRefresh = false;

        public FlammableGasProbe()
        {
            InitializeComponent();
            viewCommandEdit.CbxCommandGroup.Visible = false;

        }


        private void btAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            autoRefresh = !autoRefresh;
            if (autoRefresh)
            {
                btAutoRefresh.Image = global::ConfigDevice.Properties.Resources.refresh1;
                btAutoRefresh.Checked = true;
            }
            else
            {
                btAutoRefresh.Image = global::ConfigDevice.Properties.Resources.refresh2;
                btAutoRefresh.Checked = false;
            }
        }
    }
}
