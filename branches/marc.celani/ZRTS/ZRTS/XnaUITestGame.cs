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
using System.IO;
using ZRTSModel.Trigger;

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
            set { controller = value; }
        }

        public Texture2D SpriteSheet
        {
            get { return spriteSheet; }
            set { spriteSheet = value; }
        }

        public GameModel Model
        {
            get { return model; }
            set { model = value; }
        }

        public XnaUITestGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
        }

        protected override void Initialize()
        {
			string filename = "Content/savedMaps/scenario1a.map";

			LoadModelFromFile(filename);

            base.Initialize();
        }

		protected void LoadModelFromFile(string filename)
		{
			// Create or load the model.
			model = new GameModel();
			ZRTSCompositeViewUIFactory.Initialize(this);

			FileStream mapFile = File.OpenRead(filename); //tryit.map
			ScenarioXMLReader reader = new ScenarioXMLReader(mapFile);
			ScenarioComponent scenario = reader.GenerateScenarioFromXML();

			model.AddChild(scenario);

			foreach (PlayerComponent p in scenario.GetGameWorld().GetPlayerList().GetChildren())
			{
				foreach (PlayerComponent po in scenario.GetGameWorld().GetPlayerList().GetChildren())
				{
					if (p != po)
					{
						p.EnemyList.Add(po);
					}
				}
			}

			Console.WriteLine(ZRTSModel.Factories.BuildingFactory.Instance.getBuildingTypes()[0]);

			// Create the controller
			controller = new ZRTSController(this);
			Components.Add(controller);

			// Set the mouse visible
			this.IsMouseVisible = true;

			// Add triggers            
			foreach (PlayerComponent p in scenario.GetGameWorld().GetPlayerList().GetChildren())
			{
				if (p.Race.Equals("Human"))
				{
					LoseWhenAllPlayersUnitsAreDead lose = new LoseWhenAllPlayersUnitsAreDead(p, scenario);
					scenario.triggers.Add(lose);
				}
				else if (p.Race.Equals("Zombie"))
				{
					WinWhenAllEnemyUnitsDead win = new WinWhenAllEnemyUnitsDead(p, scenario);
					scenario.triggers.Add(win);
				}
			}
		}

		protected void SetupView()
		{
			view = new XnaUIFrame(this);
			view.DrawBox = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
			MapView mainView = new MapView(this);
			mainView.DrawBox = new Rectangle(0, 0, 1280, 720);

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

			view.AddChild(mainView);
			view.AddChild(selectionView);
			view.AddChild(commandView);
		}

        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            spriteSheet = Content.Load<Texture2D>("sprites/ZRTS_SpriteSheet_All"); //ZRTS_SpriteSheet_All
            font = Content.Load<SpriteFont>("Menu Font");

			SetupView();

            //Components.Add(view);
            GraphicsDevice.Viewport = new Microsoft.Xna.Framework.Graphics.Viewport(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            mouseInputEngine = new MouseInputEngine(this, view);
            Components.Add(mouseInputEngine);

            // Load Audio
            AudioManager.Initialize(Content);
            AudioManager.play("background", "normal");
        }

		protected override void Update(GameTime gameTime)
		{
			view.Update(gameTime);

			base.Update(gameTime);
		}

        protected override void Draw(GameTime gameTime)
        {
            // Can I get the Icon in Cornflower Blue?


            GraphicsDevice.Clear(Color.CornflowerBlue);

			view.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
