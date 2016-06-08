﻿namespace ConfigDevice
{
    partial class ViewCommandSetting
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
            this.xscCommands = new DevExpress.XtraEditors.XtraScrollableControl();
            this.plTool = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.edtBeginNum = new DevExpress.XtraEditors.SpinEdit();
            this.edtEndNum = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.plCommandToolbar = new DevExpress.XtraEditors.PanelControl();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btSyncEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btSaveCommands = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblGroupName = new System.Windows.Forms.ToolStripLabel();
            this.cbxGroup = new System.Windows.Forms.ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.plTool)).BeginInit();
            this.plTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtBeginNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEndNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plCommandToolbar)).BeginInit();
            this.plCommandToolbar.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xscCommands
            // 
            this.xscCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xscCommands.Location = new System.Drawing.Point(0, 32);
            this.xscCommands.Name = "xscCommands";
            this.xscCommands.Size = new System.Drawing.Size(1000, 422);
            this.xscCommands.TabIndex = 5;
            // 
            // plTool
            // 
            this.plTool.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.plTool.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.plTool.Controls.Add(this.panelControl2);
            this.plTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTool.Location = new System.Drawing.Point(0, 0);
            this.plTool.Name = "plTool";
            this.plTool.Size = new System.Drawing.Size(1000, 32);
            this.plTool.TabIndex = 6;
            // 
            // panelControl2
            // 
            this.panelControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Controls.Add(this.plCommandToolbar);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1000, 32);
            this.panelControl2.TabIndex = 10;
            // 
            // panelControl4
            // 
            this.panelControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.edtBeginNum);
            this.panelControl4.Controls.Add(this.edtEndNum);
            this.panelControl4.Controls.Add(this.labelControl2);
            this.panelControl4.Controls.Add(this.labelControl1);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(651, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(349, 32);
            this.panelControl4.TabIndex = 1;
            // 
            // edtBeginNum
            // 
            this.edtBeginNum.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edtBeginNum.Location = new System.Drawing.Point(37, 3);
            this.edtBeginNum.Name = "edtBeginNum";
            this.edtBeginNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtBeginNum.Properties.Appearance.Options.UseFont = true;
            this.edtBeginNum.Properties.Appearance.Options.UseTextOptions = true;
            this.edtBeginNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.edtBeginNum.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.edtBeginNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edtBeginNum.Properties.IsFloatValue = false;
            this.edtBeginNum.Properties.Mask.EditMask = "d";
            this.edtBeginNum.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.edtBeginNum.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edtBeginNum.Size = new System.Drawing.Size(46, 26);
            this.edtBeginNum.TabIndex = 7;
            this.edtBeginNum.EditValueChanged += new System.EventHandler(this.edtEndNum_ValueChanged);
            // 
            // edtEndNum
            // 
            this.edtEndNum.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.edtEndNum.Location = new System.Drawing.Point(111, 3);
            this.edtEndNum.Name = "edtEndNum";
            this.edtEndNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.edtEndNum.Properties.Appearance.Options.UseFont = true;
            this.edtEndNum.Properties.Appearance.Options.UseTextOptions = true;
            this.edtEndNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.edtEndNum.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.edtEndNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edtEndNum.Properties.IsFloatValue = false;
            this.edtEndNum.Properties.Mask.EditMask = "d";
            this.edtEndNum.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.edtEndNum.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edtEndNum.Size = new System.Drawing.Size(46, 26);
            this.edtEndNum.TabIndex = 6;
            this.edtEndNum.EditValueChanged += new System.EventHandler(this.edtEndNum_ValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(89, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(16, 19);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "到";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(15, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(16, 19);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "从";
            // 
            // plCommandToolbar
            // 
            this.plCommandToolbar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.plCommandToolbar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.plCommandToolbar.Controls.Add(this.toolStrip2);
            this.plCommandToolbar.Dock = System.Windows.Forms.DockStyle.Left;
            this.plCommandToolbar.Location = new System.Drawing.Point(0, 0);
            this.plCommandToolbar.Name = "plCommandToolbar";
            this.plCommandToolbar.Size = new System.Drawing.Size(651, 32);
            this.plCommandToolbar.TabIndex = 0;
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
            this.btRefresh,
            this.toolStripSeparator2,
            this.lblGroupName,
            this.cbxGroup});
            this.toolStrip2.Location = new System.Drawing.Point(0, 1);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(651, 31);
            this.toolStrip2.TabIndex = 4;
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
            this.btSaveCommands.Click += new System.EventHandler(this.btSaveCommands_Click);
            // 
            // btRefresh
            // 
            this.btRefresh.Image = global::ConfigDevice.Properties.Resources.refresh;
            this.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(74, 28);
            this.btRefresh.Text = "刷新 ";
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // lblGroupName
            // 
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(41, 28);
            this.lblGroupName.Text = "组名";
            this.lblGroupName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxGroup
            // 
            this.cbxGroup.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxGroup.Name = "cbxGroup";
            this.cbxGroup.Size = new System.Drawing.Size(300, 31);
            this.cbxGroup.SelectedIndexChanged += new System.EventHandler(this.cbxGroup_SelectedIndexChanged);
            // 
            // ViewCommandSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xscCommands);
            this.Controls.Add(this.plTool);
            this.Name = "ViewCommandSetting";
            this.Size = new System.Drawing.Size(1000, 454);
            ((System.ComponentModel.ISupportInitialize)(this.plTool)).EndInit();
            this.plTool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtBeginNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEndNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plCommandToolbar)).EndInit();
            this.plCommandToolbar.ResumeLayout(false);
            this.plCommandToolbar.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xscCommands;
        private DevExpress.XtraEditors.PanelControl plTool;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SpinEdit edtBeginNum;
        private DevExpress.XtraEditors.SpinEdit edtEndNum;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl plCommandToolbar;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btSyncEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btSaveCommands;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblGroupName;
        private System.Windows.Forms.ToolStripComboBox cbxGroup;
    }
}
