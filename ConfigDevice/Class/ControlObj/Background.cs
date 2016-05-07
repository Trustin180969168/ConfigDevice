using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    
    public class Background : ControlObj
    {
        public const string NAME_CMD_SWIT_ALL_MUSIC="开关音乐";
        public const string NAME_CMD_SWIT_OPEN_MUSIC="开关音乐";
        public const string NAME_CMD_SWIT_CLOSE_MUSIC = "关音乐";

        public const string NAME_SOURCE_MP3 = "MP3";
        public const string NAME_SOURCE_RADIO = "收音机";
        public const string NAME_SOURCE_AUX1 = "AUX1";
        public const string NAME_SOURCE_AUX2 = "AUX2";

        public const string NAME_PLAY_ORDER_ONE = "单曲播放";
        public const string NAME_PLAY_ORDER_ONE_LOOP = "单曲循环";
        public const string NAME_PLAY_ORDER = "顺序播放";
        public const string NAME_PLAY_ORDER_LOOP = "循环播放";
        public const string NAME_PLAY_ORDER_RANDOM = "随机播放";
        public const string NAME_PLAY_INVALID = "无效";

        public Background(Device _deviceCtrl)
        {
            Name = "背景";
            deviceControled = _deviceCtrl;
        }

    }




}
