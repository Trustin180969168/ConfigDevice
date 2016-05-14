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
    public partial class Form1 : Form
    {

        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();

            dt.Columns.Add("time1", System.Type.GetType("System.String"));
            dt.Columns.Add("time2", System.Type.GetType("System.String"));
            dcTime1.FieldName = "time1";
            dcTime2.FieldName = "time2";


            
        }
 int i = 1;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.Rows[0];
            dr["time1"] = DateTime.Parse(DateTime.Now.ToShortDateString()).AddSeconds(i).ToLongTimeString();
            dr["time2"] = DateTime.Parse(DateTime.Now.ToShortDateString()).AddSeconds(i).ToLongTimeString();
            i++;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gcTime.DataSource = dt;
            dt.Rows.Add(new object[]{"00:00:00","00:00:00"});
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int test = 65535;
            byte[] byteRunTime = System.BitConverter.GetBytes(test);
      //      byte[] byteRunTime = {0xdc ,0x3f};
            Array.Resize(ref byteRunTime, 4);
            int runTime = BitConverter.ToInt32(byteRunTime, 0);
            textEdit1.Text = runTime.ToString();
        }




    }
}
