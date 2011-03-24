using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using Microsoft.Xna.Framework;
using ZRTSModel.GameModel;

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
    }
}
