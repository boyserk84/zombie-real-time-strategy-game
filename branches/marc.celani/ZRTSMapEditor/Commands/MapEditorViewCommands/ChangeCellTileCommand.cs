using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;

namespace ZRTSMapEditor.Commands.MapEditorViewCommands
{
    public class ChangeCellTileCommand : MapEditorCommand
    {
        private CellComponent targetCell;
        private ZRTSModel.Tile targetTile;
        private ZRTSModel.Tile previousTile;

        private ChangeCellTileCommand()
        { }

        public ChangeCellTileCommand(CellComponent cell, ZRTSModel.Tile tile)
        {
            targetCell = cell;
            targetTile = tile;
        }

        public void Do()
        {
            if (CanBeDone())
            {
                previousTile = targetCell.GetTile();
                targetCell.AddChild(targetTile);
            }
        }

        public void Undo()
        {
            targetCell.AddChild(previousTile); 
        }

        public bool CanBeDone()
        {
            bool canBeDone = ((targetCell != null) && (targetTile != null));
            if (canBeDone)
            {
                // Cannot change a tile if the two types are already the same.
                canBeDone = (!targetCell.GetTile().GetType().Equals(targetTile.GetType()));
            }
            return canBeDone;
        }
    }
}
