using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Messages : ControlObj
    {
        public const string NAME_CMD_SWIT_ALL_MUSIC = "开关音乐";
        public const string NAME_CMD_SWIT_OPEN_MUSIC = "开关音乐";
        public const string NAME_CMD_SWIT_CLOSE_MUSIC = "关音乐";

        public const string NAME_SOURCE_URGENCY = "紧急";
        public const string NAME_SOURCE_DOOR_WINDOW = "门窗";

        public const string NAME_PLAY_ONE_COUNT = "单曲计次";
        public const string NAME_PLAY_ONE_COUNT_TIME = "单曲计时";
        public const string NAME_PLAY_MORE_COUNT = "多曲计次";
        public const string NAME_PLAY_MORE_COUNT_TIME = "多曲计时";
        public const string NAME_PLAY_INVALID = "无效";

        public Messages(DeviceData _deviceCtrl)
        {
            Name = "消息";
            deviceControled = _deviceCtrl;
        }

    }




}
