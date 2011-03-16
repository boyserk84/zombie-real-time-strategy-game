using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;
using ZRTSModel.EventHandlers;

namespace ZRTSModel
{
    /// <summary>
    /// A list of units.
    /// </summary>
    [Serializable()]
    public class UnitList : ModelComponent
    {
        public event UnitAddedToPlayerListHandler UnitAddedEvent;
        public event UnitRemovedFromPlayerListHandler UnitRemovedEvent;

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void AddChild(ModelComponent child)
        {
            if (child is UnitComponent)
            {
                base.AddChild(child);
                UnitAddedEventArgs args = new UnitAddedEventArgs();
                args.Unit = (UnitComponent) child;
                UnitAddedEvent(this, args);
            }
        }

        public override void RemoveChild(ModelComponent child)
        {
            base.RemoveChild(child);
            UnitRemovedEvent(this, new UnitRemovedEventArgs());
        }
    }
}
