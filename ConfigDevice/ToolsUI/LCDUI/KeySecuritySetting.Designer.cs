namespace ConfigDevice.ToolsUI.LCDUI
{
    partial class KeySecuritySetting
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ceBackSafeSetting = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ceLeaveSafeSetting = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.ceAlarmSound = new DevExpress.XtraEditors.CheckEdit();
            this.ceDoorWindowSound = new DevExpress.XtraEditors.CheckEdit();
            this.speAlarmDelay = new DevExpress.XtraEditors.SpinEdit();
            this.speAmp = new DevExpress.XtraEditors.SpinEdit();
            this.speHintVolume = new DevExpress.XtraEditors.SpinEdit();
            this.speSecurityDelay = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAmp.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceBackSafeSetting)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceLeaveSafeSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceAlarmSound.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceDoorWindowSound.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speAlarmDelay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speAmp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speHintVolume.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speSecurityDelay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl3
            // 
            this.groupControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupControl3.Controls.Add(this.lookUpEditAmp);
            this.groupControl3.Controls.Add(this.groupBox1);
            this.groupControl3.Controls.Add(this.groupBox2);
            this.groupControl3.Controls.Add(this.ceAlarmSound);
            this.groupControl3.Controls.Add(this.ceDoorWindowSound);
            this.groupControl3.Controls.Add(this.speAlarmDelay);
            this.groupControl3.Controls.Add(this.speAmp);
            this.groupControl3.Controls.Add(this.speHintVolume);
            this.groupControl3.Controls.Add(this.speSecurityDelay);
            this.groupControl3.Controls.Add(this.labelControl7);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.labelControl5);
            this.groupControl3.Controls.Add(this.labelControl6);
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(666, 270);
            this.groupControl3.TabIndex = 1;
            this.groupControl3.Text = "安防";
            // 
            // lookUpEditAmp
            // 
            this.lookUpEditAmp.EditValue = "";
            this.lookUpEditAmp.Location = new System.Drawing.Point(105, 145);
            this.lookUpEditAmp.Name = "lookUpEditAmp";
            this.lookUpEditAmp.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lookUpEditAmp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditAmp.Properties.NullText = "选择功放设备";
            this.lookUpEditAmp.Size = new System.Drawing.Size(196, 21);
            this.lookUpEditAmp.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ceBackSafeSetting);
            this.groupBox1.Location = new System.Drawing.Point(442, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 82);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "回家撤防";
            // 
            // ceBackSafeSetting
            // 
            this.ceBackSafeSetting.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ceBackSafeSetting.CheckOnClick = true;
            this.ceBackSafeSetting.ColumnWidth = 100;
            this.ceBackSafeSetting.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.ceBackSafeSetting.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("全部撤防")});
            this.ceBackSafeSetting.LeftCoord = 0;
            this.ceBackSafeSetting.Location = new System.Drawing.Point(6, 16);
            this.ceBackSafeSetting.MultiColumn = true;
            this.ceBackSafeSetting.Name = "ceBackSafeSetting";
            this.ceBackSafeSetting.Size = new System.Drawing.Size(374, 60);
            this.ceBackSafeSetting.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ceLeaveSafeSetting);
            this.groupBox2.Location = new System.Drawing.Point(35, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 82);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "离家安防";
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
            this.ceLeaveSafeSetting.Location = new System.Drawing.Point(6, 16);
            this.ceLeaveSafeSetting.MultiColumn = true;
            this.ceLeaveSafeSetting.Name = "ceLeaveSafeSetting";
            this.ceLeaveSafeSetting.Size = new System.Drawing.Size(374, 60);
            this.ceLeaveSafeSetting.TabIndex = 0;
            // 
            // ceAlarmSound
            // 
            this.ceAlarmSound.Location = new System.Drawing.Point(36, 120);
            this.ceAlarmSound.Name = "ceAlarmSound";
            this.ceAlarmSound.Properties.Caption = "预警提示音";
            this.ceAlarmSound.Size = new System.Drawing.Size(85, 19);
            this.ceAlarmSound.TabIndex = 3;
            // 
            // ceDoorWindowSound
            // 
            this.ceDoorWindowSound.Location = new System.Drawing.Point(158, 120);
            this.ceDoorWindowSound.Name = "ceDoorWindowSound";
            this.ceDoorWindowSound.Properties.Caption = "门窗提示音";
            this.ceDoorWindowSound.Size = new System.Drawing.Size(85, 19);
            this.ceDoorWindowSound.TabIndex = 3;
            // 
            // speAlarmDelay
            // 
            this.speAlarmDelay.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speAlarmDelay.Location = new System.Drawing.Point(502, 145);
            this.speAlarmDelay.Name = "speAlarmDelay";
            this.speAlarmDelay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speAlarmDelay.Properties.Mask.EditMask = "d";
            this.speAlarmDelay.Properties.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.speAlarmDelay.Size = new System.Drawing.Size(61, 21);
            this.speAlarmDelay.TabIndex = 2;
            // 
            // speAmp
            // 
            this.speAmp.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speAmp.Location = new System.Drawing.Point(361, 145);
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
            this.speAmp.Size = new System.Drawing.Size(60, 21);
            this.speAmp.TabIndex = 2;
            // 
            // speHintVolume
            // 
            this.speHintVolume.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speHintVolume.Location = new System.Drawing.Point(361, 118);
            this.speHintVolume.Name = "speHintVolume";
            this.speHintVolume.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speHintVolume.Properties.Mask.EditMask = "d";
            this.speHintVolume.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.speHintVolume.Properties.MaxValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.speHintVolume.Size = new System.Drawing.Size(60, 21);
            this.speHintVolume.TabIndex = 2;
            // 
            // speSecurityDelay
            // 
            this.speSecurityDelay.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speSecurityDelay.Location = new System.Drawing.Point(502, 118);
            this.speSecurityDelay.Name = "speSecurityDelay";
            this.speSecurityDelay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speSecurityDelay.Properties.Mask.EditMask = "d";
            this.speSecurityDelay.Properties.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.speSecurityDelay.Size = new System.Drawing.Size(61, 21);
            this.speSecurityDelay.TabIndex = 2;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(38, 148);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(60, 14);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "提示音功放";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(307, 148);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "功放地址";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(448, 148);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "预警延时";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(283, 122);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 14);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "门窗提示音量";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(448, 121);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "布防延时";
            // 
            // KeySecuritySetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl3);
            this.Name = "KeySecuritySetting";
            this.Size = new System.Drawing.Size(666, 270);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAmp.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceBackSafeSetting)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceLeaveSafeSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceAlarmSound.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceDoorWindowSound.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speAlarmDelay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speAmp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speHintVolume.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speSecurityDelay.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditAmp;
        private System.Windows.Forms.GroupBox groupBox1;
        protected DevExpress.XtraEditors.CheckedListBoxControl ceBackSafeSetting;
        private System.Windows.Forms.GroupBox groupBox2;
        protected DevExpress.XtraEditors.CheckedListBoxControl ceLeaveSafeSetting;
        private DevExpress.XtraEditors.CheckEdit ceAlarmSound;
        private DevExpress.XtraEditors.CheckEdit ceDoorWindowSound;
        private DevExpress.XtraEditors.SpinEdit speAlarmDelay;
        private DevExpress.XtraEditors.SpinEdit speAmp;
        private DevExpress.XtraEditors.SpinEdit speHintVolume;
        private DevExpress.XtraEditors.SpinEdit speSecurityDelay;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}
