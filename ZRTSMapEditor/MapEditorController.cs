using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using ZRTSModel.Scenario;
using ZRTSModel.GameWorld;

namespace ZRTSMapEditor
{
    public class MapEditorController
    {
        private MapEditorModel model;

        public MapEditorController(MapEditorModel model)
        {
            this.model = model;
        }

        public void saveScenario()
        {
            if (model.scenario != null)
            {
                Stream saveStream;
                SaveFileDialog saveMapDialog = new SaveFileDialog();
                saveMapDialog.InitialDirectory = Application.StartupPath + "\\Maps\\";
                saveMapDialog.Filter = "Map files (*.map)| *.map";

                if (saveMapDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((saveStream = saveMapDialog.OpenFile()) != null)
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(saveStream, model.scenario);
                        saveStream.Close();
                        model.saved = true;
                        // The model has changed, so notify all views of the change.
                        model.notifyAll();
                    }
                }
            }
        }

        public void loadScenario()
        {
            if (model.scenario != null)
            {
                // TODO: Ask if the user wants to discard the current scenario.
            }
            
            // Displays an OpenFileDialog so the user can select a Map.
            OpenFileDialog openMapDialog = new OpenFileDialog();
            openMapDialog.InitialDirectory = Application.StartupPath + "\\Maps\\";
            openMapDialog.Filter = "Map Files|*.map";
            openMapDialog.Title = "Select a Map File";

            if (openMapDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Deserialize the file and load it into the model.
                BinaryFormatter bin = new BinaryFormatter();
                model.scenario = (Scenario)bin.Deserialize(openMapDialog.OpenFile());
                
                // We have not edited any of the model since loading, so it is up to date.
                model.saved = true;
                // TODO model.commands = new Queue<MapEditorCommand>();
                // TODO newEntityOrnewTileOrEntitiesAlreadyInMap = none;

                //The model has changed, so notify all views of the change.
                model.notifyAll();
            }
        }

        public void createNewScenario()
        {
            // TODO Extract this logic with loadscenario.
            if (model.scenario != null)
            {
                // TODO: Ask if the user wants to discard the current scenario.
            }
            model.scenario = new ZRTSModel.Scenario.Scenario(20, 20);

            // Since this is a new scenario, it has not been saved and does not have a filename
            model.filename = null;
            model.saved = false;

            //The model has changed, so notify all views of the change.
            model.notifyAll();
        }


        /* TODO public void selectEntities(List<Entity> entities)
        {

        }*/

        /* TODO public void selectEntityTypeToAdd(Entity entity)
        {

        }*/
        // TODO public void setTile(Tile tile, int x, int y)
        // TODO public void addEntity(Entity entity, int x, int y)
        // TODO public void moveEntities(List<Entity> entities, int x, int y)
        // TODO public void addTrigger(Trigger trigger)
        // TODO public void removeTrigger(Trigger trigger)
        public bool isOkayToClose()
        {
            // TODO Open dialog box to see if it is okay to discard changes, or to save first.
            return model.saved;
        }


        internal void updateCellType(int x, int y)
        {
            TileFactory tf = TileFactory.Instance;
            Cell selectedCell = model.scenario.getGameWorld().map.getCell(x, y);
            selectedCell.tile = tf.getTile(model.TileTypeSelected);
            model.notifyAll();
        }

        internal void selectTileType(string type)
        {
            model.TileTypeSelected = type;
            model.SelectionType = SelectionType.Tile;

            //The model has changed, so notify all views of the change.
            model.notifyAll();
        }
    }
}
