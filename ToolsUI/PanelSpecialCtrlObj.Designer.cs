﻿namespace ConfigDevice
{
    partial class PanelSpecialCtrlObj
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
            this.gcSpecialObjs = new DevExpress.XtraGrid.GridControl();
            this.gvSpecialObjs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dcControlObject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcDeviceValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.parameter5 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcSpecialObjs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSpecialObjs)).BeginInit();
            this.SuspendLayout();
            // 
            // gcSpecialObjs
            // 
            this.gcSpecialObjs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSpecialObjs.EmbeddedNavigator.Name = "";
            this.gcSpecialObjs.FormsUseDefaultLookAndFeel = false;
            this.gcSpecialObjs.Location = new System.Drawing.Point(0, 0);
            this.gcSpecialObjs.MainView = this.gvSpecialObjs;
            this.gcSpecialObjs.Name = "gcSpecialObjs";
            this.gcSpecialObjs.Size = new System.Drawing.Size(311, 234);
            this.gcSpecialObjs.TabIndex = 9;
            this.gcSpecialObjs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSpecialObjs});
            // 
            // gvSpecialObjs
            // 
            this.gvSpecialObjs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dcControlObject,
            this.dcDeviceValue,
            this.dcID,
            this.parameter2,
            this.parameter3,
            this.parameter4,
            this.parameter5});
            this.gvSpecialObjs.GridControl = this.gcSpecialObjs;
            this.gvSpecialObjs.Name = "gvSpecialObjs";
            this.gvSpecialObjs.OptionsCustomization.AllowColumnMoving = false;
            this.gvSpecialObjs.OptionsCustomization.AllowColumnResizing = false;
            this.gvSpecialObjs.OptionsCustomization.AllowFilter = false;
            this.gvSpecialObjs.OptionsCustomization.AllowGroup = false;
            this.gvSpecialObjs.OptionsCustomization.AllowSort = false;
            this.gvSpecialObjs.OptionsSelection.InvertSelection = true;
            this.gvSpecialObjs.OptionsView.ColumnAutoWidth = false;
            this.gvSpecialObjs.OptionsView.ShowGroupPanel = false;
            this.gvSpecialObjs.OptionsView.ShowIndicator = false;
            this.gvSpecialObjs.RowHeight = 18;
            // 
            // dcControlObject
            // 
            this.dcControlObject.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.dcControlObject.AppearanceCell.Options.UseForeColor = true;
            this.dcControlObject.AppearanceCell.Options.UseTextOptions = true;
            this.dcControlObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcControlObject.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcControlObject.AppearanceHeader.Options.UseTextOptions = true;
            this.dcControlObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcControlObject.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcControlObject.Caption = "控制对象";
            this.dcControlObject.Name = "dcControlObject";
            this.dcControlObject.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcControlObject.OptionsColumn.ReadOnly = true;
            this.dcControlObject.Visible = true;
            this.dcControlObject.VisibleIndex = 0;
            this.dcControlObject.Width = 100;
            // 
            // dcDeviceValue
            // 
            this.dcDeviceValue.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcDeviceValue.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcDeviceValue.AppearanceCell.Options.UseBackColor = true;
            this.dcDeviceValue.AppearanceCell.Options.UseForeColor = true;
            this.dcDeviceValue.AppearanceCell.Options.UseTextOptions = true;
            this.dcDeviceValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDeviceValue.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDeviceValue.AppearanceHeader.Options.UseTextOptions = true;
            this.dcDeviceValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcDeviceValue.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcDeviceValue.Caption = "设备名称";
            this.dcDeviceValue.Name = "dcDeviceValue";
            this.dcDeviceValue.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcDeviceValue.Visible = true;
            this.dcDeviceValue.VisibleIndex = 1;
            this.dcDeviceValue.Width = 140;
            // 
            // dcID
            // 
            this.dcID.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.dcID.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.dcID.AppearanceCell.Options.UseBackColor = true;
            this.dcID.AppearanceCell.Options.UseForeColor = true;
            this.dcID.AppearanceCell.Options.UseTextOptions = true;
            this.dcID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcID.AppearanceHeader.Options.UseTextOptions = true;
            this.dcID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dcID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dcID.Caption = "ID";
            this.dcID.Name = "dcID";
            this.dcID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.dcID.Visible = true;
            this.dcID.VisibleIndex = 2;
            this.dcID.Width = 50;
            // 
            // parameter2
            // 
            this.parameter2.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter2.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter2.AppearanceCell.Options.UseBackColor = true;
            this.parameter2.AppearanceCell.Options.UseForeColor = true;
            this.parameter2.AppearanceCell.Options.UseTextOptions = true;
            this.parameter2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter2.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter2.Caption = "参数2";
            this.parameter2.Name = "parameter2";
            this.parameter2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter2.Width = 80;
            // 
            // parameter3
            // 
            this.parameter3.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter3.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter3.AppearanceCell.Options.UseBackColor = true;
            this.parameter3.AppearanceCell.Options.UseForeColor = true;
            this.parameter3.AppearanceCell.Options.UseTextOptions = true;
            this.parameter3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter3.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter3.Caption = "参数3";
            this.parameter3.Name = "parameter3";
            this.parameter3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter3.Width = 80;
            // 
            // parameter4
            // 
            this.parameter4.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter4.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter4.AppearanceCell.Options.UseBackColor = true;
            this.parameter4.AppearanceCell.Options.UseForeColor = true;
            this.parameter4.AppearanceCell.Options.UseTextOptions = true;
            this.parameter4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter4.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter4.Caption = "参数4";
            this.parameter4.Name = "parameter4";
            this.parameter4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter4.Width = 80;
            // 
            // parameter5
            // 
            this.parameter5.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.parameter5.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.parameter5.AppearanceCell.Options.UseBackColor = true;
            this.parameter5.AppearanceCell.Options.UseForeColor = true;
            this.parameter5.AppearanceCell.Options.UseTextOptions = true;
            this.parameter5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter5.AppearanceHeader.Options.UseTextOptions = true;
            this.parameter5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.parameter5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.parameter5.Caption = "参数5";
            this.parameter5.Name = "parameter5";
            this.parameter5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.parameter5.Width = 80;
            // 
            // PanelSpecialCtrlObj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcSpecialObjs);
            this.Name = "PanelSpecialCtrlObj";
            this.Size = new System.Drawing.Size(311, 234);
            ((System.ComponentModel.ISupportInitialize)(this.gcSpecialObjs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSpecialObjs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcSpecialObjs;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSpecialObjs;
        private DevExpress.XtraGrid.Columns.GridColumn dcControlObject;
        private DevExpress.XtraGrid.Columns.GridColumn dcID;
        private DevExpress.XtraGrid.Columns.GridColumn dcDeviceValue;
        private DevExpress.XtraGrid.Columns.GridColumn parameter2;
        private DevExpress.XtraGrid.Columns.GridColumn parameter3;
        private DevExpress.XtraGrid.Columns.GridColumn parameter4;
        private DevExpress.XtraGrid.Columns.GridColumn parameter5;

    }
}