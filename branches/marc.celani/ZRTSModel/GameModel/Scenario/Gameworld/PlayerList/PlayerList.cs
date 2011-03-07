using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    /// <summary>
    /// A list of players - can only contain players.
    /// </summary>
    [Serializable()]
    public class PlayerList : ModelComponent
    {
        public override void AddChild(ModelComponent child)
        {
            if (child is PlayerComponent)
            {
                base.AddChild(child);
            }
        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
