namespace ConfigDevice
{
    partial class FrmDriver
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.pageJcsz = new DevExpress.XtraTab.XtraTabPage();
            this.BaseViewSetting = new ConfigDevice.ViewBaseEdit();
            this.pagePzjm = new DevExpress.XtraTab.XtraTabPage();
            this.tsDoorInput = new System.Windows.Forms.ToolStrip();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.gccSecurity = new DevExpress.XtraEditors.GroupControl();
            this.gcCircuit = new DevExpress.XtraGrid.GridControl();
            this.gvCircuit = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.pageJcsz.SuspendLayout();
            this.pagePzjm.SuspendLayout();
            this.tsDoorInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gccSecurity)).BeginInit();
            this.gccSecurity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCircuit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCircuit)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 12F);
            this.xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 24);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.pagePzjm;
            this.xtraTabControl1.Size = new System.Drawing.Size(792, 549);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageJcsz,
            this.pagePzjm});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // pageJcsz
            // 
            this.pageJcsz.Controls.Add(this.BaseViewSetting);
            this.pageJcsz.Name = "pageJcsz";
            this.pageJcsz.Size = new System.Drawing.Size(783, 512);
            this.pageJcsz.Text = "基础配置";
            // 
            // BaseViewSetting
            // 
            this.BaseViewSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseViewSetting.Location = new System.Drawing.Point(0, 0);
            this.BaseViewSetting.Name = "BaseViewSetting";
            this.BaseViewSetting.Size = new System.Drawing.Size(783, 512);
            this.BaseViewSetting.TabIndex = 0;
            // 
            // pagePzjm
            // 
            this.pagePzjm.Controls.Add(this.tsDoorInput);
            this.pagePzjm.Controls.Add(this.gccSecurity);
            this.pagePzjm.Name = "pagePzjm";
            this.pagePzjm.Size = new System.Drawing.Size(783, 512);
            this.pagePzjm.Text = "配置界面";
            // 
            // tsDoorInput
            // 
            this.tsDoorInput.Font = new System.Drawing.Font("宋体", 12F);
            this.tsDoorInput.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsDoorInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSave,
            this.btRefresh});
            this.tsDoorInput.Location = new System.Drawing.Point(0, 0);
            this.tsDoorInput.Name = "tsDoorInput";
            this.tsDoorInput.Size = new System.Drawing.Size(783, 31);
            this.tsDoorInput.TabIndex = 7;
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
            // btRefresh
            // 
            this.btRefresh.Image = global::ConfigDevice.Properties.Resources.refresh;
            this.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(100, 28);
            this.btRefresh.Text = "刷新数据";
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // gccSecurity
            // 
            this.gccSecurity.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.gccSecurity.Controls.Add(this.gcCircuit);
            this.gccSecurity.Location = new System.Drawing.Point(49, 53);
            this.gccSecurity.Name = "gccSecurity";
            this.gccSecurity.Size = new System.Drawing.Size(523, 386);
            this.gccSecurity.TabIndex = 6;
            this.gccSecurity.Text = "回路名称";
            // 
            // gcCircuit
            // 
            this.gcCircuit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCircuit.EmbeddedNavigator.Name = "";
            this.gcCircuit.FormsUseDefaultLookAndFeel = false;
            this.gcCircuit.Location = new System.Drawing.Point(2, 21);
            this.gcCircuit.MainView = this.gvCircuit;
            this.gcCircuit.Name = "gcCircuit";
            this.gcCircuit.Size = new System.Drawing.Size(519, 363);
            this.gcCircuit.TabIndex = 21;
            this.gcCircuit.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCircuit});
            // 
            // gvCircuit
            // 
            this.gvCircuit.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.num,
            this.name,
            this.gridColumn5,
            this.gridColumn3,
            this.gridColumn4});
            this.gvCircuit.GridControl = this.gcCircuit;
            this.gvCircuit.Name = "gvCircuit";
            this.gvCircuit.OptionsView.ShowGroupPanel = false;
            this.gvCircuit.OptionsView.ShowIndicator = false;
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
            this.num.Width = 50;
            // 
            // name
            // 
            this.name.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.name.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.name.AppearanceCell.Options.UseBackColor = true;
            this.name.AppearanceCell.Options.UseForeColor = true;
            this.name.AppearanceCell.Options.UseTextOptions = true;
            this.name.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.name.AppearanceHeader.Options.UseTextOptions = true;
            this.name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.name.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.name.Caption = "名称";
            this.name.Name = "name";
            this.name.Visible = true;
            this.name.VisibleIndex = 1;
            this.name.Width = 213;
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
            // FrmDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "FrmDriver";
            this.Text = "驱动器";
            this.Load += new System.EventHandler(this.FrmBaseDevice_Load);
            this.Controls.SetChildIndex(this.xtraTabControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.pageJcsz.ResumeLayout(false);
            this.pagePzjm.ResumeLayout(false);
            this.pagePzjm.PerformLayout();
            this.tsDoorInput.ResumeLayout(false);
            this.tsDoorInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gccSecurity)).EndInit();
            this.gccSecurity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCircuit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCircuit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage pageJcsz;
        private ViewBaseEdit BaseViewSetting;
        private DevExpress.XtraTab.XtraTabPage pagePzjm;
        private DevExpress.XtraEditors.GroupControl gccSecurity;
        private DevExpress.XtraGrid.GridControl gcCircuit;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCircuit;
        private DevExpress.XtraGrid.Columns.GridColumn num;
        private DevExpress.XtraGrid.Columns.GridColumn name;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.ToolStrip tsDoorInput;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripButton btRefresh;


    }
}