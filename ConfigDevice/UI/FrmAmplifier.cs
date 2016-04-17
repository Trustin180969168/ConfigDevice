using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmAmplifier : FrmDevice
    { 

        public FrmAmplifier(DeviceData _device):base(_device)
        {
            this.Device = _device;
            InitializeComponent();
        }
    }
}
