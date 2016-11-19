using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class MenuKind
    {
        /// <summary>
        /// 【删除菜单】表示删除此菜单，
        ///  删除是指对二级菜单、三级菜单，一级因没有指令，所以删除无效。
        /// </summary>
        public const int MS_COBJ_DLE = 0;  
       
        /// <summary>
        /// 【覆盖/增加菜单:环境传感器  】
        /// 此为传感器功能，最多可以配置4个传感器，接收到手机按此键，把传感器状态推送给手机。
        /// </summary>
        public const int MS_COBJ_ENV = 5;

        /// <summary>
        /// 覆盖/增加菜单:指令控制
        /// 表示此键可以控制设备，可以配置指令，最多可以配置50条。
        /// </summary>
        public const int MS_COBJ_CMD = 6;

        /// <summary>
        /// 覆盖/增加菜单:门窗安全
        /// 按下此键返回当前窗的状态,那个窗没锁好
        /// </summary>
        public const int MS_COBJ_DWSAF = 10;

        /// <summary>
        /// 覆盖/增加菜单:入户门
        /// 通过密码开启入户门，此菜单对应两个门，两个门分别对应1号地址和二号地址。
        /// </summary>
        public const int MS_COBJ_DOOR = 12;

        /// <summary>
        /// 覆盖/增加菜单:更多 
        /// 表示此键没功能，如果是二级菜单，直接推送三级菜单内容
        /// </summary>
        public const int MS_COBJ_MORE = 8;


        public static Dictionary<int, string> MenuKindIDName = new Dictionary<int, string>();
        public static Dictionary<string, int> MenuKindNameID = new Dictionary<string, int>();


        static MenuKind()
        {
            MenuKindIDName.Add(0, "删除菜单");
            MenuKindIDName.Add(5, "环境传感器");
            MenuKindIDName.Add(6, "指令控制");
            MenuKindIDName.Add(10, "门窗安全");
            MenuKindIDName.Add(12, "入户门");
            MenuKindIDName.Add(8, "更多");

            MenuKindNameID.Add("删除菜单", 0);
            MenuKindNameID.Add("环境传感器", 5);
            MenuKindNameID.Add("指令控制", 6);
            MenuKindNameID.Add("门窗安全", 10);
            MenuKindNameID.Add("入户门", 12);
            MenuKindNameID.Add("更多", 8); 

        }


    }



}
