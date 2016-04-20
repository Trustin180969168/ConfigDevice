using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class FrmDevice : Form
    {
        public DeviceData Device;

        public FrmDevice(DeviceData _device)
        {
            this.Device = _device;
            InitializeComponent();
        }

        public FrmDevice()
        {
            InitializeComponent();
        }
    }
}
