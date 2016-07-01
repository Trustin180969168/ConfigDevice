namespace ConfigDevice
{
    partial class FrmMotorDC3
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
            this.pagePzcl = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dcPosition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcMotorStopEC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.speEC = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.dcMotorTurnTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.speSecond = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dcPosition2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcCurrentEC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spedtEC = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.dcAction1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkAction = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.dcAction2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcAction3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcAction4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tsDoorInput = new System.Windows.Forms.ToolStrip();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.pageJcsz = new DevExpress.XtraTab.XtraTabPage();
            this.viewBaseSetting = new ConfigDevice.ViewBaseEdit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.pagePzcl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speEC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spedtEC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            this.tsDoorInput.SuspendLayout();
            this.pageJcsz.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 12F);
            this.xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 24);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.pagePzcl;
            this.xtraTabControl1.Size = new System.Drawing.Size(792, 549);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageJcsz,
            this.pagePzcl});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // pagePzcl
            // 
            this.pagePzcl.Controls.Add(this.panelControl1);
            this.pagePzcl.Controls.Add(this.tsDoorInput);
            this.pagePzcl.Name = "pagePzcl";
            this.pagePzcl.Size = new System.Drawing.Size(783, 512);
            this.pagePzcl.Text = "配置窗帘";
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 31);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(783, 481);
            this.panelControl1.TabIndex = 3;
            // 
            // groupControl1
            // 
            this.groupControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(24, 23);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(717, 120);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "电机配置";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.FormsUseDefaultLookAndFeel = false;
            this.gridControl1.Location = new System.Drawing.Point(2, 21);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.speEC,
            this.speSecond});
            this.gridControl1.Size = new System.Drawing.Size(713, 97);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dcPosition,
            this.dcName,
            this.dcMotorStopEC,
            this.dcMotorTurnTime});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // dcPosition
            // 
            this.dcPosition.AppearanceCell.Options.UseTextOptions = true;
            this.dcPosition.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcPosition.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcPosition.AppearanceHeader.Options.UseTextOptions = true;
            this.dcPosition.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcPosition.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.Character;
            this.dcPosition.Caption = "位置";
            this.dcPosition.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.dcPosition.Name = "dcPosition";
            this.dcPosition.OptionsColumn.ReadOnly = true;
            this.dcPosition.Visible = true;
            this.dcPosition.VisibleIndex = 0;
            this.dcPosition.Width = 100;
            // 
            // dcName
            // 
            this.dcName.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcName.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcName.AppearanceCell.Options.UseBackColor = true;
            this.dcName.AppearanceCell.Options.UseForeColor = true;
            this.dcName.AppearanceCell.Options.UseTextOptions = true;
            this.dcName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcName.AppearanceHeader.Options.UseTextOptions = true;
            this.dcName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcName.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.Character;
            this.dcName.Caption = "电机名称";
            this.dcName.Name = "dcName";
            this.dcName.Visible = true;
            this.dcName.VisibleIndex = 1;
            this.dcName.Width = 156;
            // 
            // dcMotorStopEC
            // 
            this.dcMotorStopEC.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcMotorStopEC.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcMotorStopEC.AppearanceCell.Options.UseBackColor = true;
            this.dcMotorStopEC.AppearanceCell.Options.UseForeColor = true;
            this.dcMotorStopEC.AppearanceCell.Options.UseTextOptions = true;
            this.dcMotorStopEC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcMotorStopEC.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcMotorStopEC.AppearanceHeader.Options.UseTextOptions = true;
            this.dcMotorStopEC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcMotorStopEC.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.Character;
            this.dcMotorStopEC.Caption = "电机卡停电流";
            this.dcMotorStopEC.ColumnEdit = this.speEC;
            this.dcMotorStopEC.Name = "dcMotorStopEC";
            this.dcMotorStopEC.Visible = true;
            this.dcMotorStopEC.VisibleIndex = 2;
            this.dcMotorStopEC.Width = 156;
            // 
            // speEC
            // 
            this.speEC.AutoHeight = false;
            this.speEC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speEC.Mask.EditMask = "d";
            this.speEC.Name = "speEC";
            // 
            // dcMotorTurnTime
            // 
            this.dcMotorTurnTime.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcMotorTurnTime.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcMotorTurnTime.AppearanceCell.Options.UseBackColor = true;
            this.dcMotorTurnTime.AppearanceCell.Options.UseForeColor = true;
            this.dcMotorTurnTime.AppearanceCell.Options.UseTextOptions = true;
            this.dcMotorTurnTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcMotorTurnTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcMotorTurnTime.AppearanceHeader.Options.UseTextOptions = true;
            this.dcMotorTurnTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcMotorTurnTime.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.Character;
            this.dcMotorTurnTime.Caption = "最长转动时间";
            this.dcMotorTurnTime.ColumnEdit = this.speSecond;
            this.dcMotorTurnTime.Name = "dcMotorTurnTime";
            this.dcMotorTurnTime.Visible = true;
            this.dcMotorTurnTime.VisibleIndex = 3;
            this.dcMotorTurnTime.Width = 160;
            // 
            // speSecond
            // 
            this.speSecond.AutoHeight = false;
            this.speSecond.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speSecond.Mask.EditMask = "d";
            this.speSecond.Name = "speSecond";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.Name = "gridView2";
            // 
            // groupControl2
            // 
            this.groupControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl2.Controls.Add(this.gridControl2);
            this.groupControl2.Location = new System.Drawing.Point(22, 167);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(717, 120);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "电机状态及测试";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.EmbeddedNavigator.Name = "";
            this.gridControl2.FormsUseDefaultLookAndFeel = false;
            this.gridControl2.Location = new System.Drawing.Point(2, 21);
            this.gridControl2.MainView = this.gridView3;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.spedtEC,
            this.linkAction});
            this.gridControl2.Size = new System.Drawing.Size(713, 97);
            this.gridControl2.TabIndex = 2;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3,
            this.gridView4});
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dcPosition2,
            this.dcState,
            this.dcCurrentEC,
            this.dcAction1,
            this.dcAction2,
            this.dcAction3,
            this.dcAction4});
            this.gridView3.GridControl = this.gridControl2;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.OptionsView.ShowIndicator = false;
            // 
            // dcPosition2
            // 
            this.dcPosition2.AppearanceCell.Options.UseTextOptions = true;
            this.dcPosition2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcPosition2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcPosition2.AppearanceHeader.Options.UseTextOptions = true;
            this.dcPosition2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcPosition2.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.Character;
            this.dcPosition2.Caption = "位置";
            this.dcPosition2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.dcPosition2.Name = "dcPosition2";
            this.dcPosition2.OptionsColumn.ReadOnly = true;
            this.dcPosition2.Visible = true;
            this.dcPosition2.VisibleIndex = 0;
            this.dcPosition2.Width = 100;
            // 
            // dcState
            // 
            this.dcState.AppearanceCell.Options.UseTextOptions = true;
            this.dcState.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcState.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcState.AppearanceHeader.Options.UseTextOptions = true;
            this.dcState.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcState.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcState.Caption = "当前状态";
            this.dcState.Name = "dcState";
            this.dcState.OptionsColumn.ReadOnly = true;
            this.dcState.Visible = true;
            this.dcState.VisibleIndex = 1;
            this.dcState.Width = 135;
            // 
            // dcCurrentEC
            // 
            this.dcCurrentEC.AppearanceCell.Options.UseTextOptions = true;
            this.dcCurrentEC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcCurrentEC.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcCurrentEC.AppearanceHeader.Options.UseTextOptions = true;
            this.dcCurrentEC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcCurrentEC.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcCurrentEC.Caption = "当前电流";
            this.dcCurrentEC.ColumnEdit = this.spedtEC;
            this.dcCurrentEC.Name = "dcCurrentEC";
            this.dcCurrentEC.OptionsColumn.ReadOnly = true;
            this.dcCurrentEC.Visible = true;
            this.dcCurrentEC.VisibleIndex = 2;
            this.dcCurrentEC.Width = 135;
            // 
            // spedtEC
            // 
            this.spedtEC.AutoHeight = false;
            this.spedtEC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spedtEC.Mask.EditMask = "d";
            this.spedtEC.Name = "spedtEC";
            // 
            // dcAction1
            // 
            this.dcAction1.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcAction1.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcAction1.AppearanceCell.Options.UseBackColor = true;
            this.dcAction1.AppearanceCell.Options.UseForeColor = true;
            this.dcAction1.AppearanceCell.Options.UseTextOptions = true;
            this.dcAction1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcAction1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcAction1.AppearanceHeader.Options.UseTextOptions = true;
            this.dcAction1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcAction1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcAction1.Caption = "动作1";
            this.dcAction1.ColumnEdit = this.linkAction;
            this.dcAction1.Name = "dcAction1";
            this.dcAction1.Visible = true;
            this.dcAction1.VisibleIndex = 3;
            this.dcAction1.Width = 94;
            // 
            // linkAction
            // 
            this.linkAction.AutoHeight = false;
            this.linkAction.Name = "linkAction";
            // 
            // dcAction2
            // 
            this.dcAction2.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcAction2.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcAction2.AppearanceCell.Options.UseBackColor = true;
            this.dcAction2.AppearanceCell.Options.UseForeColor = true;
            this.dcAction2.AppearanceCell.Options.UseTextOptions = true;
            this.dcAction2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcAction2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcAction2.AppearanceHeader.Options.UseTextOptions = true;
            this.dcAction2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcAction2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcAction2.Caption = "动作2";
            this.dcAction2.ColumnEdit = this.linkAction;
            this.dcAction2.Name = "dcAction2";
            this.dcAction2.Visible = true;
            this.dcAction2.VisibleIndex = 4;
            this.dcAction2.Width = 79;
            // 
            // dcAction3
            // 
            this.dcAction3.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcAction3.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcAction3.AppearanceCell.Options.UseBackColor = true;
            this.dcAction3.AppearanceCell.Options.UseForeColor = true;
            this.dcAction3.AppearanceCell.Options.UseTextOptions = true;
            this.dcAction3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcAction3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcAction3.AppearanceHeader.Options.UseTextOptions = true;
            this.dcAction3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcAction3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcAction3.Caption = "动作3";
            this.dcAction3.ColumnEdit = this.linkAction;
            this.dcAction3.Name = "dcAction3";
            this.dcAction3.Visible = true;
            this.dcAction3.VisibleIndex = 5;
            this.dcAction3.Width = 79;
            // 
            // dcAction4
            // 
            this.dcAction4.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcAction4.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcAction4.AppearanceCell.Options.UseBackColor = true;
            this.dcAction4.AppearanceCell.Options.UseForeColor = true;
            this.dcAction4.AppearanceCell.Options.UseTextOptions = true;
            this.dcAction4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcAction4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcAction4.AppearanceHeader.Options.UseTextOptions = true;
            this.dcAction4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcAction4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcAction4.Caption = "动作4";
            this.dcAction4.ColumnEdit = this.linkAction;
            this.dcAction4.Name = "dcAction4";
            this.dcAction4.Visible = true;
            this.dcAction4.VisibleIndex = 6;
            this.dcAction4.Width = 85;
            // 
            // gridView4
            // 
            this.gridView4.GridControl = this.gridControl2;
            this.gridView4.Name = "gridView4";
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
            this.tsDoorInput.TabIndex = 2;
            this.tsDoorInput.Text = "toolStrip2";
            // 
            // btSave
            // 
            this.btSave.Image = global::ConfigDevice.Properties.Resources.save;
            this.btSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(76, 28);
            this.btSave.Text = "保存 ";
            // 
            // btRefresh
            // 
            this.btRefresh.Image = global::ConfigDevice.Properties.Resources.refresh;
            this.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(100, 28);
            this.btRefresh.Text = "刷新数据";
            // 
            // pageJcsz
            // 
            this.pageJcsz.Controls.Add(this.viewBaseSetting);
            this.pageJcsz.Name = "pageJcsz";
            this.pageJcsz.Size = new System.Drawing.Size(783, 512);
            this.pageJcsz.Text = "基础配置";
            // 
            // viewBaseSetting
            // 
            this.viewBaseSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewBaseSetting.Location = new System.Drawing.Point(0, 0);
            this.viewBaseSetting.Name = "viewBaseSetting";
            this.viewBaseSetting.Size = new System.Drawing.Size(783, 512);
            this.viewBaseSetting.TabIndex = 0;
            // 
            // FrmMotorDC3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "FrmMotorDC3";
            this.Text = "FrmBaseDevice";
            this.Load += new System.EventHandler(this.FrmMotorDC3_Load);
            this.Controls.SetChildIndex(this.xtraTabControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.pagePzcl.ResumeLayout(false);
            this.pagePzcl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speEC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spedtEC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            this.tsDoorInput.ResumeLayout(false);
            this.tsDoorInput.PerformLayout();
            this.pageJcsz.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage pageJcsz;
        private ViewBaseEdit viewBaseSetting;
        private DevExpress.XtraTab.XtraTabPage pagePzcl;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn dcPosition;
        private DevExpress.XtraGrid.Columns.GridColumn dcName;
        private DevExpress.XtraGrid.Columns.GridColumn dcMotorStopEC;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit speEC;
        private DevExpress.XtraGrid.Columns.GridColumn dcMotorTurnTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit speSecond;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn dcPosition2;
        private DevExpress.XtraGrid.Columns.GridColumn dcState;
        private DevExpress.XtraGrid.Columns.GridColumn dcCurrentEC;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spedtEC;
        private DevExpress.XtraGrid.Columns.GridColumn dcAction1;
        private DevExpress.XtraGrid.Columns.GridColumn dcAction2;
        private DevExpress.XtraGrid.Columns.GridColumn dcAction3;
        private DevExpress.XtraGrid.Columns.GridColumn dcAction4;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private System.Windows.Forms.ToolStrip tsDoorInput;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkAction;


    }
}