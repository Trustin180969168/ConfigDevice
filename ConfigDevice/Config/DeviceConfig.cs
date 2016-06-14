using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class DeviceConfig
    {


        //-----------回应启动搜索设备-------------
        public const byte RETSTARTSEARCH_TRUE = 0x0;        // 成功启动搜索设备
        public const byte RETSTARTSEARCH_NET_ER = 0x1;      // 错误,网段参数错误
        public const byte RETSTARTSEARCH_BUSY = 0x2;         // 正忙,其它设备搜索设备中
        public const byte RETSTARTSEARCH_TOTAL = 0x3;          // 总数

        //-----------定义全局对应--------
        public const string DC_NUM = "NUM";//设备ID
        public const string DC_ID = "DeviceID";//设备ID
        public const string DC_NETWORK_ID = "NetworkID";//网段ID
        public const string DC_KIND_ID = "KindID";//设备类型ID
        public const string DC_KIND_NAME = "KindName";//设备类型名称
        public const string DC_NAME = "Name";//设备名称
        public const string DC_MAC = "MAC";//物理ID
        public const string DC_STATE = "State";//连接状态
        public const string DC_REMARK = "Remark";//提示
        public const string DC_SOFTWARE_VER = "SoftwareVer";//软件版本
        public const string DC_HARDWARE_VER = "HardwareVer";//硬件版本
        public const string DC_PC_ADDRESS = "PCAddress";//PC地址
        public const string DC_NETWORK_IP = "NetworkIP";//网络IP
        public const string DC_ADDRESS_NAME = "AddressName";//设备地址 
        public const string DC_ADDRESS_ID = "AddressID";//设备地址ID 
        public const string DC_CONTROL_OBJ = "ControlObj";//设备地址
        public const string DC_COMMAND = "Command";//设备指令
        public const string DC_PARAMETER1 = "Parameter1";//参数1
        public const string DC_PARAMETER2 = "Parameter2";//参数2
        public const string DC_PARAMETER3 = "Parameter3";//参数3
        public const string DC_PARAMETER4 = "Parameter4";//参数4
        public const string DC_PARAMETER5 = "Parameter5";//参数5
        public const string DC_IMAGE1 = "Image1";//图片

        public const string ERROR_SAME_DEVICE_ID = "设备ID冲突!";//设备ID冲突
        public const string ERROR_SAME_DEVICE_TITLE = "设备名称冲突!";//设备名称冲突
        public const string ERROR_SAME_DEVICE_NETWORK_ID = "非法网络ID!";
        public const string STATE_RIGHT = "√";//正常状态
        public const string STATE_ERROR = "×";//错误状态
        public const string STATE_OPEN_LIGHT = "0";//开灯
        public const string STATE_CLOSE_LIGHT = "1";//关灯
        //-------------------设备类型----------------------------
        //设备类型   如果以下无设备的类型则新增
        public const byte EQUIPMENT_KEY = 0x0;          //代表所有的键盘
        public const byte EQUIPMENT_SWIT = 0x1;          //代表所以的开关设备   
        public const byte EQUIPMENT_KEY_LCD = 0x10;       //液晶按键
        public const byte EQUIPMENT_KEY_TFT_LCD = 0x11;       //彩色液晶按键
        public const byte EQUIPMENT_KEY_1 = 0x12;       //1键按键
        public const byte EQUIPMENT_KEY_2 = 0x13;       //2键按键
        public const byte EQUIPMENT_KEY_3 = 0x14;       //3键按键
        public const byte EQUIPMENT_KEY_4 = 0x15;       //4键按键
        public const byte EQUIPMENT_KEY_5 = 0x16;       //5键按键
        public const byte EQUIPMENT_KEY_6 = 0x17;       //6键按键
        public const byte EQUIPMENT_KEY_7 = 0x18;       //7键按键
        public const byte EQUIPMENT_KEY_8 = 0x19;       //8键按键 
        public const byte EQUIPMENT_SWIT_4 = 0x20;       //4路继电器
        public const byte EQUIPMENT_SWIT_6 = 0x21;       //6路继电器
        public const byte EQUIPMENT_SWIT_8 = 0x22;       //8路继电器
        public const byte EQUIPMENT_SWIT_12 = 0x23;       //12路继电器
        public const byte EQUIPMENT_SRC_2 = 0x24;       //2路可控硅调光
        public const byte EQUIPMENT_SRC_4 = 0x25;       //4路可控硅调光
        public const byte EQUIPMENT_SRC_6 = 0x26;       //6路可控硅调光
        public const byte EQUIPMENT_SRC_8 = 0x27;       //8路可控硅调光
        public const byte EQUIPMENT_SRC_12 = 0x28;       //12路可控硅调光器
        public const byte EQUIPMENT_10V_2 = 0x29;       //2路10调光
        public const byte EQUIPMENT_10V_4 = 0x2a;       //4路10调光
        public const byte EQUIPMENT_10V_6 = 0x2b;       //6路10调光
        public const byte EQUIPMENT_10V_8 = 0x2c;       //8路10调光
        public const byte EQUIPMENT_10V_12 = 0x2d;       //12路10调光
        public const byte EQUIPMENT_LED_2 = 0x50;       //2路LED调光  //因之前没有分配，所以临时插入
        public const byte EQUIPMENT_LED_4 = 0x51;       //4路LED调光
        public const byte EQUIPMENT_LED_6 = 0x52;       //6路LED调光
        public const byte EQUIPMENT_LED_8 = 0x53;       //8路LED调光
        public const byte EQUIPMENT_LED_12 = 0x54;       //12路LED调光
        public const byte EQUIPMENT_TRAILING_2 = 0x55;       //2路前沿调光
        public const byte EQUIPMENT_TRAILING_4 = 0x56;       //4路前沿调光
        public const byte EQUIPMENT_TRAILING_6 = 0x57;       //6路前沿调光
        public const byte EQUIPMENT_TRAILING_8 = 0x58;       //8路前沿调光
        public const byte EQUIPMENT_TRAILING_12 = 0x59;       //12路前沿调光
        public const byte EQUIPMENT_GSM = 0x30;       //GSM 模块
        public const byte EQUIPMENT_TEL = 0x31;       //有线电话模块
        public const byte EQUIPMENT_SENSOR = 0x32;
        public const byte EQUIPMENT_TEMP = 0x33;       //温度模块
        public const byte EQUIPMENT_BRIGHT = 0x34;       //亮度模块  
        public const byte EQUIPMENT_PRI_1 = 0x35;       //人体感应1个方向
        public const byte EQUIPMENT_PRI_2 = 0x36;       //人体感应2个方向
        public const byte EQUIPMENT_PRI_3 = 0x37;       //人体感应3个方向
        public const byte EQUIPMENT_PRI_x = 0x38;       //人体感应 备用
        public const byte EQUIPMENT_WEATHER = 0x39;        //气象站，功能包括：雨感、温度、温度、亮度、风速
        public const byte EQUIPMENT_CO2 = 0x3a;        //二氧化碳
        public const byte EQUIPMENT_FUEL_GAS = 0x3b;        //可然气体
        public const byte EQUIPMENT_AIR_QUALITY = 0x3c;        //空气质量
        public const byte EQUIPMENT_IR_CEIL = 0x40;        //红外线转发器,天花型
        public const byte EQUIPMENT_IR_86 = 0x41;        //红外线转发器,86型
        public const byte EQUIPMENT_POWER_1 = 0x44;        //电表1
        public const byte EQUIPMENT_POWER_2 = 0x45;        //电表2
        public const byte EQUIPMENT_POWER_3 = 0x46;        //电表3  
        public const byte EQUIPMENT_POWER_4 = 0x47;        //电表4
        public const byte EQUIPMENT_SHORT_IN_4 = 0x71;        //短路输入
        public const byte EQUIPMENT_SHORT_IN_8 = 0x72;        //短路输入
        public const byte EQUIPMENT_SHORT_OUT_4 = 0x73;        //短路输出
        public const byte EQUIPMENT_SHORT_OUT_8 = 0x74;        //短路输出
        public const byte EQUIPMENT_DOOR_IN_1 = 0x75;        //门输入1 ─┐  [2015年04月01日]
        public const byte EQUIPMENT_DOOR_IN_2 = 0x76;        //门输入2 ─┼→廖超庭：PC编辑软件有这几个宏定义，但我
        public const byte EQUIPMENT_DOOR_IN_4 = 0x77;        //门输入4 ─┘  手上的头文件没有，于是在我头文件补上。
        public const byte EQUIPMENT_WIRELESS_2G4 = 0x48;        //无线2.4G
        public const byte EQUIPMENT_WIRELESS_315M = 0x49;        //无线315M
        public const byte EQUIPMENT_WIRELESS_433M = 0x4a;        //无线433M
        public const byte EQUIPMENT_WIRELESS_ZEEBIG = 0x4b;        //无线zeebig
        public const byte EQUIPMENT_LOGIC = 0x4c;        //logic模块
        public const byte EQUIPMENT_HVAC_2CH = 0x4d;        //空调-2阀门
        public const byte EQUIPMENT_CURTAIN_2CH = 0x4e;        //窗帘-2路  
        public const byte EQUIPMENT_CURTAIN_3CH = 0x4f;        //窗帘-3路 
        public const byte EQUIPMENT_AMP_MP3 = 0x60;        //功放 
        //public const byte EQUIPMENT_WINDOWS          0x61        //门窗
        public const byte EQUIPMENT_DOORBELL = 0x62;        //门铃
        public const byte EQUIPMENT_WINDOWS_2 = 0x63;        //2路门窗
        public const byte EQUIPMENT_WINDOWS_3 = 0x64;        //3路门窗
        public const byte EQUIPMENT_WINDOWS_4 = 0x65;        //4路门窗
        public const byte EQUIPMENT_PLAYER_KEY_7 = 0x80;        //7键播放控制面板
        public const byte EQUIPMENT_ENV_SENSOR_A = 0x81;        //环境传感器A
        public const byte EQUIPMENT_ENV_SENSOR_B = 0x82;        //环境传感器B
        public const byte EQUIPMENT_ENV_SENSOR_C = 0x83;        //环境传感器C
        public const byte EQUIPMENT_ENV_SENSOR_D = 0x84;        //环境传感器D
        public const byte EQUIPMENT_ENV_SENSOR_E = 0x85;        //环境传感器E

        public const byte EQUIPMENT_PANEL = 0xE0;        //通用控制面板

        public const byte EQUIPMENT_RJ45 = 0xf0;        //RJ45类型
        public const byte EQUIPMENT_MOBILE = 0xfc;        //手机
        public const byte EQUIPMENT_SERVER = 0xfd;        //服务器
        public const byte EQUIPMENT_PC = 0xfe;        //PC类型
        public const byte EQUIPMENT_PUBLIC = 0xFF;        //共公类型 

        //-------------------指令分类--------------------
        public const byte CMD_TYPE_PUBLIC = EQUIPMENT_PUBLIC;           //公共类型
        public const byte CMD_TYPE_PC = EQUIPMENT_PC;              //电脑类型
        public const byte CMD_TYPE_SERVER = EQUIPMENT_SERVER;           //服务器   
        public const byte CMD_TYPE_SWITCH = EQUIPMENT_SWIT;        	   //开关
        public const byte CMD_TYPE_KEY = EQUIPMENT_KEY;           	   //按键
        public const byte CMD_TYPE_LOGIC = EQUIPMENT_LOGIC;             //逻辑
        public const byte CMD_TYPE_AC = EQUIPMENT_HVAC_2CH;        //空调
        public const byte CMD_TYPE_CURTAIN = EQUIPMENT_CURTAIN_2CH;   //窗帘
        public const byte CMD_TYPE_PRI = EQUIPMENT_PRI_3;	 		   //人体感应器	
        public const byte CMD_TYPE_AMP = EQUIPMENT_AMP_MP3;           //功放  
        public const byte CMD_TYPE_WINDOWS = EQUIPMENT_WINDOWS_2;		   //门窗
        public const byte CMD_TYPE_IR = EQUIPMENT_IR_CEIL;           //红外线
        public const byte CMD_TYPE_DOORBELL = EQUIPMENT_DOORBELL;          //门铃 
        public const byte CMD_TYPE_GSM = EQUIPMENT_GSM;               //GSM网络
        public const byte CMD_TYPE_MOBILE = EQUIPMENT_MOBILE;           //手机
        public const byte CMD_TYPE_PANEL = EQUIPMENT_PANEL;         //通用控制面板

        //--------------------定义设备命令-----------------------------
        //------网络命令------
        public static readonly byte[] CMD_PC_SEARCH = new byte[] { 0x01, DeviceConfig.EQUIPMENT_PC };       //----搜索网络命令----
        public static readonly byte[] CMD_PC_CONNECT = new byte[] { 0x30, DeviceConfig.EQUIPMENT_PC };      //----链接网络命令---
        public static readonly byte[] CMD_PC_CONNECT_ACK = new byte[] { 0xB0, DeviceConfig.EQUIPMENT_PC };  //----允许连接命令----
        public static readonly byte[] CMD_PC_CONNECTING = new byte[] { 0x31, DeviceConfig.EQUIPMENT_PC };   //----RJ45或主机主动刷新连接命令----
        public static readonly byte[] CMD_PC_DISCONNECT = new byte[] { 0x32, DeviceConfig.EQUIPMENT_PC };   //----PC主动断开连接命令----
        public static readonly byte[] CMD_PC_SEARCH_ACK = new byte[] { 0x81, DeviceConfig.EQUIPMENT_PC };   //2. <RJ45-PC> RJ45上报基本信息
        public static readonly byte[] CMD_PC_CHANGEPASSWORD = new byte[] { 0x02, DeviceConfig.EQUIPMENT_PC };   //3. <PC-RJ45> 修改RJ45密码
        public static readonly byte[] CMD_PC_CHANGEPASSWORD_ACK = new byte[] { 0x82, DeviceConfig.EQUIPMENT_PC };//4. <RJ45-PC> 回应修改RJ45密码结果
        public static readonly byte[] CMD_PC_CHANGENET = new byte[] { 0x03, DeviceConfig.EQUIPMENT_PC };        //5. <PC-RJ45> 修改RJ45网络参数
        public static readonly byte[] CMD_PC_CHANGENET_ACK = new byte[] { 0x83, DeviceConfig.EQUIPMENT_PC };    //6. <RJ45-PC> 回应PC修改RJ45网络参数结果
        public static readonly byte[] CMD_PC_CHANGENAME = new byte[] { 0x04, DeviceConfig.EQUIPMENT_PC };       //7. <PC-RJ45> 修改RJ45设备名称
        public static readonly byte[] CMD_PC_CHANGENAME_ACK = new byte[] { 0x84, DeviceConfig.EQUIPMENT_PC };   //8. <RJ45-PC> 回应PC修改RJ45设备名称结果
        public static readonly byte[] CMD_PC_READ_LOCALL_NAME = new byte[] { 0x05, DeviceConfig.EQUIPMENT_PC }; //14.<PC-RJ45> 读位置名称
        public static readonly byte[] CMD_PC_WRITE_LOCALL_NAME = new byte[] { 0x85, DeviceConfig.EQUIPMENT_PC };//14.<RJ45-PC> 写位置名称

        public static readonly byte[] CMD_PUBLIC_START_SEARCH = new byte[] { 0x31, EQUIPMENT_PUBLIC };//----搜索设备命令----
        public static readonly byte[] CMD_PUBLIC_RET_START_SEARCH = new byte[] { 0x3F, EQUIPMENT_PUBLIC };//----回复确认搜索设备命令----
        public static readonly byte[] CMD_PUBLIC_WRITE_INF = new byte[] { 0x82, EQUIPMENT_PUBLIC };//----设备基本信息返回命令----
        public static readonly byte[] CMD_PUBLIC_STOP_SEARCH = new byte[] { 0x32, EQUIPMENT_PUBLIC };//----停止搜索设备----
        public static readonly byte[] CMD_PUBLIC_READ_VER = new byte[] { 0xB0, EQUIPMENT_PUBLIC };//-----读设备软硬件版本----
        public static readonly byte[] CMD_PUBLIC_WRITE_VER = new byte[] { 0xB1, EQUIPMENT_PUBLIC };//----写设备软硬件版本----
        public static readonly byte[] CMD_PUBLIC_ASSIGN_ID = new byte[] { 0x35, EQUIPMENT_PUBLIC };//----修改设备ID----        
        public static readonly byte[] CMD_PUBLIC_NC1 = new byte[] { 0x00, EQUIPMENT_PUBLIC };//无效指令，上位机不会发送此指令
        public static readonly byte[] CMD_PUBLIC_NC2 = new byte[] { EQUIPMENT_PUBLIC, EQUIPMENT_PUBLIC };//同上
        public static readonly byte[] CMD_PUBLIC_NULL = new byte[] { 0x30, EQUIPMENT_PUBLIC };//空指令，但上位机会发送此指令，这个用于上位机临时修改用，下位机要保存此数据        
        public static readonly byte[] CMD_PUBLIC_ROLLCALL = new byte[] { 0x33, EQUIPMENT_PUBLIC };//设备点名             
        public static readonly byte[] CMD_PUBLIC_SEARCH_UNROLLCALL = new byte[] { 0x34, EQUIPMENT_PUBLIC };//搜索没点名的设备       
        public static readonly byte[] CMD_PUBLIC_STOP_READ = new byte[] { 0x36, EQUIPMENT_PUBLIC };//停止读取设备信息 	
        public static readonly byte[] CMD_PUBLIC_TEST = new byte[] { 0x37, EQUIPMENT_PUBLIC };//设备测试指令  
        public static readonly byte[] CMD_PUBLIC_UART_LED_ENABLE = new byte[] { 0x38, EQUIPMENT_PUBLIC };//开通度灯
        public static readonly byte[] CMD_PUBLIC_UART_LED_DISABLE = new byte[] { 0x39, EQUIPMENT_PUBLIC };//关通度灯
        public static readonly byte[] CMD_PUBLIC_DISCOVER_ENABLE = new byte[] { 0x3a, EQUIPMENT_PUBLIC };//发现设备,通信关亮1分钟，或面板的灯闪1分钟方便调试时找到设备
        public static readonly byte[] CMD_PUBLIC_DISCOVER_DISABLE = new byte[] { 0x3b, EQUIPMENT_PUBLIC };//关发现设备
        public static readonly byte[] CMD_PUBLIC_DEL_COMMAND = new byte[] { 0x3d, EQUIPMENT_PUBLIC };//删除指令
        public static readonly byte[] CMD_PUBLIC_STOP = new byte[] { 0x3e, EQUIPMENT_PUBLIC };//停止读控制信息
        public static readonly byte[] CMD_PUBLIC_RET_READ_INF = new byte[] { 0x3f, EQUIPMENT_PUBLIC };//回复读信息
        public static readonly byte[] CMD_PUBLIC_END_READ_INF = new byte[] { 0x40, EQUIPMENT_PUBLIC };//完成配置信息读取
        public static readonly byte[] CMD_PUBLIC_RET_READ_STATE = new byte[] { 0x41, EQUIPMENT_PUBLIC };//回复读状态
        public static readonly byte[] CMD_PUBLIC_WRITE_END = new byte[] { 0x43, EQUIPMENT_PUBLIC };//读完成
        public static readonly byte[] CMD_PUBLIC_END_READ_STATE = new byte[] { 0x44, EQUIPMENT_PUBLIC };//读状态完成
        public static readonly byte[] CMD_PUBLIC_BROADCAST_PACKET_ID = new byte[] { 0x45, EQUIPMENT_PUBLIC };//广播包号
        public static readonly byte[] CMD_PUBLIC_APPLY_PACKET = new byte[] { 0x46, EQUIPMENT_PUBLIC };//补包
        public static readonly byte[] CMD_PUBLIC_ROLLCALL_ONLINE = new byte[] { 0x47, EQUIPMENT_PUBLIC };//点名在线设备
        public static readonly byte[] CMD_PUBLIC_ONLINE_STATE = new byte[] { 0x48, EQUIPMENT_PUBLIC };//设备在线情况
        public static readonly byte[] CMD_PUBLIC_ONLINE = new byte[] { 0x49, EQUIPMENT_PUBLIC };//设备上线
        public static readonly byte[] CMD_PUBLIC_READ_TIME = new byte[] { 0x01, EQUIPMENT_PUBLIC };//读系统时间
        public static readonly byte[] CMD_PUBLIC_WRITE_TIME = new byte[] { 0x81, EQUIPMENT_PUBLIC };//系统时间        
        public static readonly byte[] CMD_PUBLIC_READ_INF = new byte[] { 0x02, EQUIPMENT_PUBLIC };//读基本信息         	
        public static readonly byte[] CMD_PUBLIC_READ_SET_INF = new byte[] { 0x03, EQUIPMENT_PUBLIC };//读取设备信息	    
        public static readonly byte[] CMD_PUBLIC_READ_NAME = new byte[] { 0x04, EQUIPMENT_PUBLIC };//读设备名称           
        public static readonly byte[] CMD_PUBLIC_WRITE_NAME = new byte[] { 0x84, EQUIPMENT_PUBLIC };//写设备名称   
        public static readonly byte[] CMD_PUBLIC_READ_LOOP_NAME = new byte[] { 0x05, EQUIPMENT_PUBLIC };//读回路名称 
        public static readonly byte[] CMD_PUBLIC_WRITE_LOOP_NAME = new byte[] { 0x85, EQUIPMENT_PUBLIC };//写回路名称	
        public static readonly byte[] CMD_PUBLIC_READ_APPOINT_MACHINE = new byte[] { 0x06, EQUIPMENT_PUBLIC };//读回指定设备的基本信息，这个是长按状态键，使设备处于返回状态时才有效，按下此按60秒处于有效状态，
        //当返回后3秒才把状态清，因防止数据转发时出错重发。用CMD_WRITE_INF 返回   
        public static readonly byte[] CMD_PUBLIC_READ_LOGIC_SEARCH = new byte[] { 0x07, EQUIPMENT_PUBLIC };//逻辑查找指令
        public static readonly byte[] CMD_PUBLIC_WRITE_LOGIC_SEARCH = new byte[] { 0x87, EQUIPMENT_PUBLIC };//返回逻辑查找指令
        public static readonly byte[] CMD_PUBLIC_READ_COMPARE_DATA = new byte[] { 0x08, EQUIPMENT_PUBLIC };//读查找比较控制指令
        public static readonly byte[] CMD_PUBLIC_WRITE_COMPARE_DATA = new byte[] { 0x88, EQUIPMENT_PUBLIC };//写查找比较控制指令
        public static readonly byte[] CMD_PUBLIC_READ_COMMAND = new byte[] { 0x09, EQUIPMENT_PUBLIC };//读出控制指令
        public static readonly byte[] CMD_PUBLIC_WRITE_COMMAND = new byte[] { 0x89, EQUIPMENT_PUBLIC };//写入控制指令
        public static readonly byte[] CMD_PUBLIC_WRITE_NET_ID = new byte[] { 0x8a, EQUIPMENT_PUBLIC };//修改网络ID
        public static readonly byte[] CMD_PUBLIC_READ_CONFIG = new byte[] { 0x0b, EQUIPMENT_PUBLIC };//读配置信息
        public static readonly byte[] CMD_PUBLIC_WRITE_CONFIG = new byte[] { 0x8b, EQUIPMENT_PUBLIC };//写配置信息
        public static readonly byte[] CMD_PUBLIC_READ_STATE = new byte[] { 0x0c, EQUIPMENT_PUBLIC };//读状态信息
        public static readonly byte[] CMD_PUBLIC_WRITE_STATE = new byte[] { 0x8c, EQUIPMENT_PUBLIC };//写状态	
        public static readonly byte[] CMD_PUBLIC_READ_SWIT_STATE = new byte[] { 0x0d, EQUIPMENT_PUBLIC };//读开关状态 
        public static readonly byte[] CMD_PUBLIC_WRITE_SWIT_STATE = new byte[] { 0x8d, EQUIPMENT_PUBLIC };//写开关状态 
        public static readonly byte[] CMD_PUBLIC_READ_HVAC_STATE = new byte[] { 0x0e, EQUIPMENT_PUBLIC };//读空调状态
        public static readonly byte[] CMD_PUBLIC_WRITE_HVAC_STATE = new byte[] { 0x8e, EQUIPMENT_PUBLIC };//写空调状态
        public static readonly byte[] CMD_PUBLIC_READ_CURTAIN_STATE = new byte[] { 0x0f, EQUIPMENT_PUBLIC };//读窗帘状态
        public static readonly byte[] CMD_PUBLIC_WRITE_CURTAIN_STATE = new byte[] { 0x8f, EQUIPMENT_PUBLIC };//写窗帘状态
        public static readonly byte[] CMD_PUBLIC_READ_SENSOR_STATE = new byte[] { 0x10, EQUIPMENT_PUBLIC };//读传感器状态
        public static readonly byte[] CMD_PUBLIC_WRITE_SENSOR_STATE = new byte[] { 0x90, EQUIPMENT_PUBLIC };//写传感器状态
        public static readonly byte[] CMD_PUBLIC_READ_KB_STATE = new byte[] { 0x11, EQUIPMENT_PUBLIC };//读键盘按状态
        public static readonly byte[] CMD_PUBLIC_WRITE_KB_STATE = new byte[] { 0x91, EQUIPMENT_PUBLIC };//写键盘按状态
        public static readonly byte[] CMD_PUBLIC_READ_MULTI = new byte[] { 0x12, EQUIPMENT_PUBLIC };//公共读指令多条返回  
        public static readonly byte[] CMD_PUBLIC_READ_SINGLE = new byte[] { 0x13, EQUIPMENT_PUBLIC };//公共读指令单条返回
        public static readonly byte[] CMD_PUBLIC_READ_PLACE_NAME = new byte[] { 0x14, EQUIPMENT_PUBLIC };//读位置名称
        public static readonly byte[] CMD_PUBLIC_WRITE_PLACE_NAME = new byte[] { 0x94, EQUIPMENT_PUBLIC };//写位置名称
        public static readonly byte[] CMD_PUBLIC_READ_PASSWORD = new byte[] { 0x15, EQUIPMENT_PUBLIC };//读密码
        public static readonly byte[] CMD_PUBLIC_WRITE_PASSWORD = new byte[] { 0x95, EQUIPMENT_PUBLIC };//写密码
        public static readonly byte[] CMD_PUBLIC_SIMPLE_SWIT = new byte[] { 0x20, EQUIPMENT_PUBLIC };//简易开关指令，接着数据0表示关，非0表示调光的数值
        public static readonly byte[] CMD_PUBLIC_SWIT = new byte[] { 0x22, EQUIPMENT_PUBLIC };//开关指令  ，指令的第一个数据 0表示关，1表示开，2表示取反 ,数据的第三位（不能修改）表示开关对像
        public static readonly byte[] CMD_PUBLIC_SWIT_OPEN = new byte[] { 0x23, EQUIPMENT_PUBLIC };//开指令     
        public static readonly byte[] CMD_PUBLIC_SWIT_CLOSE = new byte[] { 0x24, EQUIPMENT_PUBLIC };//关指令 
        public static readonly byte[] CMD_PUBLIC_SWIT_OPEN_CONDITION = new byte[] { 0x2b, EQUIPMENT_PUBLIC };//开指令,只有参数开时才执行
        public static readonly byte[] CMD_PUBLIC_SWIT_CLOSE_CONDITION = new byte[] { 0x2c, EQUIPMENT_PUBLIC };//关指令,只有参数关时才执行
        public static readonly byte[] CMD_PUBLIC_SIMPLE_SWIT_NOT = new byte[] { 0x25, EQUIPMENT_PUBLIC };//简易取反指令
        public static readonly byte[] CMD_PUBLIC_SWIT_NOT = new byte[] { 0x25, EQUIPMENT_PUBLIC };//相反指令	
        public static readonly byte[] CMD_PUBLIC_INC = new byte[] { 0x26, EQUIPMENT_PUBLIC };//指令加    
        public static readonly byte[] CMD_PUBLIC_DEC = new byte[] { 0x27, EQUIPMENT_PUBLIC };//指令减    
        public static readonly byte[] CMD_PUBLIC_VAL = new byte[] { 0x28, EQUIPMENT_PUBLIC };//指定数值，如灯光的亮度，量音的大小
        public static readonly byte[] CMD_PUBLIC_SIMULATE_KEY = new byte[] { 0x29, EQUIPMENT_PUBLIC };//模拟键盘
        public static readonly byte[] CMD_PUBLIC_WINDOWS_PLAY = new byte[] { 0x2a, EQUIPMENT_PUBLIC };//门窗播放指令
        public static readonly byte[] CMD_PUBLIC_SAFETY_STATE = new byte[] { 0xa0, EQUIPMENT_PUBLIC };//安防状态 
        public static readonly byte[] CMD_PUBLIC_RESET_HOST = new byte[] { 0xb2, EQUIPMENT_PUBLIC };       //复位与主机连接  
        //-------------服务器指令--------------------
        public static readonly byte[] CMD_SERVER_SEARCH = new byte[] { 0x30, CMD_TYPE_SERVER };//服务器搜索转换器
        public static readonly byte[] CMD_SERVER_CONNECT = new byte[] { 0x31, CMD_TYPE_SERVER };//申请连接
        public static readonly byte[] CMD_SERVER_RET_CONNECT = new byte[] { 0xb2, CMD_TYPE_SERVER };//回复连接
        public static readonly byte[] CMD_SERVER_HEARTBEAT = new byte[] { 0x33, CMD_TYPE_SERVER };//心跳帧
        public static readonly byte[] CMD_SERVER_RET_HEARTBEAT = new byte[] { 0xb3, CMD_TYPE_SERVER };//回复心跳帧
        public static readonly byte[] CMD_SERVER_WEIXIN = new byte[] { 0x20, CMD_TYPE_SERVER };//微信指令
        public static readonly byte[] CMD_MMSG_READ_VER = new byte[] { 0x61, CMD_TYPE_SERVER };//读设备软硬件版本
        public static readonly byte[] CMD_MMSG_WRITE_VER = new byte[] { 0xE1, CMD_TYPE_SERVER };//写设备软硬件版本
        public static readonly byte[] CMD_MMSG_READ_MEMU_NAME = new byte[] { 0x62, CMD_TYPE_SERVER };//读菜单名称
        public static readonly byte[] CMD_MMSG_WRITE_MEMU_NAME = new byte[] { 0xE2, CMD_TYPE_SERVER };//写菜单名称
        public static readonly byte[] CMD_MMSG_READ_COMMAND = new byte[] { 0x63, CMD_TYPE_SERVER };//读控制指令
        public static readonly byte[] CMD_MMSG_WRITE_COMMAND = new byte[] { 0xE3, CMD_TYPE_SERVER };//写入控制指令
        public static readonly byte[] CMD_MMSG_DEL_COMMAND = new byte[] { 0x64, CMD_TYPE_SERVER };//删除控制指令
        public static readonly byte[] CMD_MMSG_READ_SECURITY_CFG = new byte[] { 0x65, CMD_TYPE_SERVER };//读布安防配置
        public static readonly byte[] CMD_MMSG_WRITE_SECURITY_CFG = new byte[] { 0xE5, CMD_TYPE_SERVER };//写布安防配置
        public static readonly byte[] CMD_MMSG_READ_KEY_CFG = new byte[] { 0x66, CMD_TYPE_SERVER };//读菜单按键类型配置
        public static readonly byte[] CMD_MMSG_WRITE_KEY_CFG = new byte[] { 0xE6, CMD_TYPE_SERVER };//写菜单按键类型配置
        public static readonly byte[] CMD_MMSG_READ_BDEV_CFG = new byte[] { 0x67, CMD_TYPE_SERVER };//读绑定的设备设置
        public static readonly byte[] CMD_MMSG_WRITE_BDEV_CFG = new byte[] { 0xE7, CMD_TYPE_SERVER };//写绑定的设备设置

        //---------继电器，调光器指令  CMD_TYPE_SWITCH,电机--------------
        public static readonly byte[] CMD_SW_READ_GROUP_NAME = new byte[] { 0x01, CMD_TYPE_SWITCH };//读分组名称    
        public static readonly byte[] CMD_SW_WRITE_GROUP_NAME = new byte[] { 0x81, CMD_TYPE_SWITCH };//写分组名称     
        public static readonly byte[] CMD_SW_READ_SCENE_NAME = new byte[] { 0x02, CMD_TYPE_SWITCH };//读场景名称    
        public static readonly byte[] CMD_SW_WRITE_SCENE_NAME = new byte[] { 0x82, CMD_TYPE_SWITCH };//写场景名称     
        public static readonly byte[] CMD_SW_READ_LIST_NAME = new byte[] { 0x03, CMD_TYPE_SWITCH };//读时序名称    
        public static readonly byte[] CMD_SW_WRITE_LIST_NAME = new byte[] { 0x83, CMD_TYPE_SWITCH };//写时序名称     
        public static readonly byte[] CMD_SW_READ_GROUP_INF = new byte[] { 0x04, CMD_TYPE_SWITCH };//读分组信息    
        public static readonly byte[] CMD_SW_WRITE_GROUP_INF = new byte[] { 0x84, CMD_TYPE_SWITCH };//写分组信息    
        public static readonly byte[] CMD_SW_READ_SCENE_INF = new byte[] { 0x05, CMD_TYPE_SWITCH };//读场景信息    
        public static readonly byte[] CMD_SW_WRITE_SCENE_INF = new byte[] { 0x85, CMD_TYPE_SWITCH };//写场景信息    
        public static readonly byte[] CMD_SW_READ_LIST_INF = new byte[] { 0x06, CMD_TYPE_SWITCH };//读时序信息    
        public static readonly byte[] CMD_SW_WRITE_LIST_INF = new byte[] { 0x86, CMD_TYPE_SWITCH };//写时序信息    
        public static readonly byte[] CMD_SW_READ_SWIT_PROCE = new byte[] { 0x07, CMD_TYPE_SWITCH };//读回路控制过程序参数
        public static readonly byte[] CMD_SW_WRITE_SWIT_PROCE = new byte[] { 0x87, CMD_TYPE_SWITCH };//写回路控制过程序参数
        public static readonly byte[] CMD_SW_READ_SWIT_VOLTAGE = new byte[] { 0x08, CMD_TYPE_SWITCH };//读回路电压
        public static readonly byte[] CMD_SW_WRITE_SWIT_VOLTAGE = new byte[] { 0x88, CMD_TYPE_SWITCH };//写回路电压 	
        public static readonly byte[] CMD_SW_READ_SWIT_CURRENT = new byte[] { 0x09, CMD_TYPE_SWITCH };//读回路电流
        public static readonly byte[] CMD_SW_WRITE_SWIT_CURRENT = new byte[] { 0x89, CMD_TYPE_SWITCH };//写回路电流
        public static readonly byte[] CMD_SW_READ_SWIT_POWER = new byte[] { 0x0a, CMD_TYPE_SWITCH };//读回路功率
        public static readonly byte[] CMD_SW_WRITE_SWIT_POWER = new byte[] { 0x8a, CMD_TYPE_SWITCH };//写回路功率
        public static readonly byte[] CMD_SW_READ_POWER_ON_RESUME = new byte[] { 0x0b, CMD_TYPE_SWITCH };//读回路上电设置状态
        public static readonly byte[] CMD_SW_WRITE_POWER_ON_RESUME = new byte[] { 0x8b, CMD_TYPE_SWITCH };//写回路上电设置状态

        public static readonly byte[] CMD_SW_SWIT_LOOP = new byte[] { 0x20, CMD_TYPE_SWITCH };//回路开关
        public static readonly byte[] CMD_SW_SWIT_LOOP_OPEN = new byte[] { 0x21, CMD_TYPE_SWITCH };//回路开
        public static readonly byte[] CMD_SW_SWIT_LOOP_CLOSE = new byte[] { 0x22, CMD_TYPE_SWITCH };//回路关
        public static readonly byte[] CMD_SW_SWIT_LOOP_NOT = new byte[] { 0x23, CMD_TYPE_SWITCH };//回路取反
        public static readonly byte[] CMD_SW_SWIT_LOOP_OPEN_CONDITION = new byte[] { 0x30, CMD_TYPE_SWITCH };//回路带条件开
        public static readonly byte[] CMD_SW_SWIT_LOOP_CLOSE_CONDITION = new byte[] { 0x31, CMD_TYPE_SWITCH };//回路带条件关

        public static readonly byte[] CMD_SW_SWIT_SCENE = new byte[] { 0x24, CMD_TYPE_SWITCH };//场景开关
        public static readonly byte[] CMD_SW_SWIT_SCENE_OPEN = new byte[] { 0x25, CMD_TYPE_SWITCH };//场景开
        public static readonly byte[] CMD_SW_SWIT_SCENE_CLOSE = new byte[] { 0x26, CMD_TYPE_SWITCH };//场景关
        public static readonly byte[] CMD_SW_SWIT_SCENE_NOT = new byte[] { 0x27, CMD_TYPE_SWITCH };//场景取反

        public static readonly byte[] CMD_SW_SWIT_LIST = new byte[] { 0x28, CMD_TYPE_SWITCH };//时序开关
        public static readonly byte[] CMD_SW_SWIT_LIST_OPEN = new byte[] { 0x29, CMD_TYPE_SWITCH };//时序开
        public static readonly byte[] CMD_SW_SWIT_LIST_CLOSE = new byte[] { 0x2a, CMD_TYPE_SWITCH };//时序关
        public static readonly byte[] CMD_SW_SWIT_LIST_NOT = new byte[] { 0x2b, CMD_TYPE_SWITCH };//时序取反

        public static readonly byte[] CMD_SW_SWIT_ALL = new byte[] { 0x2c, CMD_TYPE_SWITCH };//全部开关
        public static readonly byte[] CMD_SW_SWIT_ALL_OPEN = new byte[] { 0x2d, CMD_TYPE_SWITCH };//全部开
        public static readonly byte[] CMD_SW_SWIT_ALL_CLOSE = new byte[] { 0x2e, CMD_TYPE_SWITCH };//全部关

        public static readonly byte[] CMD_SW_TEST_LOOP = new byte[] { 0x2f, CMD_TYPE_SWITCH };//回路测试

        public static readonly byte[] CMD_AMP_SLWR_BGM_KEY = new byte[] { 0x20, CMD_TYPE_AMP };//主机：切换背景音源＋模拟按键控制音源
        public static readonly byte[] CMD_AMP_SLWR_BGM_SONG = new byte[] { 0x21, CMD_TYPE_AMP };              //主机：切换背景音源＋指定音源及曲目播放
        public static readonly byte[] CMD_AMP_SLWR_BGM_VOL_SONG = new byte[] { 0x22, CMD_TYPE_AMP };//主机：切换背景音源＋音量＋指定音源曲目播放
        public static readonly byte[] CMD_AMP_SLWR_BGM_SRC = new byte[] { 0x23, CMD_TYPE_AMP };   //主机：切换背景音源     
        public static readonly byte[] CMD_AMP_SLWR_BGM_VOL = new byte[] { 0x24, CMD_TYPE_AMP };   //主机：修改背景音量
        public static readonly byte[] CMD_AMP_SLWR_BGM_TRE = new byte[] { 0x25, CMD_TYPE_AMP };   //主机：修改背景高音
        public static readonly byte[] CMD_AMP_SLWR_BGM_BAS = new byte[] { 0x26, CMD_TYPE_AMP };   //主机：修改背景低音
        public static readonly byte[] CMD_AMP_SLWR_BGM_TUNE = new byte[] { 0x27, CMD_TYPE_AMP };   //主机：修改背景音量,高音,低音
        public static readonly byte[] CMD_AMP_SLWR_MSN_TUNE = new byte[] { 0x28, CMD_TYPE_AMP };   //主机：修改紧急音量,高音,低音

        public static readonly byte[] CMD_AMP_SLWR_PPEMC = new byte[] { 0x2a, CMD_TYPE_AMP };   //主机：指定紧急消息播放模式
        public static readonly byte[] CMD_AMP_SLWR_OUTMSN = new byte[] { 0x2b, CMD_TYPE_AMP };   //音响：退出消息播放，连续广播3次
        public static readonly byte[] CMD_AMP_SLWR_BGM_PLAYMODE = new byte[] { 0x2c, CMD_TYPE_AMP };   //播放模式  
        public static readonly byte[] CMD_AMP_SLWR_BGM_RADIO_NOHZ = new byte[] { 0x2d, CMD_TYPE_AMP };   //保存指令频率到指令的电台

        public static readonly byte[] CMD_AMP_WIFI_SET = new byte[] { 0x2E, CMD_TYPE_AMP };   //选择WIFI网络和设置连接密码(LIAO增加:2015-10-15)
        //----逻辑部分-------

        public static readonly byte[] CMD_LOGIC_WRITE_TIMER_NAME = new byte[] { 0x01, EQUIPMENT_LOGIC };//写定时器名称      
        public static readonly byte[] CMD_LOGIC_READ_TIMER_NAME = new byte[] { 0x81, EQUIPMENT_LOGIC }; 			//读定时器名称       
        public static readonly byte[] CMD_LOGIC_WRITE_BLOCK_NAME = new byte[] { 0x02, EQUIPMENT_LOGIC }; //写逻辑块名称       
        public static readonly byte[] CMD_LOGIC_READ_BLOCK_NAME = new byte[] { 0x82, EQUIPMENT_LOGIC }; //读逻辑块名称       
        public static readonly byte[] CMD_LOGIC_WRITE_BLOCK_PORT_SATE = new byte[] { 0x03, EQUIPMENT_LOGIC }; //写逻辑块端口状态  
        public static readonly byte[] CMD_LOGIC_READ_BLOCK_PORT_SATE = new byte[] { 0x83, EQUIPMENT_LOGIC }; //读逻辑块端口状态  
        public static readonly byte[] CMD_LOGIC_WRITE_TIMER_INF = new byte[] { 0x04, EQUIPMENT_LOGIC }; //写定时器设置信息  
        public static readonly byte[] CMD_LOGIC_READ_TIMER_INF = new byte[] { 0x84, EQUIPMENT_LOGIC }; //读定时器设置信息   
        public static readonly byte[] CMD_LOGIC_WRITE_BLOCK_INF = new byte[] { 0x05, EQUIPMENT_LOGIC }; //写逻辑块设置信息   
        public static readonly byte[] CMD_LOGIC_READ_BLOCK_INF = new byte[] { 0x85, EQUIPMENT_LOGIC }; //读逻辑块设置信息            

        public static readonly byte[] CMD_LOGIC_WRITE_CMD = new byte[] { 0x06, EQUIPMENT_LOGIC }; //写控制指令        
        public static readonly byte[] CMD_LOGIC_READ_CMD = new byte[] { 0x86, EQUIPMENT_LOGIC }; //读控制指令

        public static readonly byte[] CMD_LOGIC_READ_CONFIG = new byte[] { 0x41, EQUIPMENT_LOGIC }; //读参数设置 (条件与逻辑)
        public static readonly byte[] CMD_LOGIC_WRITE_CONFIG = new byte[] { 0xC1, EQUIPMENT_LOGIC }; //写参数设置 (条件与逻辑)
        public static readonly byte[] CMD_LOGIC_READ_EXACTION = new byte[] { 0x42, EQUIPMENT_LOGIC }; //读逻辑附加动作
        public static readonly byte[] CMD_LOGIC_WRITE_EXACTION = new byte[] { 0xC2, EQUIPMENT_LOGIC }; //写逻辑附加动作   (不同设备各不一样)
        public static readonly byte[] CMD_LOGIC_WRITE_SYSLKID = new byte[] { 0x43, EQUIPMENT_LOGIC }; //写逻辑系统联动号 (注意:与[CMD_SW_SWIT_LOOP_OPEN]等指令同一格式)
        public static readonly byte[] CMD_LOGIC_READ_SECURITY = new byte[] { 0x44, EQUIPMENT_LOGIC }; //读逻辑器安防联动标志配置 (各个逻辑动作可单独联动)
        public static readonly byte[] CMD_LOGIC_WRITE_SECURITY = new byte[] { 0xC4, EQUIPMENT_LOGIC }; //写逻辑器安防联动标志配置 (各个逻辑动作可单独联动)


        public static readonly Dictionary<byte, string> EQUIPMENT_ID_NAME = new Dictionary<byte, string>();
        static DeviceConfig()//---静态构造函数------
        {
            //  EQUIPMENT_ID_NAME.Add(EQUIPMENT_SWIT_4,"4路继电器");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_RJ45, "网关处理器");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_KEY_LCD, "液晶按键");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_KEY_TFT_LCD, "彩色液晶按键");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_KEY_1, "1键按键");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_KEY_2, "2键按键");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_KEY_3, "3键按键");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_KEY_4, "4键按键");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_KEY_5, "5键按键");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_KEY_6, "6键按键");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_KEY_7, "7键按键");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SWIT_4, "4路继电器");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SWIT_6, "6路继电器");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SWIT_8, "8路继电器");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SWIT_12, "12路继电器");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SRC_2, "2路硅调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SRC_4, "4路硅调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SRC_6, "6路硅调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SRC_8, "8路硅调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SRC_12, "12路硅调光");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_10V_2, "2路10调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_10V_4, "4路10调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_10V_6, "6路10调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_10V_8, "8路10调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_10V_12, "12路10调光");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_LED_2, "2路LED调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_LED_4, "4路LED调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_LED_6, "6路LED调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_LED_8, "8路LED调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_LED_12, "12路LED调光");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_TRAILING_2, "2路前沿调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_TRAILING_4, "4路前沿调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_TRAILING_6, "6路前沿调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_TRAILING_8, "8路前沿调光");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_TRAILING_12, "12路前沿调光");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_ENV_SENSOR_A, "环境传感器A");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_ENV_SENSOR_B, "环境传感器B");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_ENV_SENSOR_C, "环境传感器C");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_ENV_SENSOR_D, "环境传感器D");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_ENV_SENSOR_E, "环境传感器E");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_WEATHER, "智能气象");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_PRI_1, "人体感应1个方向");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_PRI_2, "人体感应2个方向");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_PRI_3, "人体感应3个方向");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_PRI_x, "人体感应备用");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SHORT_IN_4, "短路输入4");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SHORT_IN_8, "短路输入8");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_DOOR_IN_1, "SCDW01（008：安全信号处理器，最多1路）");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_DOOR_IN_2, "SCDW02（008：安全信号处理器，最多2路）");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_DOOR_IN_4, "SCDW04（008：安全信号处理器，最多4路）");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_WINDOWS_2, "2路门窗");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_WINDOWS_3, "3路门窗");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_WINDOWS_4, "4路门窗");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_DOORBELL, "门铃");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_PLAYER_KEY_7, "7键播放控制面板");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_CURTAIN_2CH, "窗帘-2路");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_CURTAIN_3CH, "窗帘-3路");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SHORT_OUT_4, "短路输出4");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SHORT_OUT_8, "短路输出8");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_AMP_MP3, "蓝牙功放");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_GSM, "GSM模块");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_HVAC_2CH, "空调-2阀门");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_IR_CEIL, "天花型红外");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_IR_86, "86型红外");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SERVER, "云平台主机");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_FUEL_GAS, "可燃气体报警器");

            EQUIPMENT_ID_NAME.Add(0, "无效类型");


        }


    }


}
