using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.GameModel.Scenario.Gameworld.PlayerList.Player.Resources;

namespace ZRTSModel
{
    public class PlayerResources : ModelComponent
    {
        private int gold = 0;
        private int wood = 0;
        private int metal = 0;

        public override void Accept(ModelComponentVisitor visitor)
        {
            if (visitor is PlayerResourcesVisitor)
            {
                ((PlayerResourcesVisitor)visitor).Visit(this);
            }
            else
            {
                base.Accept(visitor);
            }
        }

        public int Gold
        {
            get
            {
                return gold;
            }
            set
            {
                gold = value;
            }
        }

        public int Wood
        {
            get
            {
                return wood;
            }
            set
            {
                wood = value;
            }
        }

        public int Metal
        {
            get
            {
                return metal;
            }
            set
            {
                metal = value;
            }
        }
    }
}
