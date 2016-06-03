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
        public bool Inited = false;

        public ViewLogicSetting()
        {
            InitializeComponent();
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic4OR);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic4AND);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic3OR_1AND);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic3AND_1OR);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic2OR_2AND_OR);
            imageCollection1.AddImage(global::ConfigDevice.Properties.Resources.logic2OR_2AND_AND);
            Inited = false;
        }


        public void InitLogicList(params string[] triggers)
        {
            foreach (Control viewlogic in plLogicList.Controls)
                (viewlogic as ViewLogicTools).InitTriggers(triggers);
            Inited = true;
        }




    }
}
