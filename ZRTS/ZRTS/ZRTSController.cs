using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using Microsoft.Xna.Framework;
using ZRTSModel.GameModel;
using ZRTSLogic.Action;

namespace ZRTS
{
    public class ZRTSController : GameComponent
    {
        public ZRTSController(Game game)
            : base(game)
        {
        }
        /// <summary>
        /// Selects the entities in the list of model components.
        /// </summary>
        /// <param name="?"></param>
        public void SelectEntities(List<ModelComponent> EntityList)
        {
            SelectionState selectionState = getGameModel().GetSelectionState();
            selectionState.ClearSelectionState();
            foreach (ModelComponent entity in EntityList)
            {
                selectionState.SelectEntity(entity);
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
                    MoveAction moveAction = new MoveAction(point.X, point.Y, getGameModel().GetScenario().GetGameWorld().GetMap());
                    ActionQueue aq = unit.GetActionQueue();
                    while (aq.GetChildren().Count > 0)
                        aq.RemoveChild(aq.GetChildren()[0]);
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
                    u.GetActionQueue().AddChild(new SimpleAttackUnitAction(u, unit));
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
    }
}
