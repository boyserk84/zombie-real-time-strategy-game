using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    public abstract class Tile : ModelComponentLeaf
    {
        virtual public bool Passable()
        {
            return true;
        }
    }
}
