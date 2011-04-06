﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTSModel.Factories;
using System.Collections;
using ZRTS.InputEngines;

namespace ZRTS.XnaCompositeView
{
    public class CommandView : XnaUIComponent
    {
        private SameSizeChildrenFlowLayout mainPanel;
        private PictureBox stopButton;
        private PictureBox moveButton;
        private PictureBox attackButton;
        private PictureBox buildButton;
        private Hashtable uiToBuildingType = new Hashtable();

        private SameSizeChildrenFlowLayout buildPanel;

        private ZRTSCompositeViewUIFactory factory = ZRTSCompositeViewUIFactory.Instance;

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


            moveButton = factory.BuildPictureBox("button", "move");
            moveButton.DrawBox = new Rectangle(GameConfig.BUTTON_MOVE * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM);
            moveButton.OnClick += handleMoveButtonClick;
            moveButton.OnMouseEnter += handleMoveButtonOver;
            moveButton.OnMouseLeave += handleMoveButtonAway;

            mainPanel.AddChild(moveButton);

            stopButton = factory.BuildPictureBox("button", "stop");
            stopButton.DrawBox = new Rectangle(GameConfig.BUTTON_STOP * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM);
            stopButton.OnClick += handleStopButtonClick;
            stopButton.OnMouseEnter += handleStopButtonOver;
            stopButton.OnMouseLeave += handleStopButtonAway;
            mainPanel.AddChild(stopButton);

            buildButton = factory.BuildPictureBox("button", "build");
            buildButton.DrawBox = new Rectangle(GameConfig.BUTTON_BUILD * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y_SECOND, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM);
            buildButton.OnClick += handleBuildButtonClick;
            buildButton.OnMouseEnter += handleBuildButtonOver;
            buildButton.OnMouseLeave += handleBuildButtonAway;
            mainPanel.AddChild(buildButton);

            attackButton = factory.BuildPictureBox("button", "attack");
            attackButton.DrawBox = new Rectangle(GameConfig.BUTTON_ATTACK * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM);
            attackButton.OnClick += handleAttackButtonClick;
            attackButton.OnMouseEnter += handleAttackButtonOver;
            attackButton.OnMouseLeave += handleAttackButtonAway;
            mainPanel.AddChild(attackButton);
            mainPanel.Visible = false;

            // Individual building buttons in the build panel

            List<String> buildingKeys = BuildingFactory.Instance.getBuildingTypes();
            foreach (String key in buildingKeys)
            {
                PictureBox buildingButton = factory.BuildPictureBox(key, "selectionAvatar");
                buildingButton.OnClick += handleBuildingButtonClick;
                buildingButton.DrawBox = new Rectangle(0, 0, 85, 85);
                buildPanel.AddChild(buildingButton);
                uiToBuildingType.Add(buildingButton, key);
            }


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
            System.Console.Out.WriteLine("Attack button is clicked!");
            // HACK // SAME EFFECT
            if (e.Bubbled && !e.Handled)
            {
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
            System.Console.Out.WriteLine("Move button is clicked!");
            //HACK // SAME EFFECT
            if (e.Bubbled && !e.Handled)
            {
                e.Handled = true;
                //moveButton.DrawBox = new Rectangle((GameConfig.BUTTON_MOVE + GameConfig.BUTTON_MOUSE_PRESS) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM);
                // First change the command and cursor
            }
        }

        private void handleMoveButtonOver(object sender, EventArgs e)
        {
            moveButton.setPicturebox(new Rectangle((GameConfig.BUTTON_MOVE + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }

        private void handleMoveButtonAway(object sender, EventArgs e)
        {
            moveButton.setPicturebox(new Rectangle((GameConfig.BUTTON_MOVE) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }

        private void handleStopButtonOver(object sender, EventArgs e)
        {
            stopButton.setPicturebox(new Rectangle((GameConfig.BUTTON_STOP + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleStopButtonAway(object sender, EventArgs e)
        {
            stopButton.setPicturebox(new Rectangle((GameConfig.BUTTON_STOP) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleBuildButtonOver(object sender, EventArgs e)
        {
            buildButton.setPicturebox(new Rectangle((GameConfig.BUTTON_BUILD + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y_SECOND, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleBuildButtonAway(object sender, EventArgs e)
        {
            buildButton.setPicturebox(new Rectangle((GameConfig.BUTTON_BUILD) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y_SECOND, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleAttackButtonOver(object sender, EventArgs e)
        {
            attackButton.setPicturebox(new Rectangle((GameConfig.BUTTON_ATTACK + GameConfig.BUTTON_MOUSE_OVER) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }
        private void handleAttackButtonAway(object sender, EventArgs e)
        {
            attackButton.setPicturebox(new Rectangle((GameConfig.BUTTON_ATTACK) * GameConfig.BUTTON_DIM, GameConfig.BUTTON_START_Y, GameConfig.BUTTON_DIM, GameConfig.BUTTON_DIM));
        }


        protected override void onDraw(XnaDrawArgs e)
        {
            e.SpriteBatch.Draw(((XnaUITestGame)Game).SpriteSheet, e.Location, new Rectangle(0, 0, 1, 1), Color.White);
        }
    }
}