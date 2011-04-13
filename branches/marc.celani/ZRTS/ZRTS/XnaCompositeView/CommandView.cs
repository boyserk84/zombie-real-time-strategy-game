using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTSModel.Factories;
using System.Collections;
using ZRTS.InputEngines;
using Microsoft.Xna.Framework.Graphics;

namespace ZRTS.XnaCompositeView
{
    public class CommandView : XnaUIComponent
    {
        private SameSizeChildrenFlowLayout mainPanel;
        private PictureBox backgroundPanel;
        private PictureBox mainBgPanel;
        private PictureBox stopButton;
        private PictureBox moveButton;
        private PictureBox attackButton;
        private PictureBox buildButton;
        private Hashtable uiToBuildingType = new Hashtable();
		private Texture2D pixel;
		private Color color;

        private SameSizeChildrenFlowLayout buildPanel;

		private SameSizeChildrenFlowLayout produceUnitPanel;
		private ZRTSCompositeViewUIFactory factory = ZRTSCompositeViewUIFactory.Instance;
		long lastClick = 0;
		const int MSECONDS_PER_CLICK = 100;

        public CommandView(Game game)
            : base(game)
        {
            this.DrawBox = new Rectangle(0, 0, 275, 275);


            // Background Panel
            backgroundPanel = new PictureBox(game, new Rectangle(GameConfig.BUILDPANEL_START_X, GameConfig.BUILDING_START_Y, 275, 275));
            backgroundPanel.DrawBox = new Rectangle(0, 0, 275, 275);
            AddChild(backgroundPanel);


            // TODO: Need to eliminate dark purple solid background


            // buildPanel
            buildPanel = new SameSizeChildrenFlowLayout(game);
            buildPanel.Visible = false;
            buildPanel.DrawBox = new Rectangle(10, 10, 255, 255);
            AddChild(buildPanel);

            // mainPanel
            mainPanel = new SameSizeChildrenFlowLayout(game);
            mainPanel.DrawBox = new Rectangle(10, 10, 255, 255);
            AddChild(mainPanel);

			// Produce Units Panel
			produceUnitPanel = new SameSizeChildrenFlowLayout(game);
			produceUnitPanel.DrawBox = new Rectangle(10, 10, 255, 255);
			produceUnitPanel.Visible = false;
			AddChild(produceUnitPanel);



            ZRTSCompositeViewUIFactory factory = ZRTSCompositeViewUIFactory.Instance;
            moveButton = factory.BuildPictureBox("button", "move");
            moveButton.DrawBox = new Rectangle(0, 0, GameConfig.BUTTON_DIM , GameConfig.BUTTON_DIM );
            moveButton.OnClick += handleMoveButtonClick;
            moveButton.OnMouseEnter += handleMoveButtonOver;
            moveButton.OnMouseLeave += handleMoveButtonAway;
            moveButton.OnMouseDown += handleMoveButtonDown;
            moveButton.OnMouseUp += handleMoveButtonUp;

            mainPanel.AddChild(moveButton);

            stopButton = factory.BuildPictureBox("button", "stop");
            stopButton.DrawBox = new Rectangle(0, 0, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM);
            stopButton.OnClick += handleStopButtonClick;
            stopButton.OnMouseEnter += handleStopButtonOver;
            stopButton.OnMouseLeave += handleStopButtonAway;
            stopButton.OnMouseDown += handleStopButtonDown;
            stopButton.OnMouseUp += handleStopButtonUp;
            mainPanel.AddChild(stopButton);

            buildButton = factory.BuildPictureBox("button", "build");
            buildButton.DrawBox = new Rectangle(GameConfig.BUTTON_BUILD * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y_SECOND, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM);
            buildButton.OnClick += handleBuildButtonClick;
            buildButton.OnMouseEnter += handleBuildButtonOver;
            buildButton.OnMouseLeave += handleBuildButtonAway;
            buildButton.OnMouseDown += handleBuildButtonDown;
            buildButton.OnMouseUp += handleBuildButtonUp;
            mainPanel.AddChild(buildButton);

            attackButton = factory.BuildPictureBox("button", "attack");
            attackButton.DrawBox = new Rectangle(GameConfig.BUTTON_ATTACK * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM);
            attackButton.OnClick += handleAttackButtonClick;
            attackButton.OnMouseEnter += handleAttackButtonOver;
            attackButton.OnMouseLeave += handleAttackButtonAway;
            attackButton.OnMouseDown += handleAttackButtonDown;
            attackButton.OnMouseUp += handleAttackButtonUp;
            mainPanel.AddChild(attackButton);
            mainPanel.Visible = false;

            // Individual building buttons in the build panel

            List<String> buildingKeys = BuildingFactory.Instance.getBuildingTypes();
            foreach (String key in buildingKeys)
            {
                PictureBox buildingButton = factory.BuildPictureBox("building", key);
                buildingButton.OnClick += handleBuildingButtonClick;
                buildingButton.DrawBox = new Rectangle(0, 0, 85, 85);
                buildPanel.AddChild(buildingButton);
                uiToBuildingType.Add(buildingButton, key);
            }

            // Get unit's information 
			List<String> unitKeys = UnitFactory.Instance.getPrefixes();

			foreach (String key in buildingKeys)
			{
                PictureBox unitButton = null;
                if (key.Equals("barracks"))
                {
                    unitButton = factory.BuildPictureBox("unitBuild", "soldier");
                    unitButton.OnClick += handleUnitProduceButtonClick;
                    unitButton.DrawBox = new Rectangle(0, 0, 85, 85);
                    produceUnitPanel.AddChild(unitButton);
                    uiToBuildingType.Add(unitButton, key);
                    continue;
                }
                else if (key.Equals("house"))
                {
                    unitButton = factory.BuildPictureBox("unitBuild", "worker");
                    unitButton.OnClick += handleUnitProduceButtonClick;
                    unitButton.DrawBox = new Rectangle(0, 0, 85, 85);
                    produceUnitPanel.AddChild(unitButton);
                    uiToBuildingType.Add(unitButton, key);
                    continue;
                }
			}


			pixel = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });
			color = new Color(100, 60, 88);
            
        }

        /// <summary>
        /// hide all command buttons
        /// </summary>
        public void disableButtons()
        {
            mainPanel.Visible = false;
        }

        /// <summary>
        /// Show all command buttons
        /// </summary>
        public void activateButtons()
        {
            mainPanel.Visible = true;
        }

		public void activateProduceUnitButtons()
		{
			produceUnitPanel.Visible = true;
		}

		public void deactivateProduceUnitButtons()
		{
			produceUnitPanel.Visible = false;
		}


        private void handleBuildingButtonClick(Object sender, XnaMouseEventArgs e)
        {
            if (e.Bubbled && !e.Handled)
            {
                string buildingType = uiToBuildingType[sender] as string;
                ((XnaUITestGame)Game).Controller.OnSelectBuildingToBuild(buildingType);
                mainPanel.Visible = true;
                buildPanel.Visible = false;
                e.Handled = true;
            }
        }

        private void handleBuildButtonClick(Object sender, XnaMouseEventArgs e)
        {
            if (e.Bubbled && !e.Handled)
            {
                mainPanel.Visible = false;
                buildPanel.Visible = true;
                e.Handled = true;
            }
        }

        /// <summary>
        /// Trigger action when stop button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handleStopButtonClick(object sender, XnaMouseEventArgs e)
        {
            System.Console.Out.WriteLine("STOP BUTTON IS CLICKED!");
            if (e.Bubbled && !e.Handled)
            {
                e.Handled = true;
                ((XnaUITestGame)Game).Controller.OnSelectUnitToStop();
            }
        }

        /// <summary>
        /// Trigger attack when attack button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handleAttackButtonClick(object sender, XnaMouseEventArgs e)
        {
            // HACK // SAME EFFECT
            if (e.Bubbled && !e.Handled)
            {
                System.Console.Out.WriteLine("Attack button is clicked!");
                e.Handled = true;
                //((XnaUITestGame)Game).Controller.TellSelectedUnitsToAttack((ZRTSModel.UnitComponent) sender);
            }

        }

        /// <summary>
        /// Trigger move command when move button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handleMoveButtonClick(object sender, XnaMouseEventArgs e)
        {
            //HACK // SAME EFFECT
            if (e.Bubbled && !e.Handled)
            {
                System.Console.Out.WriteLine("Move button is clicked!");
                e.Handled = true;
                //moveButton.DrawBox = new Rectangle((GameConfig.BUTTON_MOVE + GameConfig.BUTTON_MOUSE_PRESS) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM);
                // First change the command and cursor
            }
        }

        // Move Button
        private void handleMoveButtonOver(object sender, EventArgs e)
        {
            moveButton.setPicturebox(new Rectangle((GameConfig.BUTTON_MOVE + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleMoveButtonAway(object sender, EventArgs e)
        {
            moveButton.setPicturebox(new Rectangle((GameConfig.BUTTON_MOVE) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleMoveButtonDown(object sender, EventArgs e)
        {
            moveButton.setPicturebox(new Rectangle((GameConfig.BUTTON_MOVE + GameConfig.BUTTON_MOUSE_PRESS) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleMoveButtonUp(object sender, EventArgs e)
        {
            moveButton.setPicturebox(new Rectangle((GameConfig.BUTTON_MOVE + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        
        // Stop Button
        private void handleStopButtonOver(object sender, EventArgs e)
        {
            stopButton.setPicturebox(new Rectangle((GameConfig.BUTTON_STOP + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleStopButtonAway(object sender, EventArgs e)
        {
            stopButton.setPicturebox(new Rectangle((GameConfig.BUTTON_STOP) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleStopButtonDown(object sender, EventArgs e)
        {
            stopButton.setPicturebox(new Rectangle((GameConfig.BUTTON_STOP + GameConfig.BUTTON_MOUSE_PRESS) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleStopButtonUp(object sender, EventArgs e)
        {
            stopButton.setPicturebox(new Rectangle((GameConfig.BUTTON_STOP + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }

        // Build Button
        private void handleBuildButtonOver(object sender, EventArgs e)
        {
            buildButton.setPicturebox(new Rectangle((GameConfig.BUTTON_BUILD + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y_SECOND, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleBuildButtonAway(object sender, EventArgs e)
        {
            buildButton.setPicturebox(new Rectangle((GameConfig.BUTTON_BUILD) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y_SECOND, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleBuildButtonDown(object sender, EventArgs e)
        {
            buildButton.setPicturebox(new Rectangle((GameConfig.BUTTON_BUILD + GameConfig.BUTTON_MOUSE_PRESS) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y_SECOND, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleBuildButtonUp(object sender, EventArgs e)
        {
            buildButton.setPicturebox(new Rectangle((GameConfig.BUTTON_BUILD + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y_SECOND, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }

        // Attack Button
        private void handleAttackButtonOver(object sender, EventArgs e)
        {
            attackButton.setPicturebox(new Rectangle((GameConfig.BUTTON_ATTACK + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleAttackButtonAway(object sender, EventArgs e)
        {
            attackButton.setPicturebox(new Rectangle((GameConfig.BUTTON_ATTACK) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleAttackButtonDown(object sender, EventArgs e)
        {
            attackButton.setPicturebox(new Rectangle((GameConfig.BUTTON_ATTACK + GameConfig.BUTTON_MOUSE_PRESS) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleAttackButtonUp(object sender, EventArgs e)
        {
            attackButton.setPicturebox(new Rectangle((GameConfig.BUTTON_ATTACK + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }

        private void handleUnitProduceButtonClick(object sender, XnaMouseEventArgs e)
        {
            if (e.time - lastClick > MSECONDS_PER_CLICK)
            {
                ((XnaUITestGame)Game).Controller.TellSelectedBuildingToBuild();
                e.Handled = true;
                lastClick = e.time;
            }
        }

        protected override void onDraw(XnaDrawArgs e)
        {

            //e.SpriteBatch.Draw(pixel, e.Location, new Rectangle(0, 0, 1, 1), color);
        }
    }
}
