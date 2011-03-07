using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSMapEditor.MapEditorModel
{
    /// <summary>
    /// An interface for visiting the SelectionState component.
    /// </summary>
    public interface SelectionStateVisitor
    {
        void Visit(SelectionState selectionState);
    }
}
