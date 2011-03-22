using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTS.XnaCompositeView;
using Microsoft.Xna.Framework.Graphics;
using ZRTSModel;

namespace ZRTS
{
    public class XnaUITestGame : Game
    {
        GraphicsDeviceManager graphics;
        private static int WINDOW_HEIGHT = 720;
        private static int WINDOW_WIDTH = 1280;
        private Texture2D spriteSheet;

        public Texture2D SpriteSheet
        {
            get { return spriteSheet; }
            set { spriteSheet = value; }
        }

        public XnaUITestGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
        }

        protected override void Initialize()
        {
            ZRTSCompositeViewUIFactory.Initialize(this);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            spriteSheet = Content.Load<Texture2D>("sprites/color");
            XnaUIFrame frame = new XnaUIFrame(this);
            frame.DrawBox = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
            TestUIComponent mainView = new TestUIComponent(this, Color.Red);
            mainView.DrawBox = new Rectangle(0, 0, 1080, 520);

            TestUIComponent selectionView = new TestUIComponent(this, Color.Blue);
            selectionView.DrawBox = new Rectangle(200, 520, 1080, 200);
            selectionView.AddChild(ZRTSCompositeViewUIFactory.Instance.BuildSelectedEntityUI(new UnitComponent()));

            TestUIComponent buildingView = new TestUIComponent(this, Color.Yellow);
            buildingView.DrawBox = new Rectangle(1080, 0, 200, 520);

            TestUIComponent commandView = new TestUIComponent(this, Color.Green);
            commandView.DrawBox = new Rectangle(1080, 520, 200, 200);

            TestUIComponent minimapView = new TestUIComponent(this, Color.White);
            minimapView.DrawBox = new Rectangle(0, 520, 200, 200);

            frame.AddChild(mainView);
            frame.AddChild(selectionView);
            frame.AddChild(buildingView);
            frame.AddChild(commandView);
            frame.AddChild(minimapView);
            Components.Add(frame);
            GraphicsDevice.Viewport = new Microsoft.Xna.Framework.Graphics.Viewport(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

        }

        protected override void Draw(GameTime gameTime)
        {
            // Can I get the Icon in Cornflower Blue?
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
