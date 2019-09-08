using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class MotorWindow : Device
    {
        public Motor Motor;//---电机对象----
        public Circuit Circuit;//---回路----
        public MotorWindow(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public MotorWindow(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        public MotorWindow(DeviceData data)
            : base(data)
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
                case DeviceConfig.EQUIPMENT_CURTAIN_8CH: this.Motor = new Motor(this, 8); break;
                case DeviceConfig.EQUIPMENT_CURTAIN_2CH: this.Motor = new Motor(this, 2); break;
                case DeviceConfig.EQUIPMENT_CURTAIN_3CH: this.Motor = new Motor(this, 3); break;
                case DeviceConfig.EQUIPMENT_FUEL_GAS: this.Motor = new Motor(this, 3); break;
                default: this.Motor = new Motor(this,3); break;
            }
            switch (this.ByteKindID)
            {
                case DeviceConfig.EQUIPMENT_CURTAIN_8CH: this.Circuit = new Circuit(this, 8); break;
                case DeviceConfig.EQUIPMENT_CURTAIN_2CH: this.Circuit = new Circuit(this, 2); break;
                case DeviceConfig.EQUIPMENT_CURTAIN_3CH: this.Circuit = new Circuit(this, 3); break;
                default: this.Circuit = new Circuit(this, 3); break;
            }
 
            ContrlObjs.Add("电机",this.Motor);
            //ContrlObjs.Add(DeviceConfig.CONTROL_OBJECT_CIRCUIT_NAME, this.Circuit);
        }

        /// <summary>
        /// 取消对应指令所有回调订阅
        /// </summary>
        public void RemoveRJ45Callback()
        {
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_STATE);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_END);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_CONFIG);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_LOOP_NAME);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_PUBLIC_WRITE_VER);
            SysCtrl.RemoveRJ45CallBackList(DeviceConfig.CMD_WINDOWS_WRITE_POWER);
        }

    }


}
