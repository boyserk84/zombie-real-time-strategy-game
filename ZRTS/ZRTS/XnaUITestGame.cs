using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTS.XnaCompositeView;
using Microsoft.Xna.Framework.Graphics;
using ZRTSModel;
using ZRTSModel.GameModel;
using ZRTS.InputEngines;

namespace ZRTS
{
    public class XnaUITestGame : Game
    {
        GraphicsDeviceManager graphics;
        private static int WINDOW_HEIGHT = 720;
        private static int WINDOW_WIDTH = 1280;
        private Texture2D spriteSheet;
        private SpriteFont font;
        private MouseInputEngine mouseInputEngine;

        public MouseInputEngine MouseInputEngine
        {
            get { return mouseInputEngine; }
            set { mouseInputEngine = value; }
        }

        public SpriteFont Font
        {
            get { return font; }
        }
        private GameModel model;
        private ZRTSController controller;
        private XnaUIFrame view;

        public XnaUIFrame View
        {
            get { return view; }
            set { view = value; }
        }

        public ZRTSController Controller
        {
            get { return controller; }
        }

        public Texture2D SpriteSheet
        {
            get { return spriteSheet; }
            set { spriteSheet = value; }
        }

        public GameModel Model
        {
            get { return model; }
        }

        public XnaUITestGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
        }

        protected override void Initialize()
        {
            // Create or load the model.
            model = new GameModel();
            ZRTSCompositeViewUIFactory.Initialize(this);
            ScenarioComponent scenario = new ScenarioComponent(50, 50);

            // Add grass cells at each cell.
            ZRTSModel.Map map = scenario.GetGameWorld().GetMap();
            for (int i = 0; i < map.GetWidth(); i++)
            {
                for (int j = 0; j < map.GetHeight(); j++)
                {
                    CellComponent cell = new CellComponent();
                    cell.AddChild(new Sand());
                    cell.X = i;
                    cell.Y = j;
                    map.AddChild(cell);
                }
            }
            model.AddChild(scenario);
            model.GetScenario().GetGameWorld().GetPlayerList().AddChild(new PlayerComponent());
            UnitList list = ((PlayerComponent)model.GetScenario().GetGameWorld().GetPlayerList().GetChildren()[0]).GetUnitList();
            UnitComponent soldier1 = new UnitComponent();
            soldier1.Type = "soldier";

            soldier1.AttackRange = 1.5f;
            soldier1.CanAttack = true;
            soldier1.CurrentHealth = 100;
            soldier1.CanBuild = true;
            list.AddChild(soldier1);
            soldier1.PointLocation = new PointF((float)1.5, (float)1.0);

            UnitComponent soldier2 = new UnitComponent();
            soldier2.Type = "zombie";
            soldier2.IsZombie = true;
            soldier2.CurrentHealth = 100;
            soldier2.CanBuild = true;
            list.AddChild(soldier2);
            soldier2.PointLocation = new PointF((float)10.5, (float)6.1);

            // Create the controller
            controller = new ZRTSController(this);
            Components.Add(controller);

            // Set the mouse visible
            this.IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            spriteSheet = Content.Load<Texture2D>("sprites/ZRTS_SpriteSheet_All"); //ZRTS_SpriteSheet_All
            font = Content.Load<SpriteFont>("Menu Font");
            view = new XnaUIFrame(this);
            view.DrawBox = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
            MapView mainView = new MapView(this);
            mainView.DrawBox = new Rectangle(0, 0, 1280, 520);

            SelectionState selectionState = model.GetSelectionState();
            SelectionView selectionView = new SelectionView(this, selectionState);
            selectionView.DrawBox = new Rectangle(275, 520, 730, 200);
            SameSizeChildrenFlowLayout selectedEntityUIHolder = new SameSizeChildrenFlowLayout(this);
            selectedEntityUIHolder.DrawBox = new Rectangle(215, 25, 450, 150);
            selectionView.AddChild(selectedEntityUIHolder);

            CommandView commandView = new CommandView(this);
            commandView.DrawBox = new Rectangle(1005, 445, 275, 275);
            SameSizeChildrenFlowLayout commandViewButtonBox = new SameSizeChildrenFlowLayout(this);
            selectionView.commandBar = commandView; // Register commandView to selection View


            TestUIComponent minimapView = new TestUIComponent(this, Color.White);
            minimapView.DrawBox = new Rectangle(0, 445, 275, 275);

            view.AddChild(mainView);
            view.AddChild(selectionView);
            view.AddChild(commandView);
            view.AddChild(minimapView);
            
            Components.Add(view);
            GraphicsDevice.Viewport = new Microsoft.Xna.Framework.Graphics.Viewport(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            mouseInputEngine = new MouseInputEngine(this, view);
            Components.Add(mouseInputEngine);

        }

        protected override void Draw(GameTime gameTime)
        {
            // Can I get the Icon in Cornflower Blue?
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
