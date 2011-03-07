using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Trigger
{
    interface Trigger : ModelComponentVisitor
    {
        void PerformAction(Scenario.Scenario scenario);
        bool Eval(Scenario.Scenario scenario);
        bool NeedsToBeEvaluated();
        bool IsMet();
    }
}
