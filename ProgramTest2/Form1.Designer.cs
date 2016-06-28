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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timeEdit1 = new DevExpress.XtraEditors.TimeEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gcTime = new DevExpress.XtraGrid.GridControl();
            this.gvTime = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dcTime1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.timeEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.dcTime2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.dcWeek = new DevExpress.XtraGrid.Columns.GridColumn();
            this.iccbWeek = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.edtNum = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.dcSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glookupEdit = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.imageComboBoxEdit1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.button1 = new System.Windows.Forms.Button();
            this.temperatureEdit = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.checkedComboBoxEdit1 = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lookUpEdit2 = new DevExpress.XtraEditors.LookUpEdit();
            this.gridLookUpEdit1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iccbWeek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glookupEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // timeEdit1
            // 
            this.timeEdit1.EditValue = new System.DateTime(2016, 5, 11, 0, 0, 0, 0);
            this.timeEdit1.Location = new System.Drawing.Point(114, 34);
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
            this.gcTime.Location = new System.Drawing.Point(528, 0);
            this.gcTime.MainView = this.gvTime;
            this.gcTime.Name = "gcTime";
            this.gcTime.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.timeEdit,
            this.edtNum,
            this.dateEdit,
            this.iccbWeek,
            this.glookupEdit,
            this.repositoryItemLookUpEdit1});
            this.gcTime.Size = new System.Drawing.Size(423, 466);
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
            this.dcWeek,
            this.num,
            this.dcSelect});
            this.gvTime.GridControl = this.gcTime;
            this.gvTime.Name = "gvTime";
            this.gvTime.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gvTime.OptionsView.RowAutoHeight = true;
            this.gvTime.OptionsView.ShowGroupPanel = false;
            this.gvTime.RowHeight = 30;
            this.gvTime.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.edt_MouseWheel);
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
            this.timeEdit.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.timeEdit.Appearance.Options.UseBackColor = true;
            this.timeEdit.AutoHeight = false;
            this.timeEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEdit.Mask.UseMaskAsDisplayFormat = true;
            this.timeEdit.Name = "timeEdit";
            this.timeEdit.Leave += new System.EventHandler(this.Edit_Leave);
            this.timeEdit.Enter += new System.EventHandler(this.Edit_Enter);
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
            this.dcTime2.ColumnEdit = this.dateEdit;
            this.dcTime2.FieldName = "dcTime2";
            this.dcTime2.Name = "dcTime2";
            this.dcTime2.Visible = true;
            this.dcTime2.VisibleIndex = 1;
            // 
            // dateEdit
            // 
            this.dateEdit.AutoHeight = false;
            this.dateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit.Name = "dateEdit";
            this.dateEdit.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // dcWeek
            // 
            this.dcWeek.Caption = "周";
            this.dcWeek.ColumnEdit = this.iccbWeek;
            this.dcWeek.Name = "dcWeek";
            this.dcWeek.Visible = true;
            this.dcWeek.VisibleIndex = 4;
            // 
            // iccbWeek
            // 
            this.iccbWeek.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.iccbWeek.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期一"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期二"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期三"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期四"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期五"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期六"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期日")});
            this.iccbWeek.Name = "iccbWeek";
            this.iccbWeek.PopupFormMinSize = new System.Drawing.Size(50, 200);
            // 
            // num
            // 
            this.num.AppearanceCell.Options.UseTextOptions = true;
            this.num.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.num.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.num.AppearanceHeader.Options.UseTextOptions = true;
            this.num.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.num.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.num.Caption = "数字";
            this.num.ColumnEdit = this.edtNum;
            this.num.FieldName = "num";
            this.num.Name = "num";
            this.num.Visible = true;
            this.num.VisibleIndex = 3;
            // 
            // edtNum
            // 
            this.edtNum.AutoHeight = false;
            this.edtNum.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edtNum.Mask.EditMask = "d";
            this.edtNum.Name = "edtNum";
            this.edtNum.Leave += new System.EventHandler(this.Edit_Leave);
            this.edtNum.Enter += new System.EventHandler(this.Edit_Enter);
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
            this.dcSelect.ColumnEdit = this.glookupEdit;
            this.dcSelect.FieldName = "select";
            this.dcSelect.Name = "dcSelect";
            this.dcSelect.Visible = true;
            this.dcSelect.VisibleIndex = 2;
            // 
            // glookupEdit
            // 
            this.glookupEdit.AutoHeight = false;
            this.glookupEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glookupEdit.Name = "glookupEdit";
            this.glookupEdit.View = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(33, 61);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = "simpleButton2";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(33, 99);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(167, 23);
            this.simpleButton3.TabIndex = 9;
            this.simpleButton3.Text = "打开界面测试2";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // imageComboBoxEdit1
            // 
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(364, 12);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboBoxEdit1.Properties.DropDownRows = 4;
            this.imageComboBoxEdit1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 3, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 4, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 5, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 6, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 7, -1)});
            this.imageComboBoxEdit1.Properties.LargeImages = this.imageCollection1;
            this.imageComboBoxEdit1.Size = new System.Drawing.Size(122, 98);
            this.imageComboBoxEdit1.TabIndex = 11;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(96, 96);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(33, 149);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Size = new System.Drawing.Size(181, 21);
            this.lookUpEdit1.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "测试变换控件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // temperatureEdit
            // 
            this.temperatureEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.temperatureEdit.Location = new System.Drawing.Point(235, 149);
            this.temperatureEdit.Name = "temperatureEdit";
            this.temperatureEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.temperatureEdit.Properties.Mask.EditMask = "P0";
            this.temperatureEdit.Size = new System.Drawing.Size(100, 21);
            this.temperatureEdit.TabIndex = 14;
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(115, 61);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Size = new System.Drawing.Size(99, 21);
            this.spinEdit1.TabIndex = 15;
            // 
            // checkedComboBoxEdit1
            // 
            this.checkedComboBoxEdit1.EditValue = "";
            this.checkedComboBoxEdit1.Location = new System.Drawing.Point(137, 193);
            this.checkedComboBoxEdit1.Name = "checkedComboBoxEdit1";
            this.checkedComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedComboBoxEdit1.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.checkedComboBoxEdit1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期一"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期二"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期三"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期四"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期五"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期六"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("星期日")});
            this.checkedComboBoxEdit1.Properties.ShowAllItemCaption = "全选";
            this.checkedComboBoxEdit1.Properties.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.checkedComboBoxEdit1_Properties_QueryPopUp);
            this.checkedComboBoxEdit1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkedComboBoxEdit1.Size = new System.Drawing.Size(283, 21);
            this.checkedComboBoxEdit1.TabIndex = 16;
            this.checkedComboBoxEdit1.EditValueChanged += new System.EventHandler(this.checkedComboBoxEdit1_EditValueChanged);
            this.checkedComboBoxEdit1.QueryResultValue += new DevExpress.XtraEditors.Controls.QueryResultValueEventHandler(this.checkedComboBoxEdit1_QueryResultValue);
            this.checkedComboBoxEdit1.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.checkedComboBoxEdit1_Closed);
            // 
            // lookUpEdit2
            // 
            this.lookUpEdit2.Location = new System.Drawing.Point(33, 243);
            this.lookUpEdit2.Name = "lookUpEdit2";
            this.lookUpEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEdit2.Size = new System.Drawing.Size(136, 21);
            this.lookUpEdit2.TabIndex = 17;
            // 
            // gridLookUpEdit1
            // 
            this.gridLookUpEdit1.Location = new System.Drawing.Point(208, 241);
            this.gridLookUpEdit1.Name = "gridLookUpEdit1";
            this.gridLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridLookUpEdit1.Properties.View = this.gridLookUpEdit1View;
            this.gridLookUpEdit1.Size = new System.Drawing.Size(198, 21);
            this.gridLookUpEdit1.TabIndex = 18;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 466);
            this.Controls.Add(this.gridLookUpEdit1);
            this.Controls.Add(this.lookUpEdit2);
            this.Controls.Add(this.checkedComboBoxEdit1);
            this.Controls.Add(this.spinEdit1);
            this.Controls.Add(this.temperatureEdit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.imageComboBoxEdit1);
            this.Controls.Add(this.simpleButton3);
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
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iccbWeek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glookupEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn num;
        private DevExpress.XtraGrid.Columns.GridColumn dcSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit edtNum;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboBoxEdit1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateEdit;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.SpinEdit temperatureEdit;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit iccbWeek;
        private DevExpress.XtraGrid.Columns.GridColumn dcWeek;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEdit1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit glookupEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.GridLookUpEdit gridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}

