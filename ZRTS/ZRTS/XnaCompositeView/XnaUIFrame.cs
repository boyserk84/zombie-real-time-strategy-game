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
            ((XnaUITestGame)game).Model.VictoryStateChangedHandler += onVictory;
        }

        public void onVictory(object sender, ZRTSModel.EventHandlers.GameVictoryStateChangeEventArgs e)
        {
            // Show image
            //PictureBox image = new PictureBox(Game, new Rectangle(865, 1320,470, 92));
            //AddChild(image);
            //image.DrawBox = new Rectangle((1280/2) - 300, 720/2, 470, 92); // location on the screen
            ((XnaUITestGame)Game).state = XnaUITestGame.gameState.Win;
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
