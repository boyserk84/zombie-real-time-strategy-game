using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    interface UnitListVisitor : ModelComponentVisitor
    {
        void Visit(UnitList list);
    }
}
