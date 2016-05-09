using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ConfigDevice
{
    //
    // 摘要:UdpTools.cs
    //     用于根据返回的UDP数据生成UdpData实例,或根据参数生成Udp指令
    //
    public static class UdpTools
    {
        /*
         * 
         *
            	---------------------------------------------------------------------------
            	系统标识符：用于区分不同的系统，如：“AYLSON smarthome”  （占16字节<字符>）
            	系统识别码：用于区分在同一网络中同类型系统，目前固定为“0”     （占2字节）
            	---------------------------------------------------------------------------
            	包标识码：本包数据的包标识码，用于标识<发送>与<返回>是一对；    （占2字节）
            	包数据类：本包数据类型:发送数据,返回确认/数据；                 （占1字节）
            	包 属 性：本包是:(’B’广播)(’M’组播)(’U’单播)        （占1字节）
            	接收端口：发送此包数据设备的接收端口(如{D2,04}接收端口为1234) （占2字节）
            	---------------------------------------------------------------------------
            	用户协议：用户使用哪种自定义协议 或其它扩展功能                 （占4字节）
            	用户数据：用户自定义通讯协议数据；                              （0~n字节）
            	---------------------------------------------------------------------------
            	校 验 和：保留（本包数据校验和）       
             
             * 
            1．通信数据包格式：
            设备ID + 网段ID + 类型号+ 源设备ID + 源网段ID + 类型+ 页 + 控制字（2字节） + 长度+ 数据 + 4byte CRC
            长度 =  数据 + 4byte CRC
            页：用于指出当数据太长时被分割成多少次发送，高4位指出共有多少页，低4位指出当前的数据是第几次数据，当两者相等时则表示数据已发送完成。
            例：如果只需要发送一次数据则可完成，则页为0x11
            一次通信的最大为128个字节，如果超过则需要切割，切割后控制字后的第一个数据在每组中都需要发送。

            2．地址的分配：见SmartLight.H

            3．通信方式：CSMA/CD
            当有按键按下时自行发送数据，等待接收方返回数据，失败重发，最多重发三次。注意：如果数据有冲突，不算在重发三次的范围内。

         * 
         * 
         * */




        /// <summary>
        /// 根据设备发送包生成回复包
        /// </summary>
        /// <param name="udpDevice">发送的包</param>
        /// <returns>设备回复包</returns>
        public static void ReplyDeviceDataUdp(UdpData udpDevice)
        {
            //---udpDevice---41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 00 00 35 00 01 55 CB 2B 02 00 00 00 C9 5C FE 3E 5C 13 11 82 FF 1B 56 FF 71 06 49 86 51 52 32 14 18 87 07 00 B5 DA B0 CB D0 D0 D6 D0 00 0F 6A 92 C2 50 
            //---udpReply----41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 00 00 35 00 02 42 1C 25 02 00 00 00 55 5C 
            //---udpDevice---41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 00 00 0D 00 01 55 CB 2B 02 00 00 00 C9 5C FE FC 5C F0 11 32 FF 04 26 70 0F 0C 48 
            //---udpReply----41 59 4C 53 4F 4E 20 73 6D 61 72 74 68 6F 6D 65 00 00 0D 00 02 42 1C 25 02 00 00 00 55 5C 
            UdpData udpReply = new UdpData();
            udpReply.PacketCode = udpDevice.PacketCode;
            udpReply.PacketKind[0] = PackegeSendReply.REPLY;
            udpReply.PacketProperty[0] = BroadcastKind.Broadcast;
            udpReply.SendPort = SysConfig.ByteLocalPort;
            udpReply.Protocol = UserProtocol.Device;
            udpReply.ProtocolData = new byte[] { REPLY_RESULT.CMD_TRUE };
            udpReply.CheckCodeAdd[0] = udpDevice.ProtocolData[1];
            udpReply.Length = 30;

            MySocket.GetInstance().ReplyData(udpReply, udpDevice.IP, SysConfig.RemotePort);
        }

        /// <summary>
        /// 申请返回结果
        /// </summary>
        /// <param name="udpReply"></param>
        /// <param name="values"></param>
        public static void CallbackRequestResult(UdpData udpReply, object[] values)
        {
            string errStr = (string)values[0];
            if (udpReply.ReplyByte != REPLY_RESULT.CMD_TRUE)
                CommonTools.ShowReplyInfo(errStr, udpReply.ReplyByte);
        }


    }




}
