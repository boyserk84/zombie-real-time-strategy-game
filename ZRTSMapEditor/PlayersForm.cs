using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZRTSModel;

namespace ZRTSMapEditor
{
    public partial class PlayersForm : Form
    {
        private PlayerList playerList;

        private PlayersForm()
        {
        }

        public PlayersForm(PlayerList list)
        {
            InitializeComponent();
            playerList = list;
            BindingSource source = new BindingSource();

            /*foreach (PlayerComponent player in list.GetChildren())
            {
                PlayerDataGridAdapter adapter = new PlayerDataGridAdapter(player);
                source.Add(adapter);
            }*/

            // test Data
            // TODO: Remove and replace.
            PlayerDataGridAdapter testAdapter = new PlayerDataGridAdapter(new PlayerComponent());
            testAdapter.Player_Name = "Player 1";
            testAdapter.RaceMember = "Human";
            testAdapter.WoodMember = 200;
            testAdapter.MetalMember = 200;
            testAdapter.GoldMember = 200;

            source.Add(testAdapter);
            source.Add(testAdapter);


            // Initialize Player List to bind data properly.
            uiPlayerList.AutoGenerateColumns = false;
            uiPlayerList.AutoSize = true;
            uiPlayerList.DataSource = source;

            name.DataPropertyName = "Player_Name";
            race.DataPropertyName = "RaceMember";
            gold.DataPropertyName = "GoldMember";
            wood.DataPropertyName = "WoodMember";
            metal.DataPropertyName = "MetalMember";
        }
    }
}
