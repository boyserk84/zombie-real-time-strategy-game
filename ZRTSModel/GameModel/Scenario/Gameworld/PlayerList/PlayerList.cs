using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    [Serializable()]
    public class PlayerList : ModelComponent
    {
        public override void Accept(ModelComponentVisitor visitor)
        {
            if (visitor is PlayerListVisitor)
            {
                ((PlayerListVisitor)visitor).Visit(this);
            }
            else
            {
                base.Accept(visitor);
            }
        }

        public override void AddChild(ModelComponent child)
        {
            if (child is PlayerComponent)
            {
                base.AddChild(child);
            }
        }
    }
}
