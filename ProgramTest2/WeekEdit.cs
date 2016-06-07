
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Drawing;

namespace ProgramTest2
{

    public class WeekCheckedComboBoxEdit : CheckedComboBoxEdit
    {
        TextEdit valueEdit = new TextEdit();
        public WeekCheckedComboBoxEdit()
            : base()
        {
            this.Name = "WeekEdit";
            Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期一"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期二"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期三"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期四"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期五"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期六"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期日")});
            Properties.ShowAllItemCaption = "全选";

            this.Paint += new System.Windows.Forms.PaintEventHandler(checkedComboBoxEdit_Paint);
            this.Click += this.click;
            valueEdit.BackColor = Color.LightYellow;
            //this.checkedComboBoxEdit1.Size = new System.Drawing.Size(283, 21);
            //this.checkedComboBoxEdit1.Location = new System.Drawing.Point(137, 193);
        }


        private void checkedComboBoxEdit_Paint(object sender, PaintEventArgs e)
        {
            valueEdit.Size = this.Size;
            valueEdit.Location = this.Location;
    
        }

        private void click(object sender, EventArgs e)
        {

            this.Visible = false;
            valueEdit.Visible = true;
            //this.BringToFront();

            //valueEdit.BringToFront();
            //this.Parent.BringToFront();
        }
    }

      



}
