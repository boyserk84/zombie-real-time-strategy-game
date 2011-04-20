﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    /// <summary>
    /// A piece of model representing a player.  A player has a UNIQUE name, a race, and resources.  
    /// It also contains as children a building and unit list.
    /// </summary>
    [Serializable()]
    public class PlayerComponent : ModelComponent
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string race;

        public string Race
        {
            get { return race; }
            set { race = value; }
        }

		private List<PlayerComponent> enemyList;
		public List<PlayerComponent> EnemyList
		{
			get { return enemyList; }
		}

        private BuildingList buildingList;

        public BuildingList BuildingList
        {
            get { return buildingList; }
        }

        public PlayerComponent()
        {
            PlayerResources resources = new PlayerResources();
            AddChild(resources);

            UnitList unitList = new UnitList();
            AddChild(unitList);

            buildingList = new BuildingList();
            AddChild(buildingList);

			enemyList = new List<PlayerComponent>();
        }

        public void addBuilding(ModelComponent building)
        {
            buildingList.AddChild(building);
        }

        public void removeBuilding(ModelComponent building)
        {
            buildingList.RemoveChild(building);
        }

        public int Gold
        {
            get 
            {
                return GetResources().Gold;
            }
            set
            {
                GetResources().Gold = value;
            }
        }

        public int Wood
        {
            get
            {
                return GetResources().Wood;
            }
            set
            {
                GetResources().Wood = value;
            }
        }

        public int Metal
        {
            get
            {
                return GetResources().Metal;
            }
            set
            {
                GetResources().Metal = value;
            }
        }


        public PlayerResources GetResources()
        {
            foreach (ModelComponent component in GetChildren())
            {
                if (component is PlayerResources)
                {
                    return (PlayerResources)component;
                }
            }
            return null;
        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public UnitList GetUnitList()
        {
            UnitList list = null;
            foreach (ModelComponent component in GetChildren())
            {
                if (component is UnitList)
                {
                    list = (UnitList)component;
                    break;
                }
            }
            return list;
        }
    }
}
