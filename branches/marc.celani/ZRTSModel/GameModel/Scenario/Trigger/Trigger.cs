using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Trigger
{
    interface Trigger
    {
        void PerformActions();
        bool Eval();
        bool NeedsToBeEvaluated();
        bool IsMet();
    }
}
