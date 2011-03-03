using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Entities
{
    [Serializable()]
    public class Building : StaticEntity
    {
        public Type type = Type.Building;

        public Building(Player.Player owner, short health, byte width, byte height) 
            : base(owner, health, width, height)
        {

            this.entityType = EntityType.Building;
        }
    }
}
