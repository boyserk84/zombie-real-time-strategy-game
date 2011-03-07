using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    /// <summary>
    /// A discrete unit of space on a map.  Can contain a map resource.  Each cell contains a tile to display itself.
    /// </summary>
    [Serializable()]
    public class CellComponent : ModelComponent
    {

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
                    }
                }
            }
            foreach (ModelComponent component in toRemove)
            {
                RemoveChild(component); 
            }
            base.AddChild(child);
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
    }
}
