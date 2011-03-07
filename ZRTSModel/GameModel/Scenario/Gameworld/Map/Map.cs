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

            // A default map is all Grass.
            initializeToGrass(width, height);


        }

        private void initializeToGrass(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cells[i, j] = new CellComponent();
                    cells[i, j].AddChild(new Grass());
                    cells[i, j].SetContainer(this);
                }
            }
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

        public virtual void AddChild(ModelComponent child)
        {
            // No op - Map's only children are the cells.
        }

        public virtual void RemoveChild(ModelComponent child)
        {
            // No op - Map's only children are the cells.
        }

        public CellComponent GetCellAt(int x, int y)
        {
            return cells[x, y];
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
