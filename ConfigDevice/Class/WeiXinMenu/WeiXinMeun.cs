using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{ 

    public class WeiXinMeun
    {
        private WeiXin weiXinDevice;

        public DataTable DataTableMenu = new DataTable("Menu");//菜单数据
        public MenuList menuList;//菜单控制
  
        public WeiXinMeun(WeiXin _device)
        {

            weiXinDevice = _device;
       
        } 

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public void InitMenu()
        {
            DataTableMenu.Clear();
            menuList = new MenuList(this.weiXinDevice);//菜单控制 
            menuList.OnCallbackUI_Action += CallbackUI;
            if (DataTableMenu.Columns.Count == 0)
            {
                DataTableMenu.Columns.Add(MenuConfig.DC_ID, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_KIND_ID, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_PARENT_ID, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_SETTING, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_TITLE, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_SEQ, System.Type.GetType("System.Int16"));
                DataTableMenu.Columns.Add(MenuConfig.DC_FLAG, System.Type.GetType("System.UInt16"));
            }
            //---一级菜单---
            for (int i = 0; i < MenuConfig.Level1Menus.Length; i++)
            {
                DataTableMenu.Rows.Add(new object[] { i, "", "", "", "", MenuConfig.Level1Menus[i], i });
            }
            //---二级菜单--- 
            for (int i = 0; i < MenuConfig.Level21Menus.Length; i++)
            {
                DataTableMenu.Rows.Add(new object[] { "0." + i, MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "0", "配置", MenuConfig.Level21Menus[i], Int16.Parse("0" + (i + 1)) });
                DataTableMenu.Rows.Add(new object[] { "1." + i, MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "1", "配置", MenuConfig.Level22Menus[i], Int16.Parse("1" + (i + 1)) });
                DataTableMenu.Rows.Add(new object[] { "2." + i, MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "2", "配置", MenuConfig.Level23Menus[i], Int16.Parse("2" + (i + 1)) });
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

            DataRow dr = FindNodeDataByID("0.2");
            dr[MenuConfig.DC_KIND_ID] = MenuKind.MS_COBJ_DOOR;
            dr[MenuConfig.DC_KIND_NAME] = MenuKind.MS_COBJ_DOOR_NAME;
            dr = FindNodeDataByID("0.3");
            dr[MenuConfig.DC_KIND_ID] = MenuKind.MS_COBJ_DWSAF;
            dr[MenuConfig.DC_KIND_NAME] = MenuKind.MS_COBJ_DWSAF_NAME;
            dr = FindNodeDataByID("0.4");


            dr = FindNodeDataByID("1.4");
            dr[MenuConfig.DC_KIND_ID] = MenuKind.MS_COBJ_MORE;
            dr[MenuConfig.DC_KIND_NAME] = MenuKind.MS_COBJ_MORE_NAME;

            dr = FindNodeDataByID("2.3");
            dr[MenuConfig.DC_KIND_ID] = MenuKind.MS_COBJ_ENV;
            dr[MenuConfig.DC_KIND_NAME] = MenuKind.MS_COBJ_ENV_NAME;
            dr = FindNodeDataByID("2.4");
            dr[MenuConfig.DC_KIND_ID] = MenuKind.MS_COBJ_MORE;
            dr[MenuConfig.DC_KIND_NAME] = MenuKind.MS_COBJ_MORE_NAME;

            dr.EndEdit();
            DataTableMenu.AcceptChanges();


        }

        /// <summary>
        /// 根据ID找对应菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataRow FindNodeDataByID(String id)
        {
            DataRow[] drs = DataTableMenu.Select(string.Format(" {0} = '{1}' ", MenuConfig.DC_ID, id));
            if (drs != null)
                return drs[0];
            else
                return null;
        }

        /// <summary>
        /// 根据ID找对应菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuData GetMenuDataByID(String menuCode)
        {
            DataRow[] drs = DataTableMenu.Select(string.Format(" {0} = '{1}' ", MenuConfig.DC_ID, menuCode));

            int menuNum = this.getMenuID(menuCode);
            MenuData menuData = new MenuData(); 
            if (drs != null)
            {
                DataRow dr = drs[0];
                dr.EndEdit();
                menuData.MenuID = (UInt32)menuNum;
                menuData.KindID = Convert.ToUInt16(dr[MenuConfig.DC_KIND_ID]);
                menuData.KindName = dr[MenuConfig.DC_KIND_NAME].ToString();
                menuData.Flag = Convert.ToUInt16(dr[MenuConfig.DC_FLAG]);
                menuData.Title = dr[MenuConfig.DC_TITLE].ToString();
            }

            return menuData;
        }

        /// <summary>
        /// 增加菜单行
        /// </summary>
        public DataRow AddMenu(string parentID, int num)
        {
            DataRow drSelect = FindNodeDataByID(parentID);

            return DataTableMenu.Rows.Add(new object[] {
                parentID + "." + num, //---唯一ID编号--
                MenuKind.MS_COBJ_CMD,  MenuKind.MS_COBJ_CMD_NAME, //---默认为指令----
                parentID,"配置", "", drSelect[MenuConfig.DC_SEQ]+num.ToString(),0});
        }

        /// <summary>
        /// 删除菜单行
        /// </summary>
        public void DelMenu(string id)
        {
            DataRow drSelect = FindNodeDataByID(id);
            DataTableMenu.Rows.Remove(drSelect);
        }

        /// <summary>
        /// 读取菜单
        /// </summary>
        /// <param name="level"></param>
        /// <param name="count"></param>
        public void ReadMenu(string startCode, string endCode)
        {
            int startID = getMenuID(startCode);
            int endID = getMenuID(endCode);
            menuList.ReadMenuData(startID, endID);
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="level"></param>
        /// <param name="count"></param>
        public void SaveMenu(string code)
        { 
            MenuData menuData = GetMenuDataByID(code);
            menuList.SaveMenuData(menuData); 
        }

        /// <summary>
        /// 根据菜单编号获取菜单ID(设备)
        /// </summary>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        private int getMenuID(string menuCode)
        {
            int menuID;
            string[] arrayLevel = menuCode.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            //---根据菜单编号获取地址目录表
            int[] p = new int[3];
            p[0] = Convert.ToInt16(arrayLevel[0]);
            p[1] = Convert.ToInt16(arrayLevel[1]);
            p[2] = Convert.ToInt16(arrayLevel[2]);

            menuID = menuList.GetMemuNum(arrayLevel.Length-1, p);
            return menuID;
        }

        /// <summary>
        /// 回调
        /// </summary>
        public void CallbackUI(CallbackParameter callbackParameter)
        {

            lock (DataTableMenu)
            {
                if (callbackParameter.Action == ActionKind.ReadMenu)
                {
                    MenuData menuData = new MenuData((UserUdpData)callbackParameter.Parameters[0]);

                }
            }

        }
 


    }




}
