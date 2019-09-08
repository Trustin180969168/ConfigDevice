﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using ConfigDevice.DeviceUI;

namespace ConfigDevice
{

    public static class DeviceCtrl
    {

        private static object lockUpdateObj = new object();
        /// <summary>
        /// 初始化
        /// </summary>
        public static void InitDtDevice()
        {
            //----初始化表结构-------
            if (SysConfig.DtDevice.Columns.Count == 0)
            {
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_NUM, System.Type.GetType("System.Int16"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_ID, System.Type.GetType("System.Int16"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_NETWORK_ID, System.Type.GetType("System.Int16"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_KIND_ID, System.Type.GetType("System.Int16"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_KIND_NAME, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_NAME, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_MAC, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_STATE, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_REMARK, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_SOFTWARE_VER, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_HARDWARE_VER, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PC_ADDRESS, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_NETWORK_IP, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_ADDRESS_NAME, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_ADDRESS_ID, System.Type.GetType("System.String"));

                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PARAMETER1, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PARAMETER2, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PARAMETER3, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PARAMETER4, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_PARAMETER5, System.Type.GetType("System.String"));
                SysConfig.DtDevice.Columns.Add(DeviceConfig.DC_IMAGE1, System.Type.GetType("System.Byte[]"));
            }
        }

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="deviceData">设备数据</param>
        public static void AddDeviceData(DeviceData device)
        {
            lock (lockUpdateObj)
            {
                int num = SysConfig.DtDevice.Select(DeviceConfig.DC_KIND_ID +
                    " not in ('" + (int)DeviceConfig.EQUIPMENT_RJ45 + "', '" + (int)DeviceConfig.EQUIPMENT_SERVER + "')").Length + 1;
                if (device.ByteKindID == DeviceConfig.EQUIPMENT_SERVER || device.ByteKindID == DeviceConfig.EQUIPMENT_RJ45)
                    num = 1000 + num;
                DataRow drInsert = SysConfig.DtDevice.Rows.Add(new object[] {num.ToString(),device.DeviceID,device.NetworkID,
                            device.KindID, device.KindName,device.Name,device.MAC,device.State,device.Remark,"","",device.PCAddress,
                            device.NetworkIP,device.AddressName,device.AddressID});
                drInsert[DeviceConfig.DC_PARAMETER1] = DeviceConfig.STATE_OPEN_LIGHT;
                drInsert[DeviceConfig.DC_IMAGE1] = ImageHelper.ImageToBytes(global::ConfigDevice.Properties.Resources.on);

                drInsert.AcceptChanges();
            }
        }



        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="device">设备</param>
        public static void UpdateDeviceData(DeviceData device)
        {
            lock (lockUpdateObj)
            {
                string temp = DeviceConfig.DC_MAC + "='" + device.MAC + "'";
                DataRow[] rows = SysConfig.DtDevice.Select(temp);
                foreach (DataRow dr in SysConfig.DtDevice.Rows)
                {
                    if (dr[DeviceConfig.DC_MAC].ToString() == device.MAC)
                    {
                        dr.BeginEdit();

                        dr[DeviceConfig.DC_ID] = device.DeviceID;
                        dr[DeviceConfig.DC_NETWORK_ID] = device.NetworkID;
                        dr[DeviceConfig.DC_KIND_ID] = device.KindID;
                        dr[DeviceConfig.DC_KIND_NAME] = device.KindName;
                        dr[DeviceConfig.DC_NAME] = device.Name;
                        dr[DeviceConfig.DC_STATE] = device.State;
                        dr[DeviceConfig.DC_REMARK] = device.Remark;
                        dr[DeviceConfig.DC_PC_ADDRESS] = device.PCAddress;
                        dr[DeviceConfig.DC_NETWORK_IP] = device.NetworkIP;
                        dr[DeviceConfig.DC_ADDRESS_NAME] = device.AddressName;

                        dr.EndEdit();
                        dr.AcceptChanges();
                        return;
                    }
                }
            }
        }

    }
 
    /// <summary>
    /// 创建设备编辑界面
    /// </summary>
    public static class FactoryDevice
    {
        /// <summary>
        /// 获取抽象设备编辑
        /// </summary>
        /// <param name="kindId">类型</param>
        /// <returns></returns>
        public static IFactoryDeviceEdit GetFactoryDeviceEdit(byte kindId)
        {
            switch (kindId)
            {
                case DeviceConfig.EQUIPMENT_AMP_MP3:
                case DeviceConfig.EQUIPMENT_RJ45: return new FactoryBaseDeviceEdit();//-------RJ45,wifi功放---------
                case DeviceConfig.EQUIPMENT_SWIT_4:
                case DeviceConfig.EQUIPMENT_SWIT_6:
                case DeviceConfig.EQUIPMENT_SWIT_8:
                case DeviceConfig.EQUIPMENT_SWIT_12:
                case DeviceConfig.EQUIPMENT_TRAILING_2:
                case DeviceConfig.EQUIPMENT_TRAILING_4: 
                case DeviceConfig.EQUIPMENT_TRAILING_6: 
                case DeviceConfig.EQUIPMENT_TRAILING_8: 
                case DeviceConfig.EQUIPMENT_TRAILING_12: return new FactoryDriver();//----创建驱动器（4路，6路，8路，12路,前12,前6.....）-----     
                case DeviceConfig.EQUIPMENT_DOOR_IN_4: return new FactoryDoor4InputEdit();//------门输入4--------
                case DeviceConfig.EQUIPMENT_FUEL_GAS: return new FactoryFlammableGasProbeEdit();//----可燃气体-----
                case DeviceConfig.EQUIPMENT_WEATHER: return new FactoryWeatherStationEdit();//-----气象站-----
                case DeviceConfig.EQUIPMENT_CURTAIN_3CH:  
                case DeviceConfig.EQUIPMENT_CURTAIN_8CH: return new FactoryMotorDCEditEdit();//----直流电机-----
                case DeviceConfig.EQUIPMENT_AIR_QUALITY: return new FactoryEnvironmentEdit();//-----环境-----
                case DeviceConfig.EQUIPMENT_AIR_O2: return new FactoryO2Edit();//-----氧气传感器-----
                case DeviceConfig.EQUIPMENT_RSP: return new FactoryRadarEdit();//----雷达----
                case DeviceConfig.EQUIPMENT_KEY_2: return new FactoryButton2Edit();//----2按键----
                case DeviceConfig.EQUIPMENT_KEY_4: return new FactoryButton4Edit();//----4按键----
                case DeviceConfig.EQUIPMENT_SKEY_8: return new FactoryButton8Edit();//----8按键----
                case DeviceConfig.EQUIPMENT_KEY_TFT_LCD: return new FactoryFunctionsBoardEdit();//---多功能液晶面板---
                case DeviceConfig.EQUIPMENT_SHORT_IN_4: return new FactoryShort4InputEdit();//---短路输入4---
                case DeviceConfig.EQUIPMENT_PRI_3: return new FactoryBodyInductionEdit();//---人体感应---
                case DeviceConfig.EQUIPMENT_RFLINE_GATEWAY: return new FactoryWirlessDeviceEdit();//---无线主机---
                case DeviceConfig.EQUIPMENT_FP_LOCK: return new FactoryFingerLockDeviceEdit();//---指纹锁---
                case DeviceConfig.EQUIPMENT_EL_CUPBOARD: return new FactoryCupboardDeviceEdit();//---指纹锁---
                case DeviceConfig.EQUIPMENT_EL_DOORWINDOW: return new FactoryMaglevDoorDeviceEdit();//---悬浮门---
                default: return new FactoryBaseDeviceEdit();
            }
        }


        /// <summary>
        /// 获取抽象设备
        /// </summary>
        /// <param name="kindId">类型</param>
        /// <returns></returns>
        public static IFactoryDevice CreateDevice(byte kindId)
        {
            switch (kindId)
            {
                case DeviceConfig.EQUIPMENT_AMP_MP3: return new FactoryAmplifier();
                case DeviceConfig.EQUIPMENT_DOOR_IN_4: return new FactoryDoorInput4();
                case DeviceConfig.EQUIPMENT_CURTAIN_3CH: 
                case DeviceConfig.EQUIPMENT_CURTAIN_8CH: return new FactoryMotorWindow();
                case DeviceConfig.EQUIPMENT_SWIT_4: return new FactoryRoad4Relay();//4路继电器
                case DeviceConfig.EQUIPMENT_SWIT_8: return new FactoryRoad8Relay();//6路继电器
                case DeviceConfig.EQUIPMENT_SWIT_6: return new FactoryRoad6Relay();//8路继电器
                case DeviceConfig.EQUIPMENT_SWIT_12: return new FactoryRoad12Relay();//12路继电器
                case DeviceConfig.EQUIPMENT_TRAILING_2: return new FactoryRoad2FrontDimming();//2路前沿调光
                case DeviceConfig.EQUIPMENT_TRAILING_4: return new FactoryRoad4FrontDimming();//4路前沿调光
                case DeviceConfig.EQUIPMENT_TRAILING_6: return new FactoryRoad6FrontDimming();//6路前沿调光
                case DeviceConfig.EQUIPMENT_TRAILING_8: return new FactoryRoad8FrontDimming();//8路前沿调光
                case DeviceConfig.EQUIPMENT_TRAILING_12: return new FactoryRoad12FrontDimming();//12路前沿调光
                case DeviceConfig.EQUIPMENT_SERVER: return new FactoryServers();//服务器
                case DeviceConfig.EQUIPMENT_KEY_2: return new FactoryButton2();//2按键 
                case DeviceConfig.EQUIPMENT_KEY_4: return new FactoryButton4();//4按键 
                case DeviceConfig.EQUIPMENT_SKEY_8: return new FactoryButton8();//8按键 
                case DeviceConfig.EQUIPMENT_LINKID: return new FactorySystemInteraction();//系统联动号
                case DeviceConfig.EQUIPMENT_EL_CUPBOARD: return new FactoryCupboard();//电动升降柜
                case DeviceConfig.EQUIPMENT_EL_DOORWINDOW: return new FactoryMaglevDoor();//悬浮门
                default: return new FactoryBaseDevice();
            }
        }
    }




    //**********************设备编辑***************************
    public interface IFactoryDeviceEdit
    {
        FrmDevice CreateDevice(DataRow data);
    }

    /// <summary>
    /// 一般设备,如:功放
    /// </summary>
    public class FactoryBaseDeviceEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            Device device = new BaseDevice(data);
            return new FrmBaseDevice(device);
        }
        #endregion
    }

    /// <summary>
    /// 门输入4
    /// </summary>
    public class FactoryDoor4InputEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            DoorInput4 input4 = new DoorInput4(data);
            return new FrmDoorInput4(input4);
        }
        #endregion
    }

    /// <summary>
    /// 驱动器
    /// </summary>
    public class FactoryDriver : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {         
            DeviceData deviceData = new DeviceData(data);//设备数据
            Device driver = FactoryDevice.CreateDevice(deviceData.ByteKindID).CreateDevice(deviceData);//--根据类型新建设备对象---
            return new FrmDriver(driver);
        }
        #endregion
    }

    /// <summary>
    /// 可燃气体探头
    /// </summary>
    public class FactoryFlammableGasProbeEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            FlammableGasProbe flammableGasProbe = new FlammableGasProbe(data);
            return new FrmFlammableGasProbe(flammableGasProbe);
        }
        #endregion
    }

    /// <summary>
    /// 气象站
    /// </summary>
    public class FactoryWeatherStationEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            Environment environment = new Environment(data);
            return new FrmWeather(environment);
        }
        #endregion
    }

    /// <summary>
    /// 环境
    /// </summary>
    public class FactoryEnvironmentEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            Environment environment = new Environment(data);
            return new FrmEnvironment(environment);
        }
        #endregion
    }

    /// <summary>
    /// 3路直流电机
    /// </summary>
    public class FactoryMotorDCEditEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            MotorWindow device = new MotorWindow(data);
            return new FrmMotorDC(device);
        }
        #endregion
    }


    /// <summary>
    /// 氧气传感器
    /// </summary>
    public class FactoryO2Edit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            Environment environment = new Environment(data);
            return new FrmO2(environment);
        }
        #endregion
    }

    /// <summary>
    /// 雷达传感器
    /// </summary>
    public class FactoryRadarEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            Radar radar = new Radar(data);
            return new FrmRadar(radar);
        }
        #endregion
    }

    /// <summary>
    /// 2按键
    /// </summary>
    public class FactoryButton2Edit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            ButtonPanelKey button = new ButtonPanelKey(data);
            return new FrmCommonButton(button);
        }
        #endregion
    }

    /// <summary>
    /// 4按键
    /// </summary>
    public class FactoryButton4Edit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            ButtonPanelKey button = new ButtonPanelKey(data);
            return new FrmCommonButton(button);
        }
        #endregion
    }

    /// <summary>
    /// 8按键
    /// </summary>
    public class FactoryButton8Edit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            SpecialPanelKey button = new SpecialPanelKey(data);
            return new FrmButton8(button);
        }
        #endregion
    }

    /// <summary>
    /// 多功能液晶面板
    /// </summary>
    public class FactoryFunctionsBoardEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            LCDPanelKey button = new LCDPanelKey(data);
            return new FrmFunctionsBoard(button);
        }
        #endregion
    }

    /// <summary>
    /// 短路输入4
    /// </summary>
    public class FactoryShort4InputEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            Short4 device = new Short4(data);
            return new FrmShort4(device);
        }
        #endregion
    }

    /// <summary>
    /// 人体感应
    /// </summary>
    public class FactoryBodyInductionEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员
        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            BodyInduction device = new BodyInduction(data);
            return new FrmBodyIndction(device);
        }
        #endregion
    }

    /// <summary>
    /// 无线机
    /// </summary>
    public class FactoryWirlessDeviceEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员

        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            WirlessTransform device = new WirlessTransform(data);
            return new FrmWirlessDevices(device);
        }
        #endregion
 
    }


    /// <summary>
    /// 指纹锁
    /// </summary>
    public class FactoryFingerLockDeviceEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员

        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            VirtualPasswordFingerMarkLock device = new VirtualPasswordFingerMarkLock(data);
            return new FrmLockDevices(device);
        }
        #endregion

    }

    /// <summary>
    /// 升降柜
    /// </summary>
    public class FactoryCupboardDeviceEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员

        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            Cupboard device = new Cupboard(data);
            return new FrmCupboard(device);
        }
        #endregion

    }

    /// <summary>
    /// 悬浮门
    /// </summary>
    public class FactoryMaglevDoorDeviceEdit : IFactoryDeviceEdit
    {
        #region IFactory 成员

        FrmDevice IFactoryDeviceEdit.CreateDevice(DataRow data)
        {
            Maglev device = new Maglev(data);
            return new FrmMaglevDoor(device);
        }
        #endregion

    }

    //**********************设备***************************

    public interface IFactoryDevice
    {
        Device CreateDevice(DeviceData data);
    }
    /// <summary>
    /// 基础设备
    /// </summary>
    public class FactoryBaseDevice : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new BaseDevice(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 功放
    /// </summary>
    public class FactoryAmplifier : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Amplifier(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 门输入4
    /// </summary>
    public class FactoryDoorInput4 : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new DoorInput4(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 门窗电机
    /// </summary>
    public class FactoryMotorWindow : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new MotorWindow(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 4路继电器
    /// </summary>
    public class FactoryRoad4Relay : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Road4Relay(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 6路继电器
    /// </summary>
    public class FactoryRoad6Relay : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Road6Relay(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 12路继电器
    /// </summary>
    public class FactoryRoad12Relay : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Road6Relay(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 2路前沿调光
    /// </summary>
    public class FactoryRoad2FrontDimming : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Road2FrontDimming(data);
            return device;
        }

        #endregion
    }


    /// <summary>
    /// 4路前沿调光
    /// </summary>
    public class FactoryRoad4FrontDimming : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Road4FrontDimming(data);
            return device;
        }

        #endregion
    }


    /// <summary>
    /// 6路前沿调光
    /// </summary>
    public class FactoryRoad6FrontDimming : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Road6FrontDimming(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 8路前沿调光
    /// </summary>
    public class FactoryRoad8FrontDimming : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Road8FrontDimming(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 12路前沿调光
    /// </summary>
    public class FactoryRoad12FrontDimming : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Road12FrontDimming(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 8路继电器
    /// </summary>
    public class FactoryRoad8Relay : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Road8Relay(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 服务器
    /// </summary>
    public class FactoryServers : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Server(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 2按键
    /// </summary>
    public class FactoryButton2 : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new LCDPanelKey(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 4按键
    /// </summary>
    public class FactoryButton4 : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new LCDPanelKey(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 8按键
    /// </summary>
    public class FactoryButton8 : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new SpecialPanelKey(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 系统联动号
    /// </summary>
    public class FactorySystemInteraction : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new SystemInteraction(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 电动升降柜
    /// </summary>
    public class FactoryCupboard : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Cupboard(data);
            return device;
        }

        #endregion
    }

    /// <summary>
    /// 悬浮门
    /// </summary>
    public class FactoryMaglevDoor : IFactoryDevice
    {
        #region IFactoryDevice 成员

        public Device CreateDevice(DeviceData data)
        {
            Device device = new Maglev(data);
            return device;
        }

        #endregion
    }

}