using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConfigDevice
{

    public class Road3Window : Device
    {
        public Motor Motor;//---电机对象----
        public Circuit Circuit;//---回路----
        public Road3Window(UserUdpData userUdpData)
            : base(userUdpData)
        {
            initControlObjs();
        }

        public Road3Window(DataRow dr)
            : base(dr)
        {
            initControlObjs();
        }

        public Road3Window(DeviceData data)
            : base(data)
        {
            initControlObjs();
        }

        /// <summary>
        /// 初始化控制对象
        /// </summary>
        private void initControlObjs()
        {
            this.Motor = new Motor(this);
            this.Circuit = new Circuit(this,3);
            ContrlObjs.Add("电机",this.Motor);
            ContrlObjs.Add("回路", this.Circuit);
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

        }

    }


}
