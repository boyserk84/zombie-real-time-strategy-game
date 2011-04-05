using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using ZRTSModel.GameModel;

namespace ZRTSMapEditor.Commands.MapEditorViewCommands
{
    class AddBuildingCommand : MapEditorCommand
    {
        private Building building;
        private PlayerComponent player;
        private CellComponent cell;

        private AddBuildingCommand()
        { }

        public AddBuildingCommand(Building building, PlayerComponent player, CellComponent cell)
        {
            this.building = building;
            this.player = player;
            this.cell = cell;
        }
        public void Do()
        {
            int x = cell.X;
            int y = cell.Y;
            building.PointLocation = new PointF((float)x, (float)y);
            for (int i = 0; i < building.Width; i++)
            {
                for (int j = 0; j < building.Height; j++)
                {
                    CellComponent c = ((Map)cell.Parent).GetCellAt(x + i, y + j);
                    c.AddEntity(building);
                    building.CellsContainedWithin.Add(c);
                }
            }
            player.BuildingList.AddChild(building);
        }

        public void Undo()
        {
            //TODO: fix this
            player.BuildingList.RemoveChild(building);
            cell.RemoveEntity(building);
            building.PointLocation = null;
        }

        public bool CanBeDone()
        {
            return ((cell != null) && (!cell.ContainsEntity()) && (player != null) && (building != null));
        }
    }
}
