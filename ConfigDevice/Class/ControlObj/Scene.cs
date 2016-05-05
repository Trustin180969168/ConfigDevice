using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Scene : ControlObj
    {

        public const string NAME_CMD_SW_SWIT_SCENE = "开关回路";
        public const string NAME_CMD_SW_SWIT_SCENE_OPEN = "开回路";
        public const string NAME_CMD_SW_SWIT_SCENE_CLOSE = "关回路";


        public Scene(DeviceData _deviceCtrl)
        {
            Name = "场景";
            deviceControled = _deviceCtrl;
        }



    }




}
