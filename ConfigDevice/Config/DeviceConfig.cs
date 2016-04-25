using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    class DeviceConfig
    {
        // 回应启动搜索设备
        public const byte RETSTARTSEARCH_TRUE = 0x0;        // 成功启动搜索设备
        public const byte RETSTARTSEARCH_NET_ER = 0x1;      // 错误,网段参数错误
        public const byte RETSTARTSEARCH_BUSY = 0x2;         // 正忙,其它设备搜索设备中
        public const byte RETSTARTSEARCH_TOTAL = 0x3;          // 总数

        //------定义全局对应--------
        public const string DC_DEVICE_NUM = "NUM";//设备ID
        public const string DC_DEVICE_ID = "DeviceID";//设备ID
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
        public const string DC_ADDRESS = "Address";//设备地址

        public const string ERROR_SAME_DEVICE_ID = "设备ID冲突!";//设备ID冲突
        public const string ERROR_SAME_DEVICE_TITLE = "设备名称冲突!";//设备名称冲突
        public const string STATE_RIGHT = "√";//正常状态
        public const string STATE_ERROR = "×";//错误状态

        //--------------------定义设备命令-----------------------------
        public static readonly byte[] CMD_PUBLIC_START_SEARCH = new byte[2] { 0x31, 0xFF };//----搜索设备命令----
        public static readonly byte[] CMD_PUBLIC_RET_START_SEARCH = new byte[2] { 0x3F, 0xFF };//----回复确认搜索设备命令----
        public static readonly byte[] CMD_PUBLIC_WRITE_INF = new byte[2] { 0x82, 0xFF };//----设备基本信息返回命令----
        public static readonly byte[] CMD_PUBLIC_STOP_SEARCH = new byte[2] { 0x32, 0xFF };//----停止搜索设备----
        public static readonly byte[] CMD_PUBLIC_READ_VER = new byte[2] { 0xB0, 0xFF };//-----读设备软硬件版本----
        public static readonly byte[] CMD_PUBLIC_WRITE_VER = new byte[2] { 0xB1, 0xFF };//----写设备软硬件版本----
        public static readonly byte[] CMD_PUBLIC_ASSIGN_ID = new byte[2] { 0x35, 0xFF };//----修改设备ID----        
        public static readonly byte[] CMD_PUBLIC_NC1 = new byte[2] { 0x00, 0xFF };//无效指令，上位机不会发送此指令
        public static readonly byte[] CMD_PUBLIC_NC2 = new byte[2] { 0xff, 0xFF };//同上
        public static readonly byte[] CMD_PUBLIC_NULL = new byte[2] { 0x30, 0xFF };//空指令，但上位机会发送此指令，这个用于上位机临时修改用，下位机要保存此数据        
        public static readonly byte[] CMD_PUBLIC_ROLLCALL = new byte[2] { 0x33, 0xFF };//设备点名             
        public static readonly byte[] CMD_PUBLIC_SEARCH_UNROLLCALL = new byte[2] { 0x34, 0xFF };//搜索没点名的设备       
        public static readonly byte[] CMD_PUBLIC_STOP_READ = new byte[2] { 0x36, 0xFF };//停止读取设备信息 	
        public static readonly byte[] CMD_PUBLIC_TEST = new byte[2] { 0x37, 0xFF };//设备测试指令  
        public static readonly byte[] CMD_PUBLIC_UART_LED_ENABLE = new byte[2] { 0x38, 0xFF };//开通度灯
        public static readonly byte[] CMD_PUBLIC_UART_LED_DISABLE = new byte[2] { 0x39, 0xFF };//关通度灯
        public static readonly byte[] CMD_PUBLIC_DISCOVER_ENABLE = new byte[2] { 0x3a, 0xFF };//发现设备,通信关亮1分钟，或面板的灯闪1分钟方便调试时找到设备
        public static readonly byte[] CMD_PUBLIC_DISCOVER_DISABLE = new byte[2] { 0x3b, 0xFF };//关发现设备
        public static readonly byte[] CMD_PUBLIC_DEL_COMMAND = new byte[2] { 0x3d, 0xFF };//删除指令
        public static readonly byte[] CMD_PUBLIC_STOP = new byte[2] { 0x3e, 0xFF };//停止读控制信息
        public static readonly byte[] CMD_PUBLIC_RET_READ_INF = new byte[2] { 0x3f, 0xFF };//回复读信息
        public static readonly byte[] CMD_PUBLIC_END_READ_INF = new byte[2] { 0x40, 0xFF };//完成配置信息读取
        public static readonly byte[] CMD_PUBLIC_RET_READ_STATE = new byte[2] { 0x41, 0xFF };//回复读状态
        public static readonly byte[] CMD_PUBLIC_WRITE_END = new byte[2] { 0x43, 0xFF };//读完成
        public static readonly byte[] CMD_PUBLIC_END_READ_STATE = new byte[2] { 0x44, 0xFF };//读状态完成
        public static readonly byte[] CMD_PUBLIC_BROADCAST_PACKET_ID = new byte[2] { 0x45, 0xFF };//广播包号
        public static readonly byte[] CMD_PUBLIC_APPLY_PACKET = new byte[2] { 0x46, 0xFF };//补包
        public static readonly byte[] CMD_PUBLIC_ROLLCALL_ONLINE = new byte[2] { 0x47, 0xFF };//点名在线设备
        public static readonly byte[] CMD_PUBLIC_ONLINE_STATE = new byte[2] { 0x48, 0xFF };//设备在线情况
        public static readonly byte[] CMD_PUBLIC_ONLINE = new byte[2] { 0x49, 0xFF };//设备上线
        public static readonly byte[] CMD_PUBLIC_READ_TIME = new byte[2] { 0x01, 0xFF };//读系统时间
        public static readonly byte[] CMD_PUBLIC_WRITE_TIME = new byte[2] { 0x81, 0xFF };//系统时间        
        public static readonly byte[] CMD_PUBLIC_READ_INF = new byte[2] { 0x02, 0xFF };//读基本信息         	
        public static readonly byte[] CMD_PUBLIC_READ_SET_INF = new byte[2] { 0x03, 0xFF };//读取设备信息	    
        public static readonly byte[] CMD_PUBLIC_READ_NAME = new byte[2] { 0x04, 0xFF };//读设备名称           
        public static readonly byte[] CMD_PUBLIC_WRITE_NAME = new byte[2] { 0x84, 0xFF };//写设备名称   
        public static readonly byte[] CMD_PUBLIC_READ_LOOP_NAME = new byte[2] { 0x05, 0xFF };//读回路名称 
        public static readonly byte[] CMD_PUBLIC_WRITE_LOOP_NAME = new byte[2] { 0x85, 0xFF };//写回路名称	
        public static readonly byte[] CMD_PUBLIC_READ_APPOINT_MACHINE = new byte[2] { 0x06, 0xFF };//读回指定设备的基本信息，这个是长按状态键，使设备处于返回状态时才有效，按下此按60秒处于有效状态，
        //当返回后3秒才把状态清，因防止数据转发时出错重发。用CMD_WRITE_INF 返回   
        public static readonly byte[] CMD_PUBLIC_READ_LOGIC_SEARCH = new byte[2] { 0x07, 0xFF };//逻辑查找指令
        public static readonly byte[] CMD_PUBLIC_WRITE_LOGIC_SEARCH = new byte[2] { 0x87, 0xFF };//返回逻辑查找指令
        public static readonly byte[] CMD_PUBLIC_READ_COMPARE_DATA = new byte[2] { 0x08, 0xFF };//读查找比较控制指令
        public static readonly byte[] CMD_PUBLIC_WRITE_COMPARE_DATA = new byte[2] { 0x88, 0xFF };//写查找比较控制指令
        public static readonly byte[] CMD_PUBLIC_READ_COMMAND = new byte[2] { 0x09, 0xFF };//读出控制指令
        public static readonly byte[] CMD_PUBLIC_WRITE_COMMAND = new byte[2] { 0x89, 0xFF };//写入控制指令
        public static readonly byte[] CMD_PUBLIC_WRITE_NET_ID = new byte[2] { 0x8a, 0xFF };//修改网络ID
        public static readonly byte[] CMD_PUBLIC_READ_CONFIG = new byte[2] { 0x0b, 0xFF };//读配置信息
        public static readonly byte[] CMD_PUBLIC_WRITE_CONFIG = new byte[2] { 0x8b, 0xFF };//写配置信息
        public static readonly byte[] CMD_PUBLIC_READ_STATE = new byte[2] { 0x0c, 0xFF };//读状态信息
        public static readonly byte[] CMD_PUBLIC_WRITE_STATE = new byte[2] { 0x8c, 0xFF };//写状态	
        public static readonly byte[] CMD_PUBLIC_READ_SWIT_STATE = new byte[2] { 0x0d, 0xFF };//读开关状态 
        public static readonly byte[] CMD_PUBLIC_WRITE_SWIT_STATE = new byte[2] { 0x8d, 0xFF };//写开关状态 
        public static readonly byte[] CMD_PUBLIC_READ_HVAC_STATE = new byte[2] { 0x0e, 0xFF };//读空调状态
        public static readonly byte[] CMD_PUBLIC_WRITE_HVAC_STATE = new byte[2] { 0x8e, 0xFF };//写空调状态
        public static readonly byte[] CMD_PUBLIC_READ_CURTAIN_STATE = new byte[2] { 0x0f, 0xFF };//读窗帘状态
        public static readonly byte[] CMD_PUBLIC_WRITE_CURTAIN_STATE = new byte[2] { 0x8f, 0xFF };//写窗帘状态
        public static readonly byte[] CMD_PUBLIC_READ_SENSOR_STATE = new byte[2] { 0x10, 0xFF };//读传感器状态
        public static readonly byte[] CMD_PUBLIC_WRITE_SENSOR_STATE = new byte[2] { 0x90, 0xFF };//写传感器状态
        public static readonly byte[] CMD_PUBLIC_READ_KB_STATE = new byte[2] { 0x11, 0xFF };//读键盘按状态
        public static readonly byte[] CMD_PUBLIC_WRITE_KB_STATE = new byte[2] { 0x91, 0xFF };//写键盘按状态
        public static readonly byte[] CMD_PUBLIC_READ_MULTI = new byte[2] { 0x12, 0xFF };//公共读指令多条返回  
        public static readonly byte[] CMD_PUBLIC_READ_SINGLE = new byte[2] { 0x13, 0xFF };//公共读指令单条返回
        public static readonly byte[] CMD_PUBLIC_READ_PLACE_NAME = new byte[2] { 0x14, 0xFF };//读位置名称
        public static readonly byte[] CMD_PUBLIC_WRITE_PLACE_NAME = new byte[2] { 0x94, 0xFF };//写位置名称
        public static readonly byte[] CMD_PUBLIC_READ_PASSWORD = new byte[2] { 0x15, 0xFF };//读密码
        public static readonly byte[] CMD_PUBLIC_WRITE_PASSWORD = new byte[2] { 0x95, 0xFF };//写密码
        public static readonly byte[] CMD_PUBLIC_SIMPLE_SWIT = new byte[2] { 0x20, 0xFF };//简易开关指令，接着数据0表示关，非0表示调光的数值
        public static readonly byte[] CMD_PUBLIC_SWIT = new byte[2] { 0x22, 0xFF };//开关指令  ，指令的第一个数据 0表示关，1表示开，2表示取反 ,数据的第三位（不能修改）表示开关对像
        public static readonly byte[] CMD_PUBLIC_SWIT_OPEN = new byte[2] { 0x23, 0xFF };//开指令     
        public static readonly byte[] CMD_PUBLIC_SWIT_CLOSE = new byte[2] { 0x24, 0xFF };//关指令 
        public static readonly byte[] CMD_PUBLIC_SWIT_OPEN_CONDITION = new byte[2] { 0x2b, 0xFF };//开指令,只有参数开时才执行
        public static readonly byte[] CMD_PUBLIC_SWIT_CLOSE_CONDITION = new byte[2] { 0x2c, 0xFF };//关指令,只有参数关时才执行
        public static readonly byte[] CMD_PUBLIC_SIMPLE_SWIT_NOT = new byte[2] { 0x25, 0xFF };//简易取反指令
        public static readonly byte[] CMD_PUBLIC_SWIT_NOT = new byte[2] { 0x25, 0xFF };//相反指令	
        public static readonly byte[] CMD_PUBLIC_INC = new byte[2] { 0x26, 0xFF };//指令加    
        public static readonly byte[] CMD_PUBLIC_DEC = new byte[2] { 0x27, 0xFF };//指令减    
        public static readonly byte[] CMD_PUBLIC_VAL = new byte[2] { 0x28, 0xFF };//指定数值，如灯光的亮度，量音的大小
        public static readonly byte[] CMD_PUBLIC_SIMULATE_KEY = new byte[2] { 0x29, 0xFF };//模拟键盘
        public static readonly byte[] CMD_PUBLIC_WINDOWS_PLAY = new byte[2] { 0x2a, 0xFF };//门窗播放指令
        public static readonly byte[] CMD_PUBLIC_SAFETY_STATE = new byte[2] { 0xa0, 0xFF };//安防状态 
        public static readonly byte[] CMD_PUBLIC_RESET_HOST = new byte[2] { 0xb2, 0xFF };       //复位与主机连接  

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
        public const byte EQUIPMENT_PUBLIC = 0xff;        //共公类型


        public static readonly Dictionary<byte, string> EQUIPMENT_ID_NAME = new Dictionary<byte, string>();
        static DeviceConfig()//---静态构造函数------
        {
            //  EQUIPMENT_ID_NAME.Add(EQUIPMENT_SWIT_4,"4路继电器");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_RJ45, "RJ45");
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

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_WEATHER, "气象站");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_PRI_1, "人体感应1个方向");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_PRI_2, "人体感应2个方向");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_PRI_3, "人体感应3个方向");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_PRI_x, "人体感应备用");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SHORT_IN_4, "短路输入4");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SHORT_IN_8, "短路输入8");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_DOOR_IN_1, "门输入1");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_DOOR_IN_2, "门输入2");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_DOOR_IN_4, "门输入4");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_WINDOWS_2, "2路门窗");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_WINDOWS_3, "3路门窗");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_WINDOWS_4, "4路门窗");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_DOORBELL, "门铃");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_PLAYER_KEY_7, "7键播放控制面板");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_CURTAIN_2CH, "窗帘-2路");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_CURTAIN_3CH, "窗帘-3路");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SHORT_OUT_4, "短路输出4");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SHORT_OUT_8, "短路输出8");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_AMP_MP3, "功放");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_GSM, "GSM模块");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_HVAC_2CH, "空调-2阀门");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_IR_CEIL, "天花型红外");
            EQUIPMENT_ID_NAME.Add(EQUIPMENT_IR_86, "86型红外");

            EQUIPMENT_ID_NAME.Add(EQUIPMENT_SERVER, "服务器");

            EQUIPMENT_ID_NAME.Add(0, "无效类型");


        }


    }
}
