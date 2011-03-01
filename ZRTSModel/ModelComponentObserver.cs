using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    [Serializable()]
    interface ModelComponentObserver
    {
        public void notify(ModelComponent observable);
    }
}
