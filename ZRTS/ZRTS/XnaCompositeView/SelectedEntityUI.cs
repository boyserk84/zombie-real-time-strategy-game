using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using Microsoft.Xna.Framework;
using ZRTSModel.EventHandlers;

namespace ZRTS.XnaCompositeView
{
    public class SelectedEntityUI : XnaUIComponent
    {
        private UnitComponent unit;

        public SelectedEntityUI(Game game, UnitComponent unit)
            : base(game)
        {
            this.unit = unit;
            unit.HPChangedEventHandlers += UpdateHPBar;
        }

        public void UpdateHPBar(Object sender, UnitHPChangedEventArgs args)
        {
            foreach (XnaUIComponent child in GetChildren())
            {
                if (child is HPBar)
                {
                    ((HPBar)child).CurrentHP = args.NewHP;
                    break;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unit.HPChangedEventHandlers -= UpdateHPBar;
            }
            base.Dispose(disposing);
        }

        protected override void onDraw(XnaDrawArgs e)
        {
            e.SpriteBatch.Draw(((XnaUITestGame)Game).SpriteSheet, e.Location, Color.Red);
        }
    }
}
