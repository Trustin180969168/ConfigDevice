using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{
    public class ButtonPanelKey : Device
    {
        public KeyCircuit Circuit;//回路对象
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
            switch (this.ByteKindID)
            {
                case DeviceConfig.EQUIPMENT_KEY_2: Circuit = new KeyCircuit(this, 2); break;
                case DeviceConfig.EQUIPMENT_KEY_3: Circuit = new KeyCircuit(this, 3); break;
                case DeviceConfig.EQUIPMENT_KEY_4: Circuit = new KeyCircuit(this, 4); break;
                case DeviceConfig.EQUIPMENT_KEY_5: Circuit = new KeyCircuit(this, 5); break;
                case DeviceConfig.EQUIPMENT_KEY_6: Circuit = new KeyCircuit(this, 6); break;
                case DeviceConfig.EQUIPMENT_KEY_7: Circuit = new KeyCircuit(this, 7); break;
                case DeviceConfig.EQUIPMENT_KEY_8: Circuit = new KeyCircuit(this, 8); break;
                default: Circuit = new KeyCircuit(this, 2); break;
            }
            PanelCtrl = new ButtonPanelCtrl(this);
            ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, this.Circuit);
        }



    }


}
