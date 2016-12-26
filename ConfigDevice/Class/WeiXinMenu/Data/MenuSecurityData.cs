﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{

    public class MenuSecurityData
    {
        public byte ByteSecurityKindID = 0;//是否布防,布防类型，Bit0-1表示室外布防，Bit1-1表示全部布防
        public byte ByteSecurityHomeCancelID = 0;//是否撤防，Bit0-1表示回家键撤防
        public UInt32 MenuId;//第几个菜单  (从0开始计数)
        public byte KindId;//控制类型   （指出是哪个控制类型的配置数据）(MS_COBJ_AMP等)即:菜单类型

        public MenuSecurityData(UserUdpData userUdpData)
        {
            byte[] data = userUdpData.Data;
            MenuId = ConvertTools.Bytes4ToUInt32(data[0], data[1], data[2], data[3]);
            KindId = data[4];
            ByteSecurityKindID = data[5];
            ByteSecurityHomeCancelID = data[6];
        }

        public MenuSecurityData() { }

        /// <summary>
        /// 是否室外布防
        /// </summary>
        public bool IsSecurityOutside
        {
            get { return (ByteSecurityKindID & 1) == 1 ? true : false; }
            set { ByteSecurityKindID = (byte)(ByteSecurityKindID | 1); }
        }

        /// <summary>
        /// 是否全部布防
        /// </summary>
        public bool IsSecurityAll
        {
            get { return (ByteSecurityKindID & 2) == 2 ? true : false; }
            set { ByteSecurityKindID = (byte)(ByteSecurityKindID | 2); }
        }

        /// <summary>
        /// 是否回家键撤防
        /// </summary>
        public bool IsSecurityHomeCancel
        {
            get { return (ByteSecurityHomeCancelID & 1) == 1 ? true : false; }
            set { ByteSecurityHomeCancelID = value ? (byte)1 : (byte)0; }
        }

        /// <summary>
        /// 是否不关联安防
        /// </summary>
        public bool IsSecurityNone
        {
            get { return (ByteSecurityKindID | 0) == 0 ? true : false; }
            set
            {
                ByteSecurityKindID = 0;
            }
        }
    }
}