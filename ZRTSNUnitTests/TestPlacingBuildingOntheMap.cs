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
            PlayerComponent player1 = new PlayerComponent();
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
            building2.PointLocation = new PointF(20, 20);

            // Problem: AddChild of GetMap() not acutally adding building but cell?
            model.GetScenario().GetGameWorld().GetContainer().AddChild(building2);
            Assert.AreEqual(1, model.GetScenario().GetChildren().Count, "Not passing");
            
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
            
            model.GetScenario().GetGameWorld().AddChild(building);
            Assert.AreEqual(0, model.GetScenario().GetChildren().Count,"Should not allow to build!");

           
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
