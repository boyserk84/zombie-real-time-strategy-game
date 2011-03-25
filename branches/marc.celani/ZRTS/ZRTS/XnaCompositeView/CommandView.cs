using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZRTS.XnaCompositeView
{
    public class CommandView : XnaUIComponent
    {
        private SameSizeChildrenFlowLayout mainPanel;
        private PictureBox stopButton;
        private PictureBox moveButton;
        private PictureBox attackButton;
        private PictureBox buildButton;

        private SameSizeChildrenFlowLayout buildPanel;

        public CommandView(Game game)
            : base(game)
        {
            this.DrawBox = new Rectangle(0, 0, 275, 275);

            // buildPanel
            buildPanel = new SameSizeChildrenFlowLayout(game);
            buildPanel.Visible = false;
            buildPanel.DrawBox = new Rectangle(10, 10, 255, 255);
            AddChild(buildPanel);

            // mainPanel
            mainPanel = new SameSizeChildrenFlowLayout(game);
            mainPanel.DrawBox = new Rectangle(10, 10, 255, 255);
            AddChild(mainPanel);

            ZRTSCompositeViewUIFactory factory = ZRTSCompositeViewUIFactory.Instance;
            moveButton = factory.BuildPictureBox("button", "move");
            moveButton.DrawBox = new Rectangle(0, 0, 85, 85);
            mainPanel.AddChild(moveButton);

            stopButton = factory.BuildPictureBox("button", "stop");
            stopButton.DrawBox = new Rectangle(0, 0, 85, 85);
            mainPanel.AddChild(stopButton);

            buildButton = factory.BuildPictureBox("button", "build");
            buildButton.DrawBox = new Rectangle(0, 0, 85, 85);
            mainPanel.AddChild(buildButton);

            attackButton = factory.BuildPictureBox("button", "attack");
            attackButton.DrawBox = new Rectangle(0, 0, 85, 85);
            mainPanel.AddChild(attackButton);

            
        }

        protected override void onDraw(XnaDrawArgs e)
        {
            e.SpriteBatch.Draw(((XnaUITestGame)Game).SpriteSheet, e.Location, new Rectangle(0, 0, 1, 1), Color.White);
        }
    }
}
