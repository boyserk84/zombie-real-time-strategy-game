using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTS.XnaCompositeView;
using Microsoft.Xna.Framework.Input;

namespace ZRTS.InputEngines
{
    /// <summary>
    /// This class, constructed with a given UI Frame, will raise onClick events.
    /// </summary>
    public class MouseInputEngine : GameComponent
    {
        private XnaUIFrame frame;
        private XnaUIComponent leftMouseDownTarget = null;
        private XnaUIComponent rightMouseDownTarget = null;

        public MouseInputEngine(Game game, XnaUIFrame frame)
            : base(game)
        {
            this.frame = frame;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            Point clickPoint = new Point(mouseState.X, mouseState.Y);
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (leftMouseDownTarget == null)
                {
                    leftMouseDownTarget = getTarget(clickPoint);
                }
            }
            else
            {
                if (leftMouseDownTarget != null)
                {
                    XnaUIComponent leftMouseUpTarget = getTarget(clickPoint);
                    XnaUIComponent commonAncestor = getCommonAncestor(leftMouseDownTarget, leftMouseUpTarget);

                    XnaMouseEventArgs e = new XnaMouseEventArgs();
                    e.Target = commonAncestor;
                    e.ClickLocation = clickPoint;
                    e.Handled = false;
                    e.Bubbled = false;
                    e.ButtonPressed = MouseButton.Left;
                    e.SingleTarget = (leftMouseDownTarget == leftMouseUpTarget);
                    frame.Click(e);
                    
                    // Reset the state.
                    leftMouseDownTarget = null;
                }
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                if (rightMouseDownTarget == null)
                {
                    rightMouseDownTarget = getTarget(clickPoint);
                }
            }
            else
            {
                if (rightMouseDownTarget != null)
                {
                    XnaUIComponent rightMouseUpTarget = getTarget(clickPoint);
                    XnaUIComponent commonAncestor = getCommonAncestor(rightMouseDownTarget, rightMouseUpTarget);

                    XnaMouseEventArgs e = new XnaMouseEventArgs();
                    e.Target = commonAncestor;
                    e.ClickLocation = clickPoint;
                    e.Handled = false;
                    e.Bubbled = false;
                    e.ButtonPressed = MouseButton.Right;
                    e.SingleTarget = (rightMouseDownTarget == rightMouseUpTarget);
                    frame.Click(e);
                    
                    // Reset the state.
                    leftMouseDownTarget = null;
                    // Reset the state.
                    rightMouseDownTarget = null;
                }
            }
            base.Update(gameTime);
        }

        private XnaUIComponent getCommonAncestor(XnaUIComponent c1, XnaUIComponent c2)
        {
            List<XnaUIComponent> list1 = new List<XnaUIComponent>();
            XnaUIComponent current = c1;
            while (current != null)
            {
                list1.Add(current);
                current = current.Parent;
            }
            List<XnaUIComponent> list2 = new List<XnaUIComponent>();
            current = c2;
            while (current != null)
            {
                list2.Add(current);
                current = current.Parent;
            }
            int differenceInSize = Math.Abs(list1.Count - list2.Count);
            List<XnaUIComponent> longerList = list2.Count > list1.Count ? list2 : list1;
            while (differenceInSize > 0)
            {
                longerList.RemoveAt(0);
                differenceInSize--;
            }

            XnaUIComponent commonAncestor = null;
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] == list2[i])
                {
                    commonAncestor = list1[i];
                    break;
                }
            }
            return commonAncestor;
        }

        private XnaUIComponent getTarget(Point p)
        {
            XnaUIComponent current = frame;
            bool searching = true;

            while (searching)
            {
                p.X += current.ScrollX;
                p.Y += current.ScrollY;

                searching = false;

                for (int i = current.GetChildren().Count - 1; i >= 0; i--)
                {
                    XnaUIComponent component = current.GetChildren()[i];
                    if (component.Visible)
                    {
                        Rectangle box = new Rectangle(component.DrawBox.X, component.DrawBox.Y, component.DrawBox.Width, component.DrawBox.Height);

                        if (contains(p, box))
                        {
                            current = component;
                            p.X -= component.DrawBox.X;
                            p.Y -= component.DrawBox.Y;
                            searching = true;
                            break;
                        }
                    }
                }
            }
            return current;
        }

        private bool contains(Point p, Rectangle box)
        {
            return ((p.X >= box.X) && (p.X < (box.X + box.Width)) && (p.Y >= box.Y) && (p.Y < (box.Y + box.Height)));
        }
    }
}
