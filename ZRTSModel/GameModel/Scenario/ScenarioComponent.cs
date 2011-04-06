using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ZRTSModel
{
    /// <summary>
    /// A component representing a particular scenario.  Contains a game world.
    /// </summary>
    [Serializable()]
    public class ScenarioComponent : ModelComponent
    {
        public ScenarioComponent()
        {

        }
        public ScenarioComponent(int x, int y)
        {
            AddChild(new Gameworld(x, y));
        }

        public Gameworld GetGameWorld()
        {
            foreach (ModelComponent component in GetChildren())
            {
                if (component is Gameworld)
                {
                    return (Gameworld)component;
                }
            }
            return null;
        }

        ~ScenarioComponent()
        {
            Debug.WriteLine("Scenario Component destructing.");
        }
        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
