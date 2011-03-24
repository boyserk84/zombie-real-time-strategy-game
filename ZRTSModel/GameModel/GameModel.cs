using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.GameModel
{
    public class GameModel : ModelComponent
    {
        private PlayerComponent playerInContext;

        /// <summary>
        /// A reference to the player that is actually playing the game.
        /// </summary>
        public PlayerComponent PlayerInContext
        {
            get { return playerInContext; }
            set { playerInContext = value; }
        }

        public GameModel()
        {
            AddChild(new SelectionState());
        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public ScenarioComponent GetScenario()
        {
            ScenarioComponent scenario = null;
            foreach (ModelComponent component in GetChildren())
            {
                if (component is ScenarioComponent)
                {
                    scenario = (ScenarioComponent)component;
                    break;
                }
            }
            return scenario;
        }

        public SelectionState GetSelectionState()
        {
            SelectionState selectionState = null;
            foreach (ModelComponent component in GetChildren())
            {
                if (component is SelectionState)
                {
                    selectionState = (SelectionState)component;
                    break;
                }
            }
            return selectionState;
        }
    }
}
