using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class SpecialPanelKey : Device
    {
        public Circuit Circuit;//回路对象
        public SpecialPanelCtrl PanelCtrl;//按键对象


        public SpecialPanelKey(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public SpecialPanelKey(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }

        public SpecialPanelKey(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            Circuit = new Circuit(this, 8);
            PanelCtrl = new SpecialPanelCtrl(this);
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, this.Circuit); 
        }

 

    }


}
