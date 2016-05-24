namespace ProgramTest2
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.timeEdit1 = new DevExpress.XtraEditors.TimeEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gcTime = new DevExpress.XtraGrid.GridControl();
            this.gvTime = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dcTime1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.timeEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.dcTime2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.text = new DevExpress.XtraGrid.Columns.GridColumn();
            this.meEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.dcSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookupEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.meeEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meeEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // timeEdit1
            // 
            this.timeEdit1.EditValue = new System.DateTime(2016, 5, 11, 0, 0, 0, 0);
            this.timeEdit1.Location = new System.Drawing.Point(139, 34);
            this.timeEdit1.Name = "timeEdit1";
            this.timeEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEdit1.Size = new System.Drawing.Size(100, 21);
            this.timeEdit1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(33, 32);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gcTime
            // 
            this.gcTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.gcTime.EmbeddedNavigator.Name = "";
            this.gcTime.FormsUseDefaultLookAndFeel = false;
            this.gcTime.Location = new System.Drawing.Point(346, 0);
            this.gcTime.MainView = this.gvTime;
            this.gcTime.Name = "gcTime";
            this.gcTime.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.timeEdit,
            this.meeEdit,
            this.meEdit,
            this.lookupEdit});
            this.gcTime.Size = new System.Drawing.Size(322, 305);
            this.gcTime.TabIndex = 5;
            this.gcTime.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTime});
            // 
            // gvTime
            // 
            this.gvTime.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gvTime.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTime.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dcTime1,
            this.dcTime2,
            this.text,
            this.dcSelect});
            this.gvTime.GridControl = this.gcTime;
            this.gvTime.Name = "gvTime";
            this.gvTime.OptionsView.RowAutoHeight = true;
            this.gvTime.OptionsView.ShowGroupPanel = false;
            this.gvTime.RowHeight = 30;
            // 
            // dcTime1
            // 
            this.dcTime1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.dcTime1.AppearanceCell.Options.UseFont = true;
            this.dcTime1.AppearanceCell.Options.UseTextOptions = true;
            this.dcTime1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcTime1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcTime1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.dcTime1.AppearanceHeader.Options.UseFont = true;
            this.dcTime1.AppearanceHeader.Options.UseTextOptions = true;
            this.dcTime1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcTime1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcTime1.Caption = "时间1";
            this.dcTime1.ColumnEdit = this.timeEdit;
            this.dcTime1.FieldName = "time1";
            this.dcTime1.Name = "dcTime1";
            this.dcTime1.Visible = true;
            this.dcTime1.VisibleIndex = 0;
            // 
            // timeEdit
            // 
            this.timeEdit.AutoHeight = false;
            this.timeEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEdit.Mask.UseMaskAsDisplayFormat = true;
            this.timeEdit.Name = "timeEdit";
            // 
            // dcTime2
            // 
            this.dcTime2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.dcTime2.AppearanceCell.Options.UseFont = true;
            this.dcTime2.AppearanceCell.Options.UseTextOptions = true;
            this.dcTime2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcTime2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcTime2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.dcTime2.AppearanceHeader.Options.UseFont = true;
            this.dcTime2.AppearanceHeader.Options.UseTextOptions = true;
            this.dcTime2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcTime2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcTime2.Caption = "时间2";
            this.dcTime2.ColumnEdit = this.timeEdit;
            this.dcTime2.FieldName = "dcTime2";
            this.dcTime2.Name = "dcTime2";
            this.dcTime2.Visible = true;
            this.dcTime2.VisibleIndex = 1;
            // 
            // text
            // 
            this.text.AppearanceCell.Options.UseTextOptions = true;
            this.text.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.text.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.text.AppearanceHeader.Options.UseTextOptions = true;
            this.text.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.text.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.text.Caption = "长文本";
            this.text.ColumnEdit = this.meEdit;
            this.text.FieldName = "text";
            this.text.Name = "text";
            this.text.Visible = true;
            this.text.VisibleIndex = 2;
            // 
            // meEdit
            // 
            this.meEdit.Name = "meEdit";
            // 
            // dcSelect
            // 
            this.dcSelect.AppearanceCell.Options.UseTextOptions = true;
            this.dcSelect.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcSelect.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcSelect.AppearanceHeader.Options.UseTextOptions = true;
            this.dcSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcSelect.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcSelect.Caption = "选择";
            this.dcSelect.ColumnEdit = this.lookupEdit;
            this.dcSelect.FieldName = "select";
            this.dcSelect.Name = "dcSelect";
            this.dcSelect.Visible = true;
            this.dcSelect.VisibleIndex = 3;
            // 
            // lookupEdit
            // 

            this.lookupEdit.AutoHeight = false;
            this.lookupEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "设备ID", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "设备名称", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center, DevExpress.Data.ColumnSortOrder.None)});
            this.lookupEdit.Name = "lookupEdit";
            this.lookupEdit.ShowFooter = false;
            this.lookupEdit.ShowHeader = false;
            // 
            // meeEdit
            // 
            this.meeEdit.AutoHeight = false;
            this.meeEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.meeEdit.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.meeEdit.Name = "meeEdit";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(33, 100);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = "simpleButton2";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(139, 102);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(100, 21);
            this.textEdit1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 305);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.gcTime);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.timeEdit1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meeEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TimeEdit timeEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl gcTime;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTime;
        private DevExpress.XtraGrid.Columns.GridColumn dcTime1;
        private DevExpress.XtraGrid.Columns.GridColumn dcTime2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit timeEdit;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn text;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit meeEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit meEdit;
        private DevExpress.XtraGrid.Columns.GridColumn dcSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookupEdit;
    }
}

