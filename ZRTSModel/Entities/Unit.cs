﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.GameWorld;
using ZRTSModel.GameEvent;

namespace ZRTSModel.Entities
{
    [Serializable()]
    public class Unit : Entity, GameEventObserver
    {
        /** UNIT TYPE **/
        public string unitType;

        /** UNIT STATS **/
        public UnitStats stats;

		/// <summary>
		/// Used to give a Unit an attack bonus ( greater than 1.0) or penalty (less than 1.0)
		/// </summary>
		public double attackBuff = 1.0;

		/// <summary>
		/// Used to give a Unit a movement speed bonus or penalty.
		/// </summary>
		public double speedBuff = 1.0;

		public enum AttackStance
		{
			Passive,	// Ignore enemies
			Guard,		// Attack enemies on site, chase for a short distance before returning to starting position.
			Agressive	// Attack enemies on site, chase until enemy is dead.
		};

		AttackStance attackStance = AttackStance.Guard;

        /** UNIT LOCATION INFO **/
        Cell myCell;        // The cell the unit currently occupies
        //public float x, y;  // Unit's x and y coordinates in game space. (MOVE TO ENTITY)

		/** ORIENTATION INFO 
		 * Represents which direction a Unit is facing in the gamewold. **/
        public enum Orientation { N, S, E, W, NW, NE, SW, SE };
		public Orientation orientation = Orientation.S;

        /// <summary>
        /// Constructor for a unit
        /// </summary>
        /// <param name="owner">Owner</param>
        /// <param name="health">Current Health</param>
        /// <param name="maxHealth">Maximum Health</param>
        /// <param name="radius">Radius</param>
        /// <param name="type">Type of unit</param>
        public Unit(Player.Player owner, short health)
            : base(owner, health)
        {

            this.entityType = EntityType.Unit;
            this.stats = new UnitStats();
        }

        public Unit(Player.Player owner, UnitStats stats) : base(owner, stats.maxHealth)
        {
            this.stats = stats;
            this.entityType = EntityType.Unit;
        }

        public Cell getCell()
        {
            return this.myCell;
        }

        public void setCell(Cell cell)
        {
            this.myCell = cell;
        }


		/**** GameEventObserver functions ***/

		List<GameSubject> subjects = new List<GameSubject>();

		public void notify(Event gameEvent)
		{
			Console.WriteLine("The unit at (" + this.x + ", " + this.y + ") + sees the subject at cell (" + gameEvent.orginCell.Xcoord + ", " + gameEvent.orginCell.Ycoord + ")");
		}

		public void register(GameSubject subject)
		{
			if (!subjects.Contains(subject))
			{
				subjects.Add(subject);
				subject.registerObserver(this);
			}
		}

		public void unregister(GameSubject subject)
		{
			subject.unregisterObserver(this);
			subjects.Remove(subject);
		}

		public void unregisterAll()
		{
			foreach (GameSubject s in subjects)
			{
				s.unregisterObserver(this);
			}
			subjects.Clear();
		}
    }
}
