using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    /// <summary>
    /// A piece of model representing a player.  A player has a UNIQUE name, a race, and resources.  It also contains as children
    /// a building and unit list.
    /// </summary>
    [Serializable()]
    public class PlayerComponent : ModelComponent
    {
        // TODO: Replace race with a Race class component.
        private string name;
        private string race;

        public PlayerComponent()
        {
            PlayerResources resources = new PlayerResources();
            AddChild(resources);

            UnitList unitList = new UnitList();
            AddChild(unitList);
        }

        public int GetGold()
        {
            return GetResources().Gold;
        }

        public int GetWood()
        {
            return GetResources().Wood;
        }

        public int GetMetal()
        {
            return GetResources().Metal;
        }

        public void SetGold(int amt)
        {
            GetResources().Gold = amt;
        }

        public void SetWood(int amt)
        {
            GetResources().Wood = amt;
        }

        public void SetMetal(int amt)
        {
            GetResources().Metal = amt;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetRace()
        {
            return race;
        }

        public void SetRace(string race)
        {
            this.race = race;
        }

        public PlayerResources GetResources()
        {
            foreach (ModelComponent component in GetChildren())
            {
                if (component is PlayerResources)
                {
                    return (PlayerResources)component;
                }
            }
            return null;
        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public UnitList GetUnitList()
        {
            UnitList list = null;
            foreach (ModelComponent component in GetChildren())
            {
                if (component is UnitList)
                {
                    list = (UnitList)component;
                    break;
                }
            }
            return list;
        }
    }
}
