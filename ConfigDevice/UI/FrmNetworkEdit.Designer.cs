namespace ConfigDevice
{
    partial class FrmNetworkEdit
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
            this.ipInputTextbox1 = new IpInputExt.Ctrls.IpInputTextbox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btSaveInfo = new System.Windows.Forms.ToolStripButton();
            this.btOpenLight = new System.Windows.Forms.ToolStripButton();
            this.edtNetworkID = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.edtSoftwareVer = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.edtNetworkIP = new ConfigDevice.IpInputTextbox();
            this.edtMask = new ConfigDevice.IpInputTextbox();
            this.edtGateway = new ConfigDevice.IpInputTextbox();
            this.edtHarewareVer = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.edtNetworkName = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.gcPosition = new DevExpress.XtraGrid.GridControl();
            this.gvPosition = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.edtName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.password = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cedtHasPassword = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtNetworkID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSoftwareVer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtHarewareVer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNetworkName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtHasPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ipInputTextbox1
            // 
            this.ipInputTextbox1.IP = "IP地址格式不正确";
            this.ipInputTextbox1.Location = new System.Drawing.Point(0, 0);
            this.ipInputTextbox1.Name = "ipInputTextbox1";
            this.ipInputTextbox1.Size = new System.Drawing.Size(160, 21);
            this.ipInputTextbox1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSaveInfo,
            this.btOpenLight});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 31);
            this.toolStrip1.TabIndex = 16;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btSaveInfo
            // 
            this.btSaveInfo.Image = global::ConfigDevice.Properties.Resources.Point1;
            this.btSaveInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveInfo.Name = "btSaveInfo";
            this.btSaveInfo.Size = new System.Drawing.Size(92, 28);
            this.btSaveInfo.Text = "保存信息";
            // 
            // btOpenLight
            // 
            this.btOpenLight.Image = global::ConfigDevice.Properties.Resources.Point1;
            this.btOpenLight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btOpenLight.Name = "btOpenLight";
            this.btOpenLight.Size = new System.Drawing.Size(92, 28);
            this.btOpenLight.Text = "保存位置";
            // 
            // edtNetworkID
            // 
            this.edtNetworkID.Location = new System.Drawing.Point(113, 56);
            this.edtNetworkID.Name = "edtNetworkID";
            this.edtNetworkID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtNetworkID.Properties.Appearance.Options.UseFont = true;
            this.edtNetworkID.Size = new System.Drawing.Size(240, 26);
            this.edtNetworkID.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label1.Location = new System.Drawing.Point(69, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 19);
            this.label1.TabIndex = 19;
            this.label1.Text = "网段";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label2.Location = new System.Drawing.Point(54, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 19);
            this.label2.TabIndex = 19;
            this.label2.Text = "IP地址";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label3.Location = new System.Drawing.Point(69, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 19);
            this.label3.TabIndex = 19;
            this.label3.Text = "网关";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label4.Location = new System.Drawing.Point(37, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 19);
            this.label4.TabIndex = 19;
            this.label4.Text = "子网掩码";
            // 
            // edtSoftwareVer
            // 
            this.edtSoftwareVer.Location = new System.Drawing.Point(438, 57);
            this.edtSoftwareVer.Name = "edtSoftwareVer";
            this.edtSoftwareVer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtSoftwareVer.Properties.Appearance.Options.UseFont = true;
            this.edtSoftwareVer.Properties.ReadOnly = true;
            this.edtSoftwareVer.Size = new System.Drawing.Size(240, 26);
            this.edtSoftwareVer.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label5.Location = new System.Drawing.Point(363, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 19);
            this.label5.TabIndex = 19;
            this.label5.Text = "软件版本";
            // 
            // edtNetworkIP
            // 
            this.edtNetworkIP.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtNetworkIP.IP = "IP地址格式不正确";
            this.edtNetworkIP.Location = new System.Drawing.Point(113, 88);
            this.edtNetworkIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtNetworkIP.Name = "edtNetworkIP";
            this.edtNetworkIP.Size = new System.Drawing.Size(240, 26);
            this.edtNetworkIP.TabIndex = 18;
            // 
            // edtMask
            // 
            this.edtMask.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtMask.IP = "IP地址格式不正确";
            this.edtMask.Location = new System.Drawing.Point(113, 154);
            this.edtMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtMask.Name = "edtMask";
            this.edtMask.Size = new System.Drawing.Size(240, 26);
            this.edtMask.TabIndex = 18;
            // 
            // edtGateway
            // 
            this.edtGateway.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtGateway.IP = "IP地址格式不正确";
            this.edtGateway.Location = new System.Drawing.Point(113, 121);
            this.edtGateway.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtGateway.Name = "edtGateway";
            this.edtGateway.Size = new System.Drawing.Size(240, 26);
            this.edtGateway.TabIndex = 18;
            // 
            // edtHarewareVer
            // 
            this.edtHarewareVer.Location = new System.Drawing.Point(438, 93);
            this.edtHarewareVer.Name = "edtHarewareVer";
            this.edtHarewareVer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtHarewareVer.Properties.Appearance.Options.UseFont = true;
            this.edtHarewareVer.Properties.ReadOnly = true;
            this.edtHarewareVer.Size = new System.Drawing.Size(240, 26);
            this.edtHarewareVer.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label6.Location = new System.Drawing.Point(363, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 19);
            this.label6.TabIndex = 19;
            this.label6.Text = "硬件版本";
            // 
            // edtNetworkName
            // 
            this.edtNetworkName.Location = new System.Drawing.Point(113, 24);
            this.edtNetworkName.Name = "edtNetworkName";
            this.edtNetworkName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtNetworkName.Properties.Appearance.Options.UseFont = true;
            this.edtNetworkName.Size = new System.Drawing.Size(565, 26);
            this.edtNetworkName.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label7.Location = new System.Drawing.Point(69, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 19);
            this.label7.TabIndex = 19;
            this.label7.Text = "名称";
            // 
            // gcPosition
            // 
            this.gcPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPosition.EmbeddedNavigator.Name = "";
            this.gcPosition.FormsUseDefaultLookAndFeel = false;
            this.gcPosition.Location = new System.Drawing.Point(0, 232);
            this.gcPosition.MainView = this.gvPosition;
            this.gcPosition.Name = "gcPosition";
            this.gcPosition.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.cedtHasPassword,
            this.edtName});
            this.gcPosition.Size = new System.Drawing.Size(792, 341);
            this.gcPosition.TabIndex = 20;
            this.gcPosition.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPosition});
            // 
            // gvPosition
            // 
            this.gvPosition.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.num,
            this.name,
            this.password,
            this.gridColumn5,
            this.gridColumn3,
            this.gridColumn4});
            this.gvPosition.GridControl = this.gcPosition;
            this.gvPosition.Name = "gvPosition";
            this.gvPosition.OptionsView.ShowGroupPanel = false;
            this.gvPosition.OptionsView.ShowIndicator = false;
            this.gvPosition.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.num, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // num
            // 
            this.num.AppearanceCell.Options.UseTextOptions = true;
            this.num.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.num.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.num.AppearanceHeader.Options.UseTextOptions = true;
            this.num.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.num.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.num.Caption = "编号";
            this.num.Name = "num";
            this.num.OptionsColumn.ReadOnly = true;
            this.num.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.num.Visible = true;
            this.num.VisibleIndex = 0;
            // 
            // name
            // 
            this.name.AppearanceCell.Options.UseTextOptions = true;
            this.name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.name.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.name.AppearanceHeader.Options.UseTextOptions = true;
            this.name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.name.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.name.Caption = "名称";
            this.name.ColumnEdit = this.edtName;
            this.name.Name = "name";
            this.name.Visible = true;
            this.name.VisibleIndex = 1;
            // 
            // edtName
            // 
            this.edtName.AutoHeight = false;
            this.edtName.Name = "edtName";
            this.edtName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.edtName_KeyUp);
            // 
            // password
            // 
            this.password.AppearanceCell.Options.UseTextOptions = true;
            this.password.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.password.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.password.AppearanceHeader.Options.UseTextOptions = true;
            this.password.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.password.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.password.Caption = "密码设置";
            this.password.ColumnEdit = this.cedtHasPassword;
            this.password.Name = "password";
            this.password.Visible = true;
            this.password.VisibleIndex = 2;
            // 
            // cedtHasPassword
            // 
            this.cedtHasPassword.AutoHeight = false;
            this.cedtHasPassword.Name = "cedtHasPassword";
            this.cedtHasPassword.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.Caption = "gridColumn5";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.Caption = "gridColumn3";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "gridColumn4";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "有",
            "无"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl1.Controls.Add(this.edtNetworkName);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.edtNetworkID);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.edtSoftwareVer);
            this.panelControl1.Controls.Add(this.edtNetworkIP);
            this.panelControl1.Controls.Add(this.edtHarewareVer);
            this.panelControl1.Controls.Add(this.edtMask);
            this.panelControl1.Controls.Add(this.edtGateway);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 31);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(792, 201);
            this.panelControl1.TabIndex = 21;
            // 
            // FrmNetworkEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.gcPosition);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmNetworkEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网络信息";
            this.Load += new System.EventHandler(this.FrmNetworkEdit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtNetworkID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSoftwareVer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtHarewareVer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNetworkName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cedtHasPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IpInputExt.Ctrls.IpInputTextbox  ipInputTextbox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btSaveInfo;
        private System.Windows.Forms.ToolStripButton btOpenLight;
        private DevExpress.XtraEditors.TextEdit edtNetworkID;
        private IpInputTextbox edtGateway;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private IpInputTextbox edtNetworkIP;
        private IpInputTextbox edtMask;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit edtSoftwareVer;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit edtHarewareVer;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit edtNetworkName;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraGrid.GridControl gcPosition;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPosition;
        private DevExpress.XtraGrid.Columns.GridColumn num;
        private DevExpress.XtraGrid.Columns.GridColumn name;
        private DevExpress.XtraGrid.Columns.GridColumn password;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit cedtHasPassword;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edtName;
    }
}