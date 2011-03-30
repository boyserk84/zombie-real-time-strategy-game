using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.EventHandlers;

namespace ZRTSModel.GameModel
{
    public class SelectionState : ModelComponent
    {
        public event SelectionStateChangedHandler SelectionStateChanged;

        List<ModelComponent> selectedEntities = new List<ModelComponent>();

        public List<ModelComponent> SelectedEntities
        {
            get { return selectedEntities; }
        }
        public void SelectEntity(ModelComponent component)
        {
            if (component is UnitComponent || component is Building)
            {
                selectedEntities.Add(component);
                if (SelectionStateChanged != null)
                {
                    SelectionStateChangedArgs e = new SelectionStateChangedArgs();
                    e.SelectedEntities = selectedEntities;
                    SelectionStateChanged(this, e);
                }
            }
        }

        public void ClearSelectionState()
        {
            selectedEntities = new List<ModelComponent>();
            if (SelectionStateChanged != null)
            {
                SelectionStateChangedArgs e = new SelectionStateChangedArgs();
                e.SelectedEntities = new List<ModelComponent>();
                SelectionStateChanged(this, e);
            }

        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
