using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice.Tools
{
    public partial class ViewCommandTools : UserControl
    {
        public ControlObj currentControlObj;//---控制对象-----
        public ViewControl viewControl;// ----视图控制----

        public ViewCommandTools()
        {
            InitializeComponent();
        }

        private void gvDevices_DoubleClick(object sender, EventArgs e)
        {
            SelectDevice select = new SelectDevice();
            if (select.ShowDialog() == DialogResult.Yes)
            {

            }
        }

        /// <summary>
        /// 选择指令种类
        /// </summary>
        private void cbxBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 清空指令配置
        /// </summary>
        private void linkEdit_Click(object sender, EventArgs e)
        {

        }





    }
}
