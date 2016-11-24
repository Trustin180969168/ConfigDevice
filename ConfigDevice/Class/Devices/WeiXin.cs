using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data;

namespace ConfigDevice
{

    public class WeiXin : Network
    {
        private CallbackFromUDP callbackGetAddress;
        public DataTable DataTableMenu = new DataTable("Menu");//菜单数据
        public MenuList menuList;//菜单控制
        public string Address = "";//设备地址 
        public WeiXin(UserUdpData userData)
            : base(userData)
        {
            callbackGetAddress = new CallbackFromUDP(getCallbackAddressData);
        } 

        /// <summary>
        /// 保存名称
        /// </summary>
        /// <param name="newName">新名称</param>
        public void SaveAddress(string address)
        {
            byte[] byteAddress = Encoding.GetEncoding("GB2312").GetBytes(address);
            if (byteAddress.Length > 60)
            {
                CommonTools.MessageShow("地址超出60字节!", 2, "");
                return;
            }
            UdpData saveNameUdp = createSaveAddressUdp(address);
            MySocket.GetInstance().SendData(saveNameUdp, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackSaveAddress),
                new object[] { address  });
        }
        private void callbackSaveAddress(UdpData udpReply, object[] values)
        {
            string newAddress = (string)values[0]; 
            if (udpReply.ReplyByte == REPLY_RESULT.CMD_TRUE)
            {
                this.Address = newAddress;    
            }
            else
                CommonTools.ShowReplyInfo("保存名称失败!", udpReply.ReplyByte);
        }
        /// <summary>
        /// 创建修改设备ID的UDP包
        /// </summary>
        /// <returns>返回UDP包</returns>
        private UdpData createSaveAddressUdp(string address)
        {
  
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkID, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_WRITE_ADDRESS;//----用户命令-----  
            byte[] byteAddress = Encoding.GetEncoding("GB2312").GetBytes(address);
            byte len = (byte)(byteAddress.Length + 4);
            byte[] crcData = new byte[10+byteAddress.Length];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = len;
            Buffer.BlockCopy(byteAddress, 0, crcData, 10, byteAddress.Length);
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
 
        }

        /// <summary>
        /// 读取地址
        /// </summary>
        public void ReadAddress()
        {
            SysCtrl.AddRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_ADDRESS, this.DeviceID, callbackGetAddress);//回调刷新结果
            UdpData udpSend = createReadAddressUdp();
            MySocket.GetInstance().SendData(udpSend, NetworkIP, SysConfig.RemotePort, new CallbackUdpAction(callbackRequestReadAddress), null);
        }
        private void callbackRequestReadAddress(UdpData udpReply, object[] values)
        {
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo("申请读取地址失败!", udpReply.ReplyByte);
        }
        private UdpData createReadAddressUdp()
        {
            UdpData udp = new UdpData();

            udp.PacketKind[0] = PackegeSendReply.SEND;//----包数据类(回复包为02,发送包为01)----
            udp.PacketProperty[0] = BroadcastKind.Unicast;//----包属性(单播)----
            Buffer.BlockCopy(SysConfig.ByteLocalPort, 0, udp.SendPort, 0, 2);//-----发送端口----
            Buffer.BlockCopy(UserProtocol.RJ45, 0, udp.Protocol, 0, 4);//------用户协议----

            byte[] target = new byte[] { ByteDeviceID, ByteNetworkID, ByteKindID };//----目标信息--
            byte[] source = new byte[] { BytePCAddress, ByteNetworkID, DeviceConfig.EQUIPMENT_PC };//----源信息----
            byte page = UdpDataConfig.DEFAULT_PAGE;         //-----分页-----
            byte[] cmd = DeviceConfig.CMD_PUBLIC_READ_ADDRESS;//----用户命令-----

            byte[] crcData = new byte[10];
            Buffer.BlockCopy(target, 0, crcData, 0, 3);
            Buffer.BlockCopy(source, 0, crcData, 3, 3);
            crcData[6] = page;
            Buffer.BlockCopy(cmd, 0, crcData, 7, 2);
            crcData[9] = 0x04;
            byte[] crc = CRC32.GetCheckValue(crcData);     //---------获取CRC校验码--------
            //---------拼接到包中------
            Buffer.BlockCopy(crcData, 0, udp.ProtocolData, 0, crcData.Length);//---校验数据---
            Buffer.BlockCopy(crc, 0, udp.ProtocolData, crcData.Length, 4);//---校验码----
            Array.Resize(ref udp.ProtocolData, crcData.Length + 4);//重新设定长度    
            udp.Length = 28 + crcData.Length + 4 + 1;

            return udp;
        }

        /// <summary>
        /// 获取设备
        /// </summary>
        private void getCallbackAddressData(UdpData data, object[] values)
        {
            //------获取数据-----
            UserUdpData userData = new UserUdpData(data);
            if (userData.TargetID != this.DeviceID)
                return;
            byte[] byteName = CommonTools.CopyBytes(userData.Data, 0, userData.DataLength - 4);
            Address = Encoding.GetEncoding("GB2312").GetString(byteName).TrimEnd('\0').Trim().Replace("", ""); 
            CallbackUI(new CallbackParameter(ActionKind.ReadServerAddress,DeviceID, Address));//---返回UI---- 

        }

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public void InitMenu()
        {
            DataTableMenu.Clear();
            menuList = new MenuList(this);//菜单控制 
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
            }
            //---一级菜单---
            for (int i = 0; i < MenuConfig.Level1Menus.Length; i++)
            {
                DataTableMenu.Rows.Add(new object[] { "1" + (i + 1), "", "", "", "", MenuConfig.Level1Menus[i], i });
            }
            //---二级菜单--- 
            for (int i = 0; i < MenuConfig.Level21Menus.Length; i++)
            {
                DataTableMenu.Rows.Add(new object[] { "21" + (i + 1), MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "11", "配置", MenuConfig.Level21Menus[i], Int16.Parse("21" + (i + 1)) });
                DataTableMenu.Rows.Add(new object[] { "22" + (i + 1), MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "12", "配置", MenuConfig.Level22Menus[i], Int16.Parse("22" + (i + 1)) });
                DataTableMenu.Rows.Add(new object[] { "23" + (i + 1), MenuKind.MS_COBJ_CMD, MenuKind.MS_COBJ_CMD_NAME, "13", "配置", MenuConfig.Level23Menus[i], Int16.Parse("23" + (i + 1)) });
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
            DataTableMenu.AcceptChanges();

  
        }

        /// <summary>
        /// 根据ID找对应菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private DataRow findNodeDataByID(String id)
        {
            DataRow[] drs = DataTableMenu.Select(string.Format(" {0} = '{1}' ", MenuConfig.DC_ID, id));
            if (drs != null)
                return drs[0];
            else
                return null;
        }

        /// <summary>
        /// 增加菜单行
        /// </summary>
        public DataRow AddMenu(string id, int num)
        {
            DataRow drSelect = findNodeDataByID(id);

            return DataTableMenu.Rows.Add(new object[] {
                Guid.NewGuid().ToString(), //---唯一ID编号--
                MenuKind.MS_COBJ_CMD,  MenuKind.MS_COBJ_CMD_NAME, //---默认为指令----
                drSelect[MenuConfig.DC_ID].ToString(),"配置", "", id});



        }

        /// <summary>
        /// 删除菜单行
        /// </summary>
        public void DelMenu(string id)
        {
            DataRow drSelect = findNodeDataByID(id);
            DataTableMenu.Rows.Remove(drSelect);
        }

        /// <summary>
        /// 读取菜单
        /// </summary>
        /// <param name="level"></param>
        /// <param name="count"></param>
        public void ReadMenu(int level, int count)
        {
            int startNum = menuList.GetMemuNum(level, new int[] { 0, 1, 2, 3 });
            int endNum = startNum + count - 1;
            menuList.ReadMenuData(startNum, endNum);
        }


        /// <summary>
        /// 回调
        /// </summary>
        public  void CallbackUI(CallbackParameter callbackParameter)
        {

            lock (DataTableMenu)
            {
                if (callbackParameter.Action == ActionKind.ReadMenu)
                {
                    WeiXinMeun menu = new WeiXinMeun((UserUdpData)callbackParameter.Parameters[0]);
                    
                }
            }

        }
    }

}
