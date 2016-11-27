using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class MenuConfig
    {

        public static string[] Level1Menus = new string[] { "家居安全", "灯光场景", "设备控制" };
        public static string[] Level21Menus = new string[] { "撤防", "布防", "安全门", "门窗安全", "更多" };
        public static string[] Level22Menus = new string[] { "阅读模式", "会客模式", "休闲模式", "全关", "更多" };
        public static string[] Level23Menus = new string[] { "降温遮阳", "窗帘控制", "空调控制", "环境监测", "更多" };


        public const string DC_ID = "ID";//ID 
        public const string DC_KIND_ID = "KindID";//类型ID
        public const string DC_KIND_NAME = "KindName";//类型名称
        public const string DC_TITLE = "Title";//名称
        public const string DC_PARENT_ID= "ParentID";//父ID
        public const string DC_SETTING = "Setting";//配置
        public const string DC_SEQ = "Seq";//顺序
        public const string DC_FLAG = "Flag";//标志位
    
    }



    public class MenuKind
    {
        /// <summary>
        /// 【删除菜单】表示删除此菜单，
        ///  删除是指对二级菜单、三级菜单，一级因没有指令，所以删除无效。
        /// </summary>
        public const int MS_COBJ_DLE = 0;  
        public const string MS_COBJ_DLE_NAME= "删除菜单";

        /// <summary>
        /// 【覆盖/增加菜单:环境传感器  】
        /// 此为传感器功能，最多可以配置4个传感器，接收到手机按此键，把传感器状态推送给手机。
        /// </summary>
        public const int MS_COBJ_ENV = 5;
        public const string MS_COBJ_ENV_NAME = "环境传感器";

        /// <summary>
        /// 覆盖/增加菜单:指令控制
        /// 表示此键可以控制设备，可以配置指令，最多可以配置50条。
        /// </summary>
        public const int MS_COBJ_CMD = 6;
        public const string MS_COBJ_CMD_NAME = "指令控制";

        /// <summary>
        /// 覆盖/增加菜单:门窗安全
        /// 按下此键返回当前窗的状态,那个窗没锁好
        /// </summary>
        public const int MS_COBJ_DWSAF = 10;
        public const string MS_COBJ_DWSAF_NAME = "门窗安全";

        /// <summary>
        /// 覆盖/增加菜单:入户门
        /// 通过密码开启入户门，此菜单对应两个门，两个门分别对应1号地址和二号地址。
        /// </summary>
        public const int MS_COBJ_DOOR = 12;
        public const string MS_COBJ_DOOR_NAME = "入户门";

        /// <summary>
        /// 覆盖/增加菜单:更多 
        /// 表示此键没功能，如果是二级菜单，直接推送三级菜单内容
        /// </summary>
        public const int MS_COBJ_MORE = 8;
        public const string MS_COBJ_MORE_NAME = "更多"; 

        public static Dictionary<int, string> MenuKindIDName = new Dictionary<int, string>();
        public static Dictionary<string, int> MenuKindNameID = new Dictionary<string, int>(); 

        static MenuKind()
        {
            MenuKindIDName.Add(MS_COBJ_DLE, MS_COBJ_DLE_NAME);
            MenuKindIDName.Add(MS_COBJ_ENV, MS_COBJ_ENV_NAME);
            MenuKindIDName.Add(MS_COBJ_CMD, MS_COBJ_CMD_NAME);
            MenuKindIDName.Add(MS_COBJ_DWSAF, MS_COBJ_DWSAF_NAME);
            MenuKindIDName.Add(MS_COBJ_DOOR, MS_COBJ_DOOR_NAME);
            MenuKindIDName.Add(MS_COBJ_MORE, MS_COBJ_MORE_NAME);

            MenuKindNameID.Add(MS_COBJ_DLE_NAME, MS_COBJ_DLE);
            MenuKindNameID.Add(MS_COBJ_ENV_NAME, MS_COBJ_ENV);
            MenuKindNameID.Add(MS_COBJ_CMD_NAME, MS_COBJ_CMD);
            MenuKindNameID.Add(MS_COBJ_DWSAF_NAME, MS_COBJ_DWSAF);
            MenuKindNameID.Add(MS_COBJ_DOOR_NAME, MS_COBJ_DOOR);
            MenuKindNameID.Add(MS_COBJ_MORE_NAME, MS_COBJ_MORE); 

        }


    }



}
