﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTSModel;

namespace ZRTS.XnaCompositeView
{
    public class DrawSelectionBoxStrategy : MapViewLeftButtonStrategy
    {
        private Point mouseDownLocation;
        private bool started = false;
        private TestUIComponent dragBox = null;
        private MapView mapView;

        public DrawSelectionBoxStrategy(MapView mapView)
        {
            this.mapView = mapView;
        }

        public void HandleMouseInput(bool leftButtonPressed, bool rightButtonPressed, Point mouseLocation)
        {
            if (started)
            {
                if (leftButtonPressed)
                {
                    Rectangle newDragBox;
                    newDragBox.X = Math.Min(mouseDownLocation.X, mouseLocation.X);
                    newDragBox.Y = Math.Min(mouseDownLocation.Y, mouseLocation.Y);
                    newDragBox.Width = Math.Abs(mouseDownLocation.X - mouseLocation.X);
                    newDragBox.Height = Math.Abs(mouseDownLocation.Y - mouseLocation.Y);
                    dragBox.DrawBox = newDragBox;
                }
                else
                {
                    mapView.RemoveChild(dragBox);
                    List<ModelComponent> selectedEntities = new List<ModelComponent>();
                    foreach (XnaUIComponent child in mapView.GetChildren())
                    {
                        if (overlapsDragBox(child))
                        {
                            if (child is UnitUI)
                            {
                                selectedEntities.Add(((UnitUI)child).Unit);
                            }
                            else if (child is BuildingUI)
                            {
                                selectedEntities.Add(((BuildingUI)child).Building);
                            }
                        }
                    }
                    ((XnaUITestGame)mapView.Game).Controller.SelectEntities(selectedEntities);
                    started = false;
                }
            }
            else
            {
                if (leftButtonPressed)
                {
                    started = true;
                    dragBox = new TestUIComponent(mapView.Game, new Color(0, 125, 0, 0));
                    dragBox.DrawBox = new Rectangle(mouseLocation.X, mouseLocation.Y, 0, 0);
                    mapView.AddChild(dragBox);
                    mouseDownLocation = mouseLocation;
                }
            }
        }

        private bool overlapsDragBox(XnaUIComponent child)
        {
            // Two rectangles overlap if one of their corners is contained in the other, or if one is completely contained in the other.
            // This can be tested by testing 4 corners in one and one corner in the other.
            Point point1 = new Point(child.DrawBox.X, child.DrawBox.Y);
            if (rectContainsPoint(dragBox.DrawBox, point1))
                return true;
            Point point2 = new Point(child.DrawBox.X + child.DrawBox.Width, child.DrawBox.Y);
            if (rectContainsPoint(dragBox.DrawBox, point2))
                return true;
            Point point3 = new Point(child.DrawBox.X, child.DrawBox.Y + child.DrawBox.Height);
            if (rectContainsPoint(dragBox.DrawBox, point3))
                return true;
            Point point4 = new Point(child.DrawBox.X + child.DrawBox.Width, child.DrawBox.Y + child.DrawBox.Height);
            if (rectContainsPoint(dragBox.DrawBox, point4))
                return true;
            Point point5 = new Point(dragBox.DrawBox.X, dragBox.DrawBox.Y);
            if (rectContainsPoint(child.DrawBox, point5))
                return true;
            return false;
        }

        private bool rectContainsPoint(Rectangle rectangle, Point point)
        {
            return (point.X >= rectangle.X && point.X <= rectangle.X + rectangle.Width && point.Y >= rectangle.Y && point.Y <= rectangle.Y + rectangle.Height);
        }

        public void CancelProgress()
        {
            if (started)
            {
                mapView.RemoveChild(dragBox);
                dragBox = null;
            }
            started = false;
        }
    }
}