using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    /// <summary>
    /// A queue of actions to be taken over time.
    /// </summary>
    [Serializable()]
    public class ActionQueue : ModelComponent
    {
        public override void Accept(ModelComponentVisitor visitor)
        {
            if (visitor is ActionQueueVisitor)
            {
                ((ActionQueueVisitor)visitor).Visit(this);
            }
            else
            {
                base.Accept(visitor);
            }
        }
    }
}
