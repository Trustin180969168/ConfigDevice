namespace ConfigDevice.DeviceUI
{
    partial class FrmWirlessDevices
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
            this.tctrlEdit = new DevExpress.XtraTab.XtraTabControl();
            this.pagePzjm = new DevExpress.XtraTab.XtraTabPage();
            this.gcWirlessDevices = new DevExpress.XtraGrid.GridControl();
            this.gvWirlessDevices = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dcRowID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcAdd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkAdd = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.dcDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.dcMAC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcDeviceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbxControlObj = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.timeTest = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.spinTest = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.cedtSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.numEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.meeEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.tsDoorInput = new System.Windows.Forms.ToolStrip();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btRefrash = new System.Windows.Forms.ToolStripButton();
            this.pageJcsz = new DevExpress.XtraTab.XtraTabPage();
            this.viewBaseSetting = new ConfigDevice.ViewBaseEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tctrlEdit)).BeginInit();
            this.tctrlEdit.SuspendLayout();
            this.pagePzjm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcWirlessDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWirlessDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxControlObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meeEdit)).BeginInit();
            this.tsDoorInput.SuspendLayout();
            this.pageJcsz.SuspendLayout();
            this.SuspendLayout();
            // 
            // tctrlEdit
            // 
            this.tctrlEdit.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tctrlEdit.AppearancePage.Header.Options.UseFont = true;
            this.tctrlEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tctrlEdit.Location = new System.Drawing.Point(0, 24);
            this.tctrlEdit.Name = "tctrlEdit";
            this.tctrlEdit.SelectedTabPage = this.pagePzjm;
            this.tctrlEdit.Size = new System.Drawing.Size(893, 547);
            this.tctrlEdit.TabIndex = 3;
            this.tctrlEdit.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageJcsz,
            this.pagePzjm});
            this.tctrlEdit.Text = "xtraTabControl1";
            // 
            // pagePzjm
            // 
            this.pagePzjm.Controls.Add(this.gcWirlessDevices);
            this.pagePzjm.Controls.Add(this.tsDoorInput);
            this.pagePzjm.Name = "pagePzjm";
            this.pagePzjm.Size = new System.Drawing.Size(884, 510);
            this.pagePzjm.Text = "配置界面";
            // 
            // gcWirlessDevices
            // 
            this.gcWirlessDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcWirlessDevices.EmbeddedNavigator.Name = "";
            this.gcWirlessDevices.FormsUseDefaultLookAndFeel = false;
            this.gcWirlessDevices.Location = new System.Drawing.Point(0, 31);
            this.gcWirlessDevices.MainView = this.gvWirlessDevices;
            this.gcWirlessDevices.Name = "gcWirlessDevices";
            this.gcWirlessDevices.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbxControlObj,
            this.linkEdit,
            this.timeTest,
            this.spinTest,
            this.cedtSelect,
            this.numEdit,
            this.meeEdit,
            this.linkAdd});
            this.gcWirlessDevices.Size = new System.Drawing.Size(884, 479);
            this.gcWirlessDevices.TabIndex = 9;
            this.gcWirlessDevices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWirlessDevices});
            // 
            // gvWirlessDevices
            // 
            this.gvWirlessDevices.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dcRowID,
            this.dcAdd,
            this.dcDelete,
            this.dcMAC,
            this.dcDeviceName,
            this.parameter1,
            this.parameter3,
            this.parameter4,
            this.parameter5});
            this.gvWirlessDevices.GridControl = this.gcWirlessDevices;
            this.gvWirlessDevices.Name = "gvWirlessDevices";
            this.gvWirlessDevices.OptionsCustomization.AllowColumnMoving = false;
            this.gvWirlessDevices.OptionsCustomization.AllowColumnResizing = false;
            this.gvWirlessDevices.OptionsCustomization.AllowFilter = false;
            this.gvWirlessDevices.OptionsCustomization.AllowGroup = false;
            this.gvWirlessDevices.OptionsCustomization.AllowSort = false;
            this.gvWirlessDevices.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvWirlessDevices.OptionsView.ColumnAutoWidth = false;
            this.gvWirlessDevices.OptionsView.ShowGroupPanel = false;
            this.gvWirlessDevices.RowHeight = 18;
            // 
            // dcRowID
            // 
            this.dcRowID.AppearanceCell.Options.UseTextOptions = true;
            this.dcRowID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcRowID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcRowID.AppearanceHeader.Options.UseTextOptions = true;
            this.dcRowID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcRowID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcRowID.Caption = "序号";
            this.dcRowID.Name = "dcRowID";
            this.dcRowID.OptionsColumn.AllowMove = false;
            this.dcRowID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.dcRowID.OptionsColumn.ReadOnly = true;
            this.dcRowID.Visible = true;
            this.dcRowID.VisibleIndex = 0;
            this.dcRowID.Width = 50;
            // 
            // dcAdd
            // 
            this.dcAdd.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcAdd.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcAdd.AppearanceCell.Options.UseBackColor = true;
            this.dcAdd.AppearanceCell.Options.UseForeColor = true;
            this.dcAdd.AppearanceCell.Options.UseTextOptions = true;
            this.dcAdd.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcAdd.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcAdd.AppearanceHeader.Options.UseTextOptions = true;
            this.dcAdd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcAdd.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcAdd.ColumnEdit = this.linkAdd;
            this.dcAdd.Name = "dcAdd";
            this.dcAdd.OptionsColumn.AllowMove = false;
            this.dcAdd.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcAdd.OptionsColumn.FixedWidth = true;
            this.dcAdd.Visible = true;
            this.dcAdd.VisibleIndex = 1;
            this.dcAdd.Width = 50;
            // 
            // linkAdd
            // 
            this.linkAdd.AutoHeight = false;
            this.linkAdd.Image = global::ConfigDevice.Properties.Resources.Add;
            this.linkAdd.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.linkAdd.Name = "linkAdd";
            this.linkAdd.ReadOnly = true;
            this.linkAdd.Click += new System.EventHandler(this.linkAdd_Click);
            // 
            // dcDelete
            // 
            this.dcDelete.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcDelete.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcDelete.AppearanceCell.Options.UseBackColor = true;
            this.dcDelete.AppearanceCell.Options.UseForeColor = true;
            this.dcDelete.AppearanceCell.Options.UseTextOptions = true;
            this.dcDelete.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDelete.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.dcDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDelete.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDelete.ColumnEdit = this.linkEdit;
            this.dcDelete.Name = "dcDelete";
            this.dcDelete.OptionsColumn.AllowMove = false;
            this.dcDelete.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcDelete.OptionsColumn.FixedWidth = true;
            this.dcDelete.Visible = true;
            this.dcDelete.VisibleIndex = 2;
            this.dcDelete.Width = 50;
            // 
            // linkEdit
            // 
            this.linkEdit.AutoHeight = false;
            this.linkEdit.Image = global::ConfigDevice.Properties.Resources.del1;
            this.linkEdit.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.linkEdit.Name = "linkEdit";
            this.linkEdit.NullText = "1";
            this.linkEdit.ReadOnly = true;
            this.linkEdit.Click += new System.EventHandler(this.linkEdit_Click);
            // 
            // dcMAC
            // 
            this.dcMAC.AppearanceCell.Options.UseTextOptions = true;
            this.dcMAC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcMAC.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcMAC.AppearanceHeader.Options.UseTextOptions = true;
            this.dcMAC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcMAC.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcMAC.Caption = "MAC地址";
            this.dcMAC.Name = "dcMAC";
            this.dcMAC.OptionsColumn.AllowMove = false;
            this.dcMAC.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.dcMAC.OptionsColumn.ReadOnly = true;
            this.dcMAC.Visible = true;
            this.dcMAC.VisibleIndex = 3;
            this.dcMAC.Width = 280;
            // 
            // dcDeviceName
            // 
            this.dcDeviceName.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcDeviceName.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcDeviceName.AppearanceCell.Options.UseBackColor = true;
            this.dcDeviceName.AppearanceCell.Options.UseForeColor = true;
            this.dcDeviceName.AppearanceCell.Options.UseTextOptions = true;
            this.dcDeviceName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDeviceName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDeviceName.AppearanceHeader.Options.UseTextOptions = true;
            this.dcDeviceName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDeviceName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDeviceName.Caption = "设备名称";
            this.dcDeviceName.Name = "dcDeviceName";
            this.dcDeviceName.OptionsColumn.AllowMove = false;
            this.dcDeviceName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.dcDeviceName.Visible = true;
            this.dcDeviceName.VisibleIndex = 4;
            this.dcDeviceName.Width = 500;
            // 
            // parameter1
            // 
            this.parameter1.AppearanceCell.Options.UseTextOptions = true;
            this.parameter1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter1.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter1.Caption = "是否在线";
            this.parameter1.Name = "parameter1";
            this.parameter1.OptionsColumn.AllowMove = false;
            this.parameter1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.parameter1.Width = 80;
            // 
            // parameter3
            // 
            this.parameter3.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter3.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter3.AppearanceCell.Options.UseBackColor = true;
            this.parameter3.AppearanceCell.Options.UseForeColor = true;
            this.parameter3.AppearanceCell.Options.UseTextOptions = true;
            this.parameter3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter3.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter3.Caption = "参数3";
            this.parameter3.Name = "parameter3";
            this.parameter3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter3.OptionsColumn.FixedWidth = true;
            this.parameter3.Width = 80;
            // 
            // parameter4
            // 
            this.parameter4.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter4.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter4.AppearanceCell.Options.UseBackColor = true;
            this.parameter4.AppearanceCell.Options.UseForeColor = true;
            this.parameter4.AppearanceCell.Options.UseTextOptions = true;
            this.parameter4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter4.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter4.Caption = "参数4";
            this.parameter4.Name = "parameter4";
            this.parameter4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter4.OptionsColumn.FixedWidth = true;
            this.parameter4.Width = 80;
            // 
            // parameter5
            // 
            this.parameter5.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter5.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter5.AppearanceCell.Options.UseBackColor = true;
            this.parameter5.AppearanceCell.Options.UseForeColor = true;
            this.parameter5.AppearanceCell.Options.UseTextOptions = true;
            this.parameter5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter5.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter5.Caption = "参数5";
            this.parameter5.Name = "parameter5";
            this.parameter5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter5.OptionsColumn.FixedWidth = true;
            this.parameter5.Width = 80;
            // 
            // cbxControlObj
            // 
            this.cbxControlObj.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cbxControlObj.AutoHeight = false;
            this.cbxControlObj.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxControlObj.Name = "cbxControlObj";
            this.cbxControlObj.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // timeTest
            // 
            this.timeTest.AutoHeight = false;
            this.timeTest.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeTest.DisplayFormat.FormatString = "d";
            this.timeTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeTest.HideSelection = false;
            this.timeTest.Mask.EditMask = "HH:mm:ss";
            this.timeTest.Mask.UseMaskAsDisplayFormat = true;
            this.timeTest.Name = "timeTest";
            // 
            // spinTest
            // 
            this.spinTest.AutoHeight = false;
            this.spinTest.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinTest.Mask.EditMask = "P0";
            this.spinTest.Mask.UseMaskAsDisplayFormat = true;
            this.spinTest.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinTest.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinTest.Name = "spinTest";
            // 
            // cedtSelect
            // 
            this.cedtSelect.AutoHeight = false;
            this.cedtSelect.Name = "cedtSelect";
            this.cedtSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // numEdit
            // 
            this.numEdit.AutoHeight = false;
            this.numEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numEdit.Mask.EditMask = "\\d+";
            this.numEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numEdit.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numEdit.Name = "numEdit";
            // 
            // meeEdit
            // 
            this.meeEdit.AutoHeight = false;
            this.meeEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.meeEdit.Name = "meeEdit";
            // 
            // tsDoorInput
            // 
            this.tsDoorInput.Font = new System.Drawing.Font("宋体", 12F);
            this.tsDoorInput.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsDoorInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSave,
            this.btRefrash});
            this.tsDoorInput.Location = new System.Drawing.Point(0, 0);
            this.tsDoorInput.Name = "tsDoorInput";
            this.tsDoorInput.Size = new System.Drawing.Size(884, 31);
            this.tsDoorInput.TabIndex = 1;
            this.tsDoorInput.Text = "toolStrip2";
            // 
            // btSave
            // 
            this.btSave.Image = global::ConfigDevice.Properties.Resources.save;
            this.btSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(76, 28);
            this.btSave.Text = "保存 ";
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btRefrash
            // 
            this.btRefrash.Image = global::ConfigDevice.Properties.Resources.refresh;
            this.btRefrash.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefrash.Name = "btRefrash";
            this.btRefrash.Size = new System.Drawing.Size(76, 28);
            this.btRefrash.Text = "刷新 ";
            this.btRefrash.Click += new System.EventHandler(this.btRefrash_Click);
            // 
            // pageJcsz
            // 
            this.pageJcsz.Controls.Add(this.viewBaseSetting);
            this.pageJcsz.Name = "pageJcsz";
            this.pageJcsz.Size = new System.Drawing.Size(785, 510);
            this.pageJcsz.Text = "基础配置";
            // 
            // viewBaseSetting
            // 
            this.viewBaseSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewBaseSetting.Location = new System.Drawing.Point(0, 0);
            this.viewBaseSetting.Name = "viewBaseSetting";
            this.viewBaseSetting.Size = new System.Drawing.Size(785, 510);
            this.viewBaseSetting.TabIndex = 0;
            // 
            // FrmWirlessDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(893, 571);
            this.Controls.Add(this.tctrlEdit);
            this.Name = "FrmWirlessDevices";
            this.Load += new System.EventHandler(this.FrmWirlessDevices_Load);
            this.Controls.SetChildIndex(this.tctrlEdit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.tctrlEdit)).EndInit();
            this.tctrlEdit.ResumeLayout(false);
            this.pagePzjm.ResumeLayout(false);
            this.pagePzjm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcWirlessDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWirlessDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxControlObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meeEdit)).EndInit();
            this.tsDoorInput.ResumeLayout(false);
            this.tsDoorInput.PerformLayout();
            this.pageJcsz.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tctrlEdit;
        private DevExpress.XtraTab.XtraTabPage pagePzjm;
        private System.Windows.Forms.ToolStrip tsDoorInput;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripButton btRefrash;
        private DevExpress.XtraTab.XtraTabPage pageJcsz;
        private ViewBaseEdit viewBaseSetting;
        private DevExpress.XtraGrid.GridControl gcWirlessDevices;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWirlessDevices;
        private DevExpress.XtraGrid.Columns.GridColumn dcRowID;
        private DevExpress.XtraGrid.Columns.GridColumn dcDelete;
        private DevExpress.XtraGrid.Columns.GridColumn dcMAC;
        private DevExpress.XtraGrid.Columns.GridColumn dcDeviceName;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxControlObj;
        private DevExpress.XtraGrid.Columns.GridColumn parameter1;
        private DevExpress.XtraGrid.Columns.GridColumn dcAdd;
        private DevExpress.XtraGrid.Columns.GridColumn parameter3;
        private DevExpress.XtraGrid.Columns.GridColumn parameter4;
        private DevExpress.XtraGrid.Columns.GridColumn parameter5;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit timeTest;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTest;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit cedtSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit numEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit meeEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkAdd;
    }
}
