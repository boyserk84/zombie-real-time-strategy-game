using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSLogic.Event
{
    interface Event
    {
        // Visitor pattern
        public void Accept(EventHandler handler);
    }
}
