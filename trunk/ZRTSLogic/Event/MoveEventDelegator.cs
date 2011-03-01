using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSLogic.Event
{
    class MoveEventDelegator : EventDelegator
    {
        override void Visit(MoveEvent moveEvent)
        {
            NotifyAll(moveEvent);
        }
    }
}
