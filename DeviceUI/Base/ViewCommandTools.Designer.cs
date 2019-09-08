﻿namespace ConfigDevice
{
    partial class ViewCommandTools
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
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceNetwork = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceKind = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceCtrlObj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbxControlObj = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.command = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.timeTest = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.spinTest = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.cedtSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.numEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.meeEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.hyperLinkEdit1 = new DevExpress.XtraEditors.HyperLinkEdit();
            this.cedtNum = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCommands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCommands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxControlObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meeEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtNum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcCommands
            // 
            this.gcCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCommands.EmbeddedNavigator.Name = "";
            this.gcCommands.FormsUseDefaultLookAndFeel = false;
            this.gcCommands.Location = new System.Drawing.Point(77, 0);
            this.gcCommands.MainView = this.gvCommands;
            this.gcCommands.Name = "gcCommands";
            this.gcCommands.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbxControlObj,
            this.linkEdit,
            this.timeTest,
            this.spinTest,
            this.cedtSelect,
            this.numEdit,
            this.meeEdit});
            this.gcCommands.Size = new System.Drawing.Size(923, 53);
            this.gcCommands.TabIndex = 8;
            this.gcCommands.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCommands});
            this.gcCommands.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gcCommands_MouseDoubleClick);
            // 
            // gvCommands
            // 
            this.gvCommands.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.deviceNetwork,
            this.deviceKind,
            this.deviceName,
            this.deviceCtrlObj,
            this.command,
            this.parameter1,
            this.parameter2,
            this.parameter3,
            this.parameter4,
            this.parameter5});
            this.gvCommands.GridControl = this.gcCommands;
            this.gvCommands.Name = "gvCommands";
            this.gvCommands.OptionsCustomization.AllowColumnMoving = false;
            this.gvCommands.OptionsCustomization.AllowColumnResizing = false;
            this.gvCommands.OptionsCustomization.AllowFilter = false;
            this.gvCommands.OptionsCustomization.AllowGroup = false;
            this.gvCommands.OptionsCustomization.AllowSort = false;
            this.gvCommands.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvCommands.OptionsSelection.InvertSelection = true;
            this.gvCommands.OptionsView.ColumnAutoWidth = false;
            this.gvCommands.OptionsView.ShowGroupPanel = false;
            this.gvCommands.OptionsView.ShowIndicator = false;
            this.gvCommands.RowHeight = 18;
            this.gvCommands.DoubleClick += new System.EventHandler(this.gvDevices_DoubleClick);
            this.gvCommands.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvCommands_CellValueChanged);
            this.gvCommands.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvCommands_MouseDown);
            // 
            // ID
            // 
            this.ID.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.ID.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.ID.AppearanceCell.Options.UseBackColor = true;
            this.ID.AppearanceCell.Options.UseForeColor = true;
            this.ID.AppearanceCell.Options.UseTextOptions = true;
            this.ID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ID.AppearanceHeader.Options.UseTextOptions = true;
            this.ID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ID.Caption = "ID";
            this.ID.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ID.OptionsColumn.FixedWidth = true;
            this.ID.Visible = true;
            this.ID.VisibleIndex = 0;
            this.ID.Width = 50;
            // 
            // deviceNetwork
            // 
            this.deviceNetwork.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.deviceNetwork.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.deviceNetwork.AppearanceCell.Options.UseBackColor = true;
            this.deviceNetwork.AppearanceCell.Options.UseForeColor = true;
            this.deviceNetwork.AppearanceCell.Options.UseTextOptions = true;
            this.deviceNetwork.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceNetwork.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceNetwork.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceNetwork.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceNetwork.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceNetwork.Caption = "网段";
            this.deviceNetwork.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.deviceNetwork.Name = "deviceNetwork";
            this.deviceNetwork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.deviceNetwork.OptionsColumn.FixedWidth = true;
            this.deviceNetwork.Visible = true;
            this.deviceNetwork.VisibleIndex = 1;
            this.deviceNetwork.Width = 50;
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
            this.deviceKind.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.deviceKind.OptionsColumn.FixedWidth = true;
            this.deviceKind.OptionsColumn.ReadOnly = true;
            this.deviceKind.Visible = true;
            this.deviceKind.VisibleIndex = 2;
            this.deviceKind.Width = 80;
            // 
            // deviceName
            // 
            this.deviceName.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.deviceName.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.deviceName.AppearanceCell.Options.UseBackColor = true;
            this.deviceName.AppearanceCell.Options.UseForeColor = true;
            this.deviceName.AppearanceCell.Options.UseTextOptions = true;
            this.deviceName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceName.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceName.Caption = "设备名称";
            this.deviceName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.deviceName.Name = "deviceName";
            this.deviceName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.deviceName.OptionsColumn.FixedWidth = true;
            this.deviceName.Visible = true;
            this.deviceName.VisibleIndex = 3;
            this.deviceName.Width = 140;
            // 
            // deviceCtrlObj
            // 
            this.deviceCtrlObj.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.deviceCtrlObj.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.deviceCtrlObj.AppearanceCell.Options.UseBackColor = true;
            this.deviceCtrlObj.AppearanceCell.Options.UseForeColor = true;
            this.deviceCtrlObj.AppearanceCell.Options.UseTextOptions = true;
            this.deviceCtrlObj.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceCtrlObj.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceCtrlObj.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceCtrlObj.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceCtrlObj.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceCtrlObj.Caption = "控制对象";
            this.deviceCtrlObj.ColumnEdit = this.cbxControlObj;
            this.deviceCtrlObj.Name = "deviceCtrlObj";
            this.deviceCtrlObj.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.deviceCtrlObj.OptionsColumn.FixedWidth = true;
            this.deviceCtrlObj.Visible = true;
            this.deviceCtrlObj.VisibleIndex = 4;
            this.deviceCtrlObj.Width = 80;
            // 
            // cbxControlObj
            // 
            this.cbxControlObj.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cbxControlObj.AutoHeight = false;
            this.cbxControlObj.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxControlObj.Name = "cbxControlObj";
            this.cbxControlObj.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxControlObj.SelectedIndexChanged += new System.EventHandler(this.cbxControlObj_SelectedIndexChanged);
            // 
            // command
            // 
            this.command.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.command.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.command.AppearanceCell.Options.UseBackColor = true;
            this.command.AppearanceCell.Options.UseForeColor = true;
            this.command.AppearanceCell.Options.UseTextOptions = true;
            this.command.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.command.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.command.AppearanceHeader.Options.UseTextOptions = true;
            this.command.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.command.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.command.Caption = "指令";
            this.command.Name = "command";
            this.command.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.command.OptionsColumn.FixedWidth = true;
            this.command.Width = 80;
            // 
            // parameter1
            // 
            this.parameter1.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter1.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter1.AppearanceCell.Options.UseBackColor = true;
            this.parameter1.AppearanceCell.Options.UseForeColor = true;
            this.parameter1.AppearanceCell.Options.UseTextOptions = true;
            this.parameter1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter1.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter1.Caption = "参数1";
            this.parameter1.Name = "parameter1";
            this.parameter1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter1.OptionsColumn.FixedWidth = true;
            this.parameter1.Width = 80;
            // 
            // parameter2
            // 
            this.parameter2.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter2.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter2.AppearanceCell.Options.UseBackColor = true;
            this.parameter2.AppearanceCell.Options.UseForeColor = true;
            this.parameter2.AppearanceCell.Options.UseTextOptions = true;
            this.parameter2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter2.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter2.Caption = "参数2";
            this.parameter2.Name = "parameter2";
            this.parameter2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter2.OptionsColumn.FixedWidth = true;
            this.parameter2.Width = 80;
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
            // hyperLinkEdit1
            // 
            this.hyperLinkEdit1.Dock = System.Windows.Forms.DockStyle.Left;
            this.hyperLinkEdit1.Location = new System.Drawing.Point(47, 0);
            this.hyperLinkEdit1.Name = "hyperLinkEdit1";
            this.hyperLinkEdit1.Properties.AutoHeight = false;
            this.hyperLinkEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.hyperLinkEdit1.Properties.Image = global::ConfigDevice.Properties.Resources.del1;
            this.hyperLinkEdit1.Properties.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.hyperLinkEdit1.Size = new System.Drawing.Size(30, 53);
            this.hyperLinkEdit1.TabIndex = 11;
            this.hyperLinkEdit1.Click += new System.EventHandler(this.linkEdit_Click);
            // 
            // cedtNum
            // 
            this.cedtNum.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cedtNum.Dock = System.Windows.Forms.DockStyle.Left;
            this.cedtNum.Location = new System.Drawing.Point(0, 0);
            this.cedtNum.Name = "cedtNum";
            this.cedtNum.Properties.AllowFocused = false;
            this.cedtNum.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue;
            this.cedtNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.cedtNum.Properties.Appearance.Options.UseBackColor = true;
            this.cedtNum.Properties.Appearance.Options.UseFont = true;
            this.cedtNum.Properties.AutoHeight = false;
            this.cedtNum.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.cedtNum.Properties.Caption = "1";
            this.cedtNum.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cedtNum.Size = new System.Drawing.Size(47, 53);
            this.cedtNum.TabIndex = 13;
            // 
            // ViewCommandTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcCommands);
            this.Controls.Add(this.hyperLinkEdit1);
            this.Controls.Add(this.cedtNum);
            this.Name = "ViewCommandTools";
            this.Size = new System.Drawing.Size(1000, 53);
            ((System.ComponentModel.ISupportInitialize)(this.gcCommands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCommands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxControlObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meeEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtNum.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcCommands;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCommands;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn deviceNetwork;
        private DevExpress.XtraGrid.Columns.GridColumn deviceKind;
        private DevExpress.XtraGrid.Columns.GridColumn deviceName;
        private DevExpress.XtraGrid.Columns.GridColumn deviceCtrlObj;
        private DevExpress.XtraGrid.Columns.GridColumn command;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxControlObj;
        private DevExpress.XtraGrid.Columns.GridColumn parameter1;
        private DevExpress.XtraGrid.Columns.GridColumn parameter2;
        private DevExpress.XtraGrid.Columns.GridColumn parameter3;
        private DevExpress.XtraGrid.Columns.GridColumn parameter4;
        private DevExpress.XtraGrid.Columns.GridColumn parameter5;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit timeTest;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTest;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit cedtSelect;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit1;
        private DevExpress.XtraEditors.CheckEdit cedtNum;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit numEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit meeEdit;
    }
}