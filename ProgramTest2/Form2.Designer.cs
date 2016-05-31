namespace ProgramTest2
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcField1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcField2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcField3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcField4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcField5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcField6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.FormsUseDefaultLookAndFeel = false;
            this.gridControl1.Location = new System.Drawing.Point(0, 19);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(784, 286);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcField1,
            this.gcField2,
            this.gcField3,
            this.gcField4,
            this.gcField5,
            this.gcField6});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.AllowCellMerge = true;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gcField1
            // 
            this.gcField1.Caption = "gridColumn1";
            this.gcField1.Name = "gcField1";
            this.gcField1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcField1.Visible = true;
            this.gcField1.VisibleIndex = 0;
            // 
            // gcField2
            // 
            this.gcField2.Caption = "gridColumn2";
            this.gcField2.Name = "gcField2";
            this.gcField2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcField2.Visible = true;
            this.gcField2.VisibleIndex = 1;
            // 
            // gcField3
            // 
            this.gcField3.Caption = "gridColumn3";
            this.gcField3.Name = "gcField3";
            this.gcField3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcField3.Visible = true;
            this.gcField3.VisibleIndex = 2;
            // 
            // gcField4
            // 
            this.gcField4.Caption = "gridColumn4";
            this.gcField4.Name = "gcField4";
            this.gcField4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcField4.Visible = true;
            this.gcField4.VisibleIndex = 3;
            // 
            // gcField5
            // 
            this.gcField5.Caption = "gridColumn5";
            this.gcField5.Name = "gcField5";
            this.gcField5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcField5.Visible = true;
            this.gcField5.VisibleIndex = 4;
            // 
            // gcField6
            // 
            this.gcField6.Caption = "gridColumn6";
            this.gcField6.Name = "gcField6";
            this.gcField6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcField6.Visible = true;
            this.gcField6.VisibleIndex = 5;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(96, 96);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // checkEdit1
            // 
            this.checkEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkEdit1.Location = new System.Drawing.Point(0, 0);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "all merge";
            this.checkEdit1.Size = new System.Drawing.Size(784, 19);
            this.checkEdit1.TabIndex = 1;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            this.checkEdit1.Click += new System.EventHandler(this.checkEdit1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 487);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.checkEdit1);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gcField1;
        private DevExpress.XtraGrid.Columns.GridColumn gcField2;
        private DevExpress.XtraGrid.Columns.GridColumn gcField3;
        private DevExpress.XtraGrid.Columns.GridColumn gcField4;
        private DevExpress.XtraGrid.Columns.GridColumn gcField5;
        private DevExpress.XtraGrid.Columns.GridColumn gcField6;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
    }
}