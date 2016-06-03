using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;

namespace ProgramTest2
{
    public partial class Form1 : Form, IMessageFilter
    {
        private bool limitedMouseWheel = false;
        DataTable dt = new DataTable();
        DataTable dtSelect = new DataTable();
        public Form1()
        {
            InitializeComponent();

            dt.Columns.Add("time1", System.Type.GetType("System.String"));
            dt.Columns.Add("time2", System.Type.GetType("System.String"));
            dt.Columns.Add("select", System.Type.GetType("System.String"));
            dt.Columns.Add("num", System.Type.GetType("System.Int16"));

            dcTime1.FieldName = "time1";
            dcTime2.FieldName = "time2";
            num.FieldName = "num";
            dcSelect.FieldName = "select";

            dtSelect.Columns.Add("ID", System.Type.GetType("System.String"));
            dtSelect.Columns.Add("NAME", System.Type.GetType("System.String"));
            dtSelect.Rows.Add("1", "名称1");
            dtSelect.Rows.Add("2", "名称2");
            dtSelect.Rows.Add("3", "名称3");
            dtSelect.Rows.Add("4", "名称4");
 
            lookupEdit.DataSource = dtSelect;
            lookupEdit.DisplayMember = "ID";
            lookupEdit.ValueMember = "ID";

         
            //----时间编辑控件            
            dateEdit.DisplayFormat.FormatString = "d";
            dateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.Mask.EditMask = "yyyy-MM-dd";
            dateEdit.Mask.UseMaskAsDisplayFormat = true; 


            Application.AddMessageFilter(this);   
            
            
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

            textEdit1.Properties.DisplayFormat.FormatString = "#0.00%";
            textEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            textEdit1.Properties.Mask.EditMask = "#0.00%";
            textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            textEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            textEdit1.Name = "TextEdit";  

            gcTime.DataSource = dt;

 

            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "1", 1 });
            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "2", 2 });
            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "3", 3 });
            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "4", 4 });
            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "1", 1 });
            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "2", 2 });
            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "3", 3 });
            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "4", 4 });
            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "1", 1 });
            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "2", 2 });
            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "3", 3 });
            dt.Rows.Add(new object[] { "00:00:00", "1900-01-01", "4", 4 });
      
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

        /// <summary>
        /// 禁止鼠标滚动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edt_MouseWheel(object sender, MouseEventArgs e)
        {
            //if (e.Delta != 0)
            //    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }

        private void timeEdit_MouseWheel(object sender, MouseEventArgs e)
        {
            //if (e.Delta != 0)
            //    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }

        private void edtNum_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }



        #region IMessageFilter 成员

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 522)
            {
                if(limitedMouseWheel)
                    return true; 
            }
            return false;
        }

        #endregion

        private void Edit_Enter(object sender, EventArgs e)
        {
            limitedMouseWheel = true;
        }

        private void Edit_Leave(object sender, EventArgs e)
        {
            limitedMouseWheel = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            setGridColumnValid(dcTime2, dcTime1.ColumnEdit);
        }
        protected void setGridColumnValid(GridColumn gc, DevExpress.XtraEditors.Repository.RepositoryItem editor)
        {
            gc.ColumnEdit = editor;
            gc.AppearanceCell.BackColor = Color.LightYellow;//灰色
            gc.AppearanceCell.ForeColor = Color.Blue;
        }
    }
}
