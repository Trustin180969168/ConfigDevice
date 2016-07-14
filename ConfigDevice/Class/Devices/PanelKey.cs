using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class PanelKey : Device
    {
        public Circuit Circuit;//回路对象
        public PanelCtrl KeyCtrl;//按键对象


        public PanelKey(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public PanelKey(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }

        public PanelKey(DataRow dr)
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
            KeyCtrl = new PanelCtrl(this);
            ContrlObjs.Add("回路", this.Circuit); 
        }

 

    }


}
