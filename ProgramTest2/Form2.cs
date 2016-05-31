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


            this.dcTriggerCondition.FieldName = "field1";
            this.dcInputCondition.FieldName = "field2";
            this.dcRangeStart.FieldName = "field3";
            this.dcRangeEnd.FieldName = "field4";
            this.dcContinue.FieldName = "field5";
            this.dcEnd.FieldName = "field6"; 
            dt.Rows.Add(new object[] { "text1", "row1", "row1" });
            dt.Rows.Add(new object[] { "text1", "row2", "row2" });
            dt.Rows.Add(new object[] { "text1", "row3", "row3" });
            dt.Rows.Add(new object[] { "text1", "row4", "row4" }); 

            gridControl1.DataSource = dt;
        }


    }
}
