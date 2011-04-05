using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTS.InputEngines
{
    public enum MouseButton
    {
        Left,
        Right
    }

    public class XnaMouseEventArgs : EventArgs
    {
        public XnaCompositeView.XnaUIComponent Target;
        public Microsoft.Xna.Framework.Point ClickLocation;
        public bool Handled;
        public bool Bubbled;
        public bool SingleTarget;
        public MouseButton ButtonPressed;
        //public bool ButtonOver;
       

    }

    public delegate void ClickEventHandler(Object sender, XnaMouseEventArgs args);
    //public delegate void MouseOverEventHandler(Object sender, XnaMouseEventArgs args);
}
