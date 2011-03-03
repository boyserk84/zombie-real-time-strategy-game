using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSMapEditor
{
    public interface MapEditorCommand
    {
        void Do();
        void Undo();
        bool CanBeDone();
    }
}
