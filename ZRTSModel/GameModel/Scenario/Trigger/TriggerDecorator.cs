using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Trigger
{
    [Serializable()]
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

        public void PerformActions()
        {
            decorated.PerformActions();
            PerformMyAction();
        }

        public bool Eval()
        {
            bool myCondition = NeedsToBeEvaluated() ? CheckMyCondition() : IsMet();
            return myCondition && decorated.Eval();
        }

        public bool IsMet()
        {
            return isMet;
        }

        public bool NeedsToBeEvaluated()
        {
            return needsToBeEvaled;
        }

        public abstract void PerformMyAction();
        public abstract bool CheckMyCondition();
    }
}
