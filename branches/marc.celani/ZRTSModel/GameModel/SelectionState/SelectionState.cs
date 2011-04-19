using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.EventHandlers;

namespace ZRTSModel.GameModel
{

    /// <summary>
    /// This class will keep track and notify what game units are being selected by the player.
    /// Also, this class acts as event handler to notify the current selection state whether the new set of units being selected.
    /// </summary>
    public class SelectionState : ModelComponent
    {
        public event SelectionStateChangedHandler SelectionStateChanged;

        List<ModelComponent> selectedEntities = new List<ModelComponent>();

        public List<ModelComponent> SelectedEntities
        {
            get { return selectedEntities; }
        }

        /// <summary>
        /// Selected entities
        /// </summary>
        /// <param name="component"></param>
        public void SelectEntity(ModelComponent component)
        {
            if (component is UnitComponent || component is Building)
            {
                selectedEntities.Add(component);
				component.Selected = true;
                if (SelectionStateChanged != null)
                {
                    SelectionStateChangedArgs e = new SelectionStateChangedArgs();
                    e.SelectedEntities = selectedEntities;
                    SelectionStateChanged(this, e);
                }
            }
        }

        /// <summary>
        /// Refresh the selection state
        /// </summary>
        public void ClearSelectionState()
        {
			foreach (ModelComponent m in selectedEntities)
			{
				m.Selected = false;
			}
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
