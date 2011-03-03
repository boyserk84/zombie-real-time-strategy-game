using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.GameModel.Scenario.Gameworld.PlayerList.Player.Resources
{
    public interface PlayerResourcesVisitor
    {
        void Visit(PlayerResources resources);
    }
}
