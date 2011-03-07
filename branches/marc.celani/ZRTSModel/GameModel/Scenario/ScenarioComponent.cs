﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
    /// <summary>
    /// A component representing a particular scenario.  Contains a game world.
    /// </summary>
    [Serializable()]
    public class ScenarioComponent : ModelComponent
    {
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

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
