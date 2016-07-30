using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class Scene : ControlObj
    {

        public const string NAME_CMD_SW_SWIT_SCENE = "开关场景";
        public const string NAME_CMD_SW_SWIT_SCENE_OPEN = "开场景";
        public const string NAME_CMD_SW_SWIT_SCENE_CLOSE = "关场景";


        public Scene(Device _deviceCtrl)
            : base(_deviceCtrl)
        {
            Name = DeviceConfig.CONTROL_OBJECT_SCENE_NAME; 
        }



    }




}
