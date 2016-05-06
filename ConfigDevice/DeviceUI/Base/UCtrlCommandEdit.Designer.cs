namespace ConfigDevice.DeviceUI
{
    partial class UCtrlCommandEdit
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
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btSyncEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btSaveCommands = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.xscCommands = new DevExpress.XtraEditors.XtraScrollableControl();
            this.viewCommandTools5 = new ConfigDevice.ViewCommandTools();
            this.viewCommandTools4 = new ConfigDevice.ViewCommandTools();
            this.viewCommandTools3 = new ConfigDevice.ViewCommandTools();
            this.viewCommandTools2 = new ConfigDevice.ViewCommandTools();
            this.viewCommandTools1 = new ConfigDevice.ViewCommandTools();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.cbxGroup = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblGroupName = new DevExpress.XtraEditors.LabelControl();
            this.edtBeginNum = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.toolStrip2.SuspendLayout();
            this.xscCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtBeginNum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSyncEdit,
            this.toolStripSeparator1,
            this.btSaveCommands,
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(2, 61);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(744, 31);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btSyncEdit
            // 
            this.btSyncEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSyncEdit.Image = global::ConfigDevice.Properties.Resources.uncheck;
            this.btSyncEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSyncEdit.Name = "btSyncEdit";
            this.btSyncEdit.Size = new System.Drawing.Size(28, 28);
            this.btSyncEdit.Text = "同步编辑 ";
            this.btSyncEdit.Click += new System.EventHandler(this.btSyncEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(6, 0, 20, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // btSaveCommands
            // 
            this.btSaveCommands.Image = global::ConfigDevice.Properties.Resources.save;
            this.btSaveCommands.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveCommands.Name = "btSaveCommands";
            this.btSaveCommands.Size = new System.Drawing.Size(106, 28);
            this.btSaveCommands.Text = "保存指令 ";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::ConfigDevice.Properties.Resources.refresh;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(74, 28);
            this.toolStripButton1.Text = "刷新 ";
            // 
            // xscCommands
            // 
            this.xscCommands.Controls.Add(this.viewCommandTools5);
            this.xscCommands.Controls.Add(this.viewCommandTools4);
            this.xscCommands.Controls.Add(this.viewCommandTools3);
            this.xscCommands.Controls.Add(this.viewCommandTools2);
            this.xscCommands.Controls.Add(this.viewCommandTools1);
            this.xscCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xscCommands.Location = new System.Drawing.Point(0, 94);
            this.xscCommands.Name = "xscCommands";
            this.xscCommands.Size = new System.Drawing.Size(748, 360);
            this.xscCommands.TabIndex = 5;
            // 
            // viewCommandTools5
            // 
            this.viewCommandTools5.Checked = false;
            this.viewCommandTools5.Dock = System.Windows.Forms.DockStyle.Top;
            this.viewCommandTools5.Location = new System.Drawing.Point(0, 280);
            this.viewCommandTools5.Name = "viewCommandTools5";
            this.viewCommandTools5.Num = 5;
            this.viewCommandTools5.Size = new System.Drawing.Size(748, 70);
            this.viewCommandTools5.TabIndex = 4;
            // 
            // viewCommandTools4
            // 
            this.viewCommandTools4.Checked = false;
            this.viewCommandTools4.Dock = System.Windows.Forms.DockStyle.Top;
            this.viewCommandTools4.Location = new System.Drawing.Point(0, 210);
            this.viewCommandTools4.Name = "viewCommandTools4";
            this.viewCommandTools4.Num = 4;
            this.viewCommandTools4.Size = new System.Drawing.Size(748, 70);
            this.viewCommandTools4.TabIndex = 3;
            // 
            // viewCommandTools3
            // 
            this.viewCommandTools3.Checked = false;
            this.viewCommandTools3.Dock = System.Windows.Forms.DockStyle.Top;
            this.viewCommandTools3.Location = new System.Drawing.Point(0, 140);
            this.viewCommandTools3.Name = "viewCommandTools3";
            this.viewCommandTools3.Num = 3;
            this.viewCommandTools3.Size = new System.Drawing.Size(748, 70);
            this.viewCommandTools3.TabIndex = 2;
            // 
            // viewCommandTools2
            // 
            this.viewCommandTools2.Checked = false;
            this.viewCommandTools2.Dock = System.Windows.Forms.DockStyle.Top;
            this.viewCommandTools2.Location = new System.Drawing.Point(0, 70);
            this.viewCommandTools2.Name = "viewCommandTools2";
            this.viewCommandTools2.Num = 2;
            this.viewCommandTools2.Size = new System.Drawing.Size(748, 70);
            this.viewCommandTools2.TabIndex = 1;
            // 
            // viewCommandTools1
            // 
            this.viewCommandTools1.Checked = false;
            this.viewCommandTools1.Dock = System.Windows.Forms.DockStyle.Top;
            this.viewCommandTools1.Location = new System.Drawing.Point(0, 0);
            this.viewCommandTools1.Name = "viewCommandTools1";
            this.viewCommandTools1.Num = 1;
            this.viewCommandTools1.Size = new System.Drawing.Size(748, 70);
            this.viewCommandTools1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl1.Controls.Add(this.spinEdit1);
            this.panelControl1.Controls.Add(this.cbxGroup);
            this.panelControl1.Controls.Add(this.lblGroupName);
            this.panelControl1.Controls.Add(this.edtBeginNum);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.toolStrip2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(748, 94);
            this.panelControl1.TabIndex = 6;
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(140, 19);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.spinEdit1.Properties.Appearance.Options.UseFont = true;
            this.spinEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.spinEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.spinEdit1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Properties.IsFloatValue = false;
            this.spinEdit1.Properties.Mask.EditMask = "d";
            this.spinEdit1.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.spinEdit1.Properties.MinValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEdit1.Size = new System.Drawing.Size(46, 26);
            this.spinEdit1.TabIndex = 10;
            this.spinEdit1.ValueChanged += new System.EventHandler(this.cbxGroup_SelectedIndexChanged);
            // 
            // cbxGroup
            // 
            this.cbxGroup.Location = new System.Drawing.Point(296, 19);
            this.cbxGroup.Name = "cbxGroup";
            this.cbxGroup.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cbxGroup.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroup.Properties.Appearance.Options.UseFont = true;
            this.cbxGroup.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroup.Properties.AppearanceDisabled.Options.UseFont = true;
            this.cbxGroup.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroup.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cbxGroup.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroup.Properties.AppearanceFocused.Options.UseFont = true;
            this.cbxGroup.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroup.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.cbxGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxGroup.Properties.DropDownRows = 16;
            this.cbxGroup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxGroup.Size = new System.Drawing.Size(400, 26);
            this.cbxGroup.TabIndex = 9;
            this.cbxGroup.SelectedIndexChanged += new System.EventHandler(this.cbxGroup_SelectedIndexChanged);
            // 
            // lblGroupName
            // 
            this.lblGroupName.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblGroupName.Appearance.Options.UseFont = true;
            this.lblGroupName.Location = new System.Drawing.Point(226, 22);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(64, 19);
            this.lblGroupName.TabIndex = 8;
            this.lblGroupName.Text = "当前按键";
            // 
            // edtBeginNum
            // 
            this.edtBeginNum.EditValue = "1";
            this.edtBeginNum.Location = new System.Drawing.Point(87, 19);
            this.edtBeginNum.Name = "edtBeginNum";
            this.edtBeginNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtBeginNum.Properties.Appearance.Options.UseFont = true;
            this.edtBeginNum.Properties.Appearance.Options.UseTextOptions = true;
            this.edtBeginNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.edtBeginNum.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.edtBeginNum.Properties.Mask.EditMask = "d";
            this.edtBeginNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.edtBeginNum.Properties.ReadOnly = true;
            this.edtBeginNum.Size = new System.Drawing.Size(25, 26);
            this.edtBeginNum.TabIndex = 6;
            this.edtBeginNum.KeyUp += new System.Windows.Forms.KeyEventHandler(this.edtBeginNum_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(118, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(16, 19);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "到";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(65, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(16, 19);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "从";
            // 
            // UCtrlCommandEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xscCommands);
            this.Controls.Add(this.panelControl1);
            this.Name = "UCtrlCommandEdit";
            this.Size = new System.Drawing.Size(748, 454);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.xscCommands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtBeginNum.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ConfigDevice.ViewCommandTools viewCommandTools2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btSaveCommands;
        private ConfigDevice.ViewCommandTools viewCommandTools1;
        private DevExpress.XtraEditors.XtraScrollableControl xscCommands;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit edtBeginNum;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbxGroup;
        private DevExpress.XtraEditors.LabelControl lblGroupName;
        private ViewCommandTools viewCommandTools5;
        private ViewCommandTools viewCommandTools4;
        private ViewCommandTools viewCommandTools3;
        private System.Windows.Forms.ToolStripButton btSyncEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
    }
}
