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
using ZRTSModel;
using ZRTSMapEditor.MapEditorModel;

namespace ZRTSMapEditor
{
    public partial class BetterScenarioView : UserControl, ModelComponentObserver, ModelComponentVisitor, MapEditorFullModelVisitor, GameworldVisitor
    {

        private MapEditorController controller = null;
        private Gameworld gameworld = null;
        private ScenarioComponent context = null;

        public BetterScenarioView()
        {
            InitializeComponent();
        }

        public void setController(MapEditorController controller)
        {
            this.controller = controller;
        }

        public void SetScenario(ScenarioComponent scenario)
        {
            if (gameworld != null)
            {
                gameworld.UnregisterObserver(this);
            }
            context = scenario;

            if (scenario != null)
            {
                gameworld = scenario.GetGameWorld();
                if (gameworld != null)
                {
                    gameworld.RegisterObserver(this);

                    // Invalidate the view.
                    gameworld.Accept(this);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            Point p = PointToClient(MousePosition);

            int x = Convert.ToInt32(Math.Floor(p.X / 16.0));
            int y = Convert.ToInt32(Math.Floor(p.Y / 16.0));

           

            controller.OnClickMapCell(x, y);

            Debug.WriteLine("("+p.X+", "+p.Y+")");

        }


        public void notify(ModelComponent observable)
        {
            observable.Accept(this);
        }

        public void Visit(ModelComponent component)
        {
            // Do nothing.
        }

        public void render()
        {
            TileFactory tf = TileFactory.Instance;

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
                    //if (map.cells[x, y] != null && map.cells[x, y].tile != null && map.cells[x, y].tile.tileType != null)
                    //gr.DrawImage(tf.getBitmap(map.cells[x, y].tile.tileType), x * 16, y * 16, 16, 16);
                    else
                    {
                        gr.DrawRectangle(new Pen(Color.Black), x * 16, y * 16, 16, 16);
                    }
                }
            }
            pictureBox1.Image = pg;
        }

        public void Visit(Gameworld gameworld)
        {
            render();
        }


        public void Visit(MapEditorFullModel model)
        {
            if (model.GetScenario() != context)
            {
                SetScenario(model.GetScenario());
            }
        }
    }
}
