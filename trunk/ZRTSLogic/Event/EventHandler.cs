using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSLogic.Event
{
    interface EventHandler
    {
        void Visit(MoveEvent moveEvent);
    }
}
