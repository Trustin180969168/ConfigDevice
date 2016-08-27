namespace ConfigDevice
{
    partial class ViewSecurity
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
            this.ceLeaveSafeSetting = new DevExpress.XtraEditors.CheckedListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.ceLeaveSafeSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // ceLeaveSafeSetting
            // 
            this.ceLeaveSafeSetting.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ceLeaveSafeSetting.CheckOnClick = true;
            this.ceLeaveSafeSetting.ColumnWidth = 60;
            this.ceLeaveSafeSetting.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.ceLeaveSafeSetting.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("1"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("2"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("3"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("4"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("5"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("6"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("7"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("8"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("9"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("10"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("11"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("12"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("13"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("14"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("15"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("全部")});
            this.ceLeaveSafeSetting.LeftCoord = 0;
            this.ceLeaveSafeSetting.Location = new System.Drawing.Point(0, 0);
            this.ceLeaveSafeSetting.MultiColumn = true;
            this.ceLeaveSafeSetting.Name = "ceLeaveSafeSetting";
            this.ceLeaveSafeSetting.Size = new System.Drawing.Size(374, 60);
            this.ceLeaveSafeSetting.TabIndex = 1;
            // 
            // ViewSecurity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.ceLeaveSafeSetting);
            this.Name = "ViewSecurity";
            this.Size = new System.Drawing.Size(374, 60);
            ((System.ComponentModel.ISupportInitialize)(this.ceLeaveSafeSetting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.CheckedListBoxControl ceLeaveSafeSetting;
    }
}
