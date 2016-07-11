using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    //----------------------------------------------------------------------------------------------------
    //                                                                                 ┌实质须用[按键无效]
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 0:0:  按键无效:按键有效│→ 变暗调光      (按下有效)
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 0:0:  松开有效:按键有效│→ 关+变暗调光   (松开有效)
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 0:0:  松开有效:按键无效│→ 关            (按下有效等等)
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 0:0:┌非松有效:本参忽略┤→ 关            (按下有效等等)
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 1:1:│按键无效:按键有效│→ 变亮调光      (按下有效)
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 1:1:│松开有效:按键有效│→ 开+变亮调光   (松开有效)
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 1:1:│松开有效:按键无效│→ 开            (按下有效等等)
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 1:1:├非松有效:本参忽略┤→ 开            (按下有效等等)
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 0:1:│按键无效:按键有效│→ 循环调光      (按下有效)
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 0:1:│松开有效:按键有效│→ 开关+循环调光 (松开有效)
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 0:1:│松开有效:按键无效│→ 开关          (按下有效等等)
    //FuncKeyDataMin:FuncKeyDataMax:KeyType高4位:KeyType低4位 = 0:1:├非松有效:本参忽略┘→ 开关          (按下有效等等)
    //                              注:建议使用[按下有效]<-按键有效 ┘             其它  → 无效键
    //----------------------------------------------------------------------------------------------------
    

    public class KeyData
    {
        public byte KeyNum = 0;         //第几个按键
        public byte KeyCtrlKind = 0;    //按键类型
        public byte KeyKindID = 0;        //指令类型
        public byte DeviceKind = 0;     //设备类型

        public byte FunctionInitialValue = 0;   //功能键初值
        public byte FunctionDataFloatingStep = 0;   //浮动步进
        public byte FunctionDataMinValue = 0;   //功能键最小值
        public byte FunctionDataMaxValue = 0;   //功能键最大值

        public byte DirectionInitialValue = 0;   //方向键初值
        public byte DirectionDataFloatingStep = 0;   //方向步进
        public byte DirectionMinValue = 0;   //方向键最小值
        public byte DirectionMaxValue = 0;   //方向键最大值
        public byte RelevanceNum = 0;   //关联号 (0表示没有关联号，关联号是告诉知另一个控制按键已改变)
        public byte MutexNum = 0;   //互斥   (0表示没互斥，是同一个控制面板，多个按键之间同一时刻，最多只有一个被选中)



        public KeyData(UdpData udp)
        {
            byte[] data = udp.ProtocolData;

            KeyNum = data[0];

            KeyCtrlKind = data[1];    //按键类型
            KeyKindID = data[2];        //指令类型
            DeviceKind = data[3];     //设备类型

            FunctionInitialValue = data[4];   //功能键初值
            FunctionDataFloatingStep = data[5];   //浮动步进
            FunctionDataMinValue = data[6];   //功能键最小值
            FunctionDataMaxValue = data[7];   //功能键最大值

            DirectionInitialValue = data[8];   //方向键初值
            DirectionDataFloatingStep = data[9];   //方向步进
            DirectionMinValue = data[10];   //方向键最小值
            DirectionMaxValue = data[11];   //方向键最大值
            RelevanceNum = data[12];   //关联号 (0表示没有关联号，关联号是告诉知另一个控制按键已改变)
            MutexNum = data[13];   //互斥   (0表示没互 

        } 

        public static Dictionary<int, string> KeyKindIDName = new Dictionary<int, string>();

        static KeyData()
        {
            KeyKindIDName.Add((int)DeviceConfig.KeyKind.KEY_TYPE_NULL, "按键无效");
            KeyKindIDName.Add((int)DeviceConfig.KeyKind.KEY_TYPE_HIT, "按下有效");
            KeyKindIDName.Add((int)DeviceConfig.KeyKind.KEY_TYPE_LOOSEN, "松开有效");
            KeyKindIDName.Add((int)DeviceConfig.KeyKind.KEY_TYPE_SHORT, "短按有效");
            KeyKindIDName.Add((int)DeviceConfig.KeyKind.KEY_TYPE_LONG, "长按有效");
            KeyKindIDName.Add((int)DeviceConfig.KeyKind.KEY_TYPE_DBLCLICK, "双击按键");
            KeyKindIDName.Add((int)DeviceConfig.KeyKind.KEY_TYPE_SERIAL, "链接按键");
            KeyKindIDName.Add((int)DeviceConfig.KeyKind.KEY_TYPE_LAMP, "灯光");
            KeyKindIDName.Add((int)DeviceConfig.KeyKind.KEY_TYPE_PRESS, "开关");
        }

    }



}
