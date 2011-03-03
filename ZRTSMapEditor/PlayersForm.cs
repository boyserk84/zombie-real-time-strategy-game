using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZRTSModel;
using System.Collections;

namespace ZRTSMapEditor
{
    public partial class PlayersForm : Form
    {
        private PlayerList playerList;

        private PlayersForm()
        {
            InitializeComponent();
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
            PlayerDataGridAdapter adapter = new PlayerDataGridAdapter(new PlayerComponent());
            adapter.Player_Name = "Player 1";
            adapter.RaceMember = "Human";
            adapter.WoodMember = 200;
            adapter.MetalMember = 200;
            adapter.GoldMember = 200;
            
            source.Add(adapter);
            source.Add(adapter);
            
            
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

        private void addPlayerButton_Click(object sender, EventArgs e)
        {
            // 

        }

    }
}
