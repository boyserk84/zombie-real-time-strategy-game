using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using Microsoft.Xna.Framework;
using ZRTSModel.EventHandlers;
using ZRTS.XnaCompositeView.UIEventHandlers;
using ZRTS.InputEngines;
using Microsoft.Xna.Framework.Graphics;

namespace ZRTS.XnaCompositeView
{
    public class SelectedEntityUI : XnaUIComponent
    {
        private UnitComponent unit;
        private Building building;

        public SelectedEntityUI(Game game, UnitComponent unit)
            : base(game)
        {
            this.unit = unit;
            unit.HPChangedEventHandlers += UpdateHPBar;
            this.SizeChanged += onResize;
            this.OnClick += selectUnit;
        }

        public SelectedEntityUI(Game game, Building building)
            : base(game)
        {
            this.building = building;
            this.SizeChanged += onResize;
            this.OnClick += selectBuilding;
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
                if (unit != null)
                {
                    unit.HPChangedEventHandlers -= UpdateHPBar;
                }
                else if (building != null)
                {

                }
            }
            base.Dispose(disposing);
        }

        protected override void onDraw(XnaDrawArgs e)
        {
			Texture2D pixel = new Texture2D(e.SpriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });
            //e.SpriteBatch.Draw(pixel, e.Location, new Rectangle(0, 0, 1, 1), Color.Teal);
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

        private void selectBuilding(Object sender, XnaMouseEventArgs e)
        {
            if (e.Bubbled)
            {
                List<ModelComponent> entities = new List<ModelComponent>();
                entities.Add(building);
                ((XnaUITestGame)Game).Controller.SelectEntities(entities);
                e.Handled = true;
            }
        }
    }
}
