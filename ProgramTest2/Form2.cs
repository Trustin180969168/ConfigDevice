using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramTest2
{
    public partial class Form2 : Form
    {
        DataTable dt = new DataTable();
        public Form2()
        {
            InitializeComponent();

            dt.Columns.Add("field1", System.Type.GetType("System.String"));
            dt.Columns.Add("field2", System.Type.GetType("System.String"));
            dt.Columns.Add("field3", System.Type.GetType("System.String"));
            dt.Columns.Add("field4", System.Type.GetType("System.String"));
            dt.Columns.Add("field5", System.Type.GetType("System.String"));
            dt.Columns.Add("field6", System.Type.GetType("System.String"));
            dt.Columns.Add("field7", System.Type.GetType("System.String"));
            dt.Columns.Add("field8", System.Type.GetType("System.String"));


            gcField1.FieldName = "field1";
            gcField2.FieldName = "field2";
            gcField3.FieldName = "field3";
            gcField4.FieldName = "field4";
            gcField5.FieldName = "field5";
            gcField6.FieldName = "field6";
            byte[] byteImage1 = ImageHelper.ImageToBytes(imageCollection1.Images[0]);
            byte[] byteImage2 = ImageHelper.ImageToBytes(imageCollection1.Images[1]);
            dt.Rows.Add(new object[] { "text1", "row1", "row1" });
            dt.Rows.Add(new object[] { "text1", "row2", "row2" });
            dt.Rows.Add(new object[] { "text1", "row3", "row3" });
            dt.Rows.Add(new object[] { "text1", "row4", "row4" });
            dt.Rows.Add(new object[] { "text1", "row5", "row5" });
            dt.Rows.Add(new object[] { "text1", "row6", "row6" });
            dt.Rows.Add(new object[] { "text1", "row7", "row7" });
            dt.Rows.Add(new object[] { "text1", "row8", "row8" });

            gridControl1.DataSource = dt;
        }

        private void checkEdit1_Click(object sender, EventArgs e)
        {

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
                gcField1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            else
                gcField1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
        }
    }
}
