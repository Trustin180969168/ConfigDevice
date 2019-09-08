namespace SocketTest
{
    partial class FrmSocketClientTest
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
            this.rtbReceive = new System.Windows.Forms.RichTextBox();
            this.btSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btClear = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DeviceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NetworkSegmentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.State = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MAC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbReceive
            // 
            this.rtbReceive.Location = new System.Drawing.Point(32, 330);
            this.rtbReceive.Name = "rtbReceive";
            this.rtbReceive.Size = new System.Drawing.Size(723, 217);
            this.rtbReceive.TabIndex = 0;
            this.rtbReceive.Text = "";
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(32, 12);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(93, 32);
            this.btSend.TabIndex = 1;
            this.btSend.Text = "搜索网络";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 306);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "UDP接收区";
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(32, 301);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 3;
            this.btClear.Text = "清空";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.FormsUseDefaultLookAndFeel = false;
            this.gridControl1.Location = new System.Drawing.Point(32, 50);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(723, 231);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.DeviceID,
            this.NetworkSegmentID,
            this.State,
            this.Name,
            this.MAC,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // DeviceID
            // 
            this.DeviceID.AppearanceCell.Options.UseTextOptions = true;
            this.DeviceID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DeviceID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.DeviceID.AppearanceHeader.Options.UseTextOptions = true;
            this.DeviceID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DeviceID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.DeviceID.Caption = "设备ID";
            this.DeviceID.FieldName = "DeviceID";
            this.DeviceID.Name = "DeviceID";
            this.DeviceID.OptionsColumn.ReadOnly = true;
            this.DeviceID.Visible = true;
            this.DeviceID.VisibleIndex = 0;
            // 
            // NetworkSegmentID
            // 
            this.NetworkSegmentID.AppearanceCell.Options.UseTextOptions = true;
            this.NetworkSegmentID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NetworkSegmentID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.NetworkSegmentID.AppearanceHeader.Options.UseTextOptions = true;
            this.NetworkSegmentID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NetworkSegmentID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.NetworkSegmentID.Caption = "网段ID";
            this.NetworkSegmentID.FieldName = "NetworkSegmentID";
            this.NetworkSegmentID.Name = "NetworkSegmentID";
            this.NetworkSegmentID.OptionsColumn.ReadOnly = true;
            this.NetworkSegmentID.Visible = true;
            this.NetworkSegmentID.VisibleIndex = 1;
            // 
            // State
            // 
            this.State.AppearanceCell.Options.UseTextOptions = true;
            this.State.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.State.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.State.AppearanceHeader.Options.UseTextOptions = true;
            this.State.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.State.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.State.Caption = "状态";
            this.State.FieldName = "State";
            this.State.Name = "State";
            this.State.OptionsColumn.ReadOnly = true;
            this.State.Visible = true;
            this.State.VisibleIndex = 2;
            // 
            // Name
            // 
            this.Name.AppearanceCell.Options.UseTextOptions = true;
            this.Name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Name.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Name.AppearanceHeader.Options.UseTextOptions = true;
            this.Name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Name.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Name.Caption = "设备名称";
            this.Name.FieldName = "Name";
            this.Name.Name = "Name";
            this.Name.OptionsColumn.ReadOnly = true;
            this.Name.Visible = true;
            this.Name.VisibleIndex = 3;
            // 
            // MAC
            // 
            this.MAC.AppearanceCell.Options.UseTextOptions = true;
            this.MAC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MAC.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.MAC.AppearanceHeader.Options.UseTextOptions = true;
            this.MAC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MAC.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.MAC.Caption = "设备机身码";
            this.MAC.FieldName = "MAC";
            this.MAC.Name = "MAC";
            this.MAC.OptionsColumn.ReadOnly = true;
            this.MAC.Visible = true;
            this.MAC.VisibleIndex = 4;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
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
            // FrmSocketClientTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.rtbReceive);
            this.Text = "SocketClientTest";
            this.Load += new System.EventHandler(this.FrmSocketClientTest_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSocketClientTest_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbReceive;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btClear;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn DeviceID;
        private DevExpress.XtraGrid.Columns.GridColumn NetworkSegmentID;
        private DevExpress.XtraGrid.Columns.GridColumn State;
        private DevExpress.XtraGrid.Columns.GridColumn Name;
        private DevExpress.XtraGrid.Columns.GridColumn MAC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}