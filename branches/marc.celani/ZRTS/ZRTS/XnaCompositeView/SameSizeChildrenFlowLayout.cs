using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZRTS.XnaCompositeView
{
    /// <summary>
    /// This class lays its children out in a flow, using the assumption that all children are the same size.  This optimization
    /// makes the DoLayout() function faster, and is the primary use case in this game.
    /// </summary>
    public class SameSizeChildrenFlowLayout : XnaUIComponent
    {
        public SameSizeChildrenFlowLayout(Game game)
            : base(game)
        {
        }

        protected override void onDraw(XnaDrawArgs e)
        {
			Texture2D pixel = new Texture2D(e.SpriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });
            e.SpriteBatch.Draw(pixel, e.Location, new Rectangle(0, 0, 1, 1), Color.Blue);
        }

        public override void DoLayout()
        {
            int count = GetChildren().Count;
            if (count > 0)
            {
                XnaUIComponent firstComponent = GetChildren()[0];
                int boxWidth = firstComponent.DrawBox.Width;
                int boxHeight = firstComponent.DrawBox.Height;
                int numberOfElementInRow = DrawBox.Width / (boxWidth);
                int current = 0;
                foreach (XnaUIComponent component in GetChildren())
                {
                    component.DrawBox = new Rectangle((current % numberOfElementInRow) * boxWidth, (current / numberOfElementInRow) * boxHeight, boxWidth, boxHeight);
                    current++;
                }
            }
        }
    }
}
