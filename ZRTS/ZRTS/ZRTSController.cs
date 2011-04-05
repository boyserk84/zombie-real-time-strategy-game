using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using Microsoft.Xna.Framework;
using ZRTSModel.GameModel;
using ZRTS.XnaCompositeView;
using ZRTSModel.Factories;


namespace ZRTS
{
    public class ZRTSController : GameComponent
    {
		Game game;
        public ZRTSController(Game game)
            : base(game)
        {
			this.game = game;
        }
        /// <summary>
        /// Selects the entities in the list of model components.
        /// </summary>
        /// <param name="?"></param>
        public void SelectEntities(List<ModelComponent> EntityList)
        {

            // Filter out zombie unit from player's selected list
            for (int i = 0; i < EntityList.Count; ++i )
            {
                if (EntityList[i] is UnitComponent)
                {
                    if (((UnitComponent)EntityList[i]).IsZombie)
                    {
                        EntityList.Remove(EntityList[i]);
                    }
                }
            }


            SelectionState selectionState = getGameModel().GetSelectionState();
            selectionState.ClearSelectionState();

			bool hasUnits = false;
			bool hasPlayerEntities = false;
			PlayerComponent player = (PlayerComponent)((XnaUITestGame)game).Model.GetScenario().GetGameWorld().GetPlayerList().GetChildren()[0];
			foreach (ModelComponent entity in EntityList)
			{
				if (entity is UnitComponent)
				{
					hasUnits = true;

					if (player.GetUnitList().GetChildren().Contains(entity))
					{
						hasPlayerEntities = true;
					}
				}
				else if (entity is Building)
				{
					if (player.BuildingList.GetChildren().Contains(entity))
					{
						hasPlayerEntities = true;
					}
				}
			}

            foreach (ModelComponent entity in EntityList)
            {
				if (hasUnits && hasPlayerEntities)
				{
					if (entity is UnitComponent && player.GetUnitList().GetChildren().Contains(entity))
					{
						selectionState.SelectEntity(entity);
					}
				}
				else if (hasPlayerEntities)
				{
					if (entity is Building && player.BuildingList.GetChildren().Contains(entity))
					{
						selectionState.SelectEntity(entity);
					}
				}
				else if (hasUnits)
				{
					if (entity is UnitComponent)
					{
						selectionState.SelectEntity(entity);
					}
				}
				else
				{
					selectionState.SelectEntity(entity);
				}
            }
        }

        private GameModel getGameModel()
        {
            return ((XnaUITestGame)Game).Model;
        }

        public void MoveSelectedUnitsToPoint(PointF point)
        {
            List<ModelComponent> selectedEntities = ((XnaUITestGame)Game).Model.GetSelectionState().SelectedEntities;
            // Ensure each component is a Unit.
            bool allAreUnits = true;
            foreach (ModelComponent component in selectedEntities)
            {
                if (!(component is UnitComponent))
                {
                    allAreUnits = false;
                    break;
                }
            }
            if (allAreUnits)
            {
                foreach (UnitComponent unit in selectedEntities)
                {
                    MoveAction moveAction = new MoveAction(point.X, point.Y, getGameModel().GetScenario().GetGameWorld().GetMap(), unit);
                    ActionQueue aq = unit.GetActionQueue();
                    aq.GetChildren().Clear();
                    aq.AddChild(moveAction);
                }
            }
        }
        public void TellSelectedUnitsToAttack(UnitComponent unit)
        {
            List<ModelComponent> selectedEntities = getGameModel().GetSelectionState().SelectedEntities;
            bool canAttack = true;
            foreach (ModelComponent entity in selectedEntities)
            {
                if (!(entity is UnitComponent))
                {
                    canAttack = false;
                    break;
                }
                else
                {
                    UnitComponent u = entity as UnitComponent;
                    if (!u.CanAttack)
                    {
                        canAttack = false;
                        break;
                    }
                }
            }
            if (canAttack)
            {
                foreach (UnitComponent u in selectedEntities)
                {
                    u.GetActionQueue().GetChildren().Clear();
                    u.GetActionQueue().AddChild(new AttackAction(u, unit, getGameModel().GetScenario().GetGameWorld()));
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            PlayerList players = getGameModel().GetScenario().GetGameWorld().GetPlayerList();
            foreach (PlayerComponent player in players.GetChildren())
            {
                UnitList units = player.GetUnitList();
                // Make a copy of the list that will not be tampered with by the attack action.
                List<UnitComponent> unitList = new List<UnitComponent>();
                foreach (UnitComponent unit in units.GetChildren())
                {
                    unitList.Add(unit);
                }
                foreach (UnitComponent unit in unitList)
                {
                    unit.GetActionQueue().Work();
                }
            }
            base.Update(gameTime);
        }

        internal void OnSelectBuildingToBuild(string buildingType)
        {
            changeMapViewLeftClickStrategyToBuild(buildingType);
        }


        /// <summary>
        /// select unit to stop
        /// </summary>
        internal void OnSelectUnitToStop()
        {
           List<ModelComponent> selectedEntities = ((XnaUITestGame)Game).Model.GetSelectionState().SelectedEntities;
           foreach (ModelComponent e in selectedEntities)
           {
               if (e is UnitComponent)
               {
                   UnitComponent unit = (UnitComponent)e;
                   unit.GetActionQueue().GetChildren().Clear();
                   unit.State = UnitComponent.UnitState.IDLE;
                   
               }
           }
        }

        /// <summary>
        /// Notifies the map view of the selection so that it may change its strategy for handling mouse messages in order to show the
        /// building image before placing as well as to build the building on placement.
        /// </summary>
        /// <param name="buildingType"></param>
        private void changeMapViewLeftClickStrategyToBuild(string buildingType)
        {
            // HACK: Assumes that the map view is the first child.
            // Solution: Create a ZRTSView child to the frame, that has members for the map view, minimap, selection view, command view, and any menus.
            MapView mapView = ((XnaUITestGame)Game).View.GetChildren()[0] as MapView;
            mapView.LeftButtonStrategy = new BuildBuildingMapViewLeftClickStrategy(mapView, buildingType);
        }

        internal bool CellsAreEmpty(int x, int y, int width, int height)
        {
            Map map = getGameModel().GetScenario().GetGameWorld().GetMap();
            bool areEmpty = (x + width < map.GetWidth()) && (y + height < map.GetHeight());
            if (areEmpty)
            {

                for (int i = x; (i < x + width) && areEmpty; i++)
                {
                    for (int j = y; (j < y + height) && areEmpty; j++)
                    {
                        areEmpty = map.GetCellAt(i, j).EntitiesContainedWithin.Count == 0;
                    }
                }
            }
            return areEmpty;
        }

        internal void TellSelectedUnitsToBuildAt(string buildingType, Point upperLeftCellCoords)
        {
            List<ModelComponent> selectedEntities = getGameModel().GetSelectionState().SelectedEntities;
            bool canBuild = true;
            foreach (ModelComponent entity in selectedEntities)
            {
                if (!(entity is UnitComponent))
                {
                    canBuild = false;
                    break;
                }
                else
                {
                    UnitComponent u = entity as UnitComponent;
                    if (!u.CanBuild)
                    {
                        canBuild = false;
                        break;
                    }
                }
            }
            if (canBuild)
            {
                // TODO: Check resources.
                
                // Create the actual building that each action will reference.
                BuildingFactory factory = BuildingFactory.Instance;
                Building buildingToAdd = factory.Build(buildingType, false);
                buildingToAdd.PointLocation = new PointF(upperLeftCellCoords.X, upperLeftCellCoords.Y);
                foreach (UnitComponent u in selectedEntities)
                {
                    u.GetActionQueue().AddChild(new BuildAction(buildingToAdd, getGameModel().GetScenario().GetGameWorld().GetMap()));
                }
            }
        }

        internal void TellSelectedUnitsToAttack(Building building)
        {
            // TODO: Implement
        }
    }
}
