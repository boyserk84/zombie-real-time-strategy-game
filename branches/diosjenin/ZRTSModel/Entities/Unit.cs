﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.GameWorld;

namespace ZRTSModel.Entities
{
    [Serializable()]
    public class Unit : Entity
    {
        /** UNIT TYPE **/
        public string unitType;

        /** UNIT STATS **/
        public UnitStats stats;

        /** UNIT LOCATION INFO **/
        Cell myCell;        // The cell the unit currently occupies
        public float x, y;  // Unit's x and y coordinates in game space.

        /// <summary>
        /// Constructor for a unit
        /// </summary>
        /// <param name="owner">Owner</param>
        /// <param name="health">Current Health</param>
        /// <param name="maxHealth">Maximum Health</param>
        /// <param name="radius">Radius</param>
        /// <param name="type">Type of unit</param>
        public Unit(Player.Player owner, short health, short maxHealth)
            : base(owner, health, maxHealth)
        {
            this.entityType = EntityType.Unit;
            this.stats = new UnitStats();
        }

        public Unit(Player.Player owner, UnitStats stats) : base(owner, stats.maxHealth, stats.maxHealth)
        {
            this.entityType = EntityType.Unit;
            this.stats = stats;
        }

        public Cell getCell()
        {
            return this.myCell;
        }

        public void setCell(Cell cell)
        {
            this.myCell = cell;
        }
    }
}
