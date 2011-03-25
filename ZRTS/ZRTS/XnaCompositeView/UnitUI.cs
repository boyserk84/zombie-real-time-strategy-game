using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTSModel;
using ZRTSModel.EventHandlers;
using ZRTS.InputEngines;

namespace ZRTS.XnaCompositeView
{
    public class UnitUI : PictureBox
    {
        private UnitComponent unit;

        public UnitComponent Unit
        {
            get { return unit; }
        }
        public UnitUI(Game game, UnitComponent unit, Rectangle sourceRect)
            : base(game, sourceRect)
        {
            this.unit = unit;
            this.OnClick += getAttacked;
        }

        private void getAttacked(Object sender, XnaMouseEventArgs e)
        {
            if (e.Bubbled && !e.Handled && e.ButtonPressed == MouseButton.Right)
            {
                ((XnaUITestGame)Game).Controller.TellSelectedUnitsToAttack(unit);
                e.Handled = true;
            }
        }
    }
}
