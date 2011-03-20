using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Trigger
{
    [Serializable()]
    abstract class TriggerDecorator : Trigger
    {
        private Trigger decorated = null;
        protected bool isMet;
        protected bool needsToBeEvaled = true;

        private TriggerDecorator()
        {

        }

        public TriggerDecorator(Trigger decorated)
        {
            this.decorated = decorated;
        }

        public void PerformAction(Scenario.Scenario scenario)
        {
            decorated.PerformAction(scenario);
            PerformAction(scenario);
        }

        public bool Eval(Scenario.Scenario scenario)
        {
            bool myCondition = NeedsToBeEvaluated() ? CheckMyCondition(scenario) : IsMet();
            return myCondition && decorated.Eval(scenario);
        }

        public bool IsMet()
        {
            return isMet;
        }

        public bool NeedsToBeEvaluated()
        {
            return needsToBeEvaled;
        }

        public abstract void PerformMyAction(Scenario.Scenario scenario);
        public abstract bool CheckMyCondition(Scenario.Scenario scenario);

        public void notify(ModelComponent observable)
        {
            observable.Accept(this);
        }

        public virtual void Visit(ModelComponent component)
        {
            // No op
        }

        public virtual void Visit(Sand sand)
        {
            // No op
        }

        public virtual void Visit(Mountain mountain)
        {
            // No op
        }

        public virtual void Visit(Grass grass)
        {
            // No op
        }

        public virtual void Visit(MapGold gold)
        {
            // No op
        }

        public virtual void Visit(MapMetal metal)
        {
            // No op
        }

        public virtual void Visit(MapWood wood)
        {
            // No op
        }

        public virtual void Visit(MapResource mapResource)
        {
            // No op
        }

        public virtual void Visit(CellComponent cell)
        {
            // No op
        }

        public virtual void Visit(Map map)
        {
            // No op
        }

        public virtual void Visit(Gameworld gameworld)
        {
            // No op
        }

        public virtual void Visit(ScenarioComponent scenario)
        {
            // No op
        }

        public virtual void Visit(PlayerList list)
        {
            // No op
        }

        public virtual void Visit(PlayerComponent player)
        {
            // No op
        }

        public virtual void Visit(BuildingList list)
        {
            // No op
        }

        public virtual void Visit(PlayerResources resources)
        {
            // No op
        }

        public virtual void Visit(UnitList list)
        {
            // No op
        }

        public virtual void Visit(UnitComponent unit)
        {
            // No op
        }

        public virtual void Visit(ActionQueue queue)
        {
            // No op
        }
    }
}
