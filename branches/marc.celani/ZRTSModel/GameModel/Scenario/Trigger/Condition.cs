using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Trigger
{
    [Serializable()]
    abstract class Condition : TriggerDecorator
    {
        public Condition(Trigger decorated)
            : base(decorated)
        {

        }

        override public void PerformMyAction()
        {
            // Do nothing
        }
    }
}
