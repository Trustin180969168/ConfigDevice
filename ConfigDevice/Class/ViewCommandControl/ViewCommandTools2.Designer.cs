namespace ConfigDevice
{
    partial class ViewCommandTools2
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gcCommands = new DevExpress.XtraGrid.GridControl();
            this.gvCommands = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.del = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.xh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceNetworkID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceKind = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceCtrlObj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbxControlObj = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.cbxCommandKind = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.tedtTime = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.edtNum = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.command = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemTimeEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcCommands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCommands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxControlObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxCommandKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tedtTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcCommands
            // 
            this.gcCommands.Dock = System.Windows.Forms.DockStyle.Left;
            this.gcCommands.EmbeddedNavigator.Name = "";
            this.gcCommands.FormsUseDefaultLookAndFeel = false;
            this.gcCommands.Location = new System.Drawing.Point(0, 0);
            this.gcCommands.MainView = this.gvCommands;
            this.gcCommands.Name = "gcCommands";
            this.gcCommands.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbxControlObj,
            this.linkEdit,
            this.cbxCommandKind,
            this.tedtTime,
            this.edtNum});
            this.gcCommands.Size = new System.Drawing.Size(404, 68);
            this.gcCommands.TabIndex = 8;
            this.gcCommands.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCommands});
            // 
            // gvCommands
            // 
            this.gvCommands.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.del,
            this.xh,
            this.deviceID,
            this.deviceNetworkID,
            this.deviceKind,
            this.deviceName,
            this.deviceCtrlObj});
            this.gvCommands.GridControl = this.gcCommands;
            this.gvCommands.Name = "gvCommands";
            this.gvCommands.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvCommands.OptionsSelection.InvertSelection = true;
            this.gvCommands.OptionsView.ShowColumnHeaders = false;
            this.gvCommands.OptionsView.ShowGroupPanel = false;
            this.gvCommands.OptionsView.ShowIndicator = false;
            this.gvCommands.RowHeight = 60;
            this.gvCommands.DoubleClick += new System.EventHandler(this.gvDevices_DoubleClick);
            // 
            // del
            // 
            this.del.ColumnEdit = this.linkEdit;
            this.del.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.del.Name = "del";
            this.del.Visible = true;
            this.del.VisibleIndex = 0;
            this.del.Width = 32;
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
            // xh
            // 
            this.xh.AppearanceCell.Options.UseTextOptions = true;
            this.xh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xh.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xh.AppearanceHeader.Options.UseTextOptions = true;
            this.xh.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xh.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xh.Caption = "序号";
            this.xh.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.xh.Name = "xh";
            this.xh.OptionsColumn.ReadOnly = true;
            this.xh.Visible = true;
            this.xh.VisibleIndex = 1;
            this.xh.Width = 46;
            // 
            // deviceID
            // 
            this.deviceID.AppearanceCell.Options.UseTextOptions = true;
            this.deviceID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceID.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceID.Caption = "设备ID";
            this.deviceID.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.deviceID.Name = "deviceID";
            this.deviceID.OptionsColumn.AllowEdit = false;
            this.deviceID.OptionsColumn.ReadOnly = true;
            this.deviceID.Visible = true;
            this.deviceID.VisibleIndex = 2;
            this.deviceID.Width = 58;
            // 
            // deviceNetworkID
            // 
            this.deviceNetworkID.AppearanceCell.Options.UseTextOptions = true;
            this.deviceNetworkID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceNetworkID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceNetworkID.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceNetworkID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceNetworkID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceNetworkID.Caption = "网段ID";
            this.deviceNetworkID.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.deviceNetworkID.Name = "deviceNetworkID";
            this.deviceNetworkID.OptionsColumn.AllowEdit = false;
            this.deviceNetworkID.OptionsColumn.ReadOnly = true;
            this.deviceNetworkID.Visible = true;
            this.deviceNetworkID.VisibleIndex = 3;
            this.deviceNetworkID.Width = 58;
            // 
            // deviceKind
            // 
            this.deviceKind.AppearanceCell.Options.UseTextOptions = true;
            this.deviceKind.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceKind.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceKind.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceKind.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceKind.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceKind.Caption = "设备类型";
            this.deviceKind.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.deviceKind.Name = "deviceKind";
            this.deviceKind.OptionsColumn.AllowEdit = false;
            this.deviceKind.OptionsColumn.ReadOnly = true;
            this.deviceKind.Visible = true;
            this.deviceKind.VisibleIndex = 4;
            this.deviceKind.Width = 70;
            // 
            // deviceName
            // 
            this.deviceName.AppearanceCell.Options.UseTextOptions = true;
            this.deviceName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceName.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceName.Caption = "设备名称";
            this.deviceName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.deviceName.Name = "deviceName";
            this.deviceName.OptionsColumn.AllowEdit = false;
            this.deviceName.OptionsColumn.ReadOnly = true;
            this.deviceName.Visible = true;
            this.deviceName.VisibleIndex = 5;
            this.deviceName.Width = 70;
            // 
            // deviceCtrlObj
            // 
            this.deviceCtrlObj.Caption = "控制对象";
            this.deviceCtrlObj.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.deviceCtrlObj.Name = "deviceCtrlObj";
            this.deviceCtrlObj.Visible = true;
            this.deviceCtrlObj.VisibleIndex = 6;
            // 
            // cbxControlObj
            // 
            this.cbxControlObj.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cbxControlObj.AutoHeight = false;
            this.cbxControlObj.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxControlObj.Name = "cbxControlObj";
            this.cbxControlObj.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxControlObj.SelectedIndexChanged += new System.EventHandler(this.cbxBox_SelectedIndexChanged);
            // 
            // cbxCommandKind
            // 
            this.cbxCommandKind.AutoHeight = false;
            this.cbxCommandKind.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxCommandKind.Name = "cbxCommandKind";
            // 
            // tedtTime
            // 
            this.tedtTime.AutoHeight = false;
            this.tedtTime.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tedtTime.Name = "tedtTime";
            this.tedtTime.NullText = "00:00:00";
            // 
            // edtNum
            // 
            this.edtNum.AutoHeight = false;
            this.edtNum.Mask.EditMask = "d";
            this.edtNum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.edtNum.Name = "edtNum";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.FormsUseDefaultLookAndFeel = false;
            this.gridControl1.Location = new System.Drawing.Point(404, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemComboBox2,
            this.repositoryItemTimeEdit1,
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(596, 68);
            this.gridControl1.TabIndex = 9;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.command,
            this.parameter1,
            this.parameter2,
            this.parameter3,
            this.parameter4,
            this.parameter5});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView1.OptionsSelection.InvertSelection = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // command
            // 
            this.command.AppearanceCell.Options.UseTextOptions = true;
            this.command.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.command.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.command.AppearanceHeader.Options.UseTextOptions = true;
            this.command.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.command.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.command.Caption = "指令";
            this.command.Name = "command";
            this.command.Visible = true;
            this.command.VisibleIndex = 0;
            this.command.Width = 132;
            // 
            // parameter1
            // 
            this.parameter1.AppearanceCell.Options.UseTextOptions = true;
            this.parameter1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter1.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter1.Caption = "参数1";
            this.parameter1.Name = "parameter1";
            this.parameter1.Visible = true;
            this.parameter1.VisibleIndex = 1;
            this.parameter1.Width = 93;
            // 
            // parameter2
            // 
            this.parameter2.AppearanceCell.Options.UseTextOptions = true;
            this.parameter2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter2.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter2.Caption = "参数2";
            this.parameter2.Name = "parameter2";
            this.parameter2.Visible = true;
            this.parameter2.VisibleIndex = 2;
            this.parameter2.Width = 103;
            // 
            // parameter3
            // 
            this.parameter3.AppearanceCell.Options.UseTextOptions = true;
            this.parameter3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter3.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter3.Caption = "参数3";
            this.parameter3.Name = "parameter3";
            this.parameter3.Visible = true;
            this.parameter3.VisibleIndex = 3;
            this.parameter3.Width = 96;
            // 
            // parameter4
            // 
            this.parameter4.AppearanceCell.Options.UseTextOptions = true;
            this.parameter4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter4.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter4.Caption = "参数4";
            this.parameter4.Name = "parameter4";
            this.parameter4.Visible = true;
            this.parameter4.VisibleIndex = 4;
            this.parameter4.Width = 105;
            // 
            // parameter5
            // 
            this.parameter5.AppearanceCell.Options.UseTextOptions = true;
            this.parameter5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter5.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter5.Caption = "参数5";
            this.parameter5.Name = "parameter5";
            this.parameter5.Visible = true;
            this.parameter5.VisibleIndex = 5;
            this.parameter5.Width = 85;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Image = global::ConfigDevice.Properties.Resources.del1;
            this.repositoryItemHyperLinkEdit1.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            this.repositoryItemHyperLinkEdit1.NullText = "1";
            this.repositoryItemHyperLinkEdit1.ReadOnly = true;
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // repositoryItemTimeEdit1
            // 
            this.repositoryItemTimeEdit1.AutoHeight = false;
            this.repositoryItemTimeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemTimeEdit1.Name = "repositoryItemTimeEdit1";
            this.repositoryItemTimeEdit1.NullText = "00:00:00";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.EditMask = "d";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(404, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 68);
            this.splitterControl1.TabIndex = 10;
            this.splitterControl1.TabStop = false;
            // 
            // ViewCommandTools2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.gcCommands);
            this.Name = "ViewCommandTools2";
            this.Size = new System.Drawing.Size(1000, 68);
            ((System.ComponentModel.ISupportInitialize)(this.gcCommands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCommands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxControlObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxCommandKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tedtTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcCommands;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCommands;
        private DevExpress.XtraGrid.Columns.GridColumn xh;
        private DevExpress.XtraGrid.Columns.GridColumn deviceID;
        private DevExpress.XtraGrid.Columns.GridColumn deviceNetworkID;
        private DevExpress.XtraGrid.Columns.GridColumn deviceKind;
        private DevExpress.XtraGrid.Columns.GridColumn deviceName;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxControlObj;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkEdit;
        private DevExpress.XtraGrid.Columns.GridColumn del;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxCommandKind;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit tedtTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edtNum;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn command;
        private DevExpress.XtraGrid.Columns.GridColumn parameter1;
        private DevExpress.XtraGrid.Columns.GridColumn parameter2;
        private DevExpress.XtraGrid.Columns.GridColumn parameter3;
        private DevExpress.XtraGrid.Columns.GridColumn parameter4;
        private DevExpress.XtraGrid.Columns.GridColumn parameter5;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraGrid.Columns.GridColumn deviceCtrlObj;
    }
}
