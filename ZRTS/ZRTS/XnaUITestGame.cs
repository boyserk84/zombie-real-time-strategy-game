﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTS.XnaCompositeView;
using Microsoft.Xna.Framework.Graphics;
using ZRTSModel;
using ZRTSModel.GameModel;
using ZRTS.InputEngines;
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
			model.GetScenario().GetGameWorld().GetPlayerList().AddChild(new PlayerComponent());
            UnitList list = ((PlayerComponent)model.GetScenario().GetGameWorld().GetPlayerList().GetChildren()[0]).GetUnitList();
			UnitList list2 = ((PlayerComponent)model.GetScenario().GetGameWorld().GetPlayerList().GetChildren()[1]).GetUnitList();
            
            UnitComponent soldier1 = new UnitComponent();
            soldier1.Type = "worker";
            soldier1.IsZombie = false;
            soldier1.AttackRange = 6.0f;
            soldier1.CanAttack = true;
            soldier1.CurrentHealth = 100;
            soldier1.CanBuild = true;
            list.AddChild(soldier1);
            soldier1.PointLocation = new PointF((float)1.5, (float)1.5);
			soldier1.Speed = 0.25f;

            UnitComponent soldier2 = new UnitComponent();
            soldier2.Type = "zombie";
            soldier2.IsZombie = true;
            soldier2.CurrentHealth = 100;
            soldier2.CanBuild = true;
			soldier2.CanAttack = true;
			soldier2.AttackStance = UnitComponent.UnitAttackStance.Aggressive;
            list2.AddChild(soldier2);
            soldier2.PointLocation = new PointF((float)10.5, (float)6.1);

			UnitComponent soldier3 = new UnitComponent();
			soldier3.Type = "zombie";
			soldier3.IsZombie = true;
			soldier3.CurrentHealth = 100;
			soldier3.CanBuild = true;
			soldier3.CanAttack = true;
			soldier3.AttackStance = UnitComponent.UnitAttackStance.Aggressive;
			list2.AddChild(soldier3);
			soldier3.PointLocation = new PointF((float)12.5, (float)4.0);


                UnitComponent[] zombiesList = new UnitComponent[5];
                for (int i = 0; i < zombiesList.GetLength(0); ++i)
                {
                    zombiesList[i] = new UnitComponent();
                    zombiesList[i].Type = "zombie";
                    zombiesList[i].IsZombie = true;
                    zombiesList[i].CurrentHealth = 100;
                    zombiesList[i].CanBuild = true;
                    zombiesList[i].CanAttack = true;
                    zombiesList[i].AttackRange = 2.5f;
                    zombiesList[i].AttackStance = UnitComponent.UnitAttackStance.Aggressive;
                    list2.AddChild(zombiesList[i]);
                    zombiesList[i].PointLocation = new PointF((float) 10f + i*1.5f, (float) 10f + i * 1.0f);
                }
            

			PlayerComponent player1 = (PlayerComponent)model.GetScenario().GetGameWorld().GetPlayerList().GetChildren()[0];
			PlayerComponent player2 = (PlayerComponent)model.GetScenario().GetGameWorld().GetPlayerList().GetChildren()[1];

			player1.EnemyList.Add(player2);
			player2.EnemyList.Add(player1);

			Console.WriteLine(ZRTSModel.Factories.BuildingFactory.Instance.getBuildingTypes()[0]);

            // Create the controller
            controller = new ZRTSController(this);
            Components.Add(controller);

            // Set the mouse visible
            this.IsMouseVisible = true;

			// Add triggers
			LoseWhenAllPlayersUnitsAreDead lose = new LoseWhenAllPlayersUnitsAreDead(player1, scenario);
			scenario.triggers.Add(lose);
			WinWhenAllEnemyUnitsDead win = new WinWhenAllEnemyUnitsDead(player2, scenario);
			scenario.triggers.Add(win);

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


            TestUIComponent minimapView = new TestUIComponent(this, new Color(100, 60, 88));
            minimapView.DrawBox = new Rectangle(0, 445, 275, 275);

            view.AddChild(mainView);
            view.AddChild(selectionView);
            view.AddChild(commandView);
            view.AddChild(minimapView);
            
            Components.Add(view);
            GraphicsDevice.Viewport = new Microsoft.Xna.Framework.Graphics.Viewport(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            mouseInputEngine = new MouseInputEngine(this, view);
            Components.Add(mouseInputEngine);

            // Load Audio
            AudioManager.Initialize(Content);

        }

        protected override void Draw(GameTime gameTime)
        {
            // Can I get the Icon in Cornflower Blue?


            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
