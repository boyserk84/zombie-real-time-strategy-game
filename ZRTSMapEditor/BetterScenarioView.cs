using System;
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

namespace ZRTSMapEditor
{
    public partial class BetterScenarioView : UserControl, MapEditorModelListener 
    {

        MapEditorController controller;

        public BetterScenarioView()
        {
            InitializeComponent();
        }

        public void setController(MapEditorController controller)
        {
            this.controller = controller;
        }

        public void update(MapEditorModel model) 
        {
            TileFactory tf = TileFactory.Instance;

            Bitmap pg = new Bitmap(800,600);
            Graphics gr = Graphics.FromImage(pg);

            Map map = model.scenario.getGameWorld().map;

            // TODO: Change to include the scrolling model.
            for (int x = 0; x < map.width; x++) {
                for (int y = 0; y < map.height; y++) {
                    if (map.cells[x, y] != null && map.cells[x,y].tile != null && map.cells[x,y].tile.tileType != null)
                        gr.DrawImage(tf.getBitmap(map.cells[x,y].tile.tileType), x * 16, y * 16,16,16);
                    else
                        gr.DrawRectangle(new Pen(Color.Black), x * 16, y * 16, 16, 16);
                }
            }
            pictureBox1.Image = pg;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            Point p = PointToClient(MousePosition);

            int x = Convert.ToInt32(Math.Floor(p.X / 16.0));
            int y = Convert.ToInt32(Math.Floor(p.Y / 16.0));

           

            controller.updateCellType(x, y);

            Debug.WriteLine("("+p.X+", "+p.Y+")");

        }


        public void notify(MapEditorModel model)
        {
            update(model);
        }

    }
}
