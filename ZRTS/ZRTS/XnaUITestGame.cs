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
using System.IO;
using ZRTSModel.Trigger;
using Microsoft.Xna.Framework.Input;
//using ZRTS.Button;

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

        SpriteBatch spriteBatch;
        public gameState state = gameState.Menu;
        private List<Button> buttonList= new List<Button>();

        public enum gameState
        {
            Menu,
            Gameplay,
            Win,
            Lose
        };  

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

        /// <summary>
        /// Constructor
        /// </summary>
        public XnaUITestGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
        }

        /// <summary>
        /// Initialization
        /// </summary>
        protected override void Initialize()
        {
            initializeButtons();

            base.Initialize();
        }


        /// <summary>
        /// Load back-end game contents
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content";
            spriteSheet = Content.Load<Texture2D>("sprites/ZRTS_SpriteSheet_All"); //ZRTS_SpriteSheet_All
            font = Content.Load<SpriteFont>("Menu Font");
        }



        /// <summary>
        /// Initalize buttons for title menu
        /// </summary>
        private void initializeButtons()
        {
            buttonList.Add(new Button((int)(WINDOW_WIDTH * .55), (int)(WINDOW_HEIGHT * .1), (int)(WINDOW_WIDTH * .4), (int)(WINDOW_HEIGHT * (.2)), "Level 1"));
            buttonList.Add(new Button((int)(WINDOW_WIDTH * .55), (int)(WINDOW_HEIGHT * .4), (int)(WINDOW_WIDTH * .4), (int)(WINDOW_HEIGHT * (.2)), "Level 2"));
            buttonList.Add(new Button((int)(WINDOW_WIDTH * .55), (int)(WINDOW_HEIGHT * .7), (int)(WINDOW_WIDTH * .4), (int)(WINDOW_HEIGHT * (.2)), "Level 3"));
            buttonList.Add(new Button((int)(WINDOW_WIDTH * .05), (int)(WINDOW_HEIGHT * .7), (int)(WINDOW_WIDTH * .4), (int)(WINDOW_HEIGHT * (.2)), "Quit"));
        }


        /// <summary>
        /// Load information from the map file
        /// </summary>
        /// <param name="filename"></param>
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

			// Create the controller, Remove the old one if it exists.
			if (this.controller != null)
			{
				Components.Remove(this.controller);
			}

			controller = new ZRTSController(this);
			Components.Add(controller);

			// Set the mouse visible
			this.IsMouseVisible = true;

			// Add triggers            
			foreach (PlayerComponent p in scenario.GetGameWorld().GetPlayerList().GetChildren())
			{
				if (p.Race.Equals("Human"))
				{
					WinWhenAllEnemyUnitsDead win = new WinWhenAllEnemyUnitsDead(p, scenario);
					scenario.triggers.Add(win);
				}
				else if (p.Race.Equals("Zombie"))
				{
					LoseWhenAllPlayersUnitsAreDead lose = new LoseWhenAllPlayersUnitsAreDead(p, scenario);
					scenario.triggers.Add(lose);
				}
			}
		}

        /// <summary>
        /// Setup View for the gameplay
        /// </summary>
		private void SetupView()
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

        /// <summary>
        /// Setup game view and all associated engine
        /// </summary>
        private void SetupGame()
        {
            SetupView();
            Components.Add(view);
            GraphicsDevice.Viewport = new Microsoft.Xna.Framework.Graphics.Viewport(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
            mouseInputEngine = new MouseInputEngine(this, view);
            Components.Add(mouseInputEngine);
            // Load Audio
            AudioManager.Initialize(Content);
            //AudioManager.play("background", "normal");
        }

        /// <summary>
        /// Reset all game related contents
        /// </summary>
        private void ResetGame()
        {
            Components.Remove(view);
            mouseInputEngine.Dispose();
            view.Dispose();
        }


        /// <summary>
        /// Update game component
        /// </summary>
        /// <param name="gameTime">Gametime </param>
		protected override void Update(GameTime gameTime)
		{

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && (state == gameState.Gameplay || state == gameState.Win))
            {
                ResetGame();
                state = gameState.Menu;
            }
            else if (state == gameState.Win)
            {
                ResetGame();
            }

            if (state == gameState.Menu)
            {
                updateMenu();
            }

			base.Update(gameTime);
		}

        /// <summary>
        /// Listen for user interaction to the title menu
        /// </summary>
        private void updateMenu()
        {

            string filename = "";
            MouseState current_mouse = Mouse.GetState();
            IsMouseVisible = true;

            if (current_mouse.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    if(buttonList[i].contains(current_mouse.X,current_mouse.Y))
                    {
                        if (buttonList[i].Name.Equals("Level 1"))
                        {
                            filename = "Content/savedMaps/scenario1a.map";
                            LoadModelFromFile(filename);
                            SetupGame();
                            state = gameState.Gameplay;

                        }
                        if (buttonList[i].Name.Equals("Level 2"))
                        {
                            filename = "Content/savedMaps/hardScenerio.map";   
                            LoadModelFromFile(filename);
                            SetupGame();
                            state = gameState.Gameplay;
                        }
                        if (buttonList[i].Name.Equals("Level 3"))
                        {
                            filename = "Content/savedMaps/easyScenerio.map";   
                            LoadModelFromFile(filename);
                            SetupGame();
                            state = gameState.Gameplay;
                        }
                        if (buttonList[i].Name.Equals("Quit"))
                        {
                            this.Exit();
                        }

                        i = buttonList.Count;//break;
                    }
                }
            }
               
        }

        /// <summary>
        /// Draw game contents
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            // Can I get the Icon in Cornflower Blue?
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (state == gameState.Menu)
            {
                drawMenu();
            }
            else if (state == gameState.Win)
            {
                spriteBatch.Begin();
                // Replace this with WINNING SCREEN
                spriteBatch.DrawString(font, "WIN DAMIN IT press ESC ", new Vector2(200,200), Color.Black, 0, new Vector2(0), 5f, SpriteEffects.None, 0.5f);
                spriteBatch.End();
            }

            
            base.Draw(gameTime);
        }

        /// <summary>
        /// Show title menu to user
        /// </summary>
        private void drawMenu()
        {
            
            spriteBatch.Begin();

            //TO DO: Draw Menu Background
            //spriteBatch.Draw(spriteSheet, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), new Rectangle(0, GameConfig.TILE_START_Y, GameConfig.TILE_WIDTH, GameConfig.TILE_HEIGHT), Color.White);

            //////I consider this a placeholder it should be removed when we have a menu background
            Vector2 pos = new Vector2((int)(WINDOW_WIDTH * .05), (int)(WINDOW_HEIGHT * .1));
            spriteBatch.DrawString(font, "ZRTS", pos, Color.Black, 0, new Vector2(0), 5f, SpriteEffects.None, 0.5f);
            //////

            foreach (Button b in buttonList)
            {
                Rectangle destination = new Rectangle((int)b.PointLocation.X, (int)b.PointLocation.Y, (int)b.Width, (int)b.Height);
                Rectangle source = new Rectangle(GameConfig.MAINPANEL_START_X, GameConfig.MAINPANEL_START_Y, GameConfig.MAINPANEL_WIDTH, GameConfig.MAINPANEL_HEIGHT);
                
                spriteBatch.Draw(spriteSheet, destination, source, Color.White);
                string output = b.Name;
                Vector2 FontOrigin = font.MeasureString(output)/2;
                pos = new Vector2(b.PointLocation.X + (b.Width / 2), b.PointLocation.Y + (b.Height / 2));
                spriteBatch.DrawString(font, output, pos, Color.Black, 0, FontOrigin, 5f, SpriteEffects.None, 0.5f);
            }

            spriteBatch.End();
        }
    }
}
