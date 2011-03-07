using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;

namespace ZRTSMapEditor.MapEditorModel
{
    public class SelectionState : ModelComponent
    {
        private string selectedPlayer = null;
        private string selectedUnitType = null;
        private Type selectionType = null;

        public string SelectedPlayer
        {
            get { return selectedPlayer; }
            set { selectedPlayer = value; }
        }

        public string SelectedUnitType
        {
            get { return selectedUnitType; }
            set { selectedUnitType = value; }
        }

        public Type SelectionType
        {
            get { return selectionType; }
            set { selectionType = value; }
        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            if (visitor is SelectionStateVisitor)
            {
                ((SelectionStateVisitor)visitor).Visit(this);
            }
            else
            {
                base.Accept(visitor);
            }
        }
    }
}
