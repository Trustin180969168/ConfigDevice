using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDevice
{
    public class BaseCommand : CommandData
    {
        public BaseCommand(string _name)
            : base(_name)
        {

        }

        public override byte[] CreateCommand()
        {
            throw new NotImplementedException();
        }
    }
}
