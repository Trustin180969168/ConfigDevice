using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class ButtonPanelKey : Device
    {
        public Circuit Circuit;//回路对象
        public ButtonPanelCtrl PanelCtrl;//按键对象


        public ButtonPanelKey(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public ButtonPanelKey(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }

        public ButtonPanelKey(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            Circuit = new Circuit(this, 2);
            PanelCtrl = new ButtonPanelCtrl(this);
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, this.Circuit); 
        }

 

    }


}
