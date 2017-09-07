#ifndef _SMART_LIGHT_H_
#define _SMART_LIGHT_H_

/*========================================================================================================
** Copyright (c) 2013, Τ�����ܿƼ����޹�˾
** All rights reserved.
**
** �ļ�����: SmartLight.H
** �ļ�����: ͨ��ָ��
**
**  ע   ��: 1. ����ϵͳ�����е��豸���д�ָ��������ڴ˱����������ѳ�������ݣ���˱��ϸ���
**           2. ��Ҫ����ʱ��Ѹ��µ�����ͳһ����������ͳһ���º���ͳһ����
**           3. 
**
** ��ǰ�汾: 2.1
** �� �� ��: ��ɺ��
** �������: 2013��8��22��
** 
**
** ȡ���汾: 
** �������: 2013��11��08��
** �޸ļ�¼: 
   ( 1) v1.2�汾������CMD_PUBLIC_READ_END,CMD_PUBLIC_WRITE_STATE
   ( 2) V1.3�汾�����������Ӧ��ָ��
   ( 3) V1.4�汾�����ӹ�����ָ��,CMD_PUBLIC_READ_MULTI,CMD_PUBLIC_READ_SINGLE
   ( 4) V1.5�汾����������ָ��
   ( 5) V1.8�汾����CMD_PUBLIC_END_READ_INF ��Ϊ 0x40;  	���� :CMD_PUBLIC_ROLLCALL_ONLINE; CMD_PUBLIC_ONLINE_STATE; CMD_PUBLIC_ONLINE
   ( 6) V1.9�汾�����Ӻ�����ָ��
   ( 7) V2.0�汾�������Ŵ�2,3,4·�豸����
   ( 8) V2.1�汾���޸Ĵ���ָ��
   ( 9) V2.2�汾������[7�����ſ������] (�γ�ͥ 2015��04��01��)
   (10) V2.3�汾������GSM����ģ��ָ�� (��С�� 2015��04��01��) 
   (11) V2.4�汾������[COMMUNICATION_FORMAT]��[Data]��Сע��,[Data]��С������CRC4�ֽ� (�γ�ͥ 2015��04��03��)
   (12) V2.5�汾������[�����������Ͷ��Ź���]:[CMD_GSM_READ_SAFE_MESSAGE] (��С�� 2015��04��21��) 
   (13) V2.6�汾���޸�ԭ�е������Ӧָ����޸�Ϊ[��� CMD_PRI_READ_CONFIG]  (��С�� 2015��04��27��) 
   (13) V2.7�汾�������������Ӧָ��[��� CMD_PRI_FLASH]  (��С�� 2015��04��29��) 
   (14) V2.8�汾���޸ļ������Ӧָ��[��� CMD_PRI_READ_FLASH, CMD_PRI_WRITE_FLASH]  (��С�� 2015��05��03��) 
   (15) V2.9�汾, ����[NET_MOBILE]��[EQUIPMENT_MOBILE]���ָ��     (�γ�ͥ 2015��06��23��)
   (16) V3.0�汾, ����[EQUIPMENT_PANEL]���豸����(ͨ�ÿ������)    (�γ�ͥ 2015��06��06��)
   (17) V3.1�汾, ����[CMD_MMSG_READ_VER]��΢���������ָ��        (�γ�ͥ 2015��08��15��)
   (18) V3.2�汾, ����[EQUIPMENT_ENV_SENSOR_A]�Ȼ�������������     (�γ�ͥ 2015��09��07��)
   (19) V3.3�汾, ����[CMD_AMP_WIFI_SET]����WIFIָ��               (�γ�ͥ 2015��10��15��)
   (20) V3.4�汾, �޸�[EQUIPMENT_DOOR_IN_3]Ϊ[EQUIPMENT_DOOR_IN_4] (�γ�ͥ 2015��10��24��)
   (21) V3.5�汾, ����[CMD_PUBLIC_RESET_HOST]                      (�γ�ͥ 2016��01��13��)
   (22) V3.6�汾, ����[CMD_LOGIC_READ_CONFIG]��ָ��                (�γ�ͥ 2016��04��12��)
   (23) V3.7�汾, ����[CMD_PUBLIC_TEST_KEY_CMD]ָ��                (�γ�ͥ 2016��05��04��)
   (24) V3.8�汾, ����[EQUIPMENT_RSP]�豸����                      (�γ�ͥ 2016��05��12��)
   (25) V3.9�汾, ����[CMD_LOGIC_WRITE_SYSLKID]��λֵ��0x43��Ϊ0xC3(�γ�ͥ 2016��06��15��) -> ���ո�Ϊ0xC5
   (26) V4.0�汾, �޸�[CMD_LOGIC_WRITE_SYSLKID]Ϊ[��/��/����]��    (�γ�ͥ 2016��06��22��)
   (27) V4.1�汾, ����[EQUIPMENT_AIR_O2]�豸����                   (�γ�ͥ 2016��06��23��)
   (28) V4.2�汾, ����[CMD_PUBLIC_RESET_DEVICE]ָ��                (�γ�ͥ 2016��09��02��)
   (29) V4.3�汾�����������������ͺ������������ָ��               (��ɺ�� 2016��09��13��)               
   (30) V4.4�汾��������id��������                                 (��ɺ�� 2016��09��13��)
                  �����޸���ʹϵͳ��һ�������ݲ��޸�
   (31) V4.5�汾������[CMD_PUBLIC_WRITE_ADDRESS]���ص�ַ����(�����к�����xxxx)  (��ɺ�� 2016��11��16��)
   (32) V4.6�汾������ ����ת����������ָ���� ���� ��������������ָ����ָ�� CMD_TYPE_RFLINE ��CMD_TYPE_FP_LOCK
   (33) V4.7�汾������[CMD_RFLINE_READ_CFG][CMD_RFLINE_WRITE_CFG]                  (�γ�ͥ 2017��08��30��)
   (34) V4.8�汾������[CMD_RFLINE_WRITE_DEVAC_RSL][EQUIPMENT_EL_KNIFE_FRAME]       (�γ�ͥ 2017��08��31��)
   (35) V4.9�汾, ����[EQUIPMENT_INTELLIGENT_SINK]                                 (�γ�ͥ 2017��09��01��)
**======================================================================================================*/


/*=======================================================================================================
ϵͳͨ�Ÿ�ʽ��
            �豸ID + ����ID + ���ͺ� + Դ�豸ID + Դ����ID + ����+ ҳ + ������(2�ֽ�) + ���� + ���� +  4byte CRC
   ���� = ����	+ 4byte CRC		
   
 �豸ID ��Χ 0~100 ������Ҳ�� 0~100 ,100����Ϊ���⣬����ר���豸���������ƽ���
	
    ��λ������ �豸����,�豸ID������ID�����������Ƿ���Ҫ���ա�	
  
 �豸ID������ID�����ͺ����˵��
  �豸 ID     ����ID        ����                 ˵�� 
  
                |-- ����ID --- ָ��       ���豸����ͨ�ţ���Ҫ��������                      
                |-- ����ID --- ����       ��Ч           
  0~100 --|--	����   --- ָ��       ��Ч
                |--	����   --- ����       ��Ч
	 
 |-- ����   --- ָ��      ��������������ͬ������������
  ����  --|-- ����   --- ����      ���������豸��          ������
 |-- ����   --- ָ��      ϵͳ��������ͬ������  ������
 |-- ����   --- ����		 ϵͳ�����豸��          ������
	
 |-- ����   --- ָ��      ���η���������ָ�������豸��������  		
 �����ع���--|-- ����   --- ����      ���η����������豸��������  		
 |-- ����   --- ָ��      ϵͳ����������ָ�������豸��������  		
 |-- ����   --- ����      ϵͳ�����������豸��������  
   
========================================================================================================*/
//ע��ͨ��������С��ģʽ����λ��ǰ��λ�ں�

//�����豸
/*
enum		//�豸ID 0~100,  101����Ϊָ���豸��ID   4.4
{
 ID_MOBILE_START    = 0       ,//�ֻ���ƽ�忪ʼ��ַ
    ID_MOBILE_END      = 99      ,//�ֻ���ƽ�������ַ
 ID_PC_START        = 201     ,//PC��ַ��ʼ
 ID_PC_END          = 220     ,//PC��ַ����
 ID_GATEWAY         = 249     ,//����id     0xf9
 ID_CLOUD           = 250     ,//�Ʒ���id   0xfa
 ID_PKGNUM_PUBLIC   = 251     ,//�����Ź�����ַ(��RJ45���岹������֤��ɹ�����Ŀ��)
 ID_RJ45            = 252     ,//485ת����ת����ID
 ID_SERVER          = 253     ,//������
 ID_ANSWER_PUBLIC   = 254     ,//�����ع�����ַ
 ID_PUBLIC          = 255      //������ַ
};
*/

enum		//�豸ID 0~100,  101����Ϊָ���豸��ID
{
	ID_MOBILE_START    = 151     ,//�ֻ���ƽ�忪ʼ��ַ
    ID_MOBILE_END      = 200     ,//�ֻ���ƽ�������ַ
	ID_PC_START        = 201     ,//PC��ַ��ʼ
	ID_PC_END          = 220     ,//PC��ַ����
	ID_PKGNUM_PUBLIC   = 251     ,//�����Ź�����ַ(��RJ45���岹������֤��ɹ�����Ŀ��)
	ID_RJ45            = 252     ,//485ת����ת����ID
	ID_SERVER          = 253     ,//������
	ID_ANSWER_PUBLIC   = 254     ,//�����ع�����ַ
	ID_PUBLIC          = 255      //������ַ
};





//��������
enum					
{		
	NET_MOBILE          = 252,     //�ֻ����� (�γ�ͥ2017-05-06:����ϵͳ�����õ�������,����ȡ������)
	NET_SERVER          = 254,     //���������Ρ��ơ�Linux���ء��ֻ�����    0xfe
	NET_PUBLIC          = 255      //������ַ
};


//����������
enum		
{
	SENSOR_TEMP  		= 0		 ,//�¶ȴ�����
    SENSOR_HUMIDITY		= 1		 ,//ʪ�ȴ�����
	SENSOR_LUM			= 2 	 ,//���ȴ�����
	SENSOR_RAIN       	= 3      ,//��ˮ������
	SENSOR_WIND			= 4	     ,//����
	SENSOR_CO2          = 5      ,//������̼   
	SENSOR_FUEL_GAS     = 6      ,//��Ȼ����
	SENSOR_AIR_QUALITY  = 7      ,//��������
};


//�豸����   ����������豸������������
#define EQUIPMENT_KEY        	  	 0          //�������еļ���
#define EQUIPMENT_SWIT       	 	 1          //�������ԵĿ����豸   
#define EQUIPMENT_KEY_LCD  		 	 0x10       //Һ������
#define EQUIPMENT_KEY_TFT_LCD   	 0x11       //��ɫҺ������
#define EQUIPMENT_KEY_1          	 0x12       //1������
#define EQUIPMENT_KEY_2         	 0x13       //2������
#define EQUIPMENT_KEY_3         	 0x14       //3������
#define EQUIPMENT_KEY_4         	 0x15       //4������
#define EQUIPMENT_KEY_5        		 0x16       //5������
#define EQUIPMENT_KEY_6         	 0x17       //6������
#define EQUIPMENT_KEY_7              0x18       //7������
#define EQUIPMENT_KEY_8         	 0x19       //8������ 

#define EQUIPMENT_SWIT_4        	 0x20       //4·�̵���
#define EQUIPMENT_SWIT_6             0x21       //6·�̵���
#define EQUIPMENT_SWIT_8        	 0x22       //8·�̵���
#define EQUIPMENT_SWIT_12       	 0x23       //12·�̵���

#define EQUIPMENT_SRC_2         	 0x24       //2·�ɿع����
#define EQUIPMENT_SRC_4         	 0x25       //4·�ɿع����
#define EQUIPMENT_SRC_6         	 0x26       //6·�ɿع����
#define EQUIPMENT_SRC_8              0x27       //8·�ɿع����
#define EQUIPMENT_SRC_12        	 0x28       //12·�ɿع������

#define EQUIPMENT_10V_2         	 0x29       //2·10����
#define EQUIPMENT_10V_4        		 0x2a       //4·10����
#define EQUIPMENT_10V_6         	 0x2b       //6·10����
#define EQUIPMENT_10V_8        		 0x2c       //8·10����
#define EQUIPMENT_10V_12        	 0x2d       //12·10����

#define EQUIPMENT_LED_2          	 0x50       //2·LED����  //��֮ǰû�з��䣬������ʱ����
#define EQUIPMENT_LED_4        		 0x51       //4·LED����
#define EQUIPMENT_LED_6        		 0x52       //6·LED����
#define EQUIPMENT_LED_8              0x53       //8·LED����
#define EQUIPMENT_LED_12             0x54       //12·LED����

#define EQUIPMENT_TRAILING_2       	 0x55       //2·ǰ�ص���
#define EQUIPMENT_TRAILING_4         0x56       //4·ǰ�ص���
#define EQUIPMENT_TRAILING_6         0x57       //6·ǰ�ص���
#define EQUIPMENT_TRAILING_8         0x58       //8·ǰ�ص���
#define EQUIPMENT_TRAILING_12        0x59       //12·ǰ�ص���

#define EQUIPMENT_GSM                0x30       //GSM ģ��
#define EQUIPMENT_TEL                0x31       //���ߵ绰ģ��

#define EQUIPMENT_SENSOR             0x32
#define EQUIPMENT_TEMP               0x33       //�¶�ģ��
#define EQUIPMENT_BRIGHT             0x34       //����ģ��  

#define EQUIPMENT_PRI_1              0x35       //�����Ӧ1������
#define EQUIPMENT_PRI_2              0x36       //�����Ӧ2������
#define EQUIPMENT_PRI_3              0x37       //�����Ӧ3������
#define EQUIPMENT_PRI_x              0x38       //�����Ӧ ����

#define EQUIPMENT_WEATHER            0x39        //����վ�����ܰ�������С��¶ȡ��¶ȡ����ȡ�����
#define EQUIPMENT_CO2                0x3a        //������̼
#define EQUIPMENT_FUEL_GAS           0x3b        //��Ȼ����
#define EQUIPMENT_AIR_QUALITY        0x3c        //��������
#define EQUIPMENT_AIR_O2             0x3d        //����������

#define EQUIPMENT_IR_CEIL	         0x40        //������ת����,�컨��
#define EQUIPMENT_IR_86              0x41        //������ת����,86��

#define EQUIPMENT_POWER_1            0x44        //���1
#define EQUIPMENT_POWER_2            0x45        //���2
#define EQUIPMENT_POWER_3            0x46        //���3  
#define EQUIPMENT_POWER_4            0x47        //���4

#define EQUIPMENT_SHORT_IN_4         0x71        //��·����
#define EQUIPMENT_SHORT_IN_8         0x72        //��·����

#define EQUIPMENT_SHORT_OUT_4        0x73        //��·���
#define EQUIPMENT_SHORT_OUT_8        0x74        //��·���

#define EQUIPMENT_DOOR_IN_1          0x75        //������1 ����  [2015��04��01��]
#define EQUIPMENT_DOOR_IN_2          0x76        //������2 ������γ�ͥ��PC�༭������⼸���궨�壬����
#define EQUIPMENT_DOOR_IN_4          0x77        //������4 ����  ���ϵ�ͷ�ļ�û�У���������ͷ�ļ����ϡ�

#define EQUIPMENT_WIRELESS_2G4       0x48        //����2.4G
#define EQUIPMENT_WIRELESS_315M      0x49        //����315M
#define EQUIPMENT_WIRELESS_433M      0x4a        //����433M
#define EQUIPMENT_WIRELESS_ZEEBIG    0x4b        //����zeebig

#define EQUIPMENT_LOGIC              0x4c        //logicģ��

#define EQUIPMENT_HVAC_2CH           0x4d        //�յ�-2����
#define EQUIPMENT_CURTAIN_2CH        0x4e        //����-2·  
#define EQUIPMENT_CURTAIN_3CH        0x4f        //����-3· 

#define EQUIPMENT_AMP_MP3            0x60        //���� 
//#define EQUIPMENT_WINDOWS          0x61        //�Ŵ�
#define EQUIPMENT_DOORBELL           0x62        //����
#define EQUIPMENT_WINDOWS_2          0x63        //2·�Ŵ�
#define EQUIPMENT_WINDOWS_3          0x64        //3·�Ŵ�
#define EQUIPMENT_WINDOWS_4          0x65        //4·�Ŵ�

#define EQUIPMENT_PLAYER_KEY_7       0x80        //7�����ſ������

#define EQUIPMENT_ENV_SENSOR_A       0x81        //����������A
#define EQUIPMENT_ENV_SENSOR_B       0x82        //����������B
#define EQUIPMENT_ENV_SENSOR_C       0x83        //����������C
#define EQUIPMENT_ENV_SENSOR_D       0x84        //����������D
#define EQUIPMENT_ENV_SENSOR_E       0x85        //����������E

#define EQUIPMENT_RSP                0x90        //RSP�״�

#define EQUIPMENT_NO_LOCK_DOOR       0xa0        //��������
#define EQUIPMENT_FP_LOCK            0xa1        //ָ����
#define EQUIPMENT_EL_KNIFE_FRAME     0xa2        //�綯���߼�
#define EQUIPMENT_INTELLIGENT_SINK   0xa3        //����ˮ��

#define EQUIPMENT_RFLINE_GATEWAY     0xb0        //�������������ߵĲ�Ʒ�� 0xB0 ��ʼ (�γ�ͥ2017-05-06ע��:����ת����ת�����õ�)


#define EQUIPMENT_PANEL              0xE0        //ͨ�ÿ������

#define EQUIPMENT_GATEWAY            0xf9        //����
#define EQUIPMENT_RJ45       	     0xf0        //RJ45����
#define EQUIPMENT_LINKID             0xf1        //������(ר��<-ָ������)
#define EQUIPMENT_MOBILE             0xfc        //�ֻ�
#define EQUIPMENT_SERVER             0xfd        //������
#define EQUIPMENT_PC           	     0xfe        //PC����
#define EQUIPMENT_PUBLIC             0xff        //��������






/***********************  �����ǿ���ָ��  ****************************  
//��8λ��ʾ	ָ������
//��Ӧ�Ļظ�ָ��bit7Ϊ1
��ָ�   		0xh,1xh
дָ��:    		8xh,9xh     ���ʹ�ã�Ҳ�ᵥ������
  
����ָ��:  		2xh,        ���ƿ��صƣ��������û����Ը�֪��ָ��
״ָ̬� 		axh,
����ָ� 		3xh,4xh
�ظ�����ָ�  bxh,cxh
**********************************************************************/
enum  //ָ�����
{
	CMD_TYPE_PUBLIC     = EQUIPMENT_PUBLIC,            //��������
	CMD_TYPE_PC         = EQUIPMENT_PC,                //��������
	CMD_TYPE_SERVER     = EQUIPMENT_SERVER,            //������   
	CMD_TYPE_SWITCH     = EQUIPMENT_SWIT,        	   //����
	CMD_TYPE_KEY        = EQUIPMENT_KEY,           	   //����
	CMD_TYPE_LOGIC      = EQUIPMENT_LOGIC,             //�߼�
	CMD_TYPE_AC         = EQUIPMENT_HVAC_2CH,          //�յ�
	CMD_TYPE_CURTAIN    = EQUIPMENT_CURTAIN_2CH,	   //����
	CMD_TYPE_PRI        = EQUIPMENT_PRI_3,	 		   //�����Ӧ��	
	CMD_TYPE_AMP        = EQUIPMENT_AMP_MP3,           //����  
	CMD_TYPE_WINDOWS    = EQUIPMENT_WINDOWS_2,		   //�Ŵ�
	CMD_TYPE_IR         = EQUIPMENT_IR_CEIL,           //������
	CMD_TYPE_DOORBELL   = EQUIPMENT_DOORBELL,          //���� 
	CMD_TYPE_GSM        = EQUIPMENT_GSM,               //GSM����
	CMD_TYPE_MOBILE     = EQUIPMENT_MOBILE,            //�ֻ�
	CMD_TYPE_PANEL      = EQUIPMENT_PANEL,             //ͨ�ÿ������
	CMD_TYPE_NO_LOCK    = EQUIPMENT_NO_LOCK_DOOR,      //��������
	CMD_TYPE_RFLINE     = EQUIPMENT_RFLINE_GATEWAY,    //��������
	CMD_TYPE_FP_LOCK    = EQUIPMENT_FP_LOCK   		   //ָ����

};

enum    //����ָ��    _PUBLIC 
{    
    CMD_PUBLIC_NC1              		= ((CMD_TYPE_PUBLIC << 8) | 0x00)     	,//��Чָ���λ�����ᷢ�ʹ�ָ��
	CMD_PUBLIC_NC2              		= ((CMD_TYPE_PUBLIC << 8) | 0xff)  	    ,//ͬ��
	CMD_PUBLIC_NULL			   			= ((CMD_TYPE_PUBLIC << 8) | 0x30) 		,//��ָ�����λ���ᷢ�ʹ�ָ����������λ����ʱ�޸��ã���λ��Ҫ���������
	
	CMD_PUBLIC_START_SEARCH	    		= ((CMD_TYPE_PUBLIC << 8) | 0x31) 		,//���Է�������ʼָ��
	CMD_PUBLIC_STOP_SEARCH        		= ((CMD_TYPE_PUBLIC << 8) | 0x32)		,//ֹͣ����             
	CMD_PUBLIC_ROLLCALL           		= ((CMD_TYPE_PUBLIC << 8) | 0x33) 		,//�豸����             
	CMD_PUBLIC_SEARCH_UNROLLCALL  		= ((CMD_TYPE_PUBLIC << 8) | 0x34) 		,//����û�������豸     
	CMD_PUBLIC_ASSIGN_ID          		= ((CMD_TYPE_PUBLIC << 8) | 0x35)		,//�޸��豸ID           
	CMD_PUBLIC_STOP_READ                = ((CMD_TYPE_PUBLIC << 8) | 0x36)	    ,//ֹͣ��ȡ�豸��Ϣ 	
	CMD_PUBLIC_TEST                     = ((CMD_TYPE_PUBLIC << 8) | 0x37)		,//�豸����ָ��  
	CMD_PUBLIC_UART_LED_ENABLE         	= ((CMD_TYPE_PUBLIC << 8) | 0x38)		,//��ͨ�ȵ�
	CMD_PUBLIC_UART_LED_DISABLE        	= ((CMD_TYPE_PUBLIC << 8) | 0x39)		,//��ͨ�ȵ�
	CMD_PUBLIC_DISCOVER_ENABLE		    = ((CMD_TYPE_PUBLIC << 8) | 0x3a)		,//�����豸,ͨ�Ź���1���ӣ������ĵ���1���ӷ������ʱ�ҵ��豸
	CMD_PUBLIC_DISCOVER_DISABLE			= ((CMD_TYPE_PUBLIC << 8) | 0x3b)		,//�ط����豸
	CMD_PUBLIC_DEL_COMMAND              = ((CMD_TYPE_PUBLIC << 8) | 0x3d)		,//ɾ��ָ��
	CMD_PUBLIC_STOP                     = ((CMD_TYPE_PUBLIC << 8) | 0x3e)		,//ֹͣ��������Ϣ
	//CMD_PUBLIC_SIMPLE_SWITCH            = ((CMD_TYPE_PUBLIC << 8) | 0x3e)		,//ֹͣ��������Ϣ
	CMD_PUBLIC_RET_START_SEARCH         = ((CMD_TYPE_PUBLIC << 8) | 0x3f)		,//�ظ�����
	
	CMD_PUBLIC_RET_READ_INF				= ((CMD_TYPE_PUBLIC << 8) | 0x3f)		,//�ظ�����Ϣ
	CMD_PUBLIC_END_READ_INF				= ((CMD_TYPE_PUBLIC << 8) | 0x40)		,//���������Ϣ��ȡ
	
	CMD_PUBLIC_RET_READ_STATE		   = ((CMD_TYPE_PUBLIC << 8) | 0x41)		,//�ظ���״̬

	CMD_PUBLIC_WRITE_END               = ((CMD_TYPE_PUBLIC << 8) | 0x43)		,//�����
	CMD_PUBLIC_END_READ_STATE          = ((CMD_TYPE_PUBLIC << 8) | 0x44)		,//��״̬���
	CMD_PUBLIC_BROADCAST_PACKET_ID	   = ((CMD_TYPE_PUBLIC << 8) | 0x45)		,//�㲥����
	CMD_PUBLIC_APPLY_PACKET			   = ((CMD_TYPE_PUBLIC << 8) | 0x46)		,//����

	CMD_PUBLIC_ROLLCALL_ONLINE          = ((CMD_TYPE_PUBLIC << 8) | 0x47)       ,//���������豸
	CMD_PUBLIC_ONLINE_STATE             = ((CMD_TYPE_PUBLIC << 8) | 0x48)       ,//�豸�������
	CMD_PUBLIC_ONLINE                   = ((CMD_TYPE_PUBLIC << 8) | 0x49)       ,//�豸����
	
	
	CMD_PUBLIC_READ_TIME                = ((CMD_TYPE_PUBLIC << 8) | 0x01)       ,//��ϵͳʱ��
	CMD_PUBLIC_WRITE_TIME               = ((CMD_TYPE_PUBLIC << 8) | 0x81)    	,//ϵͳʱ��          
	
	CMD_PUBLIC_READ_INF                 = ((CMD_TYPE_PUBLIC << 8) | 0x02)  	    ,//��������Ϣ
	CMD_PUBLIC_WRITE_INF                = ((CMD_TYPE_PUBLIC << 8) | 0x82)  	    ,//д������Ϣ           	
	CMD_PUBLIC_READ_SET_INF             = ((CMD_TYPE_PUBLIC << 8) | 0x03)   	,//��ȡ�豸��Ϣ	    
	CMD_PUBLIC_READ_NAME                = ((CMD_TYPE_PUBLIC << 8) | 0x04)		,//���豸����           
	CMD_PUBLIC_WRITE_NAME               = ((CMD_TYPE_PUBLIC << 8) | 0x84)		,//д�豸����   
	CMD_PUBLIC_READ_LOOP_NAME           = ((CMD_TYPE_PUBLIC << 8) | 0x05)       ,//����·���� 
	CMD_PUBLIC_WRITE_LOOP_NAME          = ((CMD_TYPE_PUBLIC << 8) | 0x85)       ,//д��·����	
	CMD_PUBLIC_READ_APPOINT_MACHINE 	= ((CMD_TYPE_PUBLIC << 8) | 0x06)  	    ,//����ָ���豸�Ļ�����Ϣ������ǳ���״̬����ʹ�豸���ڷ���״̬ʱ����Ч�����´˰�60�봦����Ч״̬��
	//�����غ�3��Ű�״̬�壬���ֹ����ת��ʱ�����ط�����CMD_WRITE_INF ����   
	
	CMD_PUBLIC_READ_LOGIC_SEARCH        = ((CMD_TYPE_PUBLIC << 8) | 0x07)		,//�߼�����ָ��
	CMD_PUBLIC_WRITE_LOGIC_SEARCH       = ((CMD_TYPE_PUBLIC << 8) | 0x87)		,//�����߼�����ָ��
	CMD_PUBLIC_READ_COMPARE_DATA        = ((CMD_TYPE_PUBLIC << 8) | 0x08)       ,//�����ұȽϿ���ָ��
	CMD_PUBLIC_WRITE_COMPARE_DATA       = ((CMD_TYPE_PUBLIC << 8) | 0x88)       ,//д���ұȽϿ���ָ��
    
	CMD_PUBLIC_READ_COMMAND             = ((CMD_TYPE_PUBLIC << 8) | 0x09)	    ,//��������ָ��
	CMD_PUBLIC_WRITE_COMMAND            = ((CMD_TYPE_PUBLIC << 8) | 0x89)       ,//д�����ָ��
	CMD_PUBLIC_WRITE_NET_ID             = ((CMD_TYPE_PUBLIC << 8) | 0x8a)		,//�޸�����ID
	CMD_PUBLIC_READ_CONFIG              = ((CMD_TYPE_PUBLIC << 8) | 0x0b)       ,//��������Ϣ
	CMD_PUBLIC_WRITE_CONFIG			    = ((CMD_TYPE_PUBLIC << 8) | 0x8b)		,//д������Ϣ
	CMD_PUBLIC_READ_STATE				= ((CMD_TYPE_PUBLIC << 8) | 0x0c)		,//��״̬��Ϣ
	CMD_PUBLIC_WRITE_STATE              = ((CMD_TYPE_PUBLIC << 8) | 0x8c)       ,//д״̬	

	CMD_PUBLIC_READ_SWIT_STATE          = ((CMD_TYPE_PUBLIC << 8) | 0x0d)		,//������״̬ 
	CMD_PUBLIC_WRITE_SWIT_STATE         = ((CMD_TYPE_PUBLIC << 8) | 0x8d)		,//д����״̬ 
	CMD_PUBLIC_READ_HVAC_STATE          = ((CMD_TYPE_PUBLIC << 8) | 0x0e)		,//���յ�״̬
	CMD_PUBLIC_WRITE_HVAC_STATE         = ((CMD_TYPE_PUBLIC << 8) | 0x8e)		,//д�յ�״̬
	CMD_PUBLIC_READ_CURTAIN_STATE       = ((CMD_TYPE_PUBLIC << 8) | 0x0f)		,//������״̬
	CMD_PUBLIC_WRITE_CURTAIN_STATE      = ((CMD_TYPE_PUBLIC << 8) | 0x8f)		,//д����״̬
	CMD_PUBLIC_READ_SENSOR_STATE        = ((CMD_TYPE_PUBLIC << 8) | 0x10)       ,//��������״̬
	CMD_PUBLIC_WRITE_SENSOR_STATE       = ((CMD_TYPE_PUBLIC << 8) | 0x90)       ,//д������״̬
	CMD_PUBLIC_READ_KB_STATE            = ((CMD_TYPE_PUBLIC << 8) | 0x11)       ,//�����̰�״̬
	CMD_PUBLIC_WRITE_KB_STATE           = ((CMD_TYPE_PUBLIC << 8) | 0x91)       ,//д���̰�״̬
	CMD_PUBLIC_READ_MULTI               = ((CMD_TYPE_PUBLIC << 8) | 0x12)       ,//������ָ���������  
	CMD_PUBLIC_READ_SINGLE              = ((CMD_TYPE_PUBLIC << 8) | 0x13)       ,//������ָ�������
	CMD_PUBLIC_READ_PLACE_NAME			= ((CMD_TYPE_PUBLIC << 8) | 0x14)       ,//��λ������
	CMD_PUBLIC_WRITE_PLACE_NAME			= ((CMD_TYPE_PUBLIC << 8) | 0x94)       ,//дλ������
	CMD_PUBLIC_READ_PASSWORD			= ((CMD_TYPE_PUBLIC << 8) | 0x15)       ,//������
	CMD_PUBLIC_WRITE_PASSWORD			= ((CMD_TYPE_PUBLIC << 8) | 0x95)       ,//д����
	
//	CMD_PUBLIC_SWIT_STATE               = ((CMD_TYPE_PUBLIC << 8) | 0xa0)		,//�㲥����״̬ 
//	CMD_PUBLIC_HVAC_STATE               = ((CMD_TYPE_PUBLIC << 8) | 0xa1)		,//�㲥�յ�״̬
//	CMD_PUBLIC_CURTAIN_STATE            = ((CMD_TYPE_PUBLIC << 8) | 0xa2)		,//�㲥����״̬
//	CMD_PUBLIC_SENSOR_STATE             = ((CMD_TYPE_PUBLIC << 8) | 0xa3)       ,//������״̬
//	CMD_PUBLIC_KB_STATE                 = ((CMD_TYPE_PUBLIC << 8) | 0xa4)       ,//���̰�״̬
	
	CMD_PUBLIC_SIMPLE_SWIT              = ((CMD_TYPE_PUBLIC << 8) | 0x20)       ,//���׿���ָ���������0��ʾ�أ���0��ʾ�������ֵ
	CMD_PUBLIC_SWIT                     = ((CMD_TYPE_PUBLIC << 8) | 0x22)		,//����ָ��  ��ָ��ĵ�һ������ 0��ʾ�أ�1��ʾ����2��ʾȡ�� ,���ݵĵ���λ�������޸ģ���ʾ���ض���
    CMD_PUBLIC_SWIT_OPEN				= ((CMD_TYPE_PUBLIC << 8) | 0x23) 		,//��ָ��     
	CMD_PUBLIC_SWIT_CLOSE               = ((CMD_TYPE_PUBLIC << 8) | 0x24)		,//��ָ�� 
    CMD_PUBLIC_SWIT_OPEN_CONDITION   	= ((CMD_TYPE_PUBLIC << 8) | 0x2b) 		,//��ָ��,ֻ�в�����ʱ��ִ��
	CMD_PUBLIC_SWIT_CLOSE_CONDITION     = ((CMD_TYPE_PUBLIC << 8) | 0x2c)		,//��ָ��,ֻ�в�����ʱ��ִ��

	
	CMD_PUBLIC_SIMPLE_SWIT_NOT          = ((CMD_TYPE_PUBLIC << 8) | 0x25)       ,//����ȡ��ָ��	 ??
	CMD_PUBLIC_SWIT_NOT                 = ((CMD_TYPE_PUBLIC << 8) | 0x25)		,//�෴ָ��		 ??
    CMD_PUBLIC_INC                      = ((CMD_TYPE_PUBLIC << 8) | 0x26)		,//ָ���    
    CMD_PUBLIC_DEC                      = ((CMD_TYPE_PUBLIC << 8) | 0x27)		,//ָ���    
    CMD_PUBLIC_VAL                      = ((CMD_TYPE_PUBLIC << 8) | 0x28)		,//ָ����ֵ����ƹ�����ȣ������Ĵ�С
	CMD_PUBLIC_SIMULATE_KEY             = ((CMD_TYPE_PUBLIC << 8) | 0x29)		,//ģ�����
	CMD_PUBLIC_WINDOWS_PLAY          	= ((CMD_TYPE_PUBLIC << 8) | 0x2a)		,//�Ŵ�����ָ��

	CMD_PUBLIC_SAFETY_STATE             = ((CMD_TYPE_PUBLIC << 8) | 0xa0)		,//����״̬ 

	CMD_PUBLIC_READ_VER                 = ((CMD_TYPE_PUBLIC << 8) | 0xb0)       ,//���豸��Ӳ���汾 
	CMD_PUBLIC_WRITE_VER                = ((CMD_TYPE_PUBLIC << 8) | 0xb1)       ,//д�豸��Ӳ���汾 	
	CMD_PUBLIC_RESET_HOST               = ((CMD_TYPE_PUBLIC << 8) | 0xb2)       ,//��λ�������������� 
	CMD_PUBLIC_TEST_KEY_CMD             = ((CMD_TYPE_PUBLIC << 8) | 0xb3)       ,//(����)����ָ�����	
	CMD_PUBLIC_RESET_DEVICE             = ((CMD_TYPE_PUBLIC << 8) | 0xb4)       ,//�豸������ʼָ�� (ʹ�ñ�ָ��Ҫ����)

	CMD_PUBLIC_READ_ADDRESS          	= ((CMD_TYPE_PUBLIC << 8) | 0x35)		,//����ַ��
	CMD_PUBLIC_WRITE_ADDRESS          	= ((CMD_TYPE_PUBLIC << 8) | 0xb5)		 //д��ַ�������������xxxx
};



/****************
//��8λ��ʾ	ָ������
//��Ӧ�Ļظ�ָ��bit7Ϊ1
��ָ�   		0xh,1xh
дָ��:    		8xh,9xh     ���ʹ�ã�Ҳ�ᵥ������
  
����ָ��:  		2xh,        ���ƿ��صƣ��������û����Ը�֪��ָ��
״ָ̬� 		axh,
����ָ� 		3xh,4xh
�ظ�����ָ�  bxh,cxh
********************/
enum    //������    CMD_TYPE_SERVER
{	
	CMD_SERVER_SEARCH                  = ((CMD_TYPE_SERVER << 8) | 0x30)        ,//����������ת����
    CMD_SERVER_CONNECT                 = ((CMD_TYPE_SERVER << 8) | 0x31)        ,//��������
    CMD_SERVER_RET_CONNECT             = ((CMD_TYPE_SERVER << 8) | 0xb2)        ,//�ظ�����
	CMD_SERVER_HEARTBEAT               = ((CMD_TYPE_SERVER << 8) | 0x33)        ,//����֡
	CMD_SERVER_RET_HEARTBEAT           = ((CMD_TYPE_SERVER << 8) | 0xb3)        ,//�ظ�����֡

	CMD_SERVER_EMAIL                   = ((CMD_TYPE_SERVER << 8) | 0x20)        ,//emailָ��
	
	CMD_MMSG_READ_VER                  = ((CMD_TYPE_SERVER << 8) | 0x61)        ,//���豸��Ӳ���汾
	CMD_MMSG_WRITE_VER                 = ((CMD_TYPE_SERVER << 8) | 0xE1)        ,//д�豸��Ӳ���汾
	CMD_MMSG_READ_MEMU_NAME            = ((CMD_TYPE_SERVER << 8) | 0x62)        ,//���˵�����
	CMD_MMSG_WRITE_MEMU_NAME           = ((CMD_TYPE_SERVER << 8) | 0xE2)        ,//д�˵�����
	CMD_MMSG_READ_COMMAND              = ((CMD_TYPE_SERVER << 8) | 0x63)        ,//������ָ��
	CMD_MMSG_WRITE_COMMAND             = ((CMD_TYPE_SERVER << 8) | 0xE3)        ,//д�����ָ��
	CMD_MMSG_DEL_COMMAND               = ((CMD_TYPE_SERVER << 8) | 0x64)        ,//ɾ������ָ��
	CMD_MMSG_READ_SECURITY_CFG         = ((CMD_TYPE_SERVER << 8) | 0x65)        ,//������������
	CMD_MMSG_WRITE_SECURITY_CFG        = ((CMD_TYPE_SERVER << 8) | 0xE5)        ,//д����������
	CMD_MMSG_READ_KEY_CFG              = ((CMD_TYPE_SERVER << 8) | 0x66)        ,//���˵�������������
	CMD_MMSG_WRITE_KEY_CFG             = ((CMD_TYPE_SERVER << 8) | 0xE6)        ,//д�˵�������������
	CMD_MMSG_READ_BDEV_CFG             = ((CMD_TYPE_SERVER << 8) | 0x67)        ,//���󶨵��豸����
	CMD_MMSG_WRITE_BDEV_CFG            = ((CMD_TYPE_SERVER << 8) | 0xE7)        ,//д�󶨵��豸����
};

enum    //�ֻ�    EQUIPMENT_MOBILE
{
	CMD_MOBILE_SEARCH                   = ((CMD_TYPE_MOBILE << 8) | 0x30)       ,//��������
	CMD_MOBILE_GWINFORMATION            = ((CMD_TYPE_MOBILE << 8) | 0x31)       ,//�ϴ�������Ϣ
	CMD_MOBILE_CONNECT                  = ((CMD_TYPE_MOBILE << 8) | 0x32)       ,//��������
	CMD_MOBILE_CONNECT_RET              = ((CMD_TYPE_MOBILE << 8) | 0xB2)       ,//����ظ�
	CMD_MOBILE_HEARTBEAT                = ((CMD_TYPE_MOBILE << 8) | 0x33)       ,//����֡
	CMD_MOBILE_DISCONNECT               = ((CMD_TYPE_MOBILE << 8) | 0x34)       ,//�Ͽ�����
	CMD_MOBILE_CHANGE_UPASSWORD         = ((CMD_TYPE_MOBILE << 8) | 0x35)       ,//�޸��û�����
	CMD_MOBILE_CHANGE_UPASSWORD_RET     = ((CMD_TYPE_MOBILE << 8) | 0xB5)       ,//��Ӧ�޸�
};

enum    //����ָ��  CMD_TYPE_KEY 
{
	CMD_KB_READ_PICTURE_NAME 			= ((CMD_TYPE_KEY << 8) | 0x01)	    	,//д������ʾ��ͼƬ����  
	CMD_KB_WRITE_PICTURE_NAME           = ((CMD_TYPE_KEY << 8) | 0x81)	   		,//д������ʾ��ͼƬ����  
	CMD_KB_READ_BACK_LIGHT              = ((CMD_TYPE_KEY << 8) | 0x02)      	,//����������  		
	CMD_KB_WRITE_BACK_LIGHT             = ((CMD_TYPE_KEY << 8) | 0x82)       	,//д��������  		
	CMD_KB_READ_PASSWORD               	= ((CMD_TYPE_KEY << 8) | 0x03)      	,//����������   	
	CMD_KB_WRITE_PASSWORD          		= ((CMD_TYPE_KEY << 8) | 0x83)       	,//д��������   	
	CMD_KB_READ_PASSWORD_PAGE          	= ((CMD_TYPE_KEY << 8) | 0x04)	    	,//������ҳ������   
	CMD_KB_WRITE_PASSWORD_PAGE      	= ((CMD_TYPE_KEY << 8) | 0x84)	    	,//д����ҳ������   
	CMD_KB_READ_PAGE_DIS          		= ((CMD_TYPE_KEY << 8) | 0x05)			,//��ҳ����ʾ����   
	CMD_KB_WRITE_PAGE_DIS	      		= ((CMD_TYPE_KEY << 8) | 0x85)			,//дҳ����ʾ����   
	CMD_KB_READ_IR						= ((CMD_TYPE_KEY << 8) | 0x06)			,//������������     
	CMD_KB_WRITE_IR                     = ((CMD_TYPE_KEY << 8) | 0x86)			,//д����������     
	CMD_KB_READ_SYS_SETUP_INF           = ((CMD_TYPE_KEY << 8) | 0x07)			,//��ϵͳ������Ϣ  �� ָ���1 ָ���   
	CMD_KB_READ_KEY_FUNCTION        	= ((CMD_TYPE_KEY << 8) | 0x08)			,//����������       
	CMD_KB_WRITE_KEY_FUNCTION      		= ((CMD_TYPE_KEY << 8) | 0x88)			,//д��������       
	CMD_KB_READ_KEY_ALL_FUNCTION      	= ((CMD_TYPE_KEY << 8) | 0x09)			,//�����а�������   
	
	CMD_KB_READ_HVAC_STATE              = ((CMD_TYPE_KEY << 8) | 0x0a)			,//��Һ�����յ�״̬    
	CMD_KB_WRITE_HVAC_STATE             = ((CMD_TYPE_KEY << 8) | 0x8a)			,//дҺ�����յ�״̬    
	CMD_KB_READ_HVAC_CONFIG             = ((CMD_TYPE_KEY << 8) | 0x0b)			,//���յ�����            
	CMD_KB_WRITE_HVAC_CONFIG            = ((CMD_TYPE_KEY << 8) | 0x8b)			,//д�յ�����
	
    CMD_KB_READ_STARTUP_LIGHT_SET       = ((CMD_TYPE_KEY << 8) | 0x0c)			,//�������ƹ���ʾ����     0x96  ����
    CMD_KB_WRITE_STARTUP_LIGHT_SET      = ((CMD_TYPE_KEY << 8) | 0x8c)			,//д�����ƹ���ʾ����     0x97  ���ة�
    CMD_KB_READ_STARTUP_KEY_STATE       = ((CMD_TYPE_KEY << 8) | 0x0c)			,//������������ʾ����     0x96  ���Щء�Э���ĵ�˵��Ҫ�ģ���ͷ�ļ�û�У��γ�ͥ
    CMD_KB_WRITE_STARTUP_KEY_STATE      = ((CMD_TYPE_KEY << 8) | 0x8c)			,//д����������ʾ����     0x97  ����    ��2015��04��01�ո��ģ����ɶ���걣����

    CMD_KB_READ_HVAC_TGTCONFIG          = ((CMD_TYPE_KEY << 8) | 0x0d)			,//���յ����Ŀ��������� 0x98
    CMD_KB_WRITE_HVAC_TGTCONFIG         = ((CMD_TYPE_KEY << 8) | 0x8d)			,//д�յ����Ŀ��������� 0x99

	CMD_KB_READ_OPTIONS                 = ((CMD_TYPE_KEY << 8) | 0x0E)			,//�����̲�������
	CMD_KB_WRITE_OPTIONS 				= ((CMD_TYPE_KEY << 8) | 0x8E)			,//д���̲�������
	
//    CMD_KB_READ_WINDOWS_MODE            = ((CMD_TYPE_KEY << 8) | 0x0E)		,//���Ŵ���ʾģʽ
//    CMD_KB_WRITE_WINDOWS_MODE           = ((CMD_TYPE_KEY << 8) | 0x8E)		,//д�Ŵ���ʾģʽ

//	CMD_KB_READ_SHORTCUT				= ((CMD_TYPE_KEY << 8) | 0x0f)          ,//����ݼ�
//    CMD_KB_WRITE_SHORTCUT				= ((CMD_TYPE_KEY << 8) | 0x8f)          ,//д��ݼ�
//	CMD_KB_READ_OBJECT                	= ((CMD_TYPE_KEY << 8) | 0x10)          ,//��Ŀ��
//	CMD_KB_WRITE_OBJECT					= ((CMD_TYPE_KEY << 8) | 0x90)          ,//дĿ��

	CMD_KB_TEST_KEY                     = ((CMD_TYPE_KEY << 8) | 0x30)			,//���԰���
	CMD_KB_BROAD_CONJUNCTION            = ((CMD_TYPE_KEY << 8) | 0x31)			 //�㲥������״̬(ʹ�ù�����ַ�����Ρ����͡��㲥) d   �ÿ��� CMD_PUBLIC_SWIT ����
};  

enum  //�̵�����������ָ��  CMD_TYPE_SWITCH
{
	CMD_SW_READ_GROUP_NAME 				= ((CMD_TYPE_SWITCH << 8) | 0x01)           ,//����������    
	CMD_SW_WRITE_GROUP_NAME             = ((CMD_TYPE_SWITCH << 8) | 0x81)           ,//д��������     
	CMD_SW_READ_SCENE_NAME              = ((CMD_TYPE_SWITCH << 8) | 0x02) 			,//����������    
	CMD_SW_WRITE_SCENE_NAME             = ((CMD_TYPE_SWITCH << 8) | 0x82) 			,//д��������     
	CMD_SW_READ_LIST_NAME               = ((CMD_TYPE_SWITCH << 8) | 0x03) 			,//��ʱ������    
	CMD_SW_WRITE_LIST_NAME              = ((CMD_TYPE_SWITCH << 8) | 0x83) 			,//дʱ������     
	CMD_SW_READ_GROUP_INF               = ((CMD_TYPE_SWITCH << 8) | 0x04) 			,//��������Ϣ    
	CMD_SW_WRITE_GROUP_INF              = ((CMD_TYPE_SWITCH << 8) | 0x84) 			,//д������Ϣ    
	CMD_SW_READ_SCENE_INF               = ((CMD_TYPE_SWITCH << 8) | 0x05) 			,//��������Ϣ    
	CMD_SW_WRITE_SCENE_INF              = ((CMD_TYPE_SWITCH << 8) | 0x85)  			,//д������Ϣ    
	CMD_SW_READ_LIST_INF                = ((CMD_TYPE_SWITCH << 8) | 0x06) 			,//��ʱ����Ϣ    
	CMD_SW_WRITE_LIST_INF               = ((CMD_TYPE_SWITCH << 8) | 0x86) 			,//дʱ����Ϣ    
	CMD_SW_READ_SWIT_PROCE              = ((CMD_TYPE_SWITCH << 8) | 0x07) 			,//����·���ƹ��������
	CMD_SW_WRITE_SWIT_PROCE             = ((CMD_TYPE_SWITCH << 8) | 0x87) 			,//д��·���ƹ��������
	CMD_SW_READ_SWIT_VOLTAGE            = ((CMD_TYPE_SWITCH << 8) | 0x08) 			,//����·��ѹ
	CMD_SW_WRITE_SWIT_VOLTAGE           = ((CMD_TYPE_SWITCH << 8) | 0x88) 			,//д��·��ѹ 	
	CMD_SW_READ_SWIT_CURRENT            = ((CMD_TYPE_SWITCH << 8) | 0x09) 			,//����·����
	CMD_SW_WRITE_SWIT_CURRENT           = ((CMD_TYPE_SWITCH << 8) | 0x89) 			,//д��·����
	CMD_SW_READ_SWIT_POWER              = ((CMD_TYPE_SWITCH << 8) | 0x0a)  			,//����·����
	CMD_SW_WRITE_SWIT_POWER             = ((CMD_TYPE_SWITCH << 8) | 0x8a)  			,//д��·����
	CMD_SW_READ_POWER_ON_RESUME		    = ((CMD_TYPE_SWITCH << 8) | 0x0b)  			,//����·�ϵ�����״̬
	CMD_SW_WRITE_POWER_ON_RESUME		= ((CMD_TYPE_SWITCH << 8) | 0x8b)  			,//д��·�ϵ�����״̬

	CMD_SW_SWIT_LOOP		            = ((CMD_TYPE_SWITCH << 8) | 0x20)  			,//��·����
	CMD_SW_SWIT_LOOP_OPEN	            = ((CMD_TYPE_SWITCH << 8) | 0x21)  			,//��·��
	CMD_SW_SWIT_LOOP_CLOSE	            = ((CMD_TYPE_SWITCH << 8) | 0x22)  			,//��·��
	CMD_SW_SWIT_LOOP_NOT	            = ((CMD_TYPE_SWITCH << 8) | 0x23)  			,//��·ȡ��
	CMD_SW_SWIT_LOOP_OPEN_CONDITION     = ((CMD_TYPE_SWITCH << 8) | 0x30)  			,//��·��������
	CMD_SW_SWIT_LOOP_CLOSE_CONDITION    = ((CMD_TYPE_SWITCH << 8) | 0x31)  			,//��·��������

	CMD_SW_SWIT_SCENE		            = ((CMD_TYPE_SWITCH << 8) | 0x24)  			,//��������
	CMD_SW_SWIT_SCENE_OPEN	            = ((CMD_TYPE_SWITCH << 8) | 0x25)  			,//������
	CMD_SW_SWIT_SCENE_CLOSE	            = ((CMD_TYPE_SWITCH << 8) | 0x26)  			,//������
	CMD_SW_SWIT_SCENE_NOT	            = ((CMD_TYPE_SWITCH << 8) | 0x27)  			,//����ȡ��

	CMD_SW_SWIT_LIST		            = ((CMD_TYPE_SWITCH << 8) | 0x28)  			,//ʱ�򿪹�
	CMD_SW_SWIT_LIST_OPEN	            = ((CMD_TYPE_SWITCH << 8) | 0x29)  			,//ʱ��
	CMD_SW_SWIT_LIST_CLOSE	            = ((CMD_TYPE_SWITCH << 8) | 0x2a)  			,//ʱ���
	CMD_SW_SWIT_LIST_NOT	            = ((CMD_TYPE_SWITCH << 8) | 0x2b)  			,//ʱ��ȡ��

	CMD_SW_SWIT_ALL		           		= ((CMD_TYPE_SWITCH << 8) | 0x2c)  			,//ȫ������
	CMD_SW_SWIT_ALL_OPEN	            = ((CMD_TYPE_SWITCH << 8) | 0x2d)  			,//ȫ����
	CMD_SW_SWIT_ALL_CLOSE	            = ((CMD_TYPE_SWITCH << 8) | 0x2e)  			,//ȫ����

	CMD_SW_TEST_LOOP					= ((CMD_TYPE_SWITCH << 8) | 0x2f)  			,//��·����
};

enum       //PCָ��   CMD_TYPE_PC  
{
	CMD_PC_SEARCH                       = ((CMD_TYPE_PC << 8) | 0x01)           ,//1. <PC-RJ45> �������RJ45�豸
	CMD_PC_SEARCH_ACK                   = ((CMD_TYPE_PC << 8) | 0x81)           ,//2. <RJ45-PC> RJ45�ϱ�������Ϣ
	CMD_PC_CHANGEPASSWORD               = ((CMD_TYPE_PC << 8) | 0x02)           ,//3. <PC-RJ45> �޸�RJ45����
	CMD_PC_CHANGEPASSWORD_ACK           = ((CMD_TYPE_PC << 8) | 0x82)           ,//4. <RJ45-PC> ��Ӧ�޸�RJ45������
	CMD_PC_CHANGENET                    = ((CMD_TYPE_PC << 8) | 0x03)           ,//5. <PC-RJ45> �޸�RJ45�������
	CMD_PC_CHANGENET_ACK                = ((CMD_TYPE_PC << 8) | 0x83)           ,//6. <RJ45-PC> ��ӦPC�޸�RJ45����������
	CMD_PC_CHANGENAME                   = ((CMD_TYPE_PC << 8) | 0x04)           ,//7. <PC-RJ45> �޸�RJ45�豸����
	CMD_PC_CHANGENAME_ACK               = ((CMD_TYPE_PC << 8) | 0x84)           ,//8. <RJ45-PC> ��ӦPC�޸�RJ45�豸���ƽ��
	CMD_PC_READ_LOCALL_NAME           	= ((CMD_TYPE_PC << 8) | 0x05)           ,//14.<PC-RJ45> ��λ������
	CMD_PC_WRITE_LOCALL_NAME           	= ((CMD_TYPE_PC << 8) | 0x85)           ,//14.<RJ45-PC> дλ������

	CMD_PC_CONNECT                      = ((CMD_TYPE_PC << 8) | 0x30)           ,//9. <PC-RJ45> ��������
	CMD_PC_CONNECT_ACK                  = ((CMD_TYPE_PC << 8) | 0xb0)           ,//10.<RJ45-PC> ��Ӧ����
	CMD_PC_CONNECTING                   = ((CMD_TYPE_PC << 8) | 0x31)           ,//11.<PC/RJ45> ˢ������
	CMD_PC_DISCONNECT                   = ((CMD_TYPE_PC << 8) | 0x32)           ,//12.<PC/RJ45> �Ͽ�����
};


enum  // LOGIC ָ��	 EQUIPMENT_LOGIC
{
	CMD_LOGIC_WRITE_TIMER_NAME 			= ((CMD_TYPE_LOGIC << 8) | 0x01)			,//д��ʱ������      ��
	CMD_LOGIC_READ_TIMER_NAME           = ((CMD_TYPE_LOGIC << 8) | 0x81) 			,//����ʱ������      ��
	CMD_LOGIC_WRITE_BLOCK_NAME          = ((CMD_TYPE_LOGIC << 8) | 0x02) 			,//д�߼�������      ��
	CMD_LOGIC_READ_BLOCK_NAME           = ((CMD_TYPE_LOGIC << 8) | 0x82) 			,//���߼�������      ��
	CMD_LOGIC_WRITE_BLOCK_PORT_SATE     = ((CMD_TYPE_LOGIC << 8) | 0x03) 			,//д�߼���˿�״̬  ��
	CMD_LOGIC_READ_BLOCK_PORT_SATE      = ((CMD_TYPE_LOGIC << 8) | 0x83) 			,//���߼���˿�״̬  ��
	CMD_LOGIC_WRITE_TIMER_INF           = ((CMD_TYPE_LOGIC << 8) | 0x04) 			,//д��ʱ��������Ϣ  �����γ�ͥ-ע��:��дָ����ֵ������򲻷��ϱ�׼Ҫ��,
	CMD_LOGIC_READ_TIMER_INF            = ((CMD_TYPE_LOGIC << 8) | 0x84) 			,//����ʱ��������Ϣ  ��     ��׼Ҫ��:[��ָ��]bit7=0,[дָ��]bit7=1!!!!!!
	CMD_LOGIC_WRITE_BLOCK_INF           = ((CMD_TYPE_LOGIC << 8) | 0x05)			,//д�߼���������Ϣ  ��
	CMD_LOGIC_READ_BLOCK_INF            = ((CMD_TYPE_LOGIC << 8) | 0x85) 			,//���߼���������Ϣ  ��
	//                  ��
	CMD_LOGIC_WRITE_CMD                 = ((CMD_TYPE_LOGIC << 8) | 0x06) 			,//д����ָ��        ��
	CMD_LOGIC_READ_CMD                  = ((CMD_TYPE_LOGIC << 8) | 0x86) 			,//������ָ��        ��
	
	CMD_LOGIC_READ_CONFIG               = ((CMD_TYPE_LOGIC << 8) | 0x41)        ,//���������� (�������߼�)
	CMD_LOGIC_WRITE_CONFIG              = ((CMD_TYPE_LOGIC << 8) | 0xC1)        ,//д�������� (�������߼�)
	CMD_LOGIC_READ_EXACTION             = ((CMD_TYPE_LOGIC << 8) | 0x42)        ,//���߼����Ӷ���
	CMD_LOGIC_WRITE_EXACTION            = ((CMD_TYPE_LOGIC << 8) | 0xC2)        ,//д�߼����Ӷ���   (��ͬ�豸����һ��)
	CMD_LOGIC_READ_SECURITY             = ((CMD_TYPE_LOGIC << 8) | 0x44)        ,//���߼�������������־���� (�����߼������ɵ�������)
	CMD_LOGIC_WRITE_SECURITY            = ((CMD_TYPE_LOGIC << 8) | 0xC4)        ,//д�߼�������������־���� (�����߼������ɵ�������)
	
	CMD_LOGIC_WRITE_SYSLKID             = ((CMD_TYPE_LOGIC << 8) | 0xC5)        ,//д�߼�ϵͳ������-���� (ע��:��[CMD_SW_SWIT_LOOP_OPEN]��ָ��ͬһ��ʽ)
	CMD_LOGIC_WRITE_SYSLKID_OPEN        = ((CMD_TYPE_LOGIC << 8) | 0xC6)        ,//д�߼�ϵͳ������-��   (ע��:��[CMD_SW_SWIT_LOOP_OPEN]��ָ��ͬһ��ʽ)
	CMD_LOGIC_WRITE_SYSLKID_CLOSE       = ((CMD_TYPE_LOGIC << 8) | 0xC7)        ,//д�߼�ϵͳ������-��   (ע��:��[CMD_SW_SWIT_LOOP_OPEN]��ָ��ͬһ��ʽ)
	CMD_LOGIC_WRITE_SLFLKID             = ((CMD_TYPE_LOGIC << 8) | 0xC8)        ,//д�߼��ڲ�������-���� (ע��:��[CMD_SW_SWIT_LOOP_OPEN]��ָ��ͬһ��ʽ)
	CMD_LOGIC_WRITE_SLFLKID_OPEN        = ((CMD_TYPE_LOGIC << 8) | 0xC9)        ,//д�߼��ڲ�������-��   (ע��:��[CMD_SW_SWIT_LOOP_OPEN]��ָ��ͬһ��ʽ)
	CMD_LOGIC_WRITE_SLFLKID_CLOSE       = ((CMD_TYPE_LOGIC << 8) | 0xCA)        ,//д�߼��ڲ�������-��   (ע��:��[CMD_SW_SWIT_LOOP_OPEN]��ָ��ͬһ��ʽ)
}; 

enum // �յ�ָ��  	CMD_TYPE_AC
{
	CMD_AC_READ_STATE       			= ((CMD_TYPE_AC << 8) | 0x01)			,//���յ�״̬
	CMD_AC_WRITE_STATE        			= ((CMD_TYPE_AC << 8) | 0x81)  			,//д�յ�״̬
	CMD_AC_READ_CONFIG        			= ((CMD_TYPE_AC << 8) | 0x02)	        ,//���յ�����
	CMD_AC_WRITE_CONFIG       			= ((CMD_TYPE_AC << 8) | 0x82)	        ,//д�յ�����
	CMD_AC_READ_FANDC         			= ((CMD_TYPE_AC << 8) | 0x03)   		,//���յ����ٿ��Ƶ�ѹ
	CMD_AC_WRITE_FANDC        			= ((CMD_TYPE_AC << 8) | 0x83)       	,//д�յ����ٿ��Ƶ�ѹ
}; 



enum //�Ŵ�����ָ��
{
	CMD_WINDOWS_RUN_STATE	           	= ((CMD_TYPE_WINDOWS << 8) | 0x01) 			,//ִ�е�ǰ�Ŵ�״̬	  	
	CMD_WINDOWS_READ_POWER              = ((CMD_TYPE_WINDOWS << 8) | 0x02)          ,//����ǰ����/������С
	CMD_WINDOWS_WRITE_POWER             = ((CMD_TYPE_WINDOWS << 8) | 0x03)          ,//д��ǰ����/������С
};

enum //������ָ��
{
//	CMD_IR_SEND_CODE  	           	    = ((CMD_TYPE_IR << 8) | 0x20) 				,//���ͺ�������
//	CMD_IR_READ_CODE                    = ((CMD_TYPE_IR << 8) | 0x01) 				,//����������
//	CMD_IR_WRITE_CODE					= ((CMD_TYPE_IR << 8) | 0x81) 				,//д��������	

	CMD_IR_SWIT      					= ((CMD_TYPE_IR << 8) | 0x20) 				,//���ͺ�����
	CMD_IR_STUDY						= ((CMD_TYPE_IR << 8) | 0x21) 				,//������ѧϰ
	CMD_IR_STUDY_RESULT					= ((CMD_TYPE_IR << 8) | 0xa0) 				,//����ѧϰ���
	CMD_IR_SEARCH_CODE					= ((CMD_TYPE_IR << 8) | 0x22) 				,//�����߿���ָ��

	CMD_IR_READ_CODE					= ((CMD_TYPE_IR << 8) | 0x00) 				,//�����߿���ָ��
	CMD_IR_WRITE_CODE  					= ((CMD_TYPE_IR << 8) | 0x80) 				,//�����߿���ָ��

	CMD_IR_READ_SINGLE_KEY	 			= ((CMD_TYPE_IR << 8) | 0x01) 				,//�����߿���ָ��
	CMD_IR_WRITE_KEY				    = ((CMD_TYPE_IR << 8) | 0x81) 				,//�����߿���ָ��

	CMD_IR_READ_WORK_MODE               = ((CMD_TYPE_IR << 8) | 0x02) 				,//�����߿���ָ��
	CMD_IR_WRITE_WORK_MODE				= ((CMD_TYPE_IR << 8) | 0x82) 				,//�����߿���ָ��
};


enum //����
{
	CMD_DOORBELL_CONTROL  	           	= ((CMD_TYPE_DOORBELL << 8) | 0x20) 			,//�������	
};



enum //�����Ӧ��ָ��
{	
//   CMD_PUBLIC_READ_CONFIG  			= ((CMD_TYPE_PRI << 8) | 0x01)  			,//��������Ϣ
//	 CMD_PUBLIC_WRITE_CONFIG      	    = ((CMD_TYPE_PRI << 8) | 0x81) 				,//д������Ϣ

//ԭ	CMD_PRI_TEST           				= ((CMD_TYPE_PRI << 8) | 0x02)  		,//����ָ��
//ԭ	CMD_PRI_SWIT_PRI        	   		= ((CMD_TYPE_PRI << 8) | 0x03)			,//���������Ӧ����
//ԭ	CMD_PRI_SWIT_SAFETY                 = ((CMD_TYPE_PRI << 8) | 0x04)         	,//���ذ�������
    CMD_PRI_READ_CONFIG                 = ((CMD_TYPE_PRI << 8) | 0x01)  			,//��������������������
	CMD_PRI_WRITE_CONFIG           		= ((CMD_TYPE_PRI << 8) | 0x81)  			,//д������������������
	CMD_PRI_READ_SAFETY_CONFIG          = ((CMD_TYPE_PRI << 8) | 0x02)				,//����������������
	CMD_PRI_WRITE_SAFETY_CONFIG         = ((CMD_TYPE_PRI << 8) | 0x82)         		,//д��������������
	CMD_PRI_TEST                        = ((CMD_TYPE_PRI << 8) | 0x03)         		,//����������ָ��
//ԭ��	CMD_PRI_FLASH                       = ((CMD_TYPE_PRI << 8) | 0x04)         	,//��������Ӧ�ƹ⿪��
    CMD_PRI_READ_FLASH_CONFIG           = ((CMD_TYPE_PRI << 8) | 0x04)              ,//����������Ӧָʾ�ƿ���
    CMD_PRI_WRITE_FLASH_CONFIG          = ((CMD_TYPE_PRI << 8) | 0x84)              ,//д��������Ӧָʾ�ƿ���
};


enum //����ָ�� ----�����
{
//  CMD_PUBLIC_READ_STATE                                						    ,//��������ѯ״̬	 
//	CMD_PUBLIC_WRITE_STATE	                      								    ,//���죺�ϱ�����,��Դ,����,����,����
//	CMD_PUBLIC_READ_CONFIG														    ,//��mp3������Ϣ					
//	CMD_PUBLIC_WRITE_CONFIG														    ,//дmp3������Ϣ					 
	
	CMD_AMP_SLWR_BGM_KEY                = ((CMD_TYPE_AMP << 8) | 0x20)              ,//�������л�������Դ��ģ�ⰴ��������Դ
	CMD_AMP_SLWR_BGM_SONG               = ((CMD_TYPE_AMP << 8) | 0x21)              ,//�������л�������Դ��ָ����Դ����Ŀ����
	CMD_AMP_SLWR_BGM_VOL_SONG           = ((CMD_TYPE_AMP << 8) | 0x22)              ,//�������л�������Դ��������ָ����Դ��Ŀ����
	CMD_AMP_SLWR_BGM_SRC                = ((CMD_TYPE_AMP << 8) | 0x23)              ,//�������л�������Դ     
	CMD_AMP_SLWR_BGM_VOL                = ((CMD_TYPE_AMP << 8) | 0x24)              ,//�������޸ı�������
	CMD_AMP_SLWR_BGM_TRE                = ((CMD_TYPE_AMP << 8) | 0x25)              ,//�������޸ı�������
	CMD_AMP_SLWR_BGM_BAS                = ((CMD_TYPE_AMP << 8) | 0x26)              ,//�������޸ı�������
	CMD_AMP_SLWR_BGM_TUNE               = ((CMD_TYPE_AMP << 8) | 0x27)              ,//�������޸ı�������,����,����
	CMD_AMP_SLWR_MSN_TUNE               = ((CMD_TYPE_AMP << 8) | 0x28)              ,//�������޸Ľ�������,����,����
	
	CMD_AMP_SLWR_PPEMC                  = ((CMD_TYPE_AMP << 8) | 0x2a)              ,//������ָ��������Ϣ����ģʽ
	CMD_AMP_SLWR_OUTMSN                 = ((CMD_TYPE_AMP << 8) | 0x2b)              ,//���죺�˳���Ϣ���ţ������㲥3��
	CMD_AMP_SLWR_BGM_PLAYMODE           = ((CMD_TYPE_AMP << 8) | 0x2c)              ,//����ģʽ  
	CMD_AMP_SLWR_BGM_RADIO_NOHZ         = ((CMD_TYPE_AMP << 8) | 0x2d)              ,//����ָ��Ƶ�ʵ�ָ��ĵ�̨
	
	CMD_AMP_WIFI_SET                    = ((CMD_TYPE_AMP << 8) | 0x2E)              ,//ѡ��WIFI�����������������(�γ�ͥ ����:2015-10-15)
};

enum //GSMģ��
{	
//   CMD_PUBLIC_READ_CONFIG  			= ((CMD_TYPE_PRI << 8) | 0x01)  			,//��������Ϣ
//	 CMD_PUBLIC_WRITE_CONFIG      	    = ((CMD_TYPE_PRI << 8) | 0x81) 				,//д������Ϣ
//	 CMD_PUBLIC_SIMPLE_SWIT             = ((CMD_TYPE_PUBLIC << 8) | 0x20)           ,//���׿���ָ���������0��ʾ�أ���0��ʾ�������ֵ
//	 CMD_PUBLIC_SWIT                    = ((CMD_TYPE_PUBLIC << 8) | 0x22)		    ,//����ָ��  ��ָ��ĵ�һ������ 0��ʾ�أ�1��ʾ����2��ʾȡ�� ,���ݵĵ���λ�������޸ģ���ʾ���ض���
//   CMD_PUBLIC_SWIT_OPEN				= ((CMD_TYPE_PUBLIC << 8) | 0x23) 		    ,//��ָ��     
//	 CMD_PUBLIC_SWIT_CLOSE              = ((CMD_TYPE_PUBLIC << 8) | 0x24)		    ,//��ָ�� 
//   CMD_PUBLIC_SWIT_OPEN_CONDITION   	= ((CMD_TYPE_PUBLIC << 8) | 0x2b) 		    ,//��ָ��,ֻ�в�����ʱ��ִ��
//	 CMD_PUBLIC_SWIT_CLOSE_CONDITION    = ((CMD_TYPE_PUBLIC << 8) | 0x2c)		    ,//��ָ��,ֻ�в�����ʱ��ִ��

	CMD_GSM_POST_CONST_MESSAGE   		= ((CMD_TYPE_GSM << 8) | 0x01)  			,//���͹̻�����
	CMD_GSM_POST_CUSTOM_MESSAGE   		= ((CMD_TYPE_GSM << 8) | 0x02)  			,//�����Զ������
	CMD_GSM_READ_SAFE_MESSAGE   		= ((CMD_TYPE_GSM << 8) | 0x03)  			,//���������͹̻�����
	CMD_GSM_WRITE_SAFE_MESSAGE   		= ((CMD_TYPE_GSM << 8) | 0x04)  			,//д�������͹̻�����
};


enum //��������
{	
	CMD_NOLOCK_EXIT_SETADDR 		= ((CMD_TYPE_NO_LOCK << 8) | 0x01)				,//�������õ�ַ
	CMD_NOLOCK_WRITE_RCPACKET  		= ((CMD_TYPE_NO_LOCK << 8) | 0x02)				,//дҡ�����ݰ�  
	CMD_NOLOCK_READ_RCPACKET        = ((CMD_TYPE_NO_LOCK << 8) | 0x82)  			,//��ҡ�����ݰ�
	CMD_NOLOCK_WRITE_BTPACKET		= ((CMD_TYPE_NO_LOCK << 8) | 0x3)				,//дҡ�����ݰ�
	CMD_NOLOCK_READ_BTPACKET		= ((CMD_TYPE_NO_LOCK << 8) | 0x83)				,//��ҡ�����ݰ�
	CMD_NOLOCK_WRITE_NUM			= ((CMD_TYPE_NO_LOCK << 8) | 0x4)				,//д������������ 
	CMD_NOLOCK_READ_NUM             = ((CMD_TYPE_NO_LOCK << 8) | 0x84)				,//��������������
    
};


enum // ��������  	CMD_TYPE_RFLINE
{
	CMD_RFLINE_READ_DEV_LIST       		= ((CMD_TYPE_RFLINE << 8) | 0x01)			,//�������豸�б�
	CMD_RFLINE_WRITE_DEV_LIST   		= ((CMD_TYPE_RFLINE << 8) | 0x81)  			,//д�����豸�б�
	CMD_RFLINE_WRITE_DEVAC        		= ((CMD_TYPE_RFLINE << 8) | 0x82)	        ,//���ӻ�ɾ���豸
	CMD_RFLINE_WRITE_DEVAC_RSL          = ((CMD_TYPE_RFLINE << 8) | 0x83)	        ,//���ӻ�ɾ���豸���
}; 


enum // ָ���� 	  CMD_TYPE_FP_LOCK
{
	CMD_PF_LOCK_WRITE_STATE             = ((CMD_TYPE_FP_LOCK << 8) | 0x81)          ,//дָ����״̬
	CMD_PF_LOCK_WRITE_PASSWORD          = ((CMD_TYPE_FP_LOCK << 8) | 0x82)          ,//дָ��������
	CMD_RFLINE_READ_CFG2                = ((CMD_TYPE_FP_LOCK << 8) | 0x03)          ,//��ָ�����������α�־ָ��
	CMD_RFLINE_WRITE_CFG2               = ((CMD_TYPE_FP_LOCK << 8) | 0x83)          ,//дָ�����������α�־ָ��
	CMD_RFLINE_READ_CFG                 = ((CMD_TYPE_FP_LOCK << 8) | 0x04)          ,//��ר������
	CMD_RFLINE_WRITE_CFG                = ((CMD_TYPE_FP_LOCK << 8) | 0x84)          ,//дר������
}; 




#pragma pack(push) //(push)��(pop)Ҫ���, ����Ƕ��
#pragma pack(1)    //4->4�ֽڶ���, ��� 1,2,4,8,16



/***********************  �����Ǽ����õĲ��� ******************************/
enum //���ڸ�Ϊ��4λָ�����ܰ�����״̬����4λָ�������״̬
{
	KEY_TYPE_NULL        = 0		,//������Ч	       0
	KEY_TYPE_HIT            		,//������Ч	       1
	KEY_TYPE_LOOSEN	       		    ,//�ɿ���Ч	       2
	KEY_TYPE_SHORT          		,//�̰���Ч (1S)   3
	KEY_TYPE_LONG	       		    ,//������Ч (3S)   4
	KEY_TYPE_DBLCLICK       		,//˫����Ч	       5
	KEY_TYPE_SERIAL         		,//����������Ч    6
	KEY_TYPE_LAMP                   ,//�ƹ�����	       7
    KEY_TYPE_PRESS                   //�㶯�����¿����ɿ���  8
};    

//ָ������
enum
{ 
    //����ָ����Ƶ�һ������
    KEY_CMD_TYPE_SIMPLE_CHOSE   	= 0x80,      //������ָ�״̬ѡ����ֵ��Ч
	KEY_CMD_TYPE_SIMPLE_CHOOSE_INC_DEC,          //������ָ�״̬ѡ����ֵ��Ч
	KEY_CMD_TYPE_SIMPLE_CHOOSE_VALUE_INC_DEC,    //������ָ�״̬ѡ����ֵѡ��
	KEY_CMD_TYPE_SIMPLE_DIR,                     //������ָ�״ֱ̬������ֵ��Ч
	KEY_CMD_TYPE_SIMPLE_CHOOSE_CMD,              //������ָ�״ֱ̬������ֵ��Ч
	KEY_CMD_TYPE_SIMPLE_LR,                      //������ָ�״̬��Ч����ֵ��Ч
	
	//�����
	KEY_CMD_TYPE_CHOOSE   		     = 0x00,     //������ƶ���״̬ѡ����ֵ��Ч--��ƹ��ǿ��أ��������Դ����ѡ��DVD��CD��MP3��
	KEY_CMD_TYPE_CHOOSE_INC_DEC,        		 //������ƶ���״̬ѡ����ֵ��Ч������ָ����ֵ�Ĵ�С������ָ����ֵ�����ӻ��Ǽ��ٻ����ޱ仯--�細�����ƣ�ָ�����ڿ���أ�ͬʱ������������������
	KEY_CMD_TYPE_CHOOSE_VALUE_INC_DEC,  		 //������ƶ���״̬ѡ����ֵѡ��--������ȵĵƹ⣬ָ���ƹ⿪��أ�ͬʱָ������ 
	KEY_CMD_TYPE_DIR,                   		 //������ƶ���״ֱ̬������ֵ��Ч--��һ�η���һ�Σ����ı�ָ���е�״̬
	KEY_CMD_TYPE_CHOOSE_CMD,            		 //������ƶ���״ֱ̬������ֵ��Ч--����״ֵ̬�����Ͷ�Ӧ״ֵ̬��ָ�����20��ָ���ǰ״ֵ̬Ϊ18�����͵�18��ָ�ָ��Ϊ0~19��	        
	KEY_CMD_TYPE_LR,                  			 //������ƶ���״̬��Ч����ֵ��Ч--���ı�ָ���е���ֵ����ֵֻ��0��1����Ӧ�������Ӽ��ͷ�����ټ���0����ǰһ��ָ�1���ͺ�һ��ָ��        
};    


/*************************************************************************/

/************************** �����߰��� ***********************************/

typedef enum				//����������
{
	IR_AC = 1,				//�յ�
	IR_TV,					//����
	IR_SDVB,				//������
	IR_BLURAY,				//�����
	IR_HIFI,				//����
	IR_MAX,					
}IR_TYPE_ENUM;





/*������*/
typedef enum/*���ɸı��ö�ٵ�˳���ֵ*/
{
	KEY_SDVB_MUTE = 1,         //������             	
	KEY_SDVB_POWER,	           //��Դ��             	
	KEY_SDVB_MUNE,	           //�˵���             	
	KEY_SDVB_UP,	           //�ϼ�               	
	KEY_SDVB_RETURN,	       //����              	
	KEY_SDVB_LEFT,	           //���               	
	KEY_SDVB_OK,	           //OK ��              	
	KEY_SDVB_RIGHT,	           //�Ҽ�               	
	KEY_SDVB_jiemubiao,        //��Ŀ��                	
	KEY_SDVB_DOWN,	           //�¼�               	
	KEY_SDVB_EXIT,	           //�˳�               	
	KEY_SDVB_VOLUP,	           //�����Ӽ�           	
	KEY_SDVB_shengdao,         //����               	
	KEY_SDVB_CHUP,	           //Ƶ���Ӽ�           	
	KEY_SDVB_VOLDN,	           //��������           	
	KEY_SDVB_zixun,	           //��Ѷ              	
	KEY_SDVB_CHDN,	           //Ƶ������           	
	KEY_SDVB_PGUP,	           //�Ϸ�ҳ                	
	KEY_SDVB_PGDN,	           //�·�ҳ  
	KEY_SDVB_RED,	           //�� 
	KEY_SDVB_GREEN,	           //�� 
	KEY_SDVB_YELLOW,           //�� 
	KEY_SDVB_BLUE,	           //��             	              	
	KEY_SDVB_1,	               //���ּ�1            	
	KEY_SDVB_2,	               //���ּ�2            	
	KEY_SDVB_3,	               //���ּ�3            	
	KEY_SDVB_4,	               //���ּ�4            	
	KEY_SDVB_5,	               //���ּ�5            	
	KEY_SDVB_6,	               //���ּ�6            	
	KEY_SDVB_7,	               //���ּ�7            	
	KEY_SDVB_8,	               //���ּ�8            	
	KEY_SDVB_9,	               //���ּ�9            	
	KEY_SDVB_favor,	           //ϲ��                  	
	KEY_SDVB_0,	               //���ּ�0
	KEY_SDVB_INFO,	           //��Ϣ	  
	KEY_SDVB_TV_POWER,         //���ӿ�  
	KEY_SDVB_TV_VOLUP,		   //����������
	KEY_SDVB_TV_VOLDN,		   //����������
	KEY_SDVB_TV_AVTV,          //����AVTV
	KEY_SDVB_MAX,
}KEY_SDVB_ENUM;


/*  ���� */
typedef enum/*���ɸı��ö�ٵ�˳���ֵ*/
{
	KEY_TV_MUTE = 1,               //������             	
	KEY_TV_POWER,	               //��Դ��             	
	KEY_TV_MUNE,	               //�˵���             	
	KEY_TV_UP,	                   //�ϼ�               	
	KEY_TV_AVTV,	               //AV/TV              	
	KEY_TV_LEFT,	               //���               	
	KEY_TV_OK,	                   //OK ��              	
	KEY_TV_RIGHT,	               //�Ҽ�               	
	KEY_TV_pingxian,               //����               	
	KEY_TV_DOWN,	               //�¼�               	
	KEY_TV_wangfan,                //����               	
	KEY_TV_VOLUP,	               //�����Ӽ�           	
	KEY_TV_zhishi,                 //��ʽ             	
	KEY_TV_CHUP,	               //Ƶ���Ӽ�           	
	KEY_TV_VOLDN,	               //��������           	
	KEY_TV_zhengchang,             //����            	
	KEY_TV_CHDN,	               //Ƶ������           	
	KEY_TV_liyin,	               //����                	
	KEY_TV_banyin,	               //����  
	KEY_TV_PIP,	                   //���л�
	KEY_TV_SLEEP,	               //˯��          	              	
	KEY_TV_1,	       		       //���ּ�1            	
	KEY_TV_2,	      		       //���ּ�2            	
	KEY_TV_3,	                   //���ּ�3            	
	KEY_TV_4,	                   //���ּ�4            	
	KEY_TV_5,	                   //���ּ�5            	
	KEY_TV_6,	       		       //���ּ�6            	
	KEY_TV_7,	      		       //���ּ�7            	
	KEY_TV_8,	       		       //���ּ�8            	
	KEY_TV_9,	      		       //���ּ�9            	
	KEY_TV_qiehuan,                //-/--               	
	KEY_TV_0,	                   //���ּ�0            	
	KEY_TV_bili,	               //16:09
	KEY_TV_MAX,
}KEY_TV_ENUM;


/*  ���� */
typedef enum/*���ɸı��ö�ٵ�˳���ֵ*/
{
	KEY_HIFI_MUTE = 1,             //������             	
	KEY_HIFI_POWER,	               //��Դ��   
	KEY_HIFI_OPENCLOSE,            //������          	
	KEY_HIFI_MUNE,	               //�˵���             	
	KEY_HIFI_UP,	               //�ϼ�               	
	KEY_HIFI_LEFT,	               //���               	
	KEY_HIFI_OK,	               //OK ��              	
	KEY_HIFI_RIGHT,	               //�Ҽ�               		
	KEY_HIFI_DOWN,	               //�¼�               		
	KEY_HIFI_RETURN,               //����
	KEY_HIFI_VOLUP,	               //�����Ӽ�
	KEY_HIFI_VOLDN,	               //��������	
	KEY_HIFI_PRE,  			       //��һ��	 
	KEY_HIFI_NEXT,                 //��һ��
	KEY_HIFI_PLAYPAUSE,	           //������ͣ
	KEY_HIFI_STOP,	               //ֹͣ 
	KEY_HIFI_BASSUP,	           //����+
	KEY_HIFI_BASSDN,	           //����-
	KEY_HIFI_TREUP,	         	   //����+  
	KEY_HIFI_TREDN,	               //����-
	KEY_HIFI_1,	       		       //���ּ�1            	
	KEY_HIFI_2,	      		       //���ּ�2            	
	KEY_HIFI_3,	                   //���ּ�3            	
	KEY_HIFI_4,	                   //���ּ�4            	
	KEY_HIFI_5,	                   //���ּ�5            	
	KEY_HIFI_6,	       		       //���ּ�6            	
	KEY_HIFI_7,	      		       //���ּ�7            	
	KEY_HIFI_8,	       		       //���ּ�8            	
	KEY_HIFI_9,	      		       //���ּ�9            	
	KEY_HIFI_qiehuan,              //-/--               	
	KEY_HIFI_0,	                   //���ּ�0            	
	KEY_HIFI_kuohao,	           //(-)
	KEY_HIFI_MAX,
}KEY_HIFI_ENUM;



/*  ����� */
typedef enum/*���ɸı��ö�ٵ�˳���ֵ*/
{
	KEY_BLURAY_MUTE = 1,             //������
	KEY_BLURAY_POWER,	             //��Դ��
	KEY_BLURAY_OPENCLOSE,            //������
	KEY_BLURAY_MUNE,	             //�˵���
	KEY_BLURAY_UP,	                 //�ϼ�               	
	KEY_BLURAY_LEFT,	             //���               	
	KEY_BLURAY_OK,	                 //OK ��              	
	KEY_BLURAY_RIGHT,	             //�Ҽ�               		
	KEY_BLURAY_DOWN,	             //�¼�               		
	KEY_BLURAY_RETURN,               //����
	KEY_BLURAY_SHORTCUTMUNE,         //��ݲ˵�
	KEY_BLURAY_TITLE,				 //����
	KEY_BLURAY_VOLUP,	             //�����Ӽ�
	KEY_BLURAY_VOLDN,	             //��������	
	KEY_BLURAY_PRE,  			     //��һ��	 
	KEY_BLURAY_NEXT,                 //��һ��
	EKY_BLURAY_FF, 					 //���
	EKY_BLURAY_REW,					 //����
	KEY_BLURAY_PLAYPAUSE,	         //������ͣ
	KEY_BLURAY_STOP,	             //ֹͣ 
	KEY_BLURAY_RED,	                 //�� 
	KEY_BLURAY_GREEN,	             //�� 
	KEY_BLURAY_YELLOW,               //�� 
	KEY_BLURAY_BLUE,	             //�� 
	KEY_BLURAY_1,	       		     //���ּ�1            	
	KEY_BLURAY_2,	      		     //���ּ�2            	
	KEY_BLURAY_3,	                 //���ּ�3            	
	KEY_BLURAY_4,	                 //���ּ�4            	
	KEY_BLURAY_5,	                 //���ּ�5            	
	KEY_BLURAY_6,	       		     //���ּ�6            	
	KEY_BLURAY_7,	      		     //���ּ�7            	
	KEY_BLURAY_8,	       		     //���ּ�8            	
	KEY_BLURAY_9,	      		     //���ּ�9            	
	KEY_BLURAY_qiehuan,              //-/--               	
	KEY_BLURAY_0,	                 //���ּ�0            	
	KEY_BLURAY_kuohao,	             //(-)
	KEY_BLURAY_TV_POWER,			 //���ӵ�Դ
	KEY_BLURAY_TV_VOLUP,			 //����������
	KEY_BLURAY_TV_VOLDN,			 //����������
	KEY_RLURAY_TV_AVTV, 			 //����AVTVGģʽ
	KEY_BLURAY_MAX,
}KEY_BLURAY_ENUM;




/*************************************************************************/



#define CMD_CLOSE                     0				 //״̬��
#define CMD_OPEN                      1				 //״̬��
#define CMD_NOT                       2              //״̬ȡ��

//CMD_PUBLIC_SWIT   ����
#define CMD_SWIT_TYPE_LAMP_LOOP       0           //�ƹ��·  
#define CMD_SWIT_TYPE_LAMP_SCENE      1           //�ƹⳡ��  
#define CMD_SWIT_TYPE_LAMP_LIST       2           //�ƹ�ʱ��  
#define CMD_SWIT_TYPE_AC		      3           //�յ�  
#define CMD_SWIT_TYPE_HIFI		      4           //����

//�������� 
#define CMD_TEST_TYPE_KEYBOARD        0           //���̿�����
#define CMD_TEST_TYPE_SWIT            1           //������


//�������� ״̬
#define CMD_TRUE                                 0x55     //��ȷ
#define CMD_FALSE                                0xCC     //����
#define CMD_UNSAME                               0x11     //�����
#define CMD_BUSY                                 0x22     //æ 
#define CMD_ERR                                  0x33     //���ų��� 

//CMD_PUBLIC_WRITE_COMMAMD  ָ�����ͷ���  
#define COMMAMD_MESSAGE                          0x00     //����
#define COMMAND_VOICE                            0x01     //���� 
#define COMMAND_TIMER                            0x03     //��ʱ��
#define COMMAND_LOGIC                            0x04     //�߼�
#define COMMAND_KEY                              0x05     //����
#define COMMAND_CURTAIN                          0x06     //����



#define SCENE_ALL                          		 0xff     //����ȫ��
#define SCENE_MAX 								 0x08     //��ೡ��
#define LIST_MAX								 0x02     //���ʱ��
#define GROUP_MAX                                0x04     //������


//LOGIC �߼��� ��ʱ��ѭ��״̬
/**********  ѭ��ģʽ *****************/
#define REPEAT_MODE_NC          0x00      //��ѭ��    
#define REPEAT_MODE_YEAR        0x01      //��ѭ��
#define REPEAT_MODE_MONTH       0x02      //��ѭ��
#define REPEAT_MODE_DAY         0x03      //��ѭ��
#define REPEAT_MODE_WEEK        0x04      //����ѭ��

// �̵��������⣬�չ�Ƶȿ��س�ʼ״̬
#define INI_STATE_CLOSE          0x00     //�� 
#define INI_STATE_OPEN		     0x01     //��
#define INI_STATE_LAST           0x02	  //���ϵ�״̬


//��������
#define SAFETY_GRADE_OUTSIDE      0x01    //������������  bit0~bit6
#define SAFETY_GRADE_INSIDE       0x02    //������������  bit0~bit6
#define SAFETY_GRADE_ALL       	  0x7f    //��������ȫ��  bit0~bit6
#define SAFETY_ALARM           	  0x80    //��������      bit7
#define SAFETY_NC                 0       //���� 

//�Ŵ���ʾģʽ
#define WINDOWS_DISPLAY_SEQ       0x80	  //��һҳ��ʾ����  bit7 
#define WINDOWS_DISPLAY_ALL       0       //��ʾȫ���Ŵ�״̬
#define WINDOWS_DISPLAY_UNLOCK    1       //ֻ��ʾ���ź����ŵ�״̬

//�Ŵ�ִ��ָ��				    
#define WINDOWS_STATE_OPEN 		  0       //���ſ�ָ�
#define WINDOWS_STATE_CLOSE 	  1		  //���Ź�ָ�
#define WINDOWS_STATE_UNLOCK	  2		  //����û����ָ�
#define WINDOWS_STATE_LOCK  	  3		  







//��������ָ�

//��·��������ģʽ
#define SHORT_IN_MODE_NORMAL      0		  //��·���룬��ͨģʽ
#define SHORT_IN_MODE_WINDOWS 	  1		  //��·���룬�Ŵ�ģʽ


//������Ʋ���
#define DOORBELL_KEY              0       //���尴������
#define DOORBELL_OPEN             1       //����   
#define DOORBELL_ANSWER			  2 	  //Ӧ��
#define DOORBELL_END			  3		  //����


//����ָ��	 ��amp.h����
//#define BGM                       0      //������� ����
//#define MESSAGE  				    1	   //������� ��Ϣ
//
//#define CTRLP_BGMST_MP3           0      //�������� mp3 
//#define CTRLP_BGMST_RADIO		    1	   //�������� RADIO 
//#define CTRLP_BGMST_AUX1		    2	   //�������� AUX1
//#define CTRLP_BGMST_AUX2 		    3	   //�������� AUX2
//
//#define CTRLP_MSSAGEST_EMC        4      //��Ϣ--EMC
//#define CTRLP_MSSAGEST_WINDOWS    5      //��Ϣ--�Ŵ�
//
//#define CTRLP_PMD_PLY_ONE     	0		 //��������ģʽ ��������
//#define CTRLP_PMD_REP_ONE     	1		 //��������ģʽ ����ѭ��
//#define CTRLP_PMD_PLY_ALL         2		 //��������ģʽ ˳�򲥷�
//#define CTRLP_PMD_REP_ALL     	3		 //��������ģʽ ѭ������
//#define CTRLP_PMD_SHUFFLE     	4		 //��������ģʽ �������
//
//#define CTRLP_PMD_SIMPLE_COUNT    5      //��Ϣ����ģʽ �����ƴ�
//#define CTRLP_PMD_SIMPLE_TIME     6      //��Ϣ����ģʽ ������ʱ
//#define CTRLP_PMD_MULTI_COUNT     7      //��Ϣ����ģʽ �����ƴ�
//#define CTRLP_PMD_MULTI_TIME      8      //��Ϣ����ģʽ ������ʱ
//




//ͨ�����ݸ�ʽ
typedef struct _COMMUNICATION_FORMAT
{
	unsigned char  TargetId;     		//Ŀ��ID	0
	unsigned char  TargetNet;			//Ŀ������	1
	unsigned char  TargetType;          //Ŀ������	2
	unsigned char  SourceId;            //ԴID		3
	unsigned char  SourceNet;           //Դ���� 	4
	unsigned char  SourceType;          //Դ����	5
	unsigned char  Page;                //ҳ		6
	unsigned short Cmd;                 //������  ��8λ��Ϊ16λ��  7
	unsigned char  Len;                 //����					   9
	unsigned char  Data[128];           //�����128��(�ر�ע��:������CRCУ��4�ֽ�)
}COMMUNICATION_FORMAT;


//Ŀ�ڸ�ʽ
typedef struct _TIME_FORMAT
{
	unsigned char Year;                 //��
	unsigned char Month;                //��
	unsigned char Day;                  //�� 
	unsigned char Week;                 //���� 
	unsigned char Hour;                 //ʱ    
	unsigned char Min;                  //��
	unsigned char Sec;                  //��  
}TIME_FORMAT;

//�豸���Ƹ�ʽ
typedef struct _EQUIP_INF_FORMAT
{
	unsigned char  Id;
	unsigned char  Net;
	unsigned char  Type;					
	unsigned char  Mac[12];              //12λmac��ַ
	unsigned short Location;             //λ�� λ�÷�Ϊ16λ����8λ��ʾ¥�㣬��8λ��ʾ�����
	unsigned char  Name[30];             //��·���ƣ��30�ֽ�
}EQUIP_INF_FORMAT;

//�豸���Ƹ�ʽ
typedef struct _LOOP_NAME_FORMAT
{
	unsigned short Location;             //λ�� λ�÷�Ϊ16λ����8λ��ʾ¥�㣬��8λ��ʾ�����
	unsigned char  Photo;                //ͼƬ��
	unsigned char  Name[30];             //��·���ƣ��30�ֽ�
}LOOP_NAME_FORMAT;


//���������õĲ���
typedef struct _KEY_CONFIG
{
	unsigned char  KeyType;              //������������,��4λ��ʾ���������4λ��ʾ���ܼ�,������Ч���ɿ���Ч��      ��KEY_TYPE
	unsigned char  CmdType;              //ָ������(��Ӧ�Ŀ���)�� ֱ�����Ӽ���	 ��CMD_TYPE
	unsigned char  EquipType;            //�豸���� 		         --  ����
	
	unsigned char  FuncKeyData;	         //���ܼ���Ӧ������  �������ǰ�ɺڰ�Һ������Ϊ˫��ʱ��ͬʱ���µĹ���
	unsigned char  FuncKeyDataStep;      //���ܼ���������ֵ
	unsigned char  FuncKeyDataMin;       //���ܼ�������Сֵ
	unsigned char  FuncKeyDataMax;       //���ܼ��������ֵ
	
	unsigned char  DirecKeyData;	     //�������Ӧ������  ��ֵ,
	unsigned char  DirecKeyDataStep;     //����            
	unsigned char  DirecKeyDataMin;      //�����������Сֵ 
	unsigned char  DirecKeyDataMax;      //������������ֵ
	
	unsigned char  Conjunction;          //�����ţ����а����й�������ͬ�ģ������ͬһ����
	unsigned char  Mutex;                //�������0��ʾû���⣬1~255��ʾ�����, ͬһ��ҳ���У���ͬ����İ�������ͬһ���͵Ķ���ĳ�������£�����İ�����Ҫ����	
}KEY_CONFIG;


//����ָ���洢��ʽ
typedef struct _CMD_FORMAT
{
	unsigned char  TargetId;             //Ŀ��ID
	unsigned char  TargetNet;            //Ŀ������
	unsigned char  TargetType;           //Ŀ������
	
	unsigned short Cmd;			         //������
	unsigned char  Len;      			 //����
	unsigned char  Data[30];      		 //���ݣ��30�ֽ�
}CMD_FORMAT;


//�ƹ��·��������
typedef struct _CMD_LOOP_FORMAT
{
	unsigned char  Switch; 		         //����
	unsigned char  Dimmer;        	     //����
	
	unsigned char   Loop;		         //��·
	unsigned short  RunTime;  			 //����ʱ��
	unsigned short  OpenDelayTime;       //���ӳ�
	unsigned short  CloseDelayTime;      //���ӳ�   
}CMD_LOOP_FORMAT;


//�ƹⳡ����������
typedef struct _CMD_SENSE_FORMAT
{
	unsigned char  Switch; 		         //����
	unsigned char  Nc;        	     	 //����
	
	unsigned char  Group;		         //��
	unsigned char  Scene;  			 	 //�ڸ�������
	unsigned short RunTime;    			 //����ʱ��	
}CMD_SCENE_FORMAT;


//�ƹ�ȫ����������
typedef struct 
{
	unsigned char  Switch; 		         //����
	unsigned char  Nc;        	     	 //����
	
	unsigned char  RunTime;		         //����ʱ��
}CMD_SWIT_ALL_FORMAT;



//�ƹ�ʱ���������
typedef struct _CMD_LIST_FORMAT
{
	unsigned char  Switch; 		         //����
	unsigned char  Nc;        	     	 //����
	
	unsigned char  Group;		         //��
	unsigned char  List;  			 	 //�ڼ���
	unsigned short Times;    			 //���д���
}CMD_LIST_FORMAT;



//����������Ϣ
typedef struct _CMD_SENSE_LOOP_INF
{
	unsigned char  Loop; 		         //��·
	unsigned char  Dimmer;        	     //����
}CMD_SCENE_LOOP_INF;


//����������Ϣ
typedef struct _CMD_SENSE_INF
{
	unsigned char  Group; 		         //��
	unsigned char  Scene;        	     //����
	CMD_SCENE_LOOP_INF Data[SCENE_MAX];	 	 //��·������
}CMD_SCENE_INF;


//����������Ϣ
typedef struct _CMD_LIST_SENSE_INF
{
	unsigned char   Scene; 		         //����
	unsigned short  Time;        	     //ʱ��
}CMD_LIST_SCENE_INF;

//ʱ��������Ϣ
typedef struct _CMD_LIST_INF
{
	unsigned char   Group; 		         //��
	unsigned char   List;        	     //ʱ��
	unsigned char   Logic; 				 //�߼���ϵ
//	unsigned short  Times;               //����	
	CMD_LIST_SCENE_INF Data[SCENE_MAX];	 	 //��·������
}CMD_LIST_INF;



//���ع�������Ϣ
typedef struct _CMD_PROCESS_LOOP_INF
{
	unsigned char  Type;        	     //����
	unsigned char  OpenProcessTime; 	 //������ʱ��
	unsigned char  CloseProcessTime;     //�ع���ʱ��
	unsigned char  DimmerMax;			 //���ֵ
	unsigned char  DimmerMin;            //��Сֵ
	unsigned char  Line;                 //���� 
}CMD_PROCESS_LOOP_INF; 


//������ָ��
typedef struct _CMD_PUBLIC_READ_DATA_FORMAT
{
	unsigned short  Cmd;     			 //ָ��
	unsigned char   Data[128];           //�����128��
}CMD_PUBLIC_READ_DATA_FORMAT;



//���ع�������Ϣ
typedef struct _CMD_PROCESS_INF
{
	CMD_PROCESS_LOOP_INF Loop[12];		 //��·
}CMD_PROCESS_INF; 


typedef struct
{
	unsigned short Cmd;			  		 //����ָ��
}
CMD_WRITE_END;





#pragma pack(pop)
#endif  //_SMART_LIGHT_H


