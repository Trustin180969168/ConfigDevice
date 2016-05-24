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
        DataTable dtSelect = new DataTable();
        public Form1()
        {
            InitializeComponent();

            dt.Columns.Add("time1", System.Type.GetType("System.String"));
            dt.Columns.Add("time2", System.Type.GetType("System.String"));
            dt.Columns.Add("text", System.Type.GetType("System.String"));
            dt.Columns.Add("select", System.Type.GetType("System.String"));
            dcTime1.FieldName = "time1";
            dcTime2.FieldName = "time2";
            text.FieldName = "text";
            dcSelect.FieldName = "select";

            dtSelect.Columns.Add("ID", System.Type.GetType("System.String"));
            dtSelect.Columns.Add("NAME", System.Type.GetType("System.String"));
            dtSelect.Rows.Add( "1","名称1");
            dtSelect.Rows.Add("2", "名称2");
            dtSelect.Rows.Add("3", "名称3");
            dtSelect.Rows.Add("4", "名称4");
            dtSelect.Rows.Add("5", "名称5");

            lookupEdit.DataSource = dtSelect;
            lookupEdit.DisplayMember = "ID";
            lookupEdit.ValueMember = "ID";
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
            dt.Rows.Add(new object[] { "00:00:00", "00:00:00" });
            dt.Rows.Add(new object[] { "00:00:00", "00:00:00" });
            dt.Rows.Add(new object[] { "00:00:00", "00:00:00" });
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
