using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
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
        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            if (visitor is PlayerVisitor)
            {
                ((PlayerVisitor)visitor).Visit(this);
            }
            else
            {
                base.Accept(visitor);
            }
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
    }
}
