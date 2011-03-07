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

namespace ZRTSMapEditor
{
    /// <summary>
    /// A palette for selecting a unit and a player to add it to so that it may be added on the map.
    /// Currently in a demo state.
    /// </summary>
    public partial class UnitPalette : UserControl, ModelComponentObserver, ModelComponentVisitor, MapEditorFullModelVisitor, PlayerListVisitor
    {
        private MapEditorController controller;
        private ScenarioComponent context;
        private PlayerList playerList;

        public UnitPalette()
        {
            InitializeComponent();
            unitName1.Image = unitImageList.Images[0];
            unitName1.Click += uiUnitIcon_Click;
            unitName2.Image = unitImageList.Images[1];
        }

        public void Init(MapEditorController controller, MapEditorFullModel model)
        {
            this.controller = controller;
            
            context = model.GetScenario();
            InitContext();

            model.RegisterObserver(this);
        }

        private void InitContext()
        {
            if (context != null)
            {
                playerList = context.GetGameWorld().GetPlayerList();
                playerList.RegisterObserver(this);
            }
        }

        public void Visit(PlayerList list)
        {
            uiPlayerList.Items.Clear();
            foreach (PlayerComponent player in list.GetChildren())
            {
                uiPlayerList.Items.Add(player.GetName());
            }
        }

        public void Visit(MapEditorFullModel model)
        {
            if (model.GetScenario() != context)
            {
                context = model.GetScenario();
                InitContext();
            }
        }



        public void Visit(ModelComponent component)
        {
            // No op
        }

        public void notify(ModelComponent observable)
        {
            observable.Accept(this);
        }

        private void uiPlayerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.SelectPlayer(uiPlayerList.SelectedItem.ToString());
        }

        private void uiUnitIcon_Click(object sender, EventArgs e)
        {
            controller.SelectUnitType(((PictureBox)sender).Name);
        }
    }
}
