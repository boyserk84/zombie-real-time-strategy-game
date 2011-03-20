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
using System.Collections;
using System.Threading;

namespace ZRTSMapEditor
{
    public partial class MapViewComposite : UserControl
    {
        private MapEditorController controller = null;
        private ScenarioComponent context = null;
        private static int PIXELS_PER_COORDINATE = 20;
        Hashtable realizedComponents = new Hashtable();
        // private Thread workerThread = null;

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
            for (int i = 0; i < this.mapPanel.Controls.Count; )
            {
                if (this.mapPanel.Controls[0] != null)
                    this.mapPanel.Controls[0].Dispose();
            }
            realizedComponents.Clear();
            this.mapPanel.Show();

            if (scenario != null)
            {
                ZRTSModel.Map map = scenario.GetGameWorld().GetMap();

                this.mapPanel.Size = new System.Drawing.Size(map.GetWidth() * PIXELS_PER_COORDINATE, map.GetHeight() * PIXELS_PER_COORDINATE);
                
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
                this.RealizeView();
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

        protected override void OnScroll(ScrollEventArgs se)
        {
            this.RealizeView();
            base.OnScroll(se);
        }

        private void RealizeView()
        {
            /*if (workerThread != null)
            {
                workerThread.Abort();
            }
            workerThread = new Thread(new ThreadStart(DoRealize));
            workerThread.IsBackground = true;
            workerThread.Start();*/
            DoRealize();
        }

        private void DoRealize()
        {
            // Get the indices of the upper left hand cell that is visible.
            int xPos = HorizontalScroll.Value / PIXELS_PER_COORDINATE;
            int yPos = this.VerticalScroll.Value / PIXELS_PER_COORDINATE;

            HashSet<Object> componentsToVirtualize = new HashSet<Object>();
            foreach (Object o in realizedComponents.Keys)
            {
                componentsToVirtualize.Add(o);
            }
            mapPanel.SuspendLayout();
            if (context != null)
            {
                ZRTSModel.Map map = context.GetGameWorld().GetMap();
                int maxXPos = xPos + (Width / PIXELS_PER_COORDINATE) + 1;
                int mapMaxX = map.GetWidth();
                int maxX = Math.Min(maxXPos, mapMaxX);
                int maxYPos = yPos + (Height / PIXELS_PER_COORDINATE) + 1;
                int mapMaxY = map.GetHeight();
                int maxY = Math.Min(maxYPos, mapMaxY);
                List<Control> controlsToAdd = new List<Control>();
                for (int i = xPos; i < maxX; i++)
                {
                    for (int j = yPos; j < maxY; j++)
                    {
                        CellComponent cell = map.GetCellAt(i, j);
                        if (!realizedComponents.Contains(cell))
                        {
                            TileUI ui = new TileUI(controller, cell);
                            realizedComponents.Add(cell, ui);

                            ui.Location = new Point(cell.X * PIXELS_PER_COORDINATE, cell.Y * PIXELS_PER_COORDINATE);
                            ui.Size = new Size(PIXELS_PER_COORDINATE, PIXELS_PER_COORDINATE);
                            controlsToAdd.Add(ui);
                        }
                        else
                        {
                            componentsToVirtualize.Remove(cell);
                        }
                    }
                }
                mapPanel.Controls.AddRange(controlsToAdd.ToArray());
                foreach (Object o in componentsToVirtualize)
                {
                    Control c = (Control)realizedComponents[o];
                    c.Dispose();
                    realizedComponents.Remove(o);
                    this.mapPanel.Controls.Remove(c);
                }
                this.mapPanel.ResumeLayout();
            }
            // this.workerThread = null;
        }
    }
}
