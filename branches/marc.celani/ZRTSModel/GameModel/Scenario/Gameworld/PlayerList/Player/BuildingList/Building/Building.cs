using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.GameModel;

namespace ZRTSModel
{
    public class Building : ModelComponent
    {
		public Building()
		{
			this.actionQueue = new BuildingActionQueue();
			AddChild(this.actionQueue);
		}

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        // Dimensions of the building
        private byte width;

        public byte Width
        {
            get { return width; }
            set { width = value; }
        }
        private byte height;

        public byte Height
        {
            get { return height; }
            set { height = value; }
        }

        private bool completed;

        public bool Completed
        {
            get { return completed; }
            set { completed = value; }
        }

        // Max Health of the building
        private int maxHealth;

        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        private int currentHealth;

        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        // Can resources be dropped off at this building?
        private bool dropOffResources;

        public bool DropOffResources
        {
            get { return dropOffResources; }
            set { dropOffResources = value; }
        }

        // Can this building produce units?
        private bool canProduce;

        public bool CanProduce
        {
            get { return canProduce; }
            set { canProduce = value; }
        }

        // The types of units that this building can produce.
        private List<string> productionTypes = new List<string>();

        public List<string> ProductionTypes
        {
            get { return productionTypes; }
            set { productionTypes = value; }
        }

        private int waterCost;

        public int WaterCost
        {
            get { return waterCost; }
            set { waterCost = value; }
        }
        private int foodCost;

        public int FoodCost
        {
            get { return foodCost; }
            set { foodCost = value; }
        }
        private int lumberCost;

        public int LumberCost
        {
            get { return lumberCost; }
            set { lumberCost = value; }
        }
        private int metalCost;

        public int MetalCost
        {
            get { return metalCost; }
            set { metalCost = value; }
        }

        private PointF pointLocation;

        public PointF PointLocation
        {
            get { return pointLocation; }
            set { pointLocation = value; }
        }

        private List<CellComponent> cellsContainedWithin = new List<CellComponent>();

        public List<CellComponent> CellsContainedWithin
        {
            get { return cellsContainedWithin; }
            set { cellsContainedWithin = value; }
        }

		private BuildingActionQueue actionQueue = new BuildingActionQueue();

		public BuildingActionQueue BuildingActionQueue
		{
			get { return this.actionQueue; }
			set { this.actionQueue = value; }

		}

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
