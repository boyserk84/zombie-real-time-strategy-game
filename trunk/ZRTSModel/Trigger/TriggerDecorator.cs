using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Trigger
{
    abstract class TriggerDecorator : Trigger
    {
        private Trigger decorated = null;
        protected bool isMet;
        protected bool needsToBeEvaled = true;

        private TriggerDecorator()
        {

        }

        public TriggerDecorator(Trigger decorated)
        {
            this.decorated = decorated;
        }

        public void PerformAction(Scenario.Scenario scenario)
        {
            decorated.PerformAction(scenario);
            PerformAction(scenario);
        }

        public bool Eval(Scenario.Scenario scenario)
        {
            bool myCondition = NeedsToBeEvaluated() ? CheckMyCondition(scenario) : IsMet();
            return myCondition && decorated.Eval(scenario);
        }

        public bool IsMet()
        {
            return isMet;
        }

        public bool NeedsToBeEvaluated()
        {
            return needsToBeEvaled;
        }

        public abstract void PerformMyAction(Scenario.Scenario scenario);
        public abstract bool CheckMyCondition(Scenario.Scenario scenario);


    }
}
