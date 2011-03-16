using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.EventHandlers
{
    public class PlayerListChangedEventArgs : EventArgs
    {
        private List<PlayerComponent> playersAdded = new List<PlayerComponent>();

        public List<PlayerComponent> PlayersAdded
        {
            get { return playersAdded; }
            set { playersAdded = value; }
        }
    }
}
