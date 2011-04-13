﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTSModel.GameModel;
using ZRTSModel.EventHandlers;
using ZRTSModel;
using ZRTS.XnaCompositeView.SelectionViewVisitors;
using Microsoft.Xna.Framework.Graphics;

namespace ZRTS.XnaCompositeView
{
    /// <summary>
    /// SelectionView: Represent a main panel where stat is displayed.
    /// </summary>
    public class SelectionView : XnaUIComponent
    {
        private SelectionState selectionState;

        private PictureBox mainBgPanel;         // background of the panel
        public CommandView commandBar;              // Coordinate with commandBar


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="selectionState"></param>
        public SelectionView(Game game, SelectionState selectionState)
            : base(game)
        {
            this.selectionState = selectionState;
            selectionState.SelectionStateChanged += onSelectionChanged;
            mainBgPanel = new PictureBox(game, new Rectangle(GameConfig.MAINPANEL_START_X, GameConfig.MAINPANEL_START_Y, 730, 200));
            mainBgPanel.DrawBox = new Rectangle(0, 0, 730, 200);
            AddChild(mainBgPanel);
        }
        
        protected override void onDraw(XnaDrawArgs e)
        {
            // For test purposes only.
			Texture2D pixel = new Texture2D(e.SpriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White }); 
        }



        /// <summary>
        /// Event handler when selection has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        if (component is UnitComponent)
                        {
                            UnitComponent temp = (UnitComponent)component;

                            component.Accept(visitor);
                            if (visitor.UI != null)
                            {
                                holder.AddChild(visitor.UI);
                            }
                           
                        }
                        
                    }
                }
            }
            // Remove current big picture box
            for (int i = 0; i < GetChildren().Count; i++)
            {
                if (GetChildren()[i] is PictureBox && GetChildren()[i] != mainBgPanel)
                {
                    GetChildren()[i].Dispose();
                    RemoveChild(GetChildren()[i]);
                    i--;
                }
            }

            if (count > 0)
            {
                BuildLargePreviewPictureBoxVisitor visitor = new BuildLargePreviewPictureBoxVisitor();

                if (e.SelectedEntities[0] is UnitComponent)
                {
                    // Only non-zombie stat being displayed
                    if (!((UnitComponent)e.SelectedEntities[0]).IsZombie)
                    {
                        // Individual unit
                        if (count == 1 && holder != null)
                        {
                            // Use the holder to show unit stats.
                            DisplaySelectedEntityStatsVisitor visitor2 = new DisplaySelectedEntityStatsVisitor();
                            visitor2.Game = (XnaUITestGame)Game;
                            visitor2.Layout = holder;
                            e.SelectedEntities[0].Accept(visitor2);
                        }

                        e.SelectedEntities[0].Accept(visitor);
                        PictureBox bigImage = visitor.PictureBox;
                        bigImage.DrawBox = new Rectangle(25, 25, 150, 150);
                        AddChild(bigImage);
                       

                        commandBar.activateButtons();  // show commandView if selected

                    } // only non zombie
                } // unit
				else if (e.SelectedEntities[0] is Building)
				{
					commandBar.activateProduceUnitButtons();
				}
            }
            else
            {
                    commandBar.disableButtons();
					commandBar.deactivateProduceUnitButtons();
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
