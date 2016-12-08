using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class MenuSecurityData
    {
        public byte ByteSecurityKindID=0;//是否布防,布防类型，Bit0-1表示室外布防，Bit1-1表示全部布防
        public byte ByteSecurityHomeCancelID = 0;//是否撤防，Bit0-1表示回家键撤防

    }
}
