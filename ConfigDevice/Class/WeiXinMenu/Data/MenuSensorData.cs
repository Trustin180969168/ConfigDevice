using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    /// <summary>
    /// 设备值
    /// </summary>
    public class MenuSensorData
    {

        public byte ByteDeviceID = 0;                   //设备ID
        public byte ByteDeviceNetworkID = 0;            //设备网段ID
        public byte ByteDeviceKindID = 0;               //设备类型ID
        public byte[] ByteDeviceName = new byte[30];          //设备名称 

        public int DeviceID { get { return (int)ByteDeviceID; } }
        public int DeviceNetworkID { get { return (int)ByteDeviceNetworkID; } }
        public int DeviceKindID { get { return (int)ByteDeviceKindID; } }

        public string DeviceName
        {
            get { return Encoding.GetEncoding("GB2312").GetString(ByteDeviceName).TrimEnd('\0').Trim().Replace("", ""); }
            set
            {
                byte[] temp = Encoding.GetEncoding("GB2312").GetBytes(value);
                if (temp.Length > 30)
                {
                    CommonTools.MessageShow("名称长度过大!", 2, ""); 
                }
                else
                    Buffer.BlockCopy(temp, 0, ByteDeviceName, 0, temp.Length);
            }
        }

        public MenuSensorData(byte[] data)
        {
            ByteDeviceID = data[0];
            ByteDeviceNetworkID = data[1];
            ByteDeviceKindID = data[2];
            Buffer.BlockCopy(data, 3, ByteDeviceName, 0, 30);
         } 



        /// <summary>
        /// 获取设备值
        /// </summary>
        /// <returns></returns>
        public byte[] Value()
        {
            byte[] value = new byte[33];
            value[0] = ByteDeviceID;
            value[1] = ByteDeviceNetworkID;
            value[2] = ByteDeviceKindID;  
            Buffer.BlockCopy(ByteDeviceName,0, value, 3, 30); 

            return value;
        }
    }

    /// <summary>
    /// 逻辑数据
    /// </summary>
    public class MenuSensorSettingData
    {
        public UInt32 MenuId;//第几个菜单  (从0开始计数)
        public byte KindId;//控制类型   （指出是哪个控制类型的配置数据）(MS_COBJ_AMP等)即:菜单类型
        public MenuSensorData[] MenuDeviceDataList = new MenuSensorData[4];//--4组逻辑数据         

        public byte[] ByteMenuId = new byte[4];

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userData">用户数据</param>
        public MenuSensorSettingData(UserUdpData userData)
        {
            byte[] data = userData.Data;
            MenuId = ConvertTools.Bytes4ToUInt32(data[0], data[1], data[2], data[3]);
            Buffer.BlockCopy(data, 0, ByteMenuId, 0, 4);
            KindId = data[4];
            byte[] byteArrData = CommonTools.CopyBytes(data, 5, data.Length - 4);
            GetDeviceData(byteArrData);
        }

        public MenuSensorSettingData()
        {
        }

        private void GetDeviceData(byte[] data)
        {

            byte[] deviceData1 = CommonTools.CopyBytes(data, 0, 33);
            byte[] deviceData2 = CommonTools.CopyBytes(data, 33, 33);
            byte[] deviceData3 = CommonTools.CopyBytes(data, 66, 33);
            byte[] deviceData4 = CommonTools.CopyBytes(data, 99, 33);
            MenuDeviceDataList[0] = new MenuSensorData(deviceData1);
            MenuDeviceDataList[1] = new MenuSensorData(deviceData2);
            MenuDeviceDataList[2] = new MenuSensorData(deviceData3);
            MenuDeviceDataList[3] = new MenuSensorData(deviceData4);
        }

        /// <summary>
        /// 获取逻辑数据值
        /// </summary>
        /// <returns>byte[]</returns>
        public byte[] GetValue()
        {
            byte[] value = new byte[5+ 33*4];
            Buffer.BlockCopy(ByteMenuId, 0, value, 0, 4);
            value[4] = KindId;
            Buffer.BlockCopy(MenuDeviceDataList[0].Value(), 0, value, 5, 33);
            Buffer.BlockCopy(MenuDeviceDataList[1].Value(), 0, value, 38, 33);
            Buffer.BlockCopy(MenuDeviceDataList[2].Value(), 0, value, 71, 33);
            Buffer.BlockCopy(MenuDeviceDataList[3].Value(), 0, value, 104, 33);

            return value;
        }

    }

}
