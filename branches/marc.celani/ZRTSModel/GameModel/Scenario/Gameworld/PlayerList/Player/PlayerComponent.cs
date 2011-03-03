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
            return 0;
            throw new NotImplementedException();
        }

        public int GetWood()
        {
            return 0;
            throw new NotImplementedException();
        }

        public int GetMetal()
        {
            return 0;
            throw new NotImplementedException();
        }

        public void SetGold(int amt)
        {
            // throw new NotImplementedException();
        }

        public void SetWood(int amt)
        {
            // throw new NotImplementedException();
        }

        public void SetMetal(int amt)
        {
            // throw new NotImplementedException();
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
    }
}
