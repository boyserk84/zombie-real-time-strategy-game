﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZRTSModel.Scenario;
using ZRTSModel.GameWorld;
using System.Diagnostics;
using ZRTSModel;
using ZRTSMapEditor.MapEditorModel;
using ZRTSMapEditor.UI;

namespace ZRTSMapEditor
{
    /// <summary>
    /// Displays the map.
    /// </summary>
    public partial class MapView : UserControl, ModelComponentObserver, RefreshableUI
    {

        private MapEditorController controller = null;
        private Gameworld gameworld = null;
        private ScenarioComponent context = null;

        public MapView()
        {
            InitializeComponent();
        }

        public void Init(MapEditorController controller, MapEditorFullModel model)
        {
            this.controller = controller;
            SetScenario(model.GetScenario());
            model.RegisterObserver(this);
            model.ScenarioChangedEvent += new ZRTSModel.EventHandlers.ScenarioChangedHandler(ChangeScenario);
        }

        /// <summary>
        /// Called when the scenario has changed.  Unregisters the map view with all pieces of model it was associated with, and registers with
        /// the new scenario.
        /// </summary>
        /// <param name="scenario"></param>
        public void SetScenario(ScenarioComponent scenario)
        {
            if (gameworld != null)
            {
                // Unregister with the old piece of model.
                gameworld.UnregisterObserver(this);
            }
            context = scenario;

            if (scenario != null)
            {
                gameworld = scenario.GetGameWorld();
                if (gameworld != null)
                {
                    // Register with the new piece of model.
                    gameworld.RegisterObserver(this);

                    // Invalidate the view.
                    Refresh();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Determine the cell that we clicked.
            Point p = PointToClient(MousePosition);

            int x = Convert.ToInt32(Math.Floor(p.X / 16.0));
            int y = Convert.ToInt32(Math.Floor(p.Y / 16.0));

            // Notify the controller of the click.
            controller.OnClickMapCell(x, y);

            Debug.WriteLine("("+p.X+", "+p.Y+")");

        }

        private void ChangeScenario(object sender, EventArgs e)
        {
            if (sender is MapEditorFullModel) // sanity check
            {
                SetScenario(((MapEditorFullModel)sender).GetScenario());
            }
        }

        public void notify(ModelComponent observable)
        {
            ModelComponentVisitorDelegator delegator = new ModelComponentVisitorDelegator();

            // Handle Gameworld by invalidating the view.
            RenderMapViewGameworldVisitor renderer = new RenderMapViewGameworldVisitor(this);
            delegator.AddVisitor(renderer);
            observable.Accept(delegator);
        }

        public void Refresh()
        {
            TileFactory tf = TileFactory.Instance;

            // Generate a drawing surface.
            Bitmap pg = new Bitmap(800, 600);
            Graphics gr = Graphics.FromImage(pg);

            ZRTSModel.Map map = gameworld.GetMap();
            // TODO: Change to include the scrolling model.
            for (int x = 0; x < map.GetWidth(); x++)
            {
                for (int y = 0; y < map.GetHeight(); y++)
                {
                    ZRTSModel.Tile tile = map.GetCellAt(x, y).GetTile();
                    if (tile != null)
                    {
                        gr.DrawImage(tf.getBitmapImproved(tile), x * 16, y * 16, 16, 16);
                    }
                    else
                    {
                        gr.DrawRectangle(new Pen(Color.Black), x * 16, y * 16, 16, 16);
                    }
                }
            }
            pictureBox1.Image = pg;
        }
    }
}
