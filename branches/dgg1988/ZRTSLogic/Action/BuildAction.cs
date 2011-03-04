using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;
using ZRTSModel.GameWorld;

namespace ZRTSLogic.Action
{
    /// <summary>
    /// This class will represent an Action that can be given to a unit to tell that unit to "build" a building.
    /// 
    /// Note: This action assumes that the Building is already inserted into the GameWorld.
    /// </summary>
    public class BuildAction : ActionCommand
    {
        Building building;
        Unit unit;
        GameWorld gw;
        short TICKS_PER_CYCLE = 20;
        short curTicks = 0;

        /// <summary>
        /// </summary>
        /// <param name="building"></param>
        /// <param name="unit"></param>
        public BuildAction(Building building, Unit unit, GameWorld gw)
        {
            this.building = building;
            this.unit = unit;
            this.actionType = ActionType.BuildBuilding;
            this.gw = gw;
        }

        public bool work()
        {
            // Building is complete, finish action.
            if (building.isCompleted)
            {
                return true;
            }

            // Unit cannot build, disregard action.
            if (!unit.stats.canBuild)
            {
                return true;
            }

            if (curTicks % TICKS_PER_CYCLE == 0)
            {

                if (isUnitNextToBuilding())
                {
                    unit.getState().setPrimaryState(State.PrimaryState.Building);
                    if (building.stats.maxHealth - building.health <= unit.stats.buildSpeed)
                    {
                        // Finish the building.
                        building.health = building.stats.maxHealth;
                        building.isCompleted = true;
                        return true;
                    }
                    else
                    {
                        // Continue building the building.
                        building.health += unit.stats.buildSpeed;
                    }
                }
                else
                {
                    // Move towards the building.
                    Cell targetCell = EntityLocController.findClosestCell(unit, building, gw);
                    MoveAction moveAction = new MoveAction(targetCell.Xcoord, targetCell.Ycoord, gw, unit);
                    ActionController.insertIntoActionQueue(unit, moveAction);
                }
            }

            curTicks++;
            return false;
        }

        private bool isUnitNextToBuilding()
        {
            float xC = building.orginCell.Xcoord;
            float yC = building.orginCell.Ycoord;
            short width = building.width;
            short height = building.height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (EntityLocController.findDistance(unit.x, unit.y, xC + i, yC + j) <= 1.0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
