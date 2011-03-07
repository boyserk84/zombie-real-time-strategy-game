using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;

namespace ZRTSModel
{
    /// <summary>
    /// A list of units.
    /// </summary>
    [Serializable()]
    public class UnitList : ModelComponent
    {
        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
