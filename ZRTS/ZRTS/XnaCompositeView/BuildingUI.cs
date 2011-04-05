using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using Microsoft.Xna.Framework;
using ZRTS.InputEngines;

namespace ZRTS.XnaCompositeView
{
    public class BuildingUI : PictureBox
    {
        private Building building;

        public Building Building
        {
            get { return building; }
        }

        public BuildingUI(Game game, Building building, Rectangle sourceRect)
            : base(game, sourceRect)
        {
            this.building = building;
            this.OnClick += getAttacked;
        }


        private void getAttacked(Object sender, XnaMouseEventArgs e)
        {
            if (e.Bubbled && !e.Handled && e.ButtonPressed == MouseButton.Right)
            {
                ((XnaUITestGame)Game).Controller.TellSelectedUnitsToAttack(building);
                e.Handled = true;
            }
        }

		private void onSelectChanged(Object obj, bool selected)
		{
			this.selected = selected;
		}

		protected override void onDraw(XnaDrawArgs e)
		{
			base.onDraw(e);

			if (selected)
			{
				// Draw a healthbox for the Building.
				Rectangle healthBG = new Rectangle(e.Location.X, e.Location.Y + e.Location.Height - 8, e.Location.Width, 8);
				e.SpriteBatch.Draw(pixel, healthBG, Color.Black);

				int healthWidth = (int)(e.Location.Width * (1.0 * building.CurrentHealth / building.MaxHealth));
				Color healthColor = Color.LimeGreen;

				if (1.0 * building.CurrentHealth / building.MaxHealth < 0.25)
				{
					healthColor = Color.Red;
				}
				else if (1.0 * building.CurrentHealth / building.MaxHealth < 0.5)
				{
					healthColor = Color.Yellow;
				}
				Rectangle healthRect = new Rectangle(e.Location.X, e.Location.Y + e.Location.Height - 8, healthWidth, 8);
				e.SpriteBatch.Draw(pixel, healthRect, healthColor);
				Color neoGreen = new Color(111, 245, 30);

				// Draw a Rectangle around the building to show that it is selected.
				Rectangle leftRect = new Rectangle(e.Location.X, e.Location.Y, 2, e.Location.Height);
				Rectangle rightRect = new Rectangle(e.Location.X + e.Location.Width - 2, e.Location.Y, 2, e.Location.Height);
				Rectangle topRect = new Rectangle(e.Location.X, e.Location.Y, e.Location.Width, 2);
				Rectangle bottomRect = new Rectangle(e.Location.X, e.Location.Y + e.Location.Height - 2, e.Location.Width, 2);
				e.SpriteBatch.Draw(pixel, leftRect, neoGreen);
				e.SpriteBatch.Draw(pixel, topRect, neoGreen);
				e.SpriteBatch.Draw(pixel, rightRect, neoGreen);
				e.SpriteBatch.Draw(pixel, bottomRect, neoGreen);
			}
		}
    }
}
