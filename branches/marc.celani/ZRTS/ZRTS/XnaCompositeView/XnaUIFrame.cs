using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZRTS.XnaCompositeView
{
    public class XnaUIFrame : XnaUIComponent
    {
        private SpriteBatch spriteBatch;

        public XnaUIFrame(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        protected override void onDraw(XnaDrawArgs e)
        {
            // Do nothing
        }

        public override SpriteBatch GetSpriteBatch(XnaUIComponent requester)
        {
            return spriteBatch;
        }

    }
}
