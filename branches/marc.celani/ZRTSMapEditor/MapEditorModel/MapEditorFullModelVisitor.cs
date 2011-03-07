using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSMapEditor.MapEditorModel
{
    /// <summary>
    /// An interface for visiting the MapEditorFullModel.
    /// </summary>
    public interface MapEditorFullModelVisitor
    {
        void Visit(MapEditorFullModel model);
    }
}
