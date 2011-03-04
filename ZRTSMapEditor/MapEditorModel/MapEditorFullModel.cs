﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;

namespace ZRTSMapEditor.MapEditorModel
{
    public class MapEditorFullModel : ModelComponent
    {
        // TODO: Move to a component in the model (SelectionState)
        public String TileTypeSelected = null;

        public MapEditorFullModel()
        {
            AddChild(new CommandStack());
        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            if (visitor is MapEditorFullModelVisitor)
            {
                ((MapEditorFullModelVisitor)visitor).Visit(this);
            }
            else
            {
                base.Accept(visitor);
            }
        }

        public ScenarioComponent GetScenario()
        {
            foreach (ModelComponent component in GetChildren())
            {
                if (component is ScenarioComponent)
                {
                    return (ScenarioComponent)component;
                }
            }
            return null;
        }

        public CommandStack GetCommandStack()
        {
            foreach (ModelComponent component in GetChildren())
            {
                if (component is CommandStack)
                {
                    return (CommandStack)component;
                }
            }
            return null;
        }

        override public void AddChild(ModelComponent child)
        {
            // Allow only one scenario.
            if (child is ScenarioComponent)
            {
                ScenarioComponent scenario = GetScenario();
                if (scenario != null)
                {
                    RemoveChild(scenario);
                }
            }
            base.AddChild(child);
        }
    }
}
