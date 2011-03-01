﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Event
{
    class MoveEvent : Event
    {
        public ZRTSModel.Entities.Unit u;
        public int x;
        public int y;

        public void Accept(EventHandler handler)
        {
            handler.Visit(this);
        }
    }
}
