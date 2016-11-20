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
        public Network NetworkEdit = null;
        DataTable dtPosition = new DataTable("Position");
        DataTable dtMenu = new DataTable("Menu");
        private Dictionary<int, string> listNetworkKey = new Dictionary<int, string>();//保存对应关系

        public FrmWeiXin()
        {
            InitializeComponent();

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
            NetworkEdit.OnCallbackUI_Action += this.callbackUI;
            initCbxNetwork();
            initData();
            initMenus();
        }

        /// <summary>
        /// 初始化位置列表
        /// </summary>
        private void initData()
        {
            dtPosition.Clear(); dtPosition.AcceptChanges();
            foreach (Position position in NetworkEdit.ListPosition)
                dtPosition.Rows.Add(new object[] { position.Num, position.Name, position.HasPassword });

            dtPosition.AcceptChanges();
            gvPosition.BestFitColumns();
            gvPosition.RefreshData();

            edtNetworkName.Text = NetworkEdit.DeviceName;
            edtNetworkID.Text = NetworkEdit.NetworkID;
            edtNetworkIP.IP = NetworkEdit.NetworkIP;
            edtMask.IP = SysConfig.SubnetMask.ToString();//----目前固定 255.255.255.0----
            edtGateway.IP = edtNetworkIP.DefaultGateWay;//----目前固定xxx.xxx.xxx.1-----

            if (NetworkEdit.State == NetworkConfig.STATE_CONNECTED)
                NetworkEdit.SearchVer();
            else
            { edtSoftwareVer.Text = ""; edtHarewareVer.Text = ""; }
                
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
            cbxNetwork.Text = NetworkEdit.DeviceName;
        }

        /// <summary>
        /// 保存位置
        /// </summary>
        private void btSavePosition_Click(object sender, EventArgs e)
        {
            gvPosition.PostEditor();
            DataRow drEdit = gvPosition.GetDataRow(gvPosition.FocusedRowHandle);
            drEdit.EndEdit();
            DataTable dtModify = dtPosition.GetChanges(DataRowState.Modified);
            if (dtModify == null) return;
            foreach (DataRow dr in dtModify.Rows)
            {
                Position pos = new Position(Convert.ToInt16(dr[Position.DC_NUM]), dr[Position.DC_NAME].ToString(), Convert.ToBoolean(dr[Position.DC_HAS_PASSWORD]));
                NetworkEdit.SavePositionList(pos);
            }
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
            edtSoftwareVer.Text = NetworkEdit.SoftwareVer;
            edtHarewareVer.Text = NetworkEdit.HardwareVer;

            if (callbackParameter.Action == ActionKind.SaveNetworkPosition)
            {
                Position pos = callbackParameter.Parameters[0] as Position;
                dtPosition.Rows[pos.Num - 1].AcceptChanges();
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
            NetworkEdit.SaveNetworkName(edtNetworkName.Text);

            edtNetworkIP.IP = edtNetworkIP.IP;
            NetworkEdit.SaveNetworkParameter(edtNetworkIP.ByteIP, edtGateway.ByteIP, edtMask.ByteIP,
                ConvertTools.GetByteFrom8BitNumStr(edtNetworkID.Text));
        }

        /// <summary>
        /// 保存名称
        /// </summary>
        private void btSaveName_Click(object sender, EventArgs e)
        {
            NetworkEdit.SaveNetworkName(edtNetworkName.Text);
        }

        private void btSavePagameter_Click(object sender, EventArgs e)
        {
            NetworkEdit.SaveNetworkParameter(edtNetworkIP.ByteIP, edtGateway.ByteIP, edtMask.ByteIP,
                         ConvertTools.GetByteFrom8BitNumStr(edtNetworkID.Text));
        }


        private void cbxNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cbxNetwork.SelectedIndex;
            string key = listNetworkKey[i];
            NetworkEdit = SysConfig.ListNetworks[key];

            initData();
        }

        private void btFindOn_Click(object sender, EventArgs e)
        {
            NetworkEdit.OpenDiscover();
        }

        private void btFindOff_Click(object sender, EventArgs e)
        {
            NetworkEdit.CloseDiscover();
        }


        private void edtNetworkID_EditValueChanged(object sender, EventArgs e)
        {

        } 

        /// <summary>
        /// 初始化菜单
        /// </summary>
        private void initMenus()
        {
            treeMenu.KeyFieldName = MenuConfig.DC_ID;
            treeMenu.ParentFieldName = MenuConfig.DC_PARENT_ID;

            this.dtMenu.Columns.Add(MenuConfig.DC_ID, System.Type.GetType("System.String"));
            this.dtMenu.Columns.Add(MenuConfig.DC_KIND_ID, System.Type.GetType("System.String"));
            this.dtMenu.Columns.Add(MenuConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
            this.dtMenu.Columns.Add(MenuConfig.DC_PARENT_ID, System.Type.GetType("System.String"));
            this.dtMenu.Columns.Add(MenuConfig.DC_SETTING, System.Type.GetType("System.String"));
            this.dtMenu.Columns.Add(MenuConfig.DC_TITLE, System.Type.GetType("System.String"));
            this.dtMenu.Columns.Add(MenuConfig.DC_SEQ, System.Type.GetType("System.Int16"));
            //---一级菜单-- 
            for(int i=0;i< MenuConfig.Level1Menus.Length;i++)
            {
                dtMenu.Rows.Add(new object[] {"1"+(i+1),"","","","",MenuConfig.Level1Menus[i] ,i});
            }
            //---二级菜单-- 
            for (int i = 0; i < MenuConfig.Level21Menus.Length; i++)
            {
                dtMenu.Rows.Add(new object[] { "21" + (i + 1), MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "11", "配置", MenuConfig.Level21Menus[i],  Int16.Parse("21" + (i + 1))});
                dtMenu.Rows.Add(new object[] { "22" + (i + 1), MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "12", "配置", MenuConfig.Level22Menus[i],  Int16.Parse("22" + (i + 1))});
                dtMenu.Rows.Add(new object[] { "23" + (i + 1), MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "13", "配置", MenuConfig.Level23Menus[i],  Int16.Parse("23" + (i + 1))});
            }
    
            /*
             * 
             * 二级不能改变名称，类型也不能改变：名称[类型]，下发时需要发下类型
                1组：撤防[指令]、布防[指令]、安全门[入户门]、门窗安全[门窗安全]、更多[更多]
                2组：阅读模式[指令]、会客模式[指令]、休闲模式[指令]、全关[指令]、更多[更多]
                3组：降温遮阳[指令]、窗帘控制[指令]、空调控制[指令]、环境监测[环境]、更多[更多]
             * 
             * 
             */

            DataRow dr = findNodeDataByID("213");
            dr[MenuConfig.DC_KIND_ID] = MenuKind.MS_COBJ_DOOR;
            dr[MenuConfig.DC_KIND_NAME] = MenuKind.MS_COBJ_DOOR_NAME;
            dr = findNodeDataByID("214");
            dr[MenuConfig.DC_KIND_ID] = MenuKind.MS_COBJ_DWSAF;
            dr[MenuConfig.DC_KIND_NAME] = MenuKind.MS_COBJ_DWSAF_NAME;
            dr = findNodeDataByID("215");

 
            dr = findNodeDataByID("225");
            dr[MenuConfig.DC_KIND_ID] = MenuKind.MS_COBJ_MORE;
            dr[MenuConfig.DC_KIND_NAME] = MenuKind.MS_COBJ_MORE_NAME;

            dr = findNodeDataByID("234");
            dr[MenuConfig.DC_KIND_ID] = MenuKind.MS_COBJ_ENV;
            dr[MenuConfig.DC_KIND_NAME] = MenuKind.MS_COBJ_ENV_NAME;
            dr = findNodeDataByID("235");
            dr[MenuConfig.DC_KIND_ID] = MenuKind.MS_COBJ_MORE;
            dr[MenuConfig.DC_KIND_NAME] = MenuKind.MS_COBJ_MORE_NAME; 
              
            dr.EndEdit();
            dtMenu.AcceptChanges();

            treeMenu.DataSource = dtMenu;
            treeMenu.Nodes[0].Expanded = true;
            treeMenu.BestFitColumns(); 
        }

        /// <summary>
        /// 根据ID找对应菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private DataRow findNodeDataByID(String id)
        {
            DataRow[] drs = dtMenu.Select(string.Format(" {0} = '{1}' ", MenuConfig.DC_ID,id));
            if (drs != null)
                return drs[0];
            else
                return null;
        }

        /// <summary>
        /// 增加菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAdd_Click(object sender, EventArgs e)
        {
            TreeListNode selectNode = treeMenu.FocusedNode;
            if (selectNode == null || selectNode.Level != 1) return;
            DataRow drSelect = findNodeDataByID(selectNode.GetValue(MenuConfig.DC_ID).ToString()); 

            dtMenu.Rows.Add(new object[] {
                Guid.NewGuid().ToString(), //---唯一ID编号--
                MenuKind.MS_COBJ_CMD,  MenuKind.MS_COBJ_CMD_NAME, //---默认为指令----
                drSelect[MenuConfig.DC_ID].ToString(),"配置", "", selectNode.Nodes.Count+1});

            selectNode.ExpandAll();  
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDel_Click(object sender, EventArgs e)
        {
            TreeListNode selectNode = treeMenu.FocusedNode;
            if (selectNode == null || selectNode.Level != 2) return;
            DataRow drSelect = findNodeDataByID(selectNode.GetValue(MenuConfig.DC_ID).ToString());
            dtMenu.Rows.Remove(drSelect);
            selectNode.ExpandAll();  
        }


    }
}
