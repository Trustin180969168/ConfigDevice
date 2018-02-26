using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using DevExpress.XtraTreeList.Nodes;

namespace ConfigDevice
{
    public partial class FrmWeiXin : Form
    {
        public WeiXin weiXinEdit = null;
       
        DataTable dtPosition = new DataTable("Position");
        GridViewComboBox cbxMenuKind = new GridViewComboBox();//菜单类型选择编辑
        private Dictionary<int, string> listNetworkKey = new Dictionary<int, string>();//保存对应关系
        BaseMenuView menuEditView;
        public FrmWeiXin()
        {
            InitializeComponent();
            cbxMenuKind.SelectedIndexChanged += this.cbxMenuKindChanged;
   
            num.FieldName = Position.DC_NUM;
            password.FieldName = Position.DC_HAS_PASSWORD;
            name.FieldName = Position.DC_NAME;
            gcPosition.DataSource = dtPosition;
            dtPosition.Columns.Add(Position.DC_NUM, System.Type.GetType("System.Int16"));
            dtPosition.Columns.Add(Position.DC_NAME, System.Type.GetType("System.String"));
            dtPosition.Columns.Add(Position.DC_HAS_PASSWORD, System.Type.GetType("System.Boolean"));
             
        }

        private void FrmNetworkEdit_Load(object sender, EventArgs e)
        {
            weiXinEdit.OnCallbackUI_Action += this.callbackUI;
            initCbxNetwork();
            initPositionData();
            initMenus();

            loadData();
        }

        private void cbxMenuKindChanged(Object sender, EventArgs e)
        {
            TreeListNode selectNode = treeMenu.FocusedNode;
            if (selectNode == null || selectNode.Level != 2) return;
            string uuid = selectNode.GetValue(MenuConfig.DC_UUID).ToString();
            string kindName = (string)cbxMenuKind.Items[((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex];
          
            weiXinEdit.WeiXinMenu.UpdateMenuKind(uuid,kindName); 
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        private void loadData()
        {
            if (weiXinEdit.State == NetworkConfig.STATE_CONNECTED)
                weiXinEdit.SearchVer();
            else
            { 
                edtSoftwareVer.Text = ""; 
                edtHarewareVer.Text = ""; 
            }
            weiXinEdit.ReadAddress();
            weiXinEdit.WeiXinMenu.ReadAllMenu();
        }

        /// <summary>
        /// 初始化位置列表
        /// </summary>
        private void initPositionData()
        {
            dtPosition.Clear(); dtPosition.AcceptChanges();
            foreach (Position position in weiXinEdit.ListPosition)
                dtPosition.Rows.Add(new object[] { position.Num, position.Name, position.HasPassword });

            dtPosition.AcceptChanges();
            gvPosition.BestFitColumns();
            gvPosition.RefreshData();

            edtNetworkName.Text = weiXinEdit.DeviceName;
            edtNetworkID.Text = weiXinEdit.NetworkID;
            edtNetworkIP.IP = weiXinEdit.NetworkIP;
            edtMask.IP = SysConfig.SubnetMask.ToString();//---目前固定 255.255.255.0----
            edtGateway.IP = edtNetworkIP.DefaultGateWay;//----目前固定xxx.xxx.xxx.1-----

        }

        private void initCbxNetwork()
        {
            //-----网络列表------------------
            int i = 0;
            foreach (Network network in SysConfig.ListNetworks.Values)
            {
                cbxNetwork.Items.Add(network.DeviceName);
                listNetworkKey.Add(i++, network.NetworkIP);
            }
            cbxNetwork.Text = weiXinEdit.DeviceName;
        }

        /// <summary>
        /// 保存位置
        /// </summary>
        private void btSavePosition_Click(object sender, EventArgs e)
        {
            try
            {
                gvPosition.PostEditor();
                DataRow drEdit = gvPosition.GetDataRow(gvPosition.FocusedRowHandle);
                drEdit.EndEdit();
                DataTable dtModify = dtPosition.GetChanges(DataRowState.Modified);
                if (dtModify == null) return;
                foreach (DataRow dr in dtModify.Rows)
                {
                    Position pos = new Position(Convert.ToInt16(dr[Position.DC_NUM]), dr[Position.DC_NAME].ToString(), Convert.ToBoolean(dr[Position.DC_HAS_PASSWORD]));
                    weiXinEdit.SavePositionList(pos);
                }
            }
            catch { }
        }

        /// <summary>
        /// 回调结果
        /// </summary>
        /// <param name="pos">保存位置</param>
        private void callBackSavePosition(object[] values)
        {
 
        }

        private void callbackUI(CallbackParameter callbackParameter)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallbackUIAction(this.callbackUI), callbackParameter);
                return;
            }
            if (callbackParameter.Action == ActionKind.GetVer)
            {
                edtSoftwareVer.Text = weiXinEdit.SoftwareVer;
                edtHarewareVer.Text = weiXinEdit.HardwareVer;
            }
            if (callbackParameter.Action == ActionKind.SaveNetworkPosition)
            {
                Position pos = callbackParameter.Parameters[0] as Position;
                dtPosition.Rows[pos.Num - 1].AcceptChanges();
            }
            if (callbackParameter.Action == ActionKind.ReadServerAddress)
            {
                edtAddress.Text = weiXinEdit.Address;
            }
            if (callbackParameter.DeviceID == weiXinEdit.DeviceID && callbackParameter.Action == ActionKind.GetVer)
            {
                edtSoftwareVer.Text = weiXinEdit.SoftwareVer;
                edtHarewareVer.Text = weiXinEdit.HardwareVer;
            }

        }

        /// <summary>
        /// 校验是否错误
        /// </summary>
        private void edtName_Leave(object sender, EventArgs e)
        {
            DataRow dr = gvPosition.GetDataRow(gvPosition.FocusedRowHandle);
            if (dr == null) return;
            string name = dr[Position.DC_NAME].ToString();
            byte[] byteName = Encoding.GetEncoding("GB2312").GetBytes(name);
            if (byteName.Length > 12)
                CommonTools.MessageShow("\"" + name + "\"名称超出长度,最多6个字!", 2, "");
        }

        /// <summary>
        /// 校验网络名称
        /// </summary>
        private void edtNetworkName_Leave(object sender, EventArgs e)
        {
            string name = edtNetworkName.Text;
            byte[] byteName = Encoding.GetEncoding("GB2312").GetBytes(name);
            if (byteName.Length > 30)
                CommonTools.MessageShow("\"" + name + "\"名称超出长度,最多30个字!", 2, "");
        }

        /// <summary>
        /// 保存基本信息
        /// </summary>
        private void btSaveInfo_Click(object sender, EventArgs e)
        {
            weiXinEdit.SaveNetworkName(edtNetworkName.Text);

            edtNetworkIP.IP = edtNetworkIP.IP;
            weiXinEdit.SaveNetworkParameter(edtNetworkIP.ByteIP, edtGateway.ByteIP, edtMask.ByteIP,
                  ConvertTools.GetByteFrom8BitNumStr(edtNetworkID.Text), edtDNS1.ByteIP, edtDNS2.ByteIP);
        }

        /// <summary>
        /// 保存名称
        /// </summary>
        private void btSaveName_Click(object sender, EventArgs e)
        {
            weiXinEdit.SaveNetworkName(edtNetworkName.Text);
        }

        private void btSavePagameter_Click(object sender, EventArgs e)
        {
            weiXinEdit.SaveNetworkParameter(edtNetworkIP.ByteIP, edtGateway.ByteIP, edtMask.ByteIP,
                         ConvertTools.GetByteFrom8BitNumStr(edtNetworkID.Text),edtDNS1.ByteIP,edtDNS2.ByteIP);
        }


        private void cbxNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int i = cbxNetwork.SelectedIndex;
            //string key = listNetworkKey[i];
            //WeiXinEdit = ()SysConfig.ListNetworks[key];

            //initData();
        }

        private void btFindOn_Click(object sender, EventArgs e)
        {
            weiXinEdit.OpenDiscover();
        }

        private void btFindOff_Click(object sender, EventArgs e)
        {
            weiXinEdit.CloseDiscover();
        }


        private void edtNetworkID_EditValueChanged(object sender, EventArgs e)
        {


        } 

        /// <summary>
        /// 初始化菜单
        /// </summary>
        private void initMenus()
        {
            weiXinEdit.WeiXinMenu.InitMenu();
            //---编辑菜单类型---- 
            cbxMenuKind.Items.Add(MenuKind.MS_COBJ_ENV_NAME);//环境传感器
            cbxMenuKind.Items.Add(MenuKind.MS_COBJ_CMD_NAME);//指令控制
            cbxMenuKind.Items.Add(MenuKind.MS_COBJ_DWSAF_NAME);//门窗安全
            cbxMenuKind.Items.Add(MenuKind.MS_COBJ_DOOR_NAME);//入户门
            cbxMenuKind.Items.Add(MenuKind.MS_COBJ_MORE_NAME);//更多
            tlcKindName.ColumnEdit = cbxMenuKind;
            tlcSetting.ColumnEdit = linkEdit;
            tlcTitle.ColumnEdit = gedtName;

            treeMenu.KeyFieldName = MenuConfig.DC_ID;
            treeMenu.ParentFieldName = MenuConfig.DC_PARENT_ID;
            treeMenu.DataSource = weiXinEdit.WeiXinMenu.DataTableMenu;

            if (treeMenu.Nodes.Count > 0)
            {
                foreach(TreeListNode node in treeMenu.Nodes)
                    node.Expanded = true;
            }
            treeMenu.BestFitColumns();

        }

        /// <summary>
        /// 增加菜单
        /// </summary>
        private void btAdd_Click(object sender, EventArgs e)
        {
            TreeListNode selectNode = treeMenu.FocusedNode;
            TreeListNode addNode;
            if (selectNode.Level == 1)
                addNode = selectNode;
            else if (selectNode.Level == 2)
                addNode = selectNode.ParentNode;
            else
                return;
            if (addNode == null || addNode.Nodes.Count >= 9) return;
            weiXinEdit.WeiXinMenu.AddMenu(addNode.GetValue(MenuConfig.DC_ID).ToString(), addNode.Nodes.Count);

            addNode.ExpandAll();
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        private void btDel_Click(object sender, EventArgs e)
        {
            TreeListNode selectNode = treeMenu.FocusedNode;
            if (selectNode == null || selectNode.Level != 2 ) return;
            weiXinEdit.WeiXinMenu.DelMenu(selectNode.GetValue(MenuConfig.DC_UUID).ToString());

            selectNode.ExpandAll();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void btRef_Click(object sender, EventArgs e)
        {
            weiXinEdit.WeiXinMenu.ReadAllMenu();
        }


        /// <summary>
        /// 选择后控制编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            TreeListNode selectNode = treeMenu.FocusedNode;
            if (selectNode == null) return;
            if (selectNode.Level == 2)
            {
                tlcKindName.ColumnEdit.ReadOnly = false;
                tlcTitle.ColumnEdit.ReadOnly = false;   
            }
            else
            {
                tlcKindName.ColumnEdit.ReadOnly = true;
                tlcTitle.ColumnEdit.ReadOnly = true;       
            }
            int[] aa = new int[10];
            int num = WeiXinMenu.GetMemuNum(selectNode.Level, aa);
 

        }

        /// <summary>
        /// 根据2级菜单读取第三级菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TreeListNode selectNode = treeMenu.FocusedNode;
            if (selectNode == null || selectNode.Level != 1) return;
            string code = selectNode.GetValue(MenuConfig.DC_ID).ToString();
            weiXinEdit.WeiXinMenu.ReadMenu(code + ".0", code + ".8");//---三级菜单最多9个----

        }

        /// <summary>
        /// 保存当前菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            TreeListNode selectNode = treeMenu.FocusedNode;
            if (selectNode == null || selectNode.Level != 2) return;
            string code = selectNode.GetValue(MenuConfig.DC_ID).ToString(); 
            weiXinEdit.WeiXinMenu.SaveMenu(code); 
        }

        private void btSaveMenu_Click(object sender, EventArgs e)
        { 
            treeMenu.PostEditor();
            TreeListNode selectNode = treeMenu.FocusedNode;
            if (selectNode != null && selectNode.Level == 2)
            { 
                string uuid = selectNode.GetValue(MenuConfig.DC_UUID).ToString();
                DataRow dr = weiXinEdit.WeiXinMenu.FindNodeDataByUuid(uuid);
                dr.EndEdit();
            }
            if (menuEditView != null)
                menuEditView.SaveEdit();
            weiXinEdit.WeiXinMenu.SaveMenu(); 
        }

        /// <summary>
        /// 配置菜单功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkEdit_Click(object sender, EventArgs e)
        {
            //---当前菜单----
            TreeListNode selectNode = treeMenu.FocusedNode;
            lblMenu.Text = selectNode.GetDisplayText(tlcTitle); 
            if (selectNode != null && selectNode.Level != 0)
            { 
                string uuid = selectNode.GetValue(MenuConfig.DC_UUID).ToString();
                MenuData menuData = weiXinEdit.WeiXinMenu.GetMenuDataByUuid(uuid);
                menuEditView = MenuCtrl.CreateMenuSettingView(weiXinEdit, plMenuEdit, menuData);
                plMenuEdit.Tag = menuData;
                if (menuEditView == null)
                    plMenuEdit.Controls.Clear();
                else
                    menuEditView.LoadEditData();
            }
        }

    }
}
