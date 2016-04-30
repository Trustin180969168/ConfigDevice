using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public abstract class CommandData//:ICommandData
    {
        public string Name = "";//命令名称
        public byte TargetId;//目标ID
        public byte TargetNet;//目标网段
        public byte TargetType;//目标类型

        public byte[] Cmd = new byte[2];//命令
        public byte Len;//长度
        public byte[] Data = new byte[30];//数据最长30字节

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_name">名字</param>
        public CommandData(string _name)
        {
            Name = _name;
        }

        /// <summary>
        /// 创建指令
        /// </summary>
        /// <returns>byte[]</returns>
        public abstract byte[] CreateCommand();
        /// <summary>
        /// 参数表
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, System.Type> GetParameterTypeList();
        /// 指令类型
        /// </summary>
        /// <returns></returns>
        public abstract List<string> GetCommandKindList();

    }

    /// <summary>
    /// 回路指令
    /// </summary>
    public class CircuitCommand : CommandData
    {
        public static Dictionary<string, System.Type> ParameterTypeList = new Dictionary<string, System.Type>();//参数表
        public static List<string> CommandKindList = new List<string>();//参数表

        public byte ucFuncVol;//开或关
        public byte ucStepVol;//亮度
        public byte ucLoopNum;//第几个回路,0表示第一个
        public byte usRunTime;//运动时间单位S
        public byte usOpenDly;//开延迟时间单位S
        public byte usCloseDly;//关延时时间单位S

        static CircuitCommand()
        {
            ParameterTypeList.Add("回路", System.Type.GetType("System.Int16"));
            ParameterTypeList.Add("亮度", System.Type.GetType("System.Int16"));
            ParameterTypeList.Add("运行时间", System.Type.GetType("System.DateTime"));
            ParameterTypeList.Add("开延时", System.Type.GetType("System.DateTime"));
            ParameterTypeList.Add("关延时", System.Type.GetType("System.DateTime"));

            CommandKindList.Add("开关回路");
            CommandKindList.Add("开回路");
            CommandKindList.Add("关回路");
        }

        public CircuitCommand(string _name)
            : base(_name)
        {

        }

        /// <summary>
        /// 生成指令数据
        /// </summary>
        /// <returns></returns>
        public override byte[] CreateCommand()
        {
            return null;
        }

        /// <summary>
        /// 获取参数类型表
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, System.Type> GetParameterTypeList()
        {
            return ParameterTypeList;
        }

        /// <summary>
        /// 获取指令类型
        /// </summary>
        /// <returns></returns>
        public override List<string> GetCommandKindList()
        {
            return CommandKindList;
        }




    }





}
