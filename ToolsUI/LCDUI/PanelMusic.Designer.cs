namespace ConfigDevice
{
    partial class PanelMusic
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
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.lookUpEditAmp = new DevExpress.XtraEditors.LookUpEdit();
            this.speAmp = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAmp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speAmp.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl3
            // 
            this.groupControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl3.Controls.Add(this.lookUpEditAmp);
            this.groupControl3.Controls.Add(this.speAmp);
            this.groupControl3.Controls.Add(this.labelControl7);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(849, 222);
            this.groupControl3.TabIndex = 1;
            this.groupControl3.Text = "音乐面板配置";
            // 
            // lookUpEditAmp
            // 
            this.lookUpEditAmp.EditValue = "";
            this.lookUpEditAmp.Location = new System.Drawing.Point(116, 57);
            this.lookUpEditAmp.Name = "lookUpEditAmp";
            this.lookUpEditAmp.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lookUpEditAmp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditAmp.Properties.NullText = "选择功放设备";
            this.lookUpEditAmp.Size = new System.Drawing.Size(112, 21);
            this.lookUpEditAmp.TabIndex = 18;
            this.lookUpEditAmp.EditValueChanged += new System.EventHandler(this.speHintVolume_EditValueChanged);
            // 
            // speAmp
            // 
            this.speAmp.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speAmp.Location = new System.Drawing.Point(328, 57);
            this.speAmp.Name = "speAmp";
            this.speAmp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speAmp.Properties.Mask.EditMask = "d";
            this.speAmp.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.speAmp.Properties.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.speAmp.Size = new System.Drawing.Size(50, 21);
            this.speAmp.TabIndex = 17;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(52, 60);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(60, 14);
            this.labelControl7.TabIndex = 15;
            this.labelControl7.Text = "提示音功放";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(238, 60);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 14);
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "提示音功放地址";
            // 
            // PanelMusic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl3);
            this.Name = "PanelMusic";
            this.Size = new System.Drawing.Size(849, 222);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAmp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speAmp.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditAmp;
        private DevExpress.XtraEditors.SpinEdit speAmp;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
