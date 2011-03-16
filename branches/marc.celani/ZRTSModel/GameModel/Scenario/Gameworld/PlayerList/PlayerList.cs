using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.EventHandlers;

namespace ZRTSModel
{
    /// <summary>
    /// A list of players - can only contain players.
    /// </summary>
    [Serializable()]
    public class PlayerList : ModelComponent
    {
        public event PlayerListChangedHandler PlayerListChangedEvent;

        public override void AddChild(ModelComponent child)
        {
            if (child is PlayerComponent)
            {
                base.AddChild(child);
                PlayerListChangedEventArgs e = new PlayerListChangedEventArgs();
                e.PlayersAdded.Add((PlayerComponent)child);
                PlayerListChangedEvent(this, e);
            }
        }

        public override void RemoveChild(ModelComponent child)
        {
            base.RemoveChild(child);
            PlayerListChangedEvent(this, null);
        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void FireChangedEvent()
        {
            PlayerListChangedEvent(this, null);
        }

        public PlayerComponent GetPlayerByName(string p)
        {
            PlayerComponent player = null;
            foreach (PlayerComponent pc in GetChildren())
            {
                if (pc.GetName().Equals(p))
                {
                    player = pc;
                    break;
                }
            }
            return player;
        }
    }
}
