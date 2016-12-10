using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{ 

    public class WeiXinMenu
    {

        /*======================================================================================================*/
        /*++++++++++++++++++++++++++++++++++ 菜单编号[排序方式]和[分解与合成] ++++++++++++++++++++++++++++++++++*/
        /*======================================================================================================*/

        //-----------------------------------
        public const int MS_MEMU_1_TOT = 8;                                           /* 第1级限制:最多可有8项       */
        public const int MS_MEMU_2_TOT = 5;                                         /* 第2级限制:最多可有5项       */
        public const int MS_MEMU_3_TOT = 10;                                           /* 第3级限制:最多可有10项      */
        public const int MS_MEMU_4_TOT = 10;                                           /* 第4级限制:最多可有10项      */

        public const int MEMU_1_TOT = 8;                                            /* 第1级菜单:总数              */
        public const int MEMU_2_TOT = 8 * 5;                                          /* 第2级菜单:总数              */
        public const int MEMU_3_TOT = 8 * 5 * 10;                                       /* 第3级菜单:总数              */
        public const int MEMU_4_TOT = 8 * 5 * 10 * 10;                                    /* 第4级菜单:总数              */

        //-----------------------------------
        public const int MEMU_4_1N_SZ = 5 * 10 * 10;                                    /* 第4级菜单:第1级大小         */
        public const int MEMU_4_2N_SZ = 10 * 10;                                    /* 第4级菜单:第2级大小         */
        public const int MEMU_4_3N_SZ = 10;                                    /* 第4级菜单:第3级大小         */

        public const int MEMU_3_1N_SZ = 5 * 10;                                       /* 第3级菜单:第1级大小         */
        public const int MEMU_3_2N_SZ = 10;                                       /* 第3级菜单:第2级大小         */

        public const int MEMU_2_1N_SZ = 5;                                          /* 第2级菜单:第1级大小         */

        public const string BEGIN_CODE = "0.0.0";
        public const string END_CODE = "2.4.8";

        private WeiXin weiXinDevice;

        public DataTable DataTableMenu = new DataTable("Menu");//菜单数据
        public MenuList menuList;//菜单控制
  
        public WeiXinMenu(WeiXin _device)
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
                DataTableMenu.Columns.Add(MenuConfig.DC_UUID, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_ID, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_KIND_ID, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_PARENT_ID, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_SETTING, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_TITLE, System.Type.GetType("System.String"));
                DataTableMenu.Columns.Add(MenuConfig.DC_CODE, System.Type.GetType("System.Int16")); 
                DataTableMenu.Columns.Add(MenuConfig.DC_SEQ, System.Type.GetType("System.Int16"));
                DataTableMenu.Columns.Add(MenuConfig.DC_FLAG, System.Type.GetType("System.UInt16"));
            }
            //---一级菜单---
            for (int i = 0; i < MenuConfig.Level1Menus.Length; i++)
            {
                DataTableMenu.Rows.Add(new object[] { Guid.NewGuid(), i, "", "", "", "", MenuConfig.Level1Menus[i], i, 0, 0 });
            }
            //---二级菜单--- 
            for (int i = 0; i < MenuConfig.Level21Menus.Length; i++)
            {
                int menuCode = this.getMenuID("0." + i);
                DataTableMenu.Rows.Add(new object[] { Guid.NewGuid(), "0." + i, MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "0", "配置", MenuConfig.Level21Menus[i], menuCode, Int16.Parse("0" + i), 0 });
                menuCode = this.getMenuID("1." + i);
                DataTableMenu.Rows.Add(new object[] { Guid.NewGuid(), "1." + i, MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "1", "配置", MenuConfig.Level22Menus[i], menuCode, Int16.Parse("1" + i), 0 });
                menuCode = this.getMenuID("2." + i);
                DataTableMenu.Rows.Add(new object[] { Guid.NewGuid(), "2." + i, MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "2", "配置", MenuConfig.Level23Menus[i], menuCode, Int16.Parse("2" + i), 0 });
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
            if (drs.Length != 0)
                return drs[0];
            else
                return null;
        }

        /// <summary>
        /// 根据ID找对应菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataRow FindNodeDataByUuid(String id)
        {
            DataRow[] drs = DataTableMenu.Select(string.Format(" {0} = '{1}' ", MenuConfig.DC_UUID, id));
            if (drs.Length != 0)
                return drs[0];
            else
                return null;
        }

        /// <summary>
        /// 根据ID找对应菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuData GetMenuDataByCode(String menuCode)
        {
            DataRow[] drs = DataTableMenu.Select(string.Format(" {0} = '{1}' ", MenuConfig.DC_CODE, menuCode));

            //int menuNum = this.getMenuID(menuCode);
            MenuData menuData = null; 
            if (drs != null)
            {
                menuData = new MenuData();
                DataRow dr = drs[0];
                dr.EndEdit();
                menuData.MenuID = Convert.ToUInt32( menuCode);
                menuData.KindID = Convert.ToUInt16(dr[MenuConfig.DC_KIND_ID]);
                menuData.KindName = dr[MenuConfig.DC_KIND_NAME].ToString(); 
                menuData.Flag = Convert.ToUInt16(dr[MenuConfig.DC_FLAG]); 
                menuData.Title = dr[MenuConfig.DC_TITLE].ToString();
            }

            return menuData;
        }

        /// <summary>
        /// 根据ID找对应菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuData GetMenuDataByUuid(String uuid)
        {
            DataRow[] drs = DataTableMenu.Select(string.Format(" {0} = '{1}' ", MenuConfig.DC_UUID, uuid));

            //int menuNum = this.getMenuID(menuCode);
            MenuData menuData = null;
            if (drs != null)
            {
                menuData = new MenuData();
                DataRow dr = drs[0];
                dr.EndEdit();
                menuData.MenuID = Convert.ToUInt32(dr[MenuConfig.DC_CODE]);
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
            int code = getMenuID(parentID + "." + num);
            return DataTableMenu.Rows.Add(new object[] {Guid.NewGuid(),//---唯一ID编号--
                parentID + "." + num, 
                MenuKind.MS_COBJ_CMD,  MenuKind.MS_COBJ_CMD_NAME, //---默认为指令----
                parentID,"配置", "",code,code,0});
        }

        /// <summary>
        /// 根据菜单,更新菜单树
        /// </summary>
        /// <param name="menuData"></param>
        public DataRow UpdateMenu(MenuData menuData)
        { 
            string newMenuCode = menuData.GetMenuCode();
            string newMenuParentCode = menuData.GetMenuParentCode();
            DataRow drMenu = FindNodeDataByID(newMenuCode);
            int newMenuSeq = Convert.ToInt16(newMenuCode.Replace(".", ""));
            if (drMenu == null)
            {
                DataRow dr = DataTableMenu.Rows.Add(new object[] {Guid.NewGuid(),newMenuCode, menuData.KindID, menuData.KindName,
                            newMenuParentCode, "配置", menuData.Title,(int)menuData.MenuID,(int)menuData.MenuID,menuData.Flag });
                dr.EndEdit();
                dr.AcceptChanges();
            }
            else
            {
                drMenu[MenuConfig.DC_ID] = newMenuCode;
                drMenu[MenuConfig.DC_KIND_ID] = menuData.KindID;
                drMenu[MenuConfig.DC_KIND_NAME] = menuData.KindName;
                drMenu[MenuConfig.DC_PARENT_ID] = newMenuParentCode;
                drMenu[MenuConfig.DC_SETTING] = "配置";
                drMenu[MenuConfig.DC_TITLE] = menuData.Title;
                drMenu[MenuConfig.DC_SEQ] = (int)menuData.MenuID;
                drMenu[MenuConfig.DC_CODE] = (int)menuData.MenuID;
                drMenu[MenuConfig.DC_FLAG] = menuData.Flag;
                drMenu.EndEdit();
                drMenu.AcceptChanges();
            }
            return drMenu;

        }

        public void UpdateMenuKind(string uuid, string kindName)
        {
            DataRow drSelect = FindNodeDataByUuid(uuid);
            if (drSelect != null)
            {
                drSelect[MenuConfig.DC_KIND_ID] = MenuKind.MenuKindNameID[kindName];
                drSelect[MenuConfig.DC_KIND_NAME] = kindName;
            }
            drSelect.EndEdit();
        }


        /// <summary>
        /// 删除菜单行
        /// </summary>
        public void DelMenu(string id)
        {
            int i = 0;
            //foreach (DataRow dr in DataTableMenu.Rows)
            //{
            //    if (dr[MenuConfig.DC_ID].ToString() == id)
            //    {
            //        DataTableMenu.Rows[i].Delete();
            //        break;
            //    }
            //    i++;
            //}

            foreach (DataRow dr in DataTableMenu.Rows)
            {
                if (dr.RowState != DataRowState.Deleted && dr[MenuConfig.DC_UUID].ToString() == id)
                {
                    DataTableMenu.Rows[i].Delete();
                    break;
                }
                i++;
            }
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
            MenuData menuData = GetMenuDataByCode(code);
            menuList.SaveMenuData(menuData); 
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        public void SaveMenu()
        {            
            //---删除菜单----
            DataTable dtDelete = DataTableMenu.GetChanges(DataRowState.Deleted);
            if (dtDelete != null)
            {
                dtDelete.RejectChanges();
                foreach (DataRow dr in dtDelete.Rows)
                {
                    int menuNum = Convert.ToInt16( dr[MenuConfig.DC_CODE]);
                    MenuData menuData = new MenuData();
                    menuData.MenuID = (UInt32)menuNum;
                    menuData.KindID = Convert.ToUInt16(dr[MenuConfig.DC_KIND_ID]);
                    menuData.KindName = dr[MenuConfig.DC_KIND_NAME].ToString();
                    menuData.Flag = Convert.ToUInt16(dr[MenuConfig.DC_FLAG]);
                    menuData.Title = dr[MenuConfig.DC_TITLE].ToString();

                    menuData.KindID = MenuKind.MS_COBJ_DLE;//---标记为删除类型---
                    menuList.SaveMenuData(menuData);
                }
            }
            //---保存菜单----
            DataTable dtAdd = DataTableMenu.GetChanges(DataRowState.Added);
            DataTable dtUpdate = DataTableMenu.GetChanges(DataRowState.Modified);
            if (dtAdd != null)
            {
                foreach (DataRow dr in dtAdd.Rows)
                {
                    MenuData menu = GetMenuDataByCode(dr[MenuConfig.DC_CODE].ToString());
                    menuList.SaveMenuData(menu);
                }
            }
            if (dtUpdate != null)
            {
                foreach (DataRow dr in dtUpdate.Rows)
                {
                    MenuData menu = GetMenuDataByCode(dr[MenuConfig.DC_CODE].ToString());
                    menuList.SaveMenuData(menu);
                }
            }
            DataTableMenu.AcceptChanges();

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
            int[] p = new int[arrayLevel.Length];
            for(int i=0;i<arrayLevel.Length;i++)
                p[i] = Convert.ToInt16(arrayLevel[i]); 
            menuID = WeiXinMenu.GetMemuNum(arrayLevel.Length-1, p);
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
                    this.UpdateMenu(menuData);
                }
            }
        }

        /*
       ==========================================================================================================
       ** 函数名称: GetMemuLevel()
       **
       ** 函数功能: 得出的各级菜单级号
       **
       ** 入口参数: uiNum             菜单编号 (同一级的数据会存放在一起)
       **           pj[]              得出的各级菜单编号 {0,1,3,5}表示 第1级\第2级\第4级\第6级 数据
       **
       ** 返 回 值: 小于0->出错，其它->所在第几级菜单(0->第一级) 
       **========================================================================================================
       */
        /// <summary>
        /// 得出的各级菜单级号
        /// </summary>
        /// <param name="uiNum">菜单编号 (同一级的数据会存放在一起)</param>
        /// <param name="pj">得出的各级菜单编号 {0,1,3,5}表示 第1级\第2级\第4级\第6级 数据</param>
        /// <returns>小于0->出错，其它->所在第几级菜单(0->第一级)</returns>
        public static int GetMemuLevel(int uiNum, int[] pj)
        {
            if (uiNum >= (MEMU_1_TOT + MEMU_2_TOT + MEMU_3_TOT + MEMU_4_TOT))
            {
                return (0);
            }
            else if (uiNum >= (MEMU_1_TOT + MEMU_2_TOT + MEMU_3_TOT))
            {
                uiNum -= (MEMU_1_TOT + MEMU_2_TOT + MEMU_3_TOT);
                pj[0] = uiNum / MEMU_4_1N_SZ;
                uiNum = uiNum % MEMU_4_1N_SZ;
                pj[1] = uiNum / MEMU_4_2N_SZ;
                uiNum = uiNum % MEMU_4_2N_SZ;
                pj[2] = uiNum / MEMU_4_3N_SZ;
                pj[3] = uiNum % MEMU_4_3N_SZ;

                return (3);
            }
            else if (uiNum >= (MEMU_1_TOT + MEMU_2_TOT))
            {
                uiNum -= (MEMU_1_TOT + MEMU_2_TOT);
                pj[0] = uiNum / MEMU_3_1N_SZ;
                uiNum = uiNum % MEMU_3_1N_SZ;
                pj[1] = uiNum / MEMU_3_2N_SZ;
                pj[2] = uiNum % MEMU_3_2N_SZ;

                return (2);
            }
            else if (uiNum >= MEMU_1_TOT)
            {
                uiNum -= (MEMU_1_TOT);
                pj[0] = uiNum / MEMU_2_1N_SZ;
                pj[1] = uiNum % MEMU_2_1N_SZ;

                return (1);
            }
            else
            {
                pj[0] = uiNum;

                return (0);
            }
        }

        /*
        ==========================================================================================================
        ** 函数名称: GetMemuNum()
        **
        ** 函数功能: 得出菜单编号
        **
        ** 入口参数: uiLev             所在第几级菜单 (2->所在第3级菜单)
        **           pj[]              各级菜单级号 {0,1,3,5}表示 第1级\第2级\第4级\第6级 数据
        **
        ** 返 回 值: 小于0->出错，其它->得出菜单号
        **========================================================================================================
        */
        /// <summary>
        /// 得出菜单编号
        /// </summary>
        /// <param name="uiLev">所在第几级菜单 (2->所在第3级菜单)</param>
        /// <param name="pj">各级菜单级号 {0,1,3,5}表示 第1级\第2级\第4级\第6级 数据</param>
        /// <returns>小于0->出错，其它->得出菜单号</returns>
        public static int GetMemuNum(int uiLev, int[] pj)
        {
            if (uiLev >= 4)
            {
                return (-1);
            }
            else if (uiLev >= 3)
            {
                if ((pj[0] >= MS_MEMU_1_TOT)
                || (pj[1] >= MS_MEMU_2_TOT)
                || (pj[2] >= MS_MEMU_3_TOT)
                || (pj[3] >= MS_MEMU_4_TOT))
                {
                    return (-1);
                }
                return ((int)((MEMU_1_TOT + MEMU_2_TOT + MEMU_3_TOT)
                           + ((uint)(pj[0]) * MEMU_4_1N_SZ)
                           + ((uint)(pj[1]) * MEMU_4_2N_SZ)
                           + ((uint)(pj[2]) * MEMU_4_3N_SZ)
                           + ((uint)(pj[3]))));
            }
            else if (uiLev >= 2)
            {
                if ((pj[0] >= MS_MEMU_1_TOT)
                || (pj[1] >= MS_MEMU_2_TOT)
                || (pj[2] >= MS_MEMU_3_TOT))
                {
                    return (-1);
                }
                return ((int)((MEMU_1_TOT + MEMU_2_TOT)
                           + ((uint)(pj[0]) * MEMU_3_1N_SZ)
                           + ((uint)(pj[1]) * MEMU_3_2N_SZ)
                           + ((uint)(pj[2]))));
            }
            else if (uiLev >= 1)
            {
                if ((pj[0] >= MS_MEMU_1_TOT)
                || (pj[1] >= MS_MEMU_2_TOT))
                {
                    return (-1);
                }
                return ((int)((MEMU_1_TOT)
                           + ((uint)(pj[0]) * MEMU_2_1N_SZ)
                           + ((uint)(pj[1]))));
            }
            else
            {
                if ((pj[0] >= MS_MEMU_1_TOT))
                {
                    return (-1);
                }
                return ((int)((uint)(pj[0])));
            }
        }

        /// <summary>
        /// 读取所有菜单
        /// </summary>
        public void ReadAllMenu()
        {
            DataTableMenu.AcceptChanges();
            ReadMenu(BEGIN_CODE, END_CODE);
        }
    }




}
