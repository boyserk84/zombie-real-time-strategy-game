using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    class Mountain : Tile
    {
        override public void Accept(ModelComponentVisitor visitor)
        {
            if (visitor is MountainVisitor)
            {
                MountainVisitor mountainVisitor = (MountainVisitor)visitor;
                mountainVisitor.Visit(this);
            }
            else
            {
                base.Accept(visitor);
            }
        }

        override public bool Passable()
        {
            return false;
        }
    }
}
