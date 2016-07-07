using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;

namespace ProgramTest2
{



    public partial class Form1 : Form, IMessageFilter
    {
        private bool limitedMouseWheel = false;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookupEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
         DataTable dt = new DataTable();
        DataTable dtSelect = new DataTable();
        public Form1()
        {
            InitializeComponent();

            DataSet ds = new DataSet("Test");
            ds.Tables.Add(dtSelect);

            dt.Columns.Add("time1", System.Type.GetType("System.String"));
            dt.Columns.Add("time2", System.Type.GetType("System.String"));
            dt.Columns.Add("week", System.Type.GetType("System.String"));
            dt.Columns.Add("select", System.Type.GetType("System.String"));
            dt.Columns.Add("num", System.Type.GetType("System.Int16"));

            dcTime1.FieldName = "time1";
            dcTime2.FieldName = "time2";
            num.FieldName = "num";
            dcSelect.FieldName = "select";
            dcWeek.FieldName = "week";

            dtSelect.Columns.Add("ID", System.Type.GetType("System.String"));
            dtSelect.Columns.Add("NAME", System.Type.GetType("System.String"));
            dtSelect.Columns.Add("KIND", System.Type.GetType("System.String"));
            dtSelect.Columns.Add("UniqueValue", System.Type.GetType("System.String"));
            dtSelect.Rows.Add("1", "名称1", "类型1", "1_名称1_类型1");
            dtSelect.Rows.Add("4", "名称2", "类型2", "4_名称2_类型2");
            dtSelect.Rows.Add("3", "名称3", "类型3", "3_名称3_类型3");
            dtSelect.Rows.Add("4", "名称4", "类型4", "4_名称4_类型4");
            dtSelect.Rows.Add("1", "名称1", "类型2", "1_名称1_类型2");
      
            lookupEdit.DataSource = dtSelect;
            lookupEdit.DisplayMember = "ID";
            lookupEdit.ValueMember = "ID";
          
            glookupEdit.DataSource = dtSelect;
            glookupEdit.DisplayMember = "NAME";
            glookupEdit.ValueMember = "UniqueValue";
            glookupEdit.View.OptionsView.ShowIndicator = false;
            glookupEdit.View.OptionsView.ShowGroupPanel = false;
            glookupEdit.View.OptionsView.ShowAutoFilterRow = true;
            glookupEdit.View.Columns.ColumnByFieldName("UniqueValue").Visible = false;
            glookupEdit.View.Columns.ColumnByFieldName("KIND").SortIndex = 0;
            glookupEdit.View.Columns.ColumnByFieldName("ID").SortIndex = 1;
            glookupEdit.View.BestFitColumns();

            lookUpEdit2.Properties.DataSource = dtSelect;
            lookUpEdit2.Properties.DisplayMember = "NAME";
            lookUpEdit2.Properties.ValueMember = "UniqueValue";
            lookUpEdit2.Properties.Columns.Add(new LookUpColumnInfo("ID"));
            lookUpEdit2.Properties.Columns.Add(new LookUpColumnInfo("NAME"));
            lookUpEdit2.Properties.Columns.Add(new LookUpColumnInfo("UniqueValue"));
            //lookUpEdit2.Properties.Columns.Add(new LookUpColumnInfo("UniqueValue"));
            //lookUpEdit2.Properties.Columns[3].Visible = false;

            gridLookUpEdit1.Properties.DataSource = dtSelect;
            gridLookUpEdit1.Properties.DisplayMember = "NAME";
            gridLookUpEdit1.Properties.ValueMember = "UniqueValue";          

            //----时间编辑控件------
            dateEdit.DisplayFormat.FormatString = "d";
            dateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.Mask.EditMask = "yyyy-MM-dd";
            dateEdit.Mask.UseMaskAsDisplayFormat = true; 

            Application.AddMessageFilter(this);

            ds.WriteXml("d:\\test.xml", XmlWriteMode.WriteSchema);

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
            temperatureEdit.Properties.DisplayFormat.FormatString = "#0 ℃";
            temperatureEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            temperatureEdit.Properties.Mask.EditMask = "#0 ℃";
            temperatureEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            temperatureEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            temperatureEdit.Properties.MaxValue = 60;
            temperatureEdit.Properties.MinValue = -20;
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
            
            lookUpEdit2.ItemIndex = 0;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int test = 65535;
            byte[] byteRunTime = System.BitConverter.GetBytes(test);
            //      byte[] byteRunTime = {0xdc ,0x3f};
            Array.Resize(ref byteRunTime, 4);
            int runTime = BitConverter.ToInt32(byteRunTime, 0);

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



        string weekValue = ""; private bool allowEdit = true;
        private void checkedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (!allowEdit) return;
            if (checkedComboBoxEdit1.Text != "")
            {
                if (checkedComboBoxEdit1.Text.Contains("星期"))
                {
                    weekValue = checkedComboBoxEdit1.Text;
                    checkedComboBoxEdit1.Text = weekValue;
                    checkedComboBoxEdit1.Text = checkedComboBoxEdit1.Text.Replace("星期", "");
                }
            }
            else
            {
                weekValue = ""; 
                checkedComboBoxEdit1.Text = "";
                checkedComboBoxEdit1.Text = checkedComboBoxEdit1.Text.Replace("星期", "");
            }

        }

        private void checkedComboBoxEdit1_Closed(object sender, ClosedEventArgs e)
        {
            allowEdit = false;
      
            if (checkedComboBoxEdit1.Text.Contains("星期"))
            {
                weekValue = checkedComboBoxEdit1.Text;
                checkedComboBoxEdit1.Text = weekValue;
                checkedComboBoxEdit1.Text = checkedComboBoxEdit1.Text.Replace("星期", "");
            }
            allowEdit = true;
        }



        private void checkedComboBoxEdit1_Properties_QueryPopUp(object sender, CancelEventArgs e)
        {
            allowEdit = false;
            if (checkedComboBoxEdit1.Text == "") return;

            string temp1 = checkedComboBoxEdit1.EditValue.ToString().Replace(", ", ", 星期");
            string temp2 = "星期" + temp1;
            checkedComboBoxEdit1.Text = temp2;
            allowEdit = true;
        }

        private void checkedComboBoxEdit1_QueryResultValue(object sender, QueryResultValueEventArgs e)
        {
            
        }


        private void lookUpEdit2_TextChanged(object sender, EventArgs e)
        {

        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEdit2.EditValue != null)
                MessageBox.Show(lookUpEdit2.EditValue.ToString());
        }

        private void lookUpEdit2_Closed(object sender, ClosedEventArgs e)
        {

        }

   


  



 
    }
}
