using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Trigger
{
    [Serializable()]
    class EmptyTrigger : Trigger
    {
        public void PerformAction(Scenario.Scenario scenario)
        {
            // Do nothing
        }

        public bool Eval(Scenario.Scenario scenario)
        {
            return true;
        }

        public bool NeedsToBeEvaluated()
        {
            return false;
        }

        public bool IsMet()
        {
            return true;
        }
    }
}
