using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTS;
using ZRTSModel;
using ZRTSModel.GameModel;
using ZRTSModel.Factories;
using NUnit.Framework;
namespace ZRTS
{
    /// <summary>
    /// Testing placing building on the map and tell the selected units to build a building on the map
    /// 
    /// This testsuite is actually testing the followings:
    /// - Check if the player's unit is selected on the map (simulate)
    /// - Check if the player's unit is given a correct action to build the building (simulate)
    /// - Check if the building can be placed on the map under various conditions
    /// 
    /// </summary>
    [TestFixture]
    class TestPlacingBuildingOntheMap
    {
        private GameModel model;
        private PlayerComponent player1, player2, player;
        private XnaUITestGame game;
        private List<ModelComponent> unitList;

        /// <summary>
        /// Initialize necessary components
        /// </summary>
        private void initialize()
        {
            game = new XnaUITestGame();
            ZRTSController controller = new ZRTSController(game);
            game.Components.Add(controller);

            model = new GameModel();
            ScenarioComponent scenario = new ScenarioComponent(50, 50);
            player1 = new PlayerComponent();
            player2 = new PlayerComponent();
            player1.SetName("Nate");
            player2.SetName("Smith");


            // Add sand cells at each cell.
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
            game.Model = model;  
            game.Model.PlayerInContext = player1;   // Set a main Player

            //Create two players and set them to be enemies.
            game.Model.GetScenario().GetGameWorld().GetPlayerList().AddChild(player1);
            game.Model.GetScenario().GetGameWorld().GetPlayerList().AddChild(player2);

            // Set target enemy
            player1.EnemyList.Add(player2);
            player2.EnemyList.Add(player1);

            game.Controller = controller;

            // Add worker to player
            unitList = new List<ModelComponent>();
            unitList.Add(new UnitComponent());
            ((UnitComponent)unitList[0]).CanBuild = true;
            ((UnitComponent)unitList[0]).Type = "worker";
            ((UnitComponent)unitList[0]).PointLocation = new PointF(20, 20);


            //(1) Get Player
            player = (PlayerComponent)((XnaUITestGame)game).Model.GetScenario().GetGameWorld().GetPlayerList().GetChildren()[0];

            // check if fetch the corret player
            Assert.AreEqual("Nate", player.GetName());

            // (2) Add the actual player's unit list
            player.GetUnitList().AddChild(unitList[0]);

            
        }

        /// <summary>
        /// Test if correct player's unit is selected.
        /// </summary>
        [Test]
        public void TestSelectCorrectUnit()
        {
            initialize();
            // (3) Simulate the player's selected unit on the screen
            game.Controller.SelectEntities(unitList);

            // Check if select the correct unit
            Assert.AreEqual(unitList[0], game.Model.GetSelectionState().SelectedEntities[0], "Select correct unit type");
            Assert.AreEqual(1, game.Model.GetSelectionState().SelectedEntities.Count, "the number of selected units");
            Assert.AreEqual(true, player.GetUnitList().GetChildren().Contains(unitList[0]), "Check the ownership of the selected unit");
        }


        /// <summary>
        /// Check if the building is placed on the map properly
        /// </summary>
        [Test]
        public void TestPlacingBuildingOnMap()
        {
            TestSelectCorrectUnit();

            Building building2 = new Building();
            building2.PointLocation = new PointF(20, 20);
            building2.Type = "barracks";

            // (4) Simulate telling the player's unit to build the building at 20,20 (mouse click at tile 20,20 on the map)
            game.Controller.TellSelectedUnitsToBuildAt(building2.Type, new Microsoft.Xna.Framework.Point(20, 20));

            // Check if the appropriate action is given to the selected unit
            //Assert.AreEqual("<ZRTSModel.BuildAction>", ((UnitComponent)(game.Model.GetSelectionState().SelectedEntities[0])).GetActionQueue().GetChildren()[0]);

            // Check if unit is at where it can build the building
            Assert.AreEqual(true, building2.PointLocation.X - 1 < ((UnitComponent)unitList[0]).PointLocation.X && ((UnitComponent)unitList[0]).PointLocation.X < building2.PointLocation.X + building2.Width + 2, "Unit is not in a range of create a building");

            // (5) Simulate unit is building the building on the map
            ((BuildAction)((UnitComponent)(game.Model.GetSelectionState().SelectedEntities[0])).GetActionQueue().GetChildren()[0]).Work();
            //((BuildAction)((UnitComponent)(game.Model.GetSelectionState().SelectedEntities[0])).GetActionQueue().GetChildren()[0]).Work();


            // Check if building gets added on the map
            Assert.AreEqual(1, player.BuildingList.GetChildren().Count, "Building should be added!");
            Assert.AreEqual(building2.Type,((Building) player.BuildingList.GetChildren()[0]).Type, "Incorrect type of building");
           

            // Check all cells with the area of building after being placed to see if it is actually placed on the map
            for (int i = 20; i < 20 + building2.Width; ++i)
            {
                for (int j = 20; j < 20 + building2.Height; ++j)
                {
                    Assert.AreEqual(building2.Type, ((Building) game.Model.GetScenario().GetGameWorld().GetMap().GetCellAt(i, j).EntitiesContainedWithin[0]).Type, "Should be the same object at " + i + "," + j);
                }
            }


            // Event-based pattern by Marc is totally overkilling XNA framework and basic game programming. (No gameloop)
            // I don't think Marc has done it correctly and properly for the game programming. 
            // (If it was windows app, I would say "yes" but for game, NO!!!)
            // There's a lot of coupling between components, which make it hard to test.

            // Why?
            // Some members of the class are not intialized due to the fact that it is based on trigger event and you
            // must add and initialize exactly all the (unnecessary) components associated with the game 
            // and check if they are all correctly added or initialized. Otherwise, shit will happen and you can't even independently test your components.

            // For instance, if you need to check if your building is added to the map, you probably only need to care
            // player's building list, map, and cell, right?
            // Wrong!!!!
            // You need go into the buildAction.cs (work())
            // you must make sure that "parent.parent.parent.parent" of the player are actually intialized properly (WTF is that?).
            // Each parent has different object type which has nothing to do with the logic that you're trying to test.
            // Otherwise, you will encounter "Object null or no reference" when running test.

            // Way to go, smartass Marc!



        }

        [Test]
        public void TestCannotPlaceBuildingOnOccupiedSpace()
        {
            //TODO:
        }


        /// <summary>
        /// Check if building can be placed outside the screen
        /// </summary>
        [Test]
        public void TestCannotPlaceBuildingOutsideMap()
        {
            TestSelectCorrectUnit();
            Building building = new Building();
            building.Type = "hospital";
            building.PointLocation = new PointF(51, 51);

            ((UnitComponent)unitList[0]).PointLocation = new PointF(50, 50);    // set a unit by the edge of the map

            bool catchException = false;
            try 
            {
                // Mouse click outside the game screen
                game.Controller.TellSelectedUnitsToBuildAt(building.Type, new Microsoft.Xna.Framework.Point(51, 51));
            } catch (Exception e)
            {
                catchException = true;
                Assert.IsTrue(catchException);
            } 
        }


        [Test]
        public void TestCannotOverlapPlacingBuilding()
        {
            //TODO:
        }


        [Test]
        public void TestRemoveBuildingFromMap()
        {
            //TODO:
        }

    }
}
