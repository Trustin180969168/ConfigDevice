using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice
{
    public partial class ViewLogicSetting : UserControl
    {
        

        private bool Inited = false;
        public ViewLogicSetting()
        {
            InitializeComponent();
        }


        public void InitLogicList()
        {
            plLogicList.Controls.Add(new ViewLogicTools(1));
            Inited = true;
        }
    }
}
