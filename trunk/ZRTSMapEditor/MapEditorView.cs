using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZRTSModel.Scenario;
using System.Runtime.Serialization.Formatters.Binary;

// TODO Create ImageManager, Fit ScenarioView and Tile to MVC Pattern
namespace ZRTSMapEditor
{
    public partial class MapEditorView : Form
    {
       
        private MapEditorController controller;

        public MapEditorView()
        {
            InitializeComponent();
            MapEditorModel model = new MapEditorModel();
            this.controller = new MapEditorController(model);
            // TODO Add all other views
            tilePalette.loadImageList();
            tilePalette.setController(controller);
            scenarioView1.setController(controller);
            model.register(tilePalette);
            model.register(scenarioView1);

            // TODO Remove this code, testing purposes only
            this.controller.createNewScenario();

            


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (controller.isOkayToClose())
            {
                Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.saveScenario();
        }
        

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.loadScenario();
        }

        internal void selectedTileChanged(Image image)
        {
           // scenarioView1.changeImage(image);
        }

        internal void tileClicked(Tile tile)
        {
            tile.Image = tilePalette.getSelectedImage();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.createNewScenario();
        }
    }
}
