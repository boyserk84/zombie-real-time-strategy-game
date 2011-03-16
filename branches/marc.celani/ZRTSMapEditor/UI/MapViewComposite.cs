using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZRTSModel;
using ZRTSMapEditor.MapEditorModel;
using ZRTSModel.GameWorld;
using ZRTSModel.EventHandlers;

namespace ZRTSMapEditor
{
    public partial class MapViewComposite : UserControl
    {
        private MapEditorController controller = null;
        private ScenarioComponent context = null;

        public MapViewComposite()
        {
            InitializeComponent();
        }

        public void Init(MapEditorController controller, MapEditorFullModel model)
        {
            this.controller = controller;
            SetScenario(model.GetScenario());
            model.ScenarioChangedEvent += new ZRTSModel.EventHandlers.ScenarioChangedHandler(ChangeScenario);
        }

        /// <summary>
        /// Called when the scenario has changed.  Unregisters the map view with all pieces of model it was associated with, and registers with
        /// the new scenario.
        /// </summary>
        /// <param name="scenario"></param>
        public void SetScenario(ScenarioComponent scenario)
        {
            context = scenario;

            // Empty the view.
            this.mapPanel.Hide();
            this.mapPanel.Controls.Clear();
            this.mapPanel.Empty();

            if (scenario != null)
            {

                scenario.GetGameWorld().GetPlayerList().PlayerListChangedEvent += RegisterToNewPlayer;
                ZRTSModel.Map map = scenario.GetGameWorld().GetMap();
                TileUI firstTile = new TileUI(this.controller, map.GetCellAt(0, 0));
                
                this.mapPanel.SetRatioOfGameCoordToPixels(firstTile.Size.Width);
                this.mapPanel.Size = new System.Drawing.Size(map.GetWidth() * firstTile.Image.Size.Width, map.GetHeight() * firstTile.Image.Size.Height);
                
                // Location of the map panel
                int xLoc, yLoc;

                if (this.mapPanel.Size.Width > this.Size.Width)
                {
                    xLoc = 0;
                }
                else
                {
                    // Place the panel in the center.
                    xLoc = (this.Size.Width - this.mapPanel.Size.Width) / 2;
                }

                if (this.mapPanel.Size.Height > this.Size.Height)
                {
                    yLoc = 0;
                }
                else
                {
                    // Place the panel in the center.
                    yLoc = (this.Size.Height - this.mapPanel.Size.Height) / 2;
                }

                this.mapPanel.Location = new System.Drawing.Point(xLoc, yLoc);
                

                this.mapPanel.SuspendLayout();
                // Add tiles to the layout.

                for (int i = 0; i < map.GetWidth(); i++)
                {
                    for (int j = 0; j < map.GetHeight(); j++)
                    {
                        TileUI tile = new TileUI(this.controller, map.GetCellAt(i, j));
                        this.mapPanel.AddControlAtMapCoordinate(tile, i, j);
                    }
                }
                this.mapPanel.Show();
                this.mapPanel.ResumeLayout();
            }
            else
            {
                //this.flowLayout.Size = new System.Drawing.Size(0, 0);
            }
            
        }

        private void ChangeScenario(object sender, EventArgs e)
        {
            if (sender is MapEditorFullModel) // sanity check
            {
                SetScenario(((MapEditorFullModel)sender).GetScenario());
            }
        }

        private void AddUnit(object sender, UnitAddedEventArgs e)
        {
            // TODO: Logic to check that it is in view
            UnitUI unit = new UnitUI(this.controller, e.Unit);
            this.mapPanel.AddControlAtMapCoordinate(unit, e.Unit.PointLocation.X, e.Unit.PointLocation.Y);
        }

        private void RemoveUnit(object sender, UnitRemovedEventArgs e)
        {

        }

        private void RegisterToNewPlayer(object sender, PlayerListChangedEventArgs e)
        {
            if (e != null)
            {
                foreach (PlayerComponent player in e.PlayersAdded)
                {
                    player.GetUnitList().UnitAddedEvent += AddUnit;
                    player.GetUnitList().UnitRemovedEvent += RemoveUnit;
                }
            }
        }
    }
}
