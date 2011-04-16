using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    /// <summary>
    /// Contains cells.  Overrides add and remove to turn them off, but this component is not a leaf - the cells are all children, but are added
    /// at construction.
    /// </summary>
    [Serializable()]
    public class Map : ModelComponent
    {
        private int width;
        private int height;
        private CellComponent[,] cells;

        private Map()
        {
        }

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            cells = new CellComponent[width, height];

        }

        override public List<ModelComponent> GetChildren()
        {
            List<ModelComponent> list = new List<ModelComponent>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    list.Add(cells[i, j]);
                }
            }
            return list;
        }

        public override void AddChild(ModelComponent child)
        {
            // Ensure that only cells are children to the map
            if (child is CellComponent)
            {
                CellComponent cell = (CellComponent)child;
                // Ensure that the cell is inbounds
                if (cell.X >= 0 && cell.X < width && cell.Y >= 0 && cell.Y < height)
                {
                    // Remove cell currently located at that position
                    if (cells[cell.X, cell.Y] != null)
                    {
                        RemoveChild(cells[cell.X, cell.Y]);
                    }
                    cells[cell.X, cell.Y] = cell;
                    base.AddChild(cell);
                }
            }
        }

        public override void RemoveChild(ModelComponent child)
        {
            if (GetChildren().Contains(child))
            {
                // This ensures that the child is a cell, and that it is actually contained in the map.
                CellComponent cell = (CellComponent)child;
                cells[cell.X, cell.Y] = null;
                base.RemoveChild(child);
            }
        }

        public CellComponent GetCellAt(int x, int y)
        {
            return cells[x, y];
        }

        /// <summary>
        /// Check if the building can be added to the map
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool canAddBuildingToMap(ModelComponent component)
        {
            if (component is Building)
            {
                Building tempBuild = (Building)component;
                if (GetCellAt((int)tempBuild.PointLocation.X, (int)tempBuild.PointLocation.Y).ContainsEntity())
                {
                    return false;
                }
                else
                {
                    // check surrounding cells
                    for (int i = (int) tempBuild.PointLocation.X; i < (int) tempBuild.PointLocation.X + tempBuild.Width; ++i)
                    {
                        for (int j = (int)tempBuild.PointLocation.Y; j < (int) tempBuild.PointLocation.Y + tempBuild.Height; ++j)
                        {
                            if (GetCellAt(i, j).ContainsEntity())
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Add building to the map
        /// </summary>
        /// <param name="component"></param>
        /// <returns>True if successfully added to the map</returns>
        public bool addBuildingToMap(ModelComponent component)
        {
            if (canAddBuildingToMap(component))
            {
                Building tempBuild = (Building)component;
                for (int i = (int)tempBuild.PointLocation.X; i < (int)tempBuild.PointLocation.X + tempBuild.Width; ++i)
                {
                    for (int j = (int)tempBuild.PointLocation.Y; j < (int)tempBuild.PointLocation.Y + tempBuild.Height; ++j)
                    {
                        tempBuild.CellsContainedWithin.Add(GetCellAt(i, j));
                        GetCellAt(i, j).AddEntity(tempBuild);
                    }
                }
                return true;
            }
            else return false;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public void SetCellsToBeContainedInMap()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cells[i, j].SetContainer(this);
                }
            }
        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
