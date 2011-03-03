using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Trigger
{
    [Serializable()]
    class EndGameAction : Action
    {

        public EndGameAction(Trigger decorated)
            : base(decorated)
        {
        }
        public override void PerformMyAction(Scenario.Scenario scenario)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ModelComponent component)
        {
            throw new NotImplementedException();
        }
    }
}
