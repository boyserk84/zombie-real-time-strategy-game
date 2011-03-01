using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;

namespace ZRTSModel
{
    class EntityList : ModelComponent
    {
        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.visit(this);
        }

        internal static IEnumerable<Entity> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
