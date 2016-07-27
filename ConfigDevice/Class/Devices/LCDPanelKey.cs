using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class LCDPanelKey : Device
    {
        public Circuit Circuit;//回路对象
        public LCDPanelCtrl PanelCtrl;//按键对象


        public LCDPanelKey(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public LCDPanelKey(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }

        public LCDPanelKey(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            Circuit = new Circuit(this, 26);
            PanelCtrl = new LCDPanelCtrl(this);
            ContrlObjs.Add("回路", this.Circuit); 
        }

 

    }


}
