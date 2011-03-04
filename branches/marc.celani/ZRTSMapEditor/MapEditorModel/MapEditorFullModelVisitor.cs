using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSMapEditor.MapEditorModel
{
    public interface MapEditorFullModelVisitor
    {
        void Visit(MapEditorFullModel model);
    }
}
