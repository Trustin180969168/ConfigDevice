using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class MotorParameterData
    {
        public int MaxRunTime = 0;//---最大运行时间---
        public int MaxStopCE = 0;//---最大停止电流---


        public MotorParameterData()
        {
        }

        public MotorParameterData(int maxRunTime,int maxStopCE)
        {
            MaxRunTime = maxRunTime;
            MaxStopCE = maxStopCE;
        }

        public byte[] GetValue()
        {
            byte[] value = new byte[4];
            byte[] maxRunTimeByte = ConvertTools.GetByteFromInt16(MaxRunTime);
            byte[] maxStopCEByte = ConvertTools.GetByteFromInt16(MaxStopCE); 
            Buffer.BlockCopy(maxRunTimeByte, 0, value, 0, 2);
            Buffer.BlockCopy(maxStopCEByte, 0, value, 2, 2);
            return value;
        }


    }
}
