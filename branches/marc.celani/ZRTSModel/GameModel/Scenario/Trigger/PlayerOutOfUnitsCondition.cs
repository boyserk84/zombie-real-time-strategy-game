using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;

namespace ZRTSModel.Trigger
{
    [Serializable()]
    class PlayerOutOfUnitsCondition : Condition
    {
        private PlayerComponent player = null;

        public PlayerOutOfUnitsCondition(Trigger decorated, PlayerComponent player)
            : base(decorated)
        {
            this.player = player;
        }

        public override bool CheckMyCondition(Scenario.Scenario scenario)
        {
            throw new NotImplementedException();
        }


        public void Visit(UnitComponent unit)
        {
            isMet = false;
        }

        override public void Visit(UnitList list)
        {
            isMet = true;
            foreach (ModelComponent unit in list.GetChildren())
            {
                unit.Accept(this);
                if (!isMet)
                {
                    // We have found a Unit, therefore we can exit this logic.
                    break;
                }
            }
            needsToBeEvaled = false;
        }

        override public void Visit(ModelComponent component)
        {
            // Do nothing
        }
    }
}
