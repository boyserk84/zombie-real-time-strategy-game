﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using Microsoft.Xna.Framework;
using ZRTSModel.EventHandlers;
using ZRTS.XnaCompositeView.UIEventHandlers;
using ZRTS.InputEngines;

namespace ZRTS.XnaCompositeView
{
    public class SelectedEntityUI : XnaUIComponent
    {
        private UnitComponent unit;

        public SelectedEntityUI(Game game, UnitComponent unit)
            : base(game)
        {
            this.unit = unit;
            unit.HPChangedEventHandlers += UpdateHPBar;
            this.SizeChanged += onResize;
            this.OnClick += selectUnit;
        }

        public void UpdateHPBar(Object sender, UnitHPChangedEventArgs args)
        {
            HPBar hpBar = getHPBar();
            hpBar.CurrentHP = args.NewHP;
        }

        private HPBar getHPBar()
        {
            HPBar hpBar = null;
            foreach (XnaUIComponent child in GetChildren())
            {
                if (child is HPBar)
                {
                    hpBar = (HPBar)child;
                    break;
                }
            }
            return hpBar;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unit.HPChangedEventHandlers -= UpdateHPBar;
            }
            base.Dispose(disposing);
        }

        protected override void onDraw(XnaDrawArgs e)
        {
            e.SpriteBatch.Draw(((XnaUITestGame)Game).SpriteSheet, e.Location, new Rectangle(0, 0, 1, 1), Color.Teal);
        }

        private void onResize(Object sender, UISizeChangedEventArgs e)
        {
            // Update the position of the scrollbar.
            HPBar hpBar = getHPBar();
            if (hpBar != null)
            {
                int hpBarHeight = hpBar.DrawBox.Height;
                int hpBarMargin = 3;
                int pictureBoxDimension = e.DrawBox.Height - hpBarHeight - 3 * hpBarMargin;
                hpBar.DrawBox = new Rectangle(hpBar.DrawBox.X, e.DrawBox.Height - hpBarHeight - hpBarMargin, e.DrawBox.Width - (2 * hpBar.DrawBox.X), hpBarHeight);
                // Get picturebox
                TestUIComponent pictureBox = null;
                foreach (XnaUIComponent component in GetChildren())
                {
                    if (component is TestUIComponent)
                    {
                        pictureBox = (TestUIComponent)component;
                        break;
                    }
                }
                if (pictureBox != null)
                {
                    pictureBox.DrawBox = new Rectangle((e.DrawBox.Width - pictureBoxDimension) / 2, 3, pictureBoxDimension, pictureBoxDimension);
                }
            }
        }

        private void selectUnit(Object sender, XnaMouseEventArgs e)
        {
            if (e.Bubbled)
            {
                List<ModelComponent> entities = new List<ModelComponent>();
                entities.Add(unit);
                ((XnaUITestGame)Game).Controller.SelectEntities(entities);
                e.Handled = true;
            }
        }
    }
}