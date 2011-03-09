using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZRTSModel;
using System.Runtime.CompilerServices;
using ZRTSModel.EventHandlers;

namespace ZRTSMapEditor
{
    public partial class PlayersForm : Form
    {
        private PlayerList playerList;
        private BindingSource source;

        private PlayersForm()
        {
        }

        public PlayersForm(PlayerList list)
        {
            InitializeComponent();
            playerList = list;
            source = new BindingSource();

            foreach (PlayerComponent player in list.GetChildren())
            {
                PlayerDataGridAdapter adapter = new PlayerDataGridAdapter(player, playerList);
                source.Add(adapter);
            }


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

        internal void addPlayerButton_Click(object sender, EventArgs e)
        {
            PlayerDataGridAdapter adapter = new PlayerDataGridAdapter(new PlayerComponent(), playerList);
            adapter.RaceMember = "Human";
            adapter.AddedMember = true;
            source.Add(adapter);
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            List<PlayerDataGridAdapter> adapters = new List<PlayerDataGridAdapter>();
            foreach (Object o in source.List)
            {
                adapters.Add((PlayerDataGridAdapter)o);
            }
            PlayerDataGridAdapterCommitter committer = new PlayerDataGridAdapterCommitter(adapters);
            if (committer.CanBeDone())
            {
                committer.Do();
                Close();
            }
            else
            {
                // TODO: Add error handler.
            }
            
        }

        internal void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void removePlayerButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in uiPlayerList.SelectedRows)
            {
                PlayerDataGridAdapter adapter = (PlayerDataGridAdapter)row.DataBoundItem;
                adapter.RemovedMember = true;
                row.Selected = false;
                row.DefaultCellStyle.BackColor = Color.Red;
            }
        }

    }
}
