using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.EventHandlers;

namespace ZRTSModel
{
    /// <summary>
    /// A discrete unit of space on a map.  Can contain a map resource.  Each cell contains a tile to display itself.
    /// </summary>
    [Serializable()]
    public class CellComponent : ModelComponent
    {
        private int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public event TileChangedHandler TileChangedEvent;
        public event EntityInCellChangedHandler UnitAddedEvent;
        public event EntityInCellChangedHandler UnitRemovedEvent;

        private List<ModelComponent> entitiesContainedWithin = new List<ModelComponent>();

        public List<ModelComponent> EntitiesContainedWithin
        {
            get { return entitiesContainedWithin; }
        }

        override public void AddChild(ModelComponent child)
        {
            // Ensure that we only have one tile or resource
            List<ModelComponent> toRemove =  new List<ModelComponent>();
            if (child is Tile)
            {
                foreach (ModelComponent tile in GetChildren())
                {
                    if (tile is Tile)
                    {
                        toRemove.Add(tile);
                    }
                }
            }
            else if (child is MapResource)
            {
                foreach (ModelComponent resource in GetChildren())
                {
                    if (resource is MapResource)
                    {
                        toRemove.Add(resource);
                        if (entitiesContainedWithin.Count != 0)
                        {
                            UnitArgs args = new UnitArgs();
                            args.Unit = (UnitComponent)entitiesContainedWithin[0];
                            entitiesContainedWithin.Clear();
                            if (UnitRemovedEvent != null)
                                UnitRemovedEvent(this, args);
                        }
                    }
                }
                entitiesContainedWithin.Add(child);
            }
            foreach (ModelComponent component in toRemove)
            {
                RemoveChild(component); 
            }
            base.AddChild(child);
            
            // Handle notifications
            if (child is Tile)
            {
                if (TileChangedEvent != null)
                {
                    TileChangedEvent(this, new TileChangedEventArgs((Tile)child));
                }
            }
        }

        public Tile GetTile()
        {
            foreach (ModelComponent tile in GetChildren())
            {
                if (tile is Tile)
                {
                    return (Tile)tile;
                }
            }
            return null;
        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void AddEntity(ModelComponent entity)
        {
            if (entitiesContainedWithin.Count == 0)
            {
                if (entity is UnitComponent || entity is MapResource)
                {
                    entitiesContainedWithin.Add(entity);
                    if (entity is UnitComponent)
                    {
                        UnitArgs args = new UnitArgs();
                        args.Unit = (UnitComponent)entity;
                        UnitAddedEvent(this, args);
                    }
                }
            }
        }

        public bool ContainsEntity()
        {
            return !(entitiesContainedWithin.Count == 0);
        }

        public void RemoveEntity(ModelComponent entity)
        {
            entitiesContainedWithin.Remove(entity);
            UnitArgs args = new UnitArgs();
            args.Unit = (UnitComponent)entity;
            if (UnitRemovedEvent != null)
                UnitRemovedEvent(this, args);
        }
    }
}
