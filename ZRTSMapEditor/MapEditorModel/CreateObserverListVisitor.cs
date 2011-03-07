using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    /// <summary>
    /// A ModelComponent visitor that generates an empty observer list for each loaded model component.  This is used when a scenario is loaded.
    /// 
    /// It is a hack because we will need to serialize the observer list in the future in order to allow for savable triggers.
    /// </summary>
    class CreateObserverListVisitor : ModelComponentVisitor
    {

        public void Visit(ModelComponent component)
        {
            component.UnregisterAll();
            foreach (ModelComponent c in component.GetChildren())
            {
                c.Accept(this);
            }
        }
    }
}
