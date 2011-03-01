using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Event
{
    public abstract class NoOpEventHandler : EventHandler
    {
        public void Visit(MoveEvent moveEvent)
        {
            // No op.
        }
    }
}
