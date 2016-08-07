namespace ConfigDevice
{
    partial class ViewBaseEdit
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btSaveAll = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.btSave = new System.Windows.Forms.ToolStripDropDownButton();
            this.btSaveID = new System.Windows.Forms.ToolStripMenuItem();
            this.btSaveNamePosition = new System.Windows.Forms.ToolStripMenuItem();
            this.btOpenLight = new System.Windows.Forms.ToolStripButton();
            this.btCloseLight = new System.Windows.Forms.ToolStripButton();
            this.btFindOn = new System.Windows.Forms.ToolStripButton();
            this.btFindOff = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.edtName = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.edtDeviceID = new DevExpress.XtraEditors.TextEdit();
            this.edtSoftwareVer = new DevExpress.XtraEditors.TextEdit();
            this.edtNetworkID = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbxPosition = new DevExpress.XtraEditors.ComboBoxEdit();
            this.edtHardwareVer = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.edtKind = new DevExpress.XtraEditors.TextEdit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtName.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtDeviceID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSoftwareVer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNetworkID.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtHardwareVer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtKind.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSaveAll,
            this.btRefresh,
            this.btSave,
            this.btOpenLight,
            this.btCloseLight,
            this.btFindOn,
            this.btFindOff});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(779, 31);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btSaveAll
            // 
            this.btSaveAll.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btSaveAll.Image = global::ConfigDevice.Properties.Resources.save;
            this.btSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveAll.Name = "btSaveAll";
            this.btSaveAll.Size = new System.Drawing.Size(74, 28);
            this.btSaveAll.Text = "保存 ";
            this.btSaveAll.Click += new System.EventHandler(this.btSaveAll_Click);
            // 
            // btRefresh
            // 
            this.btRefresh.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btRefresh.Image = global::ConfigDevice.Properties.Resources.refresh;
            this.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(106, 28);
            this.btRefresh.Text = "刷新数据 ";
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // btSave
            // 
            this.btSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSaveID,
            this.btSaveNamePosition});
            this.btSave.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btSave.Image = global::ConfigDevice.Properties.Resources.Point1;
            this.btSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(142, 28);
            this.btSave.Text = "保存基础配置";
            this.btSave.Visible = false;
            this.btSave.MouseHover += new System.EventHandler(this.btSave_MouseHover);
            // 
            // btSaveID
            // 
            this.btSaveID.Image = global::ConfigDevice.Properties.Resources.save;
            this.btSaveID.Name = "btSaveID";
            this.btSaveID.Size = new System.Drawing.Size(190, 24);
            this.btSaveID.Text = "保存设备ID";
            this.btSaveID.Click += new System.EventHandler(this.btSaveID_Click);
            // 
            // btSaveNamePosition
            // 
            this.btSaveNamePosition.Image = global::ConfigDevice.Properties.Resources.save;
            this.btSaveNamePosition.Name = "btSaveNamePosition";
            this.btSaveNamePosition.Size = new System.Drawing.Size(190, 24);
            this.btSaveNamePosition.Text = "保存名称及位置";
            this.btSaveNamePosition.Click += new System.EventHandler(this.btSaveNamePosition_Click);
            // 
            // btOpenLight
            // 
            this.btOpenLight.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btOpenLight.Image = global::ConfigDevice.Properties.Resources.open_light;
            this.btOpenLight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btOpenLight.Name = "btOpenLight";
            this.btOpenLight.Size = new System.Drawing.Size(138, 28);
            this.btOpenLight.Text = "通信指示灯开 ";
            this.btOpenLight.Click += new System.EventHandler(this.btOpenLight_Click);
            // 
            // btCloseLight
            // 
            this.btCloseLight.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btCloseLight.Image = global::ConfigDevice.Properties.Resources.close_light;
            this.btCloseLight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCloseLight.Name = "btCloseLight";
            this.btCloseLight.Size = new System.Drawing.Size(138, 28);
            this.btCloseLight.Text = "通讯指示灯关 ";
            this.btCloseLight.Click += new System.EventHandler(this.btCloseLight_Click);
            // 
            // btFindOn
            // 
            this.btFindOn.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btFindOn.Image = global::ConfigDevice.Properties.Resources.on;
            this.btFindOn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btFindOn.Name = "btFindOn";
            this.btFindOn.Size = new System.Drawing.Size(122, 28);
            this.btFindOn.Text = "发现设备开 ";
            this.btFindOn.Click += new System.EventHandler(this.btFindOn_Click);
            // 
            // btFindOff
            // 
            this.btFindOff.Image = global::ConfigDevice.Properties.Resources.off;
            this.btFindOff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btFindOff.Name = "btFindOff";
            this.btFindOff.Size = new System.Drawing.Size(110, 28);
            this.btFindOff.Text = "发现设备关 ";
            this.btFindOff.Click += new System.EventHandler(this.btFindOff_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(779, 333);
            this.panel1.TabIndex = 26;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.edtKind, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.edtName, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(47, 39);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(582, 137);
            this.tableLayoutPanel1.TabIndex = 40;
            // 
            // edtName
            // 
            this.edtName.Location = new System.Drawing.Point(113, 108);
            this.edtName.Name = "edtName";
            this.edtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtName.Properties.Appearance.Options.UseFont = true;
            this.edtName.Properties.MaxLength = 30;
            this.edtName.Size = new System.Drawing.Size(461, 26);
            this.edtName.TabIndex = 41;
            this.edtName.Leave += new System.EventHandler(this.edtName_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 28);
            this.label1.TabIndex = 27;
            this.label1.Text = "设备类型";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.edtDeviceID);
            this.panel2.Controls.Add(this.edtSoftwareVer);
            this.panel2.Controls.Add(this.edtNetworkID);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(113, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(466, 31);
            this.panel2.TabIndex = 32;
            // 
            // edtDeviceID
            // 
            this.edtDeviceID.Location = new System.Drawing.Point(160, 3);
            this.edtDeviceID.Name = "edtDeviceID";
            this.edtDeviceID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtDeviceID.Properties.Appearance.Options.UseFont = true;
            this.edtDeviceID.Properties.Mask.EditMask = "d";
            this.edtDeviceID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.edtDeviceID.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.edtDeviceID.Size = new System.Drawing.Size(107, 26);
            this.edtDeviceID.TabIndex = 41;
            // 
            // edtSoftwareVer
            // 
            this.edtSoftwareVer.Location = new System.Drawing.Point(356, 5);
            this.edtSoftwareVer.Name = "edtSoftwareVer";
            this.edtSoftwareVer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtSoftwareVer.Properties.Appearance.Options.UseFont = true;
            this.edtSoftwareVer.Properties.ReadOnly = true;
            this.edtSoftwareVer.Size = new System.Drawing.Size(105, 26);
            this.edtSoftwareVer.TabIndex = 41;
            // 
            // edtNetworkID
            // 
            this.edtNetworkID.Location = new System.Drawing.Point(0, 3);
            this.edtNetworkID.Name = "edtNetworkID";
            this.edtNetworkID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtNetworkID.Properties.Appearance.Options.UseFont = true;
            this.edtNetworkID.Properties.ReadOnly = true;
            this.edtNetworkID.Size = new System.Drawing.Size(99, 26);
            this.edtNetworkID.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label3.Location = new System.Drawing.Point(100, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 19);
            this.label3.TabIndex = 26;
            this.label3.Text = "设备ID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label6.Location = new System.Drawing.Point(277, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 19);
            this.label6.TabIndex = 32;
            this.label6.Text = "软件版本";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbxPosition);
            this.panel3.Controls.Add(this.edtHardwareVer);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(113, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(466, 30);
            this.panel3.TabIndex = 33;
            // 
            // cbxPosition
            // 
            this.cbxPosition.Location = new System.Drawing.Point(0, 2);
            this.cbxPosition.Name = "cbxPosition";
            this.cbxPosition.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxPosition.Properties.Appearance.Options.UseFont = true;
            this.cbxPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxPosition.Properties.DropDownRows = 15;
            this.cbxPosition.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxPosition.Size = new System.Drawing.Size(267, 26);
            this.cbxPosition.TabIndex = 41;
            // 
            // edtHardwareVer
            // 
            this.edtHardwareVer.Location = new System.Drawing.Point(356, 0);
            this.edtHardwareVer.Name = "edtHardwareVer";
            this.edtHardwareVer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtHardwareVer.Properties.Appearance.Options.UseFont = true;
            this.edtHardwareVer.Properties.ReadOnly = true;
            this.edtHardwareVer.Size = new System.Drawing.Size(105, 26);
            this.edtHardwareVer.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label7.Location = new System.Drawing.Point(277, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 19);
            this.label7.TabIndex = 34;
            this.label7.Text = "硬件版本";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label5.Location = new System.Drawing.Point(3, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 26);
            this.label5.TabIndex = 30;
            this.label5.Text = "设备安装位置";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label4.Location = new System.Drawing.Point(34, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 19);
            this.label4.TabIndex = 33;
            this.label4.Text = "设备名称";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 29);
            this.label2.TabIndex = 31;
            this.label2.Text = "网段ID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edtKind
            // 
            this.edtKind.Location = new System.Drawing.Point(113, 3);
            this.edtKind.Name = "edtKind";
            this.edtKind.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtKind.Properties.Appearance.Options.UseFont = true;
            this.edtKind.Properties.Mask.EditMask = "d";
            this.edtKind.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.edtKind.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.edtKind.Properties.ReadOnly = true;
            this.edtKind.Size = new System.Drawing.Size(461, 26);
            this.edtKind.TabIndex = 41;
            // 
            // ViewBaseEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ViewBaseEdit";
            this.Size = new System.Drawing.Size(779, 364);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtName.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtDeviceID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSoftwareVer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNetworkID.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtHardwareVer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtKind.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btFindOff;
        private System.Windows.Forms.ToolStripButton btFindOn;
        private System.Windows.Forms.ToolStripButton btCloseLight;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private System.Windows.Forms.ToolStripButton btOpenLight;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.TextEdit edtName;
        private DevExpress.XtraEditors.TextEdit edtSoftwareVer;
        private DevExpress.XtraEditors.TextEdit edtNetworkID;
        private DevExpress.XtraEditors.ComboBoxEdit cbxPosition;
        private DevExpress.XtraEditors.TextEdit edtHardwareVer;
        private DevExpress.XtraEditors.TextEdit edtDeviceID;
        private System.Windows.Forms.ToolStripDropDownButton btSave;
        private System.Windows.Forms.ToolStripMenuItem btSaveID;
        private System.Windows.Forms.ToolStripMenuItem btSaveNamePosition;
        private System.Windows.Forms.ToolStripButton btSaveAll;
        private DevExpress.XtraEditors.TextEdit edtKind;
    }
}
