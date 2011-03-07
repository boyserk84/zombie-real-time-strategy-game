using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;

namespace ZRTSMapEditor
{
    public class UpdateUnitPalettePlayerListVisitor : NoOpModelComponentVisitor
    {
        private UnitPalette ui;

        public UnitPalette UnitPalette
        {
            get { return ui; }
            set { ui = value; }
        }

        public override void Visit(PlayerList list)
        {
            ui.uiPlayerList.Items.Clear();
            foreach (PlayerComponent player in list.GetChildren())
            {
                ui.uiPlayerList.Items.Add(player.GetName());
            }
            base.Visit(list);
        }
    }
}
