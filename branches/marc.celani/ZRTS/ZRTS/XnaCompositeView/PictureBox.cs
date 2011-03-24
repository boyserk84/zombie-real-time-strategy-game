using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZRTS.XnaCompositeView
{
    public class PictureBox : XnaUIComponent
    {
        private Rectangle sourceRect;

        public PictureBox(Game game, Rectangle sourceRect)
            : base(game)
        {
            this.sourceRect = sourceRect;
        }
        protected override void onDraw(XnaDrawArgs e)
        {
            e.SpriteBatch.Draw(((XnaUITestGame)Game).SpriteSheet, e.Location, sourceRect, Color.White);
        }
    }
}
