﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using Microsoft.Xna.Framework;

namespace ZRTS.XnaCompositeView
{
    public class ZRTSCompositeViewUIFactory
    {
        private static ZRTSCompositeViewUIFactory instance;
        private Game game;

        private ZRTSCompositeViewUIFactory()
        {
            
        }

        private ZRTSCompositeViewUIFactory(Game game)
        {
            // TODO: Initialize based on the xml files.
            this.game = game;
        }

        public static void Initialize(Game game)
        {
            if (instance == null)
            {
                instance = new ZRTSCompositeViewUIFactory(game);
            }
        }
        public static ZRTSCompositeViewUIFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public SelectedEntityUI BuildSelectedEntityUI(UnitComponent unit)
        {
            SelectedEntityUI seui = new SelectedEntityUI(game, unit);
            seui.DrawBox = new Rectangle(0, 0, 75, 75);

            // Add the HP Bar to the UI.
            HPBar hpBar = new HPBar(game);
            hpBar.MaxHP = unit.MaxHealth;
            hpBar.CurrentHP = unit.CurrentHealth;
            hpBar.DrawBox = new Rectangle(5, 67, 65, 5);

            PictureBox pictureBox = BuildPictureBox(unit.Type, "selectionAvatar");
            pictureBox.DrawBox = new Rectangle(7, 3, 61, 61);
            seui.AddChild(pictureBox);
            seui.AddChild(hpBar);
            return seui;
        }

        public SelectedEntityUI BuildSelectedEntityUI(Building building)
        {
            SelectedEntityUI seui = new SelectedEntityUI(game, building);
            seui.DrawBox = new Rectangle(0, 0, 75, 75);

            // Add the HP Bar to the UI.
            HPBar hpBar = new HPBar(game);
            hpBar.MaxHP = building.MaxHealth;
            hpBar.CurrentHP = building.CurrentHealth;
            hpBar.DrawBox = new Rectangle(5, 67, 65, 5);

            PictureBox pictureBox = BuildPictureBox(building.Type, "selectionAvatar");
            pictureBox.DrawBox = new Rectangle(7, 3, 61, 61);
            seui.AddChild(pictureBox);
            seui.AddChild(hpBar);
            return seui;
        }

        public PictureBox BuildPictureBox(string type, string subtype)
        {
            PictureBox pictureBox = null;
            // TODO: Add in logic to parse from XML that returns the appropriate rectangle from the type and subtype.
            if (type.Equals("soldier") && subtype.Equals("selectionAvatar"))
            {
                pictureBox = new PictureBox(game, new Rectangle(2, 0, 75, 75));
            }
            else if (type.Equals("soldier") && subtype.Equals("bigAvatar"))
            {
                pictureBox = new PictureBox(game, new Rectangle(80, 0, 150, 150));
            }
            else
                pictureBox = new PictureBox(game, new Rectangle(0, 0, 1, 1));
            return pictureBox;
        }

        /// <summary>
        /// Construct a unit UI (image representation)
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public UnitUI BuildUnitUI(UnitComponent unit)
        {
            UnitUI unitUI = null;
            if (unit.Type.Equals("soldier"))
            {
                unitUI = new UnitUI(game, unit, new Rectangle(0, 157, 36, 36));
                //unitUI = new UnitUI(game, unit, new Rectangle(2, 128, 16, 27));
                unitUI.DrawBox = new Rectangle(0, 0, GameConfig.UNIT_WIDTH, GameConfig.UNIT_HEIGHT);
            }
             // ADD NEW UNIT UI FOR EACH UNIT TYPE HERE
            else if (unit.Type.Equals("zombie"))
            {
                unitUI = new UnitUI(game, unit, new Rectangle(0, 157, 36, 36));
                unitUI.DrawBox = new Rectangle(0, 0, GameConfig.UNIT_WIDTH, GameConfig.UNIT_HEIGHT);
                //unitUI.DrawBox = new Rectangle(20, 0, 32, 54);
            }
            return unitUI;
        }

        internal BuildingUI BuildBuildingUI(Building building)
        {
            BuildingUI buildingUI = null;
            buildingUI = new BuildingUI(game, building, new Rectangle(0, 0, 1, 1));
            buildingUI.DrawBox = new Rectangle(0, 0, 120, 120);
            return buildingUI;
        }
    }
}