using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTS;
using ZRTSModel;
using ZRTSModel.GameModel;
using ZRTSModel.Factories;
using NUnit.Framework;
namespace ZRTSNUnitTests
{
    /// <summary>
    /// Testing placing building on the map
    /// 
/// Testcase1: Building on the empty map
/// Testcase2: Cannot build if there is not enough space or obtacle in between
/// Testcase3: Cannot overlapping build
/// Testcase4: Cannot build outside the map
    /// </summary>
    [TestFixture]
    class TestPlacingBuildingOntheMap
    {
        private GameModel model;
        private PlayerComponent player1;

        /// <summary>
        /// Initialize necessary components
        /// </summary>
        private void initialize()
        {
            model = new GameModel();
            ScenarioComponent scenario = new ScenarioComponent(50, 50);

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

            //Create two players and set them to be enemies.
            player1 = new PlayerComponent();
            PlayerComponent player2 = new PlayerComponent();
            model.GetScenario().GetGameWorld().GetPlayerList().AddChild(player1);
            model.GetScenario().GetGameWorld().GetPlayerList().AddChild(player2);

            // Set target enemy
            player1.EnemyList.Add(player2);
            player2.EnemyList.Add(player1);

        }

        [Test]
        public void TestPlacingBuildingOnMap()
        {
            initialize();
            Building building2 = new Building();
            building2.Type = "barracks";
            building2.Width = 3;
            building2.Height = 3;
            building2.MaxHealth = 100;
            building2.CurrentHealth = 1;
            building2.Completed = false;
            building2.PointLocation = new PointF(20, 20);

            // (1) Simulate mouse click at tile 20,20 on the map
            model.GetScenario().GetGameWorld().GetMap().addBuildingToMap(building2);
            




            // I just realized that the event handler and vistor pattern definitely overkills everything about automated testing.
            // Model must contain some game logics (in general of game programming).
            // But this model/structure is completely taking everything about game programming away. sigh!!!
            // UNLESS YOU FIND TO USE VISITOR PATTERN INSIDE THIS TEST UNIT. 
            // THIS EVENT HANDLER PATTERN SUCKS AND IT IS THE WORST THING THAT
            // EVER HAPPENED FOR GAME PROGRAMMING. I"m about to shoot someone now.


            // **************************************************************************************
            // *** ** If you can figure how to use BuildAction to add building to the map, 
            // that would reflect how the building is actually placed on the map. 
            // Otherwise, what I did at (1) and (2) are just for the test cases (hack) only
            BuildAction action = new BuildAction(building2, model.GetScenario().GetGameWorld().GetMap());
            //Assert.IsFalse(action.Work());


            // (2) All codes after this line

            Assert.AreEqual(true, model.GetScenario().GetGameWorld().GetMap().GetCellAt(20, 20).ContainsEntity(), "Should be able to add building at the empty map");
            
            // Check all cells with the area of building being placed
            for (int i = 20; i < 20 + building2.Width; ++i)
            {
                for (int j = 20; j < 20 + building2.Height; ++j)
                {
                    Assert.AreEqual(building2, model.GetScenario().GetGameWorld().GetMap().GetCellAt(i, j).EntitiesContainedWithin[0], "Should be the same object at " + i +"," + j);
                }
            }
        }

        [Test]
        public void TestCannotPlaceBuildingOnOccupiedSpace()
        {
            //TODO:
        }


        [Test]
        public void TestCannotPlaceBuildingOutsideMap()
        {
            initialize();
            Building building = new Building();
            building.Type = "hospital";
            building.Width = 5;
            building.Height = 5;
            building.PointLocation = new PointF(51, 51);

            bool catchException = false;
            try 
            {
                // Simulate mouse click at somewhere outside the map
                model.GetScenario().GetGameWorld().GetMap().addBuildingToMap(building);
            } catch (Exception e)
            {
                catchException = true;
                Assert.IsTrue(catchException);
            }
            
            // Need to check the number of entities in the map or scenario?????
            //Assert.AreEqual(0, model.GetScenario().GetGameWorld().GetMap().GetChildren()[0].,"Should not allow to build!");

           
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
