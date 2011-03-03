using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using System.Data;

namespace ZRTSMapEditor
{
    public class PlayerDataGridAdapter
    {
        private string name;
        private string race;
        private int gold;
        private int wood;
        private int metal;
        private PlayerComponent data;

        private PlayerDataGridAdapter()
        { }

        public PlayerDataGridAdapter(PlayerComponent player)
        {
            data = player;

            // TODO: Grab details from player
        }

        public string Player_Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string RaceMember
        {
            get
            {
                return race;
            }
            set
            {
                race = value;
            }
        }

        public int GoldMember
        {
            get
            {
                return gold;
            }
            set
            {
                gold = value;
            }
        }

        public int WoodMember
        {
            get
            {
                return wood;
            }
            set
            {
                wood = value;
            }
        }

        public int MetalMember
        {
            get
            {
                return metal;
            }
            set
            {
                metal = value;
            }
        }

        public void SaveBackToPlayer()
        {
            // TODO: Save details back to data
        }
    }
}
