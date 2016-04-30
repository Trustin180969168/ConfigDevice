using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace ProgramTest
{

    class Program
    {
        //响应处理方法  
        private static void MyButton_Click1(string msg)
        {
            Console.Out.WriteLine("MyButton_Click1" + msg);
        }

        //响应处理方法  
        private static void MyButton_Click2(string msg)
        {
            Console.Out.WriteLine("MyButton_Click2" + msg);
        }  

        static void Main(string[] args)
        {
            //byte temp = 0x81;//第一个字节
            //int numHas = (int)(temp >> 7);
            //int num = 0x7F & temp;
            //Console.Out.WriteLine("has:"+numHas.ToString()+num.ToString());
            /*

            List<Position> listP = new List<Position>();
            listP.Add(new Position(4, "4", true));
            listP.Add(new Position(2, "2", true));
            listP.Add(new Position(7, "7", true));
            listP.Add(new Position(3, "3", true));
            listP.Add(new Position(5, "5", true));
            listP.Add(new Position(1, "1", true));
            listP.Sort(new PositionCompare());

            foreach (Position p in listP)
            {
                Console.Out.WriteLine("P.NUM=" + p.Num);
            }

            Dictionary<byte[], string> test = new Dictionary<byte[], string>();
            byte[] a = new byte[2] { 0x85, 0xFE };
            byte[] CMD_PC_WRITE_LOCALL_NAME = new byte[2] { 0x85, 0xFE };//14.<RJ45-PC> 写位置名称
            test.Add(CMD_PC_WRITE_LOCALL_NAME, "85FE");

            if (test.ContainsKey(CMD_PC_WRITE_LOCALL_NAME))
                Console.Out.WriteLine("有 85FE");
            else
                Console.Out.WriteLine("没有 85FE");
           

 
            MyButton mb = new MyButton("MyButton对象");
            mb.Click += MyButton_Click1;
            mb.Click += MyButton_Click1;
            mb.Click += MyButton_Click2;
            mb.OnClick();  
            Console.Read();
             
             */

        } 
    }
}
