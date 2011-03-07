using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    /// <summary>
    /// A particular unit.  Contains an action queue.
    /// </summary>
    [Serializable()]
    public abstract class UnitComponent : ModelComponent
    {
        public override void Accept(ModelComponentVisitor visitor)
        {
            if (visitor is UnitVisitor)
            {
                ((UnitVisitor)visitor).Visit(this);
            }
            else
            {
                base.Accept(visitor);
            }
        }
    }
}
