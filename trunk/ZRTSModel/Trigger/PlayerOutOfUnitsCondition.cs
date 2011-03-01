using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
