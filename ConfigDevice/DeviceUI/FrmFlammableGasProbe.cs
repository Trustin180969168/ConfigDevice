using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmFlammableGasProbe : ConfigDevice.FrmDevice
    {
        private bool autoRefresh = false;

        public FrmFlammableGasProbe(Device _device)
            : base(_device)
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
