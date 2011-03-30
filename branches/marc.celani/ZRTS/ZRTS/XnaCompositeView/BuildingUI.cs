using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using Microsoft.Xna.Framework;
using ZRTS.InputEngines;

namespace ZRTS.XnaCompositeView
{
    public class BuildingUI : PictureBox
    {
        private Building building;

        public Building Building
        {
            get { return building; }
        }

        public BuildingUI(Game game, Building building, Rectangle sourceRect)
            : base(game, sourceRect)
        {
            this.building = building;
            this.OnClick += getAttacked;
        }


        private void getAttacked(Object sender, XnaMouseEventArgs e)
        {
            if (e.Bubbled && !e.Handled && e.ButtonPressed == MouseButton.Right)
            {
                ((XnaUITestGame)Game).Controller.TellSelectedUnitsToAttack(building);
                e.Handled = true;
            }
        }
    }
}
