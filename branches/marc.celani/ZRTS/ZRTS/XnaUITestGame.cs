using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTS.XnaCompositeView;
using Microsoft.Xna.Framework.Graphics;
using ZRTSModel;
using ZRTSModel.GameModel;

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
            mainView.DrawBox = new Rectangle(0, 0, 1280, 520);

            SelectionState selectionState = new SelectionState();
            UnitList unitList = new UnitList();
            unitList.AddChild(new UnitComponent());
            unitList.AddChild(new UnitComponent());
            unitList.AddChild(new UnitComponent());
            unitList.AddChild(new UnitComponent());
            unitList.AddChild(new UnitComponent());
            unitList.AddChild(new UnitComponent());
            unitList.AddChild(new UnitComponent());

            SelectionView selectionView = new SelectionView(this, selectionState);
            selectionView.DrawBox = new Rectangle(275, 520, 730, 200);
            SameSizeChildrenFlowLayout selectedEntityUIHolder = new SameSizeChildrenFlowLayout(this);
            selectedEntityUIHolder.DrawBox = new Rectangle(215, 25, 450, 150);
            selectionView.AddChild(selectedEntityUIHolder);

            // Hack: Refresh selection state.
            selectionState.AddChild(unitList);

            TestUIComponent commandView = new TestUIComponent(this, Color.White);
            commandView.DrawBox = new Rectangle(1005, 445, 275, 275);

            SameSizeChildrenFlowLayout commandViewButtonBox = new SameSizeChildrenFlowLayout(this);
            commandViewButtonBox.DrawBox = new Rectangle(10, 10, 255, 255);
            commandView.AddChild(commandViewButtonBox);

            TestUIComponent exampleButton1 = new TestUIComponent(this, Color.Red);
            exampleButton1.DrawBox = new Rectangle(0, 0, 85, 85);
            TestUIComponent exampleButton2 = new TestUIComponent(this, Color.Red);
            exampleButton2.DrawBox = new Rectangle(0, 0, 85, 85);
            TestUIComponent exampleButton3 = new TestUIComponent(this, Color.Red);
            exampleButton3.DrawBox = new Rectangle(0, 0, 85, 85);
            TestUIComponent exampleButton4 = new TestUIComponent(this, Color.Red);
            exampleButton4.DrawBox = new Rectangle(0, 0, 85, 85);
            commandViewButtonBox.AddChild(exampleButton1);
            commandViewButtonBox.AddChild(exampleButton2);
            commandViewButtonBox.AddChild(exampleButton3);
            commandViewButtonBox.AddChild(exampleButton4);

            TestUIComponent minimapView = new TestUIComponent(this, Color.White);
            minimapView.DrawBox = new Rectangle(0, 445, 275, 275);

            frame.AddChild(mainView);
            frame.AddChild(selectionView);
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
