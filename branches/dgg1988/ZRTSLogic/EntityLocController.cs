﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Scenario;
using ZRTSModel.GameWorld;
using ZRTSModel.Entities;
using ZRTSModel.Player;

namespace ZRTSLogic
{
    /// <summary>
    /// This class will be used to handle all changes that need to be made when adding, removing, and moving
    /// entities in the game. It will handle all of these changes to make sure that all of the appropriate data 
    /// structures are updated. All of these changes should use the methods in this class to ensure consistency.
    /// </summary>
    public class EntityLocController
    {

        Scenario scenario;
        GameWorld gw;
		VisibilityMapLogic visMapLogic;

        public EntityLocController(Scenario scenario, VisibilityMapLogic visMapLogic)
        {
            this.scenario = scenario;
            this.gw = scenario.getGameWorld();
			this.visMapLogic = visMapLogic;
        }

        /// <summary>
        /// This method will attempt to add the entity at the given location.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="x">x coordinate in gamespace of where the entity should be inserted</param>
        /// <param name="y">y coordinate in gamespace of where the entity should be inserted</param>
        /// <returns>true if the entity is inserted successfully, false otherwise</returns>
        public bool addEntity(Entity entity, float x, float y)
        {
            bool success = false;

            /** Try Inserting into GameWorld first **/
            if (entity.entityType == Entity.EntityType.Unit)
            {
                // The coordinates of the Cell the unit is being inserted into.
                int xC = (int)Math.Floor(x);
                int yC = (int)Math.Floor(y);

                Cell c = gw.map.getCell(xC, yC);
                // Insert into gameworld if the target cell is empty
                if (c.isValid)
                {
                    Unit u = (Unit)entity;
                    c.setUnit(u); // Insert into the cell
                    gw.getUnits().Add(u); // Insert into the Unit list
                    u.setCell(c);
                    u.x = (float)xC + 0.5f;
                    u.y = (float)yC + 0.5f;
                    success = true;
                }
            }
            else if(entity.getEntityType() == Entity.EntityType.Building)
            {
                success = gw.insert((Building)entity, (int)x, (int)y);
            }

            // If insert into GameWorld was a success, insert into right player
            if (success)
            {
                scenario.insertEntityIntoPlayer(entity);
            }

            return success;
        }


        /// <summary>
        /// This method will update the pointers to the Cell in the GameWorld that is occuppied by the Unit.
        /// </summary>
        /// <param name="unit">The unit who's location is being updated.</param>
        public void updateUnitLocation(Unit unit)
        {
            
            if (!unit.getCell().contains(unit.x, unit.y))
            {
                Cell newCell = gw.map.getCell((int)Math.Floor(unit.x), (int)Math.Floor(unit.y));
                Cell oldCell = unit.getCell();

                oldCell.removeUnit();

                // NOTE: doesn't check if newCell is already occupied.
                unit.setCell(newCell);
                newCell.setUnit(unit);

				// Update the visibility map.
				this.visMapLogic.updateVisMap(unit);
            }
        }


        /// <summary>
        /// This method will remove a Entity from the game.
        /// </summary>
        /// <param name="entity">The Entity being removed.</param>
        public void removeEntity(Entity entity)
        {
            if (entity.getEntityType() == Entity.EntityType.Unit) // entity is a Unit
            {
                // Remove Unit from its cell
                Unit u = (Unit)entity;
                Cell c = u.getCell();

                c.removeUnit();
                u.setCell(null);

                // Remove Unit from the Unit List.
                gw.getUnits().Remove(u);

            }
            else
            {
                gw.remove((StaticEntity)entity);
            }

            // Remove entity from its player.
            scenario.removeEntityFromPlayer(entity);
        }


        /** STATIC FUNCTIONS 
         *  The functions below are meant to be common helper functions.
         * **/

        public static float findDistance(float x1, float y1, float x2, float y2)
        {
            double dis = Math.Pow((double)(x1 - x2), 2) + Math.Pow((double)(y1 - y2), 2);
            dis = Math.Sqrt(dis);
            return (float)dis;
        }


        /// <summary>
        /// This method will find the cell closest to 'unit' that 'entity' is currently occupying.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Cell findClosestCell(Unit unit, StaticEntity se, GameWorld gw)
        {
            Cell cell = null;
            float dis = 10000;

            short xC = se.orginCell.Xcoord;
            short yC = se.orginCell.Ycoord;
            short width = se.width;
            short height = se.height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (EntityLocController.findDistance(unit.x, unit.y, xC + i, yC + j) <= dis)
                    {
                        cell = gw.map.getCell(xC + i, xC + j);
                        dis = EntityLocController.findDistance(unit.x, unit.y, xC + i, yC + j);
                    }
                }
            }

            return cell;
        }
    }
}
