using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ZRTSModel.GameWorld;
using ZRTSModel.Entities;
using ZRTSModel.Scenario;
using ZRTSLogic.Action;
using ZRTSModel.Factories;
//using ZRTSModel;

namespace ZRTSLogic
{
    /// <summary>
    /// 
    /// </summary>
    public class Controller
    {
        private short DEAD_DISAPPEAR_TICKS = 300; // Number of ticks to wait for a dead unit to disappear
        private long curTick = 0;

        public GameWorld gameWorld;
        public Scenario scenario;

        // Controls all actions that entities may do.
        ActionController actionController;

        // Controls adding, removing, and moving entities.
        EntityLocController locController;

		// Updates the visibilty map
		VisibilityMapLogic visMapLogic;

        /// <summary>
        /// This method will take a Scenario as input and will create a Controller object for that Scenario.
        /// </summary>
        /// <param name="scenario"></param>
        public Controller(Scenario scenario)
        {
            setUpController(scenario);
        }

        public Controller(string fileName)
        {
            //TODO: Load scenario from fileName.
        }

        private void setUpController(Scenario scenario)
        {
            this.scenario = scenario;
            this.gameWorld = this.scenario.getGameWorld();
            this.actionController = new ActionController(scenario);
            this.locController = new EntityLocController(scenario);
            
        }

        /// <summary>
        /// This function will be called by the GUI to call for another cycle of the game to be run. This function will
        /// update all of the states of the Entitys in the GameWorld.
        /// </summary>
        public void updateWorld()
        {
            List<Entity> entitiesToRemove = new List<Entity>();

            /** Have Units perform actions **/
            foreach (Unit u in gameWorld.getUnits())
            {
                actionController.update(u, locController);
                checkDeath(u);

                if (u.getState().getPrimaryState() == State.PrimaryState.Remove)
                {
                    entitiesToRemove.Add(u);
                }
            }

            /** Have Buildings perform actions **/
            foreach (Building b in gameWorld.getBuildings())
            {
                actionController.update(b, locController);
                checkDeath(b);

                if (b.getState().getPrimaryState() == State.PrimaryState.Remove)
                {
                    entitiesToRemove.Add(b);
                }
            }

            /** Handle any Events that have been generated **/


            /** Remove any entities **/
            foreach (Entity e in entitiesToRemove)
            {
                locController.removeEntity(e);
            }

            curTick++;
        }

        /// <summary>
        /// Given an Entity and an ActionCommand, this function will attempt to give the Entity the ActionCommand
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="command"></param>
        /// <returns>false if the command was rejected, true if the command was accepted.</returns>
        public bool giveActionCommand(Entity entity, ActionCommand command)
        {
            return actionController.giveCommand(entity, command);
        }

        /// <summary>
        /// Adds a static entity to the game.
        /// </summary>
        /// <param name="entity">The StaticEntity being added.</param>
        /// <param name="x">X-Coord of the desired "orgin Cell" of the static entity.</param>
        /// <param name="y">Y-Coord of the desired "orgin Cell" of the static entity.</param>
        /// <returns>true if the entity is added successfully, false otherwise.</returns>
        public bool addEntity(StaticEntity entity, short x, short y)
        {
            return locController.addEntity(entity, x, y);
        }

        /// <summary>
        /// Adds a Unit to the game.
        /// </summary>
        /// <param name="unit">The unit being added.</param>
        /// <param name="x">Desired X-coord of the Unit.</param>
        /// <param name="y">Desired Y-coord of the Unit.</param>
        /// <returns>true if the unit is added successfully, false otherwise.</returns>
        public bool addUnit(Unit unit, float x, float y)
        {
            return locController.addEntity(unit, x, y);
        }

        public bool removeEntity(Entity entity)
        {
            return true;
        }

        public ActionController getActionController()
        {
            return this.actionController;
        }

        private void checkDeath(Entity e)
        {
            if(e.getState().inState(State.PrimaryState.Dead))
            {
                if (e.getEntityType() != Entity.EntityType.Unit)
                {
                    e.getState().setPrimaryState(State.PrimaryState.Remove);
                }
                else if (e.tickKilled == 0)
                {
                    e.tickKilled = this.curTick;
                }
                else if (Math.Abs(this.curTick - e.tickKilled) > DEAD_DISAPPEAR_TICKS)
                {
                    e.getState().setPrimaryState(State.PrimaryState.Remove);
                }
            }
        }
    }
}
