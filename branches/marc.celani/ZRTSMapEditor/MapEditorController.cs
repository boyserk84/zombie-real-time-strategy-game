using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using ZRTSModel.Scenario;
using ZRTSModel.GameWorld;
using ZRTSMapEditor.MapEditorModel;
using ZRTSModel;
using ZRTSMapEditor.UI;
using ZRTSMapEditor.Commands.MapEditorViewCommands;

namespace ZRTSMapEditor
{
    public class MapEditorController
    {
        private MapEditorFullModel model;

        public MapEditorController(MapEditorFullModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Serializes and saves the current scenario.
        /// </summary>
        public void saveScenario()
        {
            if (model.GetScenario() != null)
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

                        ScenarioComponent scenario = model.GetScenario();
                        scenario.SetContainer(null);
                        bin.Serialize(saveStream, scenario);
                        saveStream.Close();
                        model.AddChild(scenario);
                        // TODO: Change so that the SaveInfo model is updated.
                    }
                }
            }
        }

        /// <summary>
        /// Loads a scenario from disc by deserializing.  Generates empty observer lists for each model component.
        /// </summary>
        public void loadScenario()
        {
            if (model.GetScenario() != null)
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
                ScenarioComponent scenario = (ScenarioComponent)bin.Deserialize(openMapDialog.OpenFile());

                // The observer lists are all null - we must set them to be empty lists.
                CreateObserverListVisitor visitor = new CreateObserverListVisitor();
                scenario.Accept(visitor);
                scenario.GetGameWorld().GetMap().SetCellsToBeContainedInMap();
                model.AddChild(scenario);

                // Invalidate the Scenario view.
                scenario.GetGameWorld().NotifyAll();

                // TODO: Update the SaveInfo state.
            }
        }

        /// <summary>
        /// Creates a CreateNewScenario dialog and uses it to determine the name and size of the new scenario.  Then, generates a 
        /// new scenario of the appropriate size.
        /// </summary>
        public void createNewScenario()
        {
            if (model.GetScenario() != null)
            {
                // TODO: Ask if the user wants to discard the current scenario or save it.
            }
            CreateNewScenarioDialog dialog = new CreateNewScenarioDialog();
            dialog.ShowDialog();

            if (dialog.ExitWithCreate)
            {
                ScenarioComponent scenario = new ScenarioComponent(dialog.ScenarioWidth, dialog.ScenarioHeight);
                // TODO: Update SaveInfo model to change filename and UpToDate flag.

                // Automatically discards old scenario, by overloaded AddChild function.
                model.AddChild(scenario);
            }
            
        }

        public bool isOkayToClose()
        {
            // TODO Open dialog box to see if it is okay to discard changes, or to save first
            return true;
        }

        /// <summary>
        /// Determines from the selection state if we are placing a resource, building, unit, or tile, and then places it based on
        /// the selection state.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal void OnClickMapCell(int x, int y)
        {
            if (model.GetSelectionState().SelectionType == typeof(ZRTSModel.Tile))
            {
                TileFactory tf = TileFactory.Instance;
                CellComponent cell = model.GetScenario().GetGameWorld().GetMap().GetCellAt(x, y);
                ZRTSModel.Tile tile = tf.GetImprovedTile(model.GetSelectionState().SelectedTileType);
                ChangeCellTileCommand command = new ChangeCellTileCommand(cell, tile);

                if (command.CanBeDone())
                {
                    model.GetCommandStack().ExecuteCommand(command);
                }
            }
            else if (model.GetSelectionState().SelectionType == typeof(UnitComponent))
            {
                // Get instance of Unit Factory, produce unit, and place on map for the given player.
            }
        }

        /// <summary>
        /// Called by the tile palette when a tile is selected - updates the selection model.
        /// </summary>
        /// <param name="type"></param>
        internal void selectTileType(string type)
        {
            model.GetSelectionState().SelectionType = typeof(ZRTSModel.Tile);
            model.GetSelectionState().SelectedTileType = type;
        }

        /// <summary>
        /// Opens a dialog box to change the players list.
        /// </summary>
        internal void OpenPlayersForm()
        {
            if (model.GetScenario() != null)
            {
                PlayersForm form = new PlayersForm(model.GetScenario().GetGameWorld().GetPlayerList());
                form.ShowDialog();
            }
        }

        /// <summary>
        /// Delegates undo command to command stack.
        /// </summary>
        internal void UndoLastCommand()
        {
            model.GetCommandStack().UndoLastCommand();
        }

        /// <summary>
        /// Delegates redo request to command stack.
        /// </summary>
        internal void RedoLastUndoCommand()
        {
            model.GetCommandStack().RedoLastUndoneCommand();
        }

        /// <summary>
        /// Called by unit and building palettes to determine for which player the building or unit should be added.
        /// </summary>
        /// <param name="playerName"></param>
        internal void SelectPlayer(string playerName)
        {
            model.GetSelectionState().SelectedPlayer = playerName;
        }

        /// <summary>
        /// Called by unit palette to set in the selection state what type of unit has been selected.
        /// </summary>
        /// <param name="unitType"></param>
        internal void SelectUnitType(string unitType)
        {
            model.GetSelectionState().SelectedUnitType = unitType;
            model.GetSelectionState().SelectionType = typeof(UnitComponent);
        }

        internal void OnClickMapCell(CellComponent cellComponent)
        {
            if (model.GetSelectionState().SelectionType == typeof(ZRTSModel.Tile))
            {
                TileFactory tf = TileFactory.Instance;
                ZRTSModel.Tile tile = tf.GetImprovedTile(model.GetSelectionState().SelectedTileType);
                ChangeCellTileCommand command = new ChangeCellTileCommand(cellComponent, tile);

                if (command.CanBeDone())
                {
                    model.GetCommandStack().ExecuteCommand(command);
                }
            }
            else if (model.GetSelectionState().SelectionType == typeof(UnitComponent))
            {
                // Get instance of Unit Factory, produce unit, and place on map for the given player.
            }
        }
    }
}
