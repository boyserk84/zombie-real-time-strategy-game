using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    public class ModelComponentVisitorDelegator : ModelComponentVisitor
    {
        private List<ModelComponentVisitor> visitors = new List<ModelComponentVisitor>();
        
        public void AddVisitor(ModelComponentVisitor v)
        {
            visitors.Add(v);
        }

        public void RemoveVisitor(ModelComponentVisitor v)
        {
            visitors.Remove(v);
        }

        public void Visit(Sand sand)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(sand);
            }
        }

        public void Visit(Mountain mountain)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(mountain);
            }
        }

        public void Visit(Grass grass)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(grass);
            }
        }

        public void Visit(MapGold gold)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(gold);
            }
        }

        public void Visit(MapMetal metal)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(metal);
            }
        }

        public void Visit(MapWood wood)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(wood);
            }
        }

        public void Visit(MapResource mapResource)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(mapResource);
            }
        }

        public void Visit(CellComponent cell)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(cell);
            }
        }

        public void Visit(Map map)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(map);
            }
        }

        public void Visit(Gameworld gameworld)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(gameworld);
            }
        }

        public void Visit(ScenarioComponent scenario)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(scenario);
            }
        }

        public void Visit(PlayerList list)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(list);
            }
        }

        public void Visit(PlayerComponent player)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(player);
            }
        }

        public void Visit(BuildingList list)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(list);
            }
        }

        public void Visit(PlayerResources resources)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(resources);
            }
        }

        public void Visit(UnitList list)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(list);
            }
        }

        public void Visit(UnitComponent unit)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(unit);
            }
        }

        public void Visit(ActionQueue queue)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                v.Visit(queue);
            }
        }

        public void Visit(ModelComponent component)
        {
            foreach (ModelComponentVisitor v in visitors)
            {
                component.Accept(v);
            }
        }
    }
}
