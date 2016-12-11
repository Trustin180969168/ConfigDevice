using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ConfigDevice 
{


    public class DoorWindowMenuViews : BaseMenuView
    {

        public DoorWindowMenuViews(WeiXin device, Control editControl, MenuData editData)
            : base(device, editControl, editData)
        {

        }

        public override void LoadEditData()
        {
      
        }

      
    }


    public class EntranceDoor : BaseMenuView
    {

             public EntranceDoor(WeiXin device, Control editControl, MenuData editData)
            : base(device, editControl, editData)
        {

        }
        public override void LoadEditData()
        {

        }

    }

    public class MoreElse : BaseMenuView
    {

        public MoreElse(WeiXin device, Control editControl, MenuData editData)
            : base(device, editControl, editData)
        {

        }
        public override void LoadEditData()
        {

        }

    }


}
