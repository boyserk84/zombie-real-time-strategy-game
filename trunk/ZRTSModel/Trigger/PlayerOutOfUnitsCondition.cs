using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;

namespace ZRTSModel.Trigger
{
    class PlayerOutOfUnitsCondition : Condition
    {
        private Player.Player player = null;

        public PlayerOutOfUnitsCondition(Trigger decorated, Player.Player player)
            : base(decorated)
        {
            this.player = player;
        }

        public override bool CheckMyCondition(Scenario.Scenario scenario)
        {
            throw new NotImplementedException();
        }

        override public void Visit(EntityList entitylist)
        {
            isMet = true;
            foreach (Entity e in EntityList.GetList())
            {
                if (e.getOwner() == player)
                {
                    isMet = false;
                    break;
                }
            }
            needsToBeEvaled = false;
        }
    }
}
