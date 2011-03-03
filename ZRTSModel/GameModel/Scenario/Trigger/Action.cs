using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Trigger
{
    [Serializable()]
    abstract class Action : TriggerDecorator
    {

        public Action(Trigger decorated)
            : base(decorated)
        {
            needsToBeEvaled = false;
            isMet = true;
        }

        override public bool CheckMyCondition(Scenario.Scenario scenario)
        {
            return true;
        }

    }
}
