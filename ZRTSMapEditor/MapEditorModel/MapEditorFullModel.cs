using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;

namespace ZRTSMapEditor.MapEditorModel
{
    /// <summary>
    /// A full representation of the model used in the Map Editor.  Includes a SelectionState, SaveInfo, CommandStack, and Scenario.
    /// It can be assumed that the SelectionState, SafeInfo, and CommandStack will be altered but neither removed nor replaced.
    /// However, the Scenario can be removed or replaced (via Open or New).  Therefore, observers of the Scenario should also observer the
    /// full model in order to ascertain when the scenario has been removed from the model and replaced with a new scenario, so that the
    /// observer may now observe the new scenario (or piece of the scenario).
    /// </summary>
    public class MapEditorFullModel : ModelComponent
    {

        public MapEditorFullModel()
        {
            AddChild(new CommandStack());
            AddChild(new SelectionState());
        }

        /// <summary>
        /// Overrides the Accept method of the ModelComponent abstract class to determine whether or not the visitor is interested
        /// in knowing that the acceptor is a MapEditorFullModel.
        /// </summary>
        /// <param name="visitor"></param>
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

        /// <summary>
        /// Overrides AddChild from ModelComponent to ensure that the model is composed of only one scenario.
        /// </summary>
        /// <param name="child"></param>
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

        public SelectionState GetSelectionState()
        {
            foreach (ModelComponent component in GetChildren())
            {
                if (component is SelectionState)
                {
                    return (SelectionState)component;
                }
            }
            return null;
        }
    }
}
