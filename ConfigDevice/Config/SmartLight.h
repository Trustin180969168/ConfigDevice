#ifndef _SMART_LIGHT_H_
#define _SMART_LIGHT_H_

/*========================================================================================================
** Copyright (c) 2013, 韦尔智能科技有限公司
** All rights reserved.
**
** 文件名称: SmartLight.H
** 文件描述: 通信指令
**
**  注   释: 1. 控制系统下所有的设备都有此指令表，不能在此表下增加自已程序的数据，因此表不断更新
**           2. 需要更新时请把更新的数据统一发到经理处，统一更新后再统一更新
**           3. 
**
** 当前版本: 2.1
** 修 改 者: 钟珊瑚
** 完成日期: 2013年8月22日
** 
**
** 取代版本: 
** 完成日期: 2013年11月08日
** 修改记录: 
   ( 1) v1.2版本，增加CMD_PUBLIC_READ_END,CMD_PUBLIC_WRITE_STATE
   ( 2) V1.3版本，增加人体感应器指令
   ( 3) V1.4版本，增加公共读指令,CMD_PUBLIC_READ_MULTI,CMD_PUBLIC_READ_SINGLE
   ( 4) V1.5版本，增加音响指令
   ( 5) V1.8版本，把CMD_PUBLIC_END_READ_INF 改为 0x40;  	增加 :CMD_PUBLIC_ROLLCALL_ONLINE; CMD_PUBLIC_ONLINE_STATE; CMD_PUBLIC_ONLINE
   ( 6) V1.9版本，增加红外线指令
   ( 7) V2.0版本，增加门窗2,3,4路设备定义
   ( 8) V2.1版本，修改窗帘指令
   ( 9) V2.2版本，增加[7键播放控制面板] (廖超庭 2015年04月01日)
   (10) V2.3版本，增加GSM短信模块指令 (黄小龙 2015年04月01日) 
   (11) V2.4版本，增加[COMMUNICATION_FORMAT]的[Data]大小注释,[Data]大小不包括CRC4字节 (廖超庭 2015年04月03日)
   (12) V2.5版本，增加[安防报警发送短信功能]:[CMD_GSM_READ_SAFE_MESSAGE] (黄小龙 2015年04月21日) 
   (13) V2.6版本，修改原有的人体感应指令，新修改为[详查 CMD_PRI_READ_CONFIG]  (黄小龙 2015年04月27日) 
   (13) V2.7版本，新增加人体感应指令[详查 CMD_PRI_FLASH]  (黄小龙 2015年04月29日) 
   (14) V2.8版本，修改加人体感应指令[详查 CMD_PRI_READ_FLASH, CMD_PRI_WRITE_FLASH]  (黄小龙 2015年05月03日) 
   (15) V2.9版本, 增加[NET_MOBILE]及[EQUIPMENT_MOBILE]相关指令     (廖超庭 2015年06月23日)
   (16) V3.0版本, 增加[EQUIPMENT_PANEL]新设备类型(通用控制面板)    (廖超庭 2015年06月06日)
   (17) V3.1版本, 增加[CMD_MMSG_READ_VER]等微信软件配置指令        (廖超庭 2015年08月15日)
   (18) V3.2版本, 增加[EQUIPMENT_ENV_SENSOR_A]等环境传感器类型     (廖超庭 2015年09月07日)
   (19) V3.3版本, 增加[CMD_AMP_WIFI_SET]音响WIFI指令               (廖超庭 2015年10月15日)
   (20) V3.4版本, 修改[EQUIPMENT_DOOR_IN_3]为[EQUIPMENT_DOOR_IN_4] (廖超庭 2015年10月24日)
   (21) V3.5版本, 增加[CMD_PUBLIC_RESET_HOST]                      (廖超庭 2016年01月13日)
   (22) V3.6版本, 增加[CMD_LOGIC_READ_CONFIG]等指令                (廖超庭 2016年04月12日)
   (23) V3.7版本, 增加[CMD_PUBLIC_TEST_KEY_CMD]指令                (廖超庭 2016年05月04日)
   (24) V3.8版本, 增加[EQUIPMENT_RSP]设备类型                      (廖超庭 2016年05月12日)
   (25) V3.9版本, 增加[CMD_LOGIC_WRITE_SYSLKID]低位值由0x43改为0xC3(廖超庭 2016年06月15日) -> 最终改为0xC5
   (26) V4.0版本, 修改[CMD_LOGIC_WRITE_SYSLKID]为[开/关/开关]等    (廖超庭 2016年06月22日)
   (27) V4.1版本, 增加[EQUIPMENT_AIR_O2]设备类型                   (廖超庭 2016年06月23日)
   (28) V4.2版本, 增加[CMD_PUBLIC_RESET_DEVICE]指令                (廖超庭 2016年09月02日)
   (29) V4.3版本，增加无锁孔门类型和相关无锁孔门指令               (钟珊瑚 2016年09月13日)               
   (30) V4.4版本，增加云id和云网段                                 (钟珊瑚 2016年09月13日)
                  由于修改了使系统不一至所以暂不修改
   (31) V4.5版本，增加[CMD_PUBLIC_WRITE_ADDRESS]网关地址设置(广州市海珠区xxxx)  (钟珊瑚 2016年11月16日)
   (32) V4.6版本，增加 无线转有线主机、指纹锁 类型 增加无线主机、指纹锁指令 CMD_TYPE_RFLINE 、CMD_TYPE_FP_LOCK
   (33) V4.7版本，增加[CMD_RFLINE_READ_CFG][CMD_RFLINE_WRITE_CFG]                  (廖超庭 2017年08月30日)
   (34) V4.8版本，增加[CMD_RFLINE_WRITE_DEVAC_RSL][EQUIPMENT_EL_KNIFE_FRAME]       (廖超庭 2017年08月31日)
   (35) V4.9版本, 增加[EQUIPMENT_INTELLIGENT_SINK]                                 (廖超庭 2017年09月01日)
**======================================================================================================*/


/*=======================================================================================================
系统通信格式：
            设备ID + 网段ID + 类型号 + 源设备ID + 源网段ID + 类型+ 页 + 控制字(2字节) + 长度 + 数据 +  4byte CRC
   长度 = 数据	+ 4byte CRC		
   
 设备ID 范围 0~100 ，网段也是 0~100 ,100以上为特殊，用于专用设备如服务器，平板等
	
    下位机利用 设备类型,设备ID，网段ID来区上数据是否需要接收。	
  
 设备ID，网段ID，类型号组合说明
  设备 ID     网段ID        类型                 说明 
  
                |-- 本段ID --- 指定       本设备正常通信，需要返回数据                      
                |-- 本段ID --- 公共       无效           
  0~100 --|--	公共   --- 指定       无效
                |--	公共   --- 公共       无效
	 
 |-- 本段   --- 指定      本段所有类型相同都处理，不返回
  公共  --|-- 本段   --- 公共      本段所有设备，          不返回
 |-- 公共   --- 指定      系统所类型相同都处理，  不返回
 |-- 公共   --- 公共		 系统所的设备，          不返回
	
 |-- 本段   --- 指定      本段符合条件的指定类型设备返回数据  		
 带返回公共--|-- 本段   --- 公共      本段符合条件的设备返回数据  		
 |-- 公共   --- 指定      系统符合条件的指定类型设备返回数据  		
 |-- 公共   --- 公共      系统符合条件的设备返回数据  
   
========================================================================================================*/
//注意通信数据用小端模式，低位在前高位在后

//特殊设备
/*
enum		//设备ID 0~100,  101以上为指定设备的ID   4.4
{
 ID_MOBILE_START    = 0       ,//手机、平板开始地址
    ID_MOBILE_END      = 99      ,//手机、平板结束地址
 ID_PC_START        = 201     ,//PC地址开始
 ID_PC_END          = 220     ,//PC地址结束
 ID_GATEWAY         = 249     ,//网关id     0xf9
 ID_CLOUD           = 250     ,//云服务id   0xfa
 ID_PKGNUM_PUBLIC   = 251     ,//带包号公共地址(由RJ45缓冲补发并保证其成功到达目标)
 ID_RJ45            = 252     ,//485转网络转换器ID
 ID_SERVER          = 253     ,//服务器
 ID_ANSWER_PUBLIC   = 254     ,//带返回公共地址
 ID_PUBLIC          = 255      //公共地址
};
*/

enum		//设备ID 0~100,  101以上为指定设备的ID
{
	ID_MOBILE_START    = 151     ,//手机、平板开始地址
    ID_MOBILE_END      = 200     ,//手机、平板结束地址
	ID_PC_START        = 201     ,//PC地址开始
	ID_PC_END          = 220     ,//PC地址结束
	ID_PKGNUM_PUBLIC   = 251     ,//带包号公共地址(由RJ45缓冲补发并保证其成功到达目标)
	ID_RJ45            = 252     ,//485转网络转换器ID
	ID_SERVER          = 253     ,//服务器
	ID_ANSWER_PUBLIC   = 254     ,//带返回公共地址
	ID_PUBLIC          = 255      //公共地址
};





//特殊网段
enum					
{		
	NET_MOBILE          = 252,     //手机网段 (廖超庭2017-05-06:无线系统还会用到此网段,所以取消屏蔽)
	NET_SERVER          = 254,     //服务器网段、云、Linux网关、手机网段    0xfe
	NET_PUBLIC          = 255      //公共地址
};


//传感器类型
enum		
{
	SENSOR_TEMP  		= 0		 ,//温度传感器
    SENSOR_HUMIDITY		= 1		 ,//湿度传感器
	SENSOR_LUM			= 2 	 ,//亮度传感器
	SENSOR_RAIN       	= 3      ,//雨水传感器
	SENSOR_WIND			= 4	     ,//风速
	SENSOR_CO2          = 5      ,//二氧化碳   
	SENSOR_FUEL_GAS     = 6      ,//可然气体
	SENSOR_AIR_QUALITY  = 7      ,//空气质量
};


//设备类型   如果以下无设备的类型则新增
#define EQUIPMENT_KEY        	  	 0          //代表所有的键盘
#define EQUIPMENT_SWIT       	 	 1          //代表所以的开关设备   
#define EQUIPMENT_KEY_LCD  		 	 0x10       //液晶按键
#define EQUIPMENT_KEY_TFT_LCD   	 0x11       //彩色液晶按键
#define EQUIPMENT_KEY_1          	 0x12       //1键按键
#define EQUIPMENT_KEY_2         	 0x13       //2键按键
#define EQUIPMENT_KEY_3         	 0x14       //3键按键
#define EQUIPMENT_KEY_4         	 0x15       //4键按键
#define EQUIPMENT_KEY_5        		 0x16       //5键按键
#define EQUIPMENT_KEY_6         	 0x17       //6键按键
#define EQUIPMENT_KEY_7              0x18       //7键按键
#define EQUIPMENT_KEY_8         	 0x19       //8键按键 

#define EQUIPMENT_SWIT_4        	 0x20       //4路继电器
#define EQUIPMENT_SWIT_6             0x21       //6路继电器
#define EQUIPMENT_SWIT_8        	 0x22       //8路继电器
#define EQUIPMENT_SWIT_12       	 0x23       //12路继电器

#define EQUIPMENT_SRC_2         	 0x24       //2路可控硅调光
#define EQUIPMENT_SRC_4         	 0x25       //4路可控硅调光
#define EQUIPMENT_SRC_6         	 0x26       //6路可控硅调光
#define EQUIPMENT_SRC_8              0x27       //8路可控硅调光
#define EQUIPMENT_SRC_12        	 0x28       //12路可控硅调光器

#define EQUIPMENT_10V_2         	 0x29       //2路10调光
#define EQUIPMENT_10V_4        		 0x2a       //4路10调光
#define EQUIPMENT_10V_6         	 0x2b       //6路10调光
#define EQUIPMENT_10V_8        		 0x2c       //8路10调光
#define EQUIPMENT_10V_12        	 0x2d       //12路10调光

#define EQUIPMENT_LED_2          	 0x50       //2路LED调光  //因之前没有分配，所以临时插入
#define EQUIPMENT_LED_4        		 0x51       //4路LED调光
#define EQUIPMENT_LED_6        		 0x52       //6路LED调光
#define EQUIPMENT_LED_8              0x53       //8路LED调光
#define EQUIPMENT_LED_12             0x54       //12路LED调光

#define EQUIPMENT_TRAILING_2       	 0x55       //2路前沿调光
#define EQUIPMENT_TRAILING_4         0x56       //4路前沿调光
#define EQUIPMENT_TRAILING_6         0x57       //6路前沿调光
#define EQUIPMENT_TRAILING_8         0x58       //8路前沿调光
#define EQUIPMENT_TRAILING_12        0x59       //12路前沿调光

#define EQUIPMENT_GSM                0x30       //GSM 模块
#define EQUIPMENT_TEL                0x31       //有线电话模块

#define EQUIPMENT_SENSOR             0x32
#define EQUIPMENT_TEMP               0x33       //温度模块
#define EQUIPMENT_BRIGHT             0x34       //亮度模块  

#define EQUIPMENT_PRI_1              0x35       //人体感应1个方向
#define EQUIPMENT_PRI_2              0x36       //人体感应2个方向
#define EQUIPMENT_PRI_3              0x37       //人体感应3个方向
#define EQUIPMENT_PRI_x              0x38       //人体感应 备用

#define EQUIPMENT_WEATHER            0x39        //气象站，功能包括：雨感、温度、温度、亮度、风速
#define EQUIPMENT_CO2                0x3a        //二氧化碳
#define EQUIPMENT_FUEL_GAS           0x3b        //可然气体
#define EQUIPMENT_AIR_QUALITY        0x3c        //空气质量
#define EQUIPMENT_AIR_O2             0x3d        //氧气传感器

#define EQUIPMENT_IR_CEIL	         0x40        //红外线转发器,天花型
#define EQUIPMENT_IR_86              0x41        //红外线转发器,86型

#define EQUIPMENT_POWER_1            0x44        //电表1
#define EQUIPMENT_POWER_2            0x45        //电表2
#define EQUIPMENT_POWER_3            0x46        //电表3  
#define EQUIPMENT_POWER_4            0x47        //电表4

#define EQUIPMENT_SHORT_IN_4         0x71        //短路输入
#define EQUIPMENT_SHORT_IN_8         0x72        //短路输入

#define EQUIPMENT_SHORT_OUT_4        0x73        //短路输出
#define EQUIPMENT_SHORT_OUT_8        0x74        //短路输出

#define EQUIPMENT_DOOR_IN_1          0x75        //门输入1 ─┐  [2015年04月01日]
#define EQUIPMENT_DOOR_IN_2          0x76        //门输入2 ─┼→廖超庭：PC编辑软件有这几个宏定义，但我
#define EQUIPMENT_DOOR_IN_4          0x77        //门输入4 ─┘  手上的头文件没有，于是在我头文件补上。

#define EQUIPMENT_WIRELESS_2G4       0x48        //无线2.4G
#define EQUIPMENT_WIRELESS_315M      0x49        //无线315M
#define EQUIPMENT_WIRELESS_433M      0x4a        //无线433M
#define EQUIPMENT_WIRELESS_ZEEBIG    0x4b        //无线zeebig

#define EQUIPMENT_LOGIC              0x4c        //logic模块

#define EQUIPMENT_HVAC_2CH           0x4d        //空调-2阀门
#define EQUIPMENT_CURTAIN_2CH        0x4e        //窗帘-2路  
#define EQUIPMENT_CURTAIN_3CH        0x4f        //窗帘-3路 

#define EQUIPMENT_AMP_MP3            0x60        //功放 
//#define EQUIPMENT_WINDOWS          0x61        //门窗
#define EQUIPMENT_DOORBELL           0x62        //门铃
#define EQUIPMENT_WINDOWS_2          0x63        //2路门窗
#define EQUIPMENT_WINDOWS_3          0x64        //3路门窗
#define EQUIPMENT_WINDOWS_4          0x65        //4路门窗

#define EQUIPMENT_PLAYER_KEY_7       0x80        //7键播放控制面板

#define EQUIPMENT_ENV_SENSOR_A       0x81        //环境传感器A
#define EQUIPMENT_ENV_SENSOR_B       0x82        //环境传感器B
#define EQUIPMENT_ENV_SENSOR_C       0x83        //环境传感器C
#define EQUIPMENT_ENV_SENSOR_D       0x84        //环境传感器D
#define EQUIPMENT_ENV_SENSOR_E       0x85        //环境传感器E

#define EQUIPMENT_RSP                0x90        //RSP雷达

#define EQUIPMENT_NO_LOCK_DOOR       0xa0        //无锁孔门
#define EQUIPMENT_FP_LOCK            0xa1        //指纹锁
#define EQUIPMENT_EL_KNIFE_FRAME     0xa2        //电动刀具架
#define EQUIPMENT_INTELLIGENT_SINK   0xa3        //智能水槽

#define EQUIPMENT_RFLINE_GATEWAY     0xb0        //无线主机，无线的产品用 0xB0 开始 (廖超庭2017-05-06注释:无线转有线转换器用到)


#define EQUIPMENT_PANEL              0xE0        //通用控制面板

#define EQUIPMENT_GATEWAY            0xf9        //网关
#define EQUIPMENT_RJ45       	     0xf0        //RJ45类型
#define EQUIPMENT_LINKID             0xf1        //联动号(专用<-指令配置)
#define EQUIPMENT_MOBILE             0xfc        //手机
#define EQUIPMENT_SERVER             0xfd        //服务器
#define EQUIPMENT_PC           	     0xfe        //PC类型
#define EQUIPMENT_PUBLIC             0xff        //共公类型






/***********************  以下是控制指令  ****************************  
//高8位表示	指令类型
//对应的回复指令bit7为1
读指令：   		0xh,1xh
写指令:    		8xh,9xh     配对使用，也会单独出现
  
控制指令:  		2xh,        控制开关灯，音量，用户可以感知的指令
状态指令： 		axh,
其他指令： 		3xh,4xh
回复其它指令：  bxh,cxh
**********************************************************************/
enum  //指令分类
{
	CMD_TYPE_PUBLIC     = EQUIPMENT_PUBLIC,            //公共类型
	CMD_TYPE_PC         = EQUIPMENT_PC,                //电脑类型
	CMD_TYPE_SERVER     = EQUIPMENT_SERVER,            //服务器   
	CMD_TYPE_SWITCH     = EQUIPMENT_SWIT,        	   //开关
	CMD_TYPE_KEY        = EQUIPMENT_KEY,           	   //按键
	CMD_TYPE_LOGIC      = EQUIPMENT_LOGIC,             //逻辑
	CMD_TYPE_AC         = EQUIPMENT_HVAC_2CH,          //空调
	CMD_TYPE_CURTAIN    = EQUIPMENT_CURTAIN_2CH,	   //窗帘
	CMD_TYPE_PRI        = EQUIPMENT_PRI_3,	 		   //人体感应器	
	CMD_TYPE_AMP        = EQUIPMENT_AMP_MP3,           //功放  
	CMD_TYPE_WINDOWS    = EQUIPMENT_WINDOWS_2,		   //门窗
	CMD_TYPE_IR         = EQUIPMENT_IR_CEIL,           //红外线
	CMD_TYPE_DOORBELL   = EQUIPMENT_DOORBELL,          //门铃 
	CMD_TYPE_GSM        = EQUIPMENT_GSM,               //GSM网络
	CMD_TYPE_MOBILE     = EQUIPMENT_MOBILE,            //手机
	CMD_TYPE_PANEL      = EQUIPMENT_PANEL,             //通用控制面板
	CMD_TYPE_NO_LOCK    = EQUIPMENT_NO_LOCK_DOOR,      //无锁孔门
	CMD_TYPE_RFLINE     = EQUIPMENT_RFLINE_GATEWAY,    //无线主机
	CMD_TYPE_FP_LOCK    = EQUIPMENT_FP_LOCK   		   //指纹锁

};

enum    //公共指令    _PUBLIC 
{    
    CMD_PUBLIC_NC1              		= ((CMD_TYPE_PUBLIC << 8) | 0x00)     	,//无效指令，上位机不会发送此指令
	CMD_PUBLIC_NC2              		= ((CMD_TYPE_PUBLIC << 8) | 0xff)  	    ,//同上
	CMD_PUBLIC_NULL			   			= ((CMD_TYPE_PUBLIC << 8) | 0x30) 		,//空指令，但上位机会发送此指令，这个用于上位机临时修改用，下位机要保存此数据
	
	CMD_PUBLIC_START_SEARCH	    		= ((CMD_TYPE_PUBLIC << 8) | 0x31) 		,//电脑发搜索开始指令
	CMD_PUBLIC_STOP_SEARCH        		= ((CMD_TYPE_PUBLIC << 8) | 0x32)		,//停止查找             
	CMD_PUBLIC_ROLLCALL           		= ((CMD_TYPE_PUBLIC << 8) | 0x33) 		,//设备点名             
	CMD_PUBLIC_SEARCH_UNROLLCALL  		= ((CMD_TYPE_PUBLIC << 8) | 0x34) 		,//搜索没点名的设备     
	CMD_PUBLIC_ASSIGN_ID          		= ((CMD_TYPE_PUBLIC << 8) | 0x35)		,//修改设备ID           
	CMD_PUBLIC_STOP_READ                = ((CMD_TYPE_PUBLIC << 8) | 0x36)	    ,//停止读取设备信息 	
	CMD_PUBLIC_TEST                     = ((CMD_TYPE_PUBLIC << 8) | 0x37)		,//设备测试指令  
	CMD_PUBLIC_UART_LED_ENABLE         	= ((CMD_TYPE_PUBLIC << 8) | 0x38)		,//开通度灯
	CMD_PUBLIC_UART_LED_DISABLE        	= ((CMD_TYPE_PUBLIC << 8) | 0x39)		,//关通度灯
	CMD_PUBLIC_DISCOVER_ENABLE		    = ((CMD_TYPE_PUBLIC << 8) | 0x3a)		,//发现设备,通信关亮1分钟，或面板的灯闪1分钟方便调试时找到设备
	CMD_PUBLIC_DISCOVER_DISABLE			= ((CMD_TYPE_PUBLIC << 8) | 0x3b)		,//关发现设备
	CMD_PUBLIC_DEL_COMMAND              = ((CMD_TYPE_PUBLIC << 8) | 0x3d)		,//删除指令
	CMD_PUBLIC_STOP                     = ((CMD_TYPE_PUBLIC << 8) | 0x3e)		,//停止读控制信息
	//CMD_PUBLIC_SIMPLE_SWITCH            = ((CMD_TYPE_PUBLIC << 8) | 0x3e)		,//停止读控制信息
	CMD_PUBLIC_RET_START_SEARCH         = ((CMD_TYPE_PUBLIC << 8) | 0x3f)		,//回复搜索
	
	CMD_PUBLIC_RET_READ_INF				= ((CMD_TYPE_PUBLIC << 8) | 0x3f)		,//回复读信息
	CMD_PUBLIC_END_READ_INF				= ((CMD_TYPE_PUBLIC << 8) | 0x40)		,//完成配置信息读取
	
	CMD_PUBLIC_RET_READ_STATE		   = ((CMD_TYPE_PUBLIC << 8) | 0x41)		,//回复读状态

	CMD_PUBLIC_WRITE_END               = ((CMD_TYPE_PUBLIC << 8) | 0x43)		,//读完成
	CMD_PUBLIC_END_READ_STATE          = ((CMD_TYPE_PUBLIC << 8) | 0x44)		,//读状态完成
	CMD_PUBLIC_BROADCAST_PACKET_ID	   = ((CMD_TYPE_PUBLIC << 8) | 0x45)		,//广播包号
	CMD_PUBLIC_APPLY_PACKET			   = ((CMD_TYPE_PUBLIC << 8) | 0x46)		,//补包

	CMD_PUBLIC_ROLLCALL_ONLINE          = ((CMD_TYPE_PUBLIC << 8) | 0x47)       ,//点名在线设备
	CMD_PUBLIC_ONLINE_STATE             = ((CMD_TYPE_PUBLIC << 8) | 0x48)       ,//设备在线情况
	CMD_PUBLIC_ONLINE                   = ((CMD_TYPE_PUBLIC << 8) | 0x49)       ,//设备上线
	
	
	CMD_PUBLIC_READ_TIME                = ((CMD_TYPE_PUBLIC << 8) | 0x01)       ,//读系统时间
	CMD_PUBLIC_WRITE_TIME               = ((CMD_TYPE_PUBLIC << 8) | 0x81)    	,//系统时间          
	
	CMD_PUBLIC_READ_INF                 = ((CMD_TYPE_PUBLIC << 8) | 0x02)  	    ,//读基本信息
	CMD_PUBLIC_WRITE_INF                = ((CMD_TYPE_PUBLIC << 8) | 0x82)  	    ,//写基本信息           	
	CMD_PUBLIC_READ_SET_INF             = ((CMD_TYPE_PUBLIC << 8) | 0x03)   	,//读取设备信息	    
	CMD_PUBLIC_READ_NAME                = ((CMD_TYPE_PUBLIC << 8) | 0x04)		,//读设备名称           
	CMD_PUBLIC_WRITE_NAME               = ((CMD_TYPE_PUBLIC << 8) | 0x84)		,//写设备名称   
	CMD_PUBLIC_READ_LOOP_NAME           = ((CMD_TYPE_PUBLIC << 8) | 0x05)       ,//读回路名称 
	CMD_PUBLIC_WRITE_LOOP_NAME          = ((CMD_TYPE_PUBLIC << 8) | 0x85)       ,//写回路名称	
	CMD_PUBLIC_READ_APPOINT_MACHINE 	= ((CMD_TYPE_PUBLIC << 8) | 0x06)  	    ,//读回指定设备的基本信息，这个是长按状态键，使设备处于返回状态时才有效，按下此按60秒处于有效状态，
	//当返回后3秒才把状态清，因防止数据转发时出错重发。用CMD_WRITE_INF 返回   
	
	CMD_PUBLIC_READ_LOGIC_SEARCH        = ((CMD_TYPE_PUBLIC << 8) | 0x07)		,//逻辑查找指令
	CMD_PUBLIC_WRITE_LOGIC_SEARCH       = ((CMD_TYPE_PUBLIC << 8) | 0x87)		,//返回逻辑查找指令
	CMD_PUBLIC_READ_COMPARE_DATA        = ((CMD_TYPE_PUBLIC << 8) | 0x08)       ,//读查找比较控制指令
	CMD_PUBLIC_WRITE_COMPARE_DATA       = ((CMD_TYPE_PUBLIC << 8) | 0x88)       ,//写查找比较控制指令
    
	CMD_PUBLIC_READ_COMMAND             = ((CMD_TYPE_PUBLIC << 8) | 0x09)	    ,//读出控制指令
	CMD_PUBLIC_WRITE_COMMAND            = ((CMD_TYPE_PUBLIC << 8) | 0x89)       ,//写入控制指令
	CMD_PUBLIC_WRITE_NET_ID             = ((CMD_TYPE_PUBLIC << 8) | 0x8a)		,//修改网络ID
	CMD_PUBLIC_READ_CONFIG              = ((CMD_TYPE_PUBLIC << 8) | 0x0b)       ,//读配置信息
	CMD_PUBLIC_WRITE_CONFIG			    = ((CMD_TYPE_PUBLIC << 8) | 0x8b)		,//写配置信息
	CMD_PUBLIC_READ_STATE				= ((CMD_TYPE_PUBLIC << 8) | 0x0c)		,//读状态信息
	CMD_PUBLIC_WRITE_STATE              = ((CMD_TYPE_PUBLIC << 8) | 0x8c)       ,//写状态	

	CMD_PUBLIC_READ_SWIT_STATE          = ((CMD_TYPE_PUBLIC << 8) | 0x0d)		,//读开关状态 
	CMD_PUBLIC_WRITE_SWIT_STATE         = ((CMD_TYPE_PUBLIC << 8) | 0x8d)		,//写开关状态 
	CMD_PUBLIC_READ_HVAC_STATE          = ((CMD_TYPE_PUBLIC << 8) | 0x0e)		,//读空调状态
	CMD_PUBLIC_WRITE_HVAC_STATE         = ((CMD_TYPE_PUBLIC << 8) | 0x8e)		,//写空调状态
	CMD_PUBLIC_READ_CURTAIN_STATE       = ((CMD_TYPE_PUBLIC << 8) | 0x0f)		,//读窗帘状态
	CMD_PUBLIC_WRITE_CURTAIN_STATE      = ((CMD_TYPE_PUBLIC << 8) | 0x8f)		,//写窗帘状态
	CMD_PUBLIC_READ_SENSOR_STATE        = ((CMD_TYPE_PUBLIC << 8) | 0x10)       ,//读传感器状态
	CMD_PUBLIC_WRITE_SENSOR_STATE       = ((CMD_TYPE_PUBLIC << 8) | 0x90)       ,//写传感器状态
	CMD_PUBLIC_READ_KB_STATE            = ((CMD_TYPE_PUBLIC << 8) | 0x11)       ,//读键盘按状态
	CMD_PUBLIC_WRITE_KB_STATE           = ((CMD_TYPE_PUBLIC << 8) | 0x91)       ,//写键盘按状态
	CMD_PUBLIC_READ_MULTI               = ((CMD_TYPE_PUBLIC << 8) | 0x12)       ,//公共读指令多条返回  
	CMD_PUBLIC_READ_SINGLE              = ((CMD_TYPE_PUBLIC << 8) | 0x13)       ,//公共读指令单条返回
	CMD_PUBLIC_READ_PLACE_NAME			= ((CMD_TYPE_PUBLIC << 8) | 0x14)       ,//读位置名称
	CMD_PUBLIC_WRITE_PLACE_NAME			= ((CMD_TYPE_PUBLIC << 8) | 0x94)       ,//写位置名称
	CMD_PUBLIC_READ_PASSWORD			= ((CMD_TYPE_PUBLIC << 8) | 0x15)       ,//读密码
	CMD_PUBLIC_WRITE_PASSWORD			= ((CMD_TYPE_PUBLIC << 8) | 0x95)       ,//写密码
	
//	CMD_PUBLIC_SWIT_STATE               = ((CMD_TYPE_PUBLIC << 8) | 0xa0)		,//广播开关状态 
//	CMD_PUBLIC_HVAC_STATE               = ((CMD_TYPE_PUBLIC << 8) | 0xa1)		,//广播空调状态
//	CMD_PUBLIC_CURTAIN_STATE            = ((CMD_TYPE_PUBLIC << 8) | 0xa2)		,//广播窗帘状态
//	CMD_PUBLIC_SENSOR_STATE             = ((CMD_TYPE_PUBLIC << 8) | 0xa3)       ,//传感器状态
//	CMD_PUBLIC_KB_STATE                 = ((CMD_TYPE_PUBLIC << 8) | 0xa4)       ,//键盘按状态
	
	CMD_PUBLIC_SIMPLE_SWIT              = ((CMD_TYPE_PUBLIC << 8) | 0x20)       ,//简易开关指令，接着数据0表示关，非0表示调光的数值
	CMD_PUBLIC_SWIT                     = ((CMD_TYPE_PUBLIC << 8) | 0x22)		,//开关指令  ，指令的第一个数据 0表示关，1表示开，2表示取反 ,数据的第三位（不能修改）表示开关对像
    CMD_PUBLIC_SWIT_OPEN				= ((CMD_TYPE_PUBLIC << 8) | 0x23) 		,//开指令     
	CMD_PUBLIC_SWIT_CLOSE               = ((CMD_TYPE_PUBLIC << 8) | 0x24)		,//关指令 
    CMD_PUBLIC_SWIT_OPEN_CONDITION   	= ((CMD_TYPE_PUBLIC << 8) | 0x2b) 		,//开指令,只有参数开时才执行
	CMD_PUBLIC_SWIT_CLOSE_CONDITION     = ((CMD_TYPE_PUBLIC << 8) | 0x2c)		,//关指令,只有参数关时才执行

	
	CMD_PUBLIC_SIMPLE_SWIT_NOT          = ((CMD_TYPE_PUBLIC << 8) | 0x25)       ,//简易取反指令	 ??
	CMD_PUBLIC_SWIT_NOT                 = ((CMD_TYPE_PUBLIC << 8) | 0x25)		,//相反指令		 ??
    CMD_PUBLIC_INC                      = ((CMD_TYPE_PUBLIC << 8) | 0x26)		,//指令加    
    CMD_PUBLIC_DEC                      = ((CMD_TYPE_PUBLIC << 8) | 0x27)		,//指令减    
    CMD_PUBLIC_VAL                      = ((CMD_TYPE_PUBLIC << 8) | 0x28)		,//指定数值，如灯光的亮度，量音的大小
	CMD_PUBLIC_SIMULATE_KEY             = ((CMD_TYPE_PUBLIC << 8) | 0x29)		,//模拟键盘
	CMD_PUBLIC_WINDOWS_PLAY          	= ((CMD_TYPE_PUBLIC << 8) | 0x2a)		,//门窗播放指令

	CMD_PUBLIC_SAFETY_STATE             = ((CMD_TYPE_PUBLIC << 8) | 0xa0)		,//安防状态 

	CMD_PUBLIC_READ_VER                 = ((CMD_TYPE_PUBLIC << 8) | 0xb0)       ,//读设备软硬件版本 
	CMD_PUBLIC_WRITE_VER                = ((CMD_TYPE_PUBLIC << 8) | 0xb1)       ,//写设备软硬件版本 	
	CMD_PUBLIC_RESET_HOST               = ((CMD_TYPE_PUBLIC << 8) | 0xb2)       ,//复位网关与主机连接 
	CMD_PUBLIC_TEST_KEY_CMD             = ((CMD_TYPE_PUBLIC << 8) | 0xb3)       ,//(按键)控制指令测试	
	CMD_PUBLIC_RESET_DEVICE             = ((CMD_TYPE_PUBLIC << 8) | 0xb4)       ,//设备出厂初始指令 (使用本指令要谨慎)

	CMD_PUBLIC_READ_ADDRESS          	= ((CMD_TYPE_PUBLIC << 8) | 0x35)		,//读地址，
	CMD_PUBLIC_WRITE_ADDRESS          	= ((CMD_TYPE_PUBLIC << 8) | 0xb5)		 //写地址，广州市天河区xxxx
};



/****************
//高8位表示	指令类型
//对应的回复指令bit7为1
读指令：   		0xh,1xh
写指令:    		8xh,9xh     配对使用，也会单独出现
  
控制指令:  		2xh,        控制开关灯，音量，用户可以感知的指令
状态指令： 		axh,
其他指令： 		3xh,4xh
回复其它指令：  bxh,cxh
********************/
enum    //服务器    CMD_TYPE_SERVER
{	
	CMD_SERVER_SEARCH                  = ((CMD_TYPE_SERVER << 8) | 0x30)        ,//服务器搜索转换器
    CMD_SERVER_CONNECT                 = ((CMD_TYPE_SERVER << 8) | 0x31)        ,//申请连接
    CMD_SERVER_RET_CONNECT             = ((CMD_TYPE_SERVER << 8) | 0xb2)        ,//回复连接
	CMD_SERVER_HEARTBEAT               = ((CMD_TYPE_SERVER << 8) | 0x33)        ,//心跳帧
	CMD_SERVER_RET_HEARTBEAT           = ((CMD_TYPE_SERVER << 8) | 0xb3)        ,//回复心跳帧

	CMD_SERVER_EMAIL                   = ((CMD_TYPE_SERVER << 8) | 0x20)        ,//email指令
	
	CMD_MMSG_READ_VER                  = ((CMD_TYPE_SERVER << 8) | 0x61)        ,//读设备软硬件版本
	CMD_MMSG_WRITE_VER                 = ((CMD_TYPE_SERVER << 8) | 0xE1)        ,//写设备软硬件版本
	CMD_MMSG_READ_MEMU_NAME            = ((CMD_TYPE_SERVER << 8) | 0x62)        ,//读菜单名称
	CMD_MMSG_WRITE_MEMU_NAME           = ((CMD_TYPE_SERVER << 8) | 0xE2)        ,//写菜单名称
	CMD_MMSG_READ_COMMAND              = ((CMD_TYPE_SERVER << 8) | 0x63)        ,//读控制指令
	CMD_MMSG_WRITE_COMMAND             = ((CMD_TYPE_SERVER << 8) | 0xE3)        ,//写入控制指令
	CMD_MMSG_DEL_COMMAND               = ((CMD_TYPE_SERVER << 8) | 0x64)        ,//删除控制指令
	CMD_MMSG_READ_SECURITY_CFG         = ((CMD_TYPE_SERVER << 8) | 0x65)        ,//读布安防配置
	CMD_MMSG_WRITE_SECURITY_CFG        = ((CMD_TYPE_SERVER << 8) | 0xE5)        ,//写布安防配置
	CMD_MMSG_READ_KEY_CFG              = ((CMD_TYPE_SERVER << 8) | 0x66)        ,//读菜单按键类型配置
	CMD_MMSG_WRITE_KEY_CFG             = ((CMD_TYPE_SERVER << 8) | 0xE6)        ,//写菜单按键类型配置
	CMD_MMSG_READ_BDEV_CFG             = ((CMD_TYPE_SERVER << 8) | 0x67)        ,//读绑定的设备设置
	CMD_MMSG_WRITE_BDEV_CFG            = ((CMD_TYPE_SERVER << 8) | 0xE7)        ,//写绑定的设备设置
};

enum    //手机    EQUIPMENT_MOBILE
{
	CMD_MOBILE_SEARCH                   = ((CMD_TYPE_MOBILE << 8) | 0x30)       ,//搜索网关
	CMD_MOBILE_GWINFORMATION            = ((CMD_TYPE_MOBILE << 8) | 0x31)       ,//上传网关信息
	CMD_MOBILE_CONNECT                  = ((CMD_TYPE_MOBILE << 8) | 0x32)       ,//申请连接
	CMD_MOBILE_CONNECT_RET              = ((CMD_TYPE_MOBILE << 8) | 0xB2)       ,//申请回复
	CMD_MOBILE_HEARTBEAT                = ((CMD_TYPE_MOBILE << 8) | 0x33)       ,//心跳帧
	CMD_MOBILE_DISCONNECT               = ((CMD_TYPE_MOBILE << 8) | 0x34)       ,//断开连接
	CMD_MOBILE_CHANGE_UPASSWORD         = ((CMD_TYPE_MOBILE << 8) | 0x35)       ,//修改用户密码
	CMD_MOBILE_CHANGE_UPASSWORD_RET     = ((CMD_TYPE_MOBILE << 8) | 0xB5)       ,//回应修改
};

enum    //键盘指令  CMD_TYPE_KEY 
{
	CMD_KB_READ_PICTURE_NAME 			= ((CMD_TYPE_KEY << 8) | 0x01)	    	,//写按键显示的图片名称  
	CMD_KB_WRITE_PICTURE_NAME           = ((CMD_TYPE_KEY << 8) | 0x81)	   		,//写按键显示的图片名称  
	CMD_KB_READ_BACK_LIGHT              = ((CMD_TYPE_KEY << 8) | 0x02)      	,//读背光设置  		
	CMD_KB_WRITE_BACK_LIGHT             = ((CMD_TYPE_KEY << 8) | 0x82)       	,//写背光设置  		
	CMD_KB_READ_PASSWORD               	= ((CMD_TYPE_KEY << 8) | 0x03)      	,//读密码设置   	
	CMD_KB_WRITE_PASSWORD          		= ((CMD_TYPE_KEY << 8) | 0x83)       	,//写密码设置   	
	CMD_KB_READ_PASSWORD_PAGE          	= ((CMD_TYPE_KEY << 8) | 0x04)	    	,//读密码页面设置   
	CMD_KB_WRITE_PASSWORD_PAGE      	= ((CMD_TYPE_KEY << 8) | 0x84)	    	,//写密码页面设置   
	CMD_KB_READ_PAGE_DIS          		= ((CMD_TYPE_KEY << 8) | 0x05)			,//读页面显示设置   
	CMD_KB_WRITE_PAGE_DIS	      		= ((CMD_TYPE_KEY << 8) | 0x85)			,//写页面显示设置   
	CMD_KB_READ_IR						= ((CMD_TYPE_KEY << 8) | 0x06)			,//读红外线设置     
	CMD_KB_WRITE_IR                     = ((CMD_TYPE_KEY << 8) | 0x86)			,//写红外线设置     
	CMD_KB_READ_SYS_SETUP_INF           = ((CMD_TYPE_KEY << 8) | 0x07)			,//读系统设置信息  按 指令表1 指令返回   
	CMD_KB_READ_KEY_FUNCTION        	= ((CMD_TYPE_KEY << 8) | 0x08)			,//读按键功能       
	CMD_KB_WRITE_KEY_FUNCTION      		= ((CMD_TYPE_KEY << 8) | 0x88)			,//写按键功能       
	CMD_KB_READ_KEY_ALL_FUNCTION      	= ((CMD_TYPE_KEY << 8) | 0x09)			,//读所有按键功能   
	
	CMD_KB_READ_HVAC_STATE              = ((CMD_TYPE_KEY << 8) | 0x0a)			,//读液晶面板空调状态    
	CMD_KB_WRITE_HVAC_STATE             = ((CMD_TYPE_KEY << 8) | 0x8a)			,//写液晶面板空调状态    
	CMD_KB_READ_HVAC_CONFIG             = ((CMD_TYPE_KEY << 8) | 0x0b)			,//读空调设置            
	CMD_KB_WRITE_HVAC_CONFIG            = ((CMD_TYPE_KEY << 8) | 0x8b)			,//写空调设置
	
    CMD_KB_READ_STARTUP_LIGHT_SET       = ((CMD_TYPE_KEY << 8) | 0x0c)			,//读开机灯光显示设置     0x96  ─┐
    CMD_KB_WRITE_STARTUP_LIGHT_SET      = ((CMD_TYPE_KEY << 8) | 0x8c)			,//写开机灯光显示设置     0x97  ─┴┐
    CMD_KB_READ_STARTUP_KEY_STATE       = ((CMD_TYPE_KEY << 8) | 0x0c)			,//读开机按键显示设置     0x96  ─┬┴→协议文档说是要改，但头文件没有，廖超庭
    CMD_KB_WRITE_STARTUP_KEY_STATE      = ((CMD_TYPE_KEY << 8) | 0x8c)			,//写开机按键显示设置     0x97  ─┘    于2015年04月01日更改，但旧定义宏保留着

    CMD_KB_READ_HVAC_TGTCONFIG          = ((CMD_TYPE_KEY << 8) | 0x0d)			,//读空调相关目标对象设置 0x98
    CMD_KB_WRITE_HVAC_TGTCONFIG         = ((CMD_TYPE_KEY << 8) | 0x8d)			,//写空调相关目标对象设置 0x99

	CMD_KB_READ_OPTIONS                 = ((CMD_TYPE_KEY << 8) | 0x0E)			,//读键盘参数设置
	CMD_KB_WRITE_OPTIONS 				= ((CMD_TYPE_KEY << 8) | 0x8E)			,//写键盘参数设置
	
//    CMD_KB_READ_WINDOWS_MODE            = ((CMD_TYPE_KEY << 8) | 0x0E)		,//读门窗显示模式
//    CMD_KB_WRITE_WINDOWS_MODE           = ((CMD_TYPE_KEY << 8) | 0x8E)		,//写门窗显示模式

//	CMD_KB_READ_SHORTCUT				= ((CMD_TYPE_KEY << 8) | 0x0f)          ,//读快捷键
//    CMD_KB_WRITE_SHORTCUT				= ((CMD_TYPE_KEY << 8) | 0x8f)          ,//写快捷键
//	CMD_KB_READ_OBJECT                	= ((CMD_TYPE_KEY << 8) | 0x10)          ,//读目标
//	CMD_KB_WRITE_OBJECT					= ((CMD_TYPE_KEY << 8) | 0x90)          ,//写目标

	CMD_KB_TEST_KEY                     = ((CMD_TYPE_KEY << 8) | 0x30)			,//测试按键
	CMD_KB_BROAD_CONJUNCTION            = ((CMD_TYPE_KEY << 8) | 0x31)			 //广播关联号状态(使用公共地址、网段、类型、广播) d   用开产 CMD_PUBLIC_SWIT 发送
};  

enum  //继电器，调光器指令  CMD_TYPE_SWITCH
{
	CMD_SW_READ_GROUP_NAME 				= ((CMD_TYPE_SWITCH << 8) | 0x01)           ,//读分组名称    
	CMD_SW_WRITE_GROUP_NAME             = ((CMD_TYPE_SWITCH << 8) | 0x81)           ,//写分组名称     
	CMD_SW_READ_SCENE_NAME              = ((CMD_TYPE_SWITCH << 8) | 0x02) 			,//读场景名称    
	CMD_SW_WRITE_SCENE_NAME             = ((CMD_TYPE_SWITCH << 8) | 0x82) 			,//写场景名称     
	CMD_SW_READ_LIST_NAME               = ((CMD_TYPE_SWITCH << 8) | 0x03) 			,//读时序名称    
	CMD_SW_WRITE_LIST_NAME              = ((CMD_TYPE_SWITCH << 8) | 0x83) 			,//写时序名称     
	CMD_SW_READ_GROUP_INF               = ((CMD_TYPE_SWITCH << 8) | 0x04) 			,//读分组信息    
	CMD_SW_WRITE_GROUP_INF              = ((CMD_TYPE_SWITCH << 8) | 0x84) 			,//写分组信息    
	CMD_SW_READ_SCENE_INF               = ((CMD_TYPE_SWITCH << 8) | 0x05) 			,//读场景信息    
	CMD_SW_WRITE_SCENE_INF              = ((CMD_TYPE_SWITCH << 8) | 0x85)  			,//写场景信息    
	CMD_SW_READ_LIST_INF                = ((CMD_TYPE_SWITCH << 8) | 0x06) 			,//读时序信息    
	CMD_SW_WRITE_LIST_INF               = ((CMD_TYPE_SWITCH << 8) | 0x86) 			,//写时序信息    
	CMD_SW_READ_SWIT_PROCE              = ((CMD_TYPE_SWITCH << 8) | 0x07) 			,//读回路控制过程序参数
	CMD_SW_WRITE_SWIT_PROCE             = ((CMD_TYPE_SWITCH << 8) | 0x87) 			,//写回路控制过程序参数
	CMD_SW_READ_SWIT_VOLTAGE            = ((CMD_TYPE_SWITCH << 8) | 0x08) 			,//读回路电压
	CMD_SW_WRITE_SWIT_VOLTAGE           = ((CMD_TYPE_SWITCH << 8) | 0x88) 			,//写回路电压 	
	CMD_SW_READ_SWIT_CURRENT            = ((CMD_TYPE_SWITCH << 8) | 0x09) 			,//读回路电流
	CMD_SW_WRITE_SWIT_CURRENT           = ((CMD_TYPE_SWITCH << 8) | 0x89) 			,//写回路电流
	CMD_SW_READ_SWIT_POWER              = ((CMD_TYPE_SWITCH << 8) | 0x0a)  			,//读回路功率
	CMD_SW_WRITE_SWIT_POWER             = ((CMD_TYPE_SWITCH << 8) | 0x8a)  			,//写回路功率
	CMD_SW_READ_POWER_ON_RESUME		    = ((CMD_TYPE_SWITCH << 8) | 0x0b)  			,//读回路上电设置状态
	CMD_SW_WRITE_POWER_ON_RESUME		= ((CMD_TYPE_SWITCH << 8) | 0x8b)  			,//写回路上电设置状态

	CMD_SW_SWIT_LOOP		            = ((CMD_TYPE_SWITCH << 8) | 0x20)  			,//回路开关
	CMD_SW_SWIT_LOOP_OPEN	            = ((CMD_TYPE_SWITCH << 8) | 0x21)  			,//回路开
	CMD_SW_SWIT_LOOP_CLOSE	            = ((CMD_TYPE_SWITCH << 8) | 0x22)  			,//回路关
	CMD_SW_SWIT_LOOP_NOT	            = ((CMD_TYPE_SWITCH << 8) | 0x23)  			,//回路取反
	CMD_SW_SWIT_LOOP_OPEN_CONDITION     = ((CMD_TYPE_SWITCH << 8) | 0x30)  			,//回路带条件开
	CMD_SW_SWIT_LOOP_CLOSE_CONDITION    = ((CMD_TYPE_SWITCH << 8) | 0x31)  			,//回路带条件关

	CMD_SW_SWIT_SCENE		            = ((CMD_TYPE_SWITCH << 8) | 0x24)  			,//场景开关
	CMD_SW_SWIT_SCENE_OPEN	            = ((CMD_TYPE_SWITCH << 8) | 0x25)  			,//场景开
	CMD_SW_SWIT_SCENE_CLOSE	            = ((CMD_TYPE_SWITCH << 8) | 0x26)  			,//场景关
	CMD_SW_SWIT_SCENE_NOT	            = ((CMD_TYPE_SWITCH << 8) | 0x27)  			,//场景取反

	CMD_SW_SWIT_LIST		            = ((CMD_TYPE_SWITCH << 8) | 0x28)  			,//时序开关
	CMD_SW_SWIT_LIST_OPEN	            = ((CMD_TYPE_SWITCH << 8) | 0x29)  			,//时序开
	CMD_SW_SWIT_LIST_CLOSE	            = ((CMD_TYPE_SWITCH << 8) | 0x2a)  			,//时序关
	CMD_SW_SWIT_LIST_NOT	            = ((CMD_TYPE_SWITCH << 8) | 0x2b)  			,//时序取反

	CMD_SW_SWIT_ALL		           		= ((CMD_TYPE_SWITCH << 8) | 0x2c)  			,//全部开关
	CMD_SW_SWIT_ALL_OPEN	            = ((CMD_TYPE_SWITCH << 8) | 0x2d)  			,//全部开
	CMD_SW_SWIT_ALL_CLOSE	            = ((CMD_TYPE_SWITCH << 8) | 0x2e)  			,//全部关

	CMD_SW_TEST_LOOP					= ((CMD_TYPE_SWITCH << 8) | 0x2f)  			,//回路测试
};

enum       //PC指令   CMD_TYPE_PC  
{
	CMD_PC_SEARCH                       = ((CMD_TYPE_PC << 8) | 0x01)           ,//1. <PC-RJ45> 搜索相关RJ45设备
	CMD_PC_SEARCH_ACK                   = ((CMD_TYPE_PC << 8) | 0x81)           ,//2. <RJ45-PC> RJ45上报基本信息
	CMD_PC_CHANGEPASSWORD               = ((CMD_TYPE_PC << 8) | 0x02)           ,//3. <PC-RJ45> 修改RJ45密码
	CMD_PC_CHANGEPASSWORD_ACK           = ((CMD_TYPE_PC << 8) | 0x82)           ,//4. <RJ45-PC> 回应修改RJ45密码结果
	CMD_PC_CHANGENET                    = ((CMD_TYPE_PC << 8) | 0x03)           ,//5. <PC-RJ45> 修改RJ45网络参数
	CMD_PC_CHANGENET_ACK                = ((CMD_TYPE_PC << 8) | 0x83)           ,//6. <RJ45-PC> 回应PC修改RJ45网络参数结果
	CMD_PC_CHANGENAME                   = ((CMD_TYPE_PC << 8) | 0x04)           ,//7. <PC-RJ45> 修改RJ45设备名称
	CMD_PC_CHANGENAME_ACK               = ((CMD_TYPE_PC << 8) | 0x84)           ,//8. <RJ45-PC> 回应PC修改RJ45设备名称结果
	CMD_PC_READ_LOCALL_NAME           	= ((CMD_TYPE_PC << 8) | 0x05)           ,//14.<PC-RJ45> 读位置名称
	CMD_PC_WRITE_LOCALL_NAME           	= ((CMD_TYPE_PC << 8) | 0x85)           ,//14.<RJ45-PC> 写位置名称

	CMD_PC_CONNECT                      = ((CMD_TYPE_PC << 8) | 0x30)           ,//9. <PC-RJ45> 申请连接
	CMD_PC_CONNECT_ACK                  = ((CMD_TYPE_PC << 8) | 0xb0)           ,//10.<RJ45-PC> 回应申请
	CMD_PC_CONNECTING                   = ((CMD_TYPE_PC << 8) | 0x31)           ,//11.<PC/RJ45> 刷新连接
	CMD_PC_DISCONNECT                   = ((CMD_TYPE_PC << 8) | 0x32)           ,//12.<PC/RJ45> 断开连接
};


enum  // LOGIC 指令	 EQUIPMENT_LOGIC
{
	CMD_LOGIC_WRITE_TIMER_NAME 			= ((CMD_TYPE_LOGIC << 8) | 0x01)			,//写定时器名称      ┐
	CMD_LOGIC_READ_TIMER_NAME           = ((CMD_TYPE_LOGIC << 8) | 0x81) 			,//读定时器名称      │
	CMD_LOGIC_WRITE_BLOCK_NAME          = ((CMD_TYPE_LOGIC << 8) | 0x02) 			,//写逻辑块名称      │
	CMD_LOGIC_READ_BLOCK_NAME           = ((CMD_TYPE_LOGIC << 8) | 0x82) 			,//读逻辑块名称      │
	CMD_LOGIC_WRITE_BLOCK_PORT_SATE     = ((CMD_TYPE_LOGIC << 8) | 0x03) 			,//写逻辑块端口状态  │
	CMD_LOGIC_READ_BLOCK_PORT_SATE      = ((CMD_TYPE_LOGIC << 8) | 0x83) 			,//读逻辑块端口状态  │
	CMD_LOGIC_WRITE_TIMER_INF           = ((CMD_TYPE_LOGIC << 8) | 0x04) 			,//写定时器设置信息  ├→廖超庭-注释:读写指令数值定义规则不符合标准要求,
	CMD_LOGIC_READ_TIMER_INF            = ((CMD_TYPE_LOGIC << 8) | 0x84) 			,//读定时器设置信息  │     标准要求:[读指令]bit7=0,[写指令]bit7=1!!!!!!
	CMD_LOGIC_WRITE_BLOCK_INF           = ((CMD_TYPE_LOGIC << 8) | 0x05)			,//写逻辑块设置信息  │
	CMD_LOGIC_READ_BLOCK_INF            = ((CMD_TYPE_LOGIC << 8) | 0x85) 			,//读逻辑块设置信息  │
	//                  │
	CMD_LOGIC_WRITE_CMD                 = ((CMD_TYPE_LOGIC << 8) | 0x06) 			,//写控制指令        │
	CMD_LOGIC_READ_CMD                  = ((CMD_TYPE_LOGIC << 8) | 0x86) 			,//读控制指令        ┘
	
	CMD_LOGIC_READ_CONFIG               = ((CMD_TYPE_LOGIC << 8) | 0x41)        ,//读参数设置 (条件与逻辑)
	CMD_LOGIC_WRITE_CONFIG              = ((CMD_TYPE_LOGIC << 8) | 0xC1)        ,//写参数设置 (条件与逻辑)
	CMD_LOGIC_READ_EXACTION             = ((CMD_TYPE_LOGIC << 8) | 0x42)        ,//读逻辑附加动作
	CMD_LOGIC_WRITE_EXACTION            = ((CMD_TYPE_LOGIC << 8) | 0xC2)        ,//写逻辑附加动作   (不同设备各不一样)
	CMD_LOGIC_READ_SECURITY             = ((CMD_TYPE_LOGIC << 8) | 0x44)        ,//读逻辑器安防联动标志配置 (各个逻辑动作可单独联动)
	CMD_LOGIC_WRITE_SECURITY            = ((CMD_TYPE_LOGIC << 8) | 0xC4)        ,//写逻辑器安防联动标志配置 (各个逻辑动作可单独联动)
	
	CMD_LOGIC_WRITE_SYSLKID             = ((CMD_TYPE_LOGIC << 8) | 0xC5)        ,//写逻辑系统联动号-开关 (注意:与[CMD_SW_SWIT_LOOP_OPEN]等指令同一格式)
	CMD_LOGIC_WRITE_SYSLKID_OPEN        = ((CMD_TYPE_LOGIC << 8) | 0xC6)        ,//写逻辑系统联动号-开   (注意:与[CMD_SW_SWIT_LOOP_OPEN]等指令同一格式)
	CMD_LOGIC_WRITE_SYSLKID_CLOSE       = ((CMD_TYPE_LOGIC << 8) | 0xC7)        ,//写逻辑系统联动号-关   (注意:与[CMD_SW_SWIT_LOOP_OPEN]等指令同一格式)
	CMD_LOGIC_WRITE_SLFLKID             = ((CMD_TYPE_LOGIC << 8) | 0xC8)        ,//写逻辑内部联动号-开关 (注意:与[CMD_SW_SWIT_LOOP_OPEN]等指令同一格式)
	CMD_LOGIC_WRITE_SLFLKID_OPEN        = ((CMD_TYPE_LOGIC << 8) | 0xC9)        ,//写逻辑内部联动号-开   (注意:与[CMD_SW_SWIT_LOOP_OPEN]等指令同一格式)
	CMD_LOGIC_WRITE_SLFLKID_CLOSE       = ((CMD_TYPE_LOGIC << 8) | 0xCA)        ,//写逻辑内部联动号-关   (注意:与[CMD_SW_SWIT_LOOP_OPEN]等指令同一格式)
}; 

enum // 空调指令  	CMD_TYPE_AC
{
	CMD_AC_READ_STATE       			= ((CMD_TYPE_AC << 8) | 0x01)			,//读空调状态
	CMD_AC_WRITE_STATE        			= ((CMD_TYPE_AC << 8) | 0x81)  			,//写空调状态
	CMD_AC_READ_CONFIG        			= ((CMD_TYPE_AC << 8) | 0x02)	        ,//读空调设置
	CMD_AC_WRITE_CONFIG       			= ((CMD_TYPE_AC << 8) | 0x82)	        ,//写空调设置
	CMD_AC_READ_FANDC         			= ((CMD_TYPE_AC << 8) | 0x03)   		,//读空调风速控制电压
	CMD_AC_WRITE_FANDC        			= ((CMD_TYPE_AC << 8) | 0x83)       	,//写空调风速控制电压
}; 



enum //门窗控制指令
{
	CMD_WINDOWS_RUN_STATE	           	= ((CMD_TYPE_WINDOWS << 8) | 0x01) 			,//执行当前门窗状态	  	
	CMD_WINDOWS_READ_POWER              = ((CMD_TYPE_WINDOWS << 8) | 0x02)          ,//读当前功率/电流大小
	CMD_WINDOWS_WRITE_POWER             = ((CMD_TYPE_WINDOWS << 8) | 0x03)          ,//写当前功率/电流大小
};

enum //红外线指令
{
//	CMD_IR_SEND_CODE  	           	    = ((CMD_TYPE_IR << 8) | 0x20) 				,//发送红外线码
//	CMD_IR_READ_CODE                    = ((CMD_TYPE_IR << 8) | 0x01) 				,//读红外线码
//	CMD_IR_WRITE_CODE					= ((CMD_TYPE_IR << 8) | 0x81) 				,//写红外线码	

	CMD_IR_SWIT      					= ((CMD_TYPE_IR << 8) | 0x20) 				,//发送红外线
	CMD_IR_STUDY						= ((CMD_TYPE_IR << 8) | 0x21) 				,//红外线学习
	CMD_IR_STUDY_RESULT					= ((CMD_TYPE_IR << 8) | 0xa0) 				,//返回学习结果
	CMD_IR_SEARCH_CODE					= ((CMD_TYPE_IR << 8) | 0x22) 				,//红外线控制指令

	CMD_IR_READ_CODE					= ((CMD_TYPE_IR << 8) | 0x00) 				,//红外线控制指令
	CMD_IR_WRITE_CODE  					= ((CMD_TYPE_IR << 8) | 0x80) 				,//红外线控制指令

	CMD_IR_READ_SINGLE_KEY	 			= ((CMD_TYPE_IR << 8) | 0x01) 				,//红外线控制指令
	CMD_IR_WRITE_KEY				    = ((CMD_TYPE_IR << 8) | 0x81) 				,//红外线控制指令

	CMD_IR_READ_WORK_MODE               = ((CMD_TYPE_IR << 8) | 0x02) 				,//红外线控制指令
	CMD_IR_WRITE_WORK_MODE				= ((CMD_TYPE_IR << 8) | 0x82) 				,//红外线控制指令
};


enum //门铃
{
	CMD_DOORBELL_CONTROL  	           	= ((CMD_TYPE_DOORBELL << 8) | 0x20) 			,//门铃控制	
};



enum //人体感应器指令
{	
//   CMD_PUBLIC_READ_CONFIG  			= ((CMD_TYPE_PRI << 8) | 0x01)  			,//读配置信息
//	 CMD_PUBLIC_WRITE_CONFIG      	    = ((CMD_TYPE_PRI << 8) | 0x81) 				,//写配置信息

//原	CMD_PRI_TEST           				= ((CMD_TYPE_PRI << 8) | 0x02)  		,//测试指令
//原	CMD_PRI_SWIT_PRI        	   		= ((CMD_TYPE_PRI << 8) | 0x03)			,//开关人体感应功能
//原	CMD_PRI_SWIT_SAFETY                 = ((CMD_TYPE_PRI << 8) | 0x04)         	,//开关安防功能
    CMD_PRI_READ_CONFIG                 = ((CMD_TYPE_PRI << 8) | 0x01)  			,//读传感器灵敏参数配置
	CMD_PRI_WRITE_CONFIG           		= ((CMD_TYPE_PRI << 8) | 0x81)  			,//写传感器灵敏参数配置
	CMD_PRI_READ_SAFETY_CONFIG          = ((CMD_TYPE_PRI << 8) | 0x02)				,//读传感器安防配置
	CMD_PRI_WRITE_SAFETY_CONFIG         = ((CMD_TYPE_PRI << 8) | 0x82)         		,//写传感器安防配置
	CMD_PRI_TEST                        = ((CMD_TYPE_PRI << 8) | 0x03)         		,//传感器测试指令
//原有	CMD_PRI_FLASH                       = ((CMD_TYPE_PRI << 8) | 0x04)         	,//传感器感应灯光开关
    CMD_PRI_READ_FLASH_CONFIG           = ((CMD_TYPE_PRI << 8) | 0x04)              ,//读传感器感应指示灯开关
    CMD_PRI_WRITE_FLASH_CONFIG          = ((CMD_TYPE_PRI << 8) | 0x84)              ,//写传感器感应指示灯开关
};


enum //音响指令 ----新添加
{
//  CMD_PUBLIC_READ_STATE                                						    ,//主机：查询状态	 
//	CMD_PUBLIC_WRITE_STATE	                      								    ,//音响：上报背景,音源,音量,高音,低音
//	CMD_PUBLIC_READ_CONFIG														    ,//读mp3配置信息					
//	CMD_PUBLIC_WRITE_CONFIG														    ,//写mp3配置信息					 
	
	CMD_AMP_SLWR_BGM_KEY                = ((CMD_TYPE_AMP << 8) | 0x20)              ,//主机：切换背景音源＋模拟按键控制音源
	CMD_AMP_SLWR_BGM_SONG               = ((CMD_TYPE_AMP << 8) | 0x21)              ,//主机：切换背景音源＋指定音源及曲目播放
	CMD_AMP_SLWR_BGM_VOL_SONG           = ((CMD_TYPE_AMP << 8) | 0x22)              ,//主机：切换背景音源＋音量＋指定音源曲目播放
	CMD_AMP_SLWR_BGM_SRC                = ((CMD_TYPE_AMP << 8) | 0x23)              ,//主机：切换背景音源     
	CMD_AMP_SLWR_BGM_VOL                = ((CMD_TYPE_AMP << 8) | 0x24)              ,//主机：修改背景音量
	CMD_AMP_SLWR_BGM_TRE                = ((CMD_TYPE_AMP << 8) | 0x25)              ,//主机：修改背景高音
	CMD_AMP_SLWR_BGM_BAS                = ((CMD_TYPE_AMP << 8) | 0x26)              ,//主机：修改背景低音
	CMD_AMP_SLWR_BGM_TUNE               = ((CMD_TYPE_AMP << 8) | 0x27)              ,//主机：修改背景音量,高音,低音
	CMD_AMP_SLWR_MSN_TUNE               = ((CMD_TYPE_AMP << 8) | 0x28)              ,//主机：修改紧急音量,高音,低音
	
	CMD_AMP_SLWR_PPEMC                  = ((CMD_TYPE_AMP << 8) | 0x2a)              ,//主机：指定紧急消息播放模式
	CMD_AMP_SLWR_OUTMSN                 = ((CMD_TYPE_AMP << 8) | 0x2b)              ,//音响：退出消息播放，连续广播3次
	CMD_AMP_SLWR_BGM_PLAYMODE           = ((CMD_TYPE_AMP << 8) | 0x2c)              ,//播放模式  
	CMD_AMP_SLWR_BGM_RADIO_NOHZ         = ((CMD_TYPE_AMP << 8) | 0x2d)              ,//保存指令频率到指令的电台
	
	CMD_AMP_WIFI_SET                    = ((CMD_TYPE_AMP << 8) | 0x2E)              ,//选择WIFI网络和设置连接密码(廖超庭 增加:2015-10-15)
};

enum //GSM模块
{	
//   CMD_PUBLIC_READ_CONFIG  			= ((CMD_TYPE_PRI << 8) | 0x01)  			,//读配置信息
//	 CMD_PUBLIC_WRITE_CONFIG      	    = ((CMD_TYPE_PRI << 8) | 0x81) 				,//写配置信息
//	 CMD_PUBLIC_SIMPLE_SWIT             = ((CMD_TYPE_PUBLIC << 8) | 0x20)           ,//简易开关指令，接着数据0表示关，非0表示调光的数值
//	 CMD_PUBLIC_SWIT                    = ((CMD_TYPE_PUBLIC << 8) | 0x22)		    ,//开关指令  ，指令的第一个数据 0表示关，1表示开，2表示取反 ,数据的第三位（不能修改）表示开关对像
//   CMD_PUBLIC_SWIT_OPEN				= ((CMD_TYPE_PUBLIC << 8) | 0x23) 		    ,//开指令     
//	 CMD_PUBLIC_SWIT_CLOSE              = ((CMD_TYPE_PUBLIC << 8) | 0x24)		    ,//关指令 
//   CMD_PUBLIC_SWIT_OPEN_CONDITION   	= ((CMD_TYPE_PUBLIC << 8) | 0x2b) 		    ,//开指令,只有参数开时才执行
//	 CMD_PUBLIC_SWIT_CLOSE_CONDITION    = ((CMD_TYPE_PUBLIC << 8) | 0x2c)		    ,//关指令,只有参数关时才执行

	CMD_GSM_POST_CONST_MESSAGE   		= ((CMD_TYPE_GSM << 8) | 0x01)  			,//发送固化短信
	CMD_GSM_POST_CUSTOM_MESSAGE   		= ((CMD_TYPE_GSM << 8) | 0x02)  			,//发送自定义短信
	CMD_GSM_READ_SAFE_MESSAGE   		= ((CMD_TYPE_GSM << 8) | 0x03)  			,//读安防发送固化短信
	CMD_GSM_WRITE_SAFE_MESSAGE   		= ((CMD_TYPE_GSM << 8) | 0x04)  			,//写安防发送固化短信
};


enum //无锁孔门
{	
	CMD_NOLOCK_EXIT_SETADDR 		= ((CMD_TYPE_NO_LOCK << 8) | 0x01)				,//结束设置地址
	CMD_NOLOCK_WRITE_RCPACKET  		= ((CMD_TYPE_NO_LOCK << 8) | 0x02)				,//写摇控数据包  
	CMD_NOLOCK_READ_RCPACKET        = ((CMD_TYPE_NO_LOCK << 8) | 0x82)  			,//读摇控数据包
	CMD_NOLOCK_WRITE_BTPACKET		= ((CMD_TYPE_NO_LOCK << 8) | 0x3)				,//写摇控数据包
	CMD_NOLOCK_READ_BTPACKET		= ((CMD_TYPE_NO_LOCK << 8) | 0x83)				,//读摇控数据包
	CMD_NOLOCK_WRITE_NUM			= ((CMD_TYPE_NO_LOCK << 8) | 0x4)				,//写无锁孔门数量 
	CMD_NOLOCK_READ_NUM             = ((CMD_TYPE_NO_LOCK << 8) | 0x84)				,//读无锁孔门数量
    
};


enum // 无线主机  	CMD_TYPE_RFLINE
{
	CMD_RFLINE_READ_DEV_LIST       		= ((CMD_TYPE_RFLINE << 8) | 0x01)			,//读无线设备列表
	CMD_RFLINE_WRITE_DEV_LIST   		= ((CMD_TYPE_RFLINE << 8) | 0x81)  			,//写无线设备列表
	CMD_RFLINE_WRITE_DEVAC        		= ((CMD_TYPE_RFLINE << 8) | 0x82)	        ,//增加或删除设备
	CMD_RFLINE_WRITE_DEVAC_RSL          = ((CMD_TYPE_RFLINE << 8) | 0x83)	        ,//增加或删除设备结果
}; 


enum // 指纹锁 	  CMD_TYPE_FP_LOCK
{
	CMD_PF_LOCK_WRITE_STATE             = ((CMD_TYPE_FP_LOCK << 8) | 0x81)          ,//写指纹锁状态
	CMD_PF_LOCK_WRITE_PASSWORD          = ((CMD_TYPE_FP_LOCK << 8) | 0x82)          ,//写指纹锁密码
	CMD_RFLINE_READ_CFG2                = ((CMD_TYPE_FP_LOCK << 8) | 0x03)          ,//读指纹锁开锁屏蔽标志指令
	CMD_RFLINE_WRITE_CFG2               = ((CMD_TYPE_FP_LOCK << 8) | 0x83)          ,//写指纹锁开锁屏蔽标志指令
	CMD_RFLINE_READ_CFG                 = ((CMD_TYPE_FP_LOCK << 8) | 0x04)          ,//读专用配置
	CMD_RFLINE_WRITE_CFG                = ((CMD_TYPE_FP_LOCK << 8) | 0x84)          ,//写专用配置
}; 




#pragma pack(push) //(push)与(pop)要配对, 可以嵌套
#pragma pack(1)    //4->4字节对齐, 其可 1,2,4,8,16



/***********************  以下是键盘用的参数 ******************************/
enum //现在改为高4位指出功能按键的状态，低4位指出方向键状态
{
	KEY_TYPE_NULL        = 0		,//按键无效	       0
	KEY_TYPE_HIT            		,//按下有效	       1
	KEY_TYPE_LOOSEN	       		    ,//松开有效	       2
	KEY_TYPE_SHORT          		,//短按有效 (1S)   3
	KEY_TYPE_LONG	       		    ,//长按有效 (3S)   4
	KEY_TYPE_DBLCLICK       		,//双击有效	       5
	KEY_TYPE_SERIAL         		,//连续按键有效    6
	KEY_TYPE_LAMP                   ,//灯光类型	       7
    KEY_TYPE_PRESS                   //点动，按下开，松开关  8
};    

//指令类型
enum
{ 
    //以下指令控制单一个对像
    KEY_CMD_TYPE_SIMPLE_CHOSE   	= 0x80,      //单对像指令，状态选择，数值无效
	KEY_CMD_TYPE_SIMPLE_CHOOSE_INC_DEC,          //单对像指令，状态选择，数值有效
	KEY_CMD_TYPE_SIMPLE_CHOOSE_VALUE_INC_DEC,    //单对像指令，状态选择，数值选择
	KEY_CMD_TYPE_SIMPLE_DIR,                     //单对像指令，状态直发，数值无效
	KEY_CMD_TYPE_SIMPLE_CHOOSE_CMD,              //单对像指令，状态直发，数值无效
	KEY_CMD_TYPE_SIMPLE_LR,                      //单对像指令，状态无效，数值有效
	
	//多对像
	KEY_CMD_TYPE_CHOOSE   		     = 0x00,     //多个控制对像，状态选择，数值无效--如灯光是开关，如果是音源则是选择DVD，CD，MP3等
	KEY_CMD_TYPE_CHOOSE_INC_DEC,        		 //多个控制对像，状态选择，数值有效，但不指出数值的大小，而是指出数值是增加还是减少或者无变化--如窗帘控制，指出窗口开或关，同时是向上拉还是向下拉
	KEY_CMD_TYPE_CHOOSE_VALUE_INC_DEC,  		 //多个控制对像，状态选择，数值选择--如带亮度的灯光，指出灯光开或关，同时指出亮度 
	KEY_CMD_TYPE_DIR,                   		 //多个控制对像，状态直发，数值无效--按一次发送一次，不改变指令中的状态
	KEY_CMD_TYPE_CHOOSE_CMD,            		 //多个控制对像，状态直发，数值无效--根据状态值，发送对应状态值的指令，如有20条指令，当前状态值为18，则发送第18条指令（指令为0~19）	        
	KEY_CMD_TYPE_LR,                  			 //多个控制对像，状态无效，数值有效--不改变指令中的数值，数值只有0和1，对应方向增加键和方向减少键，0发送前一半指令，1发送后一半指令        
};    


/*************************************************************************/

/************************** 红外线按键 ***********************************/

typedef enum				//红外线类型
{
	IR_AC = 1,				//空调
	IR_TV,					//电视
	IR_SDVB,				//机顶盒
	IR_BLURAY,				//蓝光机
	IR_HIFI,				//音响
	IR_MAX,					
}IR_TYPE_ENUM;





/*机顶盒*/
typedef enum/*不可改变此枚举的顺序和值*/
{
	KEY_SDVB_MUTE = 1,         //静音键             	
	KEY_SDVB_POWER,	           //电源键             	
	KEY_SDVB_MUNE,	           //菜单键             	
	KEY_SDVB_UP,	           //上键               	
	KEY_SDVB_RETURN,	       //返回              	
	KEY_SDVB_LEFT,	           //左键               	
	KEY_SDVB_OK,	           //OK 键              	
	KEY_SDVB_RIGHT,	           //右键               	
	KEY_SDVB_jiemubiao,        //节目表                	
	KEY_SDVB_DOWN,	           //下键               	
	KEY_SDVB_EXIT,	           //退出               	
	KEY_SDVB_VOLUP,	           //音量加键           	
	KEY_SDVB_shengdao,         //声道               	
	KEY_SDVB_CHUP,	           //频道加键           	
	KEY_SDVB_VOLDN,	           //音量减键           	
	KEY_SDVB_zixun,	           //资讯              	
	KEY_SDVB_CHDN,	           //频道减键           	
	KEY_SDVB_PGUP,	           //上翻页                	
	KEY_SDVB_PGDN,	           //下翻页  
	KEY_SDVB_RED,	           //红 
	KEY_SDVB_GREEN,	           //绿 
	KEY_SDVB_YELLOW,           //黄 
	KEY_SDVB_BLUE,	           //蓝             	              	
	KEY_SDVB_1,	               //数字键1            	
	KEY_SDVB_2,	               //数字键2            	
	KEY_SDVB_3,	               //数字键3            	
	KEY_SDVB_4,	               //数字键4            	
	KEY_SDVB_5,	               //数字键5            	
	KEY_SDVB_6,	               //数字键6            	
	KEY_SDVB_7,	               //数字键7            	
	KEY_SDVB_8,	               //数字键8            	
	KEY_SDVB_9,	               //数字键9            	
	KEY_SDVB_favor,	           //喜爱                  	
	KEY_SDVB_0,	               //数字键0
	KEY_SDVB_INFO,	           //信息	  
	KEY_SDVB_TV_POWER,         //电视开  
	KEY_SDVB_TV_VOLUP,		   //电视声音加
	KEY_SDVB_TV_VOLDN,		   //电视声音减
	KEY_SDVB_TV_AVTV,          //电视AVTV
	KEY_SDVB_MAX,
}KEY_SDVB_ENUM;


/*  电视 */
typedef enum/*不可改变此枚举的顺序和值*/
{
	KEY_TV_MUTE = 1,               //静音键             	
	KEY_TV_POWER,	               //电源键             	
	KEY_TV_MUNE,	               //菜单键             	
	KEY_TV_UP,	                   //上键               	
	KEY_TV_AVTV,	               //AV/TV              	
	KEY_TV_LEFT,	               //左键               	
	KEY_TV_OK,	                   //OK 键              	
	KEY_TV_RIGHT,	               //右键               	
	KEY_TV_pingxian,               //屏显               	
	KEY_TV_DOWN,	               //下键               	
	KEY_TV_wangfan,                //往返               	
	KEY_TV_VOLUP,	               //音量加键           	
	KEY_TV_zhishi,                 //制式             	
	KEY_TV_CHUP,	               //频道加键           	
	KEY_TV_VOLDN,	               //音量减键           	
	KEY_TV_zhengchang,             //正常            	
	KEY_TV_CHDN,	               //频道减键           	
	KEY_TV_liyin,	               //丽音                	
	KEY_TV_banyin,	               //伴音  
	KEY_TV_PIP,	                   //画中画
	KEY_TV_SLEEP,	               //睡眠          	              	
	KEY_TV_1,	       		       //数字键1            	
	KEY_TV_2,	      		       //数字键2            	
	KEY_TV_3,	                   //数字键3            	
	KEY_TV_4,	                   //数字键4            	
	KEY_TV_5,	                   //数字键5            	
	KEY_TV_6,	       		       //数字键6            	
	KEY_TV_7,	      		       //数字键7            	
	KEY_TV_8,	       		       //数字键8            	
	KEY_TV_9,	      		       //数字键9            	
	KEY_TV_qiehuan,                //-/--               	
	KEY_TV_0,	                   //数字键0            	
	KEY_TV_bili,	               //16:09
	KEY_TV_MAX,
}KEY_TV_ENUM;


/*  音响 */
typedef enum/*不可改变此枚举的顺序和值*/
{
	KEY_HIFI_MUTE = 1,             //静音键             	
	KEY_HIFI_POWER,	               //电源键   
	KEY_HIFI_OPENCLOSE,            //进出仓          	
	KEY_HIFI_MUNE,	               //菜单键             	
	KEY_HIFI_UP,	               //上键               	
	KEY_HIFI_LEFT,	               //左键               	
	KEY_HIFI_OK,	               //OK 键              	
	KEY_HIFI_RIGHT,	               //右键               		
	KEY_HIFI_DOWN,	               //下键               		
	KEY_HIFI_RETURN,               //返回
	KEY_HIFI_VOLUP,	               //音量加键
	KEY_HIFI_VOLDN,	               //音量减键	
	KEY_HIFI_PRE,  			       //上一曲	 
	KEY_HIFI_NEXT,                 //下一曲
	KEY_HIFI_PLAYPAUSE,	           //播放暂停
	KEY_HIFI_STOP,	               //停止 
	KEY_HIFI_BASSUP,	           //低音+
	KEY_HIFI_BASSDN,	           //低音-
	KEY_HIFI_TREUP,	         	   //高音+  
	KEY_HIFI_TREDN,	               //高音-
	KEY_HIFI_1,	       		       //数字键1            	
	KEY_HIFI_2,	      		       //数字键2            	
	KEY_HIFI_3,	                   //数字键3            	
	KEY_HIFI_4,	                   //数字键4            	
	KEY_HIFI_5,	                   //数字键5            	
	KEY_HIFI_6,	       		       //数字键6            	
	KEY_HIFI_7,	      		       //数字键7            	
	KEY_HIFI_8,	       		       //数字键8            	
	KEY_HIFI_9,	      		       //数字键9            	
	KEY_HIFI_qiehuan,              //-/--               	
	KEY_HIFI_0,	                   //数字键0            	
	KEY_HIFI_kuohao,	           //(-)
	KEY_HIFI_MAX,
}KEY_HIFI_ENUM;



/*  蓝光机 */
typedef enum/*不可改变此枚举的顺序和值*/
{
	KEY_BLURAY_MUTE = 1,             //静音键
	KEY_BLURAY_POWER,	             //电源键
	KEY_BLURAY_OPENCLOSE,            //进出仓
	KEY_BLURAY_MUNE,	             //菜单键
	KEY_BLURAY_UP,	                 //上键               	
	KEY_BLURAY_LEFT,	             //左键               	
	KEY_BLURAY_OK,	                 //OK 键              	
	KEY_BLURAY_RIGHT,	             //右键               		
	KEY_BLURAY_DOWN,	             //下键               		
	KEY_BLURAY_RETURN,               //返回
	KEY_BLURAY_SHORTCUTMUNE,         //快捷菜单
	KEY_BLURAY_TITLE,				 //标题
	KEY_BLURAY_VOLUP,	             //音量加键
	KEY_BLURAY_VOLDN,	             //音量减键	
	KEY_BLURAY_PRE,  			     //上一曲	 
	KEY_BLURAY_NEXT,                 //下一曲
	EKY_BLURAY_FF, 					 //块进
	EKY_BLURAY_REW,					 //快退
	KEY_BLURAY_PLAYPAUSE,	         //播放暂停
	KEY_BLURAY_STOP,	             //停止 
	KEY_BLURAY_RED,	                 //红 
	KEY_BLURAY_GREEN,	             //绿 
	KEY_BLURAY_YELLOW,               //黄 
	KEY_BLURAY_BLUE,	             //蓝 
	KEY_BLURAY_1,	       		     //数字键1            	
	KEY_BLURAY_2,	      		     //数字键2            	
	KEY_BLURAY_3,	                 //数字键3            	
	KEY_BLURAY_4,	                 //数字键4            	
	KEY_BLURAY_5,	                 //数字键5            	
	KEY_BLURAY_6,	       		     //数字键6            	
	KEY_BLURAY_7,	      		     //数字键7            	
	KEY_BLURAY_8,	       		     //数字键8            	
	KEY_BLURAY_9,	      		     //数字键9            	
	KEY_BLURAY_qiehuan,              //-/--               	
	KEY_BLURAY_0,	                 //数字键0            	
	KEY_BLURAY_kuohao,	             //(-)
	KEY_BLURAY_TV_POWER,			 //电视电源
	KEY_BLURAY_TV_VOLUP,			 //电视音量加
	KEY_BLURAY_TV_VOLDN,			 //电视音量减
	KEY_RLURAY_TV_AVTV, 			 //电视AVTVG模式
	KEY_BLURAY_MAX,
}KEY_BLURAY_ENUM;




/*************************************************************************/



#define CMD_CLOSE                     0				 //状态关
#define CMD_OPEN                      1				 //状态开
#define CMD_NOT                       2              //状态取反

//CMD_PUBLIC_SWIT   类型
#define CMD_SWIT_TYPE_LAMP_LOOP       0           //灯光回路  
#define CMD_SWIT_TYPE_LAMP_SCENE      1           //灯光场景  
#define CMD_SWIT_TYPE_LAMP_LIST       2           //灯光时序  
#define CMD_SWIT_TYPE_AC		      3           //空调  
#define CMD_SWIT_TYPE_HIFI		      4           //音响

//测试类型 
#define CMD_TEST_TYPE_KEYBOARD        0           //键盘控制类
#define CMD_TEST_TYPE_SWIT            1           //开关类


//返回数据 状态
#define CMD_TRUE                                 0x55     //正确
#define CMD_FALSE                                0xCC     //错误
#define CMD_UNSAME                               0x11     //不相符
#define CMD_BUSY                                 0x22     //忙 
#define CMD_ERR                                  0x33     //包号出错 

//CMD_PUBLIC_WRITE_COMMAMD  指令类型分类  
#define COMMAMD_MESSAGE                          0x00     //短信
#define COMMAND_VOICE                            0x01     //声音 
#define COMMAND_TIMER                            0x03     //定时器
#define COMMAND_LOGIC                            0x04     //逻辑
#define COMMAND_KEY                              0x05     //键盘
#define COMMAND_CURTAIN                          0x06     //窗帘



#define SCENE_ALL                          		 0xff     //场景全关
#define SCENE_MAX 								 0x08     //最多场景
#define LIST_MAX								 0x02     //最多时序
#define GROUP_MAX                                0x04     //最大分组


//LOGIC 逻辑器 定时器循环状态
/**********  循环模式 *****************/
#define REPEAT_MODE_NC          0x00      //不循环    
#define REPEAT_MODE_YEAR        0x01      //年循环
#define REPEAT_MODE_MONTH       0x02      //月循环
#define REPEAT_MODE_DAY         0x03      //日循环
#define REPEAT_MODE_WEEK        0x04      //星期循环

// 继电器，调光，日光灯等开关初始状态
#define INI_STATE_CLOSE          0x00     //关 
#define INI_STATE_OPEN		     0x01     //开
#define INI_STATE_LAST           0x02	  //最后断电状态


//安防参数
#define SAFETY_GRADE_OUTSIDE      0x01    //安防级别室外  bit0~bit6
#define SAFETY_GRADE_INSIDE       0x02    //安防级别室内  bit0~bit6
#define SAFETY_GRADE_ALL       	  0x7f    //安防级别全部  bit0~bit6
#define SAFETY_ALARM           	  0x80    //警报触发      bit7
#define SAFETY_NC                 0       //撤防 

//门窗显示模式
#define WINDOWS_DISPLAY_SEQ       0x80	  //第一页显示锁门  bit7 
#define WINDOWS_DISPLAY_ALL       0       //显示全部门窗状态
#define WINDOWS_DISPLAY_UNLOCK    1       //只显示关门和锁门的状态

//门窗执行指令				    
#define WINDOWS_STATE_OPEN 		  0       //发门开指令集
#define WINDOWS_STATE_CLOSE 	  1		  //发门关指令集
#define WINDOWS_STATE_UNLOCK	  2		  //发门没上锁指令集
#define WINDOWS_STATE_LOCK  	  3		  







//发门上锁指令集

//短路触发工作模式
#define SHORT_IN_MODE_NORMAL      0		  //短路输入，普通模式
#define SHORT_IN_MODE_WINDOWS 	  1		  //短路输入，门窗模式


//门铃控制参数
#define DOORBELL_KEY              0       //门铃按键按下
#define DOORBELL_OPEN             1       //开锁   
#define DOORBELL_ANSWER			  2 	  //应答
#define DOORBELL_END			  3		  //结束


//音响指令	 在amp.h已有
//#define BGM                       0      //音响类别 背景
//#define MESSAGE  				    1	   //音响类别 信息
//
//#define CTRLP_BGMST_MP3           0      //背景音乐 mp3 
//#define CTRLP_BGMST_RADIO		    1	   //背景音乐 RADIO 
//#define CTRLP_BGMST_AUX1		    2	   //背景音乐 AUX1
//#define CTRLP_BGMST_AUX2 		    3	   //背景音乐 AUX2
//
//#define CTRLP_MSSAGEST_EMC        4      //信息--EMC
//#define CTRLP_MSSAGEST_WINDOWS    5      //信息--门窗
//
//#define CTRLP_PMD_PLY_ONE     	0		 //背景播放模式 单曲播放
//#define CTRLP_PMD_REP_ONE     	1		 //背景播放模式 单曲循环
//#define CTRLP_PMD_PLY_ALL         2		 //背景播放模式 顺序播放
//#define CTRLP_PMD_REP_ALL     	3		 //背景播放模式 循环播放
//#define CTRLP_PMD_SHUFFLE     	4		 //背景播放模式 随机播放
//
//#define CTRLP_PMD_SIMPLE_COUNT    5      //信息播放模式 单曲计次
//#define CTRLP_PMD_SIMPLE_TIME     6      //信息播放模式 单曲计时
//#define CTRLP_PMD_MULTI_COUNT     7      //信息播放模式 多曲计次
//#define CTRLP_PMD_MULTI_TIME      8      //信息播放模式 多曲计时
//




//通信数据格式
typedef struct _COMMUNICATION_FORMAT
{
	unsigned char  TargetId;     		//目标ID	0
	unsigned char  TargetNet;			//目标网段	1
	unsigned char  TargetType;          //目标类型	2
	unsigned char  SourceId;            //源ID		3
	unsigned char  SourceNet;           //源网段 	4
	unsigned char  SourceType;          //源类型	5
	unsigned char  Page;                //页		6
	unsigned short Cmd;                 //控制字  由8位改为16位。  7
	unsigned char  Len;                 //长度					   9
	unsigned char  Data[128];           //数据最长128个(特别注释:不包括CRC校验4字节)
}COMMUNICATION_FORMAT;


//目期格式
typedef struct _TIME_FORMAT
{
	unsigned char Year;                 //年
	unsigned char Month;                //月
	unsigned char Day;                  //日 
	unsigned char Week;                 //星期 
	unsigned char Hour;                 //时    
	unsigned char Min;                  //分
	unsigned char Sec;                  //秒  
}TIME_FORMAT;

//设备名称格式
typedef struct _EQUIP_INF_FORMAT
{
	unsigned char  Id;
	unsigned char  Net;
	unsigned char  Type;					
	unsigned char  Mac[12];              //12位mac地址
	unsigned short Location;             //位置 位置分为16位，高8位表示楼层，低8位表示房间号
	unsigned char  Name[30];             //回路名称，最长30字节
}EQUIP_INF_FORMAT;

//设备名称格式
typedef struct _LOOP_NAME_FORMAT
{
	unsigned short Location;             //位置 位置分为16位，高8位表示楼层，低8位表示房间号
	unsigned char  Photo;                //图片号
	unsigned char  Name[30];             //回路名称，最长30字节
}LOOP_NAME_FORMAT;


//按键操设置的参数
typedef struct _KEY_CONFIG
{
	unsigned char  KeyType;              //按键操作类型,低4位表示方向键，高4位表示功能键,按下有效，松开有效等      见KEY_TYPE
	unsigned char  CmdType;              //指令类型(相应的控制)， 直发，加减等	 见CMD_TYPE
	unsigned char  EquipType;            //设备类型 		         --  保留
	
	unsigned char  FuncKeyData;	         //功能键相应的数据  这个是以前旧黑白液晶，键为双键时，同时按下的功能
	unsigned char  FuncKeyDataStep;      //功能键数据增加值
	unsigned char  FuncKeyDataMin;       //功能键数据最小值
	unsigned char  FuncKeyDataMax;       //功能键数据最大值
	
	unsigned char  DirecKeyData;	     //方向键相应的数据  初值,
	unsigned char  DirecKeyDataStep;     //步进            
	unsigned char  DirecKeyDataMin;      //方向键数据最小值 
	unsigned char  DirecKeyDataMax;      //方向键数据最大值
	
	unsigned char  Conjunction;          //关联号，所有按键中关联号相同的，则控制同一对象
	unsigned char  Mutex;                //互斥键，0表示没互斥，1~255表示互斥号, 同一个页面中，相同互斥的按键控制同一类型的对象，某个键按下，另外的按键就要弹起	
}KEY_CONFIG;


//控制指定存储格式
typedef struct _CMD_FORMAT
{
	unsigned char  TargetId;             //目标ID
	unsigned char  TargetNet;            //目标网段
	unsigned char  TargetType;           //目标类型
	
	unsigned short Cmd;			         //控制字
	unsigned char  Len;      			 //长度
	unsigned char  Data[30];      		 //数据，最长30字节
}CMD_FORMAT;


//灯光回路控制数据
typedef struct _CMD_LOOP_FORMAT
{
	unsigned char  Switch; 		         //开关
	unsigned char  Dimmer;        	     //亮度
	
	unsigned char   Loop;		         //回路
	unsigned short  RunTime;  			 //运行时间
	unsigned short  OpenDelayTime;       //开延迟
	unsigned short  CloseDelayTime;      //关延迟   
}CMD_LOOP_FORMAT;


//灯光场景控制数据
typedef struct _CMD_SENSE_FORMAT
{
	unsigned char  Switch; 		         //开关
	unsigned char  Nc;        	     	 //保留
	
	unsigned char  Group;		         //组
	unsigned char  Scene;  			 	 //第个几场景
	unsigned short RunTime;    			 //运行时间	
}CMD_SCENE_FORMAT;


//灯光全部控制数据
typedef struct 
{
	unsigned char  Switch; 		         //开关
	unsigned char  Nc;        	     	 //保留
	
	unsigned char  RunTime;		         //运行时间
}CMD_SWIT_ALL_FORMAT;



//灯光时序控制数据
typedef struct _CMD_LIST_FORMAT
{
	unsigned char  Switch; 		         //开关
	unsigned char  Nc;        	     	 //保留
	
	unsigned char  Group;		         //组
	unsigned char  List;  			 	 //第几个
	unsigned short Times;    			 //运行次数
}CMD_LIST_FORMAT;



//场景配置信息
typedef struct _CMD_SENSE_LOOP_INF
{
	unsigned char  Loop; 		         //回路
	unsigned char  Dimmer;        	     //亮度
}CMD_SCENE_LOOP_INF;


//场景配置信息
typedef struct _CMD_SENSE_INF
{
	unsigned char  Group; 		         //组
	unsigned char  Scene;        	     //场景
	CMD_SCENE_LOOP_INF Data[SCENE_MAX];	 	 //回路的数据
}CMD_SCENE_INF;


//场景配置信息
typedef struct _CMD_LIST_SENSE_INF
{
	unsigned char   Scene; 		         //场景
	unsigned short  Time;        	     //时间
}CMD_LIST_SCENE_INF;

//时序配置信息
typedef struct _CMD_LIST_INF
{
	unsigned char   Group; 		         //组
	unsigned char   List;        	     //时序
	unsigned char   Logic; 				 //逻辑关系
//	unsigned short  Times;               //次数	
	CMD_LIST_SCENE_INF Data[SCENE_MAX];	 	 //回路的数据
}CMD_LIST_INF;



//开关过程序信息
typedef struct _CMD_PROCESS_LOOP_INF
{
	unsigned char  Type;        	     //类型
	unsigned char  OpenProcessTime; 	 //开过程时间
	unsigned char  CloseProcessTime;     //关过程时间
	unsigned char  DimmerMax;			 //最大值
	unsigned char  DimmerMin;            //最小值
	unsigned char  Line;                 //曲线 
}CMD_PROCESS_LOOP_INF; 


//公共读指令
typedef struct _CMD_PUBLIC_READ_DATA_FORMAT
{
	unsigned short  Cmd;     			 //指令
	unsigned char   Data[128];           //数据最长128个
}CMD_PUBLIC_READ_DATA_FORMAT;



//开关过程序信息
typedef struct _CMD_PROCESS_INF
{
	CMD_PROCESS_LOOP_INF Loop[12];		 //回路
}CMD_PROCESS_INF; 


typedef struct
{
	unsigned short Cmd;			  		 //结束指令
}
CMD_WRITE_END;





#pragma pack(pop)
#endif  //_SMART_LIGHT_H


