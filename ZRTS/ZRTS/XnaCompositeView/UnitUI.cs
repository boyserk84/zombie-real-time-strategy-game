using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTSModel;
using ZRTSModel.EventHandlers;

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
        }
    }
}
