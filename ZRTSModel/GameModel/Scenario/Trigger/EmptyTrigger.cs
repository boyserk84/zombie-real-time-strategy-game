using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Trigger
{
    [Serializable()]
    class EmptyTrigger : Trigger
    {
        public void PerformAction(Scenario.Scenario scenario)
        {
            // Do nothing
        }

        public bool Eval(Scenario.Scenario scenario)
        {
            return true;
        }

        public bool NeedsToBeEvaluated()
        {
            return false;
        }

        public bool IsMet()
        {
            return true;
        }

        public void Visit(Sand sand)
        {
            // No op
        }

        public void Visit(Mountain mountain)
        {
            // No op
        }

        public void Visit(Grass grass)
        {
            // No op
        }

        public void Visit(MapGold gold)
        {
            // No op
        }

        public void Visit(MapMetal metal)
        {
            // No op
        }

        public void Visit(MapWood wood)
        {
            // No op
        }

        public void Visit(MapResource mapResource)
        {
            // No op
        }

        public void Visit(CellComponent cell)
        {
            // No op
        }

        public void Visit(Map map)
        {
            // No op
        }

        public void Visit(Gameworld gameworld)
        {
            // No op
        }

        public void Visit(ScenarioComponent scenario)
        {
            // No op
        }

        public void Visit(PlayerList list)
        {
            // No op
        }

        public void Visit(PlayerComponent player)
        {
            // No op
        }

        public void Visit(BuildingList list)
        {
            // No op
        }

        public void Visit(PlayerResources resources)
        {
            // No op
        }

        public void Visit(UnitList list)
        {
            // No op
        }

        public void Visit(UnitComponent unit)
        {
            // No op
        }

        public void Visit(ActionQueue queue)
        {
            // No op
        }

        public void Visit(ModelComponent component)
        {
            // No op
        }
    }
}
