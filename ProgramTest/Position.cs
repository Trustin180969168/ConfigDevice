using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramTest
{
    public class Position 
    {
        public const string DC_NUM = "Num";
        public const string DC_NAME = "Name";
        public const string DC_HAS_PASSWORD = "HasPassword";
        public int Num = 0;//位置编号
        public string Name = "";//位置名称
        public bool HasPassword;//是否有密码

        public Position(int num, string name, bool has)
        {
            Num = num;
            Name = name;
            HasPassword = has;
        }


        byte[] aa = { 0x00, 0x00 };


    }

    public class PositionCompare : IComparer<Position>
    {
        #region IComparer<Position> 成员

        public int Compare(Position x, Position y)
        {
            if (x.Num < y.Num)
                return -1;
            else
                return 1;

            
        }

        #endregion


    }




}
