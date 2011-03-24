using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ZRTS.XnaCompositeView.MapViewVisitors
{
    public class DrawTileVisitor : NoOpModelComponentVisitor
    {
        private Texture2D texture;
        private Rectangle drawRectangle;
        private SpriteBatch spriteBatch;
        public DrawTileVisitor(SpriteBatch spriteBatch, Texture2D texture, Rectangle drawRectangle)
        {
            this.texture = texture;
            this.drawRectangle = drawRectangle;
            this.spriteBatch = spriteBatch;
        }
        public override void Visit(Grass grass)
        {
            spriteBatch.Draw(texture, drawRectangle, new Rectangle(2, 80, 20, 20), Color.White);
        }

        public override void Visit(Mountain mountain)
        {
            spriteBatch.Draw(texture, drawRectangle, new Rectangle(25, 80, 20, 20), Color.White);
        }

        public override void Visit(Sand sand)
        {
            spriteBatch.Draw(texture, drawRectangle, new Rectangle(48, 80, 20, 20), Color.White);
        }
    }
}
