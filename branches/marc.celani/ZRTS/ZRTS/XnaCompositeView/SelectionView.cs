using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTSModel.GameModel;
using ZRTSModel.EventHandlers;
using ZRTSModel;
using ZRTS.XnaCompositeView.SelectionViewVisitors;

namespace ZRTS.XnaCompositeView
{
    public class SelectionView : XnaUIComponent
    {
        private SelectionState selectionState;

        public CommandView commandBar;


        public SelectionView(Game game, SelectionState selectionState)
            : base(game)
        {
            this.selectionState = selectionState;
            selectionState.SelectionStateChanged += onSelectionChanged;
        }
        
        protected override void onDraw(XnaDrawArgs e)
        {
            // For test purposes only.
            e.SpriteBatch.Draw(((XnaUITestGame)Game).SpriteSheet, e.Location, new Rectangle(0, 0, 1, 1), Color.YellowGreen);
        }




        private void onSelectionChanged(Object sender, SelectionStateChangedArgs e)
        {
            int count = e.SelectedEntities.Count;
            SameSizeChildrenFlowLayout holder = getSelectedEntityUIHolder();

            // Update the holder
            if (holder != null)
            {
                holder.Clear();
                if (count > 1)
                {
                    holder.Visible = true;
                    foreach (ModelComponent component in e.SelectedEntities)
                    {
                        BuildSelectedEntityUIVisitor visitor = new BuildSelectedEntityUIVisitor();
                        component.Accept(visitor);
                        if (visitor.UI != null)
                        {
                            holder.AddChild(visitor.UI);
                        }
                    }
                }
            }
            // Remove current big picture box
            for (int i = 0; i < GetChildren().Count; i++)
            {
                if (GetChildren()[i] is PictureBox)
                {
                    GetChildren()[i].Dispose();
                    RemoveChild(GetChildren()[i]);
                    i--;
                }
            }
            if (count > 0)
            {
                commandBar.activateButtons();  // show commandView if selected

                BuildLargePreviewPictureBoxVisitor visitor = new BuildLargePreviewPictureBoxVisitor();
                e.SelectedEntities[0].Accept(visitor);
                PictureBox bigImage = visitor.PictureBox;
                bigImage.DrawBox = new Rectangle(25, 25, 150, 150);
                AddChild(bigImage);
                if (count == 1 && holder != null)
                {
                    // Use the holder to show unit stats.
                    DisplaySelectedEntityStatsVisitor visitor2 = new DisplaySelectedEntityStatsVisitor();
                    visitor2.Game = (XnaUITestGame)Game;
                    visitor2.Layout = holder;
                    e.SelectedEntities[0].Accept(visitor2);
                }
            }
            else
            {
                commandBar.disableButtons();
            }
        }

        private SameSizeChildrenFlowLayout getSelectedEntityUIHolder()
        {
            SameSizeChildrenFlowLayout holder = null;
            foreach (XnaUIComponent component in GetChildren())
            {
                if (component is SameSizeChildrenFlowLayout)
                {
                    holder = (SameSizeChildrenFlowLayout)component;
                    break;
                }
            }
            return holder;
        }
    }
}
