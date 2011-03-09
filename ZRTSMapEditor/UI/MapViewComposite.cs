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
            this.flowLayout.Controls.Clear();

            if (scenario != null)
            {
                ZRTSModel.Map map = scenario.GetGameWorld().GetMap();
                TileUI firstTile = new TileUI(this.controller, map.GetCellAt(0, 0));
                this.flowLayout.Size = new System.Drawing.Size(map.GetWidth() * firstTile.Image.Size.Width, map.GetHeight() * firstTile.Image.Size.Height);
                
                // Location of the flow
                int xLoc, yLoc;

                if (this.flowLayout.Size.Width > this.Size.Width)
                {
                    xLoc = 0;
                }
                else
                {
                    // Place the layout in the center.
                    xLoc = (this.Size.Width - this.flowLayout.Size.Width) / 2;
                }

                if (this.flowLayout.Size.Height > this.Size.Height)
                {
                    yLoc = 0;
                }
                else
                {
                    // Place the layout in the center.
                    yLoc = (this.Size.Height - this.flowLayout.Size.Height) / 2;
                }

                this.flowLayout.Location = new System.Drawing.Point(xLoc, yLoc);

                

                // Add tiles to the layout.
                for (int i = 0; i < map.GetWidth(); i++)
                {
                    for (int j = 0; j < map.GetHeight(); j++)
                    {
                        TileUI tile = new TileUI(this.controller, map.GetCellAt(i, j));
                        this.flowLayout.Controls.Add(tile);
                    }
                }
            }
            else
            {
                this.flowLayout.Size = new System.Drawing.Size(0, 0);
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

        }

        private void ChangeScenario(object sender, EventArgs e)
        {
            if (sender is MapEditorFullModel) // sanity check
            {
                SetScenario(((MapEditorFullModel)sender).GetScenario());
            }
        }

        /*public void Refresh()
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
        }*/

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
        
        }
    }
}
