using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Event
{
    interface EventHandler
    {
        void Visit(MoveEvent moveEvent);
    }
}
