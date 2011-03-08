using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSMapEditor.MapEditorModel;
using ZRTSModel;

namespace ZRTSMapEditor.UI
{
    class RefreshUIOnModelUpdate : NoOpMapEditorModelVisitor
    {
        private RefreshableUI targetUI;
        
        private RefreshUIOnModelUpdate()
        { }

        public RefreshUIOnModelUpdate(RefreshableUI target)
        {
            targetUI = target;
        }

        override public void Visit(ModelComponent component)
        {
            targetUI.Refresh();
        }
    }
}
