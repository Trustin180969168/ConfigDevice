namespace ConfigDevice
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.gcDevices = new DevExpress.XtraGrid.GridControl();
            this.gvDevices = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceNetworkID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceKind = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceMac = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deviceRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.image1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btSearchDevices = new System.Windows.Forms.ToolStripButton();
            this.btClearDevice = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btQry = new System.Windows.Forms.ToolStripButton();
            this.btSnycNetworkID = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripLabel();
            this.cbxSelectNetwork = new System.Windows.Forms.ToolStripComboBox();
            this.imageCollectionDevices = new DevExpress.Utils.ImageCollection(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gcNetwork = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStripNetwork = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btmiConnectNetwork = new System.Windows.Forms.ToolStripMenuItem();
            this.btmiDisconnectNetwork = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiParameter = new System.Windows.Forms.ToolStripMenuItem();
            this.gvNetwork = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.networkDeviceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.networkDeviceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.networkID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.networkState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.networkDeviceMac = new DevExpress.XtraGrid.Columns.GridColumn();
            this.networkRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.networkKindName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btSaveNetwork = new System.Windows.Forms.ToolStripButton();
            this.btNetworkSearch = new System.Windows.Forms.ToolStripButton();
            this.btConnectnetwork = new System.Windows.Forms.ToolStripButton();
            this.btDisconnectNetwork = new System.Windows.Forms.ToolStripButton();
            this.btGJ = new System.Windows.Forms.ToolStripDropDownButton();
            this.btClearRJ45 = new System.Windows.Forms.ToolStripMenuItem();
            this.btSyncTime = new System.Windows.Forms.ToolStripMenuItem();
            this.btSyncData = new System.Windows.Forms.ToolStripMenuItem();
            this.btIPSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.btXtxx = new System.Windows.Forms.ToolStripSplitButton();
            this.btPCSend = new System.Windows.Forms.ToolStripMenuItem();
            this.btRJ45Send = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblIPSelect = new System.Windows.Forms.ToolStripLabel();
            this.cbxIPList = new System.Windows.Forms.ToolStripComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit)).BeginInit();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionDevices)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNetwork)).BeginInit();
            this.contextMenuStripNetwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvNetwork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gcDevices);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 164);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1016, 577);
            this.panel1.TabIndex = 6;
            // 
            // gcDevices
            // 
            this.gcDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDevices.EmbeddedNavigator.Name = "";
            this.gcDevices.FormsUseDefaultLookAndFeel = false;
            this.gcDevices.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gcDevices.Location = new System.Drawing.Point(0, 31);
            this.gcDevices.MainView = this.gvDevices;
            this.gcDevices.Name = "gcDevices";
            this.gcDevices.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.pictureEdit});
            this.gcDevices.Size = new System.Drawing.Size(1016, 546);
            this.gcDevices.TabIndex = 7;
            this.gcDevices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDevices});
            // 
            // gvDevices
            // 
            this.gvDevices.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.xh,
            this.deviceID,
            this.deviceNetworkID,
            this.deviceKind,
            this.deviceName,
            this.deviceMac,
            this.deviceState,
            this.deviceRemark,
            this.image1});
            this.gvDevices.GridControl = this.gcDevices;
            this.gvDevices.GroupFormat = "";
            this.gvDevices.Name = "gvDevices";
            this.gvDevices.OptionsView.EnableAppearanceEvenRow = true;
            this.gvDevices.OptionsView.EnableAppearanceOddRow = true;
            this.gvDevices.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvDevices.OptionsView.ShowGroupPanel = false;
            this.gvDevices.OptionsView.ShowIndicator = false;
            this.gvDevices.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.xh, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvDevices.DoubleClick += new System.EventHandler(this.gvDevices_DoubleClick);
            this.gvDevices.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvDevices_RowStyle);
            // 
            // xh
            // 
            this.xh.AppearanceCell.Options.UseTextOptions = true;
            this.xh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xh.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xh.AppearanceHeader.Options.UseTextOptions = true;
            this.xh.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xh.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xh.Caption = "序号";
            this.xh.FieldName = "xh";
            this.xh.Name = "xh";
            this.xh.OptionsColumn.AllowEdit = false;
            this.xh.OptionsColumn.ReadOnly = true;
            this.xh.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.xh.Visible = true;
            this.xh.VisibleIndex = 0;
            this.xh.Width = 77;
            // 
            // deviceID
            // 
            this.deviceID.AppearanceCell.Options.UseTextOptions = true;
            this.deviceID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceID.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceID.Caption = "设备ID";
            this.deviceID.Name = "deviceID";
            this.deviceID.Visible = true;
            this.deviceID.VisibleIndex = 2;
            this.deviceID.Width = 147;
            // 
            // deviceNetworkID
            // 
            this.deviceNetworkID.AppearanceCell.Options.UseTextOptions = true;
            this.deviceNetworkID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceNetworkID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceNetworkID.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceNetworkID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceNetworkID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceNetworkID.Caption = "网段ID";
            this.deviceNetworkID.Name = "deviceNetworkID";
            this.deviceNetworkID.OptionsColumn.AllowEdit = false;
            this.deviceNetworkID.OptionsColumn.ReadOnly = true;
            this.deviceNetworkID.Visible = true;
            this.deviceNetworkID.VisibleIndex = 3;
            this.deviceNetworkID.Width = 147;
            // 
            // deviceKind
            // 
            this.deviceKind.AppearanceCell.Options.UseTextOptions = true;
            this.deviceKind.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceKind.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceKind.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceKind.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceKind.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceKind.Caption = "设备类型";
            this.deviceKind.Name = "deviceKind";
            this.deviceKind.OptionsColumn.AllowEdit = false;
            this.deviceKind.OptionsColumn.ReadOnly = true;
            this.deviceKind.Visible = true;
            this.deviceKind.VisibleIndex = 4;
            this.deviceKind.Width = 147;
            // 
            // deviceName
            // 
            this.deviceName.AppearanceCell.Options.UseTextOptions = true;
            this.deviceName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceName.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceName.Caption = "设备名称";
            this.deviceName.Name = "deviceName";
            this.deviceName.Visible = true;
            this.deviceName.VisibleIndex = 1;
            this.deviceName.Width = 154;
            // 
            // deviceMac
            // 
            this.deviceMac.AppearanceCell.Options.UseTextOptions = true;
            this.deviceMac.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceMac.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceMac.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceMac.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceMac.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceMac.Caption = "设备机身码";
            this.deviceMac.Name = "deviceMac";
            this.deviceMac.OptionsColumn.AllowEdit = false;
            this.deviceMac.OptionsColumn.ReadOnly = true;
            // 
            // deviceState
            // 
            this.deviceState.AppearanceCell.Options.UseTextOptions = true;
            this.deviceState.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceState.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceState.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceState.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceState.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceState.Caption = "状态";
            this.deviceState.Name = "deviceState";
            this.deviceState.OptionsColumn.AllowEdit = false;
            this.deviceState.OptionsColumn.ReadOnly = true;
            this.deviceState.Visible = true;
            this.deviceState.VisibleIndex = 5;
            this.deviceState.Width = 154;
            // 
            // deviceRemark
            // 
            this.deviceRemark.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.deviceRemark.AppearanceCell.Options.UseForeColor = true;
            this.deviceRemark.AppearanceCell.Options.UseTextOptions = true;
            this.deviceRemark.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceRemark.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceRemark.AppearanceHeader.Options.UseTextOptions = true;
            this.deviceRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deviceRemark.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.deviceRemark.Caption = "提示";
            this.deviceRemark.Name = "deviceRemark";
            this.deviceRemark.OptionsColumn.AllowEdit = false;
            this.deviceRemark.OptionsColumn.ReadOnly = true;
            this.deviceRemark.Visible = true;
            this.deviceRemark.VisibleIndex = 6;
            this.deviceRemark.Width = 160;
            // 
            // image1
            // 
            this.image1.AppearanceCell.Options.UseTextOptions = true;
            this.image1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.image1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.image1.AppearanceHeader.Options.UseTextOptions = true;
            this.image1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.image1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.image1.Caption = "发现设备";
            this.image1.ColumnEdit = this.pictureEdit;
            this.image1.Name = "image1";
            this.image1.OptionsColumn.ReadOnly = true;
            this.image1.Visible = true;
            this.image1.VisibleIndex = 7;
            this.image1.Width = 86;
            // 
            // pictureEdit
            // 
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchVertical;
            this.pictureEdit.Click += new System.EventHandler(this.pictureEdit_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.Lavender;
            this.toolStrip2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSave,
            this.btSearchDevices,
            this.btClearDevice,
            this.toolStripSeparator1,
            this.btQry,
            this.btSnycNetworkID,
            this.toolStripSeparator2,
            this.toolStripButton3,
            this.cbxSelectNetwork});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1016, 31);
            this.toolStrip2.TabIndex = 6;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btSave
            // 
            this.btSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btSave.Image = global::ConfigDevice.Properties.Resources.save;
            this.btSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(73, 28);
            this.btSave.Text = "保存 ";
            // 
            // btSearchDevices
            // 
            this.btSearchDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btSearchDevices.Image = global::ConfigDevice.Properties.Resources.View;
            this.btSearchDevices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSearchDevices.Name = "btSearchDevices";
            this.btSearchDevices.Size = new System.Drawing.Size(109, 28);
            this.btSearchDevices.Text = "设备搜索  ";
            this.btSearchDevices.Click += new System.EventHandler(this.btSearchDevices_Click);
            // 
            // btClearDevice
            // 
            this.btClearDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btClearDevice.Image = global::ConfigDevice.Properties.Resources.Clear;
            this.btClearDevice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btClearDevice.Name = "btClearDevice";
            this.btClearDevice.Size = new System.Drawing.Size(105, 28);
            this.btClearDevice.Text = "清空设备 ";
            this.btClearDevice.Click += new System.EventHandler(this.btClearDevice_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(100, 0, 0, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // btQry
            // 
            this.btQry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btQry.Image = global::ConfigDevice.Properties.Resources.Traces1;
            this.btQry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btQry.Name = "btQry";
            this.btQry.Size = new System.Drawing.Size(121, 28);
            this.btQry.Text = "单网段显示 ";
            this.btQry.Click += new System.EventHandler(this.btQry_Click);
            // 
            // btSnycNetworkID
            // 
            this.btSnycNetworkID.BackColor = System.Drawing.Color.Transparent;
            this.btSnycNetworkID.Image = global::ConfigDevice.Properties.Resources.exchange;
            this.btSnycNetworkID.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSnycNetworkID.Name = "btSnycNetworkID";
            this.btSnycNetworkID.Size = new System.Drawing.Size(109, 28);
            this.btSnycNetworkID.Text = "同步网段ID ";
            this.btSnycNetworkID.Click += new System.EventHandler(this.btSyncID_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 36);
            this.toolStripSeparator2.Visible = false;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(41, 33);
            this.toolStripButton3.Text = "网络";
            this.toolStripButton3.Visible = false;
            // 
            // cbxSelectNetwork
            // 
            this.cbxSelectNetwork.BackColor = System.Drawing.SystemColors.Info;
            this.cbxSelectNetwork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectNetwork.DropDownWidth = 300;
            this.cbxSelectNetwork.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cbxSelectNetwork.ForeColor = System.Drawing.Color.DarkViolet;
            this.cbxSelectNetwork.MaxDropDownItems = 16;
            this.cbxSelectNetwork.Name = "cbxSelectNetwork";
            this.cbxSelectNetwork.Size = new System.Drawing.Size(300, 32);
            this.cbxSelectNetwork.Visible = false;
            this.cbxSelectNetwork.SelectedIndexChanged += new System.EventHandler(this.cbxSelectNetwork_SelectedIndexChanged);
            this.cbxSelectNetwork.Click += new System.EventHandler(this.cbxSelectNetwork_Click);
            // 
            // imageCollectionDevices
            // 
            this.imageCollectionDevices.ImageSize = new System.Drawing.Size(24, 24);
            this.imageCollectionDevices.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionDevices.ImageStream")));
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 160);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1016, 4);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gcNetwork);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1016, 160);
            this.panel2.TabIndex = 8;
            // 
            // gcNetwork
            // 
            this.gcNetwork.ContextMenuStrip = this.contextMenuStripNetwork;
            this.gcNetwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNetwork.EmbeddedNavigator.Name = "";
            this.gcNetwork.FormsUseDefaultLookAndFeel = false;
            this.gcNetwork.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gcNetwork.Location = new System.Drawing.Point(0, 31);
            this.gcNetwork.MainView = this.gvNetwork;
            this.gcNetwork.Name = "gcNetwork";
            this.gcNetwork.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.linkEdit});
            this.gcNetwork.Size = new System.Drawing.Size(1016, 129);
            this.gcNetwork.TabIndex = 6;
            this.gcNetwork.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNetwork});
            // 
            // contextMenuStripNetwork
            // 
            this.contextMenuStripNetwork.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btmiConnectNetwork,
            this.btmiDisconnectNetwork,
            this.tsmiChangePassword,
            this.tsmiParameter});
            this.contextMenuStripNetwork.Name = "contextMenuStripNetwork";
            this.contextMenuStripNetwork.Size = new System.Drawing.Size(119, 92);
            // 
            // btmiConnectNetwork
            // 
            this.btmiConnectNetwork.Image = global::ConfigDevice.Properties.Resources.connect1;
            this.btmiConnectNetwork.Name = "btmiConnectNetwork";
            this.btmiConnectNetwork.Size = new System.Drawing.Size(118, 22);
            this.btmiConnectNetwork.Text = "连接网络";
            this.btmiConnectNetwork.Click += new System.EventHandler(this.btConnectnetwork_Click);
            // 
            // btmiDisconnectNetwork
            // 
            this.btmiDisconnectNetwork.Image = global::ConfigDevice.Properties.Resources.disconnect1;
            this.btmiDisconnectNetwork.Name = "btmiDisconnectNetwork";
            this.btmiDisconnectNetwork.Size = new System.Drawing.Size(118, 22);
            this.btmiDisconnectNetwork.Text = "断开网络";
            this.btmiDisconnectNetwork.Click += new System.EventHandler(this.btDisconnectNetwork_Click);
            // 
            // tsmiChangePassword
            // 
            this.tsmiChangePassword.Image = global::ConfigDevice.Properties.Resources.client;
            this.tsmiChangePassword.Name = "tsmiChangePassword";
            this.tsmiChangePassword.Size = new System.Drawing.Size(118, 22);
            this.tsmiChangePassword.Text = "修改密码";
            this.tsmiChangePassword.Click += new System.EventHandler(this.tsmiChangePassword_Click);
            // 
            // tsmiParameter
            // 
            this.tsmiParameter.Image = global::ConfigDevice.Properties.Resources.goyi;
            this.tsmiParameter.Name = "tsmiParameter";
            this.tsmiParameter.Size = new System.Drawing.Size(118, 22);
            this.tsmiParameter.Text = "网络参数";
            this.tsmiParameter.Click += new System.EventHandler(this.gvNetwork_LinkEdit);
            // 
            // gvNetwork
            // 
            this.gvNetwork.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.networkDeviceName,
            this.networkDeviceID,
            this.networkID,
            this.networkState,
            this.networkDeviceMac,
            this.networkRemark,
            this.networkKindName,
            this.gridColumn3,
            this.gridColumn4});
            this.gvNetwork.GridControl = this.gcNetwork;
            this.gvNetwork.Name = "gvNetwork";
            this.gvNetwork.OptionsView.ShowGroupPanel = false;
            this.gvNetwork.OptionsView.ShowIndicator = false;
            this.gvNetwork.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvNetwork_FocusedRowChanged);
            this.gvNetwork.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvNetwork_RowStyle);
            // 
            // networkDeviceName
            // 
            this.networkDeviceName.AppearanceCell.Options.UseTextOptions = true;
            this.networkDeviceName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkDeviceName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkDeviceName.AppearanceHeader.Options.UseTextOptions = true;
            this.networkDeviceName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkDeviceName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkDeviceName.Caption = "设备名称";
            this.networkDeviceName.Name = "networkDeviceName";
            this.networkDeviceName.Visible = true;
            this.networkDeviceName.VisibleIndex = 0;
            // 
            // networkDeviceID
            // 
            this.networkDeviceID.AppearanceCell.Options.UseTextOptions = true;
            this.networkDeviceID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkDeviceID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkDeviceID.AppearanceHeader.Options.UseTextOptions = true;
            this.networkDeviceID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkDeviceID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkDeviceID.Caption = "设备ID";
            this.networkDeviceID.Name = "networkDeviceID";
            // 
            // networkID
            // 
            this.networkID.AppearanceCell.Options.UseTextOptions = true;
            this.networkID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkID.AppearanceHeader.Options.UseTextOptions = true;
            this.networkID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkID.Caption = "网段ID";
            this.networkID.Name = "networkID";
            this.networkID.Visible = true;
            this.networkID.VisibleIndex = 1;
            // 
            // networkState
            // 
            this.networkState.AppearanceCell.Options.UseTextOptions = true;
            this.networkState.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkState.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkState.AppearanceHeader.Options.UseTextOptions = true;
            this.networkState.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkState.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkState.Caption = "状态";
            this.networkState.ColumnEdit = this.linkEdit;
            this.networkState.Name = "networkState";
            this.networkState.OptionsColumn.ReadOnly = true;
            this.networkState.Visible = true;
            this.networkState.VisibleIndex = 2;
            // 
            // linkEdit
            // 
            this.linkEdit.AutoHeight = false;
            this.linkEdit.Name = "linkEdit";
            this.linkEdit.Click += new System.EventHandler(this.gvNetwork_LinkEdit);
            // 
            // networkDeviceMac
            // 
            this.networkDeviceMac.AppearanceCell.Options.UseTextOptions = true;
            this.networkDeviceMac.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkDeviceMac.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkDeviceMac.AppearanceHeader.Options.UseTextOptions = true;
            this.networkDeviceMac.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkDeviceMac.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkDeviceMac.Caption = "设备机身码";
            this.networkDeviceMac.Name = "networkDeviceMac";
            this.networkDeviceMac.OptionsColumn.ReadOnly = true;
            // 
            // networkRemark
            // 
            this.networkRemark.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.networkRemark.AppearanceCell.Options.UseForeColor = true;
            this.networkRemark.AppearanceCell.Options.UseTextOptions = true;
            this.networkRemark.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkRemark.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkRemark.AppearanceHeader.Options.UseTextOptions = true;
            this.networkRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkRemark.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkRemark.Caption = "提示";
            this.networkRemark.Name = "networkRemark";
            this.networkRemark.OptionsColumn.ReadOnly = true;
            this.networkRemark.Visible = true;
            this.networkRemark.VisibleIndex = 4;
            // 
            // networkKindName
            // 
            this.networkKindName.AppearanceCell.Options.UseTextOptions = true;
            this.networkKindName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkKindName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkKindName.AppearanceHeader.Options.UseTextOptions = true;
            this.networkKindName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.networkKindName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.networkKindName.Caption = "设备类型";
            this.networkKindName.Name = "networkKindName";
            this.networkKindName.OptionsColumn.ReadOnly = true;
            this.networkKindName.Visible = true;
            this.networkKindName.VisibleIndex = 3;
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
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.OldLace;
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSaveNetwork,
            this.btNetworkSearch,
            this.btConnectnetwork,
            this.btDisconnectNetwork,
            this.btGJ,
            this.btXtxx,
            this.toolStripButton1,
            this.lblIPSelect,
            this.cbxIPList});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1016, 31);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btSaveNetwork
            // 
            this.btSaveNetwork.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btSaveNetwork.Image = global::ConfigDevice.Properties.Resources.save;
            this.btSaveNetwork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveNetwork.Name = "btSaveNetwork";
            this.btSaveNetwork.Size = new System.Drawing.Size(73, 28);
            this.btSaveNetwork.Text = "保存 ";
            // 
            // btNetworkSearch
            // 
            this.btNetworkSearch.Image = global::ConfigDevice.Properties.Resources.View;
            this.btNetworkSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNetworkSearch.Name = "btNetworkSearch";
            this.btNetworkSearch.Size = new System.Drawing.Size(96, 28);
            this.btNetworkSearch.Text = "网络搜索 ";
            this.btNetworkSearch.Click += new System.EventHandler(this.btNetworkSearch_Click);
            // 
            // btConnectnetwork
            // 
            this.btConnectnetwork.Image = global::ConfigDevice.Properties.Resources.connect1;
            this.btConnectnetwork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btConnectnetwork.Name = "btConnectnetwork";
            this.btConnectnetwork.Size = new System.Drawing.Size(96, 28);
            this.btConnectnetwork.Text = "连接网络 ";
            this.btConnectnetwork.Click += new System.EventHandler(this.btConnectnetwork_Click);
            // 
            // btDisconnectNetwork
            // 
            this.btDisconnectNetwork.Image = global::ConfigDevice.Properties.Resources.disconnect1;
            this.btDisconnectNetwork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDisconnectNetwork.Name = "btDisconnectNetwork";
            this.btDisconnectNetwork.Size = new System.Drawing.Size(96, 28);
            this.btDisconnectNetwork.Text = "断开网络 ";
            this.btDisconnectNetwork.Click += new System.EventHandler(this.btDisconnectNetwork_Click);
            // 
            // btGJ
            // 
            this.btGJ.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btClearRJ45,
            this.btSyncTime,
            this.btSyncData,
            this.btIPSelect});
            this.btGJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGJ.Image = global::ConfigDevice.Properties.Resources.goyi;
            this.btGJ.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btGJ.Name = "btGJ";
            this.btGJ.Size = new System.Drawing.Size(82, 28);
            this.btGJ.Text = "高级 ";
            this.btGJ.MouseHover += new System.EventHandler(this.btGJ_MouseHover);
            // 
            // btClearRJ45
            // 
            this.btClearRJ45.Image = global::ConfigDevice.Properties.Resources.Clear;
            this.btClearRJ45.Name = "btClearRJ45";
            this.btClearRJ45.Size = new System.Drawing.Size(157, 24);
            this.btClearRJ45.Text = "清空RJ45";
            this.btClearRJ45.Click += new System.EventHandler(this.btClearNetwork_Click);
            // 
            // btSyncTime
            // 
            this.btSyncTime.Image = global::ConfigDevice.Properties.Resources.checkWork;
            this.btSyncTime.Name = "btSyncTime";
            this.btSyncTime.Size = new System.Drawing.Size(157, 24);
            this.btSyncTime.Text = "同步时间";
            this.btSyncTime.Click += new System.EventHandler(this.btSyncTime_Click);
            // 
            // btSyncData
            // 
            this.btSyncData.Image = global::ConfigDevice.Properties.Resources.snyc;
            this.btSyncData.Name = "btSyncData";
            this.btSyncData.Size = new System.Drawing.Size(157, 24);
            this.btSyncData.Text = "同步数据";
            this.btSyncData.Click += new System.EventHandler(this.btSyncData_Click);
            // 
            // btIPSelect
            // 
            this.btIPSelect.Image = global::ConfigDevice.Properties.Resources.goyi;
            this.btIPSelect.Name = "btIPSelect";
            this.btIPSelect.Size = new System.Drawing.Size(157, 24);
            this.btIPSelect.Text = "显示IP选择";
            this.btIPSelect.Click += new System.EventHandler(this.btIPSelect_Click);
            // 
            // btXtxx
            // 
            this.btXtxx.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btXtxx.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btPCSend,
            this.btRJ45Send});
            this.btXtxx.Image = global::ConfigDevice.Properties.Resources.fact;
            this.btXtxx.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btXtxx.Name = "btXtxx";
            this.btXtxx.Size = new System.Drawing.Size(104, 28);
            this.btXtxx.Text = "系统信息";
            this.btXtxx.MouseHover += new System.EventHandler(this.btXtxx_MouseHover);
            // 
            // btPCSend
            // 
            this.btPCSend.Image = global::ConfigDevice.Properties.Resources.work;
            this.btPCSend.Name = "btPCSend";
            this.btPCSend.Size = new System.Drawing.Size(159, 30);
            this.btPCSend.Text = "PC发送包";
            this.btPCSend.Click += new System.EventHandler(this.btPCSend_Click);
            // 
            // btRJ45Send
            // 
            this.btRJ45Send.Image = global::ConfigDevice.Properties.Resources.work;
            this.btRJ45Send.Name = "btRJ45Send";
            this.btRJ45Send.Size = new System.Drawing.Size(159, 30);
            this.btRJ45Send.Text = "RJ45发送包";
            this.btRJ45Send.Click += new System.EventHandler(this.btRJ45Send_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Margin = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 32);
            this.toolStripButton1.Visible = false;
            // 
            // lblIPSelect
            // 
            this.lblIPSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblIPSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIPSelect.Image = ((System.Drawing.Image)(resources.GetObject("lblIPSelect.Image")));
            this.lblIPSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lblIPSelect.Name = "lblIPSelect";
            this.lblIPSelect.Size = new System.Drawing.Size(60, 29);
            this.lblIPSelect.Text = "IP地址:";
            this.lblIPSelect.Visible = false;
            // 
            // cbxIPList
            // 
            this.cbxIPList.BackColor = System.Drawing.SystemColors.Info;
            this.cbxIPList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxIPList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cbxIPList.Name = "cbxIPList";
            this.cbxIPList.Size = new System.Drawing.Size(200, 32);
            this.cbxIPList.Visible = false;
            this.cbxIPList.SelectedIndexChanged += new System.EventHandler(this.cbxIPList_SelectedIndexChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备配置管理系统";
            this.Load += new System.EventHandler(this.FrmSocketClientTest_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSocketClientTest_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionDevices)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNetwork)).EndInit();
            this.contextMenuStripNetwork.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvNetwork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkEdit)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btSearchDevices;
        private DevExpress.XtraGrid.GridControl gcDevices;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDevices;
        private DevExpress.XtraGrid.Columns.GridColumn xh;
        private DevExpress.XtraGrid.Columns.GridColumn deviceID;
        private DevExpress.XtraGrid.Columns.GridColumn deviceNetworkID;
        private DevExpress.XtraGrid.Columns.GridColumn deviceKind;
        private DevExpress.XtraGrid.Columns.GridColumn deviceName;
        private DevExpress.XtraGrid.Columns.GridColumn deviceMac;
        private DevExpress.XtraGrid.Columns.GridColumn deviceState;
        private DevExpress.XtraGrid.Columns.GridColumn deviceRemark;
        private DevExpress.XtraGrid.Columns.GridColumn image1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl gcNetwork;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNetwork;
        private DevExpress.XtraGrid.Columns.GridColumn networkDeviceID;
        private DevExpress.XtraGrid.Columns.GridColumn networkID;
        private DevExpress.XtraGrid.Columns.GridColumn networkState;
        private DevExpress.XtraGrid.Columns.GridColumn networkDeviceName;
        private DevExpress.XtraGrid.Columns.GridColumn networkDeviceMac;
        private DevExpress.XtraGrid.Columns.GridColumn networkRemark;
        private DevExpress.XtraGrid.Columns.GridColumn networkKindName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btNetworkSearch;
        private System.Windows.Forms.ToolStripButton btConnectnetwork;
        private System.Windows.Forms.ToolStripButton btDisconnectNetwork;
        private System.Windows.Forms.ToolStripSplitButton btXtxx;
        private System.Windows.Forms.ToolStripMenuItem btRJ45Send;
        private System.Windows.Forms.ToolStripMenuItem btPCSend;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNetwork;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangePassword;
        private System.Windows.Forms.ToolStripMenuItem tsmiParameter;
        private System.Windows.Forms.ToolStripComboBox cbxIPList;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripLabel lblIPSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cbxSelectNetwork;
        private System.Windows.Forms.ToolStripButton btClearDevice;
        private System.Windows.Forms.ToolStripDropDownButton btGJ;
        private System.Windows.Forms.ToolStripMenuItem btClearRJ45;
        private System.Windows.Forms.ToolStripMenuItem btSyncTime;
        private System.Windows.Forms.ToolStripMenuItem btSyncData;
        private System.Windows.Forms.ToolStripLabel toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem btIPSelect;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripButton btQry;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btSaveNetwork;
        private System.Windows.Forms.ToolStripButton btSnycNetworkID;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkEdit;
        private System.Windows.Forms.ToolStripMenuItem btmiConnectNetwork;
        private System.Windows.Forms.ToolStripMenuItem btmiDisconnectNetwork;
        private DevExpress.Utils.ImageCollection imageCollectionDevices;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit pictureEdit;
    }
}